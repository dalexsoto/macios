// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Xamarin.Utils;

namespace Xamarin.MacDev.Tasks {
	public class PrepareWatchAppLaunch : XamarinTask {
		[Required]
		public ITaskItem [] Devices { get; set; } = [];

		[Required]
		public string AppBundlePath { get; set; } = "";

		[Required]
		public string BundleIdentifier { get; set; } = "";

		public string? Device { get; set; }
		public bool SdkIsSimulator { get; set; }
		public bool WaitForExit { get; set; } = true;
		public string? StandardOutputPath { get; set; }
		public string? StandardErrorPath { get; set; }
		public ITaskItem [] EnvironmentVariables { get; set; } = [];

		[Output]
		public string RunArguments { get; set; } = "";

		[Output]
		public ITaskItem [] DeployCommands { get; set; } = [];

		public override bool Execute ()
		{
			if (Platform != ApplePlatform.WatchOS || !Directory.Exists (AppBundlePath)) {
				Log.LogError ("Build the watchOS application before deploying it.");
				return false;
			}
			var candidates = Devices.Where (item => item.GetMetadata ("Type") == (SdkIsSimulator ? "Simulator" : "Device")).ToArray ();
			var selected = string.IsNullOrEmpty (Device)
				? candidates.FirstOrDefault (item => item.GetMetadata ("Status") == "Booted") ?? candidates.FirstOrDefault ()
				: candidates.FirstOrDefault (item => string.Equals (item.GetMetadata ("UDID"), Device, StringComparison.OrdinalIgnoreCase)
					|| string.Equals (item.GetMetadata ("Name"), Device, StringComparison.OrdinalIgnoreCase));
			if (selected is null) {
				Log.LogError ("No compatible watchOS {0} matches '{1}'. Use ComputeAvailableDevices and select one with -p:Device=<identifier>.", SdkIsSimulator ? "simulator" : "device", Device ?? "");
				return false;
			}
			if (!SdkIsSimulator && (!string.IsNullOrEmpty (StandardOutputPath) || !string.IsNullOrEmpty (StandardErrorPath))) {
				Log.LogError ("Device console output must be captured from dotnet run; StandardOutputPath and StandardErrorPath are simulator-only.");
				return false;
			}
			var identifier = selected.GetMetadata ("UDID");
			var prefix = new List<string> { $"DEVELOPER_DIR={SdkDevPath}", "/usr/bin/xcrun" };
			var commands = new List<ITaskItem> ();
			void AddCommand (params string [] arguments)
			{
				commands.Add (new TaskItem ("/usr/bin/env " + StringUtils.FormatArguments (prefix.Concat (arguments).ToArray ())));
			}
			if (SdkIsSimulator) {
				if (selected.GetMetadata ("Status") != "Booted")
					AddCommand ("simctl", "boot", identifier);
				AddCommand ("simctl", "bootstatus", identifier, "-b");
				AddCommand ("simctl", "install", identifier, AppBundlePath);
			} else {
				AddCommand ("devicectl", "device", "install", "app", "--device", identifier, AppBundlePath);
			}
			var run = new List<string> { $"DEVELOPER_DIR={SdkDevPath}" };
			foreach (var variable in EnvironmentVariables) {
				var name = variable.ItemSpec;
				if (string.IsNullOrEmpty (name) || name.Contains ('=') || name.Contains ('\0')) {
					Log.LogError ("Invalid application environment variable name '{0}'.", name);
					return false;
				}
				if (name.StartsWith ("DOTNET_WATCH", StringComparison.Ordinal)) {
					Log.LogError ("Hot Reload and dotnet watch are not supported on watchOS.");
					return false;
				}
				run.Add ($"{(SdkIsSimulator ? "SIMCTL_CHILD_" : "DEVICECTL_CHILD_")}{name}={variable.GetMetadata ("Value")}");
			}
			run.Add ("/usr/bin/xcrun");
			run.AddRange (SdkIsSimulator
				? new [] { "simctl", "launch", "--terminate-running-process" }
				: new [] { "devicectl", "device", "process", "launch", "--device", identifier, "--terminate-existing" });
			if (WaitForExit)
				run.Add (SdkIsSimulator ? "--console-pty" : "--console");
			if (!string.IsNullOrEmpty (StandardOutputPath))
				run.Add ("--stdout=" + StandardOutputPath);
			if (!string.IsNullOrEmpty (StandardErrorPath))
				run.Add ("--stderr=" + StandardErrorPath);
			if (SdkIsSimulator)
				run.Add (identifier);
			run.Add (BundleIdentifier);
			if (!SdkIsSimulator)
				run.Add ("--");
			RunArguments = StringUtils.FormatArguments (run);
			DeployCommands = commands.ToArray ();
			Log.LogMessage (MessageImportance.High, "Using watchOS {0}: {1}", SdkIsSimulator ? "simulator" : "device", selected.GetMetadata ("Name"));
			return true;
		}
	}
}
