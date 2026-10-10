using System;
using System.IO;

using NUnit.Framework;

using Xamarin.Tests;
using Xamarin.Utils;

namespace Xamarin.MacDev.Tasks {
	[TestFixture]
	public class DetectSdkLocationsTaskTests : TestBase {
		[TestCase (false, "WatchOS", "watchos")]
		[TestCase (true, "WatchSimulator", "watchos-simulator")]
		public void WatchPlatformIdentity (bool simulator, string sdkPlatform, string minimumVersionPlatform)
		{
			var framework = TargetFramework.Parse (TargetFramework.DotNet_watchOS_String);
			Assert.That (framework.Platform, Is.EqualTo (ApplePlatform.WatchOS));
			Assert.That (TargetFramework.IsValidFramework (framework), Is.True);
			Assert.That (ApplePlatformExtensions.Parse ("watchOS"), Is.EqualTo (ApplePlatform.WatchOS));
			Assert.That (ApplePlatform.WatchOS.AsString (), Is.EqualTo ("watchOS"));
			Assert.That (ApplePlatform.WatchOS.ToFramework ("net11.0"), Is.EqualTo ("net11.0-watchos"));
			Assert.That (ApplePlatform.WatchOS.IsDesktop (), Is.False);
			Assert.That (PlatformFrameworkHelper.GetOperatingSystem (TargetFramework.DotNet_watchOS_String), Is.EqualTo ("watchos"));
			Assert.That (PlatformFrameworkHelper.GetSdkPlatform (ApplePlatform.WatchOS, simulator), Is.EqualTo (sdkPlatform));
			Assert.That (PlatformFrameworkHelper.GetMinimumVersionArgument (TargetFramework.DotNet_watchOS_String, simulator, "26.0"), Is.EqualTo ($"-m{minimumVersionPlatform}-version-min=26.0"));
			Assert.That (PlatformFrameworkHelper.GetAppManifestPath (ApplePlatform.WatchOS, "Watch.app"), Is.EqualTo (Path.Combine ("Watch.app", "Info.plist")));
		}

		[Test]
		public void InvalidXamarinSdkRoot ()
		{
			Configuration.IgnoreIfIgnoredPlatform (ApplePlatform.iOS);
			var task = CreateTask<DetectSdkLocations> ();
			task.XamarinSdkRoot = "XYZ";
			task.TargetFrameworkMoniker = TargetFramework.DotNet_iOS_String;
			ExecuteTask (task, 1);

			Assert.That (task.XamarinSdkRoot, Is.EqualTo ("XYZ"), "#1");
		}
	}
}
