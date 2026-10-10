using System.Text.Json;

#nullable enable

namespace Xamarin.Tests {
	public enum TemplateLanguage {
		CSharp,
		FSharp,
		VisualBasic,
	}

	public static class TemplateLanguageExtensions {
		public static string AsString (this TemplateLanguage @this)
		{
			return @this switch {
				TemplateLanguage.CSharp => "csharp",
				TemplateLanguage.FSharp => "fsharp",
				TemplateLanguage.VisualBasic => "visualbasic",
				_ => throw new NotImplementedException ($"'{@this} is not implemented.'")
			};
		}

		public static string AsFileExtension (this TemplateLanguage @this)
		{
			return @this switch {
				TemplateLanguage.CSharp => "csproj",
				TemplateLanguage.FSharp => "fsproj",
				TemplateLanguage.VisualBasic => "vbproj",
				_ => throw new NotImplementedException ($"'{@this} is not implemented.'")
			};
		}

		public static string AsLanguageIdentifier (this TemplateLanguage @this)
		{
			return @this switch {
				TemplateLanguage.CSharp => "C#",
				TemplateLanguage.FSharp => "F#",
				TemplateLanguage.VisualBasic => "VB",
				_ => throw new NotImplementedException ($"'{@this} is not implemented.'")
			};
		}
	}

	[TestFixture]
	public class TemplateTest : TestBaseClass {
		public struct TemplateInfo {
			public readonly ApplePlatform Platform;
			public readonly TemplateLanguage? Language;
			public readonly string Template;
			public readonly bool Execute;
			public readonly TemplateType TemplateType;

			public TemplateInfo (ApplePlatform platform, string template, TemplateLanguage? language = null, bool execute = false)
			{
				Platform = platform;
				Language = language;
				Template = template;
				Execute = execute;
				TemplateType = ParseConfig (platform, language, template);
			}

			static TemplateType ParseConfig (ApplePlatform platform, TemplateLanguage? language, string template)
			{
				var languageDir = language?.AsString () ?? "";
				var jsonPath = Path.Combine (Configuration.SourceRoot, "dotnet", "Templates", $"Microsoft.{platform.AsString ()}.Templates", template, languageDir, ".template.config", "template.json");

				var options = new JsonSerializerOptions {
					PropertyNameCaseInsensitive = true,
					IncludeFields = true,
					AllowTrailingCommas = false, // for max compat
				};
				try {
					var json = JsonSerializer.Deserialize<TemplateConfig> (File.ReadAllText (jsonPath), options);
					var type = json?.Tags?.Type;
					return Enum.Parse<TemplateType> (type!, true);
				} catch (Exception e) {
					throw new Exception ($"Failed to parse {jsonPath}", e);
				}
			}

			public override string ToString ()
			{
				return TemplateWithLanguage;
			}

			public string TemplateWithLanguage {
				get => Language.HasValue ? $"{Template}-{Language.Value.AsString ()}" : Template;
			}
		}

		public enum TemplateType {
			Project,
			Item,
		}

