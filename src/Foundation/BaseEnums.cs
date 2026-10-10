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

	/// <summary>Encodings supported by NSString.Encode.</summary>
	/// <remarks>The encodings supported by NSData and NSString.</remarks>
	[Native]
	public enum NSStringEncoding : ulong {
		/// <summary>ASCII encoding contains, 7-bit of information stored in 8 bits.</summary>
		ASCIIStringEncoding = 1,
		/// <summary>To be added.</summary>
		NEXTSTEP = 2,
		/// <summary>To be added.</summary>
		JapaneseEUC = 3,
		/// <summary>8-bit based variable-length character encoding for Unicode. </summary>
		UTF8 = 4,
		/// <summary>8-bit ISO/IEC 8859-1, also knows as Latin1 encoding.</summary>
		ISOLatin1 = 5,
		/// <summary>To be added.</summary>
		Symbol = 6,
		/// <summary>To be added.</summary>
		NonLossyASCII = 7,
		/// <summary>To be added.</summary>
		ShiftJIS = 8,
		/// <summary>8-bit ISO/IEC 8859-2, also knows as Latin2 encoding.</summary>
		ISOLatin2 = 9,
		/// <summary>To be added.</summary>
		Unicode = 10,
		/// <summary>To be added.</summary>
		WindowsCP1251 = 11,
		/// <summary>To be added.</summary>
		WindowsCP1252 = 12,
		/// <summary>To be added.</summary>
		WindowsCP1253 = 13,
		/// <summary>To be added.</summary>
		WindowsCP1254 = 14,
		/// <summary>To be added.</summary>
		WindowsCP1250 = 15,
		/// <summary>To be added.</summary>
		ISO2022JP = 21,
		/// <summary>To be added.</summary>
		MacOSRoman = 30,
		/// <summary>16 bit-based variable length encoding, blocks are interpreted as big endian.</summary>
		UTF16BigEndian = 0x90000100,
		/// <summary>16 bit-based variable length encoding, blocks are interpreted as litle endian. </summary>
		UTF16LittleEndian = 0x94000100,
		/// <summary>32-bit unicode encoding.</summary>
		UTF32 = 0x8c000100,
		/// <summary>32 bit encoding, blocks are interpreted as big endian.</summary>
		UTF32BigEndian = 0x98000100,
		/// <summary>32 bit encoding, blocks are interpreted as little endian.</summary>
		UTF32LittleEndian = 0x9c000100,
	};


	/// <summary>An enumeration of options available to NSString search and comparison methods.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSStringCompareOptions : ulong {
		/// <summary>To be added.</summary>
		CaseInsensitiveSearch = 1,
		/// <summary>To be added.</summary>
		LiteralSearch = 2,
		/// <summary>To be added.</summary>
		BackwardsSearch = 4,
		/// <summary>To be added.</summary>
		AnchoredSearch = 8,
		/// <summary>To be added.</summary>
		NumericSearch = 64,
		/// <summary>To be added.</summary>
		DiacriticInsensitiveSearch = 128,
		/// <summary>To be added.</summary>
		WidthInsensitiveSearch = 256,
		/// <summary>To be added.</summary>
		ForcedOrderingSearch = 512,
		/// <summary>To be added.</summary>
		RegularExpressionSearch = 1024,
	}


	/// <summary>Determines how credentials are persisted.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSUrlCredentialPersistence : ulong {
		/// <summary>Not persisted.</summary>
		None,
		/// <summary>Persisted for the session.</summary>
		ForSession,
		/// <summary>Permanently.</summary>
		Permanent,
		/// <summary>To be added.</summary>
		Synchronizable,
	}


#if MONOMAC

	/// <summary>The bundle's architecture.</summary>
	[NoiOS]
	[NoTV]
	[NoMacCatalyst]
	public enum NSBundleExecutableArchitecture {
		/// <summary>x86</summary>
		I386 = 0x00000007,
		/// <summary>PPC</summary>
		PPC = 0x00000012,
		/// <summary>x86-64</summary>
		X86_64 = 0x01000007,
		/// <summary>PPC64</summary>
		PPC64 = 0x01000012,
		ARM64 = 0x0100000c,
	}
