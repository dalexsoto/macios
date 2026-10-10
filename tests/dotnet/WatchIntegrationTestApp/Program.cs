// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CoreFoundation;
using Foundation;
using ObjCRuntime;
using WatchKit;

namespace WatchIntegrationTestApp;

static class Program {
	public static void Main ()
	{
		Require (OperatingSystem.IsWatchOS (), "watchOS identity");
		Require (!RuntimeFeature.IsDynamicCodeCompiled, "no runtime code generation");
		Require (!Runtime.DynamicRegistrationSupported, "static Objective-C registration");
		Require (SynchronizationContext.Current?.GetType ().FullName == "WatchKit.WatchSynchronizationContext", "watch main-thread synchronization context");
		SoftwareExceptionChecks.Run ();
		CheckCooperativeCollection ();
		Console.WriteLine ("WATCH_INTEGRATION_MODEL_START");
		var model = new IntegrationModel ();
		Console.WriteLine ("WATCH_INTEGRATION_MODEL_CREATED");
		WatchApplication.Run (model);
	}

	static void CheckCooperativeCollection ()
	{
		using var started = new ManualResetEventSlim ();
		var state = new CollectionState ();
		var worker = new Thread (() => RunCollectionLoop (state, started)) { IsBackground = true };
		worker.Start ();
		try {
			Require (started.Wait (TimeSpan.FromSeconds (10)), "collection worker starts");
			Require (SpinWait.SpinUntil (() => Volatile.Read (ref state.Ready) != 0, TimeSpan.FromSeconds (10)), "collection worker enters its nonallocating loop");
			for (var iteration = 0; iteration < 3; iteration++) {
				GC.Collect (GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);
				GC.WaitForPendingFinalizers ();
			}
		} finally {
			Volatile.Write (ref state.Stop, 1);
			Require (worker.Join (TimeSpan.FromSeconds (10)), "collection worker stops");
		}
		Require (state.Result == 42, "cooperative collection preserves an object and its interior reference");
		Console.WriteLine ("WATCH_COOPERATIVE_GC_PASS");
	}

	[MethodImpl (MethodImplOptions.NoInlining)]
	static void RunCollectionLoop (CollectionState state, ManualResetEventSlim started)
	{
		var values = state.Values;
		ref var value = ref values [0];
		started.Set ();
		var observed = 0;
		while (Volatile.Read (ref state.Stop) == 0) {
			observed = Volatile.Read (ref value);
			Volatile.Write (ref state.Ready, 1);
		}
		state.Result = observed == 42 && Volatile.Read (ref value) == 42 && ReferenceEquals (values, state.Values) ? 42 : -1;
	}

	sealed class CollectionState {
		public readonly int [] Values = [42];
		public int Ready;
		public int Stop;
		public int Result;
	}

	internal static void Require (bool condition, string invariant)
	{
		if (!condition)
			throw new InvalidOperationException ("Watch integration invariant failed: " + invariant);
	}
}

[Register ("WatchIntegrationChild")]
public sealed class ChildCounter : NSObject, INSCopying {
	int value = 40;
	internal static int Finalized;

	[Export ("increment")]
	public NSNumber Increment ()
	{
		WatchApplication.EnsureUIThread ();
		return NSNumber.FromInt32 (++value);
	}

	public override string Description => "Managed watch counter";

	[Export ("copyWithZone:")]
	[return: Release]
	public NSObject Copy (NSZone? zone)
	{
		WatchApplication.EnsureUIThread ();
		return new ChildCounter ();
	}

	~ChildCounter ()
	{
		Interlocked.Increment (ref Finalized);
	}
}

[Register ("WatchIntegrationModel")]
public sealed class IntegrationModel : NSObject {
	WeakReference? child;
	WeakReference? blockCapture;
	WeakReference? bindingBlockCapture;
	WeakReference? handwrittenBlockCapture;
	string status = "Starting";

	[Export ("status")]
	public string Status {
		get {
			WatchApplication.EnsureUIThread ();
			return status;
		}

	}

	[Export ("createChild")]
	public ChildCounter CreateChild ()
	{
		WatchApplication.EnsureUIThread ();
		return CreateUnrootedChild ();
	}

	[MethodImpl (MethodImplOptions.NoInlining)]
	ChildCounter CreateUnrootedChild ()
	{
		var instance = new ChildCounter ();
		child = new WeakReference (instance);
		return instance;
	}

	[Export ("newChild")]
	public ChildCounter NewChild () => CreateChild ();

	[Export ("copyState")]
	public ChildCounter CopyState () => CreateChild ();

	[Export ("retainedChild")]
	[return: Release]
	public ChildCounter RetainedChild () => CreateChild ();