		public static TemplateInfo [] Templates = {
			/* project templates */
			new TemplateInfo (ApplePlatform.iOS, "ios", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.iOS, "ios", TemplateLanguage.FSharp),
			new TemplateInfo (ApplePlatform.iOS, "ios", TemplateLanguage.VisualBasic),
			new TemplateInfo (ApplePlatform.iOS, "ios-tabbed"),
			new TemplateInfo (ApplePlatform.iOS, "ioslib", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.iOS, "ioslib", TemplateLanguage.VisualBasic),
			new TemplateInfo (ApplePlatform.iOS, "iosbinding", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.iOS, "ios-notification-content-extension", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.iOS, "ios-notification-service-extension", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.iOS, "iostest", TemplateLanguage.CSharp),

			new TemplateInfo (ApplePlatform.TVOS, "tvos", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.TVOS, "tvos", TemplateLanguage.VisualBasic),
			new TemplateInfo (ApplePlatform.TVOS, "tvoslib", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.TVOS, "tvoslib", TemplateLanguage.VisualBasic),
			new TemplateInfo (ApplePlatform.TVOS, "tvosbinding", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.TVOS, "tvostest", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.WatchOS, "watch", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.WatchOS, "watchoslib", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.WatchOS, "watchosbinding", TemplateLanguage.CSharp),

			new TemplateInfo (ApplePlatform.MacCatalyst, "maccatalyst", TemplateLanguage.CSharp, execute: true),
			new TemplateInfo (ApplePlatform.MacCatalyst, "maccatalyst", TemplateLanguage.VisualBasic, execute: true),
			new TemplateInfo (ApplePlatform.MacCatalyst, "maccatalystlib", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.MacCatalyst, "maccatalystlib", TemplateLanguage.VisualBasic),
			new TemplateInfo (ApplePlatform.MacCatalyst, "maccatalystbinding", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.MacCatalyst, "maccatalysttest", TemplateLanguage.CSharp),

			new TemplateInfo (ApplePlatform.MacOSX, "macos", TemplateLanguage.CSharp, execute: true),
			new TemplateInfo (ApplePlatform.MacOSX, "macos", TemplateLanguage.VisualBasic, execute: true),
			new TemplateInfo (ApplePlatform.MacOSX, "macoslib", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.MacOSX, "macoslib", TemplateLanguage.VisualBasic),
			new TemplateInfo (ApplePlatform.MacOSX, "macosbinding", TemplateLanguage.CSharp),
			new TemplateInfo (ApplePlatform.MacOSX, "macostest", TemplateLanguage.CSharp),

			/* item templates */
			new TemplateInfo (ApplePlatform.iOS, "ios-controller"),
			new TemplateInfo (ApplePlatform.iOS, "ios-storyboard"),
			new TemplateInfo (ApplePlatform.iOS, "ios-view"),
			new TemplateInfo (ApplePlatform.iOS, "ios-viewcontroller"),

			new TemplateInfo (ApplePlatform.TVOS, "tvos-controller"),
			new TemplateInfo (ApplePlatform.TVOS, "tvos-storyboard"),
			new TemplateInfo (ApplePlatform.TVOS, "tvos-view"),
			new TemplateInfo (ApplePlatform.TVOS, "tvos-viewcontroller"),

			new TemplateInfo (ApplePlatform.MacCatalyst, "maccatalyst-controller"),
			new TemplateInfo (ApplePlatform.MacCatalyst, "maccatalyst-storyboard"),
			new TemplateInfo (ApplePlatform.MacCatalyst, "maccatalyst-view"),
			new TemplateInfo (ApplePlatform.MacCatalyst, "maccatalyst-viewcontroller"),

			new TemplateInfo (ApplePlatform.MacOSX, "macos-controller"),
			new TemplateInfo (ApplePlatform.MacOSX, "macos-storyboard"),
			new TemplateInfo (ApplePlatform.MacOSX, "macos-view"),
			new TemplateInfo (ApplePlatform.MacOSX, "macos-viewcontroller"),
		};

		public static TemplateInfo [] GetProjectTemplates ()
		{
			return Templates.Where (v => v.TemplateType == TemplateType.Project).ToArray ();
		}

		public class TemplateConfig {
			public string? Name;
			public string? ShortName;
			public TemplateConfigTag? Tags;
		}

		public class TemplateConfigTag {
			public string? Language;
			public string? Type;
		}

		[Test]
		public void AreAllTemplatesListed ()
		{
			var allListedTemplates = Templates.Select (v => v.TemplateWithLanguage).ToArray ();
			var allTemplates = new List<string> ();

			foreach (var platform in Enum.GetValues<ApplePlatform> ()) {
				var dir = Path.Combine (Configuration.SourceRoot, "dotnet", "Templates", $"Microsoft.{platform.AsString ()}.Templates");
				if (!Directory.Exists (dir))
					continue;

				var templateDirectories = Directory.GetDirectories (dir);

				// read the template's configuration to figure out if it's a project template, and if not, skip it
				foreach (var template in templateDirectories) {
					var templateDir = Path.Combine (dir, template);
					var hasFoundLangTemplate = false;

					foreach (var language in Enum.GetValues<TemplateLanguage> ()) {
						var langDir = Path.Combine (templateDir, language.AsString ());
						if (!Directory.Exists (langDir))
							continue;

						var jsonPath = Path.Combine (langDir, ".template.config", "template.json");
						Assert.That (jsonPath, Does.Exist, "Config file must exist");
						if (!File.Exists (jsonPath))
							continue;

						allTemplates.Add ($"{Path.GetFileName (template)}-{language.AsString ()}");
						hasFoundLangTemplate = true;
					}

					if (!hasFoundLangTemplate) {
						var rootJsonPath = Path.Combine (templateDir, ".template.config", "template.json");
						Assert.That (rootJsonPath, Does.Exist, "Config file must exist");
						if (!File.Exists (rootJsonPath))
							continue;

						allTemplates.Add (Path.GetFileName (template));
					}
				}
			}
			Assert.That (allListedTemplates, Is.EquivalentTo (allTemplates), "The listed templates here and the templates on disk don't match");
		}