#endif

	/// <summary>Comparison result in the Foundation Framework.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSComparisonResult : long {
		/// <summary>To be added.</summary>
		Ascending = -1,
		/// <summary>To be added.</summary>
		Same,
		/// <summary>To be added.</summary>
		Descending,
	}


	/// <summary>NSUrlRequest caching policy.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSUrlRequestCachePolicy : ulong {
		/// <summary>To be added.</summary>
		UseProtocolCachePolicy = 0,
		/// <summary>To be added.</summary>
		ReloadIgnoringLocalCacheData = 1,
		/// <summary>To be added.</summary>
		ReloadIgnoringLocalAndRemoteCacheData = 4, // Unimplemented
		/// <summary>To be added.</summary>
		ReloadIgnoringCacheData = ReloadIgnoringLocalCacheData,

		/// <summary>To be added.</summary>
		ReturnCacheDataElseLoad = 2,
		/// <summary>To be added.</summary>
		ReturnCacheDataDoNotLoad = 3,

		/// <summary>To be added.</summary>
		ReloadRevalidatingCacheData = 5, // Unimplemented
	}


	/// <summary>An enumeration of values representing valid caching strategies for use with NSUrls.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSUrlCacheStoragePolicy : ulong {
		/// <summary>To be added.</summary>
		Allowed,
		/// <summary>To be added.</summary>
		AllowedInMemoryOnly,
		/// <summary>To be added.</summary>
		NotAllowed,
	}


	/// <summary>The current status of an NSStream.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSStreamStatus : ulong {
		/// <summary>The stream is not yet open.</summary>
		NotOpen = 0,
		/// <summary>The stream is in the opening state.</summary>
		Opening = 1,
		/// <summary>The stream has been opened.</summary>
		Open = 2,
		/// <summary>The stream is reading.</summary>
		Reading = 3,
		/// <summary>The stream is writing.</summary>
		Writing = 4,
		/// <summary>At the end.</summary>
		AtEnd = 5,
		/// <summary>The stream is closed.</summary>
		Closed = 6,
		/// <summary>Error</summary>
		Error = 7,
	}


	/// <summary>Status codes for the NSNetService.</summary>
	/// <remarks>To be added.</remarks>
	[NoWatch]
	[Native]
	public enum NSNetServicesStatus : long {
		/// <summary>To be added.</summary>
		UnknownError = -72000,
		/// <summary>To be added.</summary>
		CollisionError = -72001,
		/// <summary>To be added.</summary>
		NotFoundError = -72002,
		/// <summary>To be added.</summary>
		ActivityInProgress = -72003,
		/// <summary>To be added.</summary>
		BadArgumentError = -72004,
		/// <summary>To be added.</summary>
		CancelledError = -72005,
		/// <summary>To be added.</summary>
		InvalidError = -72006,
		/// <summary>To be added.</summary>
		TimeoutError = -72007,
		MissingRequiredConfigurationError = -72008,
	}


	/// <summary>An enumeration whose values specify valid strategies for accepting <see cref="Foundation.NSHttpCookie" />s.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSHttpCookieAcceptPolicy : ulong {
		/// <summary>To be added.</summary>
		Always,
		/// <summary>To be added.</summary>
		Never,
		/// <summary>To be added.</summary>
		OnlyFromMainDocumentDomain,
	}


	/// <summary>An enumeration whose values specify calendrical units (e.g., seconds, months, eras).</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSCalendarUnit : ulong {
		/// <summary>To be added.</summary>
		Era = 2,
		/// <summary>To be added.</summary>
		Year = 4,
		/// <summary>To be added.</summary>
		Month = 8,
		/// <summary>To be added.</summary>
		Day = 16,
		/// <summary>To be added.</summary>
		Hour = 32,
		/// <summary>To be added.</summary>
		Minute = 64,
		/// <summary>To be added.</summary>
		Second = 128,
		/// <summary>Developers should not use this deprecated field. </summary>
		[Deprecated (PlatformName.MacOSX, 10, 10)]
		[Deprecated (PlatformName.iOS, 8, 0)]
		[Deprecated (PlatformName.TvOS, 9, 0)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1)]
		Week = 256,
		/// <summary>To be added.</summary>
		Weekday = 512,
		/// <summary>To be added.</summary>
		WeekdayOrdinal = 1024,
		/// <summary>To be added.</summary>
		Quarter = 2048,

		/// <summary>To be added.</summary>
		WeekOfMonth = (1 << 12),
		/// <summary>To be added.</summary>
		WeekOfYear = (1 << 13),
		/// <summary>To be added.</summary>
		YearForWeakOfYear = (1 << 14),

		/// <summary>To be added.</summary>
		Nanosecond = (1 << 15),
		DayOfYear = (1 << 16),

		/// <summary>To be added.</summary>
		Calendar = (1 << 20),
		/// <summary>To be added.</summary>
		TimeZone = (1 << 21),

		[TV (26, 0), Mac (26, 0), iOS (26, 0), MacCatalyst (26, 0)]
		IsLeapMonth = (1 << 30),

		[TV (26, 0), Mac (26, 0), iOS (26, 0), MacCatalyst (26, 0)]
		IsRepeatedDay = (1UL << 31),
	}


	/// <summary>Flags that determine how NSData loads files.</summary>
	/// <remarks>
	///       <para>
	/// 	By default NSData will loads the contents of the file in memory
	/// 	by allocating a block of memory and then reading the contents of
	/// 	the file into it.
	///       </para>
	///       <para>
	/// 	The Mapped and MappedAlways parameter instruct NSData to use
	/// 	the kernel's interface to map the file into the process
	/// 	address space.  This has a few advantages: instead of
	/// 	allocating read/write memory for the process, that becomes
	/// 	real memory usage, the mapped versions map the file into
	/// 	memory which means that the data is loaded on demand instead
	/// 	of being loaded upfront.  This also allows the kernel to
	/// 	discard the data loaded from memory when the system is running
	/// 	low on memory.
	///       </para>
	///     </remarks>
	[Flags]
	[Native]
	public enum NSDataReadingOptions : ulong {
		/// <summary>Use the kernel's virtual memory map to load the file, if possible. If sucessful, this replaces read/write memory that can be very expensive with discardable memory that is backed by a file.</summary>
		Mapped = 1 << 0,
		/// <summary>Notify the kernel that it should not try to cache the contents of this file in its buffer cache.</summary>
		Uncached = 1 << 1,
		/// <summary>Force NSData to try to use the kernel's mapping support to load the file.   If sucessful, this replaces read/write memory that can be very expensive with discardable memory that is backed by a file.</summary>
		MappedAlways = 1 << 3,
	}


	/// <summary>An enumeration of options to be used when writing <see cref="Foundation.NSData" /> objects.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSDataWritingOptions : ulong {
		/// <summary>To be added.</summary>
		Atomic = 1,

		/// <summary>To be added.</summary>
		WithoutOverwriting = 2,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		FileProtectionNone = 0x10000000,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		FileProtectionComplete = 0x20000000,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		FileProtectionMask = 0xf0000000,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		FileProtectionCompleteUnlessOpen = 0x30000000,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		FileProtectionCompleteUntilFirstUserAuthentication = 0x40000000,
		[iOS (17, 0), NoMac, MacCatalyst (17, 0), TV (17, 0)]
		FileProtectionCompleteWhenUserInactive = 0x50000000,
	}


	/// <param name="obj">To be added.</param>
	/// <param name="stop">To be added.</param>
	/// <summary>A delegate that specifies the enumerator used by <see cref="Foundation.NSSet.Enumerate(Foundation.NSSetEnumerator)" />.</summary>
	/// <remarks>To be added.</remarks>
	public delegate void NSSetEnumerator (NSObject obj, ref bool stop);


	/// <summary>An enumeration of values that specify the priority of an operation, relative to others, in a <see cref="Foundation.NSOperationQueue" />.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSOperationQueuePriority : long {
		/// <summary>To be added.</summary>
		VeryLow = -8,
		/// <summary>To be added.</summary>
		Low = -4,
		/// <summary>To be added.</summary>
		Normal = 0,
		/// <summary>To be added.</summary>
		High = 4,
		/// <summary>To be added.</summary>
		VeryHigh = 8,
	}


	/// <summary>Flags controling the search in NSData's Find method.</summary>
	/// <remarks>
	///     </remarks>
	[Flags]
	[Native]
	public enum NSDataSearchOptions : ulong {
		/// <summary>Starts search from the end, instead of the start.</summary>
		SearchBackwards = 1,
		/// <summary>Limits the search to the start (or end if SearchBackwards is specified)</summary>
		SearchAnchored = 2,
	}


	// note: Make sure names are identical/consistent with CFNetworkErrors.*
	// they share the same values but there's more entries in CFNetworkErrors
	// so anything new probably already exists over there
	/// <summary>An enumeration of errors associated with creating or loading a <see cref="Foundation.NSUrl" />.</summary>
	/// <remarks>To be added.</remarks>
	public enum NSUrlError : int {
		/// <summary>To be added.</summary>
		Unknown = -1,

		/// <summary>To be added.</summary>
		BackgroundSessionRequiresSharedContainer = -995,
		/// <summary>To be added.</summary>
		BackgroundSessionInUseByAnotherProcess = -996,
		/// <summary>To be added.</summary>
		BackgroundSessionWasDisconnected = -997,

		/// <summary>To be added.</summary>
		Cancelled = -999,
		/// <summary>To be added.</summary>
		BadURL = -1000,
		/// <summary>To be added.</summary>
		TimedOut = -1001,
		/// <summary>To be added.</summary>
		UnsupportedURL = -1002,
		/// <summary>To be added.</summary>
		CannotFindHost = -1003,
		/// <summary>To be added.</summary>
		CannotConnectToHost = -1004,
		/// <summary>To be added.</summary>
		NetworkConnectionLost = -1005,
		/// <summary>To be added.</summary>
		DNSLookupFailed = -1006,
		/// <summary>To be added.</summary>
		HTTPTooManyRedirects = -1007,
		/// <summary>To be added.</summary>
		ResourceUnavailable = -1008,
		/// <summary>To be added.</summary>
		NotConnectedToInternet = -1009,
		/// <summary>To be added.</summary>
		RedirectToNonExistentLocation = -1010,
		/// <summary>To be added.</summary>
		BadServerResponse = -1011,
		/// <summary>To be added.</summary>
		UserCancelledAuthentication = -1012,
		/// <summary>To be added.</summary>
		UserAuthenticationRequired = -1013,
		/// <summary>To be added.</summary>
		ZeroByteResource = -1014,
		/// <summary>To be added.</summary>
		CannotDecodeRawData = -1015,
		/// <summary>To be added.</summary>
		CannotDecodeContentData = -1016,
		/// <summary>To be added.</summary>
		CannotParseResponse = -1017,
		/// <summary>To be added.</summary>
		InternationalRoamingOff = -1018,
		/// <summary>To be added.</summary>
		CallIsActive = -1019,
		/// <summary>To be added.</summary>
		DataNotAllowed = -1020,
		/// <summary>To be added.</summary>
		RequestBodyStreamExhausted = -1021,
		/// <summary>To be added.</summary>
		AppTransportSecurityRequiresSecureConnection = -1022,

		/// <summary>To be added.</summary>
		FileDoesNotExist = -1100,
		/// <summary>To be added.</summary>
		FileIsDirectory = -1101,
		/// <summary>To be added.</summary>
		NoPermissionsToReadFile = -1102,
		/// <summary>To be added.</summary>
		DataLengthExceedsMaximum = -1103,
		/// <summary>To be added.</summary>
		FileOutsideSafeArea = -1104,

		/// <summary>To be added.</summary>
		SecureConnectionFailed = -1200,
		/// <summary>To be added.</summary>
		ServerCertificateHasBadDate = -1201,
		/// <summary>To be added.</summary>
		ServerCertificateUntrusted = -1202,
		/// <summary>To be added.</summary>
		ServerCertificateHasUnknownRoot = -1203,
		/// <summary>To be added.</summary>
		ServerCertificateNotYetValid = -1204,
		/// <summary>To be added.</summary>
		ClientCertificateRejected = -1205,
		/// <summary>To be added.</summary>
		ClientCertificateRequired = -1206,

		/// <summary>To be added.</summary>
		CannotLoadFromNetwork = -2000,

		// Download and file I/O errors
		/// <summary>To be added.</summary>
		CannotCreateFile = -3000,
		/// <summary>To be added.</summary>
		CannotOpenFile = -3001,
		/// <summary>To be added.</summary>
		CannotCloseFile = -3002,
		/// <summary>To be added.</summary>
		CannotWriteToFile = -3003,
		/// <summary>To be added.</summary>
		CannotRemoveFile = -3004,
		/// <summary>To be added.</summary>
		CannotMoveFile = -3005,
		/// <summary>To be added.</summary>
		DownloadDecodingFailedMidStream = -3006,
		/// <summary>To be added.</summary>
		DownloadDecodingFailedToComplete = -3007,
	}


	/// <summary>An enumeration of values specifying options to be used with the <see cref="Foundation.NSObject.AddObserver(Foundation.NSObject,System.String,Foundation.NSKeyValueObservingOptions,System.IntPtr)" /> method.</summary>
	/// <remarks>
	///       <para>
	///       </para>
	///     </remarks>
	[Flags]
	[Native]
	public enum NSKeyValueObservingOptions : ulong {
		None = 0,
		/// <summary>Request that new values be delivered to the observer.</summary>
		New = 1,
		/// <summary>Requests that old values be delivered to the observer.</summary>
		Old = 2,
		/// <summary>Requests that both old and new values be delivered to the observer.</summary>
		OldNew = 3,
		/// <summary>Use this to send a notification on first use, without waiting for an actual change to take place.</summary>
		Initial = 4,
		/// <summary>If set, notifications are sent before and after the change to the property is made.   Otherwise the notifications are only sent after the property has been set.</summary>
		Prior = 8,
	}


	/// <summary>An enumeration indicating the type of change occurring in the <see cref="Foundation.NSObject.WillChangeValue(System.String)" /> and <see cref="Foundation.NSObject.DidChangeValue(System.String)" /> methods.</summary>
	/// <remarks>
	///       <para />
	///     </remarks>
	[Native]
	public enum NSKeyValueChange : ulong {
		/// <summary>The change is reported for setting a value in a property.</summary>
		Setting = 1,
		/// <summary>The change being reported is an insertion into a collection.</summary>
		Insertion,
		/// <summary>The change being reported is a removal from a collection.</summary>
		Removal,
		/// <summary>The change being reported is a set of changes in a collection.</summary>
		Replacement,
	}


	/// <summary>An enumeration of values indicating the operation being performed on a mutable key-value store.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSKeyValueSetMutationKind : ulong {
		/// <summary>To be added.</summary>
		UnionSet = 1,
		/// <summary>To be added.</summary>
		MinusSet,
		/// <summary>To be added.</summary>
		IntersectSet,
		/// <summary>To be added.</summary>
		SetSet,
	}


	/// <summary>An enumeration of valid options for use when enumerating over Blocks.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSEnumerationOptions : ulong {
		/// <summary>To be added.</summary>
		SortConcurrent = 1,
		/// <summary>To be added.</summary>
		Reverse = 2,
	}


	/// <summary>An enumeration of values that may be sent to <see cref="Foundation.NSStreamDelegate.HandleEvent(Foundation.NSStream,Foundation.NSStreamEvent)" />.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSStreamEvent : ulong {
		/// <summary>To be added.</summary>
		None = 0,
		/// <summary>To be added.</summary>
		OpenCompleted = 1 << 0,
		/// <summary>To be added.</summary>
		HasBytesAvailable = 1 << 1,
		/// <summary>To be added.</summary>
		HasSpaceAvailable = 1 << 2,
		/// <summary>To be added.</summary>
		ErrorOccurred = 1 << 3,
		/// <summary>To be added.</summary>
		EndEncountered = 1 << 4,
	}


	/// <summary>An enumeration of values that specify rounding behaviors for <see cref="Foundation.NSDecimal" />s.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSRoundingMode : ulong {
		/// <summary>To be added.</summary>
		Plain,
		/// <summary>To be added.</summary>
		Down,
		/// <summary>To be added.</summary>
		Up,
		/// <summary>To be added.</summary>
		Bankers,
	}


	/// <summary>An enumeration whose values indicate a specific calculation error (e.g., underflow, division by zero, loss of precision).</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSCalculationError : ulong {
		/// <summary>To be added.</summary>
		None,
		/// <summary>To be added.</summary>
		PrecisionLoss,
		/// <summary>To be added.</summary>
		Underflow,
		/// <summary>To be added.</summary>
		Overflow,
		/// <summary>To be added.</summary>
		DivideByZero,
	}


	/// <summary>An enumeration of values that specify the direction of text for a language.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSLocaleLanguageDirection : ulong {
		/// <summary>To be added.</summary>
		Unknown,
		/// <summary>To be added.</summary>
		LeftToRight,
		/// <summary>To be added.</summary>
		RightToLeft,
		/// <summary>To be added.</summary>
		TopToBottom,
		/// <summary>To be added.</summary>
		BottomToTop,
	}


	/// <summary>An enumeration of options ot be used when creating a bookmark.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSUrlBookmarkCreationOptions : ulong {
		/// <summary>To be added.</summary>
		PreferFileIDResolution = 1 << 8,
		/// <summary>To be added.</summary>
		MinimalBookmark = 1 << 9,
		/// <summary>To be added.</summary>
		SuitableForBookmarkFile = 1 << 10,
		/// <summary>To be added.</summary>
		[NoWatch]
		[NoiOS, NoTV]
		[NoMacCatalyst]
		WithSecurityScope = 1 << 11,
		/// <summary>To be added.</summary>
		[NoWatch]
		[NoiOS, NoTV]
		[NoMacCatalyst]
		SecurityScopeAllowOnlyReadAccess = 1 << 12,
		[iOS (15, 0), TV (15, 0), MacCatalyst (15, 0)]
		CreationWithoutImplicitSecurityScope = 1 << 29,
	}


	/// <summary>An enumeration of options to be used when creating an NSUrl by resolving a bookmark.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSUrlBookmarkResolutionOptions : ulong {
		/// <summary>To be added.</summary>
		WithoutUI = 1 << 8,
		/// <summary>To be added.</summary>
		WithoutMounting = 1 << 9,
		/// <summary>To be added.</summary>
		[NoWatch]
		[NoiOS, NoTV]
		[NoMacCatalyst]
		WithSecurityScope = 1 << 10,
		[iOS (15, 0), TV (15, 0), MacCatalyst (15, 0)]
		WithoutImplicitStartAccessing = 1 << 15,
	}


	/// <summary>A flagging enumeration whose values specify options in calls to <see cref="NSCalendar.Components" /> and <see cref="Foundation.NSCalendar.DateByAddingComponents(Foundation.NSDateComponents,Foundation.NSDate,Foundation.NSCalendarOptions)" />.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSCalendarOptions : ulong {
		/// <summary>To be added.</summary>
		None = 0,
		/// <summary>To be added.</summary>
		WrapCalendarComponents = 1 << 0,

		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		MatchStrictly = 1 << 1,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		SearchBackwards = 1 << 2,

		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		MatchPreviousTimePreservingSmallerUnits = 1 << 8,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		MatchNextTimePreservingSmallerUnits = 1 << 9,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		MatchNextTime = 1 << 10,

		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		MatchFirst = 1 << 12,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		MatchLast = 1 << 13,
	}


	/// <summary>Network service types for <see cref="Foundation.NSUrlRequest" />.</summary>
	/// <remarks>
	///     </remarks>
	[Native]
	public enum NSUrlRequestNetworkServiceType : ulong {
		/// <summary>Default traffic</summary>
		Default,
		/// <summary>Voice over IP traffic.</summary>
		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'PushKit' framework instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'PushKit' framework instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'PushKit' framework instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'PushKit' framework instead.")]
		VoIP,
		/// <summary>Video traffic.</summary>
		Video,
		/// <summary>Background traffic.</summary>
		Background,
		/// <summary>Voice traffic.</summary>
		Voice,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		ResponsiveData = 6,
		[MacCatalyst (13, 1)]
		AVStreaming = 8,
		[MacCatalyst (13, 1)]
		ResponsiveAV = 9,
		/// <summary>To be added.</summary>
		[MacCatalyst (13, 1)]
		CallSignaling = 11,
	}


	/// <summary>A flagging enumeration that specifies sorting options in calls to <see cref="NSMutableOrderedSet.Sort" />.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSSortOptions : ulong {
		/// <summary>To be added.</summary>
		Concurrent = 1 << 0,
		/// <summary>To be added.</summary>
		Stable = 1 << 4,
	}


	/// <summary>A flagging enumeration that can be used with the <see cref="Foundation.NSData(Foundation.NSData,Foundation.NSDataBase64DecodingOptions)" /> constructor.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSDataBase64DecodingOptions : ulong {
		/// <summary>To be added.</summary>
		None = 0,
		/// <summary>To be added.</summary>
		IgnoreUnknownCharacters = 1,
	}


	/// <summary>A flagging enumeration that can be used to specify options for <see cref="Foundation.NSData.GetBase64EncodedData(Foundation.NSDataBase64EncodingOptions)" /> and <see cref="Foundation.NSData.GetBase64EncodedString(Foundation.NSDataBase64EncodingOptions)" />.</summary>
	/// <remarks>To be added.</remarks>
	[Flags]
	[Native]
	public enum NSDataBase64EncodingOptions : ulong {
		/// <summary>To be added.</summary>
		None = 0,
		/// <summary>To be added.</summary>
		SixtyFourCharacterLineLength = 1,
		/// <summary>To be added.</summary>
		SeventySixCharacterLineLength = 1 << 1,
		/// <summary>To be added.</summary>
		EndLineWithCarriageReturn = 1 << 4,
		/// <summary>To be added.</summary>
		EndLineWithLineFeed = 1 << 5,
	}


	/// <summary>An enumeration whose values specify the state of an authorization challenge.</summary>
	/// <remarks>
	///       <para>Instances of this type are passed as an argument to the completion handler callback in <see cref="Foundation.NSUrlSessionDelegate.DidReceiveChallenge(Foundation.NSUrlSession,Foundation.NSUrlAuthenticationChallenge,System.Action{Foundation.NSUrlSessionAuthChallengeDisposition,Foundation.NSUrlCredential})" /> and its overrides.</para>
	///     </remarks>
	[Native]
	public enum NSUrlSessionAuthChallengeDisposition : long {
		/// <summary>To be added.</summary>
		UseCredential = 0,
		/// <summary>To be added.</summary>
		PerformDefaultHandling = 1,
		/// <summary>To be added.</summary>
		CancelAuthenticationChallenge = 2,
		/// <summary>To be added.</summary>
		RejectProtectionSpace = 3,
	}


	/// <summary>An enumeration whose values specify the state of a <see cref="NSUrlSessionTask" />.</summary>
	[Native]
	public enum NSUrlSessionTaskState : long {
		/// <summary>To be added.</summary>
		Running = 0,
		/// <summary>To be added.</summary>
		Suspended = 1,
		/// <summary>To be added.</summary>
		Canceling = 2,
		/// <summary>To be added.</summary>
		Completed = 3,
	}


	/// <summary>An enumeration whose values specify the state of a response.</summary>
	/// <remarks>An instance of this class is passed as an argument to the completion handler of the <see cref="Foundation.NSUrlSessionDataDelegate.DidReceiveResponse(Foundation.NSUrlSession,Foundation.NSUrlSessionDataTask,Foundation.NSUrlResponse,System.Action{Foundation.NSUrlSessionResponseDisposition})" /> method.</remarks>
	[Native]
	public enum NSUrlSessionResponseDisposition : long {
		/// <summary>To be added.</summary>
		Cancel = 0,
		/// <summary>To be added.</summary>
		Allow = 1,
		/// <summary>To be added.</summary>
		BecomeDownload = 2,
		/// <summary>To be added.</summary>
		BecomeStream = 3,
	}


	/// <summary>Specifies styles for time-zone names.</summary>
	/// <remarks>To be added.</remarks>
	[Native]
	public enum NSTimeZoneNameStyle : long {
		/// <summary>To be added.</summary>
		Standard,
		/// <summary>To be added.</summary>
		ShortStandard,
		/// <summary>To be added.</summary>
		DaylightSaving,
		/// <summary>To be added.</summary>
		ShortDaylightSaving,
		/// <summary>To be added.</summary>
		Generic,
		/// <summary>To be added.</summary>
		ShortGeneric,
	}


	/// <summary>Enumerates QoS values for use with <see cref="Foundation.NSOperation" /> objects and <see cref="Foundation.NSOperationQueue" /> objects.</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSQualityOfService : long {
		/// <summary>To be added.</summary>
		UserInteractive = 33,
		/// <summary>To be added.</summary>
		UserInitiated = 25,
		/// <summary>To be added.</summary>
		Utility = 17,
		/// <summary>To be added.</summary>
		Background = 9,
		/// <summary>To be added.</summary>
		Default = -1,
	}


	/// <summary>Enumerates the manner in which a <see cref="Foundation.NSCoder" /> fails. (See <see cref="Foundation.NSCoder.DecodingFailurePolicy" />)</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSDecodingFailurePolicy : long {
		/// <summary>To be added.</summary>
		RaiseException,
		/// <summary>To be added.</summary>
		SetErrorAndReturn,
	}


	/// <summary>Enumerates the way a network resource might be loaded.</summary>
	/// <remarks>To be added.</remarks>
	[MacCatalyst (13, 1)]
	[Native]
	public enum NSUrlSessionTaskMetricsResourceFetchType : long {
		/// <summary>The manner of the resource loading is not known.</summary>
		Unknown,
		/// <summary>The resource was retrieved via a network connection.</summary>
		NetworkLoad,
		/// <summary>The resource was pushed from a server.</summary>
		[Deprecated (PlatformName.iOS, 18, 4, message: "Not supported in iOS 17+.")] // message mentions one OS version and the actual deprecation OS version don't match, but that's what the headers do.
		[Deprecated (PlatformName.TvOS, 18, 4, message: "Not supported in tvOS 17+.")] // message mentions one OS version and the actual deprecation OS version don't match, but that's what the headers do.
		[Deprecated (PlatformName.MacCatalyst, 18, 4, message: "Not supported in Mac Catalyst 17+.")] // message mentions one OS version and the actual deprecation OS version don't match, but that's what the headers do.
		[Deprecated (PlatformName.MacOSX, 15, 4, message: "Not supported in macOS 14+.")] // message mentions one OS version and the actual deprecation OS version don't match, but that's what the headers do.
		ServerPush,
		/// <summary>The resource was retrieved from a local cache.</summary>
		LocalCache,
	}



	[MacCatalyst (13, 1)]
	[Native]
	public enum NSItemProviderRepresentationVisibility : long {
		/// <summary>To be added.</summary>
		All = 0,
		/// <summary>To be added.</summary>
		[NoMac]
		[MacCatalyst (13, 1)]
		Team = 1,
		/// <summary>To be added.</summary>
		[NoWatch]
		[NoiOS, NoTV]
		[NoMacCatalyst]
		Group = 2,
		/// <summary>To be added.</summary>
		OwnProcess = 3,
	}


	[MacCatalyst (13, 1)]
	[Native]
	public enum NSUrlSessionDelayedRequestDisposition : long {
		/// <summary>To be added.</summary>
		ContinueLoading = 0,
		/// <summary>To be added.</summary>
		UseNewRequest = 1,
		/// <summary>To be added.</summary>
		Cancel = 2,
	}

}
