// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Mono.Cecil;

#nullable enable

namespace Xamarin.Linker.Steps {
	public class OptimizeGeneratedCodeStep : AssemblyModifierStep {
		protected override string Name { get; } = "Binding Optimizer";
		protected override int ErrorCode { get; } = 2020;

		OptimizeGeneratedCodeData? data;

		protected override bool IsActiveFor (AssemblyDefinition assembly)
		{
			return OptimizeGeneratedCode.IsActiveFor (assembly, Configuration.Profile, DerivedLinkContext.Annotations)
				|| (App.Platform == Xamarin.Utils.ApplePlatform.WatchOS && App.PrepareAssemblies && !App.DynamicRegistrationSupported && Configuration.Profile.IsOrReferencesProductAssembly (assembly));
		}

		protected override bool ProcessType (TypeDefinition type)
		{
			return ProcessMethods (type);
		}

		protected override bool ProcessMethod (MethodDefinition method)
		{
			if (data is null) {
				data = new OptimizeGeneratedCodeData {
					LinkContext = DerivedLinkContext,
					Optimizations = App.Optimizations,
				};
			}
			var blockSignaturesOnly = !OptimizeGeneratedCode.IsActiveFor (method.Module.Assembly, Configuration.Profile, DerivedLinkContext.Annotations);
			var modified = OptimizeGeneratedCode.OptimizeMethod (data, method, blockSignaturesOnly);
			if (data.ModifiedPlatformAssembly) {
				abr.SavePlatformAssembly ();
				data.ModifiedPlatformAssembly = false;
			}
			return modified;
		}
	}
}