		[Test]
		[TestCaseSource (nameof (GetProjectTemplates))]
		public void CreateAndBuildProjectTemplate (TemplateInfo info)
		{
			Configuration.IgnoreIfIgnoredPlatform (info.Platform);

			var language = info.Language ?? TemplateLanguage.CSharp;

			var tmpDir = Cache.CreateTemporaryDirectory ();
			var outputDir = Path.Combine (tmpDir, info.Template);
			DotNet.AssertNew (outputDir, info.Template, language: language.AsLanguageIdentifier ());
			var proj = Path.Combine (outputDir, $"{info.Template}.{language.AsFileExtension ()}");
			AssertCurrentTargetFramework (proj, info.Platform);
			var properties = GetDefaultProperties ();
			var rv = DotNet.AssertBuild (proj, properties);
			var warnings = BinLog.GetBuildLogWarnings (rv.BinLogPath).FilterWarnings (info.Platform).Select (v => v.Message);
			Assert.That (warnings, Is.Empty, $"Build warnings:\n\t{string.Join ("\n\t", warnings)}");

			if (info.Execute) {
				var platform = info.Platform;
				var runtimeIdentifiers = GetDefaultRuntimeIdentifier (platform);

				Assert.That (CanExecute (info.Platform, runtimeIdentifiers), Is.True, "Must be executable to execute!");

				// First add some code to exit the template if it launches successfully.
				InsertCodeToExitAppAfterLaunch (language, outputDir);

				// Build the sample
				rv = DotNet.AssertBuild (proj, properties);

				// There should still not be any warnings
				warnings = BinLog.GetBuildLogWarnings (rv.BinLogPath).FilterWarnings (info.Platform).Select (v => v.Message);
				Assert.That (warnings, Is.Empty, $"Build warnings (2):\n\t{string.Join ("\n\t", warnings)}");

				var appPath = GetAppPath (proj, platform, runtimeIdentifiers);
				var appExecutable = GetNativeExecutable (platform, appPath);
				ExecuteWithMagicWordAndAssert (appExecutable);
			}
		}

		[TestCase ("UseMonoRuntime", "true", "watchOS applications support CoreCLR and NativeAOT, not Mono")]
		[TestCase ("UseInterpreter", "true", "watchOS CoreCLR already includes its interpreter")]
		[TestCase ("RuntimeIdentifier", "watchos-arm64_32", "watchOS only supports the watchos-arm64 and watchossimulator-arm64")]
		[TestCase ("RuntimeIdentifier", "watchossimulator-x64", "watchOS only supports the watchos-arm64 and watchossimulator-arm64")]
		[TestCase ("IsAppExtension", "true", "modern single-target watch applications")]
		[TestCase ("DebuggerSupport", "true", "Managed debugging and Hot Reload are not supported")]
		[TestCase ("MtouchHttpClientHandler", "CFNetworkHandler", "watchOS only supports NSUrlSessionHandler")]
		[TestCase ("WatchApplicationEntryPoint", "invalid:name", "WatchApplicationEntryPoint must be a C identifier")]
		[TestCase ("EnableDefaultwatchOSItems", "false", "requires a SwiftCompile source")]
		[TestCase ("PrepareAssemblies", "false", "watchOS requires PrepareAssemblies=true")]
		public void WatchRejectsUnsupportedConfiguration (string property, string value, string expectedError)
		{
			Configuration.IgnoreIfIgnoredPlatform (ApplePlatform.WatchOS);
			var directory = Cache.CreateTemporaryDirectory ();
			DotNet.AssertNew (directory, "watch", language: "C#");
			var project = Directory.GetFiles (directory, "*.csproj").Single ();
			var properties = GetDefaultProperties ();
			properties [property] = value;
			var result = DotNet.AssertBuildFailure (project, properties);
			var errors = BinLog.GetBuildLogErrors (result.BinLogPath);
			Assert.That (errors, Has.Some.Matches<BuildLogEvent> (error => error.Message?.Contains (expectedError, StringComparison.Ordinal) == true));
		}

