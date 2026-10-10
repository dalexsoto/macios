// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

#nullable enable

using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Xamarin.Utils;

namespace Xamarin.MacDev.Tasks {
	public class ResolveWatchReadyToRunCompiler : XamarinTask {
		[Required]
		public string RuntimeIdentifier { get; set; } = "";

		[Required]
		public ITaskItem [] Crossgen2Packs { get; set; } = [];

		public string? PerfmapFormatVersion { get; set; }

		[Output]
		public ITaskItem? Crossgen2Tool { get; set; }

		public override bool Execute ()
		{
			if (Platform != ApplePlatform.WatchOS || (RuntimeIdentifier != "watchos-arm64" && RuntimeIdentifier != "watchossimulator-arm64")) {
				Log.LogError ("ReadyToRun for watchOS requires watchos-arm64 or watchossimulator-arm64.");
				return false;
			}
			if (Crossgen2Packs.Length != 1 || Crossgen2Packs [0].GetMetadata ("RuntimeIdentifier") != "osx-arm64") {
				Log.LogError ("watchOS requires the Apple Silicon Crossgen2 host pack from its installed workload.");
				return false;
			}
			var packageDirectory = Crossgen2Packs [0].GetMetadata ("PackageDirectory");
			if (string.IsNullOrEmpty (packageDirectory)) {
				Log.LogError ("The watchOS Crossgen2 package directory was not resolved.");
				return false;
			}
			var compiler = Path.Combine (packageDirectory, "tools", "crossgen2");
			if (!File.Exists (compiler)) {
				Log.LogError ("The watchOS Crossgen2 compiler '{0}' does not exist.", compiler);
				return false;
			}
			Crossgen2Tool = new TaskItem (compiler);
			Crossgen2Tool.SetMetadata ("IsVersion5", "false");
			Crossgen2Tool.SetMetadata ("TargetOS", RuntimeIdentifier == "watchos-arm64" ? "watchos" : "watchossimulator");
			Crossgen2Tool.SetMetadata ("TargetArch", "arm64");
			if (!string.IsNullOrEmpty (PerfmapFormatVersion))
				Crossgen2Tool.SetMetadata ("PerfmapFormatVersion", PerfmapFormatVersion);
			return true;
		}
	}
}
