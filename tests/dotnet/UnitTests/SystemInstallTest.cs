// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text.Json;
using NUnit.Framework;

namespace Xamarin.Tests {
	[TestFixture]
	[Platform (Include = "MacOsX")]
	[SupportedOSPlatform ("macos")]
	public class SystemInstallTest {
		[TestCase ("ios tvos")]
		[TestCase ("ios tvos watchos")]
		[TestCase ("watchos")]
		public void AggregateInstallationSerializesSystemChanges (string platforms)
		{
			var directory = Cache.CreateTemporaryDirectory ();
			var source = File.ReadAllText (Path.Combine (Configuration.SourceRoot, "dotnet", "Makefile"));
			var aggregate = Section (source, "\ninstall-system:", "\nall-local::");
			var watch = Section (source, "\ninstall-system-$(3): pack-watchos", "\nelse\ninstall-system-$(3):");
			var existing = Section (source, "\ninstall-system-$(3): $(DOTNET_PKG_DIR)", "\nendif\n\nINSTALL_SYSTEM_PACKAGE_TARGETS");
			var selected = platforms.Split (' ');
			var bin = Path.Combine (directory, "bin");
			var root = Path.Combine (directory, "system");
			var rollbackDirectory = Path.Combine (directory, "tools", "devops", "automation", "scripts", "bash");
			var rollback = Path.Combine (rollbackDirectory, "generate-workload-rollback.sh");
			var events = Path.Combine (directory, "events");
			var installationLock = Path.Combine (directory, "installation-lock");
			Directory.CreateDirectory (bin);
			Directory.CreateDirectory (Path.Combine (root, "sdk", "11.0.100"));
			Directory.CreateDirectory (rollbackDirectory);

			WriteExecutable (rollback, """
				#!/bin/sh
				printf 'rollback %s\n' "$*" >> "$INSTALL_EVENTS"
				sleep 0.05
				""");
			const string dotnet = """
				#!/bin/sh
				if test "$1" = --version; then
					printf '%s\n' 11.0.100
				fi
				""";
			WriteExecutable (Path.Combine (bin, "dotnet"), dotnet);
			WriteExecutable (Path.Combine (root, "dotnet"), dotnet);
			// Stub privileged operations; a lock makes concurrent system mutations fail deterministically.
			WriteExecutable (Path.Combine (bin, "sudo"), """
				#!/bin/sh
				mkdir "$INSTALL_LOCK" || exit 1
				printf 'start %s\n' "$*" >> "$INSTALL_EVENTS"
				sleep 0.05
				if test "$1" != install; then
					"$@" || exit 1
				fi
				printf 'end %s\n' "$*" >> "$INSTALL_EVENTS"
				rmdir "$INSTALL_LOCK"
				""");

			var makefile = $"""
				Q=@
				TOP={directory}
				HOME={directory}
				SYSTEM_DOTNET_ROOT={root}
				DOTNET_VERSION=11.0.100
				MACIOS_MANIFEST_VERSION_BAND=11.0.100
				DOTNET_PLATFORMS_LOWERCASE={platforms}
				INSTALL_SYSTEM_PACKAGE_TARGETS={string.Join (" ", selected.Select (platform => "install-system-" + platform))}
				.PHONY: install-system {string.Join (" ", selected.Select (platform => "install-system-" + platform))}
				""";
			foreach (var platform in selected) {
				var recipe = platform == "watchos" ? watch : existing;
				recipe = recipe [(recipe.IndexOf ('\n', 1) + 1)..].Replace ("$$", "$", StringComparison.Ordinal).Replace ("$(3)", platform, StringComparison.Ordinal);
				makefile += $"\ninstall-system-{platform}:\n" + recipe;
			}
			File.WriteAllText (Path.Combine (directory, "Makefile"), makefile + aggregate + "\n");
			var environment = new Dictionary<string, string?> {
				["PATH"] = bin + Path.PathSeparator + Environment.GetEnvironmentVariable ("PATH"),
				["INSTALL_EVENTS"] = events,
				["INSTALL_LOCK"] = installationLock,
			};
			var result = ExecutionHelper.Execute ("/usr/bin/make", new [] { "-j8", "install-system" }, out var output, directory, environment, TimeSpan.FromSeconds (30));
			Assert.That (result, Is.Zero, output.ToString ());
			var recorded = File.ReadAllLines (events);
			var hasExisting = selected.Any (platform => platform != "watchos");
			var hasWatch = selected.Contains ("watchos");
			var rollbacks = recorded.Where (line => line.StartsWith ("rollback ", StringComparison.Ordinal)).ToArray ();
			Assert.That (rollbacks, Has.Length.EqualTo (hasExisting ? 1 : 0), "The shared rollback file must have only one writer");
			var starts = recorded.Where (line => line.StartsWith ("start ", StringComparison.Ordinal)).ToArray ();
			Assert.That (starts, Has.Length.EqualTo ((hasExisting ? 2 : 0) + (hasWatch ? 3 : 0)));
			if (hasExisting) {
				Assert.That (rollbacks [0], Is.EqualTo ("rollback ios tvos"), "The legacy rollback must exclude watchOS");
				Assert.That (starts [0], Does.Contain ("workload remove ios tvos"));
				Assert.That (starts [1], Does.Contain ("workload install --from-rollback-file"));
				Assert.That (starts [1], Does.Contain ("ios tvos -v diag"));
			}
			if (hasWatch)
				Assert.That (starts.Last (), Does.Contain ("workload install watchos --skip-manifest-update"));
			Assert.That (Directory.Exists (installationLock), Is.False);
		}

