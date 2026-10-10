// arguments are: <platform> <outputPath>

using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;

if (args.Length == 3 && args [0] == "--watch-runtime-graph") {
	var graph = JsonNode.Parse (File.ReadAllText (args [1]))?.AsObject ()
		?? throw new InvalidDataException ("The SDK runtime graph must be a JSON object.");
	var runtimes = graph ["runtimes"]?.AsObject ()
		?? throw new InvalidDataException ("The SDK runtime graph does not contain runtime identifiers.");
	runtimes ["watchos"] = new JsonObject { ["#import"] = new JsonArray ("unix") };
	runtimes ["watchos-arm64"] = new JsonObject { ["#import"] = new JsonArray ("watchos", "unix-arm64") };
	runtimes ["watchossimulator"] = new JsonObject { ["#import"] = new JsonArray ("watchos") };
	runtimes ["watchossimulator-arm64"] = new JsonObject { ["#import"] = new JsonArray ("watchossimulator", "watchos", "unix-arm64") };
	File.WriteAllText (args [2], graph.ToJsonString (new JsonSerializerOptions { WriteIndented = true }) + "\n");
	return 0;
}

var expectedArgumentCount = 11;
if (args.Length != expectedArgumentCount) {
	Console.WriteLine ($"Need {expectedArgumentCount} arguments, got {args.Length}");
	return 1;
}

