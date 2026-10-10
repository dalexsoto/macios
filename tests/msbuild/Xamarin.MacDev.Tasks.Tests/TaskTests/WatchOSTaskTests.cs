using System;
using System.IO;
using System.Linq;
using Microsoft.Build.Utilities;
using NUnit.Framework;
using Xamarin.Tests;
using Xamarin.Utils;

namespace Xamarin.MacDev.Tasks {
	[TestFixture]
	public class WatchOSTaskTests : TestBase {
		[TestCase ("watchos-arm64", "watchos")]
		[TestCase ("watchossimulator-arm64", "watchossimulator")]
		public void ReadyToRunUsesRealWatchTarget (string rid, string targetOS)
		{
			var directory = Cache.CreateTemporaryDirectory ();
			Directory.CreateDirectory (Path.Combine (directory, "tools"));
			var compiler = Path.Combine (directory, "tools", "crossgen2");
			File.WriteAllText (compiler, "");
			var pack = new TaskItem ("Microsoft.NETCore.App.Crossgen2.osx-arm64");
			pack.SetMetadata ("RuntimeIdentifier", "osx-arm64");
			pack.SetMetadata ("PackageDirectory", directory);
			var task = CreateTask<ResolveWatchReadyToRunCompiler> ();
			task.TargetFrameworkMoniker = TargetFramework.GetTargetFramework (ApplePlatform.WatchOS).ToString ();
			task.RuntimeIdentifier = rid;
			task.Crossgen2Packs = [pack];
			ExecuteTask (task);
			Assert.That (task.Crossgen2Tool?.ItemSpec, Is.EqualTo (compiler));
			Assert.That (task.Crossgen2Tool?.GetMetadata ("TargetOS"), Is.EqualTo (targetOS));
			Assert.That (task.Crossgen2Tool?.GetMetadata ("TargetArch"), Is.EqualTo ("arm64"));
		}

		[TestCase ("watchos-arm64_32")]
		[TestCase ("watchossimulator-x64")]
		[TestCase ("ios-arm64")]
		public void ReadyToRunRejectsUnsupportedRid (string rid)
		{
			var task = CreateTask<ResolveWatchReadyToRunCompiler> ();
			task.TargetFrameworkMoniker = TargetFramework.GetTargetFramework (ApplePlatform.WatchOS).ToString ();
			task.RuntimeIdentifier = rid;
			ExecuteTask (task, expectedErrorCount: 1);
		}

		[TestCase (true, "Booted", 2)]
		[TestCase (true, "Shutdown", 3)]
		[TestCase (false, "Paired", 1)]
		public void DeploymentUsesWatchNativeTools (bool simulator, string status, int commandCount)
		{
			var directory = Cache.CreateTemporaryDirectory ();
			var device = new TaskItem ("watch-id");
			device.SetMetadata ("UDID", "watch-id");
			device.SetMetadata ("Name", "Watch test");
			device.SetMetadata ("Type", simulator ? "Simulator" : "Device");
			device.SetMetadata ("Status", status);
			var task = CreateTask<PrepareWatchAppLaunch> ();
			task.TargetFrameworkMoniker = TargetFramework.GetTargetFramework (ApplePlatform.WatchOS).ToString ();
			task.Devices = [device];
			task.Device = "watch-id";
			task.SdkIsSimulator = simulator;
			task.AppBundlePath = directory;
			task.BundleIdentifier = "com.example.watch";
			task.WaitForExit = false;
			ExecuteTask (task);
			Assert.That (task.DeployCommands, Has.Length.EqualTo (commandCount));
			Assert.That (task.RunArguments, Does.Contain (simulator ? "simctl launch" : "devicectl device process launch"));
			Assert.That (task.RunArguments, Does.Contain ("watch-id"));
			Assert.That (task.DeployCommands.Last ().ItemSpec, Does.Contain (simulator ? "simctl install" : "devicectl device install app"));
			Assert.That (task.RunArguments, Does.Not.Contain ("mlaunch"));
		}

		[Test]
		public void DeploymentRejectsMissingDevice ()
		{
			var task = CreateTask<PrepareWatchAppLaunch> ();
			task.TargetFrameworkMoniker = TargetFramework.GetTargetFramework (ApplePlatform.WatchOS).ToString ();
			task.AppBundlePath = Cache.CreateTemporaryDirectory ();
			task.BundleIdentifier = "com.example.watch";
			ExecuteTask (task, expectedErrorCount: 1);
		}

		[Test]
		public void MissingWatchProvisioningProfileReportsAnError ()
		{
			var task = CreateTask<EmbedProvisionProfile> ();
			task.TargetFrameworkMoniker = TargetFramework.GetTargetFramework (ApplePlatform.WatchOS).ToString ();
			task.AppBundleDir = Cache.CreateTemporaryDirectory ();
			task.ProvisioningProfile = Guid.NewGuid ().ToString ();
			task.EmbeddedProvisionProfilePath = Path.Combine (task.AppBundleDir, "embedded.mobileprovision");
			ExecuteTask (task, expectedErrorCount: 1);
			Assert.That (File.Exists (task.EmbeddedProvisionProfilePath), Is.False);
		}

		[TestCase (74_999_999L, 0)]
		[TestCase (75_000_000L, 0)]
		[TestCase (75_000_001L, 1)]
		public void PublishedSizeUsesExactByteLimit (long size, int errors)
		{
			var directory = Cache.CreateTemporaryDirectory ();
			var file = Path.Combine (directory, "payload");
			using (var stream = File.Create (file))
				stream.SetLength (size);
			var task = CreateTask<ValidateWatchAppBundleSize> ();
			task.TargetFrameworkMoniker = TargetFramework.GetTargetFramework (ApplePlatform.WatchOS).ToString ();
			task.AppBundlePath = directory;
			ExecuteTask (task, expectedErrorCount: errors);
			Assert.That (task.UncompressedBytes, Is.EqualTo (size));
			File.Delete (file);
		}
	}
}