		[Test]
		public void RollbackCanSelectExistingPlatformsOnly ()
		{
			var directory = Cache.CreateTemporaryDirectory ();
			var repository = Path.Combine (directory, "macios");
			var bin = Path.Combine (directory, "bin");
			Directory.CreateDirectory (repository);
			Directory.CreateDirectory (bin);
			WriteExecutable (Path.Combine (bin, "make"), """
				#!/bin/sh
				case "$*" in
					*VARIABLE=DOTNET_PLATFORMS*) printf '%s\n' 'DOTNET_PLATFORMS=iOS tvOS watchOS' ;;
					*VARIABLE=MACIOS_MANIFEST_VERSION_BAND*) printf '%s\n' 'MACIOS_MANIFEST_VERSION_BAND=11.0.100' ;;
					*VARIABLE=*NUGET_VERSION_FULL*) printf '%s\n' 'VERSION=27.0.11000+metadata' ;;
					*) exit 1 ;;
				esac
				""");
			var environment = new Dictionary<string, string?> {
				["PATH"] = bin + Path.PathSeparator + Environment.GetEnvironmentVariable ("PATH"),
				["BUILD_SOURCESDIRECTORY"] = directory,
				["BUILD_REPOSITORY_TITLE"] = "macios",
			};
			var script = Path.Combine (Configuration.SourceRoot, "tools", "devops", "automation", "scripts", "bash", "generate-workload-rollback.sh");
			var result = ExecutionHelper.Execute (script, new [] { "ios", "tvos" }, out var output, repository, environment, TimeSpan.FromSeconds (30));
			Assert.That (result, Is.Zero, output.ToString ());
			using var rollback = JsonDocument.Parse (File.ReadAllText (Path.Combine (directory, "WorkloadRollback.json")));
			Assert.That (rollback.RootElement.EnumerateObject ().Select (property => property.Name), Is.EquivalentTo (new [] { "microsoft.net.sdk.ios", "microsoft.net.sdk.tvos" }));
			foreach (var property in rollback.RootElement.EnumerateObject ())
				Assert.That (property.Value.GetString (), Is.EqualTo ("27.0.11000/11.0.100"));
		}

		static string Section (string source, string start, string end)
		{
			var first = source.IndexOf (start, StringComparison.Ordinal);
			Assert.That (first, Is.GreaterThanOrEqualTo (0), start);
			var last = source.IndexOf (end, first + start.Length, StringComparison.Ordinal);
			Assert.That (last, Is.GreaterThan (first), end);
			return source [first..last];
		}

		static void WriteExecutable (string path, string content)
		{
			File.WriteAllText (path, content + "\n");
			File.SetUnixFileMode (path, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
		}
	}
}