var argumentIndex = 0;
var platform = args [argumentIndex++];
var version = args [argumentIndex++];
var net8Version = args [argumentIndex++];
var runtimeIdentifiers = args [argumentIndex++].Split (new char [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
var outputPath = args [argumentIndex++];
var windowsPlatforms = args [argumentIndex++].Split (new char [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
var hasWindows = Array.IndexOf (windowsPlatforms, platform) >= 0;
var currentApiVersion = args [argumentIndex++];
var supportedApiVersions = args [argumentIndex++].Split (new char [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
var versionsPropsPath = args [argumentIndex++];
var ridNoArch = args [argumentIndex++];
var runtimeVersion = args [argumentIndex++];

var platformLowerCase = platform.ToLowerInvariant ();

var allApiVersions = new List<string> (supportedApiVersions);
allApiVersions = allApiVersions.Select (v => v.Replace ('-', '_')).ToList ();

var versionsPropsTable = File.ReadAllLines (versionsPropsPath).
				Where (v => v.Count (f => f == '>') > 1).
				Select (v => {
					var split = v.Trim ().Split (new char [] { '<', '>', '/' }, StringSplitOptions.RemoveEmptyEntries);
					var name = split [0];
					var value = split [1];
					return new Tuple<string, string> (name, value);
				}).
				ToDictionary (v => v.Item1, v => v.Item2, StringComparer.OrdinalIgnoreCase);

var sortedAllApiVersions = allApiVersions.
				Select (v => {
					v = v.Replace ("net", "");
					v = v [0..v.IndexOf ('_')];
					return v;
				}).
				Select (Version.Parse).
				Distinct ().
				OrderBy (v => v).
				ToArray ();
var earliestDotNetVersion = sortedAllApiVersions.First ().Major;
var latestDotNetVersion = sortedAllApiVersions.Last ().Major;

var failed = false;
using (TextWriter writer = new StreamWriter (outputPath)) {
	writer.WriteLine ($"{{");
	writer.WriteLine ($"	\"version\": \"{version}\",");
	writer.WriteLine ($"	\"workloads\": {{");
	writer.WriteLine ($"		\"{platformLowerCase}\": {{");
	writer.WriteLine ($"			\"description\": \".NET SDK Workload for building {platform} applications.\",");
	writer.WriteLine ($"			\"packs\": [");
	foreach (var tfm in allApiVersions) {
		writer.WriteLine ($"				\"Microsoft.{platform}.Sdk.{(tfm == "net8.0" ? "net8" : tfm)}\",");
	}
	if (hasWindows) {
		foreach (var tfm in allApiVersions) {
			writer.WriteLine ($"				\"Microsoft.{platform}.Windows.Sdk.Aliased.{(tfm == "net8.0" ? "net8" : tfm)}\",");
		}
	}
	writer.WriteLine ($"				\"Microsoft.{platform}.Ref.{currentApiVersion}\",");
	writer.WriteLine ($"				\"Microsoft.{platform}.Runtime.{ridNoArch}.{currentApiVersion}\",");
	foreach (var rid in runtimeIdentifiers) {
		writer.WriteLine ($"				\"Microsoft.{platform}.Runtime.{rid}.{currentApiVersion}\",");
	}
	if (platform == "watchOS") {
		writer.WriteLine ("				\"Microsoft.NETCore.App.Ref.watchOS\",");
		writer.WriteLine ("				\"Microsoft.NETCore.App.Crossgen2.watchOS\",");
		writer.WriteLine ("				\"Microsoft.DotNet.ILCompiler.watchOS\",");
		writer.WriteLine ("				\"Microsoft.DotNet.ILCompiler.watchOS.Host\",");
		writer.WriteLine ("				\"Microsoft.NET.ILLink.Tasks.watchOS\",");
		foreach (var rid in runtimeIdentifiers) {
			writer.WriteLine ($"				\"Microsoft.NETCore.App.Runtime.{rid}\",");
			writer.WriteLine ($"				\"Microsoft.NETCore.App.Runtime.NativeAOT.{rid}\",");
		}
	}
	writer.WriteLine ($"				\"Microsoft.{platform}.Templates.net9\"");
	writer.WriteLine ($"			],");
	writer.WriteLine ($"			\"extends\": [");
	if (platform == "macOS") {
		// Hopefully when https://github.com/dotnet/runtime/issues/122264 is done we can use the versioned workload for every version
		writer.WriteLine ($"				\"microsoft-net-runtime-mono-tooling\",");
		for (var i = earliestDotNetVersion; i < latestDotNetVersion; i++) {
			writer.WriteLine ($"				\"microsoft-net-runtime-mono-tooling-net{i}\",");
		}
	} else if (platform != "watchOS") {
		// Hopefully when https://github.com/dotnet/runtime/issues/122264 is done we can use the versioned workload for every version
		writer.WriteLine ($"				\"microsoft-net-runtime-{platformLowerCase}\",");
		for (var i = earliestDotNetVersion; i < latestDotNetVersion; i++) {
			writer.WriteLine ($"				\"microsoft-net-runtime-{platformLowerCase}-net{i}\",");
		}
	}
	writer.WriteLine ($"			]");
	writer.WriteLine ($"		}},");
	writer.WriteLine ($"	}},");
	writer.WriteLine ($"	\"packs\": {{");
	foreach (var tfmVersion in allApiVersions) {
		string? apiVersion = null;
		var tfm = tfmVersion;
		if (tfm == currentApiVersion) {
			apiVersion = version;
		} else if (tfm == "net8.0") {
			apiVersion = net8Version;
		} else {
			var propsPackageName = $"Microsoft{platform}Sdk" + tfm.Replace ("-", "").Replace (".", "") + "PackageVersion";
			if (!versionsPropsTable.TryGetValue (propsPackageName, out apiVersion)) {
				Console.Error.WriteLine ($"❌ Unable to find a package version for {platform}/{tfm} in {versionsPropsPath}. Package name: {propsPackageName}");
				apiVersion = "?";
				failed = true;
			}
		}
		writer.WriteLine ($"		\"Microsoft.{platform}.Sdk.{(tfm == "net8.0" ? "net8" : tfm)}\": {{");
		writer.WriteLine ($"			\"kind\": \"sdk\",");
		writer.WriteLine ($"			\"version\": \"{apiVersion}\",");
		if (tfm == "net8.0") {
			writer.WriteLine ($"			\"alias-to\": {{");
			writer.WriteLine ($"				\"any\": \"Microsoft.{platform}.Sdk\"");
			writer.WriteLine ($"			}}");
		}
		writer.WriteLine ($"		}},");
		if (hasWindows) {
			writer.WriteLine ($"		\"Microsoft.{platform}.Windows.Sdk.Aliased.{(tfm == "net8.0" ? "net8" : tfm)}\": {{");
			writer.WriteLine ($"			\"kind\": \"sdk\",");
			writer.WriteLine ($"			\"version\": \"{apiVersion}\",");
			writer.WriteLine ($"			\"alias-to\": {{");
			if (tfm == "net8.0") {
				writer.WriteLine ($"				\"win-x64\": \"Microsoft.{platform}.Windows.Sdk\",");
				writer.WriteLine ($"				\"win-x86\": \"Microsoft.{platform}.Windows.Sdk\",");
				writer.WriteLine ($"				\"win-arm64\": \"Microsoft.{platform}.Windows.Sdk\",");
			} else {
				writer.WriteLine ($"				\"win-x64\": \"Microsoft.{platform}.Windows.Sdk.{tfm}\",");
				writer.WriteLine ($"				\"win-x86\": \"Microsoft.{platform}.Windows.Sdk.{tfm}\",");
				writer.WriteLine ($"				\"win-arm64\": \"Microsoft.{platform}.Windows.Sdk.{tfm}\",");
			}
			writer.WriteLine ($"			}}");
			writer.WriteLine ($"		}},");
		}
	}
	writer.WriteLine ($"		\"Microsoft.{platform}.Ref.{currentApiVersion}\": {{");
	writer.WriteLine ($"			\"kind\": \"framework\",");
	writer.WriteLine ($"			\"version\": \"{version}\"");
	writer.WriteLine ($"		}},");
	writer.WriteLine ($"		\"Microsoft.{platform}.Runtime.{ridNoArch}.{currentApiVersion}\": {{");
	writer.WriteLine ($"			\"kind\": \"framework\",");
	writer.WriteLine ($"			\"version\": \"{version}\"");
	writer.WriteLine ($"		}},");
	foreach (var rid in runtimeIdentifiers) {
		writer.WriteLine ($"		\"Microsoft.{platform}.Runtime.{rid}.{currentApiVersion}\": {{");
		writer.WriteLine ($"			\"kind\": \"framework\",");
		writer.WriteLine ($"			\"version\": \"{version}\"");
		writer.WriteLine ($"		}},");
	}
	if (platform == "watchOS") {
		foreach (var rid in runtimeIdentifiers) {
			foreach (var prefix in new [] { "Microsoft.NETCore.App.Runtime", "Microsoft.NETCore.App.Runtime.NativeAOT" }) {
				writer.WriteLine ($"		\"{prefix}.{rid}\": {{");
				writer.WriteLine ("			\"kind\": \"framework\",");
				writer.WriteLine ($"			\"version\": \"{runtimeVersion}\"");
				writer.WriteLine ("		},");
			}
		}
		var aliasedPacks = new [] {
			(Id: "Microsoft.NETCore.App.Ref.watchOS", Kind: "framework", Host: "any", Package: "Microsoft.NETCore.App.Ref"),
			(Id: "Microsoft.NETCore.App.Crossgen2.watchOS", Kind: "sdk", Host: "osx-arm64", Package: "Microsoft.NETCore.App.Crossgen2.osx-arm64"),
			(Id: "Microsoft.DotNet.ILCompiler.watchOS", Kind: "sdk", Host: "any", Package: "Microsoft.DotNet.ILCompiler"),
			(Id: "Microsoft.DotNet.ILCompiler.watchOS.Host", Kind: "sdk", Host: "osx-arm64", Package: "runtime.osx-arm64.Microsoft.DotNet.ILCompiler"),
		};
		foreach (var pack in aliasedPacks) {
			writer.WriteLine ($"		\"{pack.Id}\": {{");
			writer.WriteLine ($"			\"kind\": \"{pack.Kind}\",");
			writer.WriteLine ($"			\"version\": \"{runtimeVersion}\",");
			writer.WriteLine ($"			\"alias-to\": {{ \"{pack.Host}\": \"{pack.Package}\" }}");
			writer.WriteLine ("		},");
		}
		writer.WriteLine ("		\"Microsoft.NET.ILLink.Tasks.watchOS\": {");
		writer.WriteLine ("			\"kind\": \"sdk\",");
		writer.WriteLine ($"			\"version\": \"{versionsPropsTable ["MicrosoftNETILLinkTasksPackageVersion"]}\",");
		writer.WriteLine ("			\"alias-to\": { \"any\": \"Microsoft.NET.ILLink.Tasks\" }");
		writer.WriteLine ("		},");
	}
	writer.WriteLine ($"		\"Microsoft.{platform}.Templates.net9\": {{");
	writer.WriteLine ($"			\"kind\": \"template\",");
	writer.WriteLine ($"			\"version\": \"{version}\",");
	writer.WriteLine ($"			\"alias-to\": {{");
	writer.WriteLine ($"				\"any\": \"Microsoft.{platform}.Templates\",");
	writer.WriteLine ($"			}}");
	writer.WriteLine ($"		}}");
	writer.WriteLine ($"	}}");
	writer.WriteLine ($"}}");
}

return failed ? 1 : 0;
