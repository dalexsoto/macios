// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.IO;
using Microsoft.Build.Framework;
using Xamarin.Utils;

namespace Xamarin.MacDev.Tasks {
	public class ValidateWatchAppBundleSize : XamarinTask {
		public const long MaximumUncompressedBytes = 75_000_000;

		[Required]
		public string AppBundlePath { get; set; } = "";

		[Output]
		public long UncompressedBytes { get; set; }

		public override bool Execute ()
		{
			if (Platform != ApplePlatform.WatchOS || !Directory.Exists (AppBundlePath)) {
				Log.LogError ("A built watchOS application bundle is required for size validation.");
				return false;
			}
			UncompressedBytes = 0;
			foreach (var file in Directory.EnumerateFiles (AppBundlePath, "*", SearchOption.AllDirectories))
				UncompressedBytes = checked(UncompressedBytes + new FileInfo (file).Length);
			Log.LogMessage (MessageImportance.Normal, "Uncompressed watchOS app size: {0} bytes.", UncompressedBytes);
			if (UncompressedBytes > MaximumUncompressedBytes) {
				Log.LogError ("The watchOS app is {0} bytes uncompressed, exceeding the {1}-byte publishing limit. Reduce app resources, enable trimming, or use NativeAOT.", UncompressedBytes, MaximumUncompressedBytes);
				return false;
			}
			return true;
		}
	}
}
