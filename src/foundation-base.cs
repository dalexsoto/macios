//
// This file describes the API that the generator will produce
//
// Authors:
//   Geoff Norton
//   Miguel de Icaza
//   Aaron Bockover
//
// Copyright 2009, Novell, Inc.
// Copyright 2010, Novell, Inc.
// Copyright 2011-2013 Xamarin Inc.
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
//

#define DOUBLE_BLOCKS

global using nfloat = global::System.Runtime.InteropServices.NFloat;

#if !WATCHOS
using CloudKit;
using CoreData;
using CoreFoundation;
using CoreGraphics;
using UniformTypeIdentifiers;
using Network;
#if HAS_APPCLIP
using AppClip;
#endif
#if IOS
using QuickLook;
#endif
#if !TVOS
using Contacts;
#endif
using CoreAnimation;
using CoreSpotlight;
using CoreMedia;
using SceneKit;
using Security;
#if IOS || MONOMAC
using FileProvider;
#else
using INSFileProviderItem = Foundation.NSObject;
#endif

#if MONOMAC
using AppKit;
using QuickLookUI;
#else
using CoreLocation;
using UIKit;
#endif

using System.ComponentModel;

// In Apple headers, this is a typedef to a pointer to a private struct
using NSAppleEventManagerSuspensionID = System.IntPtr;
// These two are both four char codes i.e. defined on a uint with constant like 'xxxx'
using AEKeyword = System.UInt32;
using OSType = System.UInt32;
// typedef double NSTimeInterval;
using NSTimeInterval = System.Double;

#if MONOMAC
// dummy usings to make code compile without having the actual types available (for [NoMac] to work)
using NSDirectionalEdgeInsets = Foundation.NSObject;
using UIEdgeInsets = Foundation.NSObject;
using UIOffset = Foundation.NSObject;
using UIPreferredPresentationStyle = Foundation.NSObject;
#else
using NSBindingOptions = Foundation.NSObject;
using NSPasteboard = Foundation.NSObject;
using NSWorkspaceAuthorization = Foundation.NSObject;

using NSStringAttributes = UIKit.UIStringAttributes;
#endif

#if IOS && !__MACCATALYST__
using NSAppleEventSendOptions = Foundation.NSObject;
using NSBezierPath = Foundation.NSObject;
using NSImage = Foundation.NSObject;
#endif

#if TVOS
using NSAppleEventSendOptions = Foundation.NSObject;
using NSBezierPath = Foundation.NSObject;
using NSImage = Foundation.NSObject;
#endif

using CoreImage;

#if !IOS
using APActivationPayload = Foundation.NSObject;
#endif

#if __MACCATALYST__
using NSAppleEventSendOptions = Foundation.NSObject;
using NSBezierPath = Foundation.NSObject;
using NSImage = Foundation.NSObject;
#endif

#if IOS || TVOS
using NSAppearance = UIKit.UIAppearance;
using NSColor = UIKit.UIColor;
using NSNotificationSuspensionBehavior = Foundation.NSObject;
using NSNotificationFlags = Foundation.NSObject;
using NSTextBlock = Foundation.NSObject;
using NSTextTable = Foundation.NSString; // Different frmo NSTextBlock, because some methods overload on these two types.
#endif


#else
using CoreFoundation;
using CoreGraphics;
using Security;
using UIKit;
using System.ComponentModel;
#endif

namespace Foundation {
	/// <param name="obj1">To be added.</param>
	/// <param name="obj2">To be added.</param>
	/// <summary>A delegate that defines the comparison function to be used with functions such as <see cref="Foundation.NSArray.Sort(Foundation.NSComparator)" />.</summary>
	/// <returns>To be added.</returns>
	/// <remarks>To be added.</remarks>
	delegate NSComparisonResult NSComparator (NSObject obj1, NSObject obj2);

	/// <param name="date">To be added.</param>
	/// <param name="exactMatch">To be added.</param>
	/// <param name="stop">To be added.</param>
	/// <summary>Completion handler for the <see cref="Foundation.NSCalendar.EnumerateDatesStartingAfterDate(Foundation.NSDate,Foundation.NSDateComponents,Foundation.NSCalendarOptions,Foundation.EnumerateDatesCallback)" /> method.</summary>
	/// <remarks>To be added.</remarks>
	delegate void EnumerateDatesCallback (NSDate date, bool exactMatch, ref bool stop);

	/// <param name="idx">To be added.</param>
	/// <param name="stop">To be added.</param>
	/// <summary>Defines the enumerator callback in calls to <see cref="Foundation.NSIndexSet.EnumerateIndexes(Foundation.NSRange,Foundation.NSEnumerationOptions,Foundation.EnumerateIndexSetCallback)" />.</summary>
	/// <remarks>To be added.</remarks>
	delegate void EnumerateIndexSetCallback (nuint idx, ref bool stop);


	[BaseType (typeof (NSObject))]
	interface NSAutoreleasePool {
	}


	interface NSArray<TValue> : NSArray { }


	[BaseType (typeof (NSObject))]
	[DesignatedDefaultCtor]
	interface NSArray : NSSecureCoding, NSMutableCopying, INSFastEnumeration
#if !WATCHOS
		, CKRecordValue
#endif
	{
		[Export ("count")]
		nuint Count { get; }

		/// <param name="idx">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("objectAtIndex:")]
		NativeHandle ValueAt (nuint idx);

		[Static]
		[Internal]
		[Export ("arrayWithObjects:count:")]
		IntPtr FromObjects (IntPtr array, nint count);

		[Export ("valueForKey:")]
		[MarshalNativeExceptions]
		NSObject ValueForKey (NSString key);

		[Export ("setValue:forKey:")]
		void SetValueForKey (NSObject value, NSString key);

		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'Write (NSUrl, out NSError)' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'Write (NSUrl, out NSError)' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'Write (NSUrl, out NSError)' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'Write (NSUrl, out NSError)' instead.")]
		[Export ("writeToFile:atomically:")]
		bool WriteToFile (string path, bool useAuxiliaryFile);

		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'NSMutableArray.FromFile' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'NSMutableArray.FromFile' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'NSMutableArray.FromFile' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'NSMutableArray.FromFile' instead.")]
		[Export ("arrayWithContentsOfFile:")]
		[Static]
		NSArray FromFile (string path);

		[Export ("sortedArrayUsingComparator:")]
		NSArray Sort (NSComparator cmptr);

		[Export ("filteredArrayUsingPredicate:")]
		NSArray Filter (NSPredicate predicate);

		[Internal]
		[Sealed]
		[Export ("containsObject:")]
		bool _Contains (NativeHandle anObject);

		[Export ("containsObject:")]
		bool Contains (NSObject anObject);

		[Internal]
		[Sealed]
		[Export ("indexOfObject:")]
		nuint _IndexOf (NativeHandle anObject);

		[Export ("indexOfObject:")]
		nuint IndexOf (NSObject anObject);

		[Export ("addObserver:toObjectsAtIndexes:forKeyPath:options:context:")]
		void AddObserver (NSObject observer, NSIndexSet indexes, string keyPath, NSKeyValueObservingOptions options, IntPtr context);

		[Export ("removeObserver:fromObjectsAtIndexes:forKeyPath:context:")]
		void RemoveObserver (NSObject observer, NSIndexSet indexes, string keyPath, IntPtr context);

		[Export ("removeObserver:fromObjectsAtIndexes:forKeyPath:")]
		void RemoveObserver (NSObject observer, NSIndexSet indexes, string keyPath);

		[MacCatalyst (13, 1)]
		[Export ("writeToURL:error:")]
		bool Write (NSUrl url, out NSError error);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("arrayWithContentsOfURL:error:")]
		[return: NullAllowed]
		NSArray FromUrl (NSUrl url, out NSError error);

#if false // https://github.com/dotnet/macios/issues/15577
		[Internal]
		[Export ("differenceFromArray:withOptions:")]
		NativeHandle _GetDifference (NSArray other, NSOrderedCollectionDifferenceCalculationOptions options);

		[Wrap ("Runtime.GetNSObject <NSOrderedCollectionDifference> (_GetDifference (NSArray.FromNSObjects (other), options))")]
		[return: NullAllowed]
		NSOrderedCollectionDifference GetDifference (NSObject[] other, NSOrderedCollectionDifferenceCalculationOptions options);

		[Internal]
		[Export ("differenceFromArray:")]
		NativeHandle _GetDifference (NSArray other);

		[Wrap ("Runtime.GetNSObject <NSOrderedCollectionDifference> (_GetDifference(NSArray.FromNSObjects (other)))")]
		[return: NullAllowed]
		NSOrderedCollectionDifference GetDifference (NSObject[] other);

		[Export ("arrayByApplyingDifference:")]
		[return: NullAllowed]
		NativeHandle _GetArrayByApplyingDifference (NSOrderedCollectionDifference difference);

		[Wrap ("NSArray.ArrayFromHandle<NSObject> (_GetArrayByApplyingDifference (difference))")]
		[return: NullAllowed]
		NSObject[] GetArrayByApplyingDifference (NSOrderedCollectionDifference difference);