		[Test]
		public void WatchSwiftIncrementalAndClean ()
		{
			Configuration.IgnoreIfIgnoredPlatform (ApplePlatform.WatchOS);
			var directory = Cache.CreateTemporaryDirectory ();
			DotNet.AssertNew (directory, "watch", language: "C#");
			var project = Directory.GetFiles (directory, "*.csproj").Single ();
			var properties = GetDefaultProperties ("watchossimulator-arm64");
			DotNet.AssertBuild (project, properties);
			var intermediate = Path.Combine (directory, "obj", "Debug", Configuration.DotNetTfm + "-watchos", "watchossimulator-arm64");
			var swiftObject = Path.Combine (intermediate, "swift", "WatchApplication.o");
			var generatedHeader = Path.Combine (intermediate, "swift", "WatchApplication-Swift.h");
			Assert.That (File.Exists (swiftObject) && File.Exists (generatedHeader), Is.True);
			var originalTime = File.GetLastWriteTimeUtc (swiftObject);
			DotNet.AssertBuild (project, properties);
			Assert.That (File.GetLastWriteTimeUtc (swiftObject), Is.EqualTo (originalTime), "No-op build must not recompile Swift");

			var source = Path.Combine (directory, "App.swift");
			File.WriteAllText (source, File.ReadAllText (source).Replace ("spacing: 12", "spacing: 13", StringComparison.Ordinal));
			DotNet.AssertBuild (project, properties);
			Assert.That (File.GetLastWriteTimeUtc (swiftObject), Is.GreaterThan (originalTime), "Swift edit must rebuild its object");
			var editedTime = File.GetLastWriteTimeUtc (swiftObject);
			DotNet.AssertBuild (project, properties);
			Assert.That (File.GetLastWriteTimeUtc (swiftObject), Is.EqualTo (editedTime), "Unchanged Swift export header must not cause repeated compilation");
			var resource = Path.Combine (directory, "PrivacyInfo.xcprivacy");
			File.AppendAllText (resource, "\n");
			DotNet.AssertBuild (project, properties);
			Assert.That (File.GetLastWriteTimeUtc (swiftObject), Is.EqualTo (editedTime), "Resource edit must not recompile Swift");
			DotNet.AssertBuild (project, properties, target: "Clean");
			Assert.That (Directory.Exists (Path.Combine (intermediate, "swift")), Is.False, "Clean must remove Swift object, headers and module cache");
		}

		[Test]
		public void WatchRejectsRuntimeBlockSignature ()
		{
			Configuration.IgnoreIfIgnoredPlatform (ApplePlatform.WatchOS);
			var directory = Cache.CreateTemporaryDirectory ();
			DotNet.AssertNew (directory, "watch", language: "C#");
			var project = Directory.GetFiles (directory, "*.csproj").Single ();
			var program = Path.Combine (directory, "Program.cs");
			File.WriteAllText (program, File.ReadAllText (program).Replace ("WatchApplication.Run (", "UnsupportedBlock.Create (typeof (UnsupportedBlock).GetMethod (\"Callback\")!); WatchApplication.Run (", StringComparison.Ordinal));
			File.WriteAllText (Path.Combine (directory, "UnsupportedBlock.cs"), """
				using System;
				using System.Reflection;
				using System.Runtime.InteropServices;
				using ObjCRuntime;
				public static class UnsupportedBlock {
					[UnmanagedCallersOnly]
					public static void Callback (IntPtr block) { }
					public static unsafe void Create (MethodInfo method) {
						using var block = new BlockLiteral ((delegate* unmanaged<IntPtr, void>) &Callback, new object (), method);
					}
				}
				""");
			var properties = GetDefaultProperties ("watchossimulator-arm64");
			properties ["AllowUnsafeBlocks"] = "true";
			var result = DotNet.AssertBuildFailure (project, properties);
			var errors = BinLog.GetBuildLogErrors (result.BinLogPath);
			Assert.That (errors, Has.Some.Matches<BuildLogEvent> (error => error.Message?.Contains ("dynamic Objective-C registration is unavailable", StringComparison.Ordinal) == true));
		}

