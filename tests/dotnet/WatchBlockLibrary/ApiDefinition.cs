using System;
using Foundation;
using ObjCRuntime;

namespace WatchBlockLibrary {
	[Category]
	[BaseType (typeof (NSOperationQueue))]
	interface OperationQueueBlocks {
		[Export ("addOperationWithBlock:")]
		void Enqueue (Action action);
	}
}