	[Export ("collect")]
	public void Collect ()
	{
		WatchApplication.EnsureUIThread ();
		Console.WriteLine ("WATCH_LIFETIME_GC_START");
		GC.Collect ();
		Console.WriteLine ("WATCH_LIFETIME_GC_COLLECTED");
		GC.WaitForPendingFinalizers ();
		Console.WriteLine ("WATCH_LIFETIME_GC_FINALIZED");
		GC.Collect ();
		Console.WriteLine ("WATCH_LIFETIME_GC_END");
	}

	[Export ("assertChildReleased")]
	public void AssertChildReleased ()
	{
		WatchApplication.EnsureUIThread ();
		Program.Require (child is not null && !child.IsAlive, "Swift release permits managed collection");
		Program.Require (Volatile.Read (ref ChildCounter.Finalized) >= 1, "child finalization");
	}

	[Export ("startAsyncChecks")]
	public async void StartAsyncChecks ()
	{
		try {
			WatchApplication.EnsureUIThread ();
			var blockTask = QueueNativeBlock (0);
			var bindingBlockTask = QueueNativeBlock (1);
			var handwrittenBlockTask = QueueNativeBlock (2);
			Collect ();
			Program.Require (blockCapture?.IsAlive == true && bindingBlockCapture?.IsAlive == true && handwrittenBlockCapture?.IsAlive == true, "copied native blocks root their captures");
			await Task.WhenAll (blockTask, bindingBlockTask, handwrittenBlockTask);
			WatchApplication.EnsureUIThread ();
			await Task.Delay (200);
			Collect ();
			Program.Require (blockCapture is not null && !blockCapture.IsAlive && bindingBlockCapture is not null && !bindingBlockCapture.IsAlive, "Foundation and binding library release completed block captures");
			Program.Require (handwrittenBlockCapture is not null && !handwrittenBlockCapture.IsAlive, "handwritten block releases its capture");

			using var client = new HttpClient ();
			client.Timeout = TimeSpan.FromSeconds (30);
			using var response = await client.GetAsync ("https://example.com/");
			WatchApplication.EnsureUIThread ();
			response.EnsureSuccessStatusCode ();
			var content = await response.Content.ReadAsStringAsync ();
			Program.Require (content.Contains ("Example Domain", StringComparison.Ordinal), "real URLSession HTTPS response");
			status = "PASS";
			NSNotificationCenter.DefaultCenter.PostNotificationName ("WatchIntegrationStateChanged", this);
		} catch (Exception exception) {
			Environment.FailFast ("Watch integration asynchronous checks failed.", exception);
		}
	}

	[Export ("checkNativeException")]
	public void CheckNativeException ()
	{
		WatchApplication.EnsureUIThread ();
		try {
			using var instance = new NSObject ();
			using var key = new NSString ("WatchIntegrationMissingKey");
			instance.ValueForKey (key);
		} catch (ObjCException exception) {
			Program.Require (exception.Name == "NSUnknownKeyException", "Objective-C exception identity");
			return;
		}
		throw new InvalidOperationException ("The native exception did not reach the managed exception boundary.");
	}

	[MethodImpl (MethodImplOptions.NoInlining)]
	unsafe Task QueueNativeBlock (int backend)
	{
		var capture = new BlockCapture { Value = 42 };
		if (backend == 1)
			bindingBlockCapture = new WeakReference (capture);
		else if (backend == 2)
			handwrittenBlockCapture = new WeakReference (capture);
		else
			blockCapture = new WeakReference (capture);
		var completion = new TaskCompletionSource (TaskCreationOptions.RunContinuationsAsynchronously);
		Action callback = () => {
			WatchApplication.EnsureUIThread ();
			Program.Require (capture.Value == 42, "Foundation block preserves captured state across GC");
			completion.SetResult ();
		};
		if (backend == 1) {
			WatchBlockLibrary.OperationQueueBlocks.Enqueue (NSOperationQueue.MainQueue, callback);
		} else if (backend == 2) {
			var queue = DispatchQueue.MainQueue;
			using var block = new BlockLiteral ((delegate* unmanaged<IntPtr, void>) &InvokeBlock, callback, typeof (IntegrationModel), nameof (InvokeBlock));
			DispatchAsync (queue.Handle, &block);
			GC.KeepAlive (queue);
		} else {
			NSRunLoop.Main.Perform (callback);
		}
		return completion.Task;
	}

	[UnmanagedCallersOnly]
	static void InvokeBlock (IntPtr block)
	{
		var callback = BlockLiteral.GetTarget<Action> (block)
			?? throw new InvalidOperationException ("The native block lost its managed callback.");
		callback ();
	}

	[DllImport ("/usr/lib/system/libdispatch.dylib", EntryPoint = "dispatch_async")]
	static extern unsafe void DispatchAsync (NativeHandle queue, BlockLiteral* block);

	sealed class BlockCapture {
		public int Value;
	}
}

[Register ("WatchSwiftOnlyObject")]
public sealed class SwiftOnlyObject : NSObject {
	[Export ("value")]
	public NSNumber Value {
		get {
			WatchApplication.EnsureUIThread ();
			return NSNumber.FromInt32 (99);
		}
	}
}
