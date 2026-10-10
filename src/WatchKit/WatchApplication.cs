// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;

using Foundation;

namespace WatchKit {
	/// <summary>Hosts a SwiftUI watch application with managed state and event handlers.</summary>
	public static class WatchApplication {
		static int mainThreadId;
		static int running;

		internal static void InitializePlatform ()
		{
			mainThreadId = Environment.CurrentManagedThreadId;
			SynchronizationContext.SetSynchronizationContext (new WatchSynchronizationContext ());
		}

		/// <summary>Throws if the caller is not on the watch application's main thread.</summary>
		public static void EnsureUIThread ()
		{
			if (mainThreadId == 0 || Environment.CurrentManagedThreadId != mainThreadId)
				throw new InvalidOperationException ("watchOS UI operations must run on the application's main thread.");
		}

		internal static void EnsureEventAndDelegateAreNotMismatched (object del, Type expectedType)
		{
			ObjCRuntime.Runtime.EnsureEventAndDelegateAreNotMismatched (true, del, expectedType);
		}

		internal static void EnsureDelegateAssignIsNotOverwritingInternalDelegate (object? currentDelegateValue, object? newDelegateValue, Type internalDelegateType)
		{
			ObjCRuntime.Runtime.EnsureDelegateAssignIsNotOverwritingInternalDelegate (true, currentDelegateValue, newDelegateValue, internalDelegateType);
		}

		/// <summary>Transfers control to the registered native watch application entry point.</summary>
		/// <param name="applicationModel">The Objective-C model shared with the native user interface.</param>
		/// <remarks>This method must be called on the main thread and never returns.</remarks>
		[DoesNotReturn]
		public static void Run (NSObject applicationModel)
		{
			ArgumentNullException.ThrowIfNull (applicationModel);
			EnsureUIThread ();
			if (Interlocked.Exchange (ref running, 1) != 0)
				throw new InvalidOperationException ("The watchOS application event loop can only be started once.");

			RunNative (applicationModel.GetNonNullHandle (nameof (applicationModel)));
			GC.KeepAlive (applicationModel);
			Environment.FailFast ("The watchOS native application entry point returned unexpectedly.");
		}

		[DllImport ("__Internal", EntryPoint = "xamarin_watch_application_main")]
		static extern void RunNative (NativeHandle applicationModel);
	}

	internal sealed class WatchSynchronizationContext : SynchronizationContext {
		public override SynchronizationContext CreateCopy () => new WatchSynchronizationContext ();

		public override void Post (SendOrPostCallback callback, object? state)
		{
			NSRunLoop.Main.BeginInvokeOnMainThread (callback, state);
		}

		public override void Send (SendOrPostCallback callback, object? state)
		{
			NSRunLoop.Main.InvokeOnMainThread (callback, state);
		}
	}
}
