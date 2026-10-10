using CoreGraphics;

namespace WatchKit {
	[NoiOS, NoTV, NoMac, NoMacCatalyst]
	[Watch (26, 0)]
	[Native]
	public enum WKHapticType : long {
		Notification,
		DirectionUp,
		DirectionDown,
		Success,
		Failure,
		Retry,
		Start,
		Stop,
		Click,
		NavigationLeftTurn,
		NavigationRightTurn,
		NavigationGenericManeuver,
		UnderwaterDepthPrompt,
		UnderwaterDepthCriticalPrompt,
	}

	[NoiOS, NoTV, NoMac, NoMacCatalyst]
	[Watch (26, 0)]
	[Native]
	public enum WKInterfaceDeviceBatteryState : long {
		Unknown,
		Unplugged,
		Charging,
		Full,
	}

	[NoiOS, NoTV, NoMac, NoMacCatalyst]
	[Watch (26, 0)]
	[BaseType (typeof (NSObject))]
	interface WKInterfaceDevice {
		[Static]
		[Export ("currentDevice")]
		WKInterfaceDevice CurrentDevice { get; }

		[Export ("screenBounds")]
		CGRect ScreenBounds { get; }

		[Export ("screenScale")]
		nfloat ScreenScale { get; }

		[Export ("preferredContentSizeCategory", ArgumentSemantic.Copy)]
		string PreferredContentSizeCategory { get; }

		[Export ("systemVersion", ArgumentSemantic.Copy)]
		string SystemVersion { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("model", ArgumentSemantic.Copy)]
		string Model { get; }

		[Export ("localizedModel", ArgumentSemantic.Copy)]
		string LocalizedModel { get; }

		[Export ("systemName", ArgumentSemantic.Copy)]
		string SystemName { get; }

		[Export ("batteryMonitoringEnabled")]
		bool BatteryMonitoringEnabled { [Bind ("isBatteryMonitoringEnabled")] get; set; }

		[Export ("batteryLevel")]
		float BatteryLevel { get; }

		[Export ("batteryState")]
		WKInterfaceDeviceBatteryState BatteryState { get; }

		[Export ("playHaptic:")]
		void PlayHaptic (WKHapticType type);
	}

	[NoiOS, NoTV, NoMac, NoMacCatalyst]
	[Watch (26, 0)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface WKApplication {
		[Static]
		[Export ("sharedApplication")]
		WKApplication SharedApplication { get; }

		[Export ("openSystemURL:")]
		void OpenSystemUrl (NSUrl url);

		[Export ("isApplicationRunningInDock")]
		bool IsApplicationRunningInDock { get; }

		[Export ("autorotating")]
		bool Autorotating { [Bind ("isAutorotating")] get; set; }

		[Export ("isAutorotated")]
		bool IsAutorotated { get; }
	}
}