		[Test]
		public void WatchRejectsIncorrectNativeOwnership ()
		{
			Configuration.IgnoreIfIgnoredPlatform (ApplePlatform.WatchOS);
			var directory = Cache.CreateTemporaryDirectory ();
			DotNet.AssertNew (directory, "watch", language: "C#");
			var project = Directory.GetFiles (directory, "*.csproj").Single ();
			File.WriteAllText (Path.Combine (directory, "InvalidCopy.cs"), """
				using Foundation;
				using ObjCRuntime;
				[Register ("WatchInvalidCopy")]
				public sealed class InvalidCopy : NSObject, INSCopying {
					[Export ("copyWithZone:")]
					public NSObject Copy (NSZone? zone) => new InvalidCopy ();
				}
				""");
			var result = DotNet.AssertBuildFailure (project, GetDefaultProperties ("watchossimulator-arm64"));
			var errors = BinLog.GetBuildLogErrors (result.BinLogPath);
			Assert.That (errors, Has.Some.Matches<BuildLogEvent> (error => error.Message?.Contains ("must declare [return: Release]", StringComparison.Ordinal) == true));
		}

		[Test]
		[TestCase (ApplePlatform.iOS, TemplateLanguage.CSharp)]
		[TestCase (ApplePlatform.iOS, TemplateLanguage.FSharp)]
		[TestCase (ApplePlatform.TVOS, TemplateLanguage.CSharp)]
		[TestCase (ApplePlatform.MacCatalyst, TemplateLanguage.CSharp)]
		[TestCase (ApplePlatform.MacOSX, TemplateLanguage.CSharp)]
		public void CreateAndBuildItemTemplates (ApplePlatform platform, TemplateLanguage language)
		{
			Configuration.IgnoreIfIgnoredPlatform (platform);

			// We create a new project from the basic project template, and then we add all the
			// item templates for the given platforms. This is faster than testing the item templates
			// one-by-one.

			bool IsMatching (TemplateLanguage lang, TemplateLanguage? value)
			{
				if (lang == TemplateLanguage.CSharp)
					return value is null or TemplateLanguage.CSharp;

				return lang == value;
			}

			var info = Templates.Single (v => string.Equals (v.Template, platform.AsString (), StringComparison.OrdinalIgnoreCase) && IsMatching (language, v.Language));
			var itemTemplates = Templates.Where (v => v.TemplateType == TemplateType.Item && v.Platform == platform && IsMatching (language, v.Language));

			var tmpDir = Cache.CreateTemporaryDirectory ();
			var outputDir = Path.Combine (tmpDir, info.Template);
			DotNet.AssertNew (outputDir, info.Template, language: language.AsLanguageIdentifier ());
			var proj = Path.Combine (outputDir, $"{info.Template}.{language.AsFileExtension ()}");
			AssertCurrentTargetFramework (proj, platform);

			foreach (var item in itemTemplates)
				DotNet.AssertNew (outputDir, item.Template, "item_" + item.Template, language: language.AsLanguageIdentifier ());

			var properties = GetDefaultProperties ();
			var rv = DotNet.AssertBuild (proj, properties);
			var warnings = BinLog.GetBuildLogWarnings (rv.BinLogPath).FilterWarnings (platform).Select (v => v.Message);
			Assert.That (warnings, Is.Empty, $"Build warnings:\n\t{string.Join ("\n\t", warnings)}");

			if (info.Execute) {
				var runtimeIdentifiers = GetDefaultRuntimeIdentifier (platform);

				Assert.That (CanExecute (info.Platform, runtimeIdentifiers), Is.True, "Must be executable to execute!");

				// First add some code to exit the template if it launches successfully.
				InsertCodeToExitAppAfterLaunch (language, outputDir);

				// Build the sample
				rv = DotNet.AssertBuild (proj, properties);

				// There should still not be any warnings
				warnings = BinLog.GetBuildLogWarnings (rv.BinLogPath).FilterWarnings (platform).Select (v => v.Message);
				Assert.That (warnings, Is.Empty, $"Build warnings (2):\n\t{string.Join ("\n\t", warnings)}");

				var appPath = GetAppPath (proj, platform, runtimeIdentifiers);
				var appExecutable = GetNativeExecutable (platform, appPath);
				ExecuteWithMagicWordAndAssert (appExecutable);
			}
		}

