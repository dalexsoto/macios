// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WatchIntegrationTestApp;

static class SoftwareExceptionChecks {
	interface IValue {
		int Read ();
	}

	class Value : IValue {
		public int Number;
		public virtual int Read () => Number;
		public int ReadNonVirtual () => Number;
	}

	sealed class OtherValue : Value {
		public override int Read () => Number + 1;
	}

	interface IGenericValue {
		int Read<T> ();
	}

	sealed class GenericValue : IGenericValue {
		public int Read<T> () => 42;
	}

	sealed class OtherGenericValue : IGenericValue {
		public int Read<T> () => 43;
	}

	[StructLayout (LayoutKind.Explicit, Size = 8192)]
	class LargeValue {
		[FieldOffset (4096)]
		public int Number;
	}

	static int effects;

	public static void Run ()
	{
#pragma warning disable CS8625 // These calls intentionally dereference null in non-inlined methods.
		Expect<NullReferenceException> (() => ReadField (null));
		Expect<NullReferenceException> (() => WriteField (null, 1));
		Expect<NullReferenceException> (() => ReadLargeField (null));
		Expect<NullReferenceException> (() => ReadDiscardedField (null));
		Expect<NullReferenceException> (() => FieldAddress (null));
		Expect<NullReferenceException> (() => VirtualCall (null));
		Expect<NullReferenceException> (() => InterfaceCall (null));
		Expect<NullReferenceException> (() => GenericVirtualCall (null));
		Expect<NullReferenceException> (() => BindVirtualMethod (null));
		Expect<NullReferenceException> (() => NonVirtualCall (null));
		Expect<NullReferenceException> (() => InvokeDelegate (null));
		Expect<NullReferenceException> (() => StringLength (null));
		Expect<NullReferenceException> (() => ArrayLength (null));
		Expect<NullReferenceException> (() => ReadElement (null, 0));
		Expect<NullReferenceException> (() => WriteElement (null, 0, 1));
		Expect<NullReferenceException> (() => WriteReferenceElement (null, new object ()));
		Expect<NullReferenceException> (() => Unbox (null));
		effects = 0;
		Expect<NullReferenceException> (() => WriteFieldWithSideEffect (null));
		Program.Require (effects == 1, "null check preserves argument side effects");
		effects = 0;
		Expect<NullReferenceException> (() => InvokeDelegateWithSideEffect (null));
		Program.Require (effects == 1, "delegate null check follows argument side effects");
		effects = 0;
		Expect<NullReferenceException> (() => WriteThroughFieldAddress (null));
		Program.Require (effects == 0, "field address null check precedes subsequent side effects");
#pragma warning restore CS8625
		Expect<NullReferenceException> (ReadNullByRef);
		Expect<NullReferenceException> (ReadOffsetNullByRef);
		Expect<NullReferenceException> (AtomicNullByRef);
		Expect<IndexOutOfRangeException> (() => ReadElement (new int [1], 1));
		Expect<IndexOutOfRangeException> (() => ReadElement (new int [1], -1));
		Expect<DivideByZeroException> (() => Divide (42, 0));
		Expect<OverflowException> (() => AddChecked (int.MaxValue, 1));
		Program.Require (ReadField (new Value { Number = 42 }) == 42, "non-null field access");
		Program.Require (ReadElement (new [] { 42 }, 0) == 42, "valid array access");
		Program.Require (VirtualCall (new OtherValue { Number = 41 }) == 42, "overridden virtual method");
		Program.Require (InterfaceCall (new Value { Number = 42 }) == 42 && InterfaceCall (new OtherValue { Number = 42 }) == 43, "distinct interface implementations");
		Program.Require (GenericVirtualCall (new GenericValue ()) == 42 && GenericVirtualCall (new OtherGenericValue ()) == 43, "distinct generic virtual implementations");
		Program.Require (BindVirtualMethod (new OtherValue { Number = 41 }) () == 42, "virtual method delegate binding");
		Program.Require (InvokeDelegateWithArgument (Identity, 42) == 42, "static delegate invocation");
		Func<string?> staticGeneric = MakeNull<string>;
		Program.Require (staticGeneric () is null && BindStaticGeneric<object> () () is null, "static generic delegate permits a null receiver");
		var captured = 2;
		Program.Require (InvokeDelegateWithArgument (value => value + captured, 40) == 42, "capturing delegate invocation");
	}

	static void Expect<T> (Action action, [CallerArgumentExpression (nameof (action))] string operation = "") where T : Exception
	{
		Console.WriteLine ("WATCH_EXCEPTION_CHECK: " + operation);
		try {
			action ();
		} catch (T) {
			return;
		}
		throw new InvalidOperationException ("Expected software exception: " + typeof (T).Name + " from " + operation);
	}

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int ReadField (Value value) => value.Number;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static void WriteField (Value value, int number) => value.Number = number;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int ReadLargeField (LargeValue value) => value.Number;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static void ReadDiscardedField (Value value) => _ = value.Number;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static ref int FieldAddress (Value value) => ref value.Number;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int VirtualCall (Value value) => value.Read ();

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int InterfaceCall (IValue value) => value.Read ();

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int GenericVirtualCall (IGenericValue value) => value.Read<int> ();

	[MethodImpl (MethodImplOptions.NoInlining)]
	static Func<int> BindVirtualMethod (Value value) => value.Read;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int NonVirtualCall (Value value) => value.ReadNonVirtual ();

	[MethodImpl (MethodImplOptions.NoInlining)]
	static void InvokeDelegate (Action value) => value ();

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int InvokeDelegateWithArgument (Func<int, int> value, int argument) => value (argument);

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int InvokeDelegateWithSideEffect (Func<int, int> value) => value (NextValue ());

	[MethodImpl (MethodImplOptions.NoInlining)]
	static void WriteFieldWithSideEffect (Value value) => value.Number = NextValue ();

	[MethodImpl (MethodImplOptions.NoInlining)]
	static void WriteThroughFieldAddress (Value value)
	{
		ref var field = ref value.Number;
		field = NextValue ();
	}

	static int Identity (int value) => value;

	static T? MakeNull<T> () where T : class => null;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static Func<T?> BindStaticGeneric<T> () where T : class => MakeNull<T>;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int StringLength (string value) => value.Length;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int ArrayLength (int [] value) => value.Length;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int ReadElement (int [] value, int index) => value [index];

	[MethodImpl (MethodImplOptions.NoInlining)]
	static void WriteElement (int [] value, int index, int number) => value [index] = number;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static void WriteReferenceElement (object [] value, object item) => value [0] = item;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int Unbox (object value) => (int) value;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static void ReadNullByRef () => GC.KeepAlive (Unsafe.NullRef<int> ());

	[MethodImpl (MethodImplOptions.NoInlining)]
	static void ReadOffsetNullByRef () => GC.KeepAlive (Unsafe.Add (ref Unsafe.NullRef<int> (), 1));

	[MethodImpl (MethodImplOptions.NoInlining)]
	static void AtomicNullByRef () => Interlocked.Increment (ref Unsafe.NullRef<int> ());

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int Divide (int numerator, int denominator) => numerator / denominator;

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int AddChecked (int left, int right) => checked(left + right);

	[MethodImpl (MethodImplOptions.NoInlining)]
	static int NextValue () => ++effects;
}
