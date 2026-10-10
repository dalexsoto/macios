// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Framework;
using Xamarin.Utils;

namespace Xamarin.MacDev.Tasks {
	public class CompileWatchSwiftCode : XamarinTask {
		[Required]
		public ITaskItem [] Sources { get; set; } = [];

		[Required]
		public string OutputFile { get; set; } = "";

		[Required]
		public string SdkRoot { get; set; } = "";

		[Required]
		public string MinimumOSVersion { get; set; } = "";

		[Required]
		public string InteropHeader { get; set; } = "";

		[Required]
		public string GeneratedHeader { get; set; } = "";

		[Required]
		public string ModuleCachePath { get; set; } = "";

		public bool SdkIsSimulator { get; set; }
		public bool Optimize { get; set; }

		public override bool Execute ()
		{
			if (Platform != ApplePlatform.WatchOS) {
				Log.LogError ("CompileWatchSwiftCode requires a watchOS target.");
				return false;
			}
			if (Sources.Length == 0 || !File.Exists (InteropHeader) || !Directory.Exists (SdkRoot)) {
				Log.LogError ("Swift compilation requires source files, the watchOS SDK, and the xamarin/watchos.h interop header.");
				return false;
			}
			if (!Version.TryParse (MinimumOSVersion, out _)) {
				Log.LogError ("Invalid watchOS deployment version '{0}'.", MinimumOSVersion);
				return false;
			}
			foreach (var source in Sources) {
				if (!File.Exists (source.ItemSpec)) {
					Log.LogError ("Swift source file '{0}' does not exist.", source.ItemSpec);
					return false;
				}
			}

			var outputDirectory = Path.GetDirectoryName (OutputFile);
			if (!string.IsNullOrEmpty (outputDirectory))
				Directory.CreateDirectory (outputDirectory);
			Directory.CreateDirectory (ModuleCachePath);

			var target = $"arm64-apple-watchos{MinimumOSVersion}" + (SdkIsSimulator ? "-simulator" : "");
			var arguments = new List<string> {
				"swiftc", "-parse-as-library", "-whole-module-optimization", "-emit-object",
				"-module-name", "DotNetWatchApplication",
				"-module-cache-path", ModuleCachePath,
				"-sdk", SdkRoot,
				"-target", target,
				"-import-objc-header", InteropHeader,
				"-emit-objc-header", "-emit-objc-header-path", GeneratedHeader,
				"-g", Optimize ? "-O" : "-Onone",
				"-o", OutputFile,
			};
			foreach (var source in Sources)
				arguments.Add (source.ItemSpec);

			ExecuteAsync ("xcrun", arguments).GetAwaiter ().GetResult ();
			if (!Log.HasLoggedErrors && (!File.Exists (OutputFile) || !File.Exists (GeneratedHeader))) {
				Log.LogError ("Swift compilation did not produce both the native object and Objective-C export header.");
				return false;
			}
			// swiftc preserves an unchanged header's timestamp, but both declared outputs must be current.
			if (!Log.HasLoggedErrors)
				File.SetLastWriteTimeUtc (GeneratedHeader, DateTime.UtcNow);
			return !Log.HasLoggedErrors;
		}
	}
}