		static void AssertCurrentTargetFramework (string projectPath, ApplePlatform platform)
		{
			var expectedTargetFramework = $"<TargetFramework>{platform.ToFramework (Configuration.DotNetTfm)}</TargetFramework>";
			Assert.That (File.ReadAllText (projectPath), Does.Contain (expectedTargetFramework), $"The generated project must target the current .NET TFM.");
		}

		static void InsertCodeToExitAppAfterLaunch (TemplateLanguage language, string outputDir)
		{
			switch (language) {
			case TemplateLanguage.CSharp:
				InsertCSharpCodeToExitAppAfterLaunch (outputDir);
				break;
			case TemplateLanguage.FSharp:
				InsertFSharpCodeToExitAppAfterLaunch (outputDir);
				break;
			case TemplateLanguage.VisualBasic:
				InsertVBCodeToExitAppAfterLaunch (outputDir);
				break;
			default:
				throw new NotImplementedException ($"'Inserting {language} code is not implemented.'");
			}
		}

		static void InsertCSharpCodeToExitAppAfterLaunch (string outputDir)
		{
			var mainFile = Path.Combine (outputDir, "Main.cs");
			var mainContents = File.ReadAllText (mainFile);
			var exitSampleWithSuccess = @"NSTimer.CreateScheduledTimer (1, (v) => {
Console.WriteLine (Environment.GetEnvironmentVariable (""MAGIC_WORD""));
Environment.Exit (0);
		});
		";
			var modifiedMainContents =
				mainContents.Replace ("// This is the main entry point of the application.",
					exitSampleWithSuccess);
			Assert.That (mainContents, Is.Not.EqualTo (modifiedMainContents), "Failed to modify the main content");
			File.WriteAllText (mainFile, modifiedMainContents);
		}

		static void InsertFSharpCodeToExitAppAfterLaunch (string outputDir)
		{
			var mainFile = Path.Combine (outputDir, "Main.fs");
			var mainContents = File.ReadAllText (mainFile);

			// Attention: The indentation is important in F#, so to avoid having to deal with that, put everything on a single line.
			var exitSampleWithSuccess =
				@"Foundation.NSTimer.CreateScheduledTimer(1, fun _ -> System.Console.WriteLine(System.Environment.GetEnvironmentVariable(""MAGIC_WORD"")); System.Environment.Exit(0)) |> ignore";

			var modifiedMainContents =
				mainContents.Replace ("// This is the main entry point of the application.",
					exitSampleWithSuccess);
			Assert.That (mainContents, Is.Not.EqualTo (modifiedMainContents), "Failed to modify the main content");
			File.WriteAllText (mainFile, modifiedMainContents);
		}

		static void InsertVBCodeToExitAppAfterLaunch (string outputDir)
		{
			var mainFile = Path.Combine (outputDir, "Main.vb");
			var mainContents = File.ReadAllText (mainFile);
			var exitSampleWithSuccess = @"Foundation.NSTimer.CreateScheduledTimer (1,
Sub (ByVal v)
	Console.WriteLine (Environment.GetEnvironmentVariable (""MAGIC_WORD""))
	Environment.Exit (0)
End Sub
	)
		";
			var modifiedMainContents =
				mainContents.Replace ("' This is the main entry point of the application.",
					exitSampleWithSuccess);
			Assert.That (mainContents, Is.Not.EqualTo (modifiedMainContents), "Failed to modify the main content");
			File.WriteAllText (mainFile, modifiedMainContents);
		}
	}
}
