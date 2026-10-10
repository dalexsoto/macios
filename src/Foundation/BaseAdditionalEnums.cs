

namespace Foundation {

	/// <summary>Run loop modes for <see cref="Foundation.NSRunLoop" />.</summary>
	public enum NSRunLoopMode {

		/// <summary>The default mode to handle input sources.   The most common run loop mode.</summary>
		[DefaultEnumValue]
		[Field ("NSDefaultRunLoopMode")]
		Default,

		/// <summary>Run loop mode constant used to run handlers in any of the declared “common” modes.</summary>
		[Field ("NSRunLoopCommonModes")]
		Common,

#if MONOMAC
		/// <summary>To be added.</summary>
		[Field ("NSConnectionReplyMode")]
		ConnectionReply = 2,

		/// <summary>To be added.</summary>
		[Field ("NSModalPanelRunLoopMode", "AppKit")]
		ModalPanel,

		/// <summary>To be added.</summary>
		[Field ("NSEventTrackingRunLoopMode", "AppKit")]
		EventTracking,
#elif !WATCHOS
		// iOS-specific Enums start in 100 to avoid conflicting with future extensions to MonoMac
		/// <summary>The NSRunLoop mode used when tracking controls. Use this to receive timers and events during UI tracking.</summary>
		[Field ("UITrackingRunLoopMode", "UIKit")]
		UITracking = 100,
#endif
		// If it is not part of these enumerations
		/// <summary>To be added.</summary>
		[Field (null)]
		Other = 1000,
	}


	[MacCatalyst (13, 1)]
	public enum NSStringTransform {
		/// <summary>To be added.</summary>
		[Field ("NSStringTransformLatinToKatakana")]
		LatinToKatakana,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformLatinToHiragana")]
		LatinToHiragana,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformLatinToHangul")]
		LatinToHangul,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformLatinToArabic")]
		LatinToArabic,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformLatinToHebrew")]
		LatinToHebrew,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformLatinToThai")]
		LatinToThai,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformLatinToCyrillic")]
		LatinToCyrillic,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformLatinToGreek")]
		LatinToGreek,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformToLatin")]
		ToLatin,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformMandarinToLatin")]
		MandarinToLatin,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformHiraganaToKatakana")]
		HiraganaToKatakana,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformFullwidthToHalfwidth")]
		FullwidthToHalfwidth,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformToXMLHex")]
		ToXmlHex,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformToUnicodeName")]
		ToUnicodeName,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformStripCombiningMarks")]
		StripCombiningMarks,

		/// <summary>To be added.</summary>
		[Field ("NSStringTransformStripDiacritics")]
		StripDiacritics,
	}


	[TV (15, 0), iOS (15, 0), MacCatalyst (15, 0)]
	[Native]
	public enum NSURLRequestAttribution : ulong {
		Developer = 0,
		User = 1,
	}

}
