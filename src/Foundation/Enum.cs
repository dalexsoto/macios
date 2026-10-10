//
// Enums.cs: enumeration definitions for Foundation
//
// Copyright 2009-2010, Novell, Inc.
// Copyright 2011, 2012 Xamarin Inc
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

namespace Foundation {



#if MONOMAC
#endif





	/// <summary>The format to use during serialization using NSKeyedArchiver.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSPropertyListFormat : ulong {
		/// <summary>Store in the old OpenStep format.</summary>
		OpenStep = 1,
		/// <summary>Store in XML format.</summary>
		Xml = 100,
		/// <summary>Store in the binary file format.</summary>
		Binary = 200,
	}

	/// <summary>An enumeration of values specifying mutability options for property lists.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSPropertyListMutabilityOptions : ulong {
		/// <summary>To be added.</summary>
		Immutable = 0,
		/// <summary>To be added.</summary>
		MutableContainers = 1,
		/// <summary>To be added.</summary>
		MutableContainersAndLeaves = 2,
	}

	// Should mirror NSPropertyListMutabilityOptions
	/// <summary>An enumeration of mutability options for use with property lists.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSPropertyListWriteOptions : ulong {
		/// <summary>To be added.</summary>
		Immutable = 0,
		/// <summary>To be added.</summary>
		MutableContainers = 1,
		/// <summary>To be added.</summary>
		MutableContainersAndLeaves = 2,
	}

	// Should mirror NSPropertyListMutabilityOptions, but currently
	// not implemented (always use Immutable/0)
	/// <summary>Apple states that functionality related to this class is not implemented.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSPropertyListReadOptions : ulong {
		/// <summary>To be added.</summary>
		Immutable = 0,
		/// <summary>To be added.</summary>
		MutableContainers = 1,
		/// <summary>To be added.</summary>
		MutableContainersAndLeaves = 2,
	}

	/// <summary>A flagging enumeration whose values specify options in calls to <see cref="Foundation.NSMachPort.FromMachPort(System.UInt32,Foundation.NSMachPortRights)" />.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	[Flags]
	public enum NSMachPortRights : ulong {
		/// <summary>To be added.</summary>
		None = 0,
		/// <summary>To be added.</summary>
		SendRight = (1 << 0),
		/// <summary>To be added.</summary>
		ReceiveRight = (1 << 1),
	}


	/// <summary>NSNetService options.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSNetServiceOptions : ulong {
		/// <summary>To be added.</summary>
		NoAutoRename = 1 << 0,
		/// <summary>To be added.</summary>
		ListenForConnections = 1 << 1,
	}

	/// <summary>An enumeration of values that specify different date-format styles.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSDateFormatterStyle : ulong {
		/// <summary>To be added.</summary>
		None,
		/// <summary>To be added.</summary>
		Short,
		/// <summary>To be added.</summary>
		Medium,
		/// <summary>To be added.</summary>
		Long,
		/// <summary>To be added.</summary>
		Full,
	}

	/// <summary>An enumeration that can specify whether the <see cref="Foundation.NSDateFormatter" /> should behave as it did prior to OS x v10.4</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSDateFormatterBehavior : ulong {
		/// <summary>To be added.</summary>
		Default = 0,
		[NoiOS]
		[NoTV]
		[NoMacCatalyst]
		Mode_10_0 = 1000,
		/// <summary>To be added.</summary>
		Mode_10_4 = 1040,
	}







	/// <summary>An enumeration of ways in which <see cref="Foundation.NSNotification" />s can be coalesced.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSNotificationCoalescing : ulong {
		/// <summary>To be added.</summary>
		NoCoalescing = 0,
		/// <summary>To be added.</summary>
		CoalescingOnName = 1,
		/// <summary>To be added.</summary>
		CoalescingOnSender = 2,
	}

	/// <summary>An enumeration of values that specify when a notification shouldbe posted.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSPostingStyle : ulong {
		/// <summary>To be added.</summary>
		PostWhenIdle = 1,
		/// <summary>To be added.</summary>
		PostASAP = 2,
		/// <summary>To be added.</summary>
		Now = 3,
	}


	/// <summary>An enumeration of valid types for a <see cref="Foundation.NSExpression" />.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSExpressionType : ulong {
		/// <summary>To be added.</summary>
		ConstantValue = 0,
		/// <summary>To be added.</summary>
		EvaluatedObject,
		/// <summary>To be added.</summary>
		Variable,
		/// <summary>To be added.</summary>
		KeyPath,
		/// <summary>To be added.</summary>
		Function,
		/// <summary>To be added.</summary>
		UnionSet,
		/// <summary>To be added.</summary>
		IntersectSet,
		/// <summary>To be added.</summary>
		MinusSet,
		/// <summary>To be added.</summary>
		Subquery = 13,
		/// <summary>To be added.</summary>
		NSAggregate,
		/// <summary>To be added.</summary>
		AnyKey = 15,
		/// <summary>To be added.</summary>
		Block = 19,
		/// <summary>To be added.</summary>
		Conditional = 20,
	}

	/// <summary>Enumeration of various errors relating to Cocoa development.</summary>
	/// <remarks>To be added.</remarks>
	public enum NSCocoaError : int {
		/// <summary>To be added.</summary>
		None,

		/// <summary>To be added.</summary>
		FileNoSuchFile = 4,
		/// <summary>To be added.</summary>
		FileLocking = 255,
		/// <summary>To be added.</summary>
		FileReadUnknown = 256,
		/// <summary>To be added.</summary>
		FileReadNoPermission = 257,
		/// <summary>To be added.</summary>
		FileReadInvalidFileName = 258,
		/// <summary>To be added.</summary>
		FileReadCorruptFile = 259,
		/// <summary>To be added.</summary>
		FileReadNoSuchFile = 260,
		/// <summary>To be added.</summary>
		FileReadInapplicableStringEncoding = 261,
		/// <summary>To be added.</summary>
		FileReadUnsupportedScheme = 262,
		/// <summary>To be added.</summary>
		FileReadTooLarge = 263,
		/// <summary>To be added.</summary>
		FileReadUnknownStringEncoding = 264,
		/// <summary>To be added.</summary>
		FileWriteUnknown = 512,
		/// <summary>To be added.</summary>
		FileWriteNoPermission = 513,
		/// <summary>To be added.</summary>
		FileWriteInvalidFileName = 514,
		/// <summary>To be added.</summary>
		FileWriteFileExists = 516,
		/// <summary>To be added.</summary>
		FileWriteInapplicableStringEncoding = 517,
		/// <summary>To be added.</summary>
		FileWriteUnsupportedScheme = 518,
		/// <summary>To be added.</summary>
		FileWriteOutOfSpace = 640,
		/// <summary>To be added.</summary>
		FileWriteVolumeReadOnly = 642,

#if MONOMAC
		/// <summary>To be added.</summary>
		FileManagerUnmountUnknownError = 768,
		/// <summary>To be added.</summary>
		FileManagerUnmountBusyError = 769,
#endif

		/// <summary>To be added.</summary>
		KeyValueValidation = 1024,
		/// <summary>To be added.</summary>
		Formatting = 2048,
		/// <summary>To be added.</summary>
		UserCancelled = 3072,
		/// <summary>To be added.</summary>
		FeatureUnsupported = 3328,
		/// <summary>To be added.</summary>
		ExecutableNotLoadable = 3584,
		/// <summary>To be added.</summary>
		ExecutableArchitectureMismatch = 3585,
		/// <summary>To be added.</summary>
		ExecutableRuntimeMismatch = 3586,
		/// <summary>To be added.</summary>
		ExecutableLoad = 3587,
		/// <summary>To be added.</summary>
		ExecutableLink = 3588,
		/// <summary>To be added.</summary>
		FileErrorMinimum = 0,
		/// <summary>To be added.</summary>
		FileErrorMaximum = 1023,
		/// <summary>To be added.</summary>
		ValidationErrorMinimum = 1024,
		/// <summary>To be added.</summary>
		ValidationErrorMaximum = 2047,
		/// <summary>To be added.</summary>
		ExecutableErrorMinimum = 3584,
		/// <summary>To be added.</summary>
		ExecutableErrorMaximum = 3839,
		/// <summary>To be added.</summary>
		FormattingErrorMinimum = 2048,
		/// <summary>To be added.</summary>
		FormattingErrorMaximum = 2559,

		/// <summary>To be added.</summary>
		PropertyListReadCorrupt = 3840,
		/// <summary>To be added.</summary>
		PropertyListReadUnknownVersion = 3841,
		/// <summary>To be added.</summary>
		PropertyListReadStream = 3842,
		/// <summary>To be added.</summary>
		PropertyListWriteStream = 3851,
		/// <summary>To be added.</summary>
		PropertyListWriteInvalid = 3852,
		/// <summary>To be added.</summary>
		PropertyListErrorMinimum = 3840,
		/// <summary>To be added.</summary>
		PropertyListErrorMaximum = 4095,

		/// <summary>To be added.</summary>
		XpcConnectionInterrupted = 4097,
		/// <summary>To be added.</summary>
		XpcConnectionInvalid = 4099,
		/// <summary>To be added.</summary>
		XpcConnectionReplyInvalid = 4101,
		XpcConnectionCodeSigningRequirementFailure = 4102,
		/// <summary>To be added.</summary>
		XpcConnectionErrorMinimum = 4096,
		/// <summary>To be added.</summary>
		XpcConnectionErrorMaximum = 4224,

		/// <summary>To be added.</summary>
		UbiquitousFileUnavailable = 4353,
		/// <summary>To be added.</summary>
		UbiquitousFileNotUploadedDueToQuota = 4354,
		/// <summary>To be added.</summary>
		UbiquitousFileUbiquityServerNotAvailable = 4355,
		/// <summary>To be added.</summary>
		UbiquitousFileErrorMinimum = 4352,
		/// <summary>To be added.</summary>
		UbiquitousFileErrorMaximum = 4607,

		/// <summary>To be added.</summary>
		UserActivityHandoffFailedError = 4608,
		/// <summary>To be added.</summary>
		UserActivityConnectionUnavailableError = 4609,
		/// <summary>To be added.</summary>
		UserActivityRemoteApplicationTimedOutError = 4610,
		/// <summary>To be added.</summary>
		UserActivityHandoffUserInfoTooLargeError = 4611,

		/// <summary>To be added.</summary>
		UserActivityErrorMinimum = 4608,
		/// <summary>To be added.</summary>
		UserActivityErrorMaximum = 4863,

		/// <summary>To be added.</summary>
		CoderReadCorruptError = 4864,
		/// <summary>To be added.</summary>
		CoderValueNotFoundError = 4865,
		/// <summary>To be added.</summary>
		CoderInvalidValueError = 4866,
		/// <summary>To be added.</summary>
		CoderErrorMinimum = 4864,
		/// <summary>To be added.</summary>
		CoderErrorMaximum = 4991,

		/// <summary>To be added.</summary>
		BundleErrorMinimum = 4992,
		/// <summary>To be added.</summary>
		BundleErrorMaximum = 5119,

		/// <summary>To be added.</summary>
		BundleOnDemandResourceOutOfSpaceError = 4992,
		/// <summary>To be added.</summary>
		BundleOnDemandResourceExceededMaximumSizeError = 4993,
		/// <summary>To be added.</summary>
		BundleOnDemandResourceInvalidTagError = 4994,

		/// <summary>To be added.</summary>
		CloudSharingNetworkFailureError = 5120,
		/// <summary>To be added.</summary>
		CloudSharingQuotaExceededError = 5121,
		/// <summary>To be added.</summary>
		CloudSharingTooManyParticipantsError = 5122,
		/// <summary>To be added.</summary>
		CloudSharingConflictError = 5123,
		/// <summary>To be added.</summary>
		CloudSharingNoPermissionError = 5124,
		/// <summary>To be added.</summary>
		CloudSharingOtherError = 5375,
		/// <summary>To be added.</summary>
		CloudSharingErrorMinimum = 5120,
		/// <summary>To be added.</summary>
		CloudSharingErrorMaximum = 5375,

		CompressionFailedError = 5376,
		DecompressionFailedError = 5377,
		CompressionErrorMinimum = 5376,
		CompressionErrorMaximum = 5503,
	}







	/// <summary>An enumeration whose values specify how a <see cref="Foundation.NSComparisonPredicate" /> should apply to an n-to-many relationship.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSComparisonPredicateModifier : ulong {
		/// <summary>To be added.</summary>
		Direct,
		/// <summary>To be added.</summary>
		All,
		/// <summary>To be added.</summary>
		Any,
	}

	/// <summary>An enumeration of values that specify comparison types for use with <see cref="Foundation.NSComparisonPredicate" />.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSPredicateOperatorType : ulong {
		/// <summary>To be added.</summary>
		LessThan,
		/// <summary>To be added.</summary>
		LessThanOrEqualTo,
		/// <summary>To be added.</summary>
		GreaterThan,
		/// <summary>To be added.</summary>
		GreaterThanOrEqualTo,
		/// <summary>To be added.</summary>
		EqualTo,
		/// <summary>To be added.</summary>
		NotEqualTo,
		/// <summary>To be added.</summary>
		Matches,
		/// <summary>To be added.</summary>
		Like,
		/// <summary>To be added.</summary>
		BeginsWith,
		/// <summary>To be added.</summary>
		EndsWith,
		/// <summary>To be added.</summary>
		In,
		/// <summary>To be added.</summary>
		CustomSelector,
		/// <summary>To be added.</summary>
		Contains = 99,
		/// <summary>To be added.</summary>
		Between,
	}

	/// <summary>An enumeration whose values specify the type of string comparison to be used in a <see cref="Foundation.NSComparisonPredicate" />.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSComparisonPredicateOptions : ulong {
		/// <summary>To be added.</summary>
		None = 0x00,
		/// <summary>To be added.</summary>
		CaseInsensitive = 1 << 0,
		/// <summary>To be added.</summary>
		DiacriticInsensitive = 1 << 1,
		/// <summary>To be added.</summary>
		Normalized = 1 << 2,
	}

	/// <summary>An enumeration whose values specify the Boolean logical operator to be applied to a <see cref="Foundation.NSCompoundPredicate" />.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSCompoundPredicateType : ulong {
		/// <summary>To be added.</summary>
		Not,
		/// <summary>To be added.</summary>
		And,
		/// <summary>To be added.</summary>
		Or,
	}

	/// <summary>An enumeration of options for use when enumerating mounted volumes.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSVolumeEnumerationOptions : ulong {
		/// <summary>To be added.</summary>
		None = 0,
		// skip                  = 1 << 0,
		/// <summary>To be added.</summary>
		SkipHiddenVolumes = 1 << 1,
		/// <summary>To be added.</summary>
		ProduceFileReferenceUrls = 1 << 2,
	}

	/// <summary>An enumeration of options for use with <see cref="Foundation.NSDirectoryEnumerator" />.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSDirectoryEnumerationOptions : ulong {
		None = 0,
		/// <summary>To be added.</summary>
		SkipsSubdirectoryDescendants = 1 << 0,
		/// <summary>To be added.</summary>
		SkipsPackageDescendants = 1 << 1,
		/// <summary>To be added.</summary>
		SkipsHiddenFiles = 1 << 2,
		[MacCatalyst (13, 1)]
		IncludesDirectoriesPostOrder = 1 << 3,
		[MacCatalyst (13, 1)]
		ProducesRelativePathUrls = 1 << 4,
	}

	/// <summary>An enumeration of options for use with <see cref="Foundation.NSFileManager.Replace(Foundation.NSUrl,Foundation.NSUrl,System.String,Foundation.NSFileManagerItemReplacementOptions,out Foundation.NSUrl,out Foundation.NSError)" />.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSFileManagerItemReplacementOptions : ulong {
		/// <summary>To be added.</summary>
		None = 0,
		/// <summary>To be added.</summary>
		UsingNewMetadataOnly = 1 << 0,
		/// <summary>To be added.</summary>
		WithoutDeletingBackupItem = 1 << 1,
	}

	/// <summary>An enumeration of special directories for use with <see cref="Foundation.NSFileManager.GetUrls" />.</summary>
	/// <remarks>Some of these constants when used can return more than one value (for example AllApplicationsDirectory).</remarks>
	[Native]
	public enum NSSearchPathDirectory : ulong {
		/// <summary>Applications directory (/Applications).</summary>
		ApplicationDirectory = 1,
		/// <summary>Demo applications directory</summary>
		DemoApplicationDirectory,
		/// <summary>Deprecated, used to be /Developer/Applications.</summary>
		DeveloperApplicationDirectory,
		/// <summary>Directory for admin applications (Application/Utilities)</summary>
		AdminApplicationDirectory,
		/// <summary>Library directory contains documentation, configuration files and support files (Library)</summary>
		LibraryDirectory,
		/// <summary>Deprecated, used to be /Developer</summary>
		DeveloperDirectory,
		/// <summary>User directory (for all users, not the currently logged in user, /Users, /Network/Users for example)</summary>
		UserDirectory,
		/// <summary>Documentation directory</summary>
		DocumentationDirectory,
		/// <summary>Document directory (this is where an application can store its documents)</summary>
		DocumentDirectory,
		/// <summary>Directory for CoreServices (System/Library/CoreServices)</summary>
		CoreServiceDirectory,
		/// <summary>User autosave directory (Library/Autosave Information)</summary>
		AutosavedInformationDirectory = 11,
		/// <summary>The user’s desktop directory.</summary>
		DesktopDirectory = 12,
		/// <summary>Cache directory (Library/Caches)</summary>
		CachesDirectory = 13,
		/// <summary>Application support directory (Library/Application Support)</summary>
		ApplicationSupportDirectory = 14,
		/// <summary>Downloads directory (only available when the domain specified includes the User value)</summary>
		DownloadsDirectory = 15,
		/// <summary>Input methods directory (Library/Input Methods)</summary>
		InputMethodsDirectory = 16,
		/// <summary>User’s movies directory (~/Movies)</summary>
		MoviesDirectory = 17,
		/// <summary>User’s music directory (~/Music)</summary>
		MusicDirectory = 18,
		/// <summary>User’s picture directory (~/Pictures)</summary>
		PicturesDirectory = 19,
		/// <summary>Printer descriptions directory, the directory that contains Postcript Printer Description files (Library/Printers/PPDS)</summary>
		PrinterDescriptionDirectory = 20,
		/// <summary>Shared public directory, when enabled (~/Public)</summary>
		SharedPublicDirectory = 21,
		/// <summary>Preference Panes directory, the directory that contains the *.prefPane bundles, (Library/PreferencePanes)</summary>
		PreferencePanesDirectory = 22,
		/// <summary>User scripts directory (Library/Application Scripts/app)</summary>
		[NoiOS]
		[NoTV]
		[NoMacCatalyst]
		ApplicationScriptsDirectory = 23,
		/// <summary>Item replacement directory, used for implementing safe-save features.</summary>
		ItemReplacementDirectory = 99,
		/// <summary>Combined directories where applications can appear.</summary>
		AllApplicationsDirectory = 100,
		/// <summary>Combined directories where resources can be appear.</summary>
		AllLibrariesDirectory = 101,
		/// <summary>Trash directory</summary>
		[NoTV]
		[MacCatalyst (13, 1)]
		TrashDirectory = 102,
	}

	/// <summary>An enumeration of values specifying search path domain constants for use with <see cref="Foundation.NSSearchPathDirectory" />.</summary>
	/// <remarks>The domain is used to specify the kind of directory you want to get from the <see cref="Foundation.NSFileManager.GetUrls(Foundation.NSSearchPathDirectory,Foundation.NSSearchPathDomain)" /> method.</remarks>
	[Flags]
	[Native]
	public enum NSSearchPathDomain : ulong {
		/// <summary>Do not return anything.</summary>
		None = 0,
		/// <summary>Return user directories.</summary>
		User = 1 << 0,
		/// <summary>Return local directories.</summary>
		Local = 1 << 1,
		/// <summary>Return network directories.</summary>
		Network = 1 << 2,
		/// <summary>Return the system directories.</summary>
		System = 1 << 3,
		/// <summary>Return items for all domains.</summary>
		All = 0x0ffff,
	}



	/// <summary>An enumeration of options for use when drawing strings.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSStringDrawingOptions : ulong {
		/// <summary>To be added.</summary>
		UsesLineFragmentOrigin = (1 << 0),
		/// <summary>To be added.</summary>
		UsesFontLeading = (1 << 1),
		/// <summary>This value is OSX-specific (not available in iOS)</summary>
		[NoiOS]
		[NoTV]
		[NoMacCatalyst]
		[Deprecated (PlatformName.MacOSX, 10, 11)]
		DisableScreenFontSubstitution = (1 << 2),
		/// <summary>To be added.</summary>
		UsesDeviceMetrics = (1 << 3),
		/// <summary>This value is OSX-specific (not available in iOS)</summary>
		[NoiOS]
		[NoTV]
		[NoMacCatalyst]
		[Deprecated (PlatformName.MacOSX, 10, 11)]
		OneShot = (1 << 4),
		/// <summary>To be added.</summary>
		TruncatesLastVisibleLine = (1 << 5),

		[iOS (26, 0), TV (26, 0), MacCatalyst (26, 0), Mac (26, 0)]
		OptionsResolvesNaturalAlignmentWithBaseWritingDirection = (1L << 9),
	}

	/// <summary>An enumeration of formats that can be used with numbers.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSNumberFormatterStyle : ulong {
		/// <summary>To be added.</summary>
		None = 0,
		/// <summary>To be added.</summary>
		Decimal = 1,
		/// <summary>To be added.</summary>
		Currency = 2,
		/// <summary>To be added.</summary>
		Percent = 3,
		/// <summary>To be added.</summary>
		Scientific = 4,
		/// <summary>To be added.</summary>
		SpellOut = 5,
		[MacCatalyst (13, 1)]
		OrdinalStyle = 6,
		[MacCatalyst (13, 1)]
		CurrencyIsoCodeStyle = 8,
		[MacCatalyst (13, 1)]
		CurrencyPluralStyle = 9,
		[MacCatalyst (13, 1)]
		CurrencyAccountingStyle = 10,
	}

	/// <summary>An enumeration whose values specify whether the number formatter should behave as it did before OS X v10.4</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSNumberFormatterBehavior : ulong {
		/// <summary>To be added.</summary>
		Default = 0,
		/// <summary>To be added.</summary>
		[NoiOS]
		[NoTV]
		[NoMacCatalyst]
		Version_10_0 = 1000,
		/// <summary>To be added.</summary>
		Version_10_4 = 1040,
	}

	/// <summary>An enumeration whose values indicates where padding should be applied to numbers.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSNumberFormatterPadPosition : ulong {
		/// <summary>To be added.</summary>
		BeforePrefix,
		/// <summary>To be added.</summary>
		AfterPrefix,
		/// <summary>To be added.</summary>
		BeforeSuffix,
		/// <summary>To be added.</summary>
		AfterSuffix,
	}

	/// <summary>An enumeration of rounding modes that can be applied to numbers.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSNumberFormatterRoundingMode : ulong {
		/// <summary>To be added.</summary>
		Ceiling,
		/// <summary>To be added.</summary>
		Floor,
		/// <summary>To be added.</summary>
		Down,
		/// <summary>To be added.</summary>
		Up,
		/// <summary>To be added.</summary>
		HalfEven,
		/// <summary>To be added.</summary>
		HalfDown,
		/// <summary>To be added.</summary>
		HalfUp,
	}

	/// <summary>Allows the application developer to specify that the old version of the file should be removed from the version store.</summary>
	[Flags]
	[Native]
	public enum NSFileVersionReplacingOptions : ulong {
		/// <summary>Moves the replacement file instead of copying it.</summary>
		ByMoving = 1 << 0,
	}

	/// <summary>Allows the application developer to specify that a new file version should be created by moving the source file.</summary>
	[Flags]
	[Native]
	public enum NSFileVersionAddingOptions : ulong {
		/// <summary>Moves the source file instead of copying it.</summary>
		ByMoving = 1 << 0,
	}

	/// <summary>An enumeration of options relating to reading the contents or attributes of a file or directory.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSFileCoordinatorReadingOptions : ulong {
		/// <summary>To be added.</summary>
		WithoutChanges = 1,
		/// <summary>To be added.</summary>
		ResolvesSymbolicLink = 1 << 1,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		ImmediatelyAvailableMetadataOnly = 1 << 2,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		ForUploading = 1 << 3,
	}

	/// <summary>An enumeration of options valid when changing the contents or attributes of a file or directory.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSFileCoordinatorWritingOptions : ulong {
		/// <summary>To be added.</summary>
		ForDeleting = 1,
		/// <summary>To be added.</summary>
		ForMoving = 2,
		/// <summary>To be added.</summary>
		ForMerging = 4,
		/// <summary>To be added.</summary>
		ForReplacing = 8,
		[MacCatalyst (13, 1)]
		ContentIndependentMetadataOnly = 16,
	}

	/// <summary>An enumeration of options for use with <see cref="Foundation.NSLinguisticTagger" />.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSLinguisticTaggerOptions : ulong {
		/// <summary>To be added.</summary>
		OmitWords = 1,
		/// <summary>To be added.</summary>
		OmitPunctuation = 2,
		/// <summary>To be added.</summary>
		OmitWhitespace = 4,
		/// <summary>To be added.</summary>
		OmitOther = 8,
		/// <summary>To be added.</summary>
		JoinNames = 16,
	}

	/// <summary>An enumeration of valid reasons for modifying the iCloud ubiquitous key store.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSUbiquitousKeyValueStoreChangeReason : long {
		/// <summary>To be added.</summary>
		ServerChange,
		/// <summary>To be added.</summary>
		InitialSyncChange,
		/// <summary>To be added.</summary>
		QuotaViolationChange,
		/// <summary>To be added.</summary>
		AccountChange,
	}

	/// <summary>Options for use when converting JSON data to instances of Foundation types.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSJsonReadingOptions : ulong {
		/// <summary>To be added.</summary>
		MutableContainers = 1,
		/// <summary>To be added.</summary>
		MutableLeaves = 2,
		FragmentsAllowed = 4,
		[iOS (15, 0), TV (15, 0), MacCatalyst (15, 0)]
		Json5Allowed = 8,
		[iOS (15, 0), TV (15, 0), MacCatalyst (15, 0)]
		TopLevelDictionaryAssumed = 16,
	}

	/// <summary>An enumeration specifying printing options (compact vs. pretty-printed) for JSON data.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSJsonWritingOptions : ulong {
		/// <summary>To be added.</summary>
		PrettyPrinted = 1,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		SortedKeys = (1 << 1),
		[MacCatalyst (13, 1)]
		FragmentsAllowed = (1 << 2),
		[MacCatalyst (13, 1)]
		WithoutEscapingSlashes = (1 << 3),
	}


	/// <summary>An enumeration of values used by alignment functions.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	public enum NSAlignmentOptions : long {
		/// <summary>To be added.</summary>
		MinXInward = 1 << 0,
		/// <summary>To be added.</summary>
		MinYInward = 1 << 1,
		/// <summary>To be added.</summary>
		MaxXInward = 1 << 2,
		/// <summary>To be added.</summary>
		MaxYInward = 1 << 3,
		/// <summary>To be added.</summary>
		WidthInward = 1 << 4,
		/// <summary>To be added.</summary>
		HeightInward = 1 << 5,

		/// <summary>To be added.</summary>
		MinXOutward = 1 << 8,
		/// <summary>To be added.</summary>
		MinYOutward = 1 << 9,
		/// <summary>To be added.</summary>
		MaxXOutward = 1 << 10,
		/// <summary>To be added.</summary>
		MaxYOutward = 1 << 11,
		/// <summary>To be added.</summary>
		WidthOutward = 1 << 12,
		/// <summary>To be added.</summary>
		HeightOutward = 1 << 13,

		/// <summary>To be added.</summary>
		MinXNearest = 1 << 16,
		/// <summary>To be added.</summary>
		MinYNearest = 1 << 17,
		/// <summary>To be added.</summary>
		MaxXNearest = 1 << 18,
		/// <summary>To be added.</summary>
		MaxYNearest = 1 << 19,
		/// <summary>To be added.</summary>
		WidthNearest = 1 << 20,
		/// <summary>To be added.</summary>
		HeightNearest = 1 << 21,

		/// <summary>To be added.</summary>
		RectFlipped = unchecked((long) (1UL << 63)),

		/// <summary>To be added.</summary>
		AllEdgesInward = MinXInward | MaxXInward | MinYInward | MaxYInward,
		/// <summary>To be added.</summary>
		AllEdgesOutward = MinXOutward | MaxXOutward | MinYOutward | MaxYOutward,
		/// <summary>To be added.</summary>
		AllEdgesNearest = MinXNearest | MaxXNearest | MinYNearest | MaxYNearest,
	}

	/// <summary>An enumeration of options to be used when reading a file-system node.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSFileWrapperReadingOptions : ulong {
		/// <summary>To be added.</summary>
		Immediate = 1 << 0,
		/// <summary>To be added.</summary>
		WithoutMapping = 1 << 1,
	}

	/// <summary>An enumeration of options to be used when writing a file-system node.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSFileWrapperWritingOptions : ulong {
		/// <summary>To be added.</summary>
		Atomic = 1 << 0,
		/// <summary>To be added.</summary>
		WithNameUpdating = 1 << 1,
	}

	/// <summary>An enumeration whose values specify the options to be used in the <see cref="Foundation.NSAttributedString.EnumerateAttribute(Foundation.NSString,Foundation.NSRange,Foundation.NSAttributedStringEnumeration,Foundation.NSAttributedStringCallback)" /> and <see cref="Foundation.NSAttributedString.EnumerateAttributes(Foundation.NSRange,Foundation.NSAttributedStringEnumeration,Foundation.NSAttributedRangeCallback)" /> methods.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native ("NSAttributedStringEnumerationOptions")]
	public enum NSAttributedStringEnumeration : ulong {
		/// <summary>To be added.</summary>
		None = 0,
		/// <summary>To be added.</summary>
		Reverse = 1 << 1,
		/// <summary>To be added.</summary>
		LongestEffectiveRangeNotRequired = 1 << 20,
	}

	// macOS has defined this in AppKit as well, but starting with .NET we're going
	// to use this one only.
	/// <summary>An enumeration of valid styles for underlines or strikethroughs.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSUnderlineStyle : long {
		/// <summary>To be added.</summary>
		None = 0x00,
		/// <summary>To be added.</summary>
		Single = 0x01,
		/// <summary>To be added.</summary>
		Thick = 0x02,
		/// <summary>To be added.</summary>
		Double = 0x09,
		/// <summary>To be added.</summary>
		PatternSolid = 0x0000,
		/// <summary>To be added.</summary>
		PatternDot = 0x0100,
		/// <summary>To be added.</summary>
		PatternDash = 0x0200,
		/// <summary>To be added.</summary>
		PatternDashDot = 0x0300,
		/// <summary>To be added.</summary>
		PatternDashDotDot = 0x0400,
		/// <summary>To be added.</summary>
		ByWord = 0x8000,
	}

	// There's an AppKit.NSWritingDirection, which is deprecated.
	// There's also an UIKit.UITextWritingDirection, which is deprecated too.
	// This is the enum we should be using.
	// See https://github.com/dotnet/macios/issues/6573
	/// <summary>An enumeration of valid writing directions.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSWritingDirection : long {
		/// <summary>To be added.</summary>
		Natural = -1,
		/// <summary>To be added.</summary>
		LeftToRight = 0,
		/// <summary>To be added.</summary>
		RightToLeft = 1,
	}

	/// <summary>An enumeration whose values specify the units to be displayed by a <see cref="Foundation.NSByteCountFormatter" />.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSByteCountFormatterUnits : ulong {
		/// <summary>To be added.</summary>
		UseDefault = 0,
		/// <summary>To be added.</summary>
		UseBytes = 1 << 0,
		/// <summary>To be added.</summary>
		UseKB = 1 << 1,
		/// <summary>To be added.</summary>
		UseMB = 1 << 2,
		/// <summary>To be added.</summary>
		UseGB = 1 << 3,
		/// <summary>To be added.</summary>
		UseTB = 1 << 4,
		/// <summary>To be added.</summary>
		UsePB = 1 << 5,
		/// <summary>To be added.</summary>
		UseEB = 1 << 6,
		/// <summary>To be added.</summary>
		UseZB = 1 << 7,
		/// <summary>To be added.</summary>
		UseYBOrHigher = 0x0FF << 8,
		/// <summary>To be added.</summary>
		UseAll = 0x0FFFF,
	}

	/// <summary>An enumeration whose values specify how byte units are calculated (e.g., if "KB" indicates 1000 or 1024 bytes).</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSByteCountFormatterCountStyle : long {
		/// <summary>To be added.</summary>
		File,
		/// <summary>To be added.</summary>
		Memory,
		/// <summary>To be added.</summary>
		Decimal,
		/// <summary>To be added.</summary>
		Binary,
	}



	/// <summary>An enumeration that defines the valid ligature types of an <see cref="Foundation.NSAttributedString" />.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSLigatureType : long {
		/// <summary>To be added.</summary>
		None,
		/// <summary>To be added.</summary>
		Default,
		/// <summary>To be added.</summary>
		All,
	}









	/// <summary>An enumeration whose values specify why a data transfer was cancelled.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSUrlErrorCancelledReason : long {
		/// <summary>To be added.</summary>
		UserForceQuitApplication,
		/// <summary>To be added.</summary>
		BackgroundUpdatesDisabled,
		/// <summary>To be added.</summary>
		InsufficientSystemResources,
	}

	/// <summary>A flagging enumeration whose values can be used with <see cref="Foundation.NSProcessInfo.BeginActivity(Foundation.NSActivityOptions,System.String)" />.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	public enum NSActivityOptions : ulong {
		/// <summary>To be added.</summary>
		IdleDisplaySleepDisabled = 1UL << 40,
		/// <summary>To be added.</summary>
		IdleSystemSleepDisabled = 1UL << 20,
		/// <summary>To be added.</summary>
		SuddenTerminationDisabled = 1UL << 14,
		/// <summary>To be added.</summary>
		AutomaticTerminationDisabled = 1UL << 15,
		AnimationTrackingEnabled = 1uL << 45,
		TrackingEnabled = 1uL << 46,
		UserInteractive = (UserInitiated | LatencyCritical),
		/// <summary>To be added.</summary>
		UserInitiated = 0x00FFFFFFUL | IdleSystemSleepDisabled,
		/// <summary>To be added.</summary>
		Background = 0x000000ffUL,
		/// <summary>To be added.</summary>
		LatencyCritical = 0xFF00000000UL,
		InitiatedAllowingIdleSystemSleep = UserInitiated & ~IdleSystemSleepDisabled,
	}


	/// <summary>Enumerates errors relating to <see cref="Foundation.NSItemProvider" /> methods.</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSItemProviderErrorCode : long {
		/// <summary>To be added.</summary>
		Unknown = -1,
		/// <summary>To be added.</summary>
		None = 0,
		/// <summary>To be added.</summary>
		ItemUnavailable = -1000,
		/// <summary>To be added.</summary>
		UnexpectedValueClass = -1100,
		/// <summary>To be added.</summary>
		UnavailableCoercion = -1200,
	}

	/// <summary>Enumerates <see cref="Foundation.NSDateComponentsFormatter" /> output styles.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	[MacCatalyst (13, 1)]
	public enum NSDateComponentsFormatterUnitsStyle : long {
		/// <summary>To be added.</summary>
		Positional = 0,
		/// <summary>To be added.</summary>
		Abbreviated,
		/// <summary>To be added.</summary>
		Short,
		/// <summary>To be added.</summary>
		Full,
		/// <summary>To be added.</summary>
		SpellOut,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		Brief,
	}

	/// <summary>Enumerates how zero values should be dealt with by a <see cref="Foundation.NSDateComponentsFormatter" />.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	[MacCatalyst (13, 1)]
	public enum NSDateComponentsFormatterZeroFormattingBehavior : ulong {
		/// <summary>To be added.</summary>
		None = (0),
		/// <summary>To be added.</summary>
		Default = (1 << 0),
		/// <summary>To be added.</summary>
		DropLeading = (1 << 1),
		/// <summary>To be added.</summary>
		DropMiddle = (1 << 2),
		/// <summary>To be added.</summary>
		DropTrailing = (1 << 3),
		/// <summary>To be added.</summary>
		DropAll = (DropLeading | DropMiddle | DropTrailing),
		/// <summary>To be added.</summary>
		Pad = (1 << 16),
	}

	/// <summary>Enumerates the position of the data being formatted. Used with <see cref="Foundation.NSByteCountFormatter" /> and <see cref="Foundation.NSDateComponentsFormatter" />.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	[MacCatalyst (13, 1)]
	public enum NSFormattingContext : long {
		/// <summary>To be added.</summary>
		Unknown = 0,
		/// <summary>To be added.</summary>
		Dynamic = 1,
		/// <summary>To be added.</summary>
		Standalone = 2,
		/// <summary>To be added.</summary>
		ListItem = 3,
		/// <summary>To be added.</summary>
		BeginningOfSentence = 4,
		/// <summary>To be added.</summary>
		MiddleOfSentence = 5,
	}

	/// <summary>Enumerates the output styles of a <see cref="Foundation.NSDateIntervalFormatter" />.</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSDateIntervalFormatterStyle : ulong {
		/// <summary>To be added.</summary>
		None = 0,
		/// <summary>To be added.</summary>
		Short = 1,
		/// <summary>To be added.</summary>
		Medium = 2,
		/// <summary>To be added.</summary>
		Long = 3,
		/// <summary>To be added.</summary>
		Full = 4,
	}

	/// <summary>The unit to be used by a <see cref="Foundation.NSEnergyFormatter" />.</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSEnergyFormatterUnit : long {
		/// <summary>To be added.</summary>
		Joule = 11,
		/// <summary>To be added.</summary>
		Kilojoule = 14,
		/// <summary>To be added.</summary>
		Calorie = (7 << 8) + 1,
		/// <summary>To be added.</summary>
		Kilocalorie = (7 << 8) + 2,
	}

	/// <summary>Enumerates the style (desired length) of an <see cref="Foundation.NSLengthFormatter" />, <see cref="Foundation.NSEnergyFormatter" />, or <see cref="Foundation.NSMassFormatter" />.</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSFormattingUnitStyle : long {
		/// <summary>To be added.</summary>
		Short = 1,
		/// <summary>To be added.</summary>
		Medium,
		/// <summary>To be added.</summary>
		Long,
	}

	/// <summary>Enumerates mass units (lb, kg, stone).</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSMassFormatterUnit : long {
		/// <summary>To be added.</summary>
		Gram = 11,
		/// <summary>To be added.</summary>
		Kilogram = 14,
		/// <summary>To be added.</summary>
		Ounce = (6 << 8) + 1,
		/// <summary>To be added.</summary>
		Pound = (6 << 8) + 2,
		/// <summary>To be added.</summary>
		Stone = (6 << 8) + 3,
	}

	/// <summary>Enumerates units of length (foot, meter, etc.) for use with <see cref="Foundation.NSLengthFormatter" />.</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSLengthFormatterUnit : long {
		/// <summary>To be added.</summary>
		Millimeter = 8,
		/// <summary>To be added.</summary>
		Centimeter = 9,
		/// <summary>To be added.</summary>
		Meter = 11,
		/// <summary>To be added.</summary>
		Kilometer = 14,
		/// <summary>To be added.</summary>
		Inch = (5 << 8) + 1,
		/// <summary>To be added.</summary>
		Foot = (5 << 8) + 2,
		/// <summary>To be added.</summary>
		Yard = (5 << 8) + 3,
		/// <summary>To be added.</summary>
		Mile = (5 << 8) + 4,
	}


	[MacCatalyst (13, 1)]
	[Native]
	public enum NSProcessInfoThermalState : long {
		/// <summary>To be added.</summary>
		Nominal,
		/// <summary>To be added.</summary>
		Fair,
		/// <summary>To be added.</summary>
		Serious,
		/// <summary>To be added.</summary>
		Critical,
	}

	/// <summary>Defines constants defining the relationship between a directory and an item.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSUrlRelationship : long {
		/// <summary>To be added.</summary>
		Contains,
		/// <summary>To be added.</summary>
		Same,
		/// <summary>To be added.</summary>
		Other,
	}

	// NSTextCheckingResult.h:typedef NS_OPTIONS(uint64_t, NSTextCheckingType)
	/// <summary>Enumerates available predefined regular expressions for checking text.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	public enum NSTextCheckingType : ulong {
		/// <summary>To be added.</summary>
		Orthography = 1 << 0,
		/// <summary>To be added.</summary>
		Spelling = 1 << 1,
		/// <summary>To be added.</summary>
		Grammar = 1 << 2,
		/// <summary>To be added.</summary>
		Date = 1 << 3,
		/// <summary>To be added.</summary>
		Address = 1 << 4,
		/// <summary>To be added.</summary>
		Link = 1 << 5,
		/// <summary>To be added.</summary>
		Quote = 1 << 6,
		/// <summary>To be added.</summary>
		Dash = 1 << 7,
		/// <summary>To be added.</summary>
		Replacement = 1 << 8,
		/// <summary>To be added.</summary>
		Correction = 1 << 9,
		/// <summary>To be added.</summary>
		RegularExpression = 1 << 10,
		/// <summary>To be added.</summary>
		PhoneNumber = 1 << 11,
		/// <summary>To be added.</summary>
		TransitInformation = 1 << 12,
	}

	// NSTextCheckingResult.h:typedef uint64_t NSTextCheckingTypes;
	/// <summary>Enumerates available predefined classes of regular expressions for checking text.</summary>
	/// <remarks>To be added.</remarks>
	public enum NSTextCheckingTypes : ulong {
		/// <summary>To be added.</summary>
		AllSystemTypes = 0xffffffff,
		/// <summary>To be added.</summary>
		AllCustomTypes = 0xffffffff00000000,
		/// <summary>To be added.</summary>
		AllTypes = 0xffffffffffffffff,
	}

	/// <summary>Defines options for use with <see cref="Foundation.NSRegularExpression" /> objects.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	[Flags]
	public enum NSRegularExpressionOptions : ulong {
		/// <summary>To be added.</summary>
		CaseInsensitive = 1 << 0,
		/// <summary>To be added.</summary>
		AllowCommentsAndWhitespace = 1 << 1,
		/// <summary>To be added.</summary>
		IgnoreMetacharacters = 1 << 2,
		/// <summary>To be added.</summary>
		DotMatchesLineSeparators = 1 << 3,
		/// <summary>To be added.</summary>
		AnchorsMatchLines = 1 << 4,
		UseUnixLineSeparators = 1 << 5,
		/// <summary>To be added.</summary>
		UseUnicodeWordBoundaries = 1 << 6,
	}

	/// <summary>Enumerates options for use with <see cref="Foundation.NSRegularExpression" /> regular expression objects.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	[Flags]
	public enum NSMatchingOptions : ulong {
		/// <summary>To be added.</summary>
		ReportProgress = 1 << 0,
		/// <summary>To be added.</summary>
		ReportCompletion = 1 << 1,
		/// <summary>To be added.</summary>
		Anchored = 1 << 2,
		/// <summary>To be added.</summary>
		WithTransparentBounds = 1 << 3,
		/// <summary>To be added.</summary>
		WithoutAnchoringBounds = 1 << 4,
	}

	/// <summary>Enumerates flags for use with the <see cref="Foundation.NSMatchEnumerator" /> delegate.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	[Flags]
	public enum NSMatchingFlags : ulong {
		/// <summary>To be added.</summary>
		Progress = 1 << 0,
		/// <summary>To be added.</summary>
		Completed = 1 << 1,
		/// <summary>To be added.</summary>
		HitEnd = 1 << 2,
		/// <summary>To be added.</summary>
		RequiredEnd = 1 << 3,
		/// <summary>To be added.</summary>
		InternalError = 1 << 4,
	}

	/// <summary>Contains a constant that, if specified, indicates that the phonetic representation of a name should be formatted, rather than the name object's own components.</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	[Flags]
	public enum NSPersonNameComponentsFormatterOptions : ulong {
		/// <summary>Value that indicates that the phonetic representation of a name should be formatted, rather than the name object's own components.</summary>
		Phonetic = (1 << 1),
	}

	/// <summary>Enumerates values that control the way that names are displayed.</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSPersonNameComponentsFormatterStyle : long {
		/// <summary>Display a medium-length representation of the name. Equivalent to <c>Medium</c>.</summary>
		Default = 0,
		/// <summary>Display a shortened form of the name by abbreviating or omitting various, user-specified, components.</summary>
		Short,
		/// <summary>Display a medium-length representation of the name. Equivalent to <c>Default</c>.</summary>
		Medium,
		/// <summary>Use all available name components, except for the nickname, to display the name.</summary>
		Long,
		/// <summary>Display the most abbreviated form of the name.</summary>
		Abbreviated,
	}


	/// <summary>A flagging enumeration of formatting options for use with <see cref="Foundation.NSIso8601DateFormatter" />.</summary>
	/// <remarks>
	///       <para>It is often easier to code a format by removing flags from <see cref="Foundation.NSIso8601DateFormatOptions.InternetDateTime" /> such as:</para>
	///       <example>
	///         <code lang="csharp lang-csharp"><![CDATA[
	/// fmt.FormatOptions = fmt.FormatOptions & ~NSIso8601DateFormatOptions.Day
	/// var s = fmt.StringFor(d); //"2016-08T23:15:36Z"          
	///           ]]></code>
	///       </example>
	///     </remarks>
	[MacCatalyst (13, 1)]
	[Native]
	[Flags]
	public enum NSIso8601DateFormatOptions : ulong {
		/// <summary>Includes the year in the string.</summary>
		Year = 1 << 0,
		/// <summary>Includes the month in the string.</summary>
		Month = 1 << 1,
		/// <summary>Includes the week of the year in the string (e.g., "W33").</summary>
		WeekOfYear = 1 << 2,
		/// <summary>Includes the day in the string.</summary>
		Day = 1 << 4,
		/// <summary>Includes the time in the string.</summary>
		Time = 1 << 5,
		/// <summary>Includes the timezone in the string.</summary>
		TimeZone = 1 << 6,
		/// <summary>Puts a space between the date and the time.</summary>
		SpaceBetweenDateAndTime = 1 << 7,
		/// <summary>Separates date components with a dash.</summary>
		DashSeparatorInDate = 1 << 8,
		/// <summary>Puts a colon between time components</summary>
		ColonSeparatorInTime = 1 << 9,
		/// <summary>Puts a colon in the time.</summary>
		ColonSeparatorInTimeZone = 1 << 10,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		FractionalSeconds = 1 << 11,
		/// <summary>Includes the date in the string.</summary>
		FullDate = Year | Month | Day | DashSeparatorInDate,
		/// <summary>Includes the time in the string.</summary>
		FullTime = Time | ColonSeparatorInTime | TimeZone | ColonSeparatorInTimeZone,
		/// <summary>The standard ISO-8601 format (<c>yyyy-MM-ddTHH:mm:ssZ</c>).</summary>
		InternetDateTime = FullDate | FullTime,
	}


	[MacCatalyst (13, 1)]
	[Native]
	[Flags]
	public enum NSMeasurementFormatterUnitOptions : ulong {
		/// <summary>To be added.</summary>
		ProvidedUnit = (1 << 0),
		/// <summary>To be added.</summary>
		NaturalScale = (1 << 1),
		/// <summary>To be added.</summary>
		TemperatureWithoutUnit = (1 << 2),
	}


	/// <summary>Specifies how an item provider supplies a file.</summary>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSItemProviderFileOptions : long {
		/// <summary>Opens the file in place instead of copying it.</summary>
		OpenInPlace = 1,
	}

	/// <summary>Enumerate the linguistic units recognized by the <see cref="Foundation.NSLinguisticTagger" /> class.</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSLinguisticTaggerUnit : long {
		/// <summary>To be added.</summary>
		Word,
		/// <summary>To be added.</summary>
		Sentence,
		/// <summary>To be added.</summary>
		Paragraph,
		/// <summary>To be added.</summary>
		Document,
	}


	[Native]
	public enum NSXpcConnectionOptions : ulong {
		Privileged = (1 << 12),
	}

	[MacCatalyst (13, 1)]
	public enum NSFileProtectionType {
		[Field ("NSFileProtectionComplete")]
		Complete,
		[Field ("NSFileProtectionCompleteUnlessOpen")]
		CompleteUnlessOpen,
		[Field ("NSFileProtectionCompleteUntilFirstUserAuthentication")]
		CompleteUntilFirstUserAuthentication,
		[Field ("NSFileProtectionNone")]
		None,
		[TV (17, 0), NoMac, iOS (17, 0), MacCatalyst (17, 0)]
		[Field ("NSFileProtectionCompleteWhenUserInactive")]
		CompleteWhenUserInactive,
	}
}