		[Internal]
		[Export ("differenceFromArray:withOptions:usingEquivalenceTest:")]
		NativeHandle _GetDifferenceFromArray (NSArray other, NSOrderedCollectionDifferenceCalculationOptions options, /* Func<NSObject, NSObject, bool> block */ ref BlockLiteral block);
#endif
	}


	[BaseType (typeof (NSObject), Name = "NSCachedURLResponse")]
	// instance created with 'init' will crash when Dispose is called
	[DisableDefaultCtor]
	interface NSCachedUrlResponse : NSCoding, NSSecureCoding, NSCopying {
		[Export ("initWithResponse:data:userInfo:storagePolicy:")]
		NativeHandle Constructor (NSUrlResponse response, NSData data, [NullAllowed] NSDictionary userInfo, NSUrlCacheStoragePolicy storagePolicy);

		[Export ("initWithResponse:data:")]
		NativeHandle Constructor (NSUrlResponse response, NSData data);

		[Export ("response")]
		NSUrlResponse Response { get; }

		[Export ("data")]
		NSData Data { get; }

		[Export ("userInfo")]
		NSDictionary UserInfo { get; }

		[Export ("storagePolicy")]
		NSUrlCacheStoragePolicy StoragePolicy { get; }
	}


	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface NSCalendar : NSSecureCoding, NSCopying {
		[DesignatedInitializer]
		[Export ("initWithCalendarIdentifier:")]
		NativeHandle Constructor (NSString identifier);

		[Wrap ("this (calendarType.GetConstant ()!)")]
		NativeHandle Constructor (NSCalendarType calendarType);

		[Export ("calendarIdentifier")]
		string Identifier { get; }

		[Export ("currentCalendar")]
		[Static]
		NSCalendar CurrentCalendar { get; }

		[Export ("locale", ArgumentSemantic.Copy)]
		NSLocale Locale { get; set; }

		[Export ("timeZone", ArgumentSemantic.Copy)]
		NSTimeZone TimeZone { get; set; }

		[Export ("firstWeekday")]
		nuint FirstWeekDay { get; set; }

		[Export ("minimumDaysInFirstWeek")]
		nuint MinimumDaysInFirstWeek { get; set; }

		[Export ("components:fromDate:")]
		NSDateComponents Components (NSCalendarUnit unitFlags, NSDate fromDate);

		[Export ("components:fromDate:toDate:options:")]
		NSDateComponents Components (NSCalendarUnit unitFlags, NSDate fromDate, NSDate toDate, NSCalendarOptions opts);

		[Export ("dateByAddingComponents:toDate:options:")]
		NSDate DateByAddingComponents (NSDateComponents comps, NSDate date, NSCalendarOptions opts);

		[Export ("dateFromComponents:")]
		NSDate DateFromComponents (NSDateComponents comps);

		[Export ("eraSymbols")]
		string [] EraSymbols { get; }

		[Export ("longEraSymbols")]
		string [] LongEraSymbols { get; }

		[Export ("monthSymbols")]
		string [] MonthSymbols { get; }

		[Export ("shortMonthSymbols")]
		string [] ShortMonthSymbols { get; }

		[Export ("veryShortMonthSymbols")]
		string [] VeryShortMonthSymbols { get; }

		[Export ("standaloneMonthSymbols")]
		string [] StandaloneMonthSymbols { get; }

		[Export ("shortStandaloneMonthSymbols")]
		string [] ShortStandaloneMonthSymbols { get; }

		[Export ("veryShortStandaloneMonthSymbols")]
		string [] VeryShortStandaloneMonthSymbols { get; }

		[Export ("weekdaySymbols")]
		string [] WeekdaySymbols { get; }

		[Export ("shortWeekdaySymbols")]
		string [] ShortWeekdaySymbols { get; }

		[Export ("veryShortWeekdaySymbols")]
		string [] VeryShortWeekdaySymbols { get; }

		[Export ("standaloneWeekdaySymbols")]
		string [] StandaloneWeekdaySymbols { get; }

		[Export ("shortStandaloneWeekdaySymbols")]
		string [] ShortStandaloneWeekdaySymbols { get; }

		[Export ("veryShortStandaloneWeekdaySymbols")]
		string [] VeryShortStandaloneWeekdaySymbols { get; }

		[Export ("quarterSymbols")]
		string [] QuarterSymbols { get; }

		[Export ("shortQuarterSymbols")]
		string [] ShortQuarterSymbols { get; }

		[Export ("standaloneQuarterSymbols")]
		string [] StandaloneQuarterSymbols { get; }

		[Export ("shortStandaloneQuarterSymbols")]
		string [] ShortStandaloneQuarterSymbols { get; }

		[Export ("AMSymbol")]
		string AMSymbol { get; }

		[Export ("PMSymbol")]
		string PMSymbol { get; }

		[Export ("compareDate:toDate:toUnitGranularity:")]
		[MacCatalyst (13, 1)]
		NSComparisonResult CompareDate (NSDate date1, NSDate date2, NSCalendarUnit granularity);

		[Export ("component:fromDate:")]
		[MacCatalyst (13, 1)]
		nint GetComponentFromDate (NSCalendarUnit unit, NSDate date);

		[Export ("components:fromDateComponents:toDateComponents:options:")]
		[MacCatalyst (13, 1)]
		NSDateComponents ComponentsFromDateToDate (NSCalendarUnit unitFlags, NSDateComponents startingDate, NSDateComponents resultDate, NSCalendarOptions options);

		[Export ("componentsInTimeZone:fromDate:")]
		[MacCatalyst (13, 1)]
		NSDateComponents ComponentsInTimeZone (NSTimeZone timezone, NSDate date);

		[Export ("date:matchesComponents:")]
		[MacCatalyst (13, 1)]
		bool Matches (NSDate date, NSDateComponents components);

		/// <param name="unit">To be added.</param>
		/// <param name="value">To be added.</param>
		/// <param name="date">To be added.</param>
		/// <param name="options">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("dateByAddingUnit:value:toDate:options:")]
		[MacCatalyst (13, 1)]
		NSDate DateByAddingUnit (NSCalendarUnit unit, nint value, NSDate date, NSCalendarOptions options);

		/// <param name="hour">To be added.</param>
		/// <param name="minute">To be added.</param>
		/// <param name="second">To be added.</param>
		/// <param name="date">To be added.</param>
		/// <param name="options">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("dateBySettingHour:minute:second:ofDate:options:")]
		[MacCatalyst (13, 1)]
		NSDate DateBySettingsHour (nint hour, nint minute, nint second, NSDate date, NSCalendarOptions options);

		/// <param name="unit">To be added.</param>
		/// <param name="value">To be added.</param>
		/// <param name="date">To be added.</param>
		/// <param name="options">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("dateBySettingUnit:value:ofDate:options:")]
		[MacCatalyst (13, 1)]
		NSDate DateBySettingUnit (NSCalendarUnit unit, nint value, NSDate date, NSCalendarOptions options);

		/// <param name="era">To be added.</param>
		/// <param name="year">To be added.</param>
		/// <param name="month">To be added.</param>
		/// <param name="date">To be added.</param>
		/// <param name="hour">To be added.</param>
		/// <param name="minute">To be added.</param>
		/// <param name="second">To be added.</param>
		/// <param name="nanosecond">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("dateWithEra:year:month:day:hour:minute:second:nanosecond:")]
		[MacCatalyst (13, 1)]
		NSDate Date (nint era, nint year, nint month, nint date, nint hour, nint minute, nint second, nint nanosecond);

		/// <param name="era">To be added.</param>
		/// <param name="year">To be added.</param>
		/// <param name="week">To be added.</param>
		/// <param name="weekday">To be added.</param>
		/// <param name="hour">To be added.</param>
		/// <param name="minute">To be added.</param>
		/// <param name="second">To be added.</param>
		/// <param name="nanosecond">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("dateWithEra:yearForWeekOfYear:weekOfYear:weekday:hour:minute:second:nanosecond:")]
		[MacCatalyst (13, 1)]
		NSDate DateForWeekOfYear (nint era, nint year, nint week, nint weekday, nint hour, nint minute, nint second, nint nanosecond);

		[Export ("enumerateDatesStartingAfterDate:matchingComponents:options:usingBlock:")]
		[MacCatalyst (13, 1)]
		void EnumerateDatesStartingAfterDate (NSDate start, NSDateComponents matchingComponents, NSCalendarOptions options, [BlockCallback] EnumerateDatesCallback callback);

		/// <param name="era">To be added.</param>
		/// <param name="year">To be added.</param>
		/// <param name="month">To be added.</param>
		/// <param name="day">To be added.</param>
		/// <param name="date">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("getEra:year:month:day:fromDate:")]
		[MacCatalyst (13, 1)]
		void GetComponentsFromDate (out nint era, out nint year, out nint month, out nint day, NSDate date);

		/// <param name="era">To be added.</param>
		/// <param name="year">To be added.</param>
		/// <param name="weekOfYear">To be added.</param>
		/// <param name="weekday">To be added.</param>
		/// <param name="date">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("getEra:yearForWeekOfYear:weekOfYear:weekday:fromDate:")]
		[MacCatalyst (13, 1)]
		void GetComponentsFromDateForWeekOfYear (out nint era, out nint year, out nint weekOfYear, out nint weekday, NSDate date);

		/// <param name="hour">To be added.</param>
		/// <param name="minute">To be added.</param>
		/// <param name="second">To be added.</param>
		/// <param name="nanosecond">To be added.</param>
		/// <param name="date">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("getHour:minute:second:nanosecond:fromDate:")]
		[MacCatalyst (13, 1)]
		void GetHourComponentsFromDate (out nint hour, out nint minute, out nint second, out nint nanosecond, NSDate date);

		[Export ("isDate:equalToDate:toUnitGranularity:")]
		[MacCatalyst (13, 1)]
		bool IsEqualToUnitGranularity (NSDate date1, NSDate date2, NSCalendarUnit unit);

		[Export ("isDate:inSameDayAsDate:")]
		[MacCatalyst (13, 1)]
		bool IsInSameDay (NSDate date1, NSDate date2);

		[Export ("isDateInToday:")]
		[MacCatalyst (13, 1)]
		bool IsDateInToday (NSDate date);

		[Export ("isDateInTomorrow:")]
		[MacCatalyst (13, 1)]
		bool IsDateInTomorrow (NSDate date);

		[Export ("isDateInWeekend:")]
		[MacCatalyst (13, 1)]
		bool IsDateInWeekend (NSDate date);

		[Export ("isDateInYesterday:")]
		[MacCatalyst (13, 1)]
		bool IsDateInYesterday (NSDate date);

		[Export ("nextDateAfterDate:matchingComponents:options:")]
		[MacCatalyst (13, 1)]
		[MarshalNativeExceptions]
		[return: NullAllowed]
		NSDate FindNextDateAfterDateMatching (NSDate date, NSDateComponents components, NSCalendarOptions options);

		/// <param name="date">To be added.</param>
		/// <param name="hour">To be added.</param>
		/// <param name="minute">To be added.</param>
		/// <param name="second">To be added.</param>
		/// <param name="options">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("nextDateAfterDate:matchingHour:minute:second:options:")]
		[MacCatalyst (13, 1)]
		[MarshalNativeExceptions]
		[return: NullAllowed]
		NSDate FindNextDateAfterDateMatching (NSDate date, nint hour, nint minute, nint second, NSCalendarOptions options);

		/// <param name="date">To be added.</param>
		/// <param name="unit">To be added.</param>
		/// <param name="value">To be added.</param>
		/// <param name="options">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("nextDateAfterDate:matchingUnit:value:options:")]
		[MacCatalyst (13, 1)]
		[MarshalNativeExceptions]
		[return: NullAllowed]
		NSDate FindNextDateAfterDateMatching (NSDate date, NSCalendarUnit unit, nint value, NSCalendarOptions options);

		[Export ("nextWeekendStartDate:interval:options:afterDate:")]
		[MacCatalyst (13, 1)]
		bool FindNextWeekend (out NSDate date, out double /* NSTimeInterval */ interval, NSCalendarOptions options, NSDate afterDate);

		[Export ("rangeOfWeekendStartDate:interval:containingDate:")]
		[MacCatalyst (13, 1)]
		bool RangeOfWeekendContainingDate (out NSDate weekendStartDate, out double /* NSTimeInterval */ interval, NSDate date);

		// although the ideal would be to use GetRange, we already have the method
		// RangeOfWeekendContainingDate and for the sake of consistency we are
		// going to use the same name pattern.
		[Export ("minimumRangeOfUnit:")]
		NSRange MinimumRange (NSCalendarUnit unit);

		[Export ("maximumRangeOfUnit:")]
		NSRange MaximumRange (NSCalendarUnit unit);

		[Export ("rangeOfUnit:inUnit:forDate:")]
		NSRange Range (NSCalendarUnit smaller, NSCalendarUnit larger, NSDate date);

		[Export ("ordinalityOfUnit:inUnit:forDate:")]
		nuint Ordinality (NSCalendarUnit smaller, NSCalendarUnit larger, NSDate date);

		[Export ("rangeOfUnit:startDate:interval:forDate:")]
		bool Range (NSCalendarUnit unit, [NullAllowed] out NSDate datep, out double /* NSTimeInterval */ interval, NSDate date);

		[Export ("startOfDayForDate:")]
		[MacCatalyst (13, 1)]
		NSDate StartOfDayForDate (NSDate date);

		[MacCatalyst (13, 1)]
		[Notification]
		[Field ("NSCalendarDayChangedNotification")]
		NSString DayChangedNotification { get; }
	}


	/// <summary>Calendar types that can be used with the NSCalendar constructor.</summary>
	enum NSCalendarType {
		/// <summary>Gregorian calendar.</summary>
		[Field ("NSCalendarIdentifierGregorian")]
		Gregorian = 0,

		/// <summary>Buddhist calendar.</summary>
		[Field ("NSCalendarIdentifierBuddhist")]
		Buddhist = 1,

		/// <summary>Chinese calendar.</summary>
		[Field ("NSCalendarIdentifierChinese")]
		Chinese = 2,

		/// <summary>Hebrew calendar.</summary>
		[Field ("NSCalendarIdentifierHebrew")]
		Hebrew = 3,

		/// <summary>Islamic calendar.</summary>
		[Field ("NSIslamicCalendar")]
		Islamic = 4,

		/// <summary>IslamicCivil calendar.</summary>
		[Field ("NSCalendarIdentifierIslamicCivil")]
		IslamicCivil = 5,

		/// <summary>Japanese calendar.</summary>
		[Field ("NSCalendarIdentifierJapanese")]
		Japanese = 6,

#if !XAMCORE_5_0
		/// <summary>Calendar for Taiwan.</summary>
		[EditorBrowsable (EditorBrowsableState.Never)]
		[Obsolete ("Use 'RepublicOfChina' instead.")]
		Taiwan = 7,
#endif

		/// <summary>Calendar for the Republic of China.</summary>
		[Field ("NSCalendarIdentifierRepublicOfChina")]
		RepublicOfChina = 7,

		/// <summary>Persian calendar.</summary>
		[Field ("NSCalendarIdentifierPersian")]
		Persian = 8,

		/// <summary>Indian calendar.</summary>
		[Field ("NSCalendarIdentifierIndian")]
		Indian = 9,

		/// <summary>ISO8601 calendar.</summary>
		[Field ("NSCalendarIdentifierISO8601")]
		ISO8601 = 10,

		/// <summary>Coptic calendar.</summary>
		[Field ("NSCalendarIdentifierCoptic")]
		Coptic = 11,

		/// <summary>Ethiopic (Amete Alem) calendar.</summary>
		[Field ("NSCalendarIdentifierEthiopicAmeteAlem")]
		EthiopicAmeteAlem = 12,

		/// <summary>Ethiopic (Amete Mihret) calendar.</summary>
		[Field ("NSCalendarIdentifierEthiopicAmeteMihret")]
		EthiopicAmeteMihret = 13,

		/// <summary>Tabular Islamic calendar.</summary>
		[MacCatalyst (13, 1)]
		[Field ("NSCalendarIdentifierIslamicTabular")]
		IslamicTabular = 14,

		/// <summary>Islamic Umm al-Qura calendar.</summary>
		[MacCatalyst (13, 1)]
		[Field ("NSCalendarIdentifierIslamicUmmAlQura")]
		IslamicUmmAlQura = 15,

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSCalendarIdentifierBangla")]
		Bangla,

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSCalendarIdentifierGujarati")]
		Gujarati,

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSCalendarIdentifierKannada")]
		Kannada,

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSCalendarIdentifierMalayalam")]
		Malayalam,

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSCalendarIdentifierMarathi")]
		Marathi,

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSCalendarIdentifierOdia")]
		Odia,

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSCalendarIdentifierTamil")]
		Tamil,

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSCalendarIdentifierTelugu")]
		Telugu,

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSCalendarIdentifierVikram")]
		Vikram,

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSCalendarIdentifierDangi")]
		Dangi,

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSCalendarIdentifierVietnamese")]
		Vietnamese,
	}


	[BaseType (typeof (NSObject))]
	interface NSCharacterSet : NSSecureCoding, NSMutableCopying {
		[Static, Export ("alphanumericCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet Alphanumerics { get; }

		[Static, Export ("capitalizedLetterCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet Capitalized { get; }

		// TODO/FIXME: constructor?
		[Static, Export ("characterSetWithBitmapRepresentation:")]
		NSCharacterSet FromBitmap (NSData data);

		// TODO/FIXME: constructor?
		[Static, Export ("characterSetWithCharactersInString:")]
		NSCharacterSet FromString (string aString);

		[return: NullAllowed]
		[Static, Export ("characterSetWithContentsOfFile:")]
		NSCharacterSet FromFile (string path);

		[Static, Export ("characterSetWithRange:")]
		NSCharacterSet FromRange (NSRange aRange);

		[Static, Export ("controlCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet Controls { get; }

		[Static, Export ("decimalDigitCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet DecimalDigits { get; }

		[Static, Export ("decomposableCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet Decomposables { get; }

		[Static, Export ("illegalCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet Illegals { get; }

		[Static, Export ("letterCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet Letters { get; }

		[Static, Export ("lowercaseLetterCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet LowercaseLetters { get; }

		[Static, Export ("newlineCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet Newlines { get; }

		[Static, Export ("nonBaseCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet Marks { get; }

		[Static, Export ("punctuationCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet Punctuation { get; }

		[Static, Export ("symbolCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet Symbols { get; }

		[Static, Export ("uppercaseLetterCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UppercaseLetters { get; }

		[Static, Export ("whitespaceAndNewlineCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet WhitespaceAndNewlines { get; }

		[Static, Export ("whitespaceCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet Whitespaces { get; }

		[Export ("bitmapRepresentation")]
		NSData GetBitmapRepresentation ();

		[Export ("characterIsMember:")]
		bool Contains (char aCharacter);

		[Export ("hasMemberInPlane:")]
		bool HasMemberInPlane (byte thePlane);

		[Export ("invertedSet")]
		NSCharacterSet InvertedSet { get; }

		[Export ("isSupersetOfSet:")]
		bool IsSupersetOf (NSCharacterSet theOtherSet);

		[Export ("longCharacterIsMember:")]
		bool Contains (uint /* UTF32Char = UInt32 */ theLongChar);

		[Static]
		[Export ("URLFragmentAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlFragmentAllowed { get; }

		[Static]
		[Export ("URLHostAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlHostAllowed { get; }

		[Static]
		[Export ("URLPasswordAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlPasswordAllowed { get; }

		[Static]
		[Export ("URLPathAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlPathAllowed { get; }

		[Static]
		[Export ("URLQueryAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlQueryAllowed { get; }

		[Static]
		[Export ("URLUserAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlUserAllowed { get; }
	}


	[BaseType (typeof (NSObject))]
	interface NSCoder {

		//
		// Encoding and decoding
		//
		[Export ("encodeObject:")]
		void Encode ([NullAllowed] NSObject obj);

		[Export ("encodeRootObject:")]
		void EncodeRoot (NSObject obj);

		[return: NullAllowed]
		[Export ("decodeObject")]
		NSObject DecodeObject ();

		[Export ("decodeValueOfObjCType:at:size:")]
		void DecodeValue (IntPtr objCTypeCode, IntPtr data, nuint size);

		//
		// Encoding and decoding with keys
		//
		[Export ("encodeConditionalObject:forKey:")]
		void EncodeConditionalObject ([NullAllowed] NSObject val, string key);

		[Export ("encodeConditionalObject:")]
		void EncodeConditionalObject ([NullAllowed] NSObject value);

		[Export ("encodeObject:forKey:")]
		void Encode ([NullAllowed] NSObject val, string key);

		[Export ("encodeBool:forKey:")]
		void Encode (bool val, string key);

		[Export ("encodeDouble:forKey:")]
		void Encode (double val, string key);

		[Export ("encodeFloat:forKey:")]
		void Encode (float /* float, not CGFloat */ val, string key);

		[Export ("encodeInt32:forKey:")]
		void Encode (int /* int32 */ val, string key);

		[Export ("encodeInt64:forKey:")]
		void Encode (long val, string key);

		/// <param name="val">Native integer value to encode.</param>
		/// <param name="key">Key to associate with the object being encoded.</param>
		/// <summary>Encodes the platform-specific native integer (32 or 64 bits) using the specified associated key.</summary>
		/// <remarks>
		///         </remarks>
		[Export ("encodeInteger:forKey:")]
		void Encode (nint val, string key);

		[Export ("encodeBytes:length:forKey:")]
		void EncodeBlock (IntPtr bytes, nint length, string key);

		[Export ("encodeBytes:length:")]
		void Encode (IntPtr bytes, nint length);

		[Export ("containsValueForKey:")]
		bool ContainsKey (string key);

		[Export ("decodeBoolForKey:")]
		bool DecodeBool (string key);

		[Export ("decodeDoubleForKey:")]
		double DecodeDouble (string key);

		[Export ("decodeFloatForKey:")]
		float DecodeFloat (string key); /* float, not CGFloat */

		[Export ("decodeInt32ForKey:")]
		int DecodeInt (string key); /* int, not NSInteger */

		[Export ("decodeInt64ForKey:")]
		long DecodeLong (string key);

		[Export ("decodeIntegerForKey:")]
		nint DecodeNInt (string key);

		[return: NullAllowed]
		[Export ("decodeObjectForKey:")]
		NSObject DecodeObject (string key);

		/// <param name="key">The key identifying the item to decode.</param>
		/// <param name="length">Number of bytes in the returned block.</param>
		/// <summary>Low-level: decodes the item with the associated key into a memory block,
		/// 	and returns a pointer to it.</summary>
		/// <returns>Pointer to the block of memory that contains at least
		/// 	the number of bytes set on the length parameter.</returns>
		/// <remarks>
		///         </remarks>
		[Export ("decodeBytesForKey:returnedLength:")]
		IntPtr DecodeBytes (string key, out nuint length);

		/// <param name="length">Number of bytes in the returned block.</param>
		/// <summary>Low-level: decodes the next item into a memory block,
		/// 	and returns a pointer to it.</summary>
		/// <returns>Pointer to the block of memory that contains at least
		/// 	the number of bytes set on the length parameter.</returns>
		/// <remarks>
		///         </remarks>
		[Export ("decodeBytesWithReturnedLength:")]
		IntPtr DecodeBytes (out nuint length);

		[NullAllowed]
		[Export ("allowedClasses")]
		NSSet AllowedClasses { get; }

		[Export ("requiresSecureCoding")]
		bool RequiresSecureCoding ();

		[return: NullAllowed]
		[MacCatalyst (13, 1)]
		[Export ("decodeTopLevelObjectAndReturnError:")]
		NSObject DecodeTopLevelObject ([NullAllowed] out NSError error);

		[return: NullAllowed]
		[MacCatalyst (13, 1)]
		[Export ("decodeTopLevelObjectForKey:error:")]
		NSObject DecodeTopLevelObject (string key, [NullAllowed] out NSError error);

		[return: NullAllowed]
		[MacCatalyst (13, 1)]
		[Export ("decodeTopLevelObjectOfClass:forKey:error:")]
		NSObject DecodeTopLevelObject (Class klass, string key, [NullAllowed] out NSError error);

		[return: NullAllowed]
		[MacCatalyst (13, 1)]
		[Export ("decodeTopLevelObjectOfClasses:forKey:error:")]
		NSObject DecodeTopLevelObject ([NullAllowed] NSSet<Class> setOfClasses, string key, [NullAllowed] out NSError error);

		[MacCatalyst (13, 1)]
		[Export ("failWithError:")]
		void Fail (NSError error);

		[Export ("systemVersion")]
		uint SystemVersion { get; }

		[MacCatalyst (13, 1)]
		[Export ("decodingFailurePolicy")]
		NSDecodingFailurePolicy DecodingFailurePolicy { get; }

		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("error", ArgumentSemantic.Copy)]
		NSError Error { get; }

		[TV (14, 0), iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Export ("decodeArrayOfObjectsOfClass:forKey:")]
		[return: NullAllowed]
		NSObject [] DecodeArrayOfObjects (Class @class, string key);

		[TV (14, 0), iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Export ("decodeArrayOfObjectsOfClasses:forKey:")]
		[return: NullAllowed]
		NSObject [] DecodeArrayOfObjects (NSSet<Class> classes, string key);

		[TV (14, 0), iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Export ("decodeDictionaryWithKeysOfClass:objectsOfClass:forKey:")]
		[return: NullAllowed]
		NSDictionary DecodeDictionary (Class keyClass, Class objectClass, string key);

		[TV (14, 0), iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Export ("decodeDictionaryWithKeysOfClasses:objectsOfClasses:forKey:")]
		[return: NullAllowed]
		NSDictionary DecodeDictionary (NSSet<Class> keyClasses, NSSet<Class> objectClasses, string key);

		[TV (18, 4), iOS (18, 4), MacCatalyst (18, 4), Mac (15, 4)]
		[Export ("decodeBytesWithMinimumLength:")]
		IntPtr DecodeBytes (nuint minimumLength);

		[TV (18, 4), iOS (18, 4), MacCatalyst (18, 4), Mac (15, 4)]
		[Export ("decodeBytesForKey:minimumLength:")]
		IntPtr DecodeBytes (string key, nuint minimumLength);

		[Export ("allowsKeyedCoding")]
		bool AllowsKeyedCoding { get; }

		[NoiOS, NoTV, NoMacCatalyst]
		[Export ("encodePropertyList:")]
		void EncodePropertyList (NSObject aPropertyList);

		[NoiOS, NoTV, NoMacCatalyst]
		[return: NullAllowed]
		[Export ("decodePropertyList")]
		NSObject DecodePropertyList ();

		[Export ("decodePropertyListForKey:")]
		[return: NullAllowed]
		NSObject DecodePropertyList (string key);

		[Export ("decodeObjectOfClass:forKey:")]
		[return: NullAllowed]
		NSObject DecodeObject (Class @class, string key);

		[Wrap ("DecodeObject (new Class (type), key)")]
		[return: NullAllowed]
		NSObject DecodeObject (Type type, string key);

		[Export ("decodeObjectOfClasses:forKey:")]
		[return: NullAllowed]
		NSObject DecodeObject ([NullAllowed] NSSet<Class> classes, string key);

		[Wrap ("DecodeObject (new NSSet<Class> (Array.ConvertAll (types, t => new Class (t))), key)")]
		[return: NullAllowed]
		NSObject DecodeObject (Type [] types, string key);

		[Export ("encodeBycopyObject:")]
		void EncodeBycopyObject ([NullAllowed] NSObject anObject);

		[Export ("encodeByrefObject:")]
		void EncodeByrefObject ([NullAllowed] NSObject anObject);

		[Export ("encodeDataObject:")]
		void Encode (NSData data);

		[return: NullAllowed]
		[Export ("decodeDataObject")]
		NSData DecodeDataObject ();

		// from the NSGeometryCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("encodePoint:")]
		void Encode (CGPoint point);

		// from the NSGeometryCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("decodePoint")]
		CGPoint DecodePoint ();

		// from the NSGeometryCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("encodeSize:")]
		void Encode (CGSize size);

		// from the NSGeometryCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("decodeSize")]
		CGSize DecodeSize ();

		// from the NSGeometryCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("encodeRect:")]
		void Encode (CGRect rect);

		// from the NSGeometryCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("decodeRect")]
		CGRect DecodeRect ();

		// from the NSGeometryKeyedCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("encodePoint:forKey:")]
		void Encode (CGPoint point, string key);

		// from the NSGeometryKeyedCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("encodeSize:forKey:")]
		void Encode (CGSize size, string key);

		// from the NSGeometryKeyedCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("encodeRect:forKey:")]
		void Encode (CGRect rect, string key);

		// from the NSGeometryKeyedCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("decodePointForKey:")]
		CGPoint DecodePoint (string key);

		// from the NSGeometryKeyedCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("decodeSizeForKey:")]
		CGSize DecodeSize (string key);

		// from the NSGeometryKeyedCoding (NSCoder) category
		[NoiOS, NoTV, MacCatalyst (13, 1)]
		[Export ("decodeRectForKey:")]
		CGRect DecodeRect (string key);
	}


	/// <param name="bytes">To be added.</param>
	/// <param name="range">To be added.</param>
	/// <param name="stop">To be added.</param>
	/// <summary>The delegate used to enumerate in calls to <see cref="Foundation.NSData.EnumerateByteRange(Foundation.NSDataByteRangeEnumerator)" />.</summary>
	/// <remarks>To be added.</remarks>
	delegate void NSDataByteRangeEnumerator (IntPtr bytes, NSRange range, ref bool stop);


	[BaseType (typeof (NSObject))]
	interface NSData : NSSecureCoding, NSMutableCopying
#if !WATCHOS
		, CKRecordValue
#endif
	{
		[Export ("dataWithContentsOfURL:")]
		[Static]
		NSData FromUrl (NSUrl url);

		[Export ("dataWithContentsOfURL:options:error:")]
		[Static]
		NSData FromUrl (NSUrl url, NSDataReadingOptions mask, out NSError error);

		[Export ("dataWithContentsOfFile:")]
		[Static]
		NSData FromFile (string path);

		[Export ("dataWithContentsOfFile:options:error:")]
		[Static]
		NSData FromFile (string path, NSDataReadingOptions mask, out NSError error);

		[Export ("dataWithData:")]
		[Static]
		NSData FromData (NSData source);

		[Export ("dataWithBytes:length:"), Static]
		NSData FromBytes (IntPtr bytes, nuint size);

		[Export ("dataWithBytesNoCopy:length:"), Static]
		NSData FromBytesNoCopy (IntPtr bytes, nuint size);

		[Export ("dataWithBytesNoCopy:length:freeWhenDone:"), Static]
		NSData FromBytesNoCopy (IntPtr bytes, nuint size, bool freeWhenDone);

		[Export ("bytes")]
		IntPtr Bytes { get; }

		[Export ("length")]
		nuint Length { get; [NotImplemented ("Not available on NSData, only available on NSMutableData")] set; }

		[Export ("writeToFile:options:error:")]
		[Internal]
		bool _Save (string file, nint options, IntPtr addr);

		[Export ("writeToURL:options:error:")]
		[Internal]
		bool _Save (NSUrl url, nint options, IntPtr addr);

		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'Save (NSUrl,bool)' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'Save (NSUrl,bool)' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'Save (NSUrl,bool)' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'Save (NSUrl,bool)' instead.")]
		[Export ("writeToFile:atomically:")]
		bool Save (string path, bool atomically);

		[Export ("writeToURL:atomically:")]
		bool Save (NSUrl url, bool atomically);

		[Export ("subdataWithRange:")]
		NSData Subdata (NSRange range);

		[Export ("getBytes:length:")]
		void GetBytes (IntPtr buffer, nuint length);

		[Export ("getBytes:range:")]
		void GetBytes (IntPtr buffer, NSRange range);

		[Export ("rangeOfData:options:range:")]
		NSRange Find (NSData dataToFind, NSDataSearchOptions searchOptions, NSRange searchRange);

		[MacCatalyst (13, 1)]
		[Export ("initWithBase64EncodedString:options:")]
		NativeHandle Constructor (string base64String, NSDataBase64DecodingOptions options);

		[MacCatalyst (13, 1)]
		[Export ("initWithBase64EncodedData:options:")]
		NativeHandle Constructor (NSData base64Data, NSDataBase64DecodingOptions options);

		[MacCatalyst (13, 1)]
		[Export ("base64EncodedDataWithOptions:")]
		NSData GetBase64EncodedData (NSDataBase64EncodingOptions options);

		[MacCatalyst (13, 1)]
		[Export ("base64EncodedStringWithOptions:")]
		string GetBase64EncodedString (NSDataBase64EncodingOptions options);

		[MacCatalyst (13, 1)]
		[Export ("enumerateByteRangesUsingBlock:")]
		void EnumerateByteRange (NSDataByteRangeEnumerator enumerator);

		[MacCatalyst (13, 1)]
		[Export ("initWithBytesNoCopy:length:deallocator:")]
		NativeHandle Constructor (IntPtr bytes, nuint length, [NullAllowed] Action<IntPtr, nuint> deallocator);

		// NSDataCompression (NSData)

		[MacCatalyst (13, 1)]
		[Export ("decompressedDataUsingAlgorithm:error:")]
		[return: NullAllowed]
		NSData Decompress (NSDataCompressionAlgorithm algorithm, [NullAllowed] out NSError error);

		[MacCatalyst (13, 1)]
		[Export ("compressedDataUsingAlgorithm:error:")]
		[return: NullAllowed]
		NSData Compress (NSDataCompressionAlgorithm algorithm, [NullAllowed] out NSError error);
	}


	[BaseType (typeof (NSObject))]
	interface NSDateComponents : NSSecureCoding, NSCopying, INSCopying, INSSecureCoding, INativeObject {
		[NullAllowed] // by default this property is null
		[Export ("timeZone", ArgumentSemantic.Copy)]
		NSTimeZone TimeZone { get; set; }

		[NullAllowed] // by default this property is null
		[Export ("calendar", ArgumentSemantic.Copy)]
		NSCalendar Calendar { get; set; }

		[Export ("quarter")]
		nint Quarter { get; set; }

		[Export ("date")]
		NSDate Date { get; }

		//Detected properties
		[Export ("era")]
		nint Era { get; set; }

		[Export ("year")]
		nint Year { get; set; }

		[Export ("month")]
		nint Month { get; set; }

		[Export ("day")]
		nint Day { get; set; }

		[Export ("hour")]
		nint Hour { get; set; }

		[Export ("minute")]
		nint Minute { get; set; }

		[Export ("second")]
		nint Second { get; set; }

		[Export ("nanosecond")]
		nint Nanosecond { get; set; }

		[Export ("week")]
		[Deprecated (PlatformName.MacOSX, 10, 9, message: "Use 'WeekOfMonth' or 'WeekOfYear' instead.")]
		[Deprecated (PlatformName.iOS, 7, 0, message: "Use 'WeekOfMonth' or 'WeekOfYear' instead.")]
		[Deprecated (PlatformName.TvOS, 9, 0, message: "Use 'WeekOfMonth' or 'WeekOfYear' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'WeekOfMonth' or 'WeekOfYear' instead.")]
		nint Week { get; set; }

		[Export ("weekday")]
		nint Weekday { get; set; }

		[Export ("weekdayOrdinal")]
		nint WeekdayOrdinal { get; set; }

		[Export ("weekOfMonth")]
		nint WeekOfMonth { get; set; }

		[Export ("weekOfYear")]
		nint WeekOfYear { get; set; }

		[Export ("yearForWeekOfYear")]
		nint YearForWeekOfYear { get; set; }

		[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
		[Export ("dayOfYear")]
		nint DayOfYear { get; set; }

		/// <summary>Whether the month is a leap month.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("leapMonth")]
		bool IsLeapMonth { [Bind ("isLeapMonth")] get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isValidDate")]
		[MacCatalyst (13, 1)]
		bool IsValidDate { get; }

		[Export ("isValidDateInCalendar:")]
		[MacCatalyst (13, 1)]
		bool IsValidDateInCalendar (NSCalendar calendar);

		/// <param name="value">To be added.</param>
		/// <param name="unit">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("setValue:forComponent:")]
		[MacCatalyst (13, 1)]
		void SetValueForComponent (nint value, NSCalendarUnit unit);

		[Export ("valueForComponent:")]
		[MacCatalyst (13, 1)]
		nint GetValueForComponent (NSCalendarUnit unit);

		[TV (26, 0), Mac (26, 0), iOS (26, 0), MacCatalyst (26, 0)]
		[Export ("repeatedDay")]
		bool IsRepeatedDay {
			[Bind ("isRepeatedDay")]
			get;
			set;
		}
	}


#if !XAMCORE_5_0
	[BaseType (typeof (NSObject))]
	[Model]
#endif
	[Protocol]
	interface NSCoding {
		// [Abstract]
		[Export ("initWithCoder:")]
		NativeHandle Constructor (NSCoder decoder);

#if WATCHOS
		/// <summary>Encodes the state of the object using the provided encoder.</summary>
		/// <param name="encoder">The encoder that receives the object's state.</param>
		/// <remarks>Implement this method and an <c>initWithCoder:</c> constructor to support native object coding.</remarks>
#else
		/// <summary>Encodes the state of the object using the provided encoder.</summary>
		/// <param name="encoder">The encoder object where the state of the object will be stored</param>
		/// <remarks>
		///   <para>This method is part of the <see cref="INSCoding" /> protocol and is used by applications to preserve the state of the object into an archive.</para>
		///   <para>Developers will typically create an <see cref="NSKeyedArchiver" /> and then invoke the <see cref="NSKeyedArchiver.ArchiveRootObjectToFile(Foundation.NSObject,System.String)" /> method which will call into this method.</para>
		///   <para>If developers want to allow their object to be archived, they should override this method and store their state in using the provided <paramref name="encoder" /> parameter. In addition, developers should also implement a constructor that takes an NSCoder argument and is exported with <c>[Export ("initWithCoder:")]</c>.</para>
		///   <example>
		///   <code lang="csharp lang-csharp"><![CDATA[public void override EncodeTo (NSCoder coder) {
		/// coder.Encode (1, key: "version");
		/// coder.Encode (userName, key: "userName");
		/// coder.Encode (hostName, key: "hostName");]]></code>
		/// </example>
		/// </remarks>
#endif
		[Abstract]
		[Export ("encodeWithCoder:")]
		void EncodeTo (NSCoder encoder);
	}


	interface INSCoding { }


	/// <summary>The secure coding category.</summary>
	/// <remarks>To be added.</remarks>
	[Protocol]
	interface NSSecureCoding : NSCoding {
		// note: +supportsSecureCoding being static it is not a good "generated" binding candidate
	}


#if !XAMCORE_5_0
	[BaseType (typeof (NSObject))]
	[Model]
#endif
	[Protocol]
	interface NSCopying {
		/// <param name="zone">Developers should pass <see langword="null" />.  Memory zones are no longer used.</param>
		/// <summary>Performs a copy of the underlying Objective-C object.</summary>
		/// <returns>The newly-allocated object.</returns>
		/// <remarks>
		///           <para>This method performs a "shallow copy" of <see langword="this" />. If this object contains references to external objects, the new object will contain references to the same object.</para>
		///         </remarks>
		[Abstract]
		[return: Release]
		[Export ("copyWithZone:")]
		NSObject Copy ([NullAllowed] NSZone zone);
	}


#if !XAMCORE_5_0
	[BaseType (typeof (NSObject))]
	[Model]
#endif
	[Protocol]
	interface NSMutableCopying : NSCopying {
		/// <param name="zone">Zone to use to allocate this object, or null to use the default zone.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[Export ("mutableCopyWithZone:")]
		[return: Release ()]
		NSObject MutableCopy ([NullAllowed] NSZone zone);
	}


	interface INSMutableCopying { }


	// Sadly, while this API is a poor API and we should in general not use it
	// Apple has now surfaced it on a few methods.   So we need to take the Obsolete
	// out, and we will have to fully support it.
	[BaseType (typeof (NSArray))]
	[DesignatedDefaultCtor]
	interface NSMutableArray {
		/// <param name="capacity">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[DesignatedInitializer]
		[Export ("initWithCapacity:")]
		NativeHandle Constructor (nuint capacity);

		[Internal]
		[Sealed]
		[Export ("addObject:")]
		void _Add (IntPtr obj);

		[Export ("addObject:")]
		void Add (NSObject obj);

		[Internal]
		[Sealed]
		[Export ("insertObject:atIndex:")]
		void _Insert (IntPtr obj, nint index);

		/// <param name="obj">To be added.</param>
		/// <param name="index">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("insertObject:atIndex:")]
		void Insert (NSObject obj, nint index);

		[Export ("removeLastObject")]
		void RemoveLastObject ();

		/// <param name="index">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("removeObjectAtIndex:")]
		void RemoveObject (nint index);

		[Internal]
		[Sealed]
		[Export ("replaceObjectAtIndex:withObject:")]
		void _ReplaceObject (nint index, IntPtr withObject);

		/// <param name="index">To be added.</param>
		/// <param name="withObject">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("replaceObjectAtIndex:withObject:")]
		void ReplaceObject (nint index, NSObject withObject);

		[Export ("removeAllObjects")]
		void RemoveAllObjects ();

		[Export ("addObjectsFromArray:")]
		void AddObjects (NSObject [] source);

		[Internal]
		[Sealed]
		[Export ("insertObjects:atIndexes:")]
		void _InsertObjects (IntPtr objects, NSIndexSet atIndexes);

		[Export ("insertObjects:atIndexes:")]
		void InsertObjects (NSObject [] objects, NSIndexSet atIndexes);

		[Export ("removeObjectsAtIndexes:")]
		void RemoveObjectsAtIndexes (NSIndexSet indexSet);

		[MacCatalyst (13, 1)]
		[Static, Export ("arrayWithContentsOfFile:")]
		NSMutableArray FromFile (string path);

		[MacCatalyst (13, 1)]
		[Static, Export ("arrayWithContentsOfURL:")]
		NSMutableArray FromUrl (NSUrl url);

#if false // https://github.com/dotnet/macios/issues/15577
		[Export ("applyDifference:")]
		void ApplyDifference (NSOrderedCollectionDifference difference);
#endif
	}


	interface NSMutableArray<TValue> : NSMutableArray { }


	[BaseType (typeof (NSData))]
	interface NSMutableData {
		/// <param name="capacity">To be added.</param>
		/// <summary>Factory method that instantiates an NSMutableData instance that can initially hold the specified capacity, in bytes.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Static, Export ("dataWithCapacity:")]
		[Autorelease]
		[PreSnippet ("if (capacity < 0 || capacity > nint.MaxValue) throw new ArgumentOutOfRangeException ();", Optimizable = true)]
		NSMutableData FromCapacity (nint capacity);

		/// <param name="length">To be added.</param>
		/// <summary>Factory method that instantiates itself with the specified length of zeroed-out bytes.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Static, Export ("dataWithLength:")]
		[Autorelease]
		[PreSnippet ("if (length < 0 || length > nint.MaxValue) throw new ArgumentOutOfRangeException ();", Optimizable = true)]
		NSMutableData FromLength (nint length);

		[Static, Export ("data")]
		[Autorelease]
		NSMutableData Create ();

		[Export ("mutableBytes")]
		IntPtr MutableBytes { get; }

		/// <param name="capacity">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("initWithCapacity:")]
		[PreSnippet ("if (capacity > (ulong) nint.MaxValue) throw new ArgumentOutOfRangeException ();", Optimizable = true)]
		NativeHandle Constructor (nuint capacity);

		[Export ("appendData:")]
		void AppendData (NSData other);

		[Export ("appendBytes:length:")]
		void AppendBytes (IntPtr bytes, nuint len);

		[Export ("setData:")]
		void SetData (NSData data);

		[Export ("length")]
		[Override]
		nuint Length { get; set; }

		[Export ("replaceBytesInRange:withBytes:")]
		void ReplaceBytes (NSRange range, IntPtr buffer);

		[Export ("resetBytesInRange:")]
		void ResetBytes (NSRange range);

		[Export ("replaceBytesInRange:withBytes:length:")]
		void ReplaceBytes (NSRange range, IntPtr buffer, nuint length);

		// NSMutableDataCompression (NSMutableData)

		[MacCatalyst (13, 1)]
		[Export ("decompressUsingAlgorithm:error:")]
		bool Decompress (NSDataCompressionAlgorithm algorithm, [NullAllowed] out NSError error);

		[MacCatalyst (13, 1)]
		[Export ("compressUsingAlgorithm:error:")]
		bool Compress (NSDataCompressionAlgorithm algorithm, [NullAllowed] out NSError error);
	}


	[MacCatalyst (13, 1)]
	[Native]
	enum NSDataCompressionAlgorithm : long {
		Lzfse = 0,
		Lz4,
		Lzma,
		Zlib,
	}


	[BaseType (typeof (NSObject))]
	[DesignatedDefaultCtor]
	interface NSDate : NSSecureCoding, NSCopying
#if !WATCHOS
		, CKRecordValue
#endif
	{
		[Export ("timeIntervalSinceReferenceDate")]
		double SecondsSinceReferenceDate { get; }

		[Export ("timeIntervalSinceDate:")]
		double GetSecondsSince (NSDate anotherDate);

		[Export ("timeIntervalSinceNow")]
		double SecondsSinceNow { get; }

		[Export ("timeIntervalSince1970")]
		double SecondsSince1970 { get; }

		[Export ("dateWithTimeIntervalSinceReferenceDate:")]
		[Static]
		NSDate FromTimeIntervalSinceReferenceDate (double secs);

		[Static, Export ("dateWithTimeIntervalSince1970:")]
		NSDate FromTimeIntervalSince1970 (double secs);

		[Export ("date")]
		[Static]
		NSDate Now { get; }

		[Export ("distantPast")]
		[Static]
		NSDate DistantPast { get; }

		[Export ("distantFuture")]
		[Static]
		NSDate DistantFuture { get; }

		[Export ("dateByAddingTimeInterval:")]
		NSDate AddSeconds (double seconds);

		[Export ("dateWithTimeIntervalSinceNow:")]
		[Static]
		NSDate FromTimeIntervalSinceNow (double secs);

		[Export ("descriptionWithLocale:")]
		string DescriptionWithLocale (NSLocale locale);

		[Export ("earlierDate:")]
		NSDate EarlierDate (NSDate anotherDate);

		[Export ("laterDate:")]
		NSDate LaterDate (NSDate anotherDate);

		[Export ("compare:")]
		NSComparisonResult Compare (NSDate other);

		[Export ("isEqualToDate:")]
		bool IsEqualToDate (NSDate other);

		// NSDate_SensorKit

		[NoTV, NoMac]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Static]
		[Export ("dateWithSRAbsoluteTime:")]
		NSDate CreateFromSRAbsoluteTime (double time);

		[NoTV, NoMac]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Export ("initWithSRAbsoluteTime:")]
		NativeHandle Constructor (double srAbsoluteTime);

		[NoTV, NoMac]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Export ("srAbsoluteTime")]
		double SrAbsoluteTime { get; }

		[Field ("NSSystemClockDidChangeNotification")]
		[Notification]
		NSString SystemClockDidChangeNotification { get; }
	}


	[BaseType (typeof (NSObject))]
	[DesignatedDefaultCtor]
	interface NSDictionary : NSSecureCoding, NSMutableCopying,
#if !WATCHOS
		NSFetchRequestResult,
#endif
		INSFastEnumeration {
		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'NSMutableDictionary.FromFile' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'NSMutableDictionary.FromFile' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'NSMutableDictionary.FromFile' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'NSMutableDictionary.FromFile' instead.")]
		[Export ("dictionaryWithContentsOfFile:")]
		[Static]
		NSDictionary FromFile (string path);

		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'NSMutableDictionary.FromUrl' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'NSMutableDictionary.FromUrl' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'NSMutableDictionary.FromUrl' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'NSMutableDictionary.FromUrl' instead.")]
		[Export ("dictionaryWithContentsOfURL:")]
		[Static]
		NSDictionary FromUrl (NSUrl url);

		[Export ("dictionaryWithObject:forKey:")]
		[Static]
		NSDictionary FromObjectAndKey (NSObject obj, NSObject key);

		[Export ("dictionaryWithDictionary:")]
		[Static]
		NSDictionary FromDictionary (NSDictionary source);

		[Export ("dictionaryWithObjects:forKeys:count:")]
		[Static, Internal]
		IntPtr _FromObjectsAndKeysInternal (IntPtr objects, IntPtr keys, nint count);

		[Export ("dictionaryWithObjects:forKeys:count:")]
		[Static, Internal]
		NSDictionary FromObjectsAndKeysInternal ([NullAllowed] NSArray objects, [NullAllowed] NSArray keys, nint count);

		[Export ("dictionaryWithObjects:forKeys:")]
		[Static, Internal]
		IntPtr _FromObjectsAndKeysInternal (IntPtr objects, IntPtr keys);

		[Export ("dictionaryWithObjects:forKeys:")]
		[Static, Internal]
		NSDictionary FromObjectsAndKeysInternal (NSArray objects, NSArray keys);

		[Export ("initWithDictionary:")]
		NativeHandle Constructor (NSDictionary other);

		[Export ("initWithDictionary:copyItems:")]
		NativeHandle Constructor (NSDictionary other, bool copyItems);

		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'NSMutableDictionary(string)' constructor instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'NSMutableDictionary(string)' constructor instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'NSMutableDictionary(string)' constructor instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'NSMutableDictionary(string)' constructor instead.")]
		[Export ("initWithContentsOfFile:")]
		NativeHandle Constructor (string fileName);

		[Export ("initWithObjects:forKeys:"), Internal]
		NativeHandle Constructor (NSArray objects, NSArray keys);

		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'NSMutableDictionary(NSUrl)' constructor instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'NSMutableDictionary(NSUrl)' constructor instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'NSMutableDictionary(NSUrl)' constructor instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'NSMutableDictionary(NSUrl)' constructor instead.")]
		[Export ("initWithContentsOfURL:")]
		NativeHandle Constructor (NSUrl url);

		[MacCatalyst (13, 1)]
		[Export ("initWithContentsOfURL:error:")]
		NativeHandle Constructor (NSUrl url, out NSError error);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("dictionaryWithContentsOfURL:error:")]
		[return: NullAllowed]
		NSDictionary<NSString, NSObject> FromUrl (NSUrl url, out NSError error);

		[Export ("count")]
		nuint Count { get; }

		[Internal]
		[Sealed]
		[Export ("objectForKey:")]
		IntPtr _ObjectForKey (IntPtr key);

		[return: NullAllowed]
		[Export ("objectForKey:")]
		NSObject ObjectForKey (NSObject key);

		[Internal]
		[Sealed]
		[Export ("allKeys")]
		IntPtr _AllKeys ();

		[Export ("allKeys")]
		[Autorelease]
		NSObject [] Keys { get; }

		[Internal]
		[Sealed]
		[Export ("allKeysForObject:")]
		IntPtr _AllKeysForObject (IntPtr obj);

		[Export ("allKeysForObject:")]
		[Autorelease]
		NSObject [] KeysForObject (NSObject obj);

		[Internal]
		[Sealed]
		[Export ("allValues")]
		IntPtr _AllValues ();

		[Export ("allValues")]
		[Autorelease]
		NSObject [] Values { get; }

		[Export ("descriptionInStringsFileFormat")]
		string DescriptionInStringsFileFormat { get; }

		[Export ("isEqualToDictionary:")]
		bool IsEqualToDictionary (NSDictionary other);

		[Export ("objectEnumerator")]
		NSEnumerator ObjectEnumerator { get; }

		[Internal]
		[Sealed]
		[Export ("objectsForKeys:notFoundMarker:")]
		IntPtr _ObjectsForKeys (IntPtr keys, IntPtr marker);

		[Export ("objectsForKeys:notFoundMarker:")]
		[Autorelease]
		NSObject [] ObjectsForKeys (NSArray keys, NSObject marker);

		[Deprecated (PlatformName.MacOSX, 10, 15)]
		[Deprecated (PlatformName.iOS, 13, 0)]
		[Deprecated (PlatformName.TvOS, 13, 0)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1)]
		[Export ("writeToFile:atomically:")]
		bool WriteToFile (string path, bool useAuxiliaryFile);

		[Deprecated (PlatformName.MacOSX, 10, 15)]
		[Deprecated (PlatformName.iOS, 13, 0)]
		[Deprecated (PlatformName.TvOS, 13, 0)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1)]
		[Export ("writeToURL:atomically:")]
		bool WriteToUrl (NSUrl url, bool atomically);

		[Static]
		[Export ("sharedKeySetForKeys:")]
		NSObject GetSharedKeySetForKeys (NSObject [] keys);

		[Export ("descriptionWithLocale:")]
		string GetDescription ([NullAllowed] NSObject locale);

		[Export ("descriptionWithLocale:indent:")]
		string GetDescription ([NullAllowed] NSObject locale, nuint level);

		[Export ("enumerateKeysAndObjectsUsingBlock:")]
		void Enumerate (NSDictionaryEnumerator block);

		[Export ("enumerateKeysAndObjectsWithOptions:usingBlock:")]
		void Enumerate (NSEnumerationOptions options, NSDictionaryEnumerator block);

		[Export ("keysOfEntriesPassingTest:")]
		NSSet GetKeys (NSDictionaryKeyFilter predicate);

		[Export ("keysOfEntriesWithOptions:passingTest:")]
		NSSet GetKeys (NSEnumerationOptions options, NSDictionaryKeyFilter predicate);

		[Export ("keysSortedByValueUsingComparator:")]
		NSObject [] GetKeysSortedByValue (NSComparator comparator);

		[Export ("keysSortedByValueWithOptions:usingComparator:")]
		NSObject [] GetKeysSortedByValue (NSSortOptions options, NSComparator comparator);

		[return: NullAllowed]
		[Export ("valueForKey:")]
		[MarshalNativeExceptions]
		NSObject ValueForKey (NSString key);

		[Export ("writeToURL:error:")]
		bool WriteToUrl (NSUrl url, [NullAllowed] out NSError error);

		[Export ("keyEnumerator")]
		NSEnumerator KeyEnumerator { get; }
	}


	delegate void NSDictionaryEnumerator (NSObject key, NSObject obj, ref bool stop);

	delegate bool NSDictionaryKeyFilter (NSObject key, NSObject obj, ref bool stop);


	interface NSDictionary<K, V> : NSDictionary { }


	[BaseType (typeof (NSObject))]
	interface NSEnumerator {
		[Export ("nextObject")]
		NSObject NextObject ();
	}


	interface NSEnumerator<T> : NSEnumerator { }


	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface NSError : NSSecureCoding, NSCopying {
		/// <param name="domain">To be added.</param>
		/// <param name="code">To be added.</param>
		/// <param name="userInfo">
		///           <para>To be added.</para>
		///           <para tool="nullallowed">This parameter can be <see langword="null" />.</para>
		///         </param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Static, Export ("errorWithDomain:code:userInfo:")]
		NSError FromDomain (NSString domain, nint code, [NullAllowed] NSDictionary userInfo);

		/// <param name="domain">To be added.</param>
		/// <param name="code">To be added.</param>
		/// <param name="userInfo">
		///           <para>To be added.</para>
		///           <para tool="nullallowed">This parameter can be <see langword="null" />.</para>
		///         </param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[DesignatedInitializer]
		[Export ("initWithDomain:code:userInfo:")]
		NativeHandle Constructor (NSString domain, nint code, [NullAllowed] NSDictionary userInfo);

		[Export ("domain")]
		string Domain { get; }

		[Export ("code")]
		nint Code { get; }

		[Export ("userInfo")]
		NSDictionary UserInfo { get; }

		[Export ("localizedDescription")]
		string LocalizedDescription { get; }

		[Export ("localizedFailureReason")]
		string LocalizedFailureReason { get; }

		[Export ("localizedRecoverySuggestion")]
		string LocalizedRecoverySuggestion { get; }

		[Export ("localizedRecoveryOptions")]
		string [] LocalizedRecoveryOptions { get; }

		[Export ("helpAnchor")]
		string HelpAnchor { get; }

		[TV (14, 5), iOS (14, 5)]
		[MacCatalyst (14, 5)]
		[Export ("underlyingErrors", ArgumentSemantic.Copy)]
		NSError [] UnderlyingErrors { get; }

		/// <summary>The Cocoa error domain.</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSCocoaErrorDomain")]
		NSString CocoaErrorDomain { get; }

		/// <summary>Represents the value associated with the constant NSPOSIXErrorDomain</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSPOSIXErrorDomain")]
		NSString PosixErrorDomain { get; }

		/// <summary>Represents the value associated with the constant NSOSStatusErrorDomain</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSOSStatusErrorDomain")]
		NSString OsStatusErrorDomain { get; }

		/// <summary>Represents the value associated with the constant NSMachErrorDomain</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSMachErrorDomain")]
		NSString MachErrorDomain { get; }

		/// <summary>Represents the value associated with the constant NSURLErrorDomain</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLErrorDomain")]
		NSString NSUrlErrorDomain { get; }

		/// <summary>Represents the value associated with the constant NSNetServicesErrorDomain</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[MacCatalyst (13, 1)]
		[Field ("NSNetServicesErrorDomain")]
		NSString NSNetServicesErrorDomain { get; }

		[NoWatch]
		[Field ("NSNetServicesErrorCode")]
		NSString NSNetServicesErrorCode { get; }

		/// <summary>Represents the value associated with the constant NSStreamSocketSSLErrorDomain</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSStreamSocketSSLErrorDomain")]
		NSString NSStreamSocketSSLErrorDomain { get; }

		/// <summary>Represents the value associated with the constant NSStreamSOCKSErrorDomain</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSStreamSOCKSErrorDomain")]
		NSString NSStreamSOCKSErrorDomain { get; }

#if !WATCHOS
		/// <summary>The CoreLocation error domain.</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("kCLErrorDomain", "CoreLocation")]
		NSString CoreLocationErrorDomain { get; }

		/// <summary>The CFNetwork error domain.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("kCFErrorDomainCFNetwork", "CFNetwork")]
		NSString CFNetworkErrorDomain { get; }

		/// <summary>The CoreMotion error domain.</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[NoMac, NoTV]
		[MacCatalyst (13, 1)]
		[Field ("CMErrorDomain", "CoreMotion")]
		NSString CoreMotionErrorDomain { get; }

		[NoMac, NoTV]
		[NoMacCatalyst] // We don't expose CarPlay on Mac Catalyst for the moment // [MacCatalyst (14, 0)]
		[Field ("CarPlayErrorDomain", "CarPlay")]
		NSString CarPlayErrorDomain { get; }
#endif

		/// <summary>Represents the value associated with the constant NSUnderlyingErrorKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSUnderlyingErrorKey")]
		NSString UnderlyingErrorKey { get; }

		[TV (14, 5), iOS (14, 5)]
		[MacCatalyst (14, 5)]
		[Field ("NSMultipleUnderlyingErrorsKey")]
		NSString MultipleUnderlyingErrorsKey { get; }

		/// <summary>Standard key to use in the userInfo payload for associating a localized description of the problem to provide to the user.</summary>
		///         <value />
		///         <remarks>To be added.</remarks>
		[Field ("NSLocalizedDescriptionKey")]
		NSString LocalizedDescriptionKey { get; }

		/// <summary>Represents the value associated with the constant NSLocalizedFailureReasonErrorKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSLocalizedFailureReasonErrorKey")]
		NSString LocalizedFailureReasonErrorKey { get; }

		/// <summary>Represents the value associated with the constant NSLocalizedRecoverySuggestionErrorKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSLocalizedRecoverySuggestionErrorKey")]
		NSString LocalizedRecoverySuggestionErrorKey { get; }

		/// <summary>Represents the value associated with the constant NSLocalizedRecoveryOptionsErrorKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSLocalizedRecoveryOptionsErrorKey")]
		NSString LocalizedRecoveryOptionsErrorKey { get; }

		/// <summary>Represents the value associated with the constant NSRecoveryAttempterErrorKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSRecoveryAttempterErrorKey")]
		NSString RecoveryAttempterErrorKey { get; }

		/// <summary>Standard key to use in the userInfo payload for associating a hint to provide to the user.</summary>
		///         <value />
		///         <remarks>To be added.</remarks>
		[Field ("NSHelpAnchorErrorKey")]
		NSString HelpAnchorErrorKey { get; }

		/// <summary>Represents the value associated with the constant NSStringEncodingErrorKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSStringEncodingErrorKey")]
		NSString StringEncodingErrorKey { get; }

		/// <summary>Represents the value associated with the constant NSURLErrorKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLErrorKey")]
		NSString UrlErrorKey { get; }

		/// <summary>Standard key to use in the userInfo payload for associating a file path with the error.</summary>
		///         <value />
		///         <remarks>To be added.</remarks>
		[Field ("NSFilePathErrorKey")]
		NSString FilePathErrorKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSDebugDescriptionErrorKey")]
		NSString DebugDescriptionErrorKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSLocalizedFailureErrorKey")]
		NSString LocalizedFailureErrorKey { get; }

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("setUserInfoValueProviderForDomain:provider:")]
		void SetUserInfoValueProvider (string errorDomain, [NullAllowed] NSErrorUserInfoValueProvider provider);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("userInfoValueProviderForDomain:")]
		[return: NullAllowed]
		NSErrorUserInfoValueProvider GetUserInfoValueProvider (string errorDomain);
#if !WATCHOS

		// From NSError (NSFileProviderError) Category to avoid static category uglyness

		/// <param name="existingItem">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[NoMacCatalyst]
		[NoTV]
		[Static]
		[Export ("fileProviderErrorForCollisionWithItem:")]
		NSError GetFileProviderError (INSFileProviderItem existingItem);

#endif

		/// <param name="nonExistentItemIdentifier">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[NoMacCatalyst]
		[NoTV]
		[Static]
		[Export ("fileProviderErrorForNonExistentItemWithIdentifier:")]
		NSError GetFileProviderError (string nonExistentItemIdentifier);
#if !WATCHOS

		[iOS (16, 0)]
		[NoMacCatalyst]
		[NoTV]
		[Static]
		[Export ("fileProviderErrorForRejectedDeletionOfItem:")]
		NSError GetFileProviderErrorForRejectedDeletion (INSFileProviderItem updatedVersion);

#endif

#if false
		// FIXME that value is present in the header (7.0 DP 6) files but returns NULL (i.e. unusable)
		// we're also missing other NSURLError* fields (which we should add)
		[Field ("NSURLErrorBackgroundTaskCancelledReasonKey")]
		NSString NSUrlErrorBackgroundTaskCancelledReasonKey { get; }
#endif

#if IOS && !MACCATALYST
		[iOS (18, 2), NoMacCatalyst, NoTV, NoMac]
		[Field ("UIApplicationCategoryDefaultRetryAvailabilityDateErrorKey", "UIKit")]
		NSString UIApplicationCategoryDefaultRetryAvailabilityDateErrorKey { get; }

		[iOS (18, 2), NoMacCatalyst, NoTV, NoMac]
		[Field ("UIApplicationCategoryDefaultStatusLastProvidedDateErrorKey", "UIKit")]
		NSString UIApplicationCategoryDefaultStatusLastProvidedDateErrorKey { get; }
#endif
	}


	/// <param name="error">To be added.</param>
	/// <param name="userInfoKey">To be added.</param>
	/// <summary>Delegate returned by <see cref="Foundation.NSError.GetUserInfoValueProvider(System.String)" />.</summary>
	/// <returns>To be added.</returns>
	/// <remarks>To be added.</remarks>
	delegate NSObject NSErrorUserInfoValueProvider (NSError error, NSString userInfoKey);


	[BaseType (typeof (NSObject))]
	// 'init' returns NIL
	[DisableDefaultCtor]
	interface NSException : NSSecureCoding, NSCopying {
		[DesignatedInitializer]
		[Export ("initWithName:reason:userInfo:")]
		NativeHandle Constructor (string name, string reason, [NullAllowed] NSDictionary userInfo);

		[Export ("name")]
		string Name { get; }

		[Export ("reason")]
		string Reason { get; }

		[Export ("userInfo")]
		NSObject UserInfo { get; }

		[Export ("callStackReturnAddresses")]
		NSNumber [] CallStackReturnAddresses { get; }

		[Export ("callStackSymbols")]
		string [] CallStackSymbols { get; }
	}


	[BaseType (typeof (NSObject))]
	interface NSNull : NSSecureCoding, NSCopying
#if !WATCHOS
		, CAAction
#endif
	{
		[Export ("null"), Static]
		[Internal]
		NSNull _Null { get; }

		[Export ("null"), Static]
		[Internal]
		IntPtr _NullHandle { get; }
	}


	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface NSLocale : NSSecureCoding, NSCopying {
		[Static]
		[Export ("systemLocale", ArgumentSemantic.Copy)]
		NSLocale SystemLocale { get; }

		[Static]
		[Export ("currentLocale", ArgumentSemantic.Copy)]
		NSLocale CurrentLocale { get; }

		[Static]
		[Export ("autoupdatingCurrentLocale", ArgumentSemantic.Strong)]
		NSLocale AutoUpdatingCurrentLocale { get; }

		[DesignatedInitializer]
		[Export ("initWithLocaleIdentifier:")]
		NativeHandle Constructor (string identifier);

		[Export ("localeIdentifier")]
		string LocaleIdentifier { get; }

		[Export ("availableLocaleIdentifiers", ArgumentSemantic.Copy)]
		[Static]
		string [] AvailableLocaleIdentifiers { get; }

		[Export ("ISOLanguageCodes", ArgumentSemantic.Copy)]
		[Static]
		string [] ISOLanguageCodes { get; }

		[Export ("ISOCurrencyCodes", ArgumentSemantic.Copy)]
		[Static]
		string [] ISOCurrencyCodes { get; }

		[Export ("ISOCountryCodes", ArgumentSemantic.Copy)]
		[Static]
		string [] ISOCountryCodes { get; }

		[Export ("commonISOCurrencyCodes", ArgumentSemantic.Copy)]
		[Static]
		string [] CommonISOCurrencyCodes { get; }

		[Export ("preferredLanguages", ArgumentSemantic.Copy)]
		[Static]
		string [] PreferredLanguages { get; }

		[Export ("componentsFromLocaleIdentifier:")]
		[Static]
		NSDictionary ComponentsFromLocaleIdentifier (string identifier);

		[Export ("localeIdentifierFromComponents:")]
		[Static]
		string LocaleIdentifierFromComponents (NSDictionary dict);

		[Export ("canonicalLanguageIdentifierFromString:")]
		[Static]
		string CanonicalLanguageIdentifierFromString (string str);

		[Export ("canonicalLocaleIdentifierFromString:")]
		[Static]
		string CanonicalLocaleIdentifierFromString (string str);

		[Export ("characterDirectionForLanguage:")]
		[Static]
		NSLocaleLanguageDirection GetCharacterDirection (string isoLanguageCode);

		[Export ("lineDirectionForLanguage:")]
		[Static]
		NSLocaleLanguageDirection GetLineDirection (string isoLanguageCode);

		[Static]
		[Export ("localeWithLocaleIdentifier:")]
		NSLocale FromLocaleIdentifier (string ident);

		[Field ("NSCurrentLocaleDidChangeNotification")]
		[Notification]
		NSString CurrentLocaleDidChangeNotification { get; }

		[return: NullAllowed]
		[Export ("objectForKey:"), Internal]
		NSObject ObjectForKey (NSString key);

		[return: NullAllowed]
		[Export ("displayNameForKey:value:"), Internal]
		NSString DisplayNameForKey (NSString key, string value);

		[Internal, Field ("NSLocaleIdentifier")]
		NSString _Identifier { get; }

		[Internal, Field ("NSLocaleLanguageCode")]
		NSString _LanguageCode { get; }

		[Internal, Field ("NSLocaleCountryCode")]
		NSString _CountryCode { get; }

		[Internal, Field ("NSLocaleScriptCode")]
		NSString _ScriptCode { get; }

		[Internal, Field ("NSLocaleVariantCode")]
		NSString _VariantCode { get; }

		[Internal, Field ("NSLocaleExemplarCharacterSet")]
		NSString _ExemplarCharacterSet { get; }

		[Internal, Field ("NSLocaleCalendar")]
		NSString _Calendar { get; }

		[Internal, Field ("NSLocaleCollationIdentifier")]
		NSString _CollationIdentifier { get; }

		[Internal, Field ("NSLocaleUsesMetricSystem")]
		NSString _UsesMetricSystem { get; }

		[Internal, Field ("NSLocaleMeasurementSystem")]
		NSString _MeasurementSystem { get; }

		[Internal, Field ("NSLocaleDecimalSeparator")]
		NSString _DecimalSeparator { get; }

		[Internal, Field ("NSLocaleGroupingSeparator")]
		NSString _GroupingSeparator { get; }

		[Internal, Field ("NSLocaleCurrencySymbol")]
		NSString _CurrencySymbol { get; }

		[Internal, Field ("NSLocaleCurrencyCode")]
		NSString _CurrencyCode { get; }

		[Internal, Field ("NSLocaleCollatorIdentifier")]
		NSString _CollatorIdentifier { get; }

		[Internal, Field ("NSLocaleQuotationBeginDelimiterKey")]
		NSString _QuotationBeginDelimiterKey { get; }

		[Internal, Field ("NSLocaleQuotationEndDelimiterKey")]
		NSString _QuotationEndDelimiterKey { get; }

		[Internal, Field ("NSLocaleAlternateQuotationBeginDelimiterKey")]
		NSString _AlternateQuotationBeginDelimiterKey { get; }

		[Internal, Field ("NSLocaleAlternateQuotationEndDelimiterKey")]
		NSString _AlternateQuotationEndDelimiterKey { get; }

		// follow the pattern of NSLocale.cs which included managed helpers that did the same

		[MacCatalyst (13, 1)]
		[Export ("calendarIdentifier")]
		string CalendarIdentifier { get; }

		[MacCatalyst (13, 1)]
		[Export ("localizedStringForCalendarIdentifier:")]
		[return: NullAllowed]
		string GetLocalizedCalendarIdentifier (string calendarIdentifier);

		[TV (17, 0), iOS (17, 0), MacCatalyst (17, 0)]
		[Export ("languageIdentifier")]
		string LanguageIdentifier { get; }

		[TV (17, 0), iOS (17, 0), MacCatalyst (17, 0)]
		[NullAllowed, Export ("regionCode")]
		string RegionCode { get; }
	}


	[BaseType (typeof (NSObject))]
	// init returns NIL
	[DisableDefaultCtor]
	interface NSRunLoop {
		[Export ("currentRunLoop", ArgumentSemantic.Strong)]
		[Static]
		[IsThreadStatic]
		NSRunLoop Current { get; }

		[Export ("mainRunLoop", ArgumentSemantic.Strong)]
		[Static]
		NSRunLoop Main { get; }

		[Export ("currentMode")]
		NSString CurrentMode { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Wrap ("NSRunLoopModeExtensions.GetValue (CurrentMode)")]
		NSRunLoopMode CurrentRunLoopMode { get; }

		[Export ("getCFRunLoop")]
		CFRunLoop GetCFRunLoop ();

		[Export ("addTimer:forMode:")]
		void AddTimer (NSTimer timer, NSString forMode);

		/// <param name="timer">To be added.</param>
		///         <param name="forMode">The runloop to insert this into.</param>
		///         <summary>To be added.</summary>
		///         <remarks>To be added.</remarks>
		[Wrap ("AddTimer (timer, forMode.GetConstant ()!)")]
		void AddTimer (NSTimer timer, NSRunLoopMode forMode);

		[Export ("limitDateForMode:")]
		NSDate LimitDateForMode (NSString mode);

		/// <param name="mode">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[Wrap ("LimitDateForMode (mode.GetConstant ()!)")]
		NSDate LimitDateForMode (NSRunLoopMode mode);

		[Export ("acceptInputForMode:beforeDate:")]
		void AcceptInputForMode (NSString mode, NSDate limitDate);

		/// <param name="mode">To be added.</param>
		///         <param name="limitDate">To be added.</param>
		///         <summary>To be added.</summary>
		///         <remarks>To be added.</remarks>
		[Wrap ("AcceptInputForMode (mode.GetConstant ()!, limitDate)")]
		void AcceptInputForMode (NSRunLoopMode mode, NSDate limitDate);

		[Export ("run")]
		void Run ();

		[Export ("runUntilDate:")]
		void RunUntil (NSDate date);

		[Export ("runMode:beforeDate:")]
		bool RunUntil (NSString runLoopMode, NSDate limitdate);

		/// <param name="runLoopMode">To be added.</param>
		///         <param name="limitDate">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[Wrap ("RunUntil (runLoopMode.GetConstant ()!, limitDate)")]
		bool RunUntil (NSRunLoopMode runLoopMode, NSDate limitDate);

		[MacCatalyst (13, 1)]
		[Export ("performBlock:")]
		void Perform (Action block);

		[MacCatalyst (13, 1)]
		[Export ("performInModes:block:")]
		void Perform (NSString [] modes, Action block);

		/// <param name="modes">To be added.</param>
		///         <param name="block">To be added.</param>
		///         <summary>To be added.</summary>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Wrap ("Perform (modes.GetConstants ()!, block)")]
		void Perform (NSRunLoopMode [] modes, Action block);
	}


	[BaseType (typeof (NSObject))]
	[DesignatedDefaultCtor]
	interface NSSet : NSSecureCoding, NSMutableCopying {
		[Export ("set")]
		[Static]
		NSSet CreateSet ();

		[Export ("initWithSet:")]
		NativeHandle Constructor (NSSet other);

		[Export ("initWithArray:")]
		NativeHandle Constructor (NSArray other);

		[Export ("count")]
		nuint Count { get; }

		[Internal]
		[Sealed]
		[Export ("member:")]
		IntPtr _LookupMember (IntPtr probe);

		[Export ("member:")]
		NSObject LookupMember (NSObject probe);

		[Internal]
		[Sealed]
		[Export ("anyObject")]
		IntPtr _AnyObject { get; }

		[Export ("anyObject")]
		NSObject AnyObject { get; }

		[Internal]
		[Sealed]
		[Export ("containsObject:")]
		bool _Contains (NativeHandle id);

		[Export ("containsObject:")]
		bool Contains (NSObject id);

		[Export ("allObjects")]
		[Internal]
		IntPtr _AllObjects ();

		[Export ("isEqualToSet:")]
		bool IsEqualToSet (NSSet other);

		[Export ("objectEnumerator"), Internal]
		NSEnumerator _GetEnumerator ();

		[Export ("isSubsetOfSet:")]
		bool IsSubsetOf (NSSet other);

		[Export ("enumerateObjectsUsingBlock:")]
		void Enumerate (NSSetEnumerator enumerator);

		[Internal]
		[Sealed]
		[Export ("setByAddingObjectsFromSet:")]
		NativeHandle _SetByAddingObjectsFromSet (NativeHandle other);

		[Export ("setByAddingObjectsFromSet:"), Internal]
		NSSet SetByAddingObjectsFromSet (NSSet other);

		[Export ("intersectsSet:")]
		bool IntersectsSet (NSSet other);

		[Internal]
		[Static]
		[Export ("setWithArray:")]
		NativeHandle _SetWithArray (NativeHandle array);
	}


	interface NSSet<TKey> : NSSet { }


	[BaseType (typeof (NSObject))]
	interface NSSortDescriptor : NSSecureCoding, NSCopying {
		[Export ("initWithKey:ascending:")]
		NativeHandle Constructor (string key, bool ascending);

		[Export ("initWithKey:ascending:selector:")]
		NativeHandle Constructor (string key, bool ascending, [NullAllowed] Selector selector);

		[Export ("initWithKey:ascending:comparator:")]
		NativeHandle Constructor (string key, bool ascending, NSComparator comparator);

		[Export ("key")]
		string Key { get; }

		[Export ("ascending")]
		bool Ascending { get; }

		[NullAllowed]
		[Export ("selector")]
		Selector Selector { get; }

		[Export ("compareObject:toObject:")]
		NSComparisonResult Compare (NSObject object1, NSObject object2);

		[Export ("reversedSortDescriptor")]
		NSObject ReversedSortDescriptor { get; }

		[MacCatalyst (13, 1)]
		[Export ("allowEvaluation")]
		void AllowEvaluation ();
	}


	/// <summary>Defines an extension method for <see cref="Foundation.NSOrderedSet" /> objects, allowing sorting by <see cref="Foundation.NSSortDescriptor" /> objects.</summary>
	/// <remarks>To be added.</remarks>
	[Category, BaseType (typeof (NSOrderedSet))]
	partial interface NSKeyValueSorting_NSOrderedSet {
		/// <param name="sortDescriptors">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("sortedArrayUsingDescriptors:")]
		NSObject [] GetSortedArray (NSSortDescriptor [] sortDescriptors);
	}


#pragma warning disable 618
	/// <summary>Defines a static method for sorting <see cref="Foundation.NSMutableArray" /> objects using <see cref="Foundation.NSSortDescriptor" /> objects.</summary>
	/// <remarks>To be added.</remarks>
	[Category, BaseType (typeof (NSMutableArray))]
#pragma warning restore 618
	partial interface NSSortDescriptorSorting_NSMutableArray {
		/// <param name="sortDescriptors">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("sortUsingDescriptors:")]
		void SortUsingDescriptors (NSSortDescriptor [] sortDescriptors);
	}


	/// <summary>Defines an extension method for <see cref="NSMutableOrderedSet" /> objects, allowing them to be sorted using <see cref="Foundation.NSSortDescriptor" /> objects.</summary>
	/// <remarks>To be added.</remarks>
	[Category, BaseType (typeof (NSMutableOrderedSet))]
	partial interface NSKeyValueSorting_NSMutableOrderedSet {
		/// <param name="sortDescriptors">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("sortUsingDescriptors:")]
		void SortUsingDescriptors (NSSortDescriptor [] sortDescriptors);
	}


	[BaseType (typeof (NSObject))]
	[Dispose ("if (disposing) { Invalidate (); } ", Optimizable = true)]
	// init returns NIL
	[DisableDefaultCtor]
	interface NSTimer {

		[Static, Export ("scheduledTimerWithTimeInterval:target:selector:userInfo:repeats:")]
		NSTimer CreateScheduledTimer (double seconds, NSObject target, Selector selector, [NullAllowed] NSObject userInfo, bool repeats);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("scheduledTimerWithTimeInterval:repeats:block:")]
		NSTimer CreateScheduledTimer (double interval, bool repeats, Action<NSTimer> block);

		[Static, Export ("timerWithTimeInterval:target:selector:userInfo:repeats:")]
		NSTimer CreateTimer (double seconds, NSObject target, Selector selector, [NullAllowed] NSObject userInfo, bool repeats);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("timerWithTimeInterval:repeats:block:")]
		NSTimer CreateTimer (double interval, bool repeats, Action<NSTimer> block);

		[DesignatedInitializer]
		[Export ("initWithFireDate:interval:target:selector:userInfo:repeats:")]
		NativeHandle Constructor (NSDate date, double seconds, NSObject target, Selector selector, [NullAllowed] NSObject userInfo, bool repeats);

		[MacCatalyst (13, 1)]
		[Export ("initWithFireDate:interval:repeats:block:")]
		NativeHandle Constructor (NSDate date, double seconds, bool repeats, Action<NSTimer> block);

		[Export ("fire")]
		void Fire ();

		[NullAllowed] // by default this property is null
		[Export ("fireDate", ArgumentSemantic.Copy)]
		NSDate FireDate { get; set; }

		[Export ("invalidate")]
		void Invalidate ();

		/// <summary>Returns <see langword="true" /> if the the timer will still fire at some point in the future.</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Export ("isValid")]
		bool IsValid { get; }

		[Export ("timeInterval")]
		double TimeInterval { get; }

		[Export ("userInfo")]
		NSObject UserInfo { get; }

		[MacCatalyst (13, 1)]
		[Export ("tolerance")]
		double Tolerance { get; set; }
	}


	[BaseType (typeof (NSObject))]
	// NSTimeZone is an abstract class that defines the behavior of time zone objects. -> http://developer.apple.com/library/ios/#documentation/Cocoa/Reference/Foundation/Classes/NSTimeZone_Class/Reference/Reference.html
	// calling 'init' returns a NIL pointer, i.e. an unusable instance
	[DisableDefaultCtor]
	interface NSTimeZone : NSSecureCoding, NSCopying {
		[Export ("initWithName:")]
		NativeHandle Constructor (string name);

		[Export ("initWithName:data:")]
		NativeHandle Constructor (string name, NSData data);

		[Export ("name")]
		string Name { get; }

		[Export ("data")]
		NSData Data { get; }

		[Export ("secondsFromGMTForDate:")]
		nint SecondsFromGMT (NSDate date);

		[Static]
		[Export ("abbreviationDictionary")]
		NSDictionary Abbreviations { get; }

		[Export ("abbreviation")]
		string Abbreviation ();

		[Export ("abbreviationForDate:")]
		string Abbreviation (NSDate date);

		[Export ("isDaylightSavingTimeForDate:")]
		bool IsDaylightSavingsTime (NSDate date);

		[Export ("daylightSavingTimeOffsetForDate:")]
		double DaylightSavingTimeOffset (NSDate date);

		[Export ("nextDaylightSavingTimeTransitionAfterDate:")]
		NSDate NextDaylightSavingTimeTransitionAfter (NSDate date);

		[Static, Export ("timeZoneWithName:")]
		NSTimeZone FromName (string tzName);

		[Static, Export ("timeZoneWithName:data:")]
		NSTimeZone FromName (string tzName, NSData data);

		/// <param name="seconds">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Static]
		[Export ("timeZoneForSecondsFromGMT:")]
		NSTimeZone FromGMT (nint seconds);

		[Static, Export ("localTimeZone", ArgumentSemantic.Copy)]
		NSTimeZone LocalTimeZone { get; }

		[Export ("secondsFromGMT")]
		nint GetSecondsFromGMT { get; }

		[Export ("defaultTimeZone", ArgumentSemantic.Copy), Static]
		NSTimeZone DefaultTimeZone { get; set; }

		[Export ("resetSystemTimeZone"), Static]
		void ResetSystemTimeZone ();

		[Export ("systemTimeZone", ArgumentSemantic.Copy), Static]
		NSTimeZone SystemTimeZone { get; }

		[Export ("timeZoneWithAbbreviation:"), Static]
		NSTimeZone FromAbbreviation (string abbreviation);

		[Export ("knownTimeZoneNames"), Static, Internal]
		string [] _KnownTimeZoneNames { get; }

		[Export ("timeZoneDataVersion"), Static]
		string DataVersion { get; }

		[Export ("localizedName:locale:")]
		string GetLocalizedName (NSTimeZoneNameStyle style, [NullAllowed] NSLocale locale);

		[Notification]
		[Field ("NSSystemTimeZoneDidChangeNotification")]
		NSString SystemTimeZoneDidChangeNotification { get; }
	}


	[BaseType (typeof (NSObject), Name = "NSURL")]
	// init returns NIL
	[DisableDefaultCtor]
	partial interface NSUrl : NSSecureCoding, NSCopying
#if MONOMAC
	, NSPasteboardReading, NSPasteboardWriting
#endif
	, NSItemProviderWriting, NSItemProviderReading
#if IOS || MONOMAC
	, QLPreviewItem
#endif
	{
		[Deprecated (PlatformName.iOS, 9, 0, message: "Use 'NSUrlComponents' instead.")]
		[Deprecated (PlatformName.TvOS, 9, 0, message: "Use 'NSUrlComponents' instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 11, message: "Use 'NSUrlComponents' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'NSUrlComponents' instead.")]
		[Export ("initWithScheme:host:path:")]
		NativeHandle Constructor (string scheme, string host, string path);

		[DesignatedInitializer]
		[Export ("initFileURLWithPath:isDirectory:")]
		NativeHandle Constructor (string path, bool isDir);

		[Export ("initWithString:")]
		NativeHandle Constructor (string urlString);

		[DesignatedInitializer]
		[Export ("initWithString:relativeToURL:")]
		NativeHandle Constructor (string urlString, NSUrl relativeToUrl);

		[return: NullAllowed]
		[Export ("URLWithString:")]
		[Static]
		NSUrl FromString ([NullAllowed] string s);

		[Export ("URLWithString:relativeToURL:")]
		[Internal]
		[Static]
		NSUrl _FromStringRelative (string url, NSUrl relative);

		[TV (17, 0), iOS (17, 0), MacCatalyst (17, 0)]
		[Static]
		[Export ("URLWithString:encodingInvalidCharacters:")]
		[return: NullAllowed]
		NSUrl FromString (string url, bool encodingInvalidCharacters);

		[Export ("absoluteString")]
		[NullAllowed]
		string AbsoluteString { get; }

		[Export ("absoluteURL")]
		[NullAllowed]
		NSUrl AbsoluteUrl { get; }

		[Export ("baseURL")]
		[NullAllowed]
		NSUrl BaseUrl { get; }

		[Export ("fragment")]
		[NullAllowed]
		string Fragment { get; }

		[Export ("host")]
		[NullAllowed]
		string Host { get; }

		[Internal]
		[Export ("isEqual:")]
		bool IsEqual ([NullAllowed] NSUrl other);

		/// <summary>Whether this NSUrl uses the file scheme.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isFileURL")]
		bool IsFileUrl { get; }

		[Export ("isFileReferenceURL")]
		bool IsFileReferenceUrl { get; }

		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Always return 'null'. Use and parse 'Path' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Always return 'null'. Use and parse 'Path' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Always return 'null'. Use and parse 'Path' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Always return 'null'. Use and parse 'Path' instead.")]
		[Export ("parameterString")]
		[NullAllowed]
		string ParameterString { get; }

		[Export ("password")]
		[NullAllowed]
		string Password { get; }

		[Export ("path")]
		[NullAllowed]
		string Path { get; }

		[Export ("query")]
		[NullAllowed]
		string Query { get; }

		[Export ("relativePath")]
		[NullAllowed]
		string RelativePath { get; }

		[Export ("pathComponents")]
		[NullAllowed]
		string [] PathComponents { get; }

		[Export ("lastPathComponent")]
		[NullAllowed]
		string LastPathComponent { get; }

		[Export ("pathExtension")]
		[NullAllowed]
		string PathExtension { get; }

		[Export ("relativeString")]
		string RelativeString { get; }

		[Export ("resourceSpecifier")]
		[NullAllowed]
		string ResourceSpecifier { get; }

		[Export ("scheme")]
		[NullAllowed]
		string Scheme { get; }

		[Export ("user")]
		[NullAllowed]
		string User { get; }

		[Export ("standardizedURL")]
		[NullAllowed]
		NSUrl StandardizedUrl { get; }

		[Export ("URLByAppendingPathComponent:isDirectory:")]
		NSUrl Append (string pathComponent, bool isDirectory);

		[Export ("URLByAppendingPathExtension:")]
		NSUrl AppendPathExtension (string extension);

		[Export ("URLByDeletingLastPathComponent")]
		NSUrl RemoveLastPathComponent ();

		[Export ("URLByDeletingPathExtension")]
		NSUrl RemovePathExtension ();

		[MacCatalyst (13, 1)]
		[Export ("getFileSystemRepresentation:maxLength:")]
		bool GetFileSystemRepresentation (IntPtr buffer, nint maxBufferLength);

		[MacCatalyst (13, 1)]
		[Export ("fileSystemRepresentation")]
		IntPtr GetFileSystemRepresentationAsUtf8Ptr { get; }

		[MacCatalyst (13, 1)]
		[Export ("removeCachedResourceValueForKey:")]
		void RemoveCachedResourceValueForKey (NSString key);

		[MacCatalyst (13, 1)]
		[Export ("removeAllCachedResourceValues")]
		void RemoveAllCachedResourceValues ();

		[MacCatalyst (13, 1)]
		[Export ("setTemporaryResourceValue:forKey:")]
		void SetTemporaryResourceValue (NSObject value, NSString key);

		[DesignatedInitializer]
		[MacCatalyst (13, 1)]
		[Export ("initFileURLWithFileSystemRepresentation:isDirectory:relativeToURL:")]
		NativeHandle Constructor (IntPtr ptrUtf8path, bool isDir, [NullAllowed] NSUrl baseURL);

		[Static, Export ("fileURLWithFileSystemRepresentation:isDirectory:relativeToURL:")]
		[MacCatalyst (13, 1)]
		NSUrl FromUTF8Pointer (IntPtr ptrUtf8path, bool isDir, [NullAllowed] NSUrl baseURL);
#if !WATCHOS

		/* These methods come from NURL_AppKitAdditions */
		/// <param name="pasteboard">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("URLFromPasteboard:")]
		[Static]
		[return: NullAllowed]
		NSUrl FromPasteboard (NSPasteboard pasteboard);

#endif
#if !WATCHOS

		/// <param name="pasteboard">To be added.</param>
		///         <summary>To be added.</summary>
		///         <remarks>To be added.</remarks>
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("writeToPasteboard:")]
		void WriteToPasteboard (NSPasteboard pasteboard);

#endif

		[Export ("bookmarkDataWithContentsOfURL:error:")]
		[Static]
		NSData GetBookmarkData (NSUrl bookmarkFileUrl, out NSError error);

		[Export ("URLByResolvingBookmarkData:options:relativeToURL:bookmarkDataIsStale:error:")]
		[Static]
		NSUrl FromBookmarkData (NSData data, NSUrlBookmarkResolutionOptions options, [NullAllowed] NSUrl relativeToUrl, out bool isStale, out NSError error);

		[Export ("writeBookmarkData:toURL:options:error:")]
		[Static]
		bool WriteBookmarkData (NSData data, NSUrl bookmarkFileUrl, NSUrlBookmarkCreationOptions options, out NSError error);

		[Export ("filePathURL")]
		[NullAllowed]
		NSUrl FilePathUrl { get; }

		[Export ("fileReferenceURL")]
		[NullAllowed]
		NSUrl FileReferenceUrl { get; }

		[Export ("getResourceValue:forKey:error:"), Internal]
		bool GetResourceValue (out NSObject value, NSString key, out NSError error);

		[Export ("resourceValuesForKeys:error:")]
		NSDictionary GetResourceValues (NSString [] keys, out NSError error);

		[Export ("setResourceValue:forKey:error:"), Internal]
		bool SetResourceValue (NSObject value, NSString key, out NSError error);

		[Export ("port"), Internal]
		[NullAllowed]
		NSNumber PortNumber { get; }

		/// <summary>Represents the value associated with the constant NSURLNameKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLNameKey")]
		NSString NameKey { get; }

		/// <summary>Represents the value associated with the constant NSURLLocalizedNameKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLLocalizedNameKey")]
		NSString LocalizedNameKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsRegularFileKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsRegularFileKey")]
		NSString IsRegularFileKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsDirectoryKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsDirectoryKey")]
		NSString IsDirectoryKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsSymbolicLinkKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsSymbolicLinkKey")]
		NSString IsSymbolicLinkKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsVolumeKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsVolumeKey")]
		NSString IsVolumeKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsPackageKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsPackageKey")]
		NSString IsPackageKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsSystemImmutableKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsSystemImmutableKey")]
		NSString IsSystemImmutableKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsUserImmutableKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsUserImmutableKey")]
		NSString IsUserImmutableKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsHiddenKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsHiddenKey")]
		NSString IsHiddenKey { get; }

		/// <summary>Represents the value associated with the constant NSURLHasHiddenExtensionKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLHasHiddenExtensionKey")]
		NSString HasHiddenExtensionKey { get; }

		/// <summary>Represents the value associated with the constant NSURLCreationDateKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLCreationDateKey")]
		NSString CreationDateKey { get; }

		/// <summary>Represents the value associated with the constant NSURLContentAccessDateKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLContentAccessDateKey")]
		NSString ContentAccessDateKey { get; }

		/// <summary>Represents the value associated with the constant NSURLContentModificationDateKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLContentModificationDateKey")]
		NSString ContentModificationDateKey { get; }

		/// <summary>Represents the value associated with the constant NSURLAttributeModificationDateKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLAttributeModificationDateKey")]
		NSString AttributeModificationDateKey { get; }

		/// <summary>Represents the value associated with the constant NSURLLinkCountKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLLinkCountKey")]
		NSString LinkCountKey { get; }

		/// <summary>Represents the value associated with the constant NSURLParentDirectoryURLKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLParentDirectoryURLKey")]
		NSString ParentDirectoryURLKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeURLKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeURLKey")]
		NSString VolumeURLKey { get; }

		/// <summary>Represents the value associated with the constant NSURLTypeIdentifierKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.iOS, 14, 0, message: "Use 'ContentTypeKey' instead.")]
		[Deprecated (PlatformName.TvOS, 14, 0, message: "Use 'ContentTypeKey' instead.")]
		[Deprecated (PlatformName.MacOSX, 11, 0, message: "Use 'ContentTypeKey' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 14, 0, message: "Use 'ContentTypeKey' instead.")]
		[Field ("NSURLTypeIdentifierKey")]
		NSString TypeIdentifierKey { get; }

		/// <summary>Represents the value associated with the constant NSURLLocalizedTypeDescriptionKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLLocalizedTypeDescriptionKey")]
		NSString LocalizedTypeDescriptionKey { get; }

		/// <summary>Represents the value associated with the constant NSURLLabelNumberKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLLabelNumberKey")]
		NSString LabelNumberKey { get; }

		/// <summary>Represents the value associated with the constant NSURLLabelColorKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLLabelColorKey")]
		NSString LabelColorKey { get; }

		/// <summary>Represents the value associated with the constant NSURLLocalizedLabelKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLLocalizedLabelKey")]
		NSString LocalizedLabelKey { get; }

		/// <summary>Represents the value associated with the constant NSURLEffectiveIconKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLEffectiveIconKey")]
		NSString EffectiveIconKey { get; }

		/// <summary>Represents the value associated with the constant NSURLCustomIconKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLCustomIconKey")]
		NSString CustomIconKey { get; }

		/// <summary>Represents the value associated with the constant NSURLFileSizeKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileSizeKey")]
		NSString FileSizeKey { get; }

		/// <summary>Represents the value associated with the constant NSURLFileAllocatedSizeKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileAllocatedSizeKey")]
		NSString FileAllocatedSizeKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsAliasFileKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsAliasFileKey")]
		NSString IsAliasFileKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeLocalizedFormatDescriptionKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeLocalizedFormatDescriptionKey")]
		NSString VolumeLocalizedFormatDescriptionKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeTotalCapacityKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeTotalCapacityKey")]
		NSString VolumeTotalCapacityKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeAvailableCapacityKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeAvailableCapacityKey")]
		NSString VolumeAvailableCapacityKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeResourceCountKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeResourceCountKey")]
		NSString VolumeResourceCountKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsPersistentIDsKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsPersistentIDsKey")]
		NSString VolumeSupportsPersistentIDsKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsSymbolicLinksKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsSymbolicLinksKey")]
		NSString VolumeSupportsSymbolicLinksKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsHardLinksKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsHardLinksKey")]
		NSString VolumeSupportsHardLinksKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsJournalingKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsJournalingKey")]
		NSString VolumeSupportsJournalingKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeIsJournalingKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeIsJournalingKey")]
		NSString VolumeIsJournalingKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsSparseFilesKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsSparseFilesKey")]
		NSString VolumeSupportsSparseFilesKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsZeroRunsKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsZeroRunsKey")]
		NSString VolumeSupportsZeroRunsKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsCaseSensitiveNamesKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsCaseSensitiveNamesKey")]
		NSString VolumeSupportsCaseSensitiveNamesKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsCasePreservedNamesKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsCasePreservedNamesKey")]
		NSString VolumeSupportsCasePreservedNamesKey { get; }

		// 5.0 Additions
		/// <summary>Represents the value associated with the constant NSURLKeysOfUnsetValuesKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLKeysOfUnsetValuesKey")]
		NSString KeysOfUnsetValuesKey { get; }

		/// <summary>Represents the value associated with the constant NSURLFileResourceIdentifierKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileResourceIdentifierKey")]
		NSString FileResourceIdentifierKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeIdentifierKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeIdentifierKey")]
		NSString VolumeIdentifierKey { get; }

		/// <summary>Represents the value associated with the constant NSURLPreferredIOBlockSizeKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLPreferredIOBlockSizeKey")]
		NSString PreferredIOBlockSizeKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsReadableKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsReadableKey")]
		NSString IsReadableKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsWritableKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsWritableKey")]
		NSString IsWritableKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsExecutableKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsExecutableKey")]
		NSString IsExecutableKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsMountTriggerKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsMountTriggerKey")]
		NSString IsMountTriggerKey { get; }

		/// <summary>Represents the value associated with the constant NSURLFileSecurityKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileSecurityKey")]
		NSString FileSecurityKey { get; }

		/// <summary>Represents the value associated with the constant NSURLFileResourceTypeKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileResourceTypeKey")]
		NSString FileResourceTypeKey { get; }

		[TV (16, 4), iOS (16, 4)]
		[MacCatalyst (16, 4)]
		[Field ("NSURLFileIdentifierKey")]
		NSString FileIdentifierKey { get; }

		/// <summary>Represents the value associated with the constant NSURLFileResourceTypeNamedPipe</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileResourceTypeNamedPipe")]
		NSString FileResourceTypeNamedPipe { get; }

		/// <summary>Represents the value associated with the constant NSURLFileResourceTypeCharacterSpecial</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileResourceTypeCharacterSpecial")]
		NSString FileResourceTypeCharacterSpecial { get; }

		/// <summary>Represents the value associated with the constant NSURLFileResourceTypeDirectory</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileResourceTypeDirectory")]
		NSString FileResourceTypeDirectory { get; }

		/// <summary>Represents the value associated with the constant NSURLFileResourceTypeBlockSpecial</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileResourceTypeBlockSpecial")]
		NSString FileResourceTypeBlockSpecial { get; }

		/// <summary>Represents the value associated with the constant NSURLFileResourceTypeRegular</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileResourceTypeRegular")]
		NSString FileResourceTypeRegular { get; }

		/// <summary>Represents the value associated with the constant NSURLFileResourceTypeSymbolicLink</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileResourceTypeSymbolicLink")]
		NSString FileResourceTypeSymbolicLink { get; }

		/// <summary>Represents the value associated with the constant NSURLFileResourceTypeSocket</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileResourceTypeSocket")]
		NSString FileResourceTypeSocket { get; }

		/// <summary>Represents the value associated with the constant NSURLFileResourceTypeUnknown</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLFileResourceTypeUnknown")]
		NSString FileResourceTypeUnknown { get; }

		/// <summary>Represents the value associated with the constant NSURLTotalFileSizeKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLTotalFileSizeKey")]
		NSString TotalFileSizeKey { get; }

		/// <summary>Represents the value associated with the constant NSURLTotalFileAllocatedSizeKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLTotalFileAllocatedSizeKey")]
		NSString TotalFileAllocatedSizeKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsRootDirectoryDatesKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsRootDirectoryDatesKey")]
		NSString VolumeSupportsRootDirectoryDatesKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsVolumeSizesKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsVolumeSizesKey")]
		NSString VolumeSupportsVolumeSizesKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsRenamingKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsRenamingKey")]
		NSString VolumeSupportsRenamingKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsAdvisoryFileLockingKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsAdvisoryFileLockingKey")]
		NSString VolumeSupportsAdvisoryFileLockingKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeSupportsExtendedSecurityKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeSupportsExtendedSecurityKey")]
		NSString VolumeSupportsExtendedSecurityKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeIsBrowsableKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeIsBrowsableKey")]
		NSString VolumeIsBrowsableKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeMaximumFileSizeKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeMaximumFileSizeKey")]
		NSString VolumeMaximumFileSizeKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeIsEjectableKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeIsEjectableKey")]
		NSString VolumeIsEjectableKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeIsRemovableKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeIsRemovableKey")]
		NSString VolumeIsRemovableKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeIsInternalKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeIsInternalKey")]
		NSString VolumeIsInternalKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeIsAutomountedKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeIsAutomountedKey")]
		NSString VolumeIsAutomountedKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeIsLocalKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeIsLocalKey")]
		NSString VolumeIsLocalKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeIsReadOnlyKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeIsReadOnlyKey")]
		NSString VolumeIsReadOnlyKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeCreationDateKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeCreationDateKey")]
		NSString VolumeCreationDateKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeURLForRemountingKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeURLForRemountingKey")]
		NSString VolumeURLForRemountingKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeUUIDStringKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeUUIDStringKey")]
		NSString VolumeUUIDStringKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeNameKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeNameKey")]
		NSString VolumeNameKey { get; }

		/// <summary>Represents the value associated with the constant NSURLVolumeLocalizedNameKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLVolumeLocalizedNameKey")]
		NSString VolumeLocalizedNameKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLVolumeIsEncryptedKey")]
		NSString VolumeIsEncryptedKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLVolumeIsRootFileSystemKey")]
		NSString VolumeIsRootFileSystemKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLVolumeSupportsCompressionKey")]
		NSString VolumeSupportsCompressionKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLVolumeSupportsFileCloningKey")]
		NSString VolumeSupportsFileCloningKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLVolumeSupportsSwapRenamingKey")]
		NSString VolumeSupportsSwapRenamingKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLVolumeSupportsExclusiveRenamingKey")]
		NSString VolumeSupportsExclusiveRenamingKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLVolumeSupportsImmutableFilesKey")]
		NSString VolumeSupportsImmutableFilesKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLVolumeSupportsAccessPermissionsKey")]
		NSString VolumeSupportsAccessPermissionsKey { get; }

		[TV (14, 0), iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Field ("NSURLVolumeSupportsFileProtectionKey")]
		NSString VolumeSupportsFileProtectionKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoTV]
		[MacCatalyst (13, 1)]
		[Field ("NSURLVolumeAvailableCapacityForImportantUsageKey")]
		NSString VolumeAvailableCapacityForImportantUsageKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoTV]
		[MacCatalyst (13, 1)]
		[Field ("NSURLVolumeAvailableCapacityForOpportunisticUsageKey")]
		NSString VolumeAvailableCapacityForOpportunisticUsageKey { get; }

		[TV (16, 4), iOS (16, 4)]
		[MacCatalyst (16, 4)]
		[Field ("NSURLVolumeTypeNameKey")]
		NSString VolumeTypeNameKey { get; }

		[TV (16, 4), iOS (16, 4)]
		[MacCatalyst (16, 4)]
		[Field ("NSURLVolumeSubtypeKey")]
		NSString VolumeSubtypeKey { get; }

		[TV (16, 4), iOS (16, 4)]
		[MacCatalyst (16, 4)]
		[Field ("NSURLVolumeMountFromLocationKey")]
		NSString VolumeMountFromLocationKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsUbiquitousItemKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsUbiquitousItemKey")]
		NSString IsUbiquitousItemKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemHasUnresolvedConflictsKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLUbiquitousItemHasUnresolvedConflictsKey")]
		NSString UbiquitousItemHasUnresolvedConflictsKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemIsDownloadedKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLUbiquitousItemIsDownloadedKey")]
		NSString UbiquitousItemIsDownloadedKey { get; }

		/// <summary>Developers should not use this deprecated property. </summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLUbiquitousItemIsDownloadingKey")]
		[Deprecated (PlatformName.iOS, 7, 0)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1)]
		NSString UbiquitousItemIsDownloadingKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemIsUploadedKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLUbiquitousItemIsUploadedKey")]
		NSString UbiquitousItemIsUploadedKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemIsUploadingKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLUbiquitousItemIsUploadingKey")]
		NSString UbiquitousItemIsUploadingKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemPercentDownloadedKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLUbiquitousItemPercentDownloadedKey")]
		[Deprecated (PlatformName.iOS, 6, 0, message: "Use 'NSMetadataQuery.UbiquitousItemPercentDownloadedKey' on 'NSMetadataItem' instead.")]
		[Deprecated (PlatformName.TvOS, 9, 0, message: "Use 'NSMetadataQuery.UbiquitousItemPercentDownloadedKey' on 'NSMetadataItem' instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 8, message: "Use 'NSMetadataQuery.UbiquitousItemPercentDownloadedKey' on 'NSMetadataItem' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'NSMetadataQuery.UbiquitousItemPercentDownloadedKey' on 'NSMetadataItem' instead.")]
		NSString UbiquitousItemPercentDownloadedKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemPercentUploadedKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.iOS, 6, 0, message: "Use 'NSMetadataQuery.UbiquitousItemPercentUploadedKey' on 'NSMetadataItem' instead.")]
		[Deprecated (PlatformName.TvOS, 9, 0, message: "Use 'NSMetadataQuery.UbiquitousItemPercentUploadedKey' on 'NSMetadataItem' instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 8, message: "Use 'NSMetadataQuery.UbiquitousItemPercentUploadedKey' on 'NSMetadataItem' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'NSMetadataQuery.UbiquitousItemPercentUploadedKey' on 'NSMetadataItem' instead.")]
		[Field ("NSURLUbiquitousItemPercentUploadedKey")]
		NSString UbiquitousItemPercentUploadedKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoTV]
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousItemIsSharedKey")]
		NSString UbiquitousItemIsSharedKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoTV]
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousSharedItemCurrentUserRoleKey")]
		NSString UbiquitousSharedItemCurrentUserRoleKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoTV]
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousSharedItemCurrentUserPermissionsKey")]
		NSString UbiquitousSharedItemCurrentUserPermissionsKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoTV]
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousSharedItemOwnerNameComponentsKey")]
		NSString UbiquitousSharedItemOwnerNameComponentsKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoTV]
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousSharedItemMostRecentEditorNameComponentsKey")]
		NSString UbiquitousSharedItemMostRecentEditorNameComponentsKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoTV]
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousSharedItemRoleOwner")]
		NSString UbiquitousSharedItemRoleOwner { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoTV]
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousSharedItemRoleParticipant")]
		NSString UbiquitousSharedItemRoleParticipant { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoTV]
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousSharedItemPermissionsReadOnly")]
		NSString UbiquitousSharedItemPermissionsReadOnly { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoTV]
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousSharedItemPermissionsReadWrite")]
		NSString UbiquitousSharedItemPermissionsReadWrite { get; }

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSURLUbiquitousItemSupportedSyncControlsKey")]
		NSString UbiquitousItemSupportedSyncControlsKey { get; }

		[MacCatalyst (26, 0), TV (26, 0), Mac (26, 0), iOS (26, 0)]
		[Field ("NSURLUbiquitousItemIsSyncPausedKey")]
		NSString UbiquitousItemIsSyncPausedKey { get; }

		/// <summary>Represents the value associated with the constant NSURLIsExcludedFromBackupKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLIsExcludedFromBackupKey")]
		NSString IsExcludedFromBackupKey { get; }

		[Export ("bookmarkDataWithOptions:includingResourceValuesForKeys:relativeToURL:error:")]
		NSData CreateBookmarkData (NSUrlBookmarkCreationOptions options, [NullAllowed] string [] resourceValues, [NullAllowed] NSUrl relativeUrl, out NSError error);

		[Export ("initByResolvingBookmarkData:options:relativeToURL:bookmarkDataIsStale:error:")]
		NativeHandle Constructor (NSData bookmarkData, NSUrlBookmarkResolutionOptions resolutionOptions, [NullAllowed] NSUrl relativeUrl, out bool bookmarkIsStale, out NSError error);

		/// <summary>Represents the value associated with the constant NSURLPathKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLPathKey")]
		NSString PathKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemDownloadingStatusKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousItemDownloadingStatusKey")]
		NSString UbiquitousItemDownloadingStatusKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemDownloadingErrorKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousItemDownloadingErrorKey")]
		NSString UbiquitousItemDownloadingErrorKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemUploadingErrorKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousItemUploadingErrorKey")]
		NSString UbiquitousItemUploadingErrorKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemDownloadingStatusNotDownloaded</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousItemDownloadingStatusNotDownloaded")]
		NSString UbiquitousItemDownloadingStatusNotDownloaded { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemDownloadingStatusDownloaded</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousItemDownloadingStatusDownloaded")]
		NSString UbiquitousItemDownloadingStatusDownloaded { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemDownloadingStatusCurrent</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousItemDownloadingStatusCurrent")]
		NSString UbiquitousItemDownloadingStatusCurrent { get; }

		[MacCatalyst (13, 1)]
		[Export ("startAccessingSecurityScopedResource")]
		bool StartAccessingSecurityScopedResource ();

		[MacCatalyst (13, 1)]
		[Export ("stopAccessingSecurityScopedResource")]
		void StopAccessingSecurityScopedResource ();

		[MacCatalyst (13, 1)]
		[Static, Export ("URLByResolvingAliasFileAtURL:options:error:")]
		NSUrl ResolveAlias (NSUrl aliasFileUrl, NSUrlBookmarkResolutionOptions options, out NSError error);

		[Static, Export ("fileURLWithPathComponents:")]
		NSUrl CreateFileUrl (string [] pathComponents);

		/// <summary>Represents the value associated with the constant NSURLAddedToDirectoryDateKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLAddedToDirectoryDateKey")]
		NSString AddedToDirectoryDateKey { get; }

		/// <summary>Represents the value associated with the constant NSURLDocumentIdentifierKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLDocumentIdentifierKey")]
		NSString DocumentIdentifierKey { get; }

		/// <summary>Represents the value associated with the constant NSURLGenerationIdentifierKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLGenerationIdentifierKey")]
		NSString GenerationIdentifierKey { get; }

		/// <summary>Represents the value associated with the constant NSURLThumbnailDictionaryKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLThumbnailDictionaryKey")]
		NSString ThumbnailDictionaryKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemContainerDisplayNameKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousItemContainerDisplayNameKey")]
		NSString UbiquitousItemContainerDisplayNameKey { get; }

		[TV (14, 5), iOS (14, 5)]
		[MacCatalyst (14, 5)]
		[Field ("NSURLUbiquitousItemIsExcludedFromSyncKey")]
		NSString UbiquitousItemIsExcludedFromSyncKey { get; }

		/// <summary>Represents the value associated with the constant NSURLUbiquitousItemDownloadRequestedKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLUbiquitousItemDownloadRequestedKey")]
		NSString UbiquitousItemDownloadRequestedKey { get; }

		//
		// iOS 9.0/osx 10.11 additions
		//
		[DesignatedInitializer]
		[MacCatalyst (13, 1)]
		[Export ("initFileURLWithPath:isDirectory:relativeToURL:")]
		NativeHandle Constructor (string path, bool isDir, [NullAllowed] NSUrl relativeToUrl);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("fileURLWithPath:isDirectory:relativeToURL:")]
		NSUrl CreateFileUrl (string path, bool isDir, [NullAllowed] NSUrl relativeToUrl);

		[Static]
		[Export ("fileURLWithPath:isDirectory:")]
		NSUrl CreateFileUrl (string path, bool isDir);

		[Static]
		[Export ("fileURLWithPath:relativeToURL:")]
		NSUrl CreateFileUrl (string path, [NullAllowed] NSUrl relativeToUrl);

		[Static]
		[Export ("fileURLWithPath:")]
		NSUrl CreateFileUrl (string path);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("URLWithDataRepresentation:relativeToURL:")]
		NSUrl CreateWithDataRepresentation (NSData data, [NullAllowed] NSUrl relativeToUrl);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("absoluteURLWithDataRepresentation:relativeToURL:")]
		NSUrl CreateAbsoluteUrlWithDataRepresentation (NSData data, [NullAllowed] NSUrl relativeToUrl);

		[MacCatalyst (13, 1)]
		[Export ("dataRepresentation", ArgumentSemantic.Copy)]
		NSData DataRepresentation { get; }

		[MacCatalyst (13, 1)]
		[Export ("hasDirectoryPath")]
		bool HasDirectoryPath { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLIsApplicationKey")]
		NSString IsApplicationKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLFileProtectionKey")]
		NSString FileProtectionKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLFileProtectionNone")]
		NSString FileProtectionNone { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLFileProtectionComplete")]
		NSString FileProtectionComplete { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLFileProtectionCompleteUnlessOpen")]
		NSString FileProtectionCompleteUnlessOpen { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLFileProtectionCompleteUntilFirstUserAuthentication")]
		NSString FileProtectionCompleteUntilFirstUserAuthentication { get; }

		[TV (17, 0), NoMac, iOS (17, 0), MacCatalyst (17, 0)]
		[Field ("NSURLFileProtectionCompleteWhenUserInactive")]
		NSString FileProtectionCompleteWhenUserInactive { get; }

		[TV (17, 0), iOS (17, 0), MacCatalyst (17, 0)]
		[Field ("NSURLDirectoryEntryCountKey")]
		NSString DirectoryEntryCountKey { get; }

		[TV (14, 0)]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Field ("NSURLContentTypeKey")]
		NSString ContentTypeKey { get; }

		[TV (14, 0)]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Field ("NSURLFileContentIdentifierKey")]
		NSString FileContentIdentifierKey { get; }

		[TV (14, 0)]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Field ("NSURLIsPurgeableKey")]
		NSString IsPurgeableKey { get; }

		[TV (14, 0)]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Field ("NSURLIsSparseKey")]
		NSString IsSparseKey { get; }

		[TV (14, 0)]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Field ("NSURLMayHaveExtendedAttributesKey")]
		NSString MayHaveExtendedAttributesKey { get; }

		[TV (14, 0)]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Field ("NSURLMayShareFileContentKey")]
		NSString MayShareFileContentKey { get; }

		// From the NSItemProviderReading protocol
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("readableTypeIdentifiersForItemProvider", ArgumentSemantic.Copy)]
		new string [] ReadableTypeIdentifiers { get; }

		// From the NSItemProviderReading protocol
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("objectWithItemProviderData:typeIdentifier:error:")]
		[return: NullAllowed]
		new NSUrl GetObject (NSData data, string typeIdentifier, [NullAllowed] out NSError outError);

		// From the NSItemProviderWriting protocol
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("writableTypeIdentifiersForItemProvider", ArgumentSemantic.Copy)]
		new string [] WritableTypeIdentifiers { get; }
	}


	/// <summary>Defines static methods defining character sets for various subcomponents of a <see cref="Foundation.NSUrl" />.</summary>
	/// <remarks>To be added.</remarks>
	[Category, BaseType (typeof (NSCharacterSet))]
	partial interface NSUrlUtilities_NSCharacterSet {
		/// <summary>The <see cref="Foundation.NSCharacterSet" /> for characters allowed in a URL user component.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Static, Export ("URLUserAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlUserAllowedCharacterSet { get; }

		/// <summary>The <see cref="Foundation.NSCharacterSet" /> for characters allowed in a URL password component.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Static, Export ("URLPasswordAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlPasswordAllowedCharacterSet { get; }

		/// <summary>The <see cref="Foundation.NSCharacterSet" /> for characters allowed in a host URL.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Static, Export ("URLHostAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlHostAllowedCharacterSet { get; }

		/// <summary>The <see cref="Foundation.NSCharacterSet" /> for characters allowed in a URL path component.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Static, Export ("URLPathAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlPathAllowedCharacterSet { get; }

		/// <summary>The <see cref="Foundation.NSCharacterSet" /> for characters allowed in a URL query component.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Static, Export ("URLQueryAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlQueryAllowedCharacterSet { get; }

		/// <summary>The <see cref="Foundation.NSCharacterSet" /> for characters allowed in a fragment URL component.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Static, Export ("URLFragmentAllowedCharacterSet", ArgumentSemantic.Copy)]
		NSCharacterSet UrlFragmentAllowedCharacterSet { get; }
	}


	[BaseType (typeof (NSObject), Name = "NSURLCache")]
	interface NSUrlCache {
		[Export ("sharedURLCache", ArgumentSemantic.Strong), Static]
		NSUrlCache SharedCache { get; set; }

		/// <param name="memoryCapacity">To be added.</param>
		/// <param name="diskCapacity">To be added.</param>
		/// <param name="diskPath">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use the overload that accepts an 'NSUrl' parameter instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use the overload that accepts an 'NSUrl' parameter instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use the overload that accepts an 'NSUrl' parameter instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use the overload that accepts an 'NSUrl' parameter instead.")]
		[Export ("initWithMemoryCapacity:diskCapacity:diskPath:")]
		NativeHandle Constructor (nuint memoryCapacity, nuint diskCapacity, [NullAllowed] string diskPath);

		[MacCatalyst (13, 1)]
		[Export ("initWithMemoryCapacity:diskCapacity:directoryURL:")]
		NativeHandle Constructor (nuint memoryCapacity, nuint diskCapacity, [NullAllowed] NSUrl directoryUrl);

		[Export ("cachedResponseForRequest:")]
		NSCachedUrlResponse CachedResponseForRequest (NSUrlRequest request);

		[Export ("storeCachedResponse:forRequest:")]
		void StoreCachedResponse (NSCachedUrlResponse cachedResponse, NSUrlRequest forRequest);

		[Export ("removeCachedResponseForRequest:")]
		void RemoveCachedResponse (NSUrlRequest request);

		[Export ("removeAllCachedResponses")]
		void RemoveAllCachedResponses ();

		[Export ("memoryCapacity")]
		nuint MemoryCapacity { get; set; }

		[Export ("diskCapacity")]
		nuint DiskCapacity { get; set; }

		[Export ("currentMemoryUsage")]
		nuint CurrentMemoryUsage { get; }

		[Export ("currentDiskUsage")]
		nuint CurrentDiskUsage { get; }

		[MacCatalyst (13, 1)]
		[Export ("removeCachedResponsesSinceDate:")]
		void RemoveCachedResponsesSinceDate (NSDate date);

		[MacCatalyst (13, 1)]
		[Export ("storeCachedResponse:forDataTask:")]
		void StoreCachedResponse (NSCachedUrlResponse cachedResponse, NSUrlSessionDataTask dataTask);

		[MacCatalyst (13, 1)]
		[Export ("getCachedResponseForDataTask:completionHandler:")]
		[Async (XmlDocs = """
			<param name="dataTask">To be added.</param>
			<summary>To be added.</summary>
			<returns>
			          <para class="improve-task-t-return-type-description">A task that represents the asynchronous GetCachedResponse operation.  The value of the TResult parameter is of type System.Action&lt;Foundation.NSCachedUrlResponse&gt;.</para>
			        </returns>
			<remarks>
			          <para copied="true">The GetCachedResponseAsync method is suitable to be used with C# async by returning control to the caller with a Task representing the operation.</para>
			          <para copied="true">To be added.</para>
			        </remarks>
			""")]
		void GetCachedResponse (NSUrlSessionDataTask dataTask, Action<NSCachedUrlResponse> completionHandler);

		[MacCatalyst (13, 1)]
		[Export ("removeCachedResponseForDataTask:")]
		void RemoveCachedResponse (NSUrlSessionDataTask dataTask);
	}


	[BaseType (typeof (NSObject), Name = "NSURLAuthenticationChallenge")]
	// 'init' returns NIL
	[DisableDefaultCtor]
	interface NSUrlAuthenticationChallenge : NSSecureCoding {
#if !WATCHOS
		/// <param name="space">To be added.</param>
		/// <param name="credential">To be added.</param>
		/// <param name="previousFailureCount">To be added.</param>
		/// <param name="response">To be added.</param>
		/// <param name="error">
		///           <para>To be added.</para>
		///           <para tool="nullallowed">This parameter can be <see langword="null" />.</para>
		///         </param>
		/// <param name="sender">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("initWithProtectionSpace:proposedCredential:previousFailureCount:failureResponse:error:sender:")]
		NativeHandle Constructor (NSUrlProtectionSpace space, NSUrlCredential credential, nint previousFailureCount, [NullAllowed] NSUrlResponse response, [NullAllowed] NSError error, NSUrlConnection sender);

#endif
#if !WATCHOS

		[Export ("initWithAuthenticationChallenge:sender:")]
		NativeHandle Constructor (NSUrlAuthenticationChallenge challenge, NSUrlConnection sender);

#endif

		[Export ("protectionSpace")]
		NSUrlProtectionSpace ProtectionSpace { get; }

		[Export ("proposedCredential")]
		NSUrlCredential ProposedCredential { get; }

		[Export ("previousFailureCount")]
		nint PreviousFailureCount { get; }

		[Export ("failureResponse")]
		NSUrlResponse FailureResponse { get; }

		[Export ("error")]
		NSError Error { get; }

#if XAMCORE_5_0
		[Export ("sender")]
		INSUrlAuthenticationChallengeSender Sender { get; }

#if !XAMCORE_6_0
		[Obsolete ("Use 'Sender' instead.")]
		[Export ("sender")]
		[Sealed]
		INSUrlAuthenticationChallengeSender SenderObject { get; }
#endif
#else
#if !WATCHOS
		[Obsolete ("Use 'SenderObject' instead, this property has the wrong type.")]
		[Export ("sender")]
		NSUrlConnection Sender { get; }

#endif

		[Export ("sender")]
		[Sealed]
		INSUrlAuthenticationChallengeSender SenderObject { get; }
#endif
	}


	interface INSUrlAuthenticationChallengeSender { }


	[Protocol (Name = "NSURLAuthenticationChallengeSender")]
	interface NSUrlAuthenticationChallengeSender {
		[Abstract]
		[Export ("useCredential:forAuthenticationChallenge:")]
		void UseCredential (NSUrlCredential credential, NSUrlAuthenticationChallenge challenge);

		[Abstract]
		[Export ("continueWithoutCredentialForAuthenticationChallenge:")]
		void ContinueWithoutCredential (NSUrlAuthenticationChallenge challenge);

		[Abstract]
		[Export ("cancelAuthenticationChallenge:")]
		void CancelAuthenticationChallenge (NSUrlAuthenticationChallenge challenge);

		[Export ("performDefaultHandlingForAuthenticationChallenge:")]
		void PerformDefaultHandling (NSUrlAuthenticationChallenge challenge);

		[Export ("rejectProtectionSpaceAndContinueWithChallenge:")]
		void RejectProtectionSpaceAndContinue (NSUrlAuthenticationChallenge challenge);
	}


	[BaseType (typeof (NSObject), Name = "NSURLCredential")]
	// crash when calling NSObject.get_Description (and likely other selectors)
	[DisableDefaultCtor]
	interface NSUrlCredential : NSSecureCoding, NSCopying {

		[Export ("initWithTrust:")]
		NativeHandle Constructor (SecTrust trust);

		[Export ("persistence")]
		NSUrlCredentialPersistence Persistence { get; }

		[Export ("initWithUser:password:persistence:")]
		NativeHandle Constructor (string user, string password, NSUrlCredentialPersistence persistence);

		[Static]
		[Export ("credentialWithUser:password:persistence:")]
		NSUrlCredential Create (string user, string password, NSUrlCredentialPersistence persistence);

#if !XAMCORE_5_0
		[Obsolete ("Use 'Create' instead.")]
		[EditorBrowsable (EditorBrowsableState.Never)]
		[Wrap ("Create (user, password, persistence)")]
		[Static]
		NSUrlCredential FromUserPasswordPersistance (string user, string password, NSUrlCredentialPersistence persistence);
#endif

		[Export ("user")]
		string User { get; }

		[Export ("password")]
		string Password { get; }

		[Export ("hasPassword")]
		bool HasPassword { get; }

		[Internal]
		[Export ("initWithIdentity:certificates:persistence:")]
		NativeHandle _InitWithIdentity (IntPtr identity, IntPtr certificates, NSUrlCredentialPersistence persistence);

		[Static]
		[Internal]
		[Export ("credentialWithIdentity:certificates:persistence:")]
		NSUrlCredential FromIdentityCertificatesPersistanceInternal (IntPtr identity, IntPtr certificates, NSUrlCredentialPersistence persistence);

		[Internal]
		[Export ("identity")]
		IntPtr Identity { get; }

		[Export ("certificates")]
		SecCertificate [] Certificates { get; }

		[Internal]
		[Static]
		[Export ("credentialForTrust:")]
		NSUrlCredential FromTrust (IntPtr trust);
	}


	[BaseType (typeof (NSObject), Name = "NSURLCredentialStorage")]
	// init returns NIL -> SharedCredentialStorage
	[DisableDefaultCtor]
	interface NSUrlCredentialStorage {
		[Static]
		[Export ("sharedCredentialStorage", ArgumentSemantic.Strong)]
		NSUrlCredentialStorage SharedCredentialStorage { get; }

		[Export ("credentialsForProtectionSpace:")]
		NSDictionary GetCredentials (NSUrlProtectionSpace forProtectionSpace);

		[Export ("allCredentials")]
		NSDictionary AllCredentials { get; }

		[Export ("setCredential:forProtectionSpace:")]
		void SetCredential (NSUrlCredential credential, NSUrlProtectionSpace forProtectionSpace);

		[Export ("removeCredential:forProtectionSpace:")]
		void RemoveCredential (NSUrlCredential credential, NSUrlProtectionSpace forProtectionSpace);

		[Export ("defaultCredentialForProtectionSpace:")]
		NSUrlCredential GetDefaultCredential (NSUrlProtectionSpace forProtectionSpace);

		[Export ("setDefaultCredential:forProtectionSpace:")]
		void SetDefaultCredential (NSUrlCredential credential, NSUrlProtectionSpace forProtectionSpace);

		[MacCatalyst (13, 1)]
		[Export ("removeCredential:forProtectionSpace:options:")]
		void RemoveCredential (NSUrlCredential credential, NSUrlProtectionSpace forProtectionSpace, NSDictionary options);

		/// <summary>Represents the value associated with the constant NSURLCredentialStorageRemoveSynchronizableCredentials</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Field ("NSURLCredentialStorageRemoveSynchronizableCredentials")]
		NSString RemoveSynchronizableCredentials { get; }

		[Deprecated (PlatformName.MacOSX, 10, 14, "This notification is never posted.")]
		[Deprecated (PlatformName.iOS, 12, 0, "This notification is never posted.")]
		[Deprecated (PlatformName.TvOS, 12, 0, "This notification is never posted.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, "This notification is never posted.")]
		[Field ("NSURLCredentialStorageChangedNotification")]
		[Notification]
		NSString ChangedNotification { get; }

		[MacCatalyst (13, 1)]
		[Async (XmlDocs = """
			<param name="protectionSpace">To be added.</param>
			<param name="task">To be added.</param>
			<summary>To be added.</summary>
			<returns>
			          <para class="improve-task-t-return-type-description">A task that represents the asynchronous GetCredentials operation.  The value of the TResult parameter is of type System.Action&lt;Foundation.NSDictionary&gt;.</para>
			        </returns>
			<remarks>To be added.</remarks>
			""")]
		[Export ("getCredentialsForProtectionSpace:task:completionHandler:")]
		void GetCredentials (NSUrlProtectionSpace protectionSpace, NSUrlSessionTask task, [NullAllowed] Action<NSDictionary> completionHandler);

		[MacCatalyst (13, 1)]
		[Export ("setCredential:forProtectionSpace:task:")]
		void SetCredential (NSUrlCredential credential, NSUrlProtectionSpace protectionSpace, NSUrlSessionTask task);

		[MacCatalyst (13, 1)]
		[Export ("removeCredential:forProtectionSpace:options:task:")]
		void RemoveCredential (NSUrlCredential credential, NSUrlProtectionSpace protectionSpace, NSDictionary options, NSUrlSessionTask task);

		[MacCatalyst (13, 1)]
		[Async (XmlDocs = """
			<param name="space">To be added.</param>
			<param name="task">To be added.</param>
			<summary>To be added.</summary>
			<returns>
			          <para class="improve-task-t-return-type-description">A task that represents the asynchronous GetDefaultCredential operation.  The value of the TResult parameter is of type System.Action&lt;Foundation.NSUrlCredential&gt;.</para>
			        </returns>
			<remarks>
			          <para copied="true">The GetDefaultCredentialAsync method is suitable to be used with C# async by returning control to the caller with a Task representing the operation.</para>
			          <para copied="true">To be added.</para>
			        </remarks>
			""")]
		[Export ("getDefaultCredentialForProtectionSpace:task:completionHandler:")]
		void GetDefaultCredential (NSUrlProtectionSpace space, NSUrlSessionTask task, [NullAllowed] Action<NSUrlCredential> completionHandler);

		[MacCatalyst (13, 1)]
		[Export ("setDefaultCredential:forProtectionSpace:task:")]
		void SetDefaultCredential (NSUrlCredential credential, NSUrlProtectionSpace protectionSpace, NSUrlSessionTask task);

	}


	/// <param name="dataTasks">To be added.</param>
	/// <param name="uploadTasks">To be added.</param>
	/// <param name="downloadTasks">To be added.</param>
	/// <summary>The delegate that serves as the completion handler for <see cref="Foundation.NSUrlSession.GetTasks(Foundation.NSUrlSessionPendingTasks)" />.</summary>
	/// <remarks>To be added.</remarks>
	delegate void NSUrlSessionPendingTasks (NSUrlSessionTask [] dataTasks, NSUrlSessionTask [] uploadTasks, NSUrlSessionTask [] downloadTasks);

	delegate void NSUrlSessionAllPendingTasks (NSUrlSessionTask [] tasks);

	/// <param name="data">Data that was received.</param>
	/// <param name="response">The object representing the response.</param>
	/// <param name="error">Error code, if any.</param>
	/// <summary>Signature for callbacks invoked by NSUrlSession for various background operations.</summary>
	/// <remarks>
	///     </remarks>
	delegate void NSUrlSessionResponse ([NullAllowed] NSData data, [NullAllowed] NSUrlResponse response, [NullAllowed] NSError error);


	/// <param name="location">To be added.</param>
	/// <param name="response">To be added.</param>
	/// <param name="error">To be added.</param>
	/// <summary>Completion handler for calls to <see cref="Foundation.NSUrlSession.CreateDownloadTask" /> and <see cref="Foundation.NSUrlSession.CreateDownloadTaskFromResumeData(Foundation.NSData,Foundation.NSUrlDownloadSessionResponse)" />.</summary>
	/// <remarks>To be added.</remarks>
	delegate void NSUrlDownloadSessionResponse ([NullAllowed] NSUrl location, [NullAllowed] NSUrlResponse response, [NullAllowed] NSError error);


	interface INSUrlSessionDelegate { }


	//
	// Some of the XxxTaskWith methods that take a completion were flagged as allowing a null in
	// 083d9cba1eb997eac5c5ded77db32180c3eef566 with comment:
	//
	// "Add missing [NullAllowed] on NSUrlSession since the
	// delegate is optional and the handler can be null when one
	// is provided (but requiring a delegate along with handlers
	// only duplicates code)"
	//
	// but Apple has flagged these as not allowing null.
	//
	// Leaving the null allowed for now.
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject), Name = "NSURLSession")]
	[DisableDefaultCtorAttribute]
	partial interface NSUrlSession {

		[Static, Export ("sharedSession", ArgumentSemantic.Strong)]
		NSUrlSession SharedSession { get; }

		[Static, Export ("sessionWithConfiguration:")]
		NSUrlSession FromConfiguration (NSUrlSessionConfiguration configuration);

		[Static, Export ("sessionWithConfiguration:delegate:delegateQueue:")]
		NSUrlSession FromWeakConfiguration (NSUrlSessionConfiguration configuration, [NullAllowed] NSObject weakDelegate, [NullAllowed] NSOperationQueue delegateQueue);

		/// <param name="configuration">To be added.</param>
		///         <param name="sessionDelegate">To be added.</param>
		///         <param name="delegateQueue">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[Static, Wrap ("FromWeakConfiguration (configuration, (NSObject) sessionDelegate, delegateQueue);")]
		NSUrlSession FromConfiguration (NSUrlSessionConfiguration configuration, INSUrlSessionDelegate sessionDelegate, [NullAllowed] NSOperationQueue delegateQueue);

		[Export ("delegateQueue", ArgumentSemantic.Retain)]
		NSOperationQueue DelegateQueue { get; }

		[Export ("delegate", ArgumentSemantic.Retain), NullAllowed]
		NSObject WeakDelegate { get; }

		/// <summary>An instance of the Foundation.INSUrlSessionDelegate model class which acts as the class delegate.</summary>
		///         <value>The instance of the Foundation.INSUrlSessionDelegate model class</value>
		///         <remarks>
		///           <para>The delegate instance assigned to this object will be used to handle events or provide data on demand to this class.</para>
		///           <para>When setting the Delegate or WeakDelegate values events will be delivered to the specified instance instead of being delivered to the C#-style events</para>
		///           <para>This is the strongly typed version of the object, developers should use the WeakDelegate property instead if they want to merely assign a class derived from NSObject that has been decorated with [Export] attributes.</para>
		///         </remarks>
		[Wrap ("WeakDelegate")]
		INSUrlSessionDelegate Delegate { get; }

		[Export ("configuration", ArgumentSemantic.Copy)]
		NSUrlSessionConfiguration Configuration { get; }

		[NullAllowed]
		[Export ("sessionDescription", ArgumentSemantic.Copy)]
		string SessionDescription { get; set; }

		[Export ("finishTasksAndInvalidate")]
		void FinishTasksAndInvalidate ();

		[Export ("invalidateAndCancel")]
		void InvalidateAndCancel ();

		[Export ("resetWithCompletionHandler:")]
		[Async (XmlDocs = """
			<summary>To be added.</summary>
			<returns>A task that represents the asynchronous Reset operation</returns>
			<remarks>To be added.</remarks>
			""")]
		void Reset (Action completionHandler);

		[Export ("flushWithCompletionHandler:")]
		[Async (XmlDocs = """
			<summary>To be added.</summary>
			<returns>A task that represents the asynchronous Flush operation</returns>
			<remarks>To be added.</remarks>
			""")]
		void Flush (Action completionHandler);

		// Fixed version (breaking change) only for NET
		[Export ("getTasksWithCompletionHandler:")]
		[Async (ResultTypeName = "NSUrlSessionActiveTasks", XmlDocs = """
			<summary>Requests the groups of pending tasks (data, upload and downloads).</summary>
			<returns>
			          <para>A task that represents the asynchronous GetTasks operation.   The value of the TResult parameter is of type <c>Action&lt;Foundation.NSUrlSessionActiveTasks&gt;</c>.</para>
			        </returns>
			<remarks>To be added.</remarks>
			""")]
		void GetTasks (NSUrlSessionPendingTasks completionHandler);

		[Export ("dataTaskWithRequest:")]
		[return: ForcedType]
		NSUrlSessionDataTask CreateDataTask (NSUrlRequest request);

		[Export ("dataTaskWithURL:")]
		[return: ForcedType]
		NSUrlSessionDataTask CreateDataTask (NSUrl url);

		[Export ("uploadTaskWithRequest:fromFile:")]
		[return: ForcedType]
		NSUrlSessionUploadTask CreateUploadTask (NSUrlRequest request, NSUrl fileURL);

		[Export ("uploadTaskWithRequest:fromData:")]
		[return: ForcedType]
		NSUrlSessionUploadTask CreateUploadTask (NSUrlRequest request, NSData bodyData);

		[Export ("uploadTaskWithStreamedRequest:")]
		[return: ForcedType]
		NSUrlSessionUploadTask CreateUploadTask (NSUrlRequest request);

		[Export ("downloadTaskWithRequest:")]
		[return: ForcedType]
		NSUrlSessionDownloadTask CreateDownloadTask (NSUrlRequest request);

		[Export ("downloadTaskWithURL:")]
		[return: ForcedType]
		NSUrlSessionDownloadTask CreateDownloadTask (NSUrl url);

		[Export ("downloadTaskWithResumeData:")]
		[return: ForcedType]
		NSUrlSessionDownloadTask CreateDownloadTask (NSData resumeData);

		[Export ("dataTaskWithRequest:completionHandler:")]
		[return: ForcedType]
		[Async (ResultTypeName = "NSUrlSessionDataTaskRequest", PostNonResultSnippet = "result.Resume ();", XmlDocs = """
			<param name="request">To be added.</param>
			<summary>To be added.</summary>
			<returns>
			          <para>A task that represents the asynchronous CreateDataTask operation.   The value of the TResult parameter is of type <c>Action&lt;Foundation.NSUrlSessionDataTaskRequest&gt;</c>.</para>
			        </returns>
			<remarks>To be added.</remarks>
			""",
			XmlDocsWithOutParameter = """
			<param name="request">To be added.</param>
			<param name="result">To be added.</param>
			<summary>To be added.</summary>
			<returns>To be added.</returns>
			<remarks>To be added.</remarks>
			""")]
		NSUrlSessionDataTask CreateDataTask (NSUrlRequest request, [NullAllowed] NSUrlSessionResponse completionHandler);

		[Export ("dataTaskWithURL:completionHandler:")]
		[return: ForcedType]
		[Async (ResultTypeName = "NSUrlSessionDataTaskRequest", PostNonResultSnippet = "result.Resume ();", XmlDocs = """
			<param name="url">To be added.</param>
			<summary>To be added.</summary>
			<returns>
			          <para>A task that represents the asynchronous CreateDataTask operation.   The value of the TResult parameter is of type <c>Action&lt;Foundation.NSUrlSessionDataTaskRequest&gt;</c>.</para>
			        </returns>
			<remarks>To be added.</remarks>
			""",
			XmlDocsWithOutParameter = """
			<param name="url">To be added.</param>
			<param name="result">To be added.</param>
			<summary>To be added.</summary>
			<returns>To be added.</returns>
			<remarks>To be added.</remarks>
			""")]
		NSUrlSessionDataTask CreateDataTask (NSUrl url, [NullAllowed] NSUrlSessionResponse completionHandler);

		[Export ("uploadTaskWithRequest:fromFile:completionHandler:")]
		[return: ForcedType]
		[Async (ResultTypeName = "NSUrlSessionDataTaskRequest", PostNonResultSnippet = "result.Resume ();", XmlDocs = """
			<param name="request">To be added.</param>
			<param name="fileURL">To be added.</param>
			<summary>To be added.</summary>
			<returns>
			          <para>A task that represents the asynchronous CreateUploadTask operation.   The value of the TResult parameter is of type <c>Action&lt;Foundation.NSUrlSessionDataTaskRequest&gt;</c>.</para>
			        </returns>
			<remarks>To be added.</remarks>
			""",
			XmlDocsWithOutParameter = """
			<param name="request">To be added.</param>
			<param name="fileURL">To be added.</param>
			<param name="result">To be added.</param>
			<summary>To be added.</summary>
			<returns>To be added.</returns>
			<remarks>To be added.</remarks>
			""")]
		NSUrlSessionUploadTask CreateUploadTask (NSUrlRequest request, NSUrl fileURL, NSUrlSessionResponse completionHandler);

		[Export ("uploadTaskWithRequest:fromData:completionHandler:")]
		[return: ForcedType]
		[Async (ResultTypeName = "NSUrlSessionDataTaskRequest", PostNonResultSnippet = "result.Resume ();", XmlDocs = """
			<param name="request">To be added.</param>
			<param name="bodyData">To be added.</param>
			<summary>To be added.</summary>
			<returns>
			          <para>A task that represents the asynchronous CreateUploadTask operation.   The value of the TResult parameter is of type <c>Action&lt;Foundation.NSUrlSessionDataTaskRequest&gt;</c>.</para>
			        </returns>
			<remarks>To be added.</remarks>
			""",
			XmlDocsWithOutParameter = """
			<param name="request">To be added.</param>
			<param name="bodyData">To be added.</param>
			<param name="result">To be added.</param>
			<summary>To be added.</summary>
			<returns>To be added.</returns>
			<remarks>To be added.</remarks>
			""")]
		NSUrlSessionUploadTask CreateUploadTask (NSUrlRequest request, NSData bodyData, NSUrlSessionResponse completionHandler);

		[Export ("downloadTaskWithRequest:completionHandler:")]
		[return: ForcedType]
		[Async (ResultTypeName = "NSUrlSessionDownloadTaskRequest", PostNonResultSnippet = "result.Resume ();", XmlDocs = """
			<param name="request">To be added.</param>
			<summary>To be added.</summary>
			<returns>
			          <para>A task that represents the asynchronous CreateDownloadTask operation.</para>
			        </returns>
			<remarks>
			          <para>
			The downloaded content is stored in a temporary file, whose path is stored in the returned NSUrlSessionDownloadTaskRequest instance. Since this is a temporary file, it will be deleted once the NSUrlSessionDownloadTaskRequest instance is garbage collected (or disposed).
			        </para>
			        </remarks>
			""",
			XmlDocsWithOutParameter = """
			<param name="request">A url request that specifies the resource to download.</param>
			<param name="result">Upon return contains the NSUrlSessionDownloadTask for that was created.</param>
			<summary>Downloads a url resource asynchronously to a temporary file.</summary>
			<returns>
			          <para>A task that represents the asynchronous CreateDownloadTask operation.</para>
			        </returns>
			<remarks>
			          <para>
			The downloaded content is stored in a temporary file, whose path is stored in the returned NSUrlSessionDownloadTaskRequest instance. Since this is a temporary file, it will be deleted once the NSUrlSessionDownloadTaskRequest instance is garbage collected (or disposed).
			          </para>
			        </remarks>
			""")]
		NSUrlSessionDownloadTask CreateDownloadTask (NSUrlRequest request, [NullAllowed] NSUrlDownloadSessionResponse completionHandler);

		[Export ("downloadTaskWithURL:completionHandler:")]
		[return: ForcedType]
		[Async (ResultTypeName = "NSUrlSessionDownloadTaskRequest", PostNonResultSnippet = "result.Resume ();", XmlDocs = """
			<param name="url">To be added.</param>
			<summary>To be added.</summary>
			<returns>
			          <para>A task that represents the asynchronous CreateDownloadTask operation.</para>
			        </returns>
			<remarks>
			          <para>
			The downloaded content is stored in a temporary file, whose path is stored in the returned NSUrlSessionDownloadTaskRequest instance. Since this is a temporary file, it will be deleted once the NSUrlSessionDownloadTaskRequest instance is garbage collected (or disposed).
			        </para>
			        </remarks>
			""",
			XmlDocsWithOutParameter = """
			<param name="url">The url that specifies the resource to download.</param>
			<param name="result">Upon return contains the NSUrlSessionDownloadTask for that was created.</param>
			<summary>Downloads a url resource asynchronously to a temporary file.</summary>
			<returns>
			          <para>A task that represents the asynchronous CreateDownloadTask operation.</para>
			        </returns>
			<remarks>
			The downloaded content is stored in a temporary file, whose path is stored in the returned NSUrlSessionDownloadTaskRequest instance. Since this is a temporary file, it will be deleted once the NSUrlSessionDownloadTaskRequest instance is garbage collected (or disposed).
			        </remarks>
			""")]
		NSUrlSessionDownloadTask CreateDownloadTask (NSUrl url, [NullAllowed] NSUrlDownloadSessionResponse completionHandler);

		[Export ("downloadTaskWithResumeData:completionHandler:")]
		[return: ForcedType]
		[Async (ResultTypeName = "NSUrlSessionDownloadTaskRequest", PostNonResultSnippet = "result.Resume ();", XmlDocs = """
			<param name="resumeData">To be added.</param>
			<summary>To be added.</summary>
			<returns>
			          <para>A task that represents the asynchronous CreateDownloadTaskFromResumeData operation.   The value of the TResult parameter is of type <c>Action&lt;Foundation.NSUrlSessionDownloadTaskRequest&gt;</c>.</para>
			        </returns>
			<remarks>
			          <para>
			The downloaded content is stored in a temporary file, whose path is stored in the returned NSUrlSessionDownloadTaskRequest instance. Since this is a temporary file, it will be deleted once the NSUrlSessionDownloadTaskRequest instance is garbage collected (or disposed).
			          </para>
			        </remarks>
			""",
			XmlDocsWithOutParameter = """
			<param name="resumeData">To be added.</param>
			<param name="result">To be added.</param>
			<summary>To be added.</summary>
			<returns>To be added.</returns>
			<remarks>To be added.</remarks>
			""")]
		NSUrlSessionDownloadTask CreateDownloadTaskFromResumeData (NSData resumeData, [NullAllowed] NSUrlDownloadSessionResponse completionHandler);


		[MacCatalyst (13, 1)]
		[Export ("getAllTasksWithCompletionHandler:")]
		[Async (ResultTypeName = "NSUrlSessionCombinedTasks", XmlDocs = """
			<summary>To be added.</summary>
			<returns>
			          <para>A task that represents the asynchronous GetAllTasks operation.   The value of the TResult parameter is an array of NSUrlSessionTask.  The base class for data-transfer tasks created by a .</para>
			        </returns>
			<remarks>
			          <para copied="true">The GetAllTasksAsync method is suitable to be used with C# async by returning control to the caller with a Task representing the operation.</para>
			          <para copied="true">To be added.</para>
			        </remarks>
			""")]
		void GetAllTasks (NSUrlSessionAllPendingTasks completionHandler);
#if !WATCHOS

		/// <param name="hostname">To be added.</param>
		/// <param name="port">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("streamTaskWithHostName:port:")]
		NSUrlSessionStreamTask CreateBidirectionalStream (string hostname, nint port);

#endif
#if !WATCHOS

		[Deprecated (PlatformName.MacOSX, 12, 0, message: "Use the Network.framework instead.")]
		[Deprecated (PlatformName.iOS, 15, 0, message: "Use the Network.framework instead.")]
		[Deprecated (PlatformName.TvOS, 15, 0, message: "Use the Network.framework instead.")]
		[MacCatalyst (13, 1)]
		[Deprecated (PlatformName.MacCatalyst, 15, 0, message: "Use the Network.framework instead.")]
		[Export ("streamTaskWithNetService:")]
		NSUrlSessionStreamTask CreateBidirectionalStream (NSNetService service);

#endif
#if !WATCHOS

		[MacCatalyst (13, 1)]
		[Export ("webSocketTaskWithURL:")]
		NSUrlSessionWebSocketTask CreateWebSocketTask (NSUrl url);

#endif
#if !WATCHOS

		[MacCatalyst (13, 1)]
		[Export ("webSocketTaskWithURL:protocols:")]
		NSUrlSessionWebSocketTask CreateWebSocketTask (NSUrl url, string [] protocols);

#endif
#if !WATCHOS

		[MacCatalyst (13, 1)]
		[Export ("webSocketTaskWithRequest:")]
		NSUrlSessionWebSocketTask CreateWebSocketTask (NSUrlRequest request);

#endif

		[TV (17, 0), iOS (17, 0), MacCatalyst (17, 0)]
		[Export ("uploadTaskWithResumeData:")]
		[return: ForcedType]
		NSUrlSessionUploadTask CreateUploadTask (NSData resumeData);

		[Async (ResultTypeName = "NSUrlSessionUploadTaskResumeRequest")]
		[TV (17, 0), iOS (17, 0), MacCatalyst (17, 0)]
		[Export ("uploadTaskWithResumeData:completionHandler:")]
		[return: ForcedType]
		NSUrlSessionUploadTask CreateUploadTask (NSData resumeData, Action<NSData, NSUrlResponse, NSError> completionHandler);
	}


	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject), Name = "NSURLSessionTask")]
	[DisableDefaultCtor]
	partial interface NSUrlSessionTask : NSCopying, NSProgressReporting {
		[Deprecated (PlatformName.MacOSX, 10, 15, message: "This type is not meant to be user created.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "This type is not meant to be user created.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "This type is not meant to be user created.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "This type is not meant to be user created.")]
		[Export ("init")]
		NativeHandle Constructor ();

		[Export ("taskIdentifier")]
		nuint TaskIdentifier { get; }

		[Export ("originalRequest", ArgumentSemantic.Copy), NullAllowed]
		NSUrlRequest OriginalRequest { get; }

		[Export ("currentRequest", ArgumentSemantic.Copy), NullAllowed]
		NSUrlRequest CurrentRequest { get; }

		[Export ("response", ArgumentSemantic.Copy), NullAllowed]
		NSUrlResponse Response { get; }

		[Export ("countOfBytesReceived")]
		long BytesReceived { get; }

		[Export ("countOfBytesSent")]
		long BytesSent { get; }

		[Export ("countOfBytesExpectedToSend")]
		long BytesExpectedToSend { get; }

		[Export ("countOfBytesExpectedToReceive")]
		long BytesExpectedToReceive { get; }

		[NullAllowed] // by default this property is null
		[Export ("taskDescription", ArgumentSemantic.Copy)]
		string TaskDescription { get; set; }

		[Export ("cancel")]
		void Cancel ();

		[Export ("state")]
		NSUrlSessionTaskState State { get; }

		[Export ("error", ArgumentSemantic.Copy), NullAllowed]
		NSError Error { get; }

		[Export ("suspend")]
		void Suspend ();

		[Export ("resume")]
		void Resume ();

		/// <summary>Represents the value associated with the constant NSUrlSessionTransferSizeUnknown</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLSessionTransferSizeUnknown")]
		long TransferSizeUnknown { get; }

		[MacCatalyst (13, 1)]
		[Export ("priority")]
		float Priority { get; set; } /* float, not CGFloat */

		[TV (14, 5), iOS (14, 5)]
		[MacCatalyst (14, 5)]
		[Export ("prefersIncrementalDelivery")]
		bool PrefersIncrementalDelivery { get; set; }

		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("earliestBeginDate", ArgumentSemantic.Copy)]
		NSDate EarliestBeginDate { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("countOfBytesClientExpectsToSend")]
		long CountOfBytesClientExpectsToSend { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("countOfBytesClientExpectsToReceive")]
		long CountOfBytesClientExpectsToReceive { get; set; }

		[TV (15, 0), iOS (15, 0), MacCatalyst (15, 0)]
		[NullAllowed, Export ("delegate", ArgumentSemantic.Retain)]
		NSObject WeakDelegate { get; set; }

		[TV (15, 0), iOS (15, 0), MacCatalyst (15, 0)]
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		INSUrlSessionTaskDelegate Delegate { get; set; }
	}


	// All of the NSUrlSession APIs are either 10.10, or 10.9 and 64-bit only
	// "NSURLSession is not available for i386 targets before Mac OS X 10.10."

	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSUrlSessionTask), Name = "NSURLSessionDataTask")]
	[DisableDefaultCtor]
	partial interface NSUrlSessionDataTask {
		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'NSURLSession.CreateDataTask' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'NSURLSession.CreateDataTask' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'NSURLSession.CreateDataTask' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'NSURLSession.CreateDataTask' instead.")]
		[Export ("init")]
		NativeHandle Constructor ();
	}


	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSUrlSessionDataTask), Name = "NSURLSessionUploadTask")]
	[DisableDefaultCtor]
	partial interface NSUrlSessionUploadTask {
		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'NSURLSession.CreateUploadTask' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'NSURLSession.CreateUploadTask' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'NSURLSession.CreateUploadTask' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'NSURLSession.CreateUploadTask' instead.")]
		[Export ("init")]
		NativeHandle Constructor ();

		[TV (17, 0), iOS (17, 0), MacCatalyst (17, 0)]
		[Field ("NSURLSessionUploadTaskResumeData")]
		NSString ResumeDataKey { get; }

		[Async]
		[TV (17, 0), iOS (17, 0), MacCatalyst (17, 0)]
		[Export ("cancelByProducingResumeData:")]
		void CancelByProducingResumeData (Action<NSData> completionHandler);
	}


	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSUrlSessionTask), Name = "NSURLSessionDownloadTask")]
	[DisableDefaultCtor]
	partial interface NSUrlSessionDownloadTask {
		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'NSURLSession.CreateDownloadTask' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'NSURLSession.CreateDownloadTask' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'NSURLSession.CreateDownloadTask' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'NSURLSession.CreateDownloadTask' instead.")]
		[Export ("init")]
		NativeHandle Constructor ();

		[Export ("cancelByProducingResumeData:")]
		void Cancel (Action<NSData> resumeCallback);
	}


	[Internal]
	[Static]
	[MacCatalyst (13, 1)]
	[NoWatch]
	interface ProxyConfigurationDictionaryKeys {
		[Field ("kCFNetworkProxiesHTTPEnable")]
		NSString HttpEnableKey { get; }

		[Field ("kCFStreamPropertyHTTPProxyHost")]
		NSString HttpProxyHostKey { get; }

		[Field ("kCFStreamPropertyHTTPProxyPort")]
		NSString HttpProxyPortKey { get; }

		[NoiOS, NoTV]
		[NoMacCatalyst]
		[Field ("kCFNetworkProxiesHTTPSEnable")]
		NSString HttpsEnableKey { get; }

		[Field ("kCFStreamPropertyHTTPSProxyHost")]
		NSString HttpsProxyHostKey { get; }

		[Field ("kCFStreamPropertyHTTPSProxyPort")]
		NSString HttpsProxyPortKey { get; }
	}


	[MacCatalyst (13, 1)]
	[StrongDictionary ("ProxyConfigurationDictionaryKeys")]
	[NoWatch]
	interface ProxyConfigurationDictionary {
		bool HttpEnable { get; set; }
		string HttpProxyHost { get; set; }
		int HttpProxyPort { get; set; }
		[NoiOS, NoTV]
		[NoMacCatalyst]
		bool HttpsEnable { get; set; }
		string HttpsProxyHost { get; set; }
		int HttpsProxyPort { get; set; }
	}


	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject), Name = "NSURLSessionConfiguration")]
	[DisableDefaultCtorAttribute]
	partial interface NSUrlSessionConfiguration : NSCopying {

		[Internal]
		[Static, Export ("defaultSessionConfiguration", ArgumentSemantic.Strong)]
		NSUrlSessionConfiguration _DefaultSessionConfiguration { get; }

		[Internal]
		[Static, Export ("ephemeralSessionConfiguration", ArgumentSemantic.Strong)]
		NSUrlSessionConfiguration _EphemeralSessionConfiguration { get; }

		[Internal]
		[Static, Export ("backgroundSessionConfiguration:")]
		NSUrlSessionConfiguration _BackgroundSessionConfiguration (string identifier);

		[Export ("identifier", ArgumentSemantic.Copy), NullAllowed]
		string Identifier { get; }

		[Export ("requestCachePolicy")]
		NSUrlRequestCachePolicy RequestCachePolicy { get; set; }

		[Export ("timeoutIntervalForRequest")]
		double TimeoutIntervalForRequest { get; set; }

		[Export ("timeoutIntervalForResource")]
		double TimeoutIntervalForResource { get; set; }

		[Export ("networkServiceType")]
		NSUrlRequestNetworkServiceType NetworkServiceType { get; set; }

		[Export ("allowsCellularAccess")]
		bool AllowsCellularAccess { get; set; }

		/// <summary>Whether background tasks can be scheduled at the discretion of the system in order to improve performance.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("discretionary")]
		bool Discretionary { [Bind ("isDiscretionary")] get; set; }

		[MacCatalyst (13, 1)]
		[Export ("sessionSendsLaunchEvents")]
		bool SessionSendsLaunchEvents { get; set; }

		[NullAllowed]
		[Export ("connectionProxyDictionary", ArgumentSemantic.Copy)]
		NSDictionary ConnectionProxyDictionary { get; set; }

		[MacCatalyst (13, 1)]
		[NoWatch]
		ProxyConfigurationDictionary StrongConnectionProxyDictionary {
			[Wrap ("new ProxyConfigurationDictionary (ConnectionProxyDictionary!)")]
			get;
			[Wrap ("ConnectionProxyDictionary = value.GetDictionary ()")]
			set;
		}

		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'TlsMinimumSupportedProtocolVersion' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'TlsMinimumSupportedProtocolVersion' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'TlsMinimumSupportedProtocolVersion' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'TlsMinimumSupportedProtocolVersion' instead.")]
		[Export ("TLSMinimumSupportedProtocol")]
		SslProtocol TLSMinimumSupportedProtocol { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("TLSMinimumSupportedProtocolVersion", ArgumentSemantic.Assign)]
		TlsProtocolVersion TlsMinimumSupportedProtocolVersion { get; set; }

		[Deprecated (PlatformName.MacOSX, 10, 15, message: "Use 'TlsMaximumSupportedProtocolVersion' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "Use 'TlsMaximumSupportedProtocolVersion' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "Use 'TlsMaximumSupportedProtocolVersion' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'TlsMaximumSupportedProtocolVersion' instead.")]
		[Export ("TLSMaximumSupportedProtocol")]
		SslProtocol TLSMaximumSupportedProtocol { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("TLSMaximumSupportedProtocolVersion", ArgumentSemantic.Assign)]
		TlsProtocolVersion TlsMaximumSupportedProtocolVersion { get; set; }

		[Deprecated (PlatformName.iOS, 18, 4, message: "Use HTTP/2 or HTTP/3 instead. Only supported when the classic loader is enabled.")]
		[Deprecated (PlatformName.TvOS, 18, 4, message: "Use HTTP/2 or HTTP/3 instead. Only supported when the classic loader is enabled.")]
		[Deprecated (PlatformName.MacCatalyst, 18, 4, message: "Use HTTP/2 or HTTP/3 instead. Only supported when the classic loader is enabled.")]
		[Deprecated (PlatformName.MacOSX, 15, 4, message: "Use HTTP/2 or HTTP/3 instead. Only supported when the classic loader is enabled.")]
		[Export ("HTTPShouldUsePipelining")]
		bool HttpShouldUsePipelining { get; set; }

		[Export ("HTTPShouldSetCookies")]
		bool HttpShouldSetCookies { get; set; }

		[Export ("HTTPCookieAcceptPolicy")]
		NSHttpCookieAcceptPolicy HttpCookieAcceptPolicy { get; set; }

		[NullAllowed]
		[Export ("HTTPAdditionalHeaders", ArgumentSemantic.Copy)]
		NSDictionary HttpAdditionalHeaders { get; set; }

		[Export ("HTTPMaximumConnectionsPerHost")]
		nint HttpMaximumConnectionsPerHost { get; set; }

		[NullAllowed]
		[Export ("HTTPCookieStorage", ArgumentSemantic.Retain)]
		NSHttpCookieStorage HttpCookieStorage { get; set; }

		[NullAllowed]
		[Export ("URLCredentialStorage", ArgumentSemantic.Retain)]
		NSUrlCredentialStorage URLCredentialStorage { get; set; }

		[NullAllowed]
		[Export ("URLCache", ArgumentSemantic.Retain)]
		NSUrlCache URLCache { get; set; }

		[NullAllowed]
		[Export ("protocolClasses", ArgumentSemantic.Copy)]
		NSArray WeakProtocolClasses { get; set; }

		[NullAllowed]
		[MacCatalyst (13, 1)]
		[Export ("sharedContainerIdentifier")]
		string SharedContainerIdentifier { get; set; }

		[Internal]
		[MacCatalyst (13, 1)]
		[Static, Export ("backgroundSessionConfigurationWithIdentifier:")]
		NSUrlSessionConfiguration _CreateBackgroundSessionConfiguration (string identifier);

		[Deprecated (PlatformName.iOS, 18, 4, message: "Not supported anymore.")]
		[Deprecated (PlatformName.TvOS, 18, 4, message: "Not supported anymore.")]
		[Deprecated (PlatformName.MacCatalyst, 18, 4, message: "Not supported anymore.")]
		[Deprecated (PlatformName.MacOSX, 15, 4, message: "Not supported anymore.")]
		[MacCatalyst (13, 1)]
		[Export ("shouldUseExtendedBackgroundIdleMode")]
		bool ShouldUseExtendedBackgroundIdleMode { get; set; }
#if !WATCHOS

		[NoTV, NoMac]
		[MacCatalyst (13, 1)]
		[Export ("multipathServiceType", ArgumentSemantic.Assign)]
		NSUrlSessionMultipathServiceType MultipathServiceType { get; set; }

#endif

		[MacCatalyst (13, 1)]
		[Export ("waitsForConnectivity")]
		bool WaitsForConnectivity { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("allowsExpensiveNetworkAccess")]
		bool AllowsExpensiveNetworkAccess { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("allowsConstrainedNetworkAccess")]
		bool AllowsConstrainedNetworkAccess { get; set; }

		[Watch (26, 1)]
		[TV (26, 1), Mac (26, 1), iOS (26, 1), MacCatalyst (26, 1)]
		[Export ("allowsUltraConstrainedNetworkAccess")]
		bool AllowsUltraConstrainedNetworkAccess { get; set; }

		[TV (16, 0), iOS (16, 0)]
		[MacCatalyst (16, 0)]
		[Export ("requiresDNSSECValidation")]
		bool RequiresDnsSecValidation { get; set; }
#if !WATCHOS

		[Internal]
		[Export ("proxyConfigurations", ArgumentSemantic.Copy)]
		IntPtr _ProxyConfigurations { get; set; }

#endif

		[iOS (18, 4), TV (18, 4), MacCatalyst (18, 4), Mac (15, 4)]
		[Export ("usesClassicLoadingMode")]
		bool UsesClassicLoadingMode { get; set; }

		[TV (26, 0), Mac (26, 0), iOS (26, 0), MacCatalyst (26, 0)]
		[Export ("enablesEarlyData")]
		bool EnablesEarlyData { get; set; }
	}


	[MacCatalyst (13, 1)]
	[Model, BaseType (typeof (NSObject), Name = "NSURLSessionDelegate")]
	[Protocol]
	partial interface NSUrlSessionDelegate {
		/// <param name="session">To be added.</param>
		/// <param name="error">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:didBecomeInvalidWithError:")]
		void DidBecomeInvalid (NSUrlSession session, NSError error);

		/// <param name="session">To be added.</param>
		/// <param name="challenge">To be added.</param>
		/// <param name="completionHandler">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:didReceiveChallenge:completionHandler:")]
		void DidReceiveChallenge (NSUrlSession session, NSUrlAuthenticationChallenge challenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler);

		/// <param name="session">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("URLSessionDidFinishEventsForBackgroundURLSession:")]
		void DidFinishEventsForBackgroundSession (NSUrlSession session);
	}


	public interface INSUrlSessionTaskDelegate { }


	[MacCatalyst (13, 1)]
	[Model]
	[BaseType (typeof (NSUrlSessionDelegate), Name = "NSURLSessionTaskDelegate")]
	[Protocol]
	partial interface NSUrlSessionTaskDelegate {

		/// <param name="session">To be added.</param>
		/// <param name="task">To be added.</param>
		/// <param name="response">To be added.</param>
		/// <param name="newRequest">To be added.</param>
		/// <param name="completionHandler">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:task:willPerformHTTPRedirection:newRequest:completionHandler:")]
#nullable enable
		void WillPerformHttpRedirection (NSUrlSession session, NSUrlSessionTask task, NSHttpUrlResponse response, NSUrlRequest newRequest, Action<NSUrlRequest?> completionHandler);
#nullable disable

		/// <param name="session">To be added.</param>
		/// <param name="task">To be added.</param>
		/// <param name="challenge">To be added.</param>
		/// <param name="completionHandler">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:task:didReceiveChallenge:completionHandler:")]
		void DidReceiveChallenge (NSUrlSession session, NSUrlSessionTask task, NSUrlAuthenticationChallenge challenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler);

		/// <param name="session">To be added.</param>
		/// <param name="task">To be added.</param>
		/// <param name="completionHandler">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:task:needNewBodyStream:")]
		void NeedNewBodyStream (NSUrlSession session, NSUrlSessionTask task, Action<NSInputStream> completionHandler);

		/// <param name="session">To be added.</param>
		/// <param name="task">To be added.</param>
		/// <param name="bytesSent">To be added.</param>
		/// <param name="totalBytesSent">To be added.</param>
		/// <param name="totalBytesExpectedToSend">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:task:didSendBodyData:totalBytesSent:totalBytesExpectedToSend:")]
		void DidSendBodyData (NSUrlSession session, NSUrlSessionTask task, long bytesSent, long totalBytesSent, long totalBytesExpectedToSend);

		/// <param name="session">To be added.</param>
		/// <param name="task">To be added.</param>
		/// <param name="error">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:task:didCompleteWithError:")]
		void DidCompleteWithError (NSUrlSession session, NSUrlSessionTask task, [NullAllowed] NSError error);

		/// <param name="session">To be added.</param>
		/// <param name="task">To be added.</param>
		/// <param name="metrics">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("URLSession:task:didFinishCollectingMetrics:")]
		void DidFinishCollectingMetrics (NSUrlSession session, NSUrlSessionTask task, NSUrlSessionTaskMetrics metrics);

		/// <param name="session">To be added.</param>
		/// <param name="task">To be added.</param>
		/// <param name="request">To be added.</param>
		/// <param name="completionHandler">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("URLSession:task:willBeginDelayedRequest:completionHandler:")]
		void WillBeginDelayedRequest (NSUrlSession session, NSUrlSessionTask task, NSUrlRequest request, Action<NSUrlSessionDelayedRequestDisposition, NSUrlRequest> completionHandler);

		/// <param name="session">To be added.</param>
		/// <param name="task">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("URLSession:taskIsWaitingForConnectivity:")]
		void TaskIsWaitingForConnectivity (NSUrlSession session, NSUrlSessionTask task);

		[TV (16, 0), iOS (16, 0)]
		[MacCatalyst (16, 0)]
		[Export ("URLSession:didCreateTask:")]
		void DidCreateTask (NSUrlSession session, NSUrlSessionTask task);

		[TV (17, 0), iOS (17, 0), MacCatalyst (17, 0)]
		[Export ("URLSession:task:didReceiveInformationalResponse:")]
		void DidReceiveInformationalResponse (NSUrlSession session, NSUrlSessionTask task, NSHttpUrlResponse response);

		[TV (17, 0), iOS (17, 0), MacCatalyst (17, 0)]
		[Export ("URLSession:task:needNewBodyStreamFromOffset:completionHandler:")]
		void NeedNewBodyStream (NSUrlSession session, NSUrlSessionTask task, long offset, Action<NSInputStream> completionHandler);
	}


	[MacCatalyst (13, 1)]
	[Model]
	[BaseType (typeof (NSUrlSessionTaskDelegate), Name = "NSURLSessionDataDelegate")]
	[Protocol]
	partial interface NSUrlSessionDataDelegate {
		/// <param name="session">To be added.</param>
		/// <param name="dataTask">To be added.</param>
		/// <param name="response">To be added.</param>
		/// <param name="completionHandler">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:dataTask:didReceiveResponse:completionHandler:")]
		void DidReceiveResponse (NSUrlSession session, NSUrlSessionDataTask dataTask, NSUrlResponse response, Action<NSUrlSessionResponseDisposition> completionHandler);

		/// <param name="session">To be added.</param>
		/// <param name="dataTask">To be added.</param>
		/// <param name="downloadTask">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:dataTask:didBecomeDownloadTask:")]
		void DidBecomeDownloadTask (NSUrlSession session, NSUrlSessionDataTask dataTask, NSUrlSessionDownloadTask downloadTask);

		/// <param name="session">To be added.</param>
		/// <param name="dataTask">To be added.</param>
		/// <param name="data">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:dataTask:didReceiveData:")]
		void DidReceiveData (NSUrlSession session, NSUrlSessionDataTask dataTask, NSData data);

		/// <param name="session">To be added.</param>
		/// <param name="dataTask">To be added.</param>
		/// <param name="proposedResponse">To be added.</param>
		/// <param name="completionHandler">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:dataTask:willCacheResponse:completionHandler:")]
		void WillCacheResponse (NSUrlSession session, NSUrlSessionDataTask dataTask, NSCachedUrlResponse proposedResponse, Action<NSCachedUrlResponse> completionHandler);
#if !WATCHOS

		/// <param name="session">To be added.</param>
		/// <param name="dataTask">To be added.</param>
		/// <param name="streamTask">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("URLSession:dataTask:didBecomeStreamTask:")]
		void DidBecomeStreamTask (NSUrlSession session, NSUrlSessionDataTask dataTask, NSUrlSessionStreamTask streamTask);

#endif
	}


	[MacCatalyst (13, 1)]
	[Model]
	[BaseType (typeof (NSUrlSessionTaskDelegate), Name = "NSURLSessionDownloadDelegate")]
	[Protocol]
	partial interface NSUrlSessionDownloadDelegate {

		/// <param name="session">To be added.</param>
		/// <param name="downloadTask">To be added.</param>
		/// <param name="location">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[Export ("URLSession:downloadTask:didFinishDownloadingToURL:")]
		void DidFinishDownloading (NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location);

		/// <param name="session">To be added.</param>
		/// <param name="downloadTask">To be added.</param>
		/// <param name="bytesWritten">To be added.</param>
		/// <param name="totalBytesWritten">To be added.</param>
		/// <param name="totalBytesExpectedToWrite">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:downloadTask:didWriteData:totalBytesWritten:totalBytesExpectedToWrite:")]
		void DidWriteData (NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long bytesWritten, long totalBytesWritten, long totalBytesExpectedToWrite);

		/// <param name="session">To be added.</param>
		/// <param name="downloadTask">To be added.</param>
		/// <param name="resumeFileOffset">To be added.</param>
		/// <param name="expectedTotalBytes">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("URLSession:downloadTask:didResumeAtOffset:expectedTotalBytes:")]
		void DidResume (NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long resumeFileOffset, long expectedTotalBytes);

		/// <summary>Represents the value associated with the constant NSURLSessionDownloadTaskResumeData</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLSessionDownloadTaskResumeData")]
		NSString TaskResumeDataKey { get; }
	}


	[BaseType (typeof (NSObject), Name = "NSURLProtectionSpace")]
	// 'init' returns NIL
	[DisableDefaultCtor]
	interface NSUrlProtectionSpace : NSSecureCoding, NSCopying {

		[Internal]
		[Export ("initWithHost:port:protocol:realm:authenticationMethod:")]
		IntPtr _Init (string host, nint port, [NullAllowed] string protocol, [NullAllowed] string realm, [NullAllowed] string authenticationMethod);

		[Internal]
		[Export ("initWithProxyHost:port:type:realm:authenticationMethod:")]
		IntPtr _InitWithProxy (string host, nint port, [NullAllowed] string type, [NullAllowed] string realm, [NullAllowed] string authenticationMethod);

		[Export ("realm")]
		string Realm { get; }

		[Export ("receivesCredentialSecurely")]
		bool ReceivesCredentialSecurely { get; }

		[Export ("isProxy")]
		bool IsProxy { get; }

		[Export ("host")]
		string Host { get; }

		[Export ("port")]
		nint Port { get; }

		[Export ("proxyType")]
		string ProxyType { get; }

		[Export ("protocol")]
		string Protocol { get; }

		[Export ("authenticationMethod")]
		string AuthenticationMethod { get; }

		// NSURLProtectionSpace(NSClientCertificateSpace)

		[Export ("distinguishedNames")]
		NSData [] DistinguishedNames { get; }

		// NSURLProtectionSpace(NSServerTrustValidationSpace)
		[Internal]
		[Export ("serverTrust")]
		IntPtr ServerTrust { get; }

		/// <summary>Represents the value associated with the constant NSURLProtectionSpaceHTTP</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLProtectionSpaceHTTP")]
		NSString HTTP { get; }

		/// <summary>Represents the value associated with the constant NSURLProtectionSpaceHTTPS</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLProtectionSpaceHTTPS")]
		NSString HTTPS { get; }

		/// <summary>Represents the value associated with the constant NSURLProtectionSpaceFTP</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.iOS, 18, 4, message: "FTP is deprecated. Only supported when the classic loader is enabled.")]
		[Deprecated (PlatformName.TvOS, 18, 4, message: "FTP is deprecated. Only supported when the classic loader is enabled.")]
		[Deprecated (PlatformName.MacCatalyst, 18, 4, message: "FTP is deprecated. Only supported when the classic loader is enabled.")]
		[Deprecated (PlatformName.MacOSX, 15, 4, message: "FTP is deprecated. Only supported when the classic loader is enabled.")]
		[Field ("NSURLProtectionSpaceFTP")]
		NSString FTP { get; }

		/// <summary>Represents the value associated with the constant NSURLProtectionSpaceHTTPProxy</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.iOS, 18, 4, message: "FTP is deprecated. Only supported when the classic loader is enabled.")]
		[Deprecated (PlatformName.TvOS, 18, 4, message: "FTP is deprecated. Only supported when the classic loader is enabled.")]
		[Deprecated (PlatformName.MacCatalyst, 18, 4, message: "FTP is deprecated. Only supported when the classic loader is enabled.")]
		[Deprecated (PlatformName.MacOSX, 15, 4, message: "FTP is deprecated. Only supported when the classic loader is enabled.")]
		[Field ("NSURLProtectionSpaceHTTPProxy")]
		NSString HTTPProxy { get; }

		/// <summary>Represents the value associated with the constant NSURLProtectionSpaceHTTPSProxy</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLProtectionSpaceHTTPSProxy")]
		NSString HTTPSProxy { get; }

		/// <summary>Represents the value associated with the constant NSURLProtectionSpaceFTPProxy</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLProtectionSpaceFTPProxy")]
		NSString FTPProxy { get; }

		/// <summary>Represents the value associated with the constant NSURLProtectionSpaceSOCKSProxy</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLProtectionSpaceSOCKSProxy")]
		NSString SOCKSProxy { get; }

		/// <summary>Represents the value associated with the constant NSURLAuthenticationMethodDefault</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLAuthenticationMethodDefault")]
		NSString AuthenticationMethodDefault { get; }

		/// <summary>Represents the value associated with the constant NSURLAuthenticationMethodHTTPBasic</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLAuthenticationMethodHTTPBasic")]
		NSString AuthenticationMethodHTTPBasic { get; }

		/// <summary>Represents the value associated with the constant NSURLAuthenticationMethodHTTPDigest</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLAuthenticationMethodHTTPDigest")]
		NSString AuthenticationMethodHTTPDigest { get; }

		/// <summary>Represents the value associated with the constant NSURLAuthenticationMethodHTMLForm</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLAuthenticationMethodHTMLForm")]
		NSString AuthenticationMethodHTMLForm { get; }

		/// <summary>Represents the value associated with the constant NSURLAuthenticationMethodNTLM</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLAuthenticationMethodNTLM")]
		NSString AuthenticationMethodNTLM { get; }

		/// <summary>Represents the value associated with the constant NSURLAuthenticationMethodNegotiate</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLAuthenticationMethodNegotiate")]
		NSString AuthenticationMethodNegotiate { get; }

		/// <summary>Represents the value associated with the constant NSURLAuthenticationMethodClientCertificate</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLAuthenticationMethodClientCertificate")]
		NSString AuthenticationMethodClientCertificate { get; }

		/// <summary>Represents the value associated with the constant NSURLAuthenticationMethodServerTrust</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSURLAuthenticationMethodServerTrust")]
		NSString AuthenticationMethodServerTrust { get; }
	}


	[BaseType (typeof (NSObject), Name = "NSURLRequest")]
	interface NSUrlRequest : NSSecureCoding, NSMutableCopying {
		[Export ("initWithURL:")]
		NativeHandle Constructor (NSUrl url);

		[DesignatedInitializer]
		[Export ("initWithURL:cachePolicy:timeoutInterval:")]
		NativeHandle Constructor (NSUrl url, NSUrlRequestCachePolicy cachePolicy, double timeoutInterval);

		[Export ("requestWithURL:")]
		[Static]
		NSUrlRequest FromUrl (NSUrl url);

		[Export ("URL")]
		NSUrl Url { get; }

		[Export ("cachePolicy")]
		NSUrlRequestCachePolicy CachePolicy { get; }

		[Export ("timeoutInterval")]
		double TimeoutInterval { get; }

		[Export ("mainDocumentURL")]
		NSUrl MainDocumentURL { get; }

		[Export ("networkServiceType")]
		NSUrlRequestNetworkServiceType NetworkServiceType { get; }

		[Export ("allowsCellularAccess")]
		bool AllowsCellularAccess { get; }

		[MacCatalyst (13, 1)]
		[Export ("allowsExpensiveNetworkAccess")]
		bool AllowsExpensiveNetworkAccess { get; [NotImplemented] set; }

		[MacCatalyst (13, 1)]
		[Export ("allowsConstrainedNetworkAccess")]
		bool AllowsConstrainedNetworkAccess { get; [NotImplemented] set; }

		// It's in the headers, but introspection says it's not, and an Xcode test confirms it's not there
		// [TV (26, 1), Mac (26, 1), iOS (26, 1), MacCatalyst (26, 1)]
		// [Export ("allowsUltraConstrainedNetworkAccess")]
		// bool AllowsUltraConstrainedNetworkAccess { get; [NotImplemented] set; }

		[Export ("HTTPMethod")]
		string HttpMethod { get; }

		[Export ("allHTTPHeaderFields")]
		NSDictionary Headers { get; }

		[Internal]
		[Export ("valueForHTTPHeaderField:")]
		string Header (string field);

		[Export ("HTTPBody")]
		NSData Body { get; }

		[Export ("HTTPBodyStream")]
		NSInputStream BodyStream { get; }

		[Export ("HTTPShouldHandleCookies")]
		bool ShouldHandleCookies { get; }

		[TV (14, 5), iOS (14, 5)]
		[MacCatalyst (14, 5)]
		[Export ("assumesHTTP3Capable")]
		bool AssumesHttp3Capable { get; [NotImplemented] set; }

		[TV (15, 0), iOS (15, 0), MacCatalyst (15, 0)]
		[Export ("attribution")]
		NSURLRequestAttribution Attribution { get; }

		// macOS is documented out of sync with iOS here
		[TV (16, 1), iOS (16, 1)]
		[MacCatalyst (16, 1)]
		[Export ("requiresDNSSECValidation")]
		bool RequiresDnsSecValidation { get; }

		[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
		[Export ("allowsPersistentDNS")]
		bool AllowsPersistentDns { get; }

		[TV (18, 2), iOS (18, 2), MacCatalyst (18, 2), Mac (15, 2)]
		[Export ("cookiePartitionIdentifier", ArgumentSemantic.Copy), NullAllowed]
		string CookiePartitionIdentifier { get; }
	}


	[BaseType (typeof (NSDictionary))]
	[DesignatedDefaultCtor]
	interface NSMutableDictionary {
		[Export ("dictionaryWithContentsOfFile:")]
		[Static]
		NSMutableDictionary FromFile (string path);

		[Export ("dictionaryWithContentsOfURL:")]
		[Static]
		NSMutableDictionary FromUrl (NSUrl url);

		[Export ("dictionaryWithObject:forKey:")]
		[Static]
		NSMutableDictionary FromObjectAndKey (NSObject obj, NSObject key);

		[Export ("dictionaryWithDictionary:")]
		[Static, New]
		NSMutableDictionary FromDictionary (NSDictionary source);

		[Export ("dictionaryWithObjects:forKeys:count:")]
		[Static, Internal]
		NSMutableDictionary FromObjectsAndKeysInternalCount (NSArray objects, NSArray keys, nint count);

		[Export ("dictionaryWithObjects:forKeys:")]
		[Static, Internal, New]
		NSMutableDictionary FromObjectsAndKeysInternal (NSArray objects, NSArray Keys);

		[Export ("initWithDictionary:")]
		NativeHandle Constructor (NSDictionary other);

		[Export ("initWithDictionary:copyItems:")]
		NativeHandle Constructor (NSDictionary other, bool copyItems);

		[Export ("initWithContentsOfFile:")]
		NativeHandle Constructor (string fileName);

		[Export ("initWithContentsOfURL:")]
		NativeHandle Constructor (NSUrl url);

		[Internal]
		[Export ("initWithObjects:forKeys:")]
		NativeHandle Constructor (NSArray objects, NSArray keys);

		[Export ("removeAllObjects"), Internal]
		void RemoveAllObjects ();

		[Sealed]
		[Internal]
		[Export ("removeObjectForKey:")]
		void _RemoveObjectForKey (IntPtr key);

		[Export ("removeObjectForKey:"), Internal]
		void RemoveObjectForKey (NSObject key);

		[Sealed]
		[Internal]
		[Export ("setObject:forKey:")]
		void _SetObject (IntPtr obj, IntPtr key);

		[Export ("setObject:forKey:"), Internal]
		void SetObject (NSObject obj, NSObject key);

		[Static]
		[Export ("dictionaryWithSharedKeySet:")]
		NSDictionary FromSharedKeySet (NSObject sharedKeyToken);

		[Export ("addEntriesFromDictionary:")]
		void AddEntries (NSDictionary other);
	}


	interface NSMutableDictionary<K, V> : NSDictionary { }


	[BaseType (typeof (NSSet))]
	[DesignatedDefaultCtor]
	interface NSMutableSet {
		[Export ("initWithArray:")]
		NativeHandle Constructor (NSArray other);

		[Export ("initWithSet:")]
		NativeHandle Constructor (NSSet other);

		/// <param name="capacity">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[DesignatedInitializer]
		[Export ("initWithCapacity:")]
		NativeHandle Constructor (nint capacity);

		[Internal]
		[Sealed]
		[Export ("addObject:")]
		void _Add (IntPtr obj);

		[Export ("addObject:")]
		void Add (NSObject nso);

		[Internal]
		[Sealed]
		[Export ("removeObject:")]
		void _Remove (IntPtr nso);

		[Export ("removeObject:")]
		void Remove (NSObject nso);

		[Export ("removeAllObjects")]
		void RemoveAll ();

		[Internal]
		[Sealed]
		[Export ("addObjectsFromArray:")]
		void _AddObjects (IntPtr objects);

		[Export ("addObjectsFromArray:")]
		void AddObjects (NSObject [] objects);

		[Internal, Export ("minusSet:")]
		void MinusSet (NSSet other);

		[Internal, Export ("unionSet:")]
		void UnionSet (NSSet other);
	}


	[BaseType (typeof (NSUrlRequest), Name = "NSMutableURLRequest")]
	interface NSMutableUrlRequest {
		[Export ("initWithURL:")]
		NativeHandle Constructor (NSUrl url);

		[Export ("initWithURL:cachePolicy:timeoutInterval:")]
		NativeHandle Constructor (NSUrl url, NSUrlRequestCachePolicy cachePolicy, double timeoutInterval);

		[NullAllowed] // by default this property is null
		[New]
		[Export ("URL")]
		NSUrl Url { get; set; }

		[New]
		[Export ("cachePolicy")]
		NSUrlRequestCachePolicy CachePolicy { get; set; }

		[New]
		[Export ("timeoutInterval")]
		double TimeoutInterval { set; get; }

		[NullAllowed] // by default this property is null
		[New]
		[Export ("mainDocumentURL")]
		NSUrl MainDocumentURL { get; set; }

		[New]
		[Export ("HTTPMethod")]
		string HttpMethod { get; set; }

		[NullAllowed] // by default this property is null
		[New]
		[Export ("allHTTPHeaderFields")]
		NSDictionary Headers { get; set; }

		[Internal]
		[Export ("setValue:forHTTPHeaderField:")]
		void _SetValue (string value, string field);

		[NullAllowed] // by default this property is null
		[New]
		[Export ("HTTPBody")]
		NSData Body { get; set; }

		[NullAllowed] // by default this property is null
		[New]
		[Export ("HTTPBodyStream")]
		NSInputStream BodyStream { get; set; }

		[New]
		[Export ("HTTPShouldHandleCookies")]
		bool ShouldHandleCookies { get; set; }

		[Export ("networkServiceType")]
		NSUrlRequestNetworkServiceType NetworkServiceType { set; get; }

		[New]
		[Export ("allowsCellularAccess")]
		bool AllowsCellularAccess { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("allowsExpensiveNetworkAccess")]
		bool AllowsExpensiveNetworkAccess { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("allowsConstrainedNetworkAccess")]
		bool AllowsConstrainedNetworkAccess { get; set; }

		// It's in the headers, but introspection says it's not, and an Xcode test confirms it's not there
		// [TV (26, 1), Mac (26, 1), iOS (26, 1), MacCatalyst (26, 1)]
		// [Export ("allowsUltraConstrainedNetworkAccess")]
		// bool AllowsUltraConstrainedNetworkAccess { get; set; }

		[TV (14, 5), iOS (14, 5)]
		[MacCatalyst (14, 5)]
		[Export ("assumesHTTP3Capable")]
		bool AssumesHttp3Capable { get; set; }

		[TV (15, 0), iOS (15, 0), MacCatalyst (15, 0)]
		[Export ("attribution", ArgumentSemantic.Assign)]
		NSURLRequestAttribution Attribution { get; set; }

		// Documented as 16.0 but did not work until 16.1 - https://github.com/xamarin/maccore/issues/2608 - https://feedbackassistant.apple.com/feedback/10897552
		[TV (16, 1), iOS (16, 1)]
		[MacCatalyst (16, 1)]
		[Export ("requiresDNSSECValidation")]
		bool RequiresDnsSecValidation { get; set; }

		[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
		[Export ("allowsPersistentDNS")]
		bool AllowsPersistentDns { get; set; }

		[TV (18, 2), iOS (18, 2), MacCatalyst (18, 2), Mac (15, 2)]
		[Export ("cookiePartitionIdentifier", ArgumentSemantic.Copy), NullAllowed]
		string CookiePartitionIdentifier { get; set; }
	}


	[BaseType (typeof (NSObject), Name = "NSURLResponse")]
	interface NSUrlResponse : NSSecureCoding, NSCopying {
		/// <param name="url">To be added.</param>
		/// <param name="mimetype">To be added.</param>
		/// <param name="expectedContentLength">To be added.</param>
		/// <param name="textEncodingName">
		///           <para>To be added.</para>
		///           <para tool="nullallowed">This parameter can be <see langword="null" />.</para>
		///         </param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[DesignatedInitializer]
		[Export ("initWithURL:MIMEType:expectedContentLength:textEncodingName:")]
		NativeHandle Constructor (NSUrl url, string mimetype, nint expectedContentLength, [NullAllowed] string textEncodingName);

		[Export ("URL")]
		NSUrl Url { get; }

		[Export ("MIMEType")]
		string MimeType { get; }

		[Export ("expectedContentLength")]
		long ExpectedContentLength { get; }

		[Export ("textEncodingName")]
		string TextEncodingName { get; }

		[Export ("suggestedFilename")]
		string SuggestedFilename { get; }
	}


	[BaseType (typeof (NSObject), Delegates = new string [] { "WeakDelegate" }, Events = new Type [] { typeof (NSStreamDelegate) })]
	interface NSStream {
		[Export ("open")]
		void Open ();

		[Export ("close")]
		void Close ();

		// Header says:
		//    assign /* actually weak */
		// so bind as weak
		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		NSObject WeakDelegate { get; set; }

		/// <summary>An instance of the Foundation.INSStreamDelegate model class which acts as the class delegate.</summary>
		///         <value>The instance of the Foundation.INSStreamDelegate model class</value>
		///         <remarks>
		///           <para>The delegate instance assigned to this object will be used to handle events or provide data on demand to this class.</para>
		///           <para>When setting the Delegate or WeakDelegate values events will be delivered to the specified instance instead of being delivered to the C#-style events</para>
		///           <para>This is the strongly typed version of the object, developers should use the WeakDelegate property instead if they want to merely assign a class derived from NSObject that has been decorated with [Export] attributes.</para>
		///         </remarks>
		[Wrap ("WeakDelegate")]
		INSStreamDelegate Delegate { get; set; }

		[Abstract]
		[return: NullAllowed]
		[Protected]
		[Export ("propertyForKey:")]
		NSObject GetProperty (NSString key);

		[Abstract]
		[Protected]
		[Export ("setProperty:forKey:")]
		bool SetProperty ([NullAllowed] NSObject property, NSString key);

		[Export ("scheduleInRunLoop:forMode:")]
		void Schedule (NSRunLoop aRunLoop, NSString mode);

		[Export ("removeFromRunLoop:forMode:")]
		void Unschedule (NSRunLoop aRunLoop, NSString mode);
		/// <param name="aRunLoop">To be added.</param>
		///         <param name="mode">To be added.</param>
		///         <summary>To be added.</summary>
		///         <remarks>To be added.</remarks>
		[Wrap ("Schedule (aRunLoop, mode.GetConstant ()!)")]
		void Schedule (NSRunLoop aRunLoop, NSRunLoopMode mode);

		/// <param name="aRunLoop">To be added.</param>
		///         <param name="mode">To be added.</param>
		///         <summary>To be added.</summary>
		///         <remarks>To be added.</remarks>
		[Wrap ("Unschedule (aRunLoop, mode.GetConstant ()!)")]
		void Unschedule (NSRunLoop aRunLoop, NSRunLoopMode mode);

		[Export ("streamStatus")]
		NSStreamStatus Status { get; }

		[Export ("streamError")]
		NSError Error { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSocketSecurityLevelKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSocketSecurityLevelKey")]
		NSString SocketSecurityLevelKey { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSocketSecurityLevelNone</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSocketSecurityLevelNone")]
		NSString SocketSecurityLevelNone { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSocketSecurityLevelSSLv2</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSocketSecurityLevelSSLv2")]
		NSString SocketSecurityLevelSslV2 { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSocketSecurityLevelSSLv3</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSocketSecurityLevelSSLv3")]
		NSString SocketSecurityLevelSslV3 { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSocketSecurityLevelTLSv1</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSocketSecurityLevelTLSv1")]
		NSString SocketSecurityLevelTlsV1 { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSocketSecurityLevelNegotiatedSSL</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSocketSecurityLevelNegotiatedSSL")]
		NSString SocketSecurityLevelNegotiatedSsl { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSOCKSProxyConfigurationKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSOCKSProxyConfigurationKey")]
		NSString SocksProxyConfigurationKey { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSOCKSProxyHostKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSOCKSProxyHostKey")]
		NSString SocksProxyHostKey { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSOCKSProxyPortKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSOCKSProxyPortKey")]
		NSString SocksProxyPortKey { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSOCKSProxyVersionKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSOCKSProxyVersionKey")]
		NSString SocksProxyVersionKey { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSOCKSProxyUserKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSOCKSProxyUserKey")]
		NSString SocksProxyUserKey { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSOCKSProxyPasswordKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSOCKSProxyPasswordKey")]
		NSString SocksProxyPasswordKey { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSOCKSProxyVersion4</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSOCKSProxyVersion4")]
		NSString SocksProxyVersion4 { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSOCKSProxyVersion5</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSOCKSProxyVersion5")]
		NSString SocksProxyVersion5 { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamDataWrittenToMemoryStreamKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamDataWrittenToMemoryStreamKey")]
		NSString DataWrittenToMemoryStreamKey { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamFileCurrentOffsetKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamFileCurrentOffsetKey")]
		NSString FileCurrentOffsetKey { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSocketSSLErrorDomain</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSocketSSLErrorDomain")]
		NSString SocketSslErrorDomain { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamSOCKSErrorDomain</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamSOCKSErrorDomain")]
		NSString SocksErrorDomain { get; }

		/// <summary>The low-level service type that this stream is providing.</summary>
		///         <value>
		///         </value>
		///         <remarks>This is the low-level token-based setting for the service type, in general, you can use the strongly typed ServiceType instead.</remarks>
		[Advanced, Field ("NSStreamNetworkServiceType")]
		NSString NetworkServiceType { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamNetworkServiceTypeVoIP</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamNetworkServiceTypeVoIP")]
		NSString NetworkServiceTypeVoIP { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamNetworkServiceTypeVideo</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamNetworkServiceTypeVideo")]
		NSString NetworkServiceTypeVideo { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamNetworkServiceTypeBackground</summary>
		///         <value>
		///         </value>
		///         <remarks>
		///         </remarks>
		[Advanced, Field ("NSStreamNetworkServiceTypeBackground")]
		NSString NetworkServiceTypeBackground { get; }

		/// <summary>Low-level key: Represents the value associated with the constant NSStreamNetworkServiceTypeVoice</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Advanced, Field ("NSStreamNetworkServiceTypeVoice")]
		NSString NetworkServiceTypeVoice { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Advanced]
		[MacCatalyst (13, 1)]
		[Field ("NSStreamNetworkServiceTypeCallSignaling")]
		NSString NetworkServiceTypeCallSignaling { get; }

		/// <param name="bufferSize">To be added.</param>
		/// <param name="inputStream">To be added.</param>
		/// <param name="outputStream">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static, Export ("getBoundStreamsWithBufferSize:inputStream:outputStream:")]
		void GetBoundStreams (nuint bufferSize, out NSInputStream inputStream, out NSOutputStream outputStream);
#if !WATCHOS

		/// <param name="hostname">To be added.</param>
		/// <param name="port">To be added.</param>
		/// <param name="inputStream">To be added.</param>
		/// <param name="outputStream">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static, Export ("getStreamsToHostWithName:port:inputStream:outputStream:")]
		void GetStreamsToHost (string hostname, nint port, out NSInputStream inputStream, out NSOutputStream outputStream);

#endif
	}


	interface INSStreamDelegate { }


	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface NSStreamDelegate {
		/// <param name="theStream">To be added.</param>
		/// <param name="streamEvent">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("stream:handleEvent:"), EventArgs ("NSStream", XmlDocs = """
			<summary>To be added.</summary>
			<remarks>To be added.</remarks>
			"""), EventName ("OnEvent")]
		void HandleEvent (NSStream theStream, NSStreamEvent streamEvent);
	}


	[BaseType (typeof (NSObject)), Bind ("NSString")]
	[DesignatedDefaultCtor]
	interface NSString2 : NSSecureCoding, NSMutableCopying
#if !WATCHOS
		, CKRecordValue
#endif
#if MONOMAC
		, NSPasteboardReading, NSPasteboardWriting // Documented that it implements NSPasteboard protocols even if header doesn't show it
#endif
		, NSItemProviderReading, NSItemProviderWriting {
		[Export ("initWithData:encoding:")]
		NativeHandle Constructor (NSData data, NSStringEncoding encoding);
#if !WATCHOS

		/// <param name="attributedStringAttributes">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Bind ("sizeWithAttributes:")]
		CGSize StringSize ([NullAllowed] NSDictionary attributedStringAttributes);

#endif
#if !WATCHOS

		/// <param name="size">To be added.</param>
		/// <param name="options">To be added.</param>
		/// <param name="attributes">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Bind ("boundingRectWithSize:options:attributes:")]
		CGRect BoundingRectWithSize (CGSize size, NSStringDrawingOptions options, NSDictionary attributes);

#endif
#if !WATCHOS

		/// <param name="point">To be added.</param>
		/// <param name="attributes">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Bind ("drawAtPoint:withAttributes:")]
		void DrawString (CGPoint point, NSDictionary attributes);

#endif
#if !WATCHOS

		/// <param name="rect">To be added.</param>
		/// <param name="attributes">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Bind ("drawInRect:withAttributes:")]
		void DrawString (CGRect rect, NSDictionary attributes);

#endif
#if !WATCHOS

		/// <param name="rect">To be added.</param>
		/// <param name="options">To be added.</param>
		/// <param name="attributes">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Bind ("drawWithRect:options:attributes:")]
		void DrawString (CGRect rect, NSStringDrawingOptions options, NSDictionary attributes);

#endif

		[Internal]
		[Export ("characterAtIndex:")]
		char _characterAtIndex (nint index);

		[Export ("length")]
		nint Length { get; }

		/// <param name="handle">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Sealed]
		[Export ("isEqualToString:")]
		bool IsEqualTo (IntPtr handle);

		[Export ("compare:")]
		NSComparisonResult Compare (NSString aString);

		[Export ("compare:options:")]
		NSComparisonResult Compare (NSString aString, NSStringCompareOptions mask);

		[Export ("compare:options:range:")]
		NSComparisonResult Compare (NSString aString, NSStringCompareOptions mask, NSRange range);

		[Export ("compare:options:range:locale:")]
		NSComparisonResult Compare (NSString aString, NSStringCompareOptions mask, NSRange range, [NullAllowed] NSLocale locale);

		[Export ("stringByReplacingCharactersInRange:withString:")]
		NSString Replace (NSRange range, NSString replacement);

		[Export ("commonPrefixWithString:options:")]
		NSString CommonPrefix (NSString aString, NSStringCompareOptions options);

		// start methods from NSStringPathExtensions category

		[Static]
		[Export ("pathWithComponents:")]
		string [] PathWithComponents (string [] components);

		[Export ("pathComponents")]
		string [] PathComponents { get; }

		/// <summary>Whether this is an absolute path.</summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		[Export ("isAbsolutePath")]
		bool IsAbsolutePath { get; }

		[Export ("lastPathComponent")]
		NSString LastPathComponent { get; }

		[Export ("stringByDeletingLastPathComponent")]
		NSString DeleteLastPathComponent ();

		[Export ("stringByAppendingPathComponent:")]
		NSString AppendPathComponent (NSString str);

		[Export ("pathExtension")]
		NSString PathExtension { get; }

		[Export ("stringByDeletingPathExtension")]
		NSString DeletePathExtension ();

		[Export ("stringByAppendingPathExtension:")]
		NSString AppendPathExtension (NSString str);

		[Export ("stringByAbbreviatingWithTildeInPath")]
		NSString AbbreviateTildeInPath ();

		[Export ("stringByExpandingTildeInPath")]
		NSString ExpandTildeInPath ();

		[Export ("stringByStandardizingPath")]
#if XAMCORE_5_0
		NSString StandardizePath ();
#else
		[Obsolete ("Use 'StandardizePath' instead.")]
		[EditorBrowsable (EditorBrowsableState.Never)]
		NSString StandarizePath ();

		[Wrap ("StandarizePath ()")]
		NSString StandardizePath ();
#endif

		[Export ("stringByResolvingSymlinksInPath")]
		NSString ResolveSymlinksInPath ();

		[Export ("stringsByAppendingPaths:")]
		string [] AppendPaths (string [] paths);

		// end methods from NSStringPathExtensions category

		[Export ("capitalizedStringWithLocale:")]
		string Capitalize ([NullAllowed] NSLocale locale);

		[Export ("lowercaseStringWithLocale:")]
		string ToLower (NSLocale locale);

		[Export ("uppercaseStringWithLocale:")]
		string ToUpper (NSLocale locale);

		[MacCatalyst (13, 1)]
		[Export ("containsString:")]
		bool Contains (NSString str);

		[MacCatalyst (13, 1)]
		[Export ("localizedCaseInsensitiveContainsString:")]
		bool LocalizedCaseInsensitiveContains (NSString str);

		[MacCatalyst (13, 1)]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Static, Export ("stringEncodingForData:encodingOptions:convertedString:usedLossyConversion:")]
		nuint DetectStringEncoding (NSData rawData, NSDictionary options, out string convertedString, out bool usedLossyConversion);

		/// <param name="rawData">To be added.</param>
		/// <param name="options">To be added.</param>
		/// <param name="convertedString">To be added.</param>
		/// <param name="usedLossyConversion">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static, Wrap ("DetectStringEncoding(rawData,options.GetDictionary ()!, out convertedString, out usedLossyConversion)")]
		nuint DetectStringEncoding (NSData rawData, EncodingDetectionOptions options, out string convertedString, out bool usedLossyConversion);

		[MacCatalyst (13, 1)]
		[Internal, Field ("NSStringEncodingDetectionSuggestedEncodingsKey")]
		NSString EncodingDetectionSuggestedEncodingsKey { get; }

		[MacCatalyst (13, 1)]
		[Internal, Field ("NSStringEncodingDetectionDisallowedEncodingsKey")]
		NSString EncodingDetectionDisallowedEncodingsKey { get; }

		[MacCatalyst (13, 1)]
		[Internal, Field ("NSStringEncodingDetectionUseOnlySuggestedEncodingsKey")]
		NSString EncodingDetectionUseOnlySuggestedEncodingsKey { get; }

		[MacCatalyst (13, 1)]
		[Internal, Field ("NSStringEncodingDetectionAllowLossyKey")]
		NSString EncodingDetectionAllowLossyKey { get; }

		[MacCatalyst (13, 1)]
		[Internal, Field ("NSStringEncodingDetectionFromWindowsKey")]
		NSString EncodingDetectionFromWindowsKey { get; }

		[MacCatalyst (13, 1)]
		[Internal, Field ("NSStringEncodingDetectionLossySubstitutionKey")]
		NSString EncodingDetectionLossySubstitutionKey { get; }

		[MacCatalyst (13, 1)]
		[Internal, Field ("NSStringEncodingDetectionLikelyLanguageKey")]
		NSString EncodingDetectionLikelyLanguageKey { get; }

		[Export ("lineRangeForRange:")]
		NSRange LineRangeForRange (NSRange range);

		/// <param name="startPtr">To be added.</param>
		/// <param name="lineEndPtr">To be added.</param>
		/// <param name="contentsEndPtr">To be added.</param>
		/// <param name="range">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("getLineStart:end:contentsEnd:forRange:")]
		void GetLineStart (out nuint startPtr, out nuint lineEndPtr, out nuint contentsEndPtr, NSRange range);

		/// <param name="width">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("variantFittingPresentationWidth:")]
		NSString GetVariantFittingPresentationWidth (nint width);

		[MacCatalyst (13, 1)]
		[Export ("localizedStandardContainsString:")]
		bool LocalizedStandardContainsString (NSString str);

		[MacCatalyst (13, 1)]
		[Export ("localizedStandardRangeOfString:")]
		NSRange LocalizedStandardRangeOfString (NSString str);

		[MacCatalyst (13, 1)]
		[Export ("localizedUppercaseString")]
		NSString LocalizedUppercaseString { get; }

		[MacCatalyst (13, 1)]
		[Export ("localizedLowercaseString")]
		NSString LocalizedLowercaseString { get; }

		[MacCatalyst (13, 1)]
		[Export ("localizedCapitalizedString")]
		NSString LocalizedCapitalizedString { get; }

		[MacCatalyst (13, 1)]
		[Export ("stringByApplyingTransform:reverse:")]
		[return: NullAllowed]
		NSString TransliterateString (NSString transform, bool reverse);

		[Export ("hasPrefix:")]
		bool HasPrefix (NSString prefix);

		[Export ("hasSuffix:")]
		bool HasSuffix (NSString suffix);

		// UNUserNotificationCenterSupport category
		/// <param name="key">To be added.</param>
		/// <param name="arguments">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[NoTV]
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("localizedUserNotificationStringForKey:arguments:")]
		NSString GetLocalizedUserNotificationString (NSString key, [Params][NullAllowed] NSObject [] arguments);

		/// <param name="paragraphStartPosition">To be added.</param>
		/// <param name="paragraphEndPosition">To be added.</param>
		/// <param name="contentsEndPosition">To be added.</param>
		/// <param name="range">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("getParagraphStart:end:contentsEnd:forRange:")]
		void GetParagraphPositions (out nuint paragraphStartPosition, out nuint paragraphEndPosition, out nuint contentsEndPosition, NSRange range);

		[Export ("paragraphRangeForRange:")]
		NSRange GetParagraphRange (NSRange range);

		[Export ("componentsSeparatedByString:")]
		NSString [] SeparateComponents (NSString separator);

		[Export ("componentsSeparatedByCharactersInSet:")]
		NSString [] SeparateComponents (NSCharacterSet separator);

		// From the NSItemProviderReading protocol

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("readableTypeIdentifiersForItemProvider", ArgumentSemantic.Copy)]
		new string [] ReadableTypeIdentifiers { get; }

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("objectWithItemProviderData:typeIdentifier:error:")]
		[return: NullAllowed]
		new NSString GetObject (NSData data, string typeIdentifier, [NullAllowed] out NSError outError);

		// From the NSItemProviderWriting protocol
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("writableTypeIdentifiersForItemProvider", ArgumentSemantic.Copy)]
		new string [] WritableTypeIdentifiers { get; }
	}


	/// <summary>A <see cref="Foundation.DictionaryContainer" /> containing hints for detecting the encoding of an <see cref="Foundation.NSString" />.</summary>
	/// <remarks>To be added.</remarks>
	[StrongDictionary ("NSString")]
	interface EncodingDetectionOptions {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		NSStringEncoding [] EncodingDetectionSuggestedEncodings { get; set; }
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		NSStringEncoding [] EncodingDetectionDisallowedEncodings { get; set; }
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		bool EncodingDetectionUseOnlySuggestedEncodings { get; set; }
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		bool EncodingDetectionAllowLossy { get; set; }
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		bool EncodingDetectionFromWindows { get; set; }
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		NSString EncodingDetectionLossySubstitution { get; set; }
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		NSString EncodingDetectionLikelyLanguage { get; set; }
	}


	[BaseType (typeof (NSString))]
	// hack: it seems that generator.cs can't track NSCoding correctly ? maybe because the type is named NSString2 at that time
	interface NSMutableString : NSCoding {
		/// <param name="capacity">Initial capacity for the mutable string.</param>
		/// <summary>Creates a new mutable string with the specified initial capacity.</summary>
		/// <remarks>
		///         </remarks>
		[Export ("initWithCapacity:")]
		NativeHandle Constructor (nint capacity);

		/// <param name="str">String to insert.</param>
		/// <param name="index">Position in the mutable string where the string will be inserted.</param>
		/// <summary>Inserts a string into the mutable string.</summary>
		/// <remarks>
		///         </remarks>
		[PreSnippet ("Check (index);", Optimizable = true)]
		[Export ("insertString:atIndex:")]
		void Insert (NSString str, nint index);

		[PreSnippet ("Check (range);", Optimizable = true)]
		[Export ("deleteCharactersInRange:")]
		void DeleteCharacters (NSRange range);

		[Export ("appendString:")]
		void Append (NSString str);

		[Export ("setString:")]
		void SetString (NSString str);

		[PreSnippet ("Check (range);", Optimizable = true)]
		[Export ("replaceOccurrencesOfString:withString:options:range:")]
#if XAMCORE_5_0
		nuint ReplaceOccurrences (NSString target, NSString replacement, NSStringCompareOptions options, NSRange range);
#else
		[Obsolete ("Use 'ReplaceOccurrences' instead.")]
		[EditorBrowsable (EditorBrowsableState.Never)]
		nuint ReplaceOcurrences (NSString target, NSString replacement, NSStringCompareOptions options, NSRange range);

		[Wrap ("ReplaceOcurrences (target, replacement, options, range)")]
		nuint ReplaceOccurrences (NSString target, NSString replacement, NSStringCompareOptions options, NSRange range);
#endif

		[MacCatalyst (13, 1)]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("applyTransform:reverse:range:updatedRange:")]
		bool ApplyTransform (NSString transform, bool reverse, NSRange range, out NSRange resultingRange);

		/// <param name="transform">To be added.</param>
		///         <param name="reverse">To be added.</param>
		///         <param name="range">To be added.</param>
		///         <param name="resultingRange">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Wrap ("ApplyTransform (transform.GetConstant ()!, reverse, range, out resultingRange)")]
		bool ApplyTransform (NSStringTransform transform, bool reverse, NSRange range, out NSRange resultingRange);

		[Export ("replaceCharactersInRange:withString:")]
		void ReplaceCharactersInRange (NSRange range, NSString aString);
	}


	/// <summary>Defines static methods for URL encoding and escaping.</summary>
	/// <remarks>To be added.</remarks>
	[Category, BaseType (typeof (NSString))]
	partial interface NSUrlUtilities_NSString {
		/// <param name="allowedCharacters">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("stringByAddingPercentEncodingWithAllowedCharacters:")]
		NSString CreateStringByAddingPercentEncoding (NSCharacterSet allowedCharacters);

		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("stringByRemovingPercentEncoding")]
		NSString CreateStringByRemovingPercentEncoding ();

		/// <param name="enc">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("stringByAddingPercentEscapesUsingEncoding:")]
		NSString CreateStringByAddingPercentEscapes (NSStringEncoding enc);

		/// <param name="enc">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("stringByReplacingPercentEscapesUsingEncoding:")]
		NSString CreateStringByReplacingPercentEscapes (NSStringEncoding enc);
	}


	[BaseType (typeof (NSStream))]
	[DefaultCtorVisibility (Visibility.Protected)]
	interface NSInputStream {
		[Export ("hasBytesAvailable")]
		bool HasBytesAvailable ();

		[Export ("initWithFileAtPath:")]
		NativeHandle Constructor (string path);

		[DesignatedInitializer]
		[Export ("initWithData:")]
		NativeHandle Constructor (NSData data);

		[DesignatedInitializer]
		[Export ("initWithURL:")]
		NativeHandle Constructor (NSUrl url);

		[Static]
		[Export ("inputStreamWithData:")]
		NSInputStream FromData (NSData data);

		[Static]
		[Export ("inputStreamWithFileAtPath:")]
		NSInputStream FromFile (string path);

		[Static]
		[Export ("inputStreamWithURL:")]
		NSInputStream FromUrl (NSUrl url);

		[return: NullAllowed]
		[Protected]
		[Export ("propertyForKey:"), Override]
		NSObject GetProperty (NSString key);

		[Protected]
		[Export ("setProperty:forKey:"), Override]
		bool SetProperty ([NullAllowed] NSObject property, NSString key);
	}


	//
	// We expose NSString versions of these methods because it could
	// avoid an extra lookup in cases where there is a large volume of
	// calls being made and the keys are mostly tokens
	//
	[BaseType (typeof (NSObject)), Bind ("NSObject")]
	interface NSObject2 : NSObjectProtocol {

		// those are to please the compiler while creating the definition .dll
		// but, for the final binary, we'll be using manually bounds alternatives
		// not the generated code
#pragma warning disable 108
		[Manual]
		[Export ("conformsToProtocol:")]
		bool ConformsToProtocol (NativeHandle /* Protocol */ aProtocol);

		[Manual]
		[Export ("retain")]
		NSObject DangerousRetain ();

		[Manual]
		[Export ("release")]
		void DangerousRelease ();

		[Manual]
		[Export ("autorelease")]
		NSObject DangerousAutorelease ();
#pragma warning restore 108

		[Export ("doesNotRecognizeSelector:")]
		void DoesNotRecognizeSelector (Selector sel);

		[Export ("observeValueForKeyPath:ofObject:change:context:")]
		void ObserveValue (NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context);

		[Export ("addObserver:forKeyPath:options:context:")]
		void AddObserver (NSObject observer, NSString keyPath, NSKeyValueObservingOptions options, IntPtr context);

		[Wrap ("AddObserver (observer, (NSString) keyPath, options, context)")]
		void AddObserver (NSObject observer, string keyPath, NSKeyValueObservingOptions options, IntPtr context);

		[Export ("removeObserver:forKeyPath:context:")]
		void RemoveObserver (NSObject observer, NSString keyPath, IntPtr context);

		[Wrap ("RemoveObserver (observer, (NSString) keyPath, context)")]
		void RemoveObserver (NSObject observer, string keyPath, IntPtr context);

		[Export ("removeObserver:forKeyPath:")]
		void RemoveObserver (NSObject observer, NSString keyPath);

		[Wrap ("RemoveObserver (observer, (NSString) keyPath)")]
		void RemoveObserver (NSObject observer, string keyPath);

		[Export ("willChangeValueForKey:")]
		void WillChangeValue (string forKey);

		[Export ("didChangeValueForKey:")]
		void DidChangeValue (string forKey);

		[Export ("willChange:valuesAtIndexes:forKey:")]
		void WillChange (NSKeyValueChange changeKind, NSIndexSet indexes, NSString forKey);

		[Export ("didChange:valuesAtIndexes:forKey:")]
		void DidChange (NSKeyValueChange changeKind, NSIndexSet indexes, NSString forKey);

		[Export ("willChangeValueForKey:withSetMutation:usingObjects:")]
		void WillChange (NSString forKey, NSKeyValueSetMutationKind mutationKind, NSSet objects);

		[Export ("didChangeValueForKey:withSetMutation:usingObjects:")]
		void DidChange (NSString forKey, NSKeyValueSetMutationKind mutationKind, NSSet objects);

		[Static, Export ("keyPathsForValuesAffectingValueForKey:")]
		NSSet GetKeyPathsForValuesAffecting (NSString key);

		[Static, Export ("automaticallyNotifiesObserversForKey:")]
		bool AutomaticallyNotifiesObserversForKey (string key);

		[Export ("valueForKey:")]
		[MarshalNativeExceptions]
		NSObject ValueForKey (NSString key);

		[Export ("setValue:forKey:")]
		void SetValueForKey (NSObject value, NSString key);

		[Export ("valueForKeyPath:")]
		NSObject ValueForKeyPath (NSString keyPath);

		[Export ("setValue:forKeyPath:")]
		void SetValueForKeyPath (NSObject value, NSString keyPath);

		[Export ("valueForUndefinedKey:")]
		NSObject ValueForUndefinedKey (NSString key);

		[Export ("setValue:forUndefinedKey:")]
		void SetValueForUndefinedKey (NSObject value, NSString undefinedKey);

		[Export ("setNilValueForKey:")]
		void SetNilValueForKey (NSString key);

		[Export ("dictionaryWithValuesForKeys:")]
		NSDictionary GetDictionaryOfValuesFromKeys (NSString [] keys);

		[Export ("setValuesForKeysWithDictionary:")]
		void SetValuesForKeysWithDictionary (NSDictionary keyedValues);

		[Field ("NSKeyValueChangeKindKey")]
		NSString ChangeKindKey { get; }

		[Field ("NSKeyValueChangeNewKey")]
		NSString ChangeNewKey { get; }

		[Field ("NSKeyValueChangeOldKey")]
		NSString ChangeOldKey { get; }

		[Field ("NSKeyValueChangeIndexesKey")]
		NSString ChangeIndexesKey { get; }

		[Field ("NSKeyValueChangeNotificationIsPriorKey")]
		NSString ChangeNotificationIsPriorKey { get; }
#if !WATCHOS

		// Cocoa Bindings added by Kenneth J. Pouncey 2010/11/17
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("valueClassForBinding:")]
		Class GetBindingValueClass (NSString binding);

#endif
#if !WATCHOS

		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("exposedBindings")]
		NSString [] ExposedBindings { get; }

#endif
#if !WATCHOS

		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("bind:toObject:withKeyPath:options:")]
		void Bind (NSString binding, NSObject observable, string keyPath, [NullAllowed] NSDictionary options);

#endif
#if !WATCHOS

		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Wrap ("Bind (binding, observable, keyPath, options.GetDictionary ())")]
		void Bind (NSString binding, NSObject observable, string keyPath, [NullAllowed] NSBindingOptions options);

#endif
#if !WATCHOS

		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("unbind:")]
		void Unbind (NSString binding);

#endif
#if !WATCHOS

		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("infoForBinding:")]
		NSDictionary GetBindingInfo (NSString binding);

#endif
#if !WATCHOS

		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("optionDescriptionsForBinding:")]
		NSObject [] GetBindingOptionDescriptions (NSString aBinding);

#endif
#if !WATCHOS

		// NSPlaceholders (informal) protocol
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Deprecated (PlatformName.MacOSX, 10, 15)]
		[Static]
		[Export ("defaultPlaceholderForMarker:withBinding:")]
		NSObject GetDefaultPlaceholder (NSObject marker, NSString binding);

#endif
#if !WATCHOS

		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Deprecated (PlatformName.MacOSX, 10, 15)]
		[Static]
		[Export ("setDefaultPlaceholder:forMarker:withBinding:")]
		void SetDefaultPlaceholder (NSObject placeholder, NSObject marker, NSString binding);

#endif
#if !WATCHOS

		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Deprecated (PlatformName.MacOSX, message: "Now on 'NSEditor' protocol.")]
		[Export ("objectDidEndEditing:")]
		void ObjectDidEndEditing (NSObject editor);

#endif
#if !WATCHOS

		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Deprecated (PlatformName.MacOSX, message: "Now on 'NSEditor' protocol.")]
		[Export ("commitEditing")]
		bool CommitEditing ();

#endif
#if !WATCHOS

		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Deprecated (PlatformName.MacOSX, message: "Now on 'NSEditor' protocol.")]
		[Export ("commitEditingWithDelegate:didCommitSelector:contextInfo:")]
		void CommitEditing (NSObject objDelegate, Selector didCommitSelector, IntPtr contextInfo);

#endif

		[Export ("methodForSelector:")]
		IntPtr GetMethodForSelector (Selector sel);

		[PreSnippet ("if (!(this is INSCopying)) throw new InvalidOperationException (\"Type does not conform to NSCopying\");", Optimizable = true)]
		[Export ("copy")]
		[return: Release ()]
		NSObject Copy ();

		[PreSnippet ("if (!(this is INSMutableCopying)) throw new InvalidOperationException (\"Type does not conform to NSMutableCopying\");", Optimizable = true)]
		[Export ("mutableCopy")]
		[return: Release ()]
		NSObject MutableCopy ();

		//
		// Extra Perform methods, with selectors
		//
		[Export ("performSelector:withObject:afterDelay:inModes:")]
		void PerformSelector (Selector selector, [NullAllowed] NSObject withObject, double afterDelay, NSString [] nsRunLoopModes);

		[Export ("performSelector:withObject:afterDelay:")]
		void PerformSelector (Selector selector, [NullAllowed] NSObject withObject, double delay);

		[Export ("performSelector:onThread:withObject:waitUntilDone:")]
		void PerformSelector (Selector selector, NSThread onThread, [NullAllowed] NSObject withObject, bool waitUntilDone);

		[Export ("performSelector:onThread:withObject:waitUntilDone:modes:")]
		void PerformSelector (Selector selector, NSThread onThread, [NullAllowed] NSObject withObject, bool waitUntilDone, [NullAllowed] NSString [] nsRunLoopModes);

		[Static, Export ("cancelPreviousPerformRequestsWithTarget:")]
		void CancelPreviousPerformRequest (NSObject aTarget);

		[Static, Export ("cancelPreviousPerformRequestsWithTarget:selector:object:")]
		void CancelPreviousPerformRequest (NSObject aTarget, Selector selector, [NullAllowed] NSObject argument);
#if !WATCHOS

		[MacCatalyst (13, 1)]
		[Export ("prepareForInterfaceBuilder")]
		void PrepareForInterfaceBuilder ();

#endif
#if !WATCHOS

		[MacCatalyst (13, 1)]
#if MONOMAC
		// comes from NSNibAwaking category and does not requires calling super
#else
		[RequiresSuper] // comes from UINibLoadingAdditions category - which is decorated
#endif
		[Export ("awakeFromNib")]
		void AwakeFromNib ();

#endif
#if !WATCHOS

		[NoMac]
		[MacCatalyst (13, 1)]
		[Export ("accessibilityRespondsToUserInteraction")]
		bool AccessibilityRespondsToUserInteraction { get; set; }

#endif
#if !WATCHOS

		[NoMac]
		[MacCatalyst (13, 1)]
		[Export ("accessibilityUserInputLabels", ArgumentSemantic.Strong)]
		string [] AccessibilityUserInputLabels { get; set; }

#endif
#if !WATCHOS

		[NoMac]
		[MacCatalyst (13, 1)]
		[Export ("accessibilityAttributedUserInputLabels", ArgumentSemantic.Copy)]
		NSAttributedString [] AccessibilityAttributedUserInputLabels { get; set; }

#endif
#if !WATCHOS

		[NoMac]
		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("accessibilityTextualContext", ArgumentSemantic.Strong)]
		string AccessibilityTextualContext { get; set; }

#endif
	}


	/// <summary>Base-level object protocol required to be considered a first class Objective-C object.</summary>
	/// <remarks>To be added.</remarks>
	[Protocol (Name = "NSObject")] // exists both as a type and a protocol in ObjC, Swift uses NSObjectProtocol
	interface NSObjectProtocol {

		/// <summary>To be added.</summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[Export ("description")]
		string Description { get; }

		[Export ("debugDescription")]
		string DebugDescription { get; }

		/// <summary>To be added.</summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("superclass")]
		Class Superclass { get; }

		// defined multiple times (method, property and even static), one (not static) is required
		// and that match Apple's documentation
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("hash")]
		nuint GetNativeHash ();

		/// <param name="anObject">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("isEqual:")]
		bool IsEqual ([NullAllowed] NSObject anObject);

		/// <summary>To be added.</summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("class")]
		Class Class { get; }

		/// <summary>To be added.</summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Never)]
		[Export ("self")]
		[Transient]
		NSObject Self { get; }

		/// <param name="aSelector">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[Export ("performSelector:")]
		NSObject PerformSelector (Selector aSelector);

		/// <param name="aSelector">To be added.</param>
		/// <param name="anObject">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[Export ("performSelector:withObject:")]
		NSObject PerformSelector (Selector aSelector, [NullAllowed] NSObject anObject);

		/// <param name="aSelector">To be added.</param>
		/// <param name="object1">To be added.</param>
		/// <param name="object2">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[Export ("performSelector:withObject:withObject:")]
		NSObject PerformSelector (Selector aSelector, [NullAllowed] NSObject object1, [NullAllowed] NSObject object2);

		/// <summary>To be added.</summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("isProxy")]
		bool IsProxy { get; }

		/// <param name="aClass">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("isKindOfClass:")]
		bool IsKindOfClass ([NullAllowed] Class aClass);

		/// <param name="aClass">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("isMemberOfClass:")]
		bool IsMemberOfClass ([NullAllowed] Class aClass);

		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("conformsToProtocol:")]
		bool ConformsToProtocol ([NullAllowed] NativeHandle /* Protocol */ aProtocol);

		/// <param name="sel">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("respondsToSelector:")]
		bool RespondsToSelector ([NullAllowed] Selector sel);

		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("retain")]
		NSObject DangerousRetain ();

		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("release")]
		void DangerousRelease ();

		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("autorelease")]
		NSObject DangerousAutorelease ();

		/// <summary>To be added.</summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[Export ("retainCount")]
		nuint RetainCount { get; }

		/// <summary>To be added.</summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		[Abstract]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("zone")]
		NSZone Zone { get; }
	}


	[BaseType (typeof (NSObject))]
	interface NSOperation {
		[Export ("start")]
		void Start ();

		[Export ("main")]
		void Main ();

		/// <summary>Whether this NSOperation has been cancelled.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isCancelled")]
		bool IsCancelled { get; }

		[Export ("cancel")]
		void Cancel ();

		/// <summary>Whether this operation is currently executing.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isExecuting")]
		bool IsExecuting { get; }

		/// <summary>Whether this operation is done executing.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isFinished")]
		bool IsFinished { get; }

		/// <summary>Whether this operation runs asynchronously.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isConcurrent")]
		bool IsConcurrent { get; }

		/// <summary>Whether this operation can be performed now.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isReady")]
		bool IsReady { get; }

		[Export ("addDependency:")]
		[PostGet ("Dependencies")]
		void AddDependency (NSOperation op);

		[Export ("removeDependency:")]
		[PostGet ("Dependencies")]
		void RemoveDependency (NSOperation op);

		[Export ("dependencies")]
		NSOperation [] Dependencies { get; }

		[NullAllowed]
		[Export ("completionBlock", ArgumentSemantic.Copy)]
		Action CompletionBlock { get; set; }

		/// <summary>Blocks the current thread until this operation finishes.</summary>
		[Export ("waitUntilFinished")]
		void WaitUntilFinished ();

		[Export ("threadPriority")]
		[Deprecated (PlatformName.iOS, 8, 0)]
		[Deprecated (PlatformName.TvOS, 9, 0)]
		[Deprecated (PlatformName.MacOSX, 10, 10)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1)]
		double ThreadPriority { get; set; }

		//Detected properties
		[Export ("queuePriority")]
		NSOperationQueuePriority QueuePriority { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("asynchronous")]
		bool Asynchronous { [Bind ("isAsynchronous")] get; }

		[MacCatalyst (13, 1)]
		[Export ("qualityOfService")]
		NSQualityOfService QualityOfService { get; set; }

		[MacCatalyst (13, 1)]
		[NullAllowed] // by default this property is null
		[Export ("name")]
		string Name { get; set; }
	}


	[BaseType (typeof (NSObject))]
	interface NSOperationQueue : NSProgressReporting {
		[Export ("addOperation:")]
		[PostGet ("Operations")]
		void AddOperation ([NullAllowed] NSOperation op);

		[Export ("addOperations:waitUntilFinished:")]
		[PostGet ("Operations")]
		void AddOperations ([NullAllowed] NSOperation [] operations, bool waitUntilFinished);

		[Export ("addOperationWithBlock:")]
		[PostGet ("Operations")]
		void AddOperation (/* non null */ Action operation);

		[Deprecated (PlatformName.MacOSX, 10, 15, 0, message: "This API should not be used as it is subject to race conditions. If synchronization is needed use 'AddBarrier' instead.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "This API should not be used as it is subject to race conditions. If synchronization is needed use 'AddBarrier' instead.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "This API should not be used as it is subject to race conditions. If synchronization is needed use 'AddBarrier' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "This API should not be used as it is subject to race conditions. If synchronization is needed use 'AddBarrier' instead.")]
		[Export ("operations")]
		NSOperation [] Operations { get; }

		[Deprecated (PlatformName.MacOSX, 10, 15)]
		[Deprecated (PlatformName.iOS, 13, 0)]
		[Deprecated (PlatformName.TvOS, 13, 0)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1)]
		[Export ("operationCount")]
		nint OperationCount { get; }

		[MacCatalyst (13, 1)]
		[Export ("addBarrierBlock:")]
		void AddBarrier (Action barrier);

		[Export ("name")]
		string Name { get; set; }

		[Export ("cancelAllOperations")]
		[PostGet ("Operations")]
		void CancelAllOperations ();

		[Export ("waitUntilAllOperationsAreFinished")]
		void WaitUntilAllOperationsAreFinished ();

		[Static]
		[Export ("currentQueue", ArgumentSemantic.Strong)]
		NSOperationQueue CurrentQueue { get; }

		[Static]
		[Export ("mainQueue", ArgumentSemantic.Strong)]
		NSOperationQueue MainQueue { get; }

		//Detected properties
		[Export ("maxConcurrentOperationCount")]
		nint MaxConcurrentOperationCount { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("suspended")]
		bool Suspended { [Bind ("isSuspended")] get; set; }

		[MacCatalyst (13, 1)]
		[Export ("qualityOfService")]
		NSQualityOfService QualityOfService { get; set; }

		[NullAllowed]
		[MacCatalyst (13, 1)]
		[Export ("underlyingQueue", ArgumentSemantic.UnsafeUnretained)]
		DispatchQueue UnderlyingQueue { get; set; }

	}


	interface NSOrderedSet<TKey> : NSOrderedSet { }


	[BaseType (typeof (NSObject))]
	[DesignatedDefaultCtor]
	interface NSOrderedSet : NSSecureCoding, NSMutableCopying {
		[Export ("initWithObject:")]
		NativeHandle Constructor (NSObject start);

		[Export ("initWithArray:"), Internal]
		NativeHandle Constructor (NSArray array);

		[Export ("initWithSet:")]
		NativeHandle Constructor (NSSet source);

		[Export ("initWithOrderedSet:")]
		NativeHandle Constructor (NSOrderedSet source);

		[Export ("count")]
		nint Count { get; }

		[Internal]
		[Sealed]
		[Export ("objectAtIndex:")]
		IntPtr _GetObject (nint idx);

		[Export ("objectAtIndex:"), Internal]
		NSObject GetObject (nint idx);

		[Export ("array"), Internal]
		IntPtr _ToArray ();

		[Internal]
		[Sealed]
		[Export ("indexOfObject:")]
		nint _IndexOf (IntPtr obj);

		[Export ("indexOfObject:")]
		nint IndexOf (NSObject obj);

		[Export ("objectEnumerator"), Internal]
		NSEnumerator _GetEnumerator ();

		[Internal]
		[Sealed]
		[Export ("set")]
		IntPtr _AsSet ();

		[Export ("set")]
		NSSet AsSet ();

		[Internal]
		[Sealed]
		[Export ("containsObject:")]
		bool _Contains (IntPtr obj);

		[Export ("containsObject:")]
		bool Contains (NSObject obj);

		[Internal]
		[Sealed]
		[Export ("firstObject")]
		IntPtr _FirstObject ();

		[Export ("firstObject")]
		[return: NullAllowed]
		NSObject FirstObject ();

		[Internal]
		[Sealed]
		[Export ("lastObject")]
		IntPtr _LastObject ();

		[Export ("lastObject")]
		[return: NullAllowed]
		NSObject LastObject ();

		[Export ("isEqualToOrderedSet:")]
		bool IsEqualToOrderedSet (NSOrderedSet other);

		[Export ("intersectsOrderedSet:")]
		bool Intersects (NSOrderedSet other);

		[Export ("intersectsSet:")]
		bool Intersects (NSSet other);

		[Export ("isSubsetOfOrderedSet:")]
		bool IsSubset (NSOrderedSet other);

		[Export ("isSubsetOfSet:")]
		bool IsSubset (NSSet other);

		[Export ("reversedOrderedSet")]
		NSOrderedSet GetReverseOrderedSet ();

#if false // https://github.com/dotnet/macios/issues/15577
		[Wrap ("Runtime.GetNSObject <NSOrderedCollectionDifference> (_GetDifference (other, options))")]
		[return: NullAllowed]
		NSOrderedCollectionDifference GetDifference (NSOrderedSet other, NSOrderedCollectionDifferenceCalculationOptions options);

		[Internal]
		[Export ("differenceFromOrderedSet:withOptions:")]
		IntPtr _GetDifference (NSOrderedSet other, NSOrderedCollectionDifferenceCalculationOptions options);

		[Wrap ("Runtime.GetNSObject <NSOrderedCollectionDifference> (_GetDifference (other))")]
		[return: NullAllowed]
		NSOrderedCollectionDifference GetDifference (NSOrderedSet other);

		[Internal]
		[Export ("differenceFromOrderedSet:")]
		IntPtr _GetDifference (NSOrderedSet other);

		[Wrap ("Runtime.GetNSObject <NSOrderedSet> (_GetOrderedSet (difference))")]
		[return: NullAllowed]
		NSOrderedSet GetOrderedSet (NSOrderedCollectionDifference difference);

		[Internal]
		[Export ("orderedSetByApplyingDifference:")]
		[return: NullAllowed]
		IntPtr _GetOrderedSet (NSOrderedCollectionDifference difference);

		[Internal]
		[Export ("differenceFromOrderedSet:withOptions:usingEquivalenceTest:")]
		/* NSOrderedCollectionDifference<NSObject>*/ IntPtr _GetDifference (NSOrderedSet other, NSOrderedCollectionDifferenceCalculationOptions options, /* Func<NSObject, NSObject, bool> */ ref BlockLiteral block);
#endif
	}


	interface NSMutableOrderedSet<TKey> : NSMutableOrderedSet { }


	[BaseType (typeof (NSOrderedSet))]
	[DesignatedDefaultCtor]
	interface NSMutableOrderedSet {
		[Export ("initWithObject:")]
		NativeHandle Constructor (NSObject start);

		[Export ("initWithSet:")]
		NativeHandle Constructor (NSSet source);

		[Export ("initWithOrderedSet:")]
		NativeHandle Constructor (NSOrderedSet source);

		/// <param name="capacity">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[DesignatedInitializer]
		[Export ("initWithCapacity:")]
		NativeHandle Constructor (nint capacity);

		[Export ("initWithArray:"), Internal]
		NativeHandle Constructor (NSArray array);

		[Export ("unionSet:"), Internal]
		void UnionSet (NSSet other);

		[Export ("minusSet:"), Internal]
		void MinusSet (NSSet other);

		[Export ("unionOrderedSet:"), Internal]
		void UnionSet (NSOrderedSet other);

		[Export ("minusOrderedSet:"), Internal]
		void MinusSet (NSOrderedSet other);

		[Internal]
		[Sealed]
		[Export ("insertObject:atIndex:")]
		void _Insert (IntPtr obj, nint atIndex);

		/// <param name="obj">To be added.</param>
		/// <param name="atIndex">To be added.</param>
		/// <summary>Inserts the specified object at the specified index.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("insertObject:atIndex:")]
		void Insert (NSObject obj, nint atIndex);

		/// <param name="index">To be added.</param>
		/// <summary>Removes the object at the specified index.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("removeObjectAtIndex:")]
		void Remove (nint index);

		[Internal]
		[Sealed]
		[Export ("replaceObjectAtIndex:withObject:")]
		void _Replace (nint objectAtIndex, IntPtr newObject);

		/// <param name="objectAtIndex">To be added.</param>
		/// <param name="newObject">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("replaceObjectAtIndex:withObject:")]
		void Replace (nint objectAtIndex, NSObject newObject);

		[Internal]
		[Sealed]
		[Export ("addObject:")]
		void _Add (IntPtr obj);

		[Export ("addObject:")]
		void Add (NSObject obj);

		[Internal]
		[Sealed]
		[Export ("addObjectsFromArray:")]
		void _AddObjects (NSArray source);

		[Export ("addObjectsFromArray:")]
		void AddObjects (NSObject [] source);

		[Internal]
		[Sealed]
		[Export ("insertObjects:atIndexes:")]
		void _InsertObjects (NSArray objects, NSIndexSet atIndexes);

		[Export ("insertObjects:atIndexes:")]
		void InsertObjects (NSObject [] objects, NSIndexSet atIndexes);

		[Export ("removeObjectsAtIndexes:")]
		void RemoveObjects (NSIndexSet indexSet);

		/// <param name="first">To be added.</param>
		/// <param name="second">To be added.</param>
		/// <summary>Exchanges the objects at the specified indices.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("exchangeObjectAtIndex:withObjectAtIndex:")]
		void ExchangeObject (nint first, nint second);

		/// <param name="indexSet">To be added.</param>
		/// <param name="destination">To be added.</param>
		/// <summary>Moves the objects currently at the specified indices so that they start at the specified destination index.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("moveObjectsAtIndexes:toIndex:")]
		void MoveObjects (NSIndexSet indexSet, nint destination);

		[Internal]
		[Sealed]
		[Export ("setObject:atIndex:")]
		void _SetObject (IntPtr obj, nint index);

		/// <param name="obj">To be added.</param>
		/// <param name="index">To be added.</param>
		/// <summary>Appends or replaces the object at the specified index.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("setObject:atIndex:")]
		void SetObject (NSObject obj, nint index);

		[Internal]
		[Sealed]
		[Export ("replaceObjectsAtIndexes:withObjects:")]
		void _ReplaceObjects (NSIndexSet indexSet, NSArray replacementObjects);

		[Export ("replaceObjectsAtIndexes:withObjects:")]
		void ReplaceObjects (NSIndexSet indexSet, NSObject [] replacementObjects);

		[Export ("removeObjectsInRange:")]
		void RemoveObjects (NSRange range);

		[Export ("removeAllObjects")]
		void RemoveAllObjects ();

		[Internal]
		[Sealed]
		[Export ("removeObject:")]
		void _RemoveObject (IntPtr obj);

		[Export ("removeObject:")]
		void RemoveObject (NSObject obj);

		[Internal]
		[Sealed]
		[Export ("removeObjectsInArray:")]
		void _RemoveObjects (NSArray objects);

		[Export ("removeObjectsInArray:")]
		void RemoveObjects (NSObject [] objects);

		[Export ("intersectOrderedSet:")]
		void Intersect (NSOrderedSet intersectWith);

		[Export ("intersectSet:")]
		void Intersect (NSSet intersectWith);

		[Export ("sortUsingComparator:")]
		void Sort (NSComparator comparator);

		[Export ("sortWithOptions:usingComparator:")]
		void Sort (NSSortOptions sortOptions, NSComparator comparator);

		[Export ("sortRange:options:usingComparator:")]
		void SortRange (NSRange range, NSSortOptions sortOptions, NSComparator comparator);

#if false // https://github.com/dotnet/macios/issues/15577
		[Internal]
		[Export ("applyDifference:")]
		void _ApplyDifference (IntPtr difference);

		[Sealed]
		[Export ("applyDifference:")]
		void ApplyDifference (NSOrderedCollectionDifference<NSObject> difference);
#endif
	}


	[BaseType (typeof (NSStream))]
	[DisableDefaultCtor] // crash when used
	interface NSOutputStream {
		[DesignatedInitializer]
		[Export ("initToMemory")]
		NativeHandle Constructor ();

		[Export ("hasSpaceAvailable")]
		bool HasSpaceAvailable ();

		//[Export ("initToBuffer:capacity:")]
		//NativeHandle Constructor (uint8_t  buffer, NSUInteger capacity);

		[Export ("initToFileAtPath:append:")]
		NativeHandle Constructor (string path, bool shouldAppend);

		[Static]
		[Export ("outputStreamToMemory")]
		NSObject OutputStreamToMemory ();

		//[Static]
		//[Export ("outputStreamToBuffer:capacity:")]
		//NSObject OutputStreamToBuffer (uint8_t  buffer, NSUInteger capacity);

		[Static]
		[Export ("outputStreamToFileAtPath:append:")]
		NSOutputStream CreateFile (string path, bool shouldAppend);

		[return: NullAllowed]
		[Protected]
		[Export ("propertyForKey:"), Override]
		NSObject GetProperty (NSString key);

		[Protected]
		[Export ("setProperty:forKey:"), Override]
		bool SetProperty ([NullAllowed] NSObject property, NSString key);
	}


	[BaseType (typeof (NSObject), Name = "NSHTTPCookie")]
	// default 'init' crash both simulator and devices
	[DisableDefaultCtor]
	interface NSHttpCookie {
		[Export ("initWithProperties:")]
		NativeHandle Constructor (NSDictionary properties);

		[Export ("cookieWithProperties:"), Static]
		NSHttpCookie CookieFromProperties (NSDictionary properties);

		[Export ("requestHeaderFieldsWithCookies:"), Static]
		NSDictionary RequestHeaderFieldsWithCookies (NSHttpCookie [] cookies);

		[Export ("cookiesWithResponseHeaderFields:forURL:"), Static]
		NSHttpCookie [] CookiesWithResponseHeaderFields (NSDictionary headerFields, NSUrl url);

		[Export ("properties")]
		NSDictionary Properties { get; }

		[Export ("version")]
		nuint Version { get; }

		[Export ("value")]
		string Value { get; }

		[Export ("expiresDate")]
		NSDate ExpiresDate { get; }

		/// <summary>Whether this cookie should be discarded at the end of the session, ignoring the expiration date.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isSessionOnly")]
		bool IsSessionOnly { get; }

		[Export ("domain")]
		string Domain { get; }

		[Export ("name")]
		string Name { get; }

		[Export ("path")]
		string Path { get; }

		/// <summary>Whether this cookie should only be sent over secure channels.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isSecure")]
		bool IsSecure { get; }

		/// <summary>Whether this cookie should only be sent to HTTP servers per RFC 2965.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isHTTPOnly")]
		bool IsHttpOnly { get; }

		[Export ("comment")]
		string Comment { get; }

		[Export ("commentURL")]
		NSUrl CommentUrl { get; }

		[Export ("portList")]
		NSNumber [] PortList { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookieName")]
		NSString KeyName { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookieValue")]
		NSString KeyValue { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookieOriginURL")]
		NSString KeyOriginUrl { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookieVersion")]
		NSString KeyVersion { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookieDomain")]
		NSString KeyDomain { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookiePath")]
		NSString KeyPath { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookieSecure")]
		NSString KeySecure { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookieExpires")]
		NSString KeyExpires { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookieComment")]
		NSString KeyComment { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookieCommentURL")]
		NSString KeyCommentUrl { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookieDiscard")]
		NSString KeyDiscard { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookieMaximumAge")]
		NSString KeyMaximumAge { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Field ("NSHTTPCookiePort")]
		NSString KeyPort { get; }

		[MacCatalyst (13, 1)]
		[Field ("NSHTTPCookieSameSitePolicy")]
		NSString KeySameSitePolicy { get; }

		[MacCatalyst (13, 1)]
		[Field ("NSHTTPCookieSameSiteLax")]
		NSString KeySameSiteLax { get; }

		[MacCatalyst (13, 1)]
		[Field ("NSHTTPCookieSameSiteStrict")]
		NSString KeySameSiteStrict { get; }

		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("sameSitePolicy")]
		NSString SameSitePolicy { get; }

		[TV (18, 2), iOS (18, 2), MacCatalyst (18, 2), Mac (15, 2)]
		[Field ("NSHTTPCookieSetByJavaScript")]
		NSString KeySetByJavaScript { get; }
	}


	[BaseType (typeof (NSObject), Name = "NSHTTPCookieStorage")]
	// NSHTTPCookieStorage implements a singleton object -> use SharedStorage since 'init' returns NIL
	[DisableDefaultCtor]
	interface NSHttpCookieStorage {
		[Export ("sharedHTTPCookieStorage", ArgumentSemantic.Strong), Static]
		NSHttpCookieStorage SharedStorage { get; }

		[Export ("cookies")]
		NSHttpCookie [] Cookies { get; }

		[Export ("setCookie:")]
		void SetCookie (NSHttpCookie cookie);

		[Export ("deleteCookie:")]
		void DeleteCookie (NSHttpCookie cookie);

		[Export ("cookiesForURL:")]
		NSHttpCookie [] CookiesForUrl (NSUrl url);

		[Export ("setCookies:forURL:mainDocumentURL:")]
		void SetCookies (NSHttpCookie [] cookies, NSUrl forUrl, NSUrl mainDocumentUrl);

		[Export ("cookieAcceptPolicy")]
		NSHttpCookieAcceptPolicy AcceptPolicy { get; set; }

		[Export ("sortedCookiesUsingDescriptors:")]
		NSHttpCookie [] GetSortedCookies (NSSortDescriptor [] sortDescriptors);

		// @required - (void)removeCookiesSinceDate:(NSDate *)date;
		[MacCatalyst (13, 1)]
		[Export ("removeCookiesSinceDate:")]
		void RemoveCookiesSinceDate (NSDate date);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("sharedCookieStorageForGroupContainerIdentifier:")]
		NSHttpCookieStorage GetSharedCookieStorage (string groupContainerIdentifier);

		[MacCatalyst (13, 1)]
		[Async (XmlDocs = """
			<param name="task">To be added.</param>
			<summary>To be added.</summary>
			<returns>
			          <para class="improve-task-t-return-type-description">A task that represents the asynchronous GetCookiesForTask operation.  The value of the TResult parameter is of type System.Action&lt;Foundation.NSHttpCookie[]&gt;.</para>
			        </returns>
			<remarks>
			          <para copied="true">The GetCookiesForTaskAsync method is suitable to be used with C# async by returning control to the caller with a Task representing the operation.</para>
			          <para copied="true">To be added.</para>
			        </remarks>
			""")]
		[Export ("getCookiesForTask:completionHandler:")]
		void GetCookiesForTask (NSUrlSessionTask task, Action<NSHttpCookie []> completionHandler);

		[MacCatalyst (13, 1)]
		[Export ("storeCookies:forTask:")]
		void StoreCookies (NSHttpCookie [] cookies, NSUrlSessionTask task);

		[Notification]
		[Field ("NSHTTPCookieManagerAcceptPolicyChangedNotification")]
		NSString CookiesChangedNotification { get; }

		[Deprecated (PlatformName.MacOSX, 10, 11, "This notification is never posted.")]
		[Deprecated (PlatformName.iOS, 9, 0, "This notification is never posted.")]
		[Deprecated (PlatformName.TvOS, 9, 0, "This notification is never posted.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, "This notification is never posted.")]
		[Notification]
		[Field ("NSHTTPCookieManagerCookiesChangedNotification")]
		NSString AcceptPolicyChangedNotification { get; }
	}


	[BaseType (typeof (NSUrlResponse), Name = "NSHTTPURLResponse")]
	interface NSHttpUrlResponse {
		/// <param name="url">To be added.</param>
		/// <param name="mimetype">To be added.</param>
		/// <param name="expectedContentLength">To be added.</param>
		/// <param name="textEncodingName">
		///           <para>To be added.</para>
		///           <para tool="nullallowed">This parameter can be <see langword="null" />.</para>
		///         </param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("initWithURL:MIMEType:expectedContentLength:textEncodingName:")]
		NativeHandle Constructor (NSUrl url, string mimetype, nint expectedContentLength, [NullAllowed] string textEncodingName);

		/// <param name="url">To be added.</param>
		/// <param name="statusCode">To be added.</param>
		/// <param name="httpVersion">To be added.</param>
		/// <param name="headerFields">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("initWithURL:statusCode:HTTPVersion:headerFields:")]
		NativeHandle Constructor (NSUrl url, nint statusCode, [NullAllowed] string httpVersion, [NullAllowed] NSDictionary headerFields);

		[Export ("statusCode")]
		nint StatusCode { get; }

		[Export ("allHeaderFields")]
		NSDictionary AllHeaderFields { get; }

		/// <param name="statusCode">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("localizedStringForStatusCode:")]
		[Static]
		string LocalizedStringForStatusCode (nint statusCode);

		[MacCatalyst (13, 1)]
		[Export ("valueForHTTPHeaderField:")]
		[return: NullAllowed]
		string GetHttpHeaderValue (string headerField);
	}


	/// <param name="range">To be added.</param>
	/// <param name="stop">To be added.</param>
	/// <summary>A delegate used to specify the iterator used by <see cref="Foundation.NSIndexSet.EnumerateRanges(Foundation.NSRange,Foundation.NSEnumerationOptions,Foundation.NSRangeIterator)" />.</summary>
	/// <remarks>To be added.</remarks>
	delegate void NSRangeIterator (NSRange range, ref bool stop);


	[BaseType (typeof (NSObject))]
	interface NSIndexSet : NSCoding, NSSecureCoding, NSMutableCopying {
		/// <param name="idx">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Static, Export ("indexSetWithIndex:")]
		NSIndexSet FromIndex (nint idx);

		[Static, Export ("indexSetWithIndexesInRange:")]
		NSIndexSet FromNSRange (NSRange indexRange);

		[Export ("initWithIndex:")]
		NativeHandle Constructor (nuint index);

		[DesignatedInitializer]
		[Export ("initWithIndexSet:")]
		NativeHandle Constructor (NSIndexSet other);

		[Export ("count")]
		nint Count { get; }

		[Export ("isEqualToIndexSet:")]
		bool IsEqual (NSIndexSet other);

		[Export ("firstIndex")]
		nuint FirstIndex { get; }

		[Export ("lastIndex")]
		nuint LastIndex { get; }

		/// <param name="index">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("indexGreaterThanIndex:")]
		nuint IndexGreaterThan (nuint index);

		/// <param name="index">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("indexLessThanIndex:")]
		nuint IndexLessThan (nuint index);

		/// <param name="index">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("indexGreaterThanOrEqualToIndex:")]
		nuint IndexGreaterThanOrEqual (nuint index);

		/// <param name="index">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("indexLessThanOrEqualToIndex:")]
		nuint IndexLessThanOrEqual (nuint index);

		/// <param name="index">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("containsIndex:")]
		bool Contains (nuint index);

		[Export ("containsIndexes:")]
		bool Contains (NSIndexSet indexes);

		[Export ("enumerateRangesUsingBlock:")]
		void EnumerateRanges (NSRangeIterator iterator);

		[Export ("enumerateRangesWithOptions:usingBlock:")]
		void EnumerateRanges (NSEnumerationOptions opts, NSRangeIterator iterator);

		[Export ("enumerateRangesInRange:options:usingBlock:")]
		void EnumerateRanges (NSRange range, NSEnumerationOptions opts, NSRangeIterator iterator);

		[Export ("enumerateIndexesUsingBlock:")]
		void EnumerateIndexes (EnumerateIndexSetCallback enumeratorCallback);

		[Export ("enumerateIndexesWithOptions:usingBlock:")]
		void EnumerateIndexes (NSEnumerationOptions opts, EnumerateIndexSetCallback enumeratorCallback);

		[Export ("enumerateIndexesInRange:options:usingBlock:")]
		void EnumerateIndexes (NSRange range, NSEnumerationOptions opts, EnumerateIndexSetCallback enumeratorCallback);
	}


	interface INSItemProviderReading { }


	[MacCatalyst (13, 1)]
	[Protocol]
	interface NSItemProviderReading {
		// This static method has to be implemented on each class that implements
		// this, this is not a capability that exists in C#.
		// We are inlining these on each class that implements NSItemProviderReading
		// for the sake of the method being callable from C#, for user code, the
		// user needs to manually [Export] the selector on a static method, like
		// they do for the "layer" property on CALayer subclasses.
		//
		/// <summary>To be added.</summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		[Static, Abstract]
		[Export ("readableTypeIdentifiersForItemProvider", ArgumentSemantic.Copy)]
		string [] ReadableTypeIdentifiers { get; }

		/// <param name="data">To be added.</param>
		/// <param name="typeIdentifier">To be added.</param>
		/// <param name="outError">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Static, Abstract]
		[Export ("objectWithItemProviderData:typeIdentifier:error:")]
		[return: NullAllowed]
		INSItemProviderReading GetObject (NSData data, string typeIdentifier, [NullAllowed] out NSError outError);
	}


	interface INSItemProviderWriting { }


#if WATCHOS
	/// <summary>Provides data representations of a Foundation object.</summary>
#else
	/// <summary>Interface used by <see cref="Foundation.NSItemProvider" /> for retrieving data from an object.</summary>
	/// <remarks>To be added.</remarks>
#endif
	[MacCatalyst (13, 1)]
	[Protocol]
	interface NSItemProviderWriting {
		//
		// This static method has to be implemented on each class that implements
		// this, this is not a capability that exists in C#.
		// We are inlining these on each class that implements NSItemProviderWriting
		// for the sake of the method being callable from C#, for user code, the
		// user needs to manually [Export] the selector on a static method, like
		// they do for the "layer" property on CALayer subclasses.
		//
		/// <summary>To be added.</summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		[Static, Abstract]
		[Export ("writableTypeIdentifiersForItemProvider", ArgumentSemantic.Copy)]
		string [] WritableTypeIdentifiers { get; }

		// This is an optional method, which means the generator will inline it in any classes
		// that implements this interface. Unfortunately none of the native classes that implements
		// the protocol actually implements this method, which means that inlining the method will cause
		// introspection to complain (rightly). So comment out this method to avoid generator a lot of unusable API.
		// See also https://bugzilla.xamarin.com/show_bug.cgi?id=59308.
		//
		// [Static]
		// [Export ("itemProviderVisibilityForRepresentationWithTypeIdentifier:")]
		// NSItemProviderRepresentationVisibility GetItemProviderVisibility (string typeIdentifier);

		/// <summary>To be added.</summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		[Export ("writableTypeIdentifiersForItemProvider", ArgumentSemantic.Copy)]
		// 'WritableTypeIdentifiers' is a nicer name, but there's a static property with that name.
		string [] WritableTypeIdentifiersForItemProvider { get; }

		/// <param name="typeIdentifier">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("itemProviderVisibilityForRepresentationWithTypeIdentifier:")]
		// 'GetItemProviderVisibility' is a nicer name, but there's a static method with that name.
		NSItemProviderRepresentationVisibility GetItemProviderVisibilityForTypeIdentifier (string typeIdentifier);

#if WATCHOS
		/// <summary>Loads a data representation of this object.</summary>
		/// <param name="typeIdentifier">The Uniform Type Identifier of the requested representation.</param>
		/// <param name="completionHandler">The callback that receives the data or an error.</param>
		/// <returns>The progress of the data-loading operation.</returns>
#else
		/// <param name="typeIdentifier">A Universal Type Identifier (UTI) indicating the type of data to load.</param>
		/// <param name="completionHandler">The method called after the data is loaded.</param>
		/// <summary>Implement this method to customize the loading of data by an <see cref="Foundation.NSItemProvider" />.</summary>
		/// <returns>An <see cref="NSProgress" /> object reflecting the data-loading operation.</returns>
		/// <remarks>
		///           <para>The <paramref name="typeIdentifier" /> must be in the set of values returned by <see cref="Foundation.NSItemProviderWriting_Extensions.GetWritableTypeIdentifiersForItemProvider(Foundation.INSItemProviderWriting)" />.</para>
		///         </remarks>
#endif
		[Abstract]
		[Async (XmlDocs = """
			<param name="typeIdentifier">A Universal Type Identifier (UTI) indicating the type of data to load.</param>
			<summary>Asynchronously loads data for the identified type from an item provider, returning a task that contains the data.</summary>
			<returns>To be added.</returns>
			<remarks>To be added.</remarks>
			"""), Export ("loadDataWithTypeIdentifier:forItemProviderCompletionHandler:")]
		[return: NullAllowed]
		NSProgress LoadData (string typeIdentifier, Action<NSData, NSError> completionHandler);
	}


	[BaseType (typeof (NSIndexSet))]
	interface NSMutableIndexSet : NSSecureCoding {
		/// <param name="index">To be added.</param>
		/// <summary>Initializes a new index set from an existing unsigned integer.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("initWithIndex:")]
		NativeHandle Constructor (nuint index);

		[Export ("initWithIndexSet:")]
		NativeHandle Constructor (NSIndexSet other);

		[Export ("addIndexes:")]
		void Add (NSIndexSet other);

		[Export ("removeIndexes:")]
		void Remove (NSIndexSet other);

		[Export ("removeAllIndexes")]
		void Clear ();

		/// <param name="index">The index to add.</param>
		/// <summary>Adds a single index to the existing set.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("addIndex:")]
		void Add (nuint index);

		/// <param name="index">The index to remove.</param>
		/// <summary>Removes a single index from the collection.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("removeIndex:")]
		void Remove (nuint index);

		/// <param name="startIndex">To be added.</param>
		/// <param name="delta">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("shiftIndexesStartingAtIndex:by:")]
		void ShiftIndexes (nuint startIndex, nint delta);

		[Export ("addIndexesInRange:")]
		void AddIndexesInRange (NSRange range);

		[Export ("removeIndexesInRange:")]
		void RemoveIndexesInRange (NSRange range);
	}


	[BaseType (typeof (NSObject))]
	// Objective-C exception thrown.  Name: NSGenericException Reason: *** -[NSConcreteNotification init]: should never be used
	[DisableDefaultCtor] // added in iOS7 but header files says "do not invoke; not a valid initializer for this class"
	interface NSNotification : NSCoding, NSCopying {
		[Export ("name")]
		// Null not allowed
		string Name { get; }

		[Export ("object")]
		[NullAllowed]
		NSObject Object { get; }

		[Export ("userInfo")]
		[NullAllowed]
		NSDictionary UserInfo { get; }

		[Export ("notificationWithName:object:")]
		[Static]
		NSNotification FromName (string name, [NullAllowed] NSObject obj);

		[Export ("notificationWithName:object:userInfo:")]
		[Static]
		NSNotification FromName (string name, [NullAllowed] NSObject obj, [NullAllowed] NSDictionary userInfo);

	}


	[BaseType (typeof (NSObject))]
	interface NSNotificationCenter {
		[Static]
		[Export ("defaultCenter", ArgumentSemantic.Strong)]
		NSNotificationCenter DefaultCenter { get; }

		[Export ("addObserver:selector:name:object:")]
		[PostSnippet ("AddObserverToList (observer, aName, anObject);", Optimizable = true)]
		void AddObserver (NSObject observer, Selector aSelector, [NullAllowed] NSString aName, [NullAllowed] NSObject anObject);

		[Export ("postNotification:")]
		void PostNotification (NSNotification notification);

		[Export ("postNotificationName:object:")]
		void PostNotificationName (string aName, [NullAllowed] NSObject anObject);

		[Export ("postNotificationName:object:userInfo:")]
		void PostNotificationName (string aName, [NullAllowed] NSObject anObject, [NullAllowed] NSDictionary aUserInfo);

		[Export ("removeObserver:")]
		[PostSnippet ("RemoveObserversFromList (observer, null, null);", Optimizable = true)]
		void RemoveObserver (NSObject observer);

		[Export ("removeObserver:name:object:")]
		[PostSnippet ("RemoveObserversFromList (observer, aName, anObject);", Optimizable = true)]
		void RemoveObserver (NSObject observer, [NullAllowed] string aName, [NullAllowed] NSObject anObject);

		[Export ("addObserverForName:object:queue:usingBlock:")]
		NSObject AddObserver ([NullAllowed] string name, [NullAllowed] NSObject obj, [NullAllowed] NSOperationQueue queue, Action<NSNotification> handler);
	}


	[BaseType (typeof (NSObject))]
	// init returns NIL
	[DisableDefaultCtor]
	partial interface NSValue : NSSecureCoding, NSCopying {
		[Deprecated (PlatformName.MacOSX, 10, 13, message: "Potential for buffer overruns. Use 'StoreValueAtAddress (IntPtr, nuint)' instead.")]
		[Deprecated (PlatformName.iOS, 11, 0, message: "Potential for buffer overruns. Use 'StoreValueAtAddress (IntPtr, nuint)' instead.")]
		[Deprecated (PlatformName.TvOS, 11, 0, message: "Potential for buffer overruns. Use 'StoreValueAtAddress (IntPtr, nuint)' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Potential for buffer overruns. Use 'StoreValueAtAddress (IntPtr, nuint)' instead.")]
		[Export ("getValue:")]
		void StoreValueAtAddress (IntPtr value);

		[MacCatalyst (13, 1)]
		[Export ("getValue:size:")]
		void StoreValueAtAddress (IntPtr value, nuint size);

		[Export ("objCType")]
		[Internal]
		IntPtr ObjCTypePtr ();

		//[Export ("initWithBytes:objCType:")][Internal]
		//NSValue InitFromBytes (IntPtr byte_ptr, IntPtr char_ptr_type);

		[Static]
		[Internal]
		[Export ("valueWithBytes:objCType:")]
		NSValue Create (IntPtr bytes, IntPtr objCType);

		[Static]
		[Export ("valueWithNonretainedObject:")]
		NSValue ValueFromNonretainedObject (NSObject anObject);

		[Export ("nonretainedObjectValue")]
		NSObject NonretainedObjectValue { get; }

		[Static]
		[Export ("valueWithPointer:")]
		NSValue ValueFromPointer (IntPtr pointer);

		[Export ("pointerValue")]
		IntPtr PointerValue { get; }

		[Export ("isEqualToValue:")]
		bool IsEqualTo (NSValue value);

		[Export ("valueWithRange:")]
		[Static]
		NSValue FromRange (NSRange range);

		[Export ("rangeValue")]
		NSRange RangeValue { get; }
#if !WATCHOS

		/// <param name="time">To be added.</param>
		///         <summary>Creates an NSValue that wraps a CMTime object..</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static, Export ("valueWithCMTime:")]
		NSValue FromCMTime (CMTime time);

#endif
#if !WATCHOS

		/// <summary>Returns the CMTime value wrapped by this NSValue object.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("CMTimeValue")]
		CMTime CMTimeValue { get; }

#endif
#if !WATCHOS

		/// <param name="timeMapping">To be added.</param>
		///         <summary>Creates an NSValue that wraps a CMTimeMapping object.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static, Export ("valueWithCMTimeMapping:")]
		NSValue FromCMTimeMapping (CMTimeMapping timeMapping);

#endif
#if !WATCHOS

		/// <summary>Returns the CMTimeMapping value wrapped by this NSValue object.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("CMTimeMappingValue")]
		CMTimeMapping CMTimeMappingValue { get; }

#endif
#if !WATCHOS

		/// <param name="timeRange">To be added.</param>
		///         <summary>Creates an NSValue that wraps a CMTimeRange object.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static, Export ("valueWithCMTimeRange:")]
		NSValue FromCMTimeRange (CMTimeRange timeRange);

#endif
#if !WATCHOS

		/// <summary>Returns the CMTimeRange value wrapped by this NSValue object.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("CMTimeRangeValue")]
		CMTimeRange CMTimeRangeValue { get; }

#endif

#if MONOMAC
		[Export ("valueWithRect:")]
#else
		[Export ("valueWithCGRect:")]
#endif
		[Static]
		NSValue FromCGRect (CGRect rect);

#if MONOMAC
		[Export ("valueWithSize:")]
#else
		[Export ("valueWithCGSize:")]
#endif
		[Static]
		NSValue FromCGSize (CGSize size);

#if MONOMAC
		[Export ("valueWithPoint:")]
#else
		[Export ("valueWithCGPoint:")]
#endif
		[Static]
		NSValue FromCGPoint (CGPoint point);

		[MacCatalyst (15, 0)]
#if MONOMAC
		[Export ("rectValue")]
#else
		[Export ("CGRectValue")]
#endif
		CGRect CGRectValue { get; }

		[MacCatalyst (15, 0)]
#if MONOMAC
		[Export ("sizeValue")]
#else
		[Export ("CGSizeValue")]
#endif
		CGSize CGSizeValue { get; }

		[MacCatalyst (15, 0)]
#if MONOMAC
		[Export ("pointValue")]
#else
		[Export ("CGPointValue")]
#endif
		CGPoint CGPointValue { get; }

		/// <summary>Returns the CGAffineTransform value wrapped by this NSValue object.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoMac]
		[MacCatalyst (13, 1)]
		[Export ("CGAffineTransformValue")]
		CoreGraphics.CGAffineTransform CGAffineTransformValue { get; }

		/// <summary>Returns the UIEdgeInsets value wrapped by this NSValue object.</summary>
		///         <value>
		///         </value>
		///         <remarks>
		///         </remarks>
		[NoMac]
		[MacCatalyst (13, 1)]
		[Export ("UIEdgeInsetsValue")]
		UIEdgeInsets UIEdgeInsetsValue { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoMac]
		[MacCatalyst (13, 1)]
		[Export ("directionalEdgeInsetsValue")]
		NSDirectionalEdgeInsets DirectionalEdgeInsetsValue { get; }

		/// <param name="tran">To be added.</param>
		///         <summary>Creates an NSValue that wraps a CGAffineTransform object..</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[NoMac]
		[MacCatalyst (13, 1)]
		[Export ("valueWithCGAffineTransform:")]
		[Static]
		NSValue FromCGAffineTransform (CoreGraphics.CGAffineTransform tran);

		/// <param name="insets">To be added.</param>
		///         <summary>Creates an NSValue that wraps a UIEdgeInsets object.</summary>
		///         <returns>
		///         </returns>
		///         <remarks>
		///         </remarks>
		[NoMac]
		[MacCatalyst (13, 1)]
		[Export ("valueWithUIEdgeInsets:")]
		[Static]
		NSValue FromUIEdgeInsets (UIEdgeInsets insets);

		/// <param name="insets">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[NoMac]
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("valueWithDirectionalEdgeInsets:")]
		NSValue FromDirectionalEdgeInsets (NSDirectionalEdgeInsets insets);

		/// <param name="insets">The UIOffset instance</param>
		///         <summary>Creates an NSValue that wraps an UIOffset structure.</summary>
		///         <returns>
		///         </returns>
		///         <remarks>
		///         </remarks>
		[Export ("valueWithUIOffset:")]
		[Static]
		[NoMac]
		[MacCatalyst (13, 1)]
		NSValue FromUIOffset (UIOffset insets);

		/// <summary>Returns the UIOffset value wrapped by in this NSValue.</summary>
		///         <value>
		///         </value>
		///         <remarks>
		///         </remarks>
		[Export ("UIOffsetValue")]
		[NoMac]
		[MacCatalyst (13, 1)]
		UIOffset UIOffsetValue { get; }
		// from UIGeometry.h - those are in iOS8 only (even if the header is silent about them)
		// and not in OSX 10.10

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("CGVectorValue")]
		[NoMac]
		[MacCatalyst (13, 1)]
		CGVector CGVectorValue { get; }

		/// <param name="vector">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[Static, Export ("valueWithCGVector:")]
		[NoMac]
		[MacCatalyst (13, 1)]
		NSValue FromCGVector (CGVector vector);
#if !WATCHOS

		// Maybe we should include this inside mapkit.cs instead (it's a partial interface, so that's trivial)?
		/// <param name="coordinate">To be added.</param>
		///         <summary>Creates an NSValue that stores a CLLocationCoordinate2D.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static, Export ("valueWithMKCoordinate:")]
		NSValue FromMKCoordinate (CoreLocation.CLLocationCoordinate2D coordinate);

#endif
#if !WATCHOS

		/// <param name="coordinateSpan">To be added.</param>
		///         <summary>Creates an NSValue that stores an MKCoordinateSpan.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static, Export ("valueWithMKCoordinateSpan:")]
		NSValue FromMKCoordinateSpan (MapKit.MKCoordinateSpan coordinateSpan);

#endif
#if !WATCHOS

		/// <summary>The CLLocationCoordinate2D stored in this NSValue.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("MKCoordinateValue")]
		CoreLocation.CLLocationCoordinate2D CoordinateValue { get; }

#endif
#if !WATCHOS

		/// <summary>The MKCoordinateSpan stored in this NSValue.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("MKCoordinateSpanValue")]
		MapKit.MKCoordinateSpan CoordinateSpanValue { get; }

#endif
#if !WATCHOS

		/// <param name="transform">To be added.</param>
		///         <summary>Creates an NSValue that wraps a CATransform3D object.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[Export ("valueWithCATransform3D:")]
		[Static]
		NSValue FromCATransform3D (CoreAnimation.CATransform3D transform);

#endif
#if !WATCHOS

		/// <summary>Returns the CATransform3D value wrapped by this NSValue object.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("CATransform3DValue")]
		CoreAnimation.CATransform3D CATransform3DValue { get; }

#endif
#if !WATCHOS

		[iOS (16, 0)]
		[MacCatalyst (16, 0)]
		[TV (16, 0)]
		[Export ("CMVideoDimensionsValue")]
		CMVideoDimensions CMVideoDimensionsValue { get; }

#endif
#if !WATCHOS

		[iOS (16, 0)]
		[MacCatalyst (16, 0)]
		[TV (16, 0)]
		[Export ("valueWithCMVideoDimensions:")]
		[Static]
		NSValue FromCMVideoDimensions (CMVideoDimensions value);

#endif

		#region SceneKit Additions
#if !WATCHOS

		/// <param name="vector">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static, Export ("valueWithSCNVector3:")]
		NSValue FromVector (SCNVector3 vector);

#endif
#if !WATCHOS

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("SCNVector3Value")]
		SCNVector3 Vector3Value { get; }

#endif
#if !WATCHOS

		/// <param name="vector">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static, Export ("valueWithSCNVector4:")]
		NSValue FromVector (SCNVector4 vector);

#endif
#if !WATCHOS

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("SCNVector4Value")]
		SCNVector4 Vector4Value { get; }

#endif
#if !WATCHOS

		/// <param name="matrix">To be added.</param>
		///         <summary>To be added.</summary>
		///         <returns>To be added.</returns>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static, Export ("valueWithSCNMatrix4:")]
		NSValue FromSCNMatrix4 (SCNMatrix4 matrix);

#endif
#if !WATCHOS

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("SCNMatrix4Value")]
		SCNMatrix4 SCNMatrix4Value { get; }

#endif

		#endregion
	}


	[BaseType (typeof (NSValue))]
	// init returns NIL
	[DisableDefaultCtor]
	interface NSNumber
#if !WATCHOS
		: CKRecordValue, NSFetchRequestResult
#endif
	{
		[Export ("charValue")]
		sbyte SByteValue { get; }

		[Export ("unsignedCharValue")]
		byte ByteValue { get; }

		[Export ("shortValue")]
		short Int16Value { get; }

		[Export ("unsignedShortValue")]
		ushort UInt16Value { get; }

		[Export ("intValue")]
		int Int32Value { get; }

		[Export ("unsignedIntValue")]
		uint UInt32Value { get; }

		[Export ("longValue")]
		nint LongValue { get; }

		[Export ("unsignedLongValue")]
		nuint UnsignedLongValue { get; }

		[Export ("longLongValue")]
		long Int64Value { get; }

		[Export ("unsignedLongLongValue")]
		ulong UInt64Value { get; }

		[Export ("floatValue")]
		float FloatValue { get; } /* float, not CGFloat */

		[Export ("doubleValue")]
		double DoubleValue { get; }

		[Export ("decimalValue")]
		NSDecimal NSDecimalValue { get; }

		[Export ("boolValue")]
		bool BoolValue { get; }

		[Export ("integerValue")]
		nint NIntValue { get; }

		[Export ("unsignedIntegerValue")]
		nuint NUIntValue { get; }

		[Export ("stringValue")]
		string StringValue { get; }

		[Export ("compare:")]
		nint Compare (NSNumber otherNumber);

		[Export ("isEqualToNumber:")]
		bool IsEqualTo (IntPtr number);

		[Export ("descriptionWithLocale:")]
		string DescriptionWithLocale (NSLocale locale);

		[DesignatedInitializer]
		[Export ("initWithChar:")]
		NativeHandle Constructor (sbyte value);

		[DesignatedInitializer]
		[Export ("initWithUnsignedChar:")]
		NativeHandle Constructor (byte value);

		[DesignatedInitializer]
		[Export ("initWithShort:")]
		NativeHandle Constructor (short value);

		[DesignatedInitializer]
		[Export ("initWithUnsignedShort:")]
		NativeHandle Constructor (ushort value);

		[DesignatedInitializer]
		[Export ("initWithInt:")]
		NativeHandle Constructor (int /* int, not NSInteger */ value);

		[DesignatedInitializer]
		[Export ("initWithUnsignedInt:")]
		NativeHandle Constructor (uint /* unsigned int, not NSUInteger */value);

		//[Export ("initWithLong:")]
		//NativeHandle Constructor (long value);
		//
		//[Export ("initWithUnsignedLong:")]
		//NativeHandle Constructor (ulong value);

		[DesignatedInitializer]
		[Export ("initWithLongLong:")]
		NativeHandle Constructor (long value);

		[DesignatedInitializer]
		[Export ("initWithUnsignedLongLong:")]
		NativeHandle Constructor (ulong value);

		[DesignatedInitializer]
		[Export ("initWithFloat:")]
		NativeHandle Constructor (float /* float, not CGFloat */ value);

		[DesignatedInitializer]
		[Export ("initWithDouble:")]
		NativeHandle Constructor (double value);

		[DesignatedInitializer]
		[Export ("initWithBool:")]
		NativeHandle Constructor (bool value);

		/// <param name="value">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[DesignatedInitializer]
		[Export ("initWithInteger:")]
		NativeHandle Constructor (nint value);

		/// <param name="value">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[DesignatedInitializer]
		[Export ("initWithUnsignedInteger:")]
		NativeHandle Constructor (nuint value);

		[Export ("numberWithChar:")]
		[Static]
		NSNumber FromSByte (sbyte value);

		[Static]
		[Export ("numberWithUnsignedChar:")]
		NSNumber FromByte (byte value);

		[Static]
		[Export ("numberWithShort:")]
		NSNumber FromInt16 (short value);

		[Static]
		[Export ("numberWithUnsignedShort:")]
		NSNumber FromUInt16 (ushort value);

		[Static]
		[Export ("numberWithInt:")]
		NSNumber FromInt32 (int /* int, not NSInteger */ value);

		[Static]
		[Export ("numberWithUnsignedInt:")]
		NSNumber FromUInt32 (uint /* unsigned int, not NSUInteger */ value);

		/// <param name="value">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Static]
		[Export ("numberWithLong:")]
		NSNumber FromLong (nint value);
		//
		/// <param name="value">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Static]
		[Export ("numberWithUnsignedLong:")]
		NSNumber FromUnsignedLong (nuint value);

		[Static]
		[Export ("numberWithLongLong:")]
		NSNumber FromInt64 (long value);

		[Static]
		[Export ("numberWithUnsignedLongLong:")]
		NSNumber FromUInt64 (ulong value);

		[Static]
		[Export ("numberWithFloat:")]
		NSNumber FromFloat (float /* float, not CGFloat */ value);

		[Static]
		[Export ("numberWithDouble:")]
		NSNumber FromDouble (double value);

		[Static]
		[Export ("numberWithBool:")]
		NSNumber FromBoolean (bool value);

		/// <param name="value">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Static]
		[Export ("numberWithInteger:")]
		NSNumber FromNInt (nint value);

		/// <param name="value">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Static]
		[Export ("numberWithUnsignedInteger:")]
		NSNumber FromNUInt (nuint value);
	}


	[BaseType (typeof (NSNumber))]
	interface NSDecimalNumber : NSSecureCoding {
		[Export ("initWithMantissa:exponent:isNegative:")]
		NativeHandle Constructor (long mantissa, short exponent, bool isNegative);

		[DesignatedInitializer]
		[Export ("initWithDecimal:")]
		NativeHandle Constructor (NSDecimal dec);

		[Export ("initWithString:")]
		NativeHandle Constructor (string numberValue);

		[Export ("initWithString:locale:")]
		NativeHandle Constructor (string numberValue, NSObject locale);

		[Export ("descriptionWithLocale:")]
		[Override]
		string DescriptionWithLocale (NSLocale locale);

		[Export ("decimalValue")]
		NSDecimal NSDecimalValue { get; }

		[Export ("zero", ArgumentSemantic.Copy)]
		[Static]
		NSDecimalNumber Zero { get; }

		[Export ("one", ArgumentSemantic.Copy)]
		[Static]
		NSDecimalNumber One { get; }

		[Export ("minimumDecimalNumber", ArgumentSemantic.Copy)]
		[Static]
		NSDecimalNumber MinValue { get; }

		[Export ("maximumDecimalNumber", ArgumentSemantic.Copy)]
		[Static]
		NSDecimalNumber MaxValue { get; }

		[Export ("notANumber", ArgumentSemantic.Copy)]
		[Static]
		NSDecimalNumber NaN { get; }

		//
		// All the behavior ones require:
		// id <NSDecimalNumberBehaviors>)behavior;

		[Export ("decimalNumberByAdding:")]
		NSDecimalNumber Add (NSDecimalNumber d);

		[Export ("decimalNumberByAdding:withBehavior:")]
		NSDecimalNumber Add (NSDecimalNumber d, NSObject Behavior);

		[Export ("decimalNumberBySubtracting:")]
		NSDecimalNumber Subtract (NSDecimalNumber d);

		[Export ("decimalNumberBySubtracting:withBehavior:")]
		NSDecimalNumber Subtract (NSDecimalNumber d, NSObject Behavior);

		[Export ("decimalNumberByMultiplyingBy:")]
		NSDecimalNumber Multiply (NSDecimalNumber d);

		[Export ("decimalNumberByMultiplyingBy:withBehavior:")]
		NSDecimalNumber Multiply (NSDecimalNumber d, NSObject Behavior);

		[Export ("decimalNumberByDividingBy:")]
		NSDecimalNumber Divide (NSDecimalNumber d);

		[Export ("decimalNumberByDividingBy:withBehavior:")]
		NSDecimalNumber Divide (NSDecimalNumber d, NSObject Behavior);

		/// <param name="power">To be added.</param>
		/// <summary>Raises this number to the specified power.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("decimalNumberByRaisingToPower:")]
		NSDecimalNumber RaiseTo (nuint power);

		/// <param name="power">To be added.</param>
		/// <param name="Behavior">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("decimalNumberByRaisingToPower:withBehavior:")]
		NSDecimalNumber RaiseTo (nuint power, [NullAllowed] NSObject Behavior);

		[Export ("decimalNumberByMultiplyingByPowerOf10:")]
		NSDecimalNumber MultiplyPowerOf10 (short power);

		[Export ("decimalNumberByMultiplyingByPowerOf10:withBehavior:")]
		NSDecimalNumber MultiplyPowerOf10 (short power, [NullAllowed] NSObject Behavior);

		[Export ("decimalNumberByRoundingAccordingToBehavior:")]
		NSDecimalNumber Rounding (NSObject behavior);

		[Export ("compare:")]
		[Override]
		nint Compare (NSNumber other);

		[Export ("defaultBehavior", ArgumentSemantic.Strong)]
		[Static]
		NSObject DefaultBehavior { get; set; }

		[Export ("doubleValue")]
		[Override]
		double DoubleValue { get; }
	}


	[BaseType (typeof (NSObject))]
	[DesignatedDefaultCtor]
	interface NSThread {
		[Static, Export ("currentThread", ArgumentSemantic.Strong)]
		NSThread Current { get; }

		[Static, Export ("callStackSymbols", ArgumentSemantic.Copy)]
		string [] NativeCallStack { get; }

		//+ (void)detachNewThreadSelector:(SEL)selector toTarget:(id)target withObject:(id)argument;

		[Static, Export ("isMultiThreaded")]
		bool IsMultiThreaded { get; }

		//- (NSMutableDictionary *)threadDictionary;

		[Static, Export ("sleepUntilDate:")]
		void SleepUntil (NSDate date);

		[Static, Export ("sleepForTimeInterval:")]
		void SleepFor (double timeInterval);

		[Static, Export ("exit")]
		void Exit ();

		[Static, Export ("threadPriority"), Internal]
		double _GetPriority ();

		[Static, Export ("setThreadPriority:"), Internal]
		bool _SetPriority (double priority);

		//+ (NSArray *)callStackReturnAddresses;

		[NullAllowed] // by default this property is null
		[Export ("name")]
		string Name { get; set; }

		[Export ("stackSize")]
		nuint StackSize { get; set; }

		[Export ("isMainThread")]
		bool IsMainThread { get; }

		// MainThread is already used for the instance selector and we can't reuse the same name
		[Static]
		[Export ("isMainThread")]
		bool IsMain { get; }

		[Static]
		[Export ("mainThread", ArgumentSemantic.Strong)]
		NSThread MainThread { get; }

		/// <summary>Whether this NSThread is executing.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isExecuting")]
		bool IsExecuting { get; }

		/// <summary>Whether this NSThread has finished processing.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isFinished")]
		bool IsFinished { get; }

		/// <summary>Whether this NSThread is cancelled.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isCancelled")]
		bool IsCancelled { get; }

		[Export ("cancel")]
		void Cancel ();

		[Export ("start")]
		void Start ();

		[Export ("main")]
		void Main ();

		[MacCatalyst (13, 1)]
		[Export ("qualityOfService")]
		NSQualityOfService QualityOfService { get; set; }

		[Deprecated (PlatformName.MacOSX, 26, 0, "This notification does not protect against race conditions.")]
		[Deprecated (PlatformName.iOS, 26, 0, "This notification does not protect against race conditions.")]
		[Deprecated (PlatformName.TvOS, 26, 0, "This notification does not protect against race conditions.")]
		[Deprecated (PlatformName.MacCatalyst, 26, 0, "This notification does not protect against race conditions.")]
		[Notification]
		[Field ("NSThreadWillExitNotification")]
		NSString ThreadWillExitNotification { get; }

		[Deprecated (PlatformName.MacOSX, 26, 0, "This notification does not protect against race conditions.")]
		[Deprecated (PlatformName.iOS, 26, 0, "This notification does not protect against race conditions.")]
		[Deprecated (PlatformName.TvOS, 26, 0, "This notification does not protect against race conditions.")]
		[Deprecated (PlatformName.MacCatalyst, 26, 0, "This notification does not protect against race conditions.")]
		[Notification]
		[Field ("NSWillBecomeMultiThreadedNotification")]
		NSString WillBecomeMultiThreadedNotification { get; }
	}


	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	partial interface NSProgress {

		[Static, Export ("currentProgress")]
		NSProgress CurrentProgress { get; }

		[Static, Export ("progressWithTotalUnitCount:")]
		NSProgress FromTotalUnitCount (long unitCount);

		[MacCatalyst (13, 1)]
		[Static, Export ("discreteProgressWithTotalUnitCount:")]
		NSProgress GetDiscreteProgress (long unitCount);

		[MacCatalyst (13, 1)]
		[Static, Export ("progressWithTotalUnitCount:parent:pendingUnitCount:")]
		NSProgress FromTotalUnitCount (long unitCount, NSProgress parent, long portionOfParentTotalUnitCount);

		[DesignatedInitializer]
		[Export ("initWithParent:userInfo:")]
		NativeHandle Constructor ([NullAllowed] NSProgress parentProgress, [NullAllowed] NSDictionary userInfo);

		[Export ("becomeCurrentWithPendingUnitCount:")]
		void BecomeCurrent (long pendingUnitCount);

		[Export ("resignCurrent")]
		void ResignCurrent ();

		[MacCatalyst (13, 1)]
		[Export ("addChild:withPendingUnitCount:")]
		void AddChild (NSProgress child, long pendingUnitCount);

		[Export ("totalUnitCount")]
		long TotalUnitCount { get; set; }

		[Export ("completedUnitCount")]
		long CompletedUnitCount { get; set; }

		[Export ("localizedDescription", ArgumentSemantic.Copy), NullAllowed]
		string LocalizedDescription { get; set; }

		[Export ("localizedAdditionalDescription", ArgumentSemantic.Copy), NullAllowed]
		string LocalizedAdditionalDescription { get; set; }

		/// <summary>Whether this is tracking work that can be cancelled.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("cancellable")]
		bool Cancellable { [Bind ("isCancellable")] get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("pausable")]
		bool Pausable { [Bind ("isPausable")] get; set; }

		/// <summary>Whether this is tracking work that was cancelled.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("cancelled")]
		bool Cancelled { [Bind ("isCancelled")] get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("paused")]
		bool Paused { [Bind ("isPaused")] get; }

		/// <param name="handler">To be added.</param>
		///         <summary>To be added.</summary>
		///         <remarks>To be added.</remarks>
		[Export ("setCancellationHandler:")]
		void SetCancellationHandler (Action handler);

		/// <param name="handler">To be added.</param>
		///         <summary>To be added.</summary>
		///         <remarks>To be added.</remarks>
		[Export ("setPausingHandler:")]
		void SetPauseHandler (Action handler);

		/// <param name="handler">To be added.</param>
		///         <summary>To be added.</summary>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("setResumingHandler:")]
		void SetResumingHandler (Action handler);

		[Export ("setUserInfoObject:forKey:")]
		void SetUserInfo ([NullAllowed] NSObject obj, NSString key);

		/// <summary>Whether the tracked progress is indeterminate. Read-only.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("indeterminate")]
		bool Indeterminate { [Bind ("isIndeterminate")] get; }

		[Export ("fractionCompleted")]
		double FractionCompleted { get; }

		[Export ("cancel")]
		void Cancel ();

		[Export ("pause")]
		void Pause ();

		[MacCatalyst (13, 1)]
		[Export ("resume")]
		void Resume ();

		[Export ("userInfo")]
		NSDictionary UserInfo { get; }

		[NullAllowed] // by default this property is null
		[Export ("kind", ArgumentSemantic.Copy)]
		NSString Kind { get; set; }

		[NoWatch]
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("publish")]
		void Publish ();

		[NoWatch]
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("unpublish")]
		void Unpublish ();

		/// <param name="acknowledgementHandler">To be added.</param>
		///         <param name="appBundleIdentifier">To be added.</param>
		///         <summary>To be added.</summary>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("setAcknowledgementHandler:forAppBundleIdentifier:")]
		void SetAcknowledgementHandler (Action<bool> acknowledgementHandler, string appBundleIdentifier);

		[NoWatch]
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Static, Export ("addSubscriberForFileURL:withPublishingHandler:")]
		NSObject AddSubscriberForFile (NSUrl url, Action<NSProgress> publishingHandler);

		[NoWatch]
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Static, Export ("removeSubscriber:")]
		void RemoveSubscriber (NSObject subscriber);

		/// <param name="success">To be added.</param>
		///         <summary>To be added.</summary>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("acknowledgeWithSuccess:")]
		void AcknowledgeWithSuccess (bool success);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("old")]
		bool Old { [Bind ("isOld")] get; }

		/// <summary>Represents the value associated with the constant NSProgressKindFile</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSProgressKindFile")]
		NSString KindFile { get; }

		/// <summary>Represents the value associated with the constant NSProgressEstimatedTimeRemainingKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSProgressEstimatedTimeRemainingKey")]
		NSString EstimatedTimeRemainingKey { get; }

		/// <summary>Represents the value associated with the constant NSProgressThroughputKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSProgressThroughputKey")]
		NSString ThroughputKey { get; }

		/// <summary>Represents the value associated with the constant NSProgressFileOperationKindKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSProgressFileOperationKindKey")]
		NSString FileOperationKindKey { get; }

		/// <summary>Represents the value associated with the constant NSProgressFileOperationKindDownloading</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSProgressFileOperationKindDownloading")]
		NSString FileOperationKindDownloading { get; }

		/// <summary>Represents the value associated with the constant NSProgressFileOperationKindDecompressingAfterDownloading</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSProgressFileOperationKindDecompressingAfterDownloading")]
		NSString FileOperationKindDecompressingAfterDownloading { get; }

		/// <summary>Represents the value associated with the constant NSProgressFileOperationKindReceiving</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSProgressFileOperationKindReceiving")]
		NSString FileOperationKindReceiving { get; }

		/// <summary>Represents the value associated with the constant NSProgressFileOperationKindCopying</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSProgressFileOperationKindCopying")]
		NSString FileOperationKindCopying { get; }

		[TV (14, 5), iOS (14, 5)]
		[MacCatalyst (14, 5)]
		[Field ("NSProgressFileOperationKindUploading")]
		NSString FileOperationKindUploading { get; }

		/// <summary>Represents the value associated with the constant NSProgressFileURLKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSProgressFileURLKey")]
		NSString FileURLKey { get; }

		/// <summary>Represents the value associated with the constant NSProgressFileTotalCountKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSProgressFileTotalCountKey")]
		NSString FileTotalCountKey { get; }

		/// <summary>Represents the value associated with the constant NSProgressFileCompletedCountKey</summary>
		///         <value>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Field ("NSProgressFileCompletedCountKey")]
		NSString FileCompletedCountKey { get; }

		[TV (15, 0), iOS (15, 0), MacCatalyst (15, 0)]
		[Field ("NSProgressFileOperationKindDuplicating")]
		NSString FileOperationKindDuplicatingKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Field ("NSProgressFileAnimationImageKey")]
		NSString FileAnimationImageKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Field ("NSProgressFileAnimationImageOriginalRectKey")]
		NSString FileAnimationImageOriginalRectKey { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NoWatch]
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Field ("NSProgressFileIconKey")]
		NSString FileIconKey { get; }

		[MacCatalyst (13, 1)]
		[Async (XmlDocs = """
			<param name="unitCount">To be added.</param>
			<summary>To be added.</summary>
			<returns>A task that represents the asynchronous PerformAsCurrent operation</returns>
			<remarks>
			          <para copied="true">The PerformAsCurrentAsync method is suitable to be used with C# async by returning control to the caller with a Task representing the operation.</para>
			          <para copied="true">To be added.</para>
			        </remarks>
			"""), Export ("performAsCurrentWithPendingUnitCount:usingBlock:")]
		void PerformAsCurrent (long unitCount, Action work);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("finished")]
		bool Finished { [Bind ("isFinished")] get; }

		[Internal]
		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("estimatedTimeRemaining", ArgumentSemantic.Copy)]
		//[BindAs (typeof (nint?))]
		NSNumber _EstimatedTimeRemaining { get; set; }

		[Internal]
		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("throughput", ArgumentSemantic.Copy)]
		//[BindAs (typeof (nint?))]
		NSNumber _Throughput { get; set; }

		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("fileOperationKind")]
		string FileOperationKind { get; set; }

		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("fileURL", ArgumentSemantic.Copy)]
		NSUrl FileUrl { get; set; }

		[Internal]
		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("fileTotalCount", ArgumentSemantic.Copy)]
		//[BindAs (typeof (nint?))]
		NSNumber _FileTotalCount { get; set; }

		[Internal]
		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("fileCompletedCount", ArgumentSemantic.Copy)]
		//[BindAs (typeof (nint?))]
		NSNumber _FileCompletedCount { get; set; }
	}


	interface INSProgressReporting { }


	[MacCatalyst (13, 1)]
	[Protocol]
	interface NSProgressReporting {
		[Abstract]
		[Export ("progress")]
		NSProgress Progress { get; }
	}


	/// <param name="evaluatedObject">To be added.</param>
	/// <param name="bindings">To be added.</param>
	/// <summary>A delegate that represents the expression to use with <see cref="Foundation.NSPredicate.FromExpression(Foundation.NSPredicateEvaluator)" />.</summary>
	/// <returns>To be added.</returns>
	/// <remarks>To be added.</remarks>
	delegate bool NSPredicateEvaluator ([NullAllowed] NSObject evaluatedObject, [NullAllowed] NSDictionary bindings);


	[BaseType (typeof (NSObject))]
	// 'init' returns NIL
	[DisableDefaultCtor]
	interface NSPredicate : NSSecureCoding, NSCopying {
		[Static]
		[Internal]
		[Export ("predicateWithFormat:argumentArray:")]
		NSPredicate _FromFormat (string predicateFormat, [NullAllowed] NSObject [] arguments);

		[Static, Export ("predicateWithValue:")]
		NSPredicate FromValue (bool value);

		[Static, Export ("predicateWithBlock:")]
		NSPredicate FromExpression (NSPredicateEvaluator evaluator);

		[Export ("predicateFormat")]
		string PredicateFormat { get; }

		[Export ("predicateWithSubstitutionVariables:")]
		NSPredicate PredicateWithSubstitutionVariables (NSDictionary substitutionVariables);

		[Export ("evaluateWithObject:")]
		bool EvaluateWithObject (NSObject obj);

		[Export ("evaluateWithObject:substitutionVariables:")]
		bool EvaluateWithObject (NSObject obj, NSDictionary substitutionVariables);
#if !WATCHOS

		[return: NullAllowed]
		[Static]
		[NoiOS]
		[NoMacCatalyst]
		[NoTV]
		[Export ("predicateFromMetadataQueryString:")]
		NSPredicate FromMetadataQueryString (string query);

#endif

		[MacCatalyst (13, 1)]
		[Export ("allowEvaluation")]
		void AllowEvaluation ();
#if !WATCHOS

		/// <summary>Validates this predicate using the specified validator and, if valid, allows it to be evaluated.</summary>
		/// <param name="validator">The <see cref="INSPredicateValidating" /> object used to validate the predicate before allowing evaluation.</param>
		/// <param name="error">When this method returns <see langword="false" />, contains an <see cref="NSError" /> describing the validation failure; otherwise, <see langword="null" />.</param>
		/// <returns><see langword="true" /> if the predicate passed validation and is now allowed to be evaluated; otherwise, <see langword="false" />.</returns>
		[iOS (26, 4), TV (26, 4), Mac (26, 4), MacCatalyst (26, 4)]
		[Export ("allowEvaluationWithValidator:error:")]
		bool AllowEvaluation (INSPredicateValidating validator, [NullAllowed] out NSError error);

#endif
	}


	/// <summary>Defines an extension method for <see cref="Foundation.NSOrderedSet" /> objects allowing them to be filtered via an <see cref="Foundation.NSPredicate" />.</summary>
	/// <remarks>To be added.</remarks>
	[Category, BaseType (typeof (NSOrderedSet))]
	partial interface NSPredicateSupport_NSOrderedSet {
		/// <param name="p">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("filteredOrderedSetUsingPredicate:")]
		NSOrderedSet FilterUsingPredicate (NSPredicate p);
	}


	/// <summary>Defines an extension method for <see cref="Foundation.NSMutableOrderedSet" /> objects allowing them to be filtered using a <see cref="Foundation.NSPredicate" />.</summary>
	/// <remarks>To be added.</remarks>
	[Category, BaseType (typeof (NSMutableOrderedSet))]
	partial interface NSPredicateSupport_NSMutableOrderedSet {
		/// <param name="p">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("filterUsingPredicate:")]
		void FilterUsingPredicate (NSPredicate p);
	}


	/// <summary>Extension method for <see cref="Foundation.NSArray" /> objects, allowing them to be filtered with a <see cref="Foundation.NSPredicate" />.</summary>
	/// <remarks>To be added.</remarks>
	[Category, BaseType (typeof (NSArray))]
	partial interface NSPredicateSupport_NSArray {
		/// <param name="array">To be added.</param>
		/// <summary>To be added.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		[Export ("filteredArrayUsingPredicate:")]
		NSArray FilterUsingPredicate (NSArray array);
	}


#pragma warning disable 618
	/// <summary>Helper metohds for applying predicates to mutable arrays.</summary>
	/// <remarks>
	///     </remarks>
	[Category, BaseType (typeof (NSMutableArray))]
#pragma warning restore 618
	partial interface NSPredicateSupport_NSMutableArray {
		/// <param name="predicate">The predicate used to filter the
		/// 	elements of the array.</param>
		/// <summary>Filters the element of the array in place, by keeping
		/// 	only the elements that match.</summary>
		/// <remarks>
		///         </remarks>
		[Export ("filterUsingPredicate:")]
		void FilterUsingPredicate (NSPredicate predicate);
	}


	/// <summary>Helper methods for applying predicates to sets.</summary>
	/// <remarks>To be added.</remarks>
	[Category, BaseType (typeof (NSSet))]
	partial interface NSPredicateSupport_NSSet {
		/// <param name="predicate">The predicate used to filter the
		/// 	elements of the set.</param>
		/// <summary>Returns a new set that contains the elements that
		/// 	match the predicate.</summary>
		/// <returns>A new immutable set.</returns>
		/// <remarks>
		///         </remarks>
		[Export ("filteredSetUsingPredicate:")]
		NSSet FilterUsingPredicate (NSPredicate predicate);
	}


	/// <summary>Extension method for <see cref="Foundation.NSMutableSet" /> objects, allowing them to be filtered with a <see cref="Foundation.NSPredicate" />.</summary>
	/// <remarks>To be added.</remarks>
	[Category, BaseType (typeof (NSMutableSet))]
	partial interface NSPredicateSupport_NSMutableSet {
		/// <param name="predicate">To be added.</param>
		/// <summary>To be added.</summary>
		/// <remarks>To be added.</remarks>
		[Export ("filterUsingPredicate:")]
		void FilterUsingPredicate (NSPredicate predicate);
	}


	// Not yet, the IntPtr[] argument isn't handled correctly by the generator (it tries to convert to NSArray, while the native method expects a C array).
	//	[Protocol]
	//	interface NSFastEnumeration {
	//		[Abstract]
	//		[Export ("countByEnumeratingWithState:objects:count:")]
	//		nuint Enumerate (ref NSFastEnumerationState state, IntPtr[] objects, nuint count);
	//	}

	// Placeholer, just so we can start flagging things
	interface INSFastEnumeration { }


	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	interface NSDateInterval : NSCopying, NSSecureCoding {
		[Export ("startDate", ArgumentSemantic.Copy)]
		NSDate StartDate { get; }

		[Export ("endDate", ArgumentSemantic.Copy)]
		NSDate EndDate { get; }

		[Export ("duration")]
		double Duration { get; }

		[Export ("initWithStartDate:duration:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSDate startDate, double duration);

		[Export ("initWithStartDate:endDate:")]
		NativeHandle Constructor (NSDate startDate, NSDate endDate);

		[Export ("compare:")]
		NSComparisonResult Compare (NSDateInterval dateInterval);

		[Export ("isEqualToDateInterval:")]
		bool IsEqualTo (NSDateInterval dateInterval);

		[Export ("intersectsDateInterval:")]
		bool Intersects (NSDateInterval dateInterval);

		[Export ("intersectionWithDateInterval:")]
		[return: NullAllowed]
		NSDateInterval GetIntersection (NSDateInterval dateInterval);

		[Export ("containsDate:")]
		bool ContainsDate (NSDate date);
	}


	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject), Name = "NSURLSessionTaskTransactionMetrics")]
	[DisableDefaultCtor]
	interface NSUrlSessionTaskTransactionMetrics {

		[Deprecated (PlatformName.MacOSX, 10, 15, message: "This type is not meant to be user created.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "This type is not meant to be user created.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "This type is not meant to be user created.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "This type is not meant to be user created.")]
		[Export ("init")]
		NativeHandle Constructor ();

		[Export ("request", ArgumentSemantic.Copy)]
		NSUrlRequest Request { get; }

		[NullAllowed, Export ("response", ArgumentSemantic.Copy)]
		NSUrlResponse Response { get; }

		[NullAllowed, Export ("fetchStartDate", ArgumentSemantic.Copy)]
		NSDate FetchStartDate { get; }

		[NullAllowed, Export ("domainLookupStartDate", ArgumentSemantic.Copy)]
		NSDate DomainLookupStartDate { get; }

		[NullAllowed, Export ("domainLookupEndDate", ArgumentSemantic.Copy)]
		NSDate DomainLookupEndDate { get; }

		[NullAllowed, Export ("connectStartDate", ArgumentSemantic.Copy)]
		NSDate ConnectStartDate { get; }

		[NullAllowed, Export ("secureConnectionStartDate", ArgumentSemantic.Copy)]
		NSDate SecureConnectionStartDate { get; }

		[NullAllowed, Export ("secureConnectionEndDate", ArgumentSemantic.Copy)]
		NSDate SecureConnectionEndDate { get; }

		[NullAllowed, Export ("connectEndDate", ArgumentSemantic.Copy)]
		NSDate ConnectEndDate { get; }

		[NullAllowed, Export ("requestStartDate", ArgumentSemantic.Copy)]
		NSDate RequestStartDate { get; }

		[NullAllowed, Export ("requestEndDate", ArgumentSemantic.Copy)]
		NSDate RequestEndDate { get; }

		[NullAllowed, Export ("responseStartDate", ArgumentSemantic.Copy)]
		NSDate ResponseStartDate { get; }

		[NullAllowed, Export ("responseEndDate", ArgumentSemantic.Copy)]
		NSDate ResponseEndDate { get; }

		[NullAllowed, Export ("networkProtocolName")]
		string NetworkProtocolName { get; }

		/// <summary>
		///           <see langword="true" /> if a proxy was used to retrieve the resource.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("proxyConnection")]
		bool ProxyConnection { [Bind ("isProxyConnection")] get; }

		/// <summary>
		///           <see langword="true" /> if the resource was retrieved via a persistent connection.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("reusedConnection")]
		bool ReusedConnection { [Bind ("isReusedConnection")] get; }

		[Export ("resourceFetchType", ArgumentSemantic.Assign)]
		NSUrlSessionTaskMetricsResourceFetchType ResourceFetchType { get; }

		[MacCatalyst (13, 1)]
		[Export ("countOfRequestHeaderBytesSent")]
		long CountOfRequestHeaderBytesSent { get; }

		[MacCatalyst (13, 1)]
		[Export ("countOfRequestBodyBytesSent")]
		long CountOfRequestBodyBytesSent { get; }

		[MacCatalyst (13, 1)]
		[Export ("countOfRequestBodyBytesBeforeEncoding")]
		long CountOfRequestBodyBytesBeforeEncoding { get; }

		[MacCatalyst (13, 1)]
		[Export ("countOfResponseHeaderBytesReceived")]
		long CountOfResponseHeaderBytesReceived { get; }

		[MacCatalyst (13, 1)]
		[Export ("countOfResponseBodyBytesReceived")]
		long CountOfResponseBodyBytesReceived { get; }

		[MacCatalyst (13, 1)]
		[Export ("countOfResponseBodyBytesAfterDecoding")]
		long CountOfResponseBodyBytesAfterDecoding { get; }

		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("localAddress")]
		string LocalAddress { get; }

		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("localPort", ArgumentSemantic.Copy)]
		// 0-1023
		[BindAs (typeof (ushort?))]
		NSNumber LocalPort { get; }

		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("remoteAddress")]
		string RemoteAddress { get; }

		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("remotePort", ArgumentSemantic.Copy)]
		// 0-1023
		[BindAs (typeof (ushort?))]
		NSNumber RemotePort { get; }

		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("negotiatedTLSProtocolVersion", ArgumentSemantic.Copy)]
		// <quote>It is a 2-byte sequence in host byte order.</quote> but it refers to (nicer) `tls_protocol_version_t`
		[BindAs (typeof (SslProtocol?))]
		NSNumber NegotiatedTlsProtocolVersion { get; }

		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("negotiatedTLSCipherSuite", ArgumentSemantic.Copy)]
		// <quote>It is a 2-byte sequence in host byte order.</quote> but it refers to (nicer) `tls_ciphersuite_t`
		[BindAs (typeof (TlsCipherSuite?))]
		NSNumber NegotiatedTlsCipherSuite { get; }

		[MacCatalyst (13, 1)]
		[Export ("cellular")]
		bool Cellular { [Bind ("isCellular")] get; }

		[MacCatalyst (13, 1)]
		[Export ("expensive")]
		bool Expensive { [Bind ("isExpensive")] get; }

		[MacCatalyst (13, 1)]
		[Export ("constrained")]
		bool Constrained { [Bind ("isConstrained")] get; }

		[MacCatalyst (13, 1)]
		[Export ("multipath")]
		bool Multipath { [Bind ("isMultipath")] get; }

		[TV (14, 0), iOS (14, 0)]
		[MacCatalyst (14, 0)]
		[Export ("domainResolutionProtocol")]
		NSUrlSessionTaskMetricsDomainResolutionProtocol DomainResolutionProtocol { get; }
	}


	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject), Name = "NSURLSessionTaskMetrics")]
	[DisableDefaultCtor]
	interface NSUrlSessionTaskMetrics {

		[Deprecated (PlatformName.MacOSX, 10, 15, message: "This type is not meant to be user created.")]
		[Deprecated (PlatformName.iOS, 13, 0, message: "This type is not meant to be user created.")]
		[Deprecated (PlatformName.TvOS, 13, 0, message: "This type is not meant to be user created.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "This type is not meant to be user created.")]
		[Export ("init")]
		NativeHandle Constructor ();

		[Export ("transactionMetrics", ArgumentSemantic.Copy)]
		NSUrlSessionTaskTransactionMetrics [] TransactionMetrics { get; }

		[Export ("taskInterval", ArgumentSemantic.Copy)]
		NSDateInterval TaskInterval { get; }

		[Export ("redirectCount")]
		nuint RedirectCount { get; }
	}


	[TV (14, 0), iOS (14, 0)]
	[MacCatalyst (14, 0)]
	[Native]
	public enum NSUrlSessionTaskMetricsDomainResolutionProtocol : long {
		Unknown,
		Udp,
		Tcp,
		Tls,
		Https,
	}

}
