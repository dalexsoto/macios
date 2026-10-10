using Foundation;
using ObjCRuntime;
using WatchKit;

namespace WatchApp1;

public static class Program {
	public static void Main (string [] args)
	{
		WatchApplication.Run (new WatchModel ());
	}
}

[Register ("DotNetWatchModel")]
public sealed class WatchModel : NSObject {
	int value;

	[Export ("title")]
	public string Title {
		get {
			WatchApplication.EnsureUIThread ();
			return "Hello from .NET";
		}
	}

	[Export ("counter")]
	public NSNumber Counter {
		get {
			WatchApplication.EnsureUIThread ();
			return NSNumber.FromInt32 (value);
		}
	}

	[Export ("increment")]
	public void Increment ()
	{
		WatchApplication.EnsureUIThread ();
		value++;
		NSNotificationCenter.DefaultCenter.PostNotificationName ("DotNetWatchModelChanged", this);
	}
}
