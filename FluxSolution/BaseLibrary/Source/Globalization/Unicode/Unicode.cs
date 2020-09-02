//using System.Linq;

//namespace Flux
//{
//  /// <summary>
//  /// 
//  /// </summary>
//  /// <remarks>A code point is a Unicode number representing a defined meaning.</remarks>
//  /// <remarks>A code unit is a unit of storage for encoding code points. E.g. UTF-16 is a 16-bit code unit. One or more code units may be used to represented a code point.</remarks>
//  /// <remarks>A grapheme is one or more code points representing an element of writing.</remarks>
//  /// <remarks>A glyph is a visual "image", typically in a font, used to represent visual "symbols". One or more glyphs may be used to represent a grapheme.</remarks>
//  /// <remarks>A .NET CLR character, is UTF-16, i.e. a code unit (mentioned above).</remarks>
//  /// <remarks>A .NET text element is equivalent to a grapheme.</remarks>
//  public static partial class Unicode
//  {
//    /// <summary>Returns a new sequence of Unicode code points from the string.</summary>
//    public static System.Collections.Generic.IEnumerable<int> GetCodePoints(string characters)
//    {
//      if (characters is null) throw new System.ArgumentNullException(nameof(characters));

//      for (var index = 0; index < characters.Length; index += char.IsSurrogatePair(characters, index) ? 2 : 1)
//      {
//        yield return char.ConvertToUtf32(characters, index);
//      }
//    }

//    //public static System.Collections.Generic.IEnumerable<int> GetCodePoints(this Block source)
//    //{
//    //  for (int codePoint = source.GetFirstCodePoint(), lastCodePoint = source.GetLastCodePoint(); codePoint <= lastCodePoint; codePoint++)
//    //    yield return codePoint;
//    //}
//    //public static System.Collections.Generic.IEnumerable<char> GetCodeUnits(this Block source)
//    //{
//    //  if (source > Block.Specials) throw new System.ArgumentOutOfRangeException(nameof(source));

//    //  var lastCodeUnit = (char)source.GetLastCodePoint();
//    //  for (var codeUnit = (char)source.GetFirstCodePoint(); codeUnit < lastCodeUnit; codeUnit++)
//    //    yield return codeUnit;
//    //  yield return lastCodeUnit;
//    //}

//    //public static int GetFirstCodePoint(this Block source) => source switch
//    //{
//    //  Block.BasicLatin => 0x00,
//    //  Block.Latin1Supplement => 0x80,
//    //  Block.LatinExtendedA => 0x100,
//    //  Block.LatinExtendedB => 0x180,
//    //  Block.IPAExtensions => 0x250,
//    //  Block.SpacingModifierLetters => 0x2B0,
//    //  Block.CombiningDiacriticalMarks => 0x300,
//    //  Block.GreekandCoptic => 0x370,
//    //  Block.Cyrillic => 0x400,
//    //  Block.CyrillicSupplement => 0x500,
//    //  Block.Armenian => 0x530,
//    //  Block.Hebrew => 0x590,
//    //  Block.Arabic => 0x600,
//    //  Block.Syriac => 0x700,
//    //  Block.ArabicSupplement => 0x750,
//    //  Block.Thaana => 0x780,
//    //  Block.NKo => 0x7C0,
//    //  Block.Samaritan => 0x800,
//    //  Block.Mandaic => 0x840,
//    //  Block.SyriacSupplement => 0x860,
//    //  Block.ArabicExtendedA => 0x8A0,
//    //  Block.Devanagari => 0x900,
//    //  Block.Bengali => 0x980,
//    //  Block.Gurmukhi => 0xA00,
//    //  Block.Gujarati => 0xA80,
//    //  Block.Oriya => 0xB00,
//    //  Block.Tamil => 0xB80,
//    //  Block.Telugu => 0xC00,
//    //  Block.Kannada => 0xC80,
//    //  Block.Malayalam => 0xD00,
//    //  Block.Sinhala => 0xD80,
//    //  Block.Thai => 0xE00,
//    //  Block.Lao => 0xE80,
//    //  Block.Tibetan => 0xF00,
//    //  Block.Myanmar => 0x1000,
//    //  Block.Georgian => 0x10A0,
//    //  Block.HangulJamo => 0x1100,
//    //  Block.Ethiopic => 0x1200,
//    //  Block.EthiopicSupplement => 0x1380,
//    //  Block.Cherokee => 0x13A0,
//    //  Block.UnifiedCanadianAboriginalSyllabics => 0x1400,
//    //  Block.Ogham => 0x1680,
//    //  Block.Runic => 0x16A0,
//    //  Block.Tagalog => 0x1700,
//    //  Block.Hanunoo => 0x1720,
//    //  Block.Buhid => 0x1740,
//    //  Block.Tagbanwa => 0x1760,
//    //  Block.Khmer => 0x1780,
//    //  Block.Mongolian => 0x1800,
//    //  Block.UnifiedCanadianAboriginalSyllabicsExtended => 0x18B0,
//    //  Block.Limbu => 0x1900,
//    //  Block.TaiLe => 0x1950,
//    //  Block.NewTaiLue => 0x1980,
//    //  Block.KhmerSymbols => 0x19E0,
//    //  Block.Buginese => 0x1A00,
//    //  Block.TaiTham => 0x1A20,
//    //  Block.CombiningDiacriticalMarksExtended => 0x1AB0,
//    //  Block.Balinese => 0x1B00,
//    //  Block.Sundanese => 0x1B80,
//    //  Block.Batak => 0x1BC0,
//    //  Block.Lepcha => 0x1C00,
//    //  Block.OlChiki => 0x1C50,
//    //  Block.CyrillicExtendedC => 0x1C80,
//    //  Block.GeorgianExtended => 0x1C90,
//    //  Block.SundaneseSupplement => 0x1CC0,
//    //  Block.VedicExtensions => 0x1CD0,
//    //  Block.PhoneticExtensions => 0x1D00,
//    //  Block.PhoneticExtensionsSupplement => 0x1D80,
//    //  Block.CombiningDiacriticalMarksSupplement => 0x1DC0,
//    //  Block.LatinExtendedAdditional => 0x1E00,
//    //  Block.GreekExtended => 0x1F00,
//    //  Block.GeneralPunctuation => 0x2000,
//    //  Block.SuperscriptsandSubscripts => 0x2070,
//    //  Block.CurrencySymbols => 0x20A0,
//    //  Block.CombiningDiacriticalMarksforSymbols => 0x20D0,
//    //  Block.LetterlikeSymbols => 0x2100,
//    //  Block.NumberForms => 0x2150,
//    //  Block.Arrows => 0x2190,
//    //  Block.MathematicalOperators => 0x2200,
//    //  Block.MiscellaneousTechnical => 0x2300,
//    //  Block.ControlPictures => 0x2400,
//    //  Block.OpticalCharacterRecognition => 0x2440,
//    //  Block.EnclosedAlphanumerics => 0x2460,
//    //  Block.BoxDrawing => 0x2500,
//    //  Block.BlockElements => 0x2580,
//    //  Block.GeometricShapes => 0x25A0,
//    //  Block.MiscellaneousSymbols => 0x2600,
//    //  Block.Dingbats => 0x2700,
//    //  Block.MiscellaneousMathematicalSymbolsA => 0x27C0,
//    //  Block.SupplementalArrowsA => 0x27F0,
//    //  Block.BraillePatterns => 0x2800,
//    //  Block.SupplementalArrowsB => 0x2900,
//    //  Block.MiscellaneousMathematicalSymbolsB => 0x2980,
//    //  Block.SupplementalMathematicalOperators => 0x2A00,
//    //  Block.MiscellaneousSymbolsandArrows => 0x2B00,
//    //  Block.Glagolitic => 0x2C00,
//    //  Block.LatinExtendedC => 0x2C60,
//    //  Block.Coptic => 0x2C80,
//    //  Block.GeorgianSupplement => 0x2D00,
//    //  Block.Tifinagh => 0x2D30,
//    //  Block.EthiopicExtended => 0x2D80,
//    //  Block.CyrillicExtendedA => 0x2DE0,
//    //  Block.SupplementalPunctuation => 0x2E00,
//    //  Block.CJKRadicalsSupplement => 0x2E80,
//    //  Block.KangxiRadicals => 0x2F00,
//    //  Block.IdeographicDescriptionCharacters => 0x2FF0,
//    //  Block.CJKSymbolsandPunctuation => 0x3000,
//    //  Block.Hiragana => 0x3040,
//    //  Block.Katakana => 0x30A0,
//    //  Block.Bopomofo => 0x3100,
//    //  Block.HangulCompatibilityJamo => 0x3130,
//    //  Block.Kanbun => 0x3190,
//    //  Block.BopomofoExtended => 0x31A0,
//    //  Block.CJKStrokes => 0x31C0,
//    //  Block.KatakanaPhoneticExtensions => 0x31F0,
//    //  Block.EnclosedCJKLettersandMonths => 0x3200,
//    //  Block.CJKCompatibility => 0x3300,
//    //  Block.CJKUnifiedIdeographsExtensionA => 0x3400,
//    //  Block.YijingHexagramSymbols => 0x4DC0,
//    //  Block.CJKUnifiedIdeographs => 0x4E00,
//    //  Block.YiSyllables => 0xA000,
//    //  Block.YiRadicals => 0xA490,
//    //  Block.Lisu => 0xA4D0,
//    //  Block.Vai => 0xA500,
//    //  Block.CyrillicExtendedB => 0xA640,
//    //  Block.Bamum => 0xA6A0,
//    //  Block.ModifierToneLetters => 0xA700,
//    //  Block.LatinExtendedD => 0xA720,
//    //  Block.SylotiNagri => 0xA800,
//    //  Block.CommonIndicNumberForms => 0xA830,
//    //  Block.Phagspa => 0xA840,
//    //  Block.Saurashtra => 0xA880,
//    //  Block.DevanagariExtended => 0xA8E0,
//    //  Block.KayahLi => 0xA900,
//    //  Block.Rejang => 0xA930,
//    //  Block.HangulJamoExtendedA => 0xA960,
//    //  Block.Javanese => 0xA980,
//    //  Block.MyanmarExtendedB => 0xA9E0,
//    //  Block.Cham => 0xAA00,
//    //  Block.MyanmarExtendedA => 0xAA60,
//    //  Block.TaiViet => 0xAA80,
//    //  Block.MeeteiMayekExtensions => 0xAAE0,
//    //  Block.EthiopicExtendedA => 0xAB00,
//    //  Block.LatinExtendedE => 0xAB30,
//    //  Block.CherokeeSupplement => 0xAB70,
//    //  Block.MeeteiMayek => 0xABC0,
//    //  Block.HangulSyllables => 0xAC00,
//    //  Block.HangulJamoExtendedB => 0xD7B0,
//    //  Block.HighSurrogates => 0xD800,
//    //  Block.HighPrivateUseSurrogates => 0xDB80,
//    //  Block.LowSurrogates => 0xDC00,
//    //  Block.PrivateUseArea => 0xE000,
//    //  Block.CJKCompatibilityIdeographs => 0xF900,
//    //  Block.AlphabeticPresentationForms => 0xFB00,
//    //  Block.ArabicPresentationFormsA => 0xFB50,
//    //  Block.VariationSelectors => 0xFE00,
//    //  Block.VerticalForms => 0xFE10,
//    //  Block.CombiningHalfMarks => 0xFE20,
//    //  Block.CJKCompatibilityForms => 0xFE30,
//    //  Block.SmallFormVariants => 0xFE50,
//    //  Block.ArabicPresentationFormsB => 0xFE70,
//    //  Block.HalfwidthandFullwidthForms => 0xFF00,
//    //  Block.Specials => 0xFFF0,
//    //  Block.LinearBSyllabary => 0x10000,
//    //  Block.LinearBIdeograms => 0x10080,
//    //  Block.AegeanNumbers => 0x10100,
//    //  Block.AncientGreekNumbers => 0x10140,
//    //  Block.AncientSymbols => 0x10190,
//    //  Block.PhaistosDisc => 0x101D0,
//    //  Block.Lycian => 0x10280,
//    //  Block.Carian => 0x102A0,
//    //  Block.CopticEpactNumbers => 0x102E0,
//    //  Block.OldItalic => 0x10300,
//    //  Block.Gothic => 0x10330,
//    //  Block.OldPermic => 0x10350,
//    //  Block.Ugaritic => 0x10380,
//    //  Block.OldPersian => 0x103A0,
//    //  Block.Deseret => 0x10400,
//    //  Block.Shavian => 0x10450,
//    //  Block.Osmanya => 0x10480,
//    //  Block.Osage => 0x104B0,
//    //  Block.Elbasan => 0x10500,
//    //  Block.CaucasianAlbanian => 0x10530,
//    //  Block.LinearA => 0x10600,
//    //  Block.CypriotSyllabary => 0x10800,
//    //  Block.ImperialAramaic => 0x10840,
//    //  Block.Palmyrene => 0x10860,
//    //  Block.Nabataean => 0x10880,
//    //  Block.Hatran => 0x108E0,
//    //  Block.Phoenician => 0x10900,
//    //  Block.Lydian => 0x10920,
//    //  Block.MeroiticHieroglyphs => 0x10980,
//    //  Block.MeroiticCursive => 0x109A0,
//    //  Block.Kharoshthi => 0x10A00,
//    //  Block.OldSouthArabian => 0x10A60,
//    //  Block.OldNorthArabian => 0x10A80,
//    //  Block.Manichaean => 0x10AC0,
//    //  Block.Avestan => 0x10B00,
//    //  Block.InscriptionalParthian => 0x10B40,
//    //  Block.InscriptionalPahlavi => 0x10B60,
//    //  Block.PsalterPahlavi => 0x10B80,
//    //  Block.OldTurkic => 0x10C00,
//    //  Block.OldHungarian => 0x10C80,
//    //  Block.HanifiRohingya => 0x10D00,
//    //  Block.RumiNumeralSymbols => 0x10E60,
//    //  Block.OldSogdian => 0x10F00,
//    //  Block.Sogdian => 0x10F30,
//    //  Block.Elymaic => 0x10FE0,
//    //  Block.Brahmi => 0x11000,
//    //  Block.Kaithi => 0x11080,
//    //  Block.SoraSompeng => 0x110D0,
//    //  Block.Chakma => 0x11100,
//    //  Block.Mahajani => 0x11150,
//    //  Block.Sharada => 0x11180,
//    //  Block.SinhalaArchaicNumbers => 0x111E0,
//    //  Block.Khojki => 0x11200,
//    //  Block.Multani => 0x11280,
//    //  Block.Khudawadi => 0x112B0,
//    //  Block.Grantha => 0x11300,
//    //  Block.Newa => 0x11400,
//    //  Block.Tirhuta => 0x11480,
//    //  Block.Siddham => 0x11580,
//    //  Block.Modi => 0x11600,
//    //  Block.MongolianSupplement => 0x11660,
//    //  Block.Takri => 0x11680,
//    //  Block.Ahom => 0x11700,
//    //  Block.Dogra => 0x11800,
//    //  Block.WarangCiti => 0x118A0,
//    //  Block.Nandinagari => 0x119A0,
//    //  Block.ZanabazarSquare => 0x11A00,
//    //  Block.Soyombo => 0x11A50,
//    //  Block.PauCinHau => 0x11AC0,
//    //  Block.Bhaiksuki => 0x11C00,
//    //  Block.Marchen => 0x11C70,
//    //  Block.MasaramGondi => 0x11D00,
//    //  Block.GunjalaGondi => 0x11D60,
//    //  Block.Makasar => 0x11EE0,
//    //  Block.TamilSupplement => 0x11FC0,
//    //  Block.Cuneiform => 0x12000,
//    //  Block.CuneiformNumbersandPunctuation => 0x12400,
//    //  Block.EarlyDynasticCuneiform => 0x12480,
//    //  Block.EgyptianHieroglyphs => 0x13000,
//    //  Block.EgyptianHieroglyphFormatControls => 0x13430,
//    //  Block.AnatolianHieroglyphs => 0x14400,
//    //  Block.BamumSupplement => 0x16800,
//    //  Block.Mro => 0x16A40,
//    //  Block.BassaVah => 0x16AD0,
//    //  Block.PahawhHmong => 0x16B00,
//    //  Block.Medefaidrin => 0x16E40,
//    //  Block.Miao => 0x16F00,
//    //  Block.IdeographicSymbolsandPunctuation => 0x16FE0,
//    //  Block.Tangut => 0x17000,
//    //  Block.TangutComponents => 0x18800,
//    //  Block.KanaSupplement => 0x1B000,
//    //  Block.KanaExtendedA => 0x1B100,
//    //  Block.SmallKanaExtension => 0x1B130,
//    //  Block.Nushu => 0x1B170,
//    //  Block.Duployan => 0x1BC00,
//    //  Block.ShorthandFormatControls => 0x1BCA0,
//    //  Block.ByzantineMusicalSymbols => 0x1D000,
//    //  Block.MusicalSymbols => 0x1D100,
//    //  Block.AncientGreekMusicalNotation => 0x1D200,
//    //  Block.MayanNumerals => 0x1D2E0,
//    //  Block.TaiXuanJingSymbols => 0x1D300,
//    //  Block.CountingRodNumerals => 0x1D360,
//    //  Block.MathematicalAlphanumericSymbols => 0x1D400,
//    //  Block.SuttonSignWriting => 0x1D800,
//    //  Block.GlagoliticSupplement => 0x1E000,
//    //  Block.NyiakengPuachueHmong => 0x1E100,
//    //  Block.Wancho => 0x1E2C0,
//    //  Block.MendeKikakui => 0x1E800,
//    //  Block.Adlam => 0x1E900,
//    //  Block.IndicSiyaqNumbers => 0x1EC70,
//    //  Block.OttomanSiyaqNumbers => 0x1ED00,
//    //  Block.ArabicMathematicalAlphabeticSymbols => 0x1EE00,
//    //  Block.MahjongTiles => 0x1F000,
//    //  Block.DominoTiles => 0x1F030,
//    //  Block.PlayingCards => 0x1F0A0,
//    //  Block.EnclosedAlphanumericSupplement => 0x1F100,
//    //  Block.EnclosedIdeographicSupplement => 0x1F200,
//    //  Block.MiscellaneousSymbolsandPictographs => 0x1F300,
//    //  Block.Emoticons => 0x1F600,
//    //  Block.OrnamentalDingbats => 0x1F650,
//    //  Block.TransportandMapSymbols => 0x1F680,
//    //  Block.AlchemicalSymbols => 0x1F700,
//    //  Block.GeometricShapesExtended => 0x1F780,
//    //  Block.SupplementalArrowsC => 0x1F800,
//    //  Block.SupplementalSymbolsandPictographs => 0x1F900,
//    //  Block.ChessSymbols => 0x1FA00,
//    //  Block.SymbolsandPictographsExtendedA => 0x1FA70,
//    //  Block.CJKUnifiedIdeographsExtensionB => 0x20000,
//    //  Block.CJKUnifiedIdeographsExtensionC => 0x2A700,
//    //  Block.CJKUnifiedIdeographsExtensionD => 0x2B740,
//    //  Block.CJKUnifiedIdeographsExtensionE => 0x2B820,
//    //  Block.CJKUnifiedIdeographsExtensionF => 0x2CEB0,
//    //  Block.CJKCompatibilityIdeographsSupplement => 0x2F800,
//    //  Block.Tags => 0xE0000,
//    //  Block.VariationSelectorsSupplement => 0xE0100,
//    //  Block.SupplementaryPrivateUseAreaA => 0xF0000,
//    //  Block.SupplementaryPrivateUseAreaB => 0x100000,
//    //  _ => throw new System.ArgumentOutOfRangeException(nameof(source)),
//    //};
//    //public static int GetLastCodePoint(this Block source) => source switch
//    //{
//    //  Block.BasicLatin => 0x7F,
//    //  Block.Latin1Supplement => 0xFF,
//    //  Block.LatinExtendedA => 0x17F,
//    //  Block.LatinExtendedB => 0x24F,
//    //  Block.IPAExtensions => 0x2AF,
//    //  Block.SpacingModifierLetters => 0x2FF,
//    //  Block.CombiningDiacriticalMarks => 0x36F,
//    //  Block.GreekandCoptic => 0x3FF,
//    //  Block.Cyrillic => 0x4FF,
//    //  Block.CyrillicSupplement => 0x52F,
//    //  Block.Armenian => 0x58F,
//    //  Block.Hebrew => 0x5FF,
//    //  Block.Arabic => 0x6FF,
//    //  Block.Syriac => 0x74F,
//    //  Block.ArabicSupplement => 0x77F,
//    //  Block.Thaana => 0x7BF,
//    //  Block.NKo => 0x7FF,
//    //  Block.Samaritan => 0x83F,
//    //  Block.Mandaic => 0x85F,
//    //  Block.SyriacSupplement => 0x86F,
//    //  Block.ArabicExtendedA => 0x8FF,
//    //  Block.Devanagari => 0x97F,
//    //  Block.Bengali => 0x9FF,
//    //  Block.Gurmukhi => 0xA7F,
//    //  Block.Gujarati => 0xAFF,
//    //  Block.Oriya => 0xB7F,
//    //  Block.Tamil => 0xBFF,
//    //  Block.Telugu => 0xC7F,
//    //  Block.Kannada => 0xCFF,
//    //  Block.Malayalam => 0xD7F,
//    //  Block.Sinhala => 0xDFF,
//    //  Block.Thai => 0xE7F,
//    //  Block.Lao => 0xEFF,
//    //  Block.Tibetan => 0xFFF,
//    //  Block.Myanmar => 0x109F,
//    //  Block.Georgian => 0x10FF,
//    //  Block.HangulJamo => 0x11FF,
//    //  Block.Ethiopic => 0x137F,
//    //  Block.EthiopicSupplement => 0x139F,
//    //  Block.Cherokee => 0x13FF,
//    //  Block.UnifiedCanadianAboriginalSyllabics => 0x167F,
//    //  Block.Ogham => 0x169F,
//    //  Block.Runic => 0x16FF,
//    //  Block.Tagalog => 0x171F,
//    //  Block.Hanunoo => 0x173F,
//    //  Block.Buhid => 0x175F,
//    //  Block.Tagbanwa => 0x177F,
//    //  Block.Khmer => 0x17FF,
//    //  Block.Mongolian => 0x18AF,
//    //  Block.UnifiedCanadianAboriginalSyllabicsExtended => 0x18FF,
//    //  Block.Limbu => 0x194F,
//    //  Block.TaiLe => 0x197F,
//    //  Block.NewTaiLue => 0x19DF,
//    //  Block.KhmerSymbols => 0x19FF,
//    //  Block.Buginese => 0x1A1F,
//    //  Block.TaiTham => 0x1AAF,
//    //  Block.CombiningDiacriticalMarksExtended => 0x1AFF,
//    //  Block.Balinese => 0x1B7F,
//    //  Block.Sundanese => 0x1BBF,
//    //  Block.Batak => 0x1BFF,
//    //  Block.Lepcha => 0x1C4F,
//    //  Block.OlChiki => 0x1C7F,
//    //  Block.CyrillicExtendedC => 0x1C8F,
//    //  Block.GeorgianExtended => 0x1CBF,
//    //  Block.SundaneseSupplement => 0x1CCF,
//    //  Block.VedicExtensions => 0x1CFF,
//    //  Block.PhoneticExtensions => 0x1D7F,
//    //  Block.PhoneticExtensionsSupplement => 0x1DBF,
//    //  Block.CombiningDiacriticalMarksSupplement => 0x1DFF,
//    //  Block.LatinExtendedAdditional => 0x1EFF,
//    //  Block.GreekExtended => 0x1FFF,
//    //  Block.GeneralPunctuation => 0x206F,
//    //  Block.SuperscriptsandSubscripts => 0x209F,
//    //  Block.CurrencySymbols => 0x20CF,
//    //  Block.CombiningDiacriticalMarksforSymbols => 0x20FF,
//    //  Block.LetterlikeSymbols => 0x214F,
//    //  Block.NumberForms => 0x218F,
//    //  Block.Arrows => 0x21FF,
//    //  Block.MathematicalOperators => 0x22FF,
//    //  Block.MiscellaneousTechnical => 0x23FF,
//    //  Block.ControlPictures => 0x243F,
//    //  Block.OpticalCharacterRecognition => 0x245F,
//    //  Block.EnclosedAlphanumerics => 0x24FF,
//    //  Block.BoxDrawing => 0x257F,
//    //  Block.BlockElements => 0x259F,
//    //  Block.GeometricShapes => 0x25FF,
//    //  Block.MiscellaneousSymbols => 0x26FF,
//    //  Block.Dingbats => 0x27BF,
//    //  Block.MiscellaneousMathematicalSymbolsA => 0x27EF,
//    //  Block.SupplementalArrowsA => 0x27FF,
//    //  Block.BraillePatterns => 0x28FF,
//    //  Block.SupplementalArrowsB => 0x297F,
//    //  Block.MiscellaneousMathematicalSymbolsB => 0x29FF,
//    //  Block.SupplementalMathematicalOperators => 0x2AFF,
//    //  Block.MiscellaneousSymbolsandArrows => 0x2BFF,
//    //  Block.Glagolitic => 0x2C5F,
//    //  Block.LatinExtendedC => 0x2C7F,
//    //  Block.Coptic => 0x2CFF,
//    //  Block.GeorgianSupplement => 0x2D2F,
//    //  Block.Tifinagh => 0x2D7F,
//    //  Block.EthiopicExtended => 0x2DDF,
//    //  Block.CyrillicExtendedA => 0x2DFF,
//    //  Block.SupplementalPunctuation => 0x2E7F,
//    //  Block.CJKRadicalsSupplement => 0x2EFF,
//    //  Block.KangxiRadicals => 0x2FDF,
//    //  Block.IdeographicDescriptionCharacters => 0x2FFF,
//    //  Block.CJKSymbolsandPunctuation => 0x303F,
//    //  Block.Hiragana => 0x309F,
//    //  Block.Katakana => 0x30FF,
//    //  Block.Bopomofo => 0x312F,
//    //  Block.HangulCompatibilityJamo => 0x318F,
//    //  Block.Kanbun => 0x319F,
//    //  Block.BopomofoExtended => 0x31BF,
//    //  Block.CJKStrokes => 0x31EF,
//    //  Block.KatakanaPhoneticExtensions => 0x31FF,
//    //  Block.EnclosedCJKLettersandMonths => 0x32FF,
//    //  Block.CJKCompatibility => 0x33FF,
//    //  Block.CJKUnifiedIdeographsExtensionA => 0x4DBF,
//    //  Block.YijingHexagramSymbols => 0x4DFF,
//    //  Block.CJKUnifiedIdeographs => 0x9FFF,
//    //  Block.YiSyllables => 0xA48F,
//    //  Block.YiRadicals => 0xA4CF,
//    //  Block.Lisu => 0xA4FF,
//    //  Block.Vai => 0xA63F,
//    //  Block.CyrillicExtendedB => 0xA69F,
//    //  Block.Bamum => 0xA6FF,
//    //  Block.ModifierToneLetters => 0xA71F,
//    //  Block.LatinExtendedD => 0xA7FF,
//    //  Block.SylotiNagri => 0xA82F,
//    //  Block.CommonIndicNumberForms => 0xA83F,
//    //  Block.Phagspa => 0xA87F,
//    //  Block.Saurashtra => 0xA8DF,
//    //  Block.DevanagariExtended => 0xA8FF,
//    //  Block.KayahLi => 0xA92F,
//    //  Block.Rejang => 0xA95F,
//    //  Block.HangulJamoExtendedA => 0xA97F,
//    //  Block.Javanese => 0xA9DF,
//    //  Block.MyanmarExtendedB => 0xA9FF,
//    //  Block.Cham => 0xAA5F,
//    //  Block.MyanmarExtendedA => 0xAA7F,
//    //  Block.TaiViet => 0xAADF,
//    //  Block.MeeteiMayekExtensions => 0xAAFF,
//    //  Block.EthiopicExtendedA => 0xAB2F,
//    //  Block.LatinExtendedE => 0xAB6F,
//    //  Block.CherokeeSupplement => 0xABBF,
//    //  Block.MeeteiMayek => 0xABFF,
//    //  Block.HangulSyllables => 0xD7AF,
//    //  Block.HangulJamoExtendedB => 0xD7FF,
//    //  Block.HighSurrogates => 0xDB7F,
//    //  Block.HighPrivateUseSurrogates => 0xDBFF,
//    //  Block.LowSurrogates => 0xDFFF,
//    //  Block.PrivateUseArea => 0xF8FF,
//    //  Block.CJKCompatibilityIdeographs => 0xFAFF,
//    //  Block.AlphabeticPresentationForms => 0xFB4F,
//    //  Block.ArabicPresentationFormsA => 0xFDFF,
//    //  Block.VariationSelectors => 0xFE0F,
//    //  Block.VerticalForms => 0xFE1F,
//    //  Block.CombiningHalfMarks => 0xFE2F,
//    //  Block.CJKCompatibilityForms => 0xFE4F,
//    //  Block.SmallFormVariants => 0xFE6F,
//    //  Block.ArabicPresentationFormsB => 0xFEFF,
//    //  Block.HalfwidthandFullwidthForms => 0xFFEF,
//    //  Block.Specials => 0xFFFF,
//    //  Block.LinearBSyllabary => 0x1007F,
//    //  Block.LinearBIdeograms => 0x100FF,
//    //  Block.AegeanNumbers => 0x1013F,
//    //  Block.AncientGreekNumbers => 0x1018F,
//    //  Block.AncientSymbols => 0x101CF,
//    //  Block.PhaistosDisc => 0x101FF,
//    //  Block.Lycian => 0x1029F,
//    //  Block.Carian => 0x102DF,
//    //  Block.CopticEpactNumbers => 0x102FF,
//    //  Block.OldItalic => 0x1032F,
//    //  Block.Gothic => 0x1034F,
//    //  Block.OldPermic => 0x1037F,
//    //  Block.Ugaritic => 0x1039F,
//    //  Block.OldPersian => 0x103DF,
//    //  Block.Deseret => 0x1044F,
//    //  Block.Shavian => 0x1047F,
//    //  Block.Osmanya => 0x104AF,
//    //  Block.Osage => 0x104FF,
//    //  Block.Elbasan => 0x1052F,
//    //  Block.CaucasianAlbanian => 0x1056F,
//    //  Block.LinearA => 0x1077F,
//    //  Block.CypriotSyllabary => 0x1083F,
//    //  Block.ImperialAramaic => 0x1085F,
//    //  Block.Palmyrene => 0x1087F,
//    //  Block.Nabataean => 0x108AF,
//    //  Block.Hatran => 0x108FF,
//    //  Block.Phoenician => 0x1091F,
//    //  Block.Lydian => 0x1093F,
//    //  Block.MeroiticHieroglyphs => 0x1099F,
//    //  Block.MeroiticCursive => 0x109FF,
//    //  Block.Kharoshthi => 0x10A5F,
//    //  Block.OldSouthArabian => 0x10A7F,
//    //  Block.OldNorthArabian => 0x10A9F,
//    //  Block.Manichaean => 0x10AFF,
//    //  Block.Avestan => 0x10B3F,
//    //  Block.InscriptionalParthian => 0x10B5F,
//    //  Block.InscriptionalPahlavi => 0x10B7F,
//    //  Block.PsalterPahlavi => 0x10BAF,
//    //  Block.OldTurkic => 0x10C4F,
//    //  Block.OldHungarian => 0x10CFF,
//    //  Block.HanifiRohingya => 0x10D3F,
//    //  Block.RumiNumeralSymbols => 0x10E7F,
//    //  Block.OldSogdian => 0x10F2F,
//    //  Block.Sogdian => 0x10F6F,
//    //  Block.Elymaic => 0x10FFF,
//    //  Block.Brahmi => 0x1107F,
//    //  Block.Kaithi => 0x110CF,
//    //  Block.SoraSompeng => 0x110FF,
//    //  Block.Chakma => 0x1114F,
//    //  Block.Mahajani => 0x1117F,
//    //  Block.Sharada => 0x111DF,
//    //  Block.SinhalaArchaicNumbers => 0x111FF,
//    //  Block.Khojki => 0x1124F,
//    //  Block.Multani => 0x112AF,
//    //  Block.Khudawadi => 0x112FF,
//    //  Block.Grantha => 0x1137F,
//    //  Block.Newa => 0x1147F,
//    //  Block.Tirhuta => 0x114DF,
//    //  Block.Siddham => 0x115FF,
//    //  Block.Modi => 0x1165F,
//    //  Block.MongolianSupplement => 0x1167F,
//    //  Block.Takri => 0x116CF,
//    //  Block.Ahom => 0x1173F,
//    //  Block.Dogra => 0x1184F,
//    //  Block.WarangCiti => 0x118FF,
//    //  Block.Nandinagari => 0x119FF,
//    //  Block.ZanabazarSquare => 0x11A4F,
//    //  Block.Soyombo => 0x11AAF,
//    //  Block.PauCinHau => 0x11AFF,
//    //  Block.Bhaiksuki => 0x11C6F,
//    //  Block.Marchen => 0x11CBF,
//    //  Block.MasaramGondi => 0x11D5F,
//    //  Block.GunjalaGondi => 0x11DAF,
//    //  Block.Makasar => 0x11EFF,
//    //  Block.TamilSupplement => 0x11FFF,
//    //  Block.Cuneiform => 0x123FF,
//    //  Block.CuneiformNumbersandPunctuation => 0x1247F,
//    //  Block.EarlyDynasticCuneiform => 0x1254F,
//    //  Block.EgyptianHieroglyphs => 0x1342F,
//    //  Block.EgyptianHieroglyphFormatControls => 0x1343F,
//    //  Block.AnatolianHieroglyphs => 0x1467F,
//    //  Block.BamumSupplement => 0x16A3F,
//    //  Block.Mro => 0x16A6F,
//    //  Block.BassaVah => 0x16AFF,
//    //  Block.PahawhHmong => 0x16B8F,
//    //  Block.Medefaidrin => 0x16E9F,
//    //  Block.Miao => 0x16F9F,
//    //  Block.IdeographicSymbolsandPunctuation => 0x16FFF,
//    //  Block.Tangut => 0x187FF,
//    //  Block.TangutComponents => 0x18AFF,
//    //  Block.KanaSupplement => 0x1B0FF,
//    //  Block.KanaExtendedA => 0x1B12F,
//    //  Block.SmallKanaExtension => 0x1B16F,
//    //  Block.Nushu => 0x1B2FF,
//    //  Block.Duployan => 0x1BC9F,
//    //  Block.ShorthandFormatControls => 0x1BCAF,
//    //  Block.ByzantineMusicalSymbols => 0x1D0FF,
//    //  Block.MusicalSymbols => 0x1D1FF,
//    //  Block.AncientGreekMusicalNotation => 0x1D24F,
//    //  Block.MayanNumerals => 0x1D2FF,
//    //  Block.TaiXuanJingSymbols => 0x1D35F,
//    //  Block.CountingRodNumerals => 0x1D37F,
//    //  Block.MathematicalAlphanumericSymbols => 0x1D7FF,
//    //  Block.SuttonSignWriting => 0x1DAAF,
//    //  Block.GlagoliticSupplement => 0x1E02F,
//    //  Block.NyiakengPuachueHmong => 0x1E14F,
//    //  Block.Wancho => 0x1E2FF,
//    //  Block.MendeKikakui => 0x1E8DF,
//    //  Block.Adlam => 0x1E95F,
//    //  Block.IndicSiyaqNumbers => 0x1ECBF,
//    //  Block.OttomanSiyaqNumbers => 0x1ED4F,
//    //  Block.ArabicMathematicalAlphabeticSymbols => 0x1EEFF,
//    //  Block.MahjongTiles => 0x1F02F,
//    //  Block.DominoTiles => 0x1F09F,
//    //  Block.PlayingCards => 0x1F0FF,
//    //  Block.EnclosedAlphanumericSupplement => 0x1F1FF,
//    //  Block.EnclosedIdeographicSupplement => 0x1F2FF,
//    //  Block.MiscellaneousSymbolsandPictographs => 0x1F5FF,
//    //  Block.Emoticons => 0x1F64F,
//    //  Block.OrnamentalDingbats => 0x1F67F,
//    //  Block.TransportandMapSymbols => 0x1F6FF,
//    //  Block.AlchemicalSymbols => 0x1F77F,
//    //  Block.GeometricShapesExtended => 0x1F7FF,
//    //  Block.SupplementalArrowsC => 0x1F8FF,
//    //  Block.SupplementalSymbolsandPictographs => 0x1F9FF,
//    //  Block.ChessSymbols => 0x1FA6F,
//    //  Block.SymbolsandPictographsExtendedA => 0x1FAFF,
//    //  Block.CJKUnifiedIdeographsExtensionB => 0x2A6DF,
//    //  Block.CJKUnifiedIdeographsExtensionC => 0x2B73F,
//    //  Block.CJKUnifiedIdeographsExtensionD => 0x2B81F,
//    //  Block.CJKUnifiedIdeographsExtensionE => 0x2CEAF,
//    //  Block.CJKUnifiedIdeographsExtensionF => 0x2EBEF,
//    //  Block.CJKCompatibilityIdeographsSupplement => 0x2FA1F,
//    //  Block.Tags => 0xE007F,
//    //  Block.VariationSelectorsSupplement => 0xE01EF,
//    //  Block.SupplementaryPrivateUseAreaA => 0xFFFFF,
//    //  Block.SupplementaryPrivateUseAreaB => 0x10FFFF,
//    //  _ => throw new System.ArgumentOutOfRangeException(nameof(source))
//    //};

//    //#region Unicode Blocks
//    //public enum Block
//    //{
//    //  #region All Unicode Blocks
//    //  BasicLatin,
//    //  Latin1Supplement,
//    //  LatinExtendedA,
//    //  LatinExtendedB,
//    //  IPAExtensions,
//    //  SpacingModifierLetters,
//    //  CombiningDiacriticalMarks,
//    //  GreekandCoptic,
//    //  Cyrillic,
//    //  CyrillicSupplement,
//    //  Armenian,
//    //  Hebrew,
//    //  Arabic,
//    //  Syriac,
//    //  ArabicSupplement,
//    //  Thaana,
//    //  NKo,
//    //  Samaritan,
//    //  Mandaic,
//    //  SyriacSupplement,
//    //  ArabicExtendedA,
//    //  Devanagari,
//    //  Bengali,
//    //  Gurmukhi,
//    //  Gujarati,
//    //  Oriya,
//    //  Tamil,
//    //  Telugu,
//    //  Kannada,
//    //  Malayalam,
//    //  Sinhala,
//    //  Thai,
//    //  Lao,
//    //  Tibetan,
//    //  Myanmar,
//    //  Georgian,
//    //  HangulJamo,
//    //  Ethiopic,
//    //  EthiopicSupplement,
//    //  Cherokee,
//    //  UnifiedCanadianAboriginalSyllabics,
//    //  Ogham,
//    //  Runic,
//    //  Tagalog,
//    //  Hanunoo,
//    //  Buhid,
//    //  Tagbanwa,
//    //  Khmer,
//    //  Mongolian,
//    //  UnifiedCanadianAboriginalSyllabicsExtended,
//    //  Limbu,
//    //  TaiLe,
//    //  NewTaiLue,
//    //  KhmerSymbols,
//    //  Buginese,
//    //  TaiTham,
//    //  CombiningDiacriticalMarksExtended,
//    //  Balinese,
//    //  Sundanese,
//    //  Batak,
//    //  Lepcha,
//    //  OlChiki,
//    //  CyrillicExtendedC,
//    //  GeorgianExtended,
//    //  SundaneseSupplement,
//    //  VedicExtensions,
//    //  PhoneticExtensions,
//    //  PhoneticExtensionsSupplement,
//    //  CombiningDiacriticalMarksSupplement,
//    //  LatinExtendedAdditional,
//    //  GreekExtended,
//    //  GeneralPunctuation,
//    //  SuperscriptsandSubscripts,
//    //  CurrencySymbols,
//    //  CombiningDiacriticalMarksforSymbols,
//    //  LetterlikeSymbols,
//    //  NumberForms,
//    //  Arrows,
//    //  MathematicalOperators,
//    //  MiscellaneousTechnical,
//    //  ControlPictures,
//    //  OpticalCharacterRecognition,
//    //  EnclosedAlphanumerics,
//    //  BoxDrawing,
//    //  BlockElements,
//    //  GeometricShapes,
//    //  MiscellaneousSymbols,
//    //  Dingbats,
//    //  MiscellaneousMathematicalSymbolsA,
//    //  SupplementalArrowsA,
//    //  BraillePatterns,
//    //  SupplementalArrowsB,
//    //  MiscellaneousMathematicalSymbolsB,
//    //  SupplementalMathematicalOperators,
//    //  MiscellaneousSymbolsandArrows,
//    //  Glagolitic,
//    //  LatinExtendedC,
//    //  Coptic,
//    //  GeorgianSupplement,
//    //  Tifinagh,
//    //  EthiopicExtended,
//    //  CyrillicExtendedA,
//    //  SupplementalPunctuation,
//    //  CJKRadicalsSupplement,
//    //  KangxiRadicals,
//    //  IdeographicDescriptionCharacters,
//    //  CJKSymbolsandPunctuation,
//    //  Hiragana,
//    //  Katakana,
//    //  Bopomofo,
//    //  HangulCompatibilityJamo,
//    //  Kanbun,
//    //  BopomofoExtended,
//    //  CJKStrokes,
//    //  KatakanaPhoneticExtensions,
//    //  EnclosedCJKLettersandMonths,
//    //  CJKCompatibility,
//    //  CJKUnifiedIdeographsExtensionA,
//    //  YijingHexagramSymbols,
//    //  CJKUnifiedIdeographs,
//    //  YiSyllables,
//    //  YiRadicals,
//    //  Lisu,
//    //  Vai,
//    //  CyrillicExtendedB,
//    //  Bamum,
//    //  ModifierToneLetters,
//    //  LatinExtendedD,
//    //  SylotiNagri,
//    //  CommonIndicNumberForms,
//    //  Phagspa,
//    //  Saurashtra,
//    //  DevanagariExtended,
//    //  KayahLi,
//    //  Rejang,
//    //  HangulJamoExtendedA,
//    //  Javanese,
//    //  MyanmarExtendedB,
//    //  Cham,
//    //  MyanmarExtendedA,
//    //  TaiViet,
//    //  MeeteiMayekExtensions,
//    //  EthiopicExtendedA,
//    //  LatinExtendedE,
//    //  CherokeeSupplement,
//    //  MeeteiMayek,
//    //  HangulSyllables,
//    //  HangulJamoExtendedB,
//    //  HighSurrogates,
//    //  HighPrivateUseSurrogates,
//    //  LowSurrogates,
//    //  PrivateUseArea,
//    //  CJKCompatibilityIdeographs,
//    //  AlphabeticPresentationForms,
//    //  ArabicPresentationFormsA,
//    //  VariationSelectors,
//    //  VerticalForms,
//    //  CombiningHalfMarks,
//    //  CJKCompatibilityForms,
//    //  SmallFormVariants,
//    //  ArabicPresentationFormsB,
//    //  HalfwidthandFullwidthForms,
//    //  Specials,
//    //  LinearBSyllabary,
//    //  LinearBIdeograms,
//    //  AegeanNumbers,
//    //  AncientGreekNumbers,
//    //  AncientSymbols,
//    //  PhaistosDisc,
//    //  Lycian,
//    //  Carian,
//    //  CopticEpactNumbers,
//    //  OldItalic,
//    //  Gothic,
//    //  OldPermic,
//    //  Ugaritic,
//    //  OldPersian,
//    //  Deseret,
//    //  Shavian,
//    //  Osmanya,
//    //  Osage,
//    //  Elbasan,
//    //  CaucasianAlbanian,
//    //  LinearA,
//    //  CypriotSyllabary,
//    //  ImperialAramaic,
//    //  Palmyrene,
//    //  Nabataean,
//    //  Hatran,
//    //  Phoenician,
//    //  Lydian,
//    //  MeroiticHieroglyphs,
//    //  MeroiticCursive,
//    //  Kharoshthi,
//    //  OldSouthArabian,
//    //  OldNorthArabian,
//    //  Manichaean,
//    //  Avestan,
//    //  InscriptionalParthian,
//    //  InscriptionalPahlavi,
//    //  PsalterPahlavi,
//    //  OldTurkic,
//    //  OldHungarian,
//    //  HanifiRohingya,
//    //  RumiNumeralSymbols,
//    //  OldSogdian,
//    //  Sogdian,
//    //  Elymaic,
//    //  Brahmi,
//    //  Kaithi,
//    //  SoraSompeng,
//    //  Chakma,
//    //  Mahajani,
//    //  Sharada,
//    //  SinhalaArchaicNumbers,
//    //  Khojki,
//    //  Multani,
//    //  Khudawadi,
//    //  Grantha,
//    //  Newa,
//    //  Tirhuta,
//    //  Siddham,
//    //  Modi,
//    //  MongolianSupplement,
//    //  Takri,
//    //  Ahom,
//    //  Dogra,
//    //  WarangCiti,
//    //  Nandinagari,
//    //  ZanabazarSquare,
//    //  Soyombo,
//    //  PauCinHau,
//    //  Bhaiksuki,
//    //  Marchen,
//    //  MasaramGondi,
//    //  GunjalaGondi,
//    //  Makasar,
//    //  TamilSupplement,
//    //  Cuneiform,
//    //  CuneiformNumbersandPunctuation,
//    //  EarlyDynasticCuneiform,
//    //  EgyptianHieroglyphs,
//    //  EgyptianHieroglyphFormatControls,
//    //  AnatolianHieroglyphs,
//    //  BamumSupplement,
//    //  Mro,
//    //  BassaVah,
//    //  PahawhHmong,
//    //  Medefaidrin,
//    //  Miao,
//    //  IdeographicSymbolsandPunctuation,
//    //  Tangut,
//    //  TangutComponents,
//    //  KanaSupplement,
//    //  KanaExtendedA,
//    //  SmallKanaExtension,
//    //  Nushu,
//    //  Duployan,
//    //  ShorthandFormatControls,
//    //  ByzantineMusicalSymbols,
//    //  MusicalSymbols,
//    //  AncientGreekMusicalNotation,
//    //  MayanNumerals,
//    //  TaiXuanJingSymbols,
//    //  CountingRodNumerals,
//    //  MathematicalAlphanumericSymbols,
//    //  SuttonSignWriting,
//    //  GlagoliticSupplement,
//    //  NyiakengPuachueHmong,
//    //  Wancho,
//    //  MendeKikakui,
//    //  Adlam,
//    //  IndicSiyaqNumbers,
//    //  OttomanSiyaqNumbers,
//    //  ArabicMathematicalAlphabeticSymbols,
//    //  MahjongTiles,
//    //  DominoTiles,
//    //  PlayingCards,
//    //  EnclosedAlphanumericSupplement,
//    //  EnclosedIdeographicSupplement,
//    //  MiscellaneousSymbolsandPictographs,
//    //  Emoticons,
//    //  OrnamentalDingbats,
//    //  TransportandMapSymbols,
//    //  AlchemicalSymbols,
//    //  GeometricShapesExtended,
//    //  SupplementalArrowsC,
//    //  SupplementalSymbolsandPictographs,
//    //  ChessSymbols,
//    //  SymbolsandPictographsExtendedA,
//    //  CJKUnifiedIdeographsExtensionB,
//    //  CJKUnifiedIdeographsExtensionC,
//    //  CJKUnifiedIdeographsExtensionD,
//    //  CJKUnifiedIdeographsExtensionE,
//    //  CJKUnifiedIdeographsExtensionF,
//    //  CJKCompatibilityIdeographsSupplement,
//    //  Tags,
//    //  VariationSelectorsSupplement,
//    //  SupplementaryPrivateUseAreaA,
//    //  SupplementaryPrivateUseAreaB
//    //  #endregion All Unicode Blocks
//    //}

//    //public static System.Collections.Generic.IEnumerable<(Block, int firstCodeUnit, int lastCodeUnit)> GetBlockRanges()
//    //{
//    //  yield return (Block.BasicLatin, 0x00, 0x7F);
//    //  yield return (Block.Latin1Supplement, 0x80, 0xFF);
//    //  yield return (Block.LatinExtendedA, 0x100, 0x17F);
//    //  yield return (Block.LatinExtendedB, 0x180, 0x24F);
//    //  yield return (Block.IPAExtensions, 0x250, 0x2AF);
//    //  yield return (Block.SpacingModifierLetters, 0x2B0, 0x2FF);
//    //  yield return (Block.CombiningDiacriticalMarks, 0x300, 0x36F);
//    //  yield return (Block.GreekandCoptic, 0x370, 0x3FF);
//    //  yield return (Block.Cyrillic, 0x400, 0x4FF);
//    //  yield return (Block.CyrillicSupplement, 0x500, 0x52F);
//    //  yield return (Block.Armenian, 0x530, 0x58F);
//    //  yield return (Block.Hebrew, 0x590, 0x5FF);
//    //  yield return (Block.Arabic, 0x600, 0x6FF);
//    //  yield return (Block.Syriac, 0x700, 0x74F);
//    //  yield return (Block.ArabicSupplement, 0x750, 0x77F);
//    //  yield return (Block.Thaana, 0x780, 0x7BF);
//    //  yield return (Block.NKo, 0x7C0, 0x7FF);
//    //  yield return (Block.Samaritan, 0x800, 0x83F);
//    //  yield return (Block.Mandaic, 0x840, 0x85F);
//    //  yield return (Block.SyriacSupplement, 0x860, 0x86F);
//    //  yield return (Block.ArabicExtendedA, 0x8A0, 0x8FF);
//    //  yield return (Block.Devanagari, 0x900, 0x97F);
//    //  yield return (Block.Bengali, 0x980, 0x9FF);
//    //  yield return (Block.Gurmukhi, 0xA00, 0xA7F);
//    //  yield return (Block.Gujarati, 0xA80, 0xAFF);
//    //  yield return (Block.Oriya, 0xB00, 0xB7F);
//    //  yield return (Block.Tamil, 0xB80, 0xBFF);
//    //  yield return (Block.Telugu, 0xC00, 0xC7F);
//    //  yield return (Block.Kannada, 0xC80, 0xCFF);
//    //  yield return (Block.Malayalam, 0xD00, 0xD7F);
//    //  yield return (Block.Sinhala, 0xD80, 0xDFF);
//    //  yield return (Block.Thai, 0xE00, 0xE7F);
//    //  yield return (Block.Lao, 0xE80, 0xEFF);
//    //  yield return (Block.Tibetan, 0xF00, 0xFFF);
//    //  yield return (Block.Myanmar, 0x1000, 0x109F);
//    //  yield return (Block.Georgian, 0x10A0, 0x10FF);
//    //  yield return (Block.HangulJamo, 0x1100, 0x11FF);
//    //  yield return (Block.Ethiopic, 0x1200, 0x137F);
//    //  yield return (Block.EthiopicSupplement, 0x1380, 0x139F);
//    //  yield return (Block.Cherokee, 0x13A0, 0x13FF);
//    //  yield return (Block.UnifiedCanadianAboriginalSyllabics, 0x1400, 0x167F);
//    //  yield return (Block.Ogham, 0x1680, 0x169F);
//    //  yield return (Block.Runic, 0x16A0, 0x16FF);
//    //  yield return (Block.Tagalog, 0x1700, 0x171F);
//    //  yield return (Block.Hanunoo, 0x1720, 0x173F);
//    //  yield return (Block.Buhid, 0x1740, 0x175F);
//    //  yield return (Block.Tagbanwa, 0x1760, 0x177F);
//    //  yield return (Block.Khmer, 0x1780, 0x17FF);
//    //  yield return (Block.Mongolian, 0x1800, 0x18AF);
//    //  yield return (Block.UnifiedCanadianAboriginalSyllabicsExtended, 0x18B0, 0x18FF);
//    //  yield return (Block.Limbu, 0x1900, 0x194F);
//    //  yield return (Block.TaiLe, 0x1950, 0x197F);
//    //  yield return (Block.NewTaiLue, 0x1980, 0x19DF);
//    //  yield return (Block.KhmerSymbols, 0x19E0, 0x19FF);
//    //  yield return (Block.Buginese, 0x1A00, 0x1A1F);
//    //  yield return (Block.TaiTham, 0x1A20, 0x1AAF);
//    //  yield return (Block.CombiningDiacriticalMarksExtended, 0x1AB0, 0x1AFF);
//    //  yield return (Block.Balinese, 0x1B00, 0x1B7F);
//    //  yield return (Block.Sundanese, 0x1B80, 0x1BBF);
//    //  yield return (Block.Batak, 0x1BC0, 0x1BFF);
//    //  yield return (Block.Lepcha, 0x1C00, 0x1C4F);
//    //  yield return (Block.OlChiki, 0x1C50, 0x1C7F);
//    //  yield return (Block.CyrillicExtendedC, 0x1C80, 0x1C8F);
//    //  yield return (Block.GeorgianExtended, 0x1C90, 0x1CBF);
//    //  yield return (Block.SundaneseSupplement, 0x1CC0, 0x1CCF);
//    //  yield return (Block.VedicExtensions, 0x1CD0, 0x1CFF);
//    //  yield return (Block.PhoneticExtensions, 0x1D00, 0x1D7F);
//    //  yield return (Block.PhoneticExtensionsSupplement, 0x1D80, 0x1DBF);
//    //  yield return (Block.CombiningDiacriticalMarksSupplement, 0x1DC0, 0x1DFF);
//    //  yield return (Block.LatinExtendedAdditional, 0x1E00, 0x1EFF);
//    //  yield return (Block.GreekExtended, 0x1F00, 0x1FFF);
//    //  yield return (Block.GeneralPunctuation, 0x2000, 0x206F);
//    //  yield return (Block.SuperscriptsandSubscripts, 0x2070, 0x209F);
//    //  yield return (Block.CurrencySymbols, 0x20A0, 0x20CF);
//    //  yield return (Block.CombiningDiacriticalMarksforSymbols, 0x20D0, 0x20FF);
//    //  yield return (Block.LetterlikeSymbols, 0x2100, 0x214F);
//    //  yield return (Block.NumberForms, 0x2150, 0x218F);
//    //  yield return (Block.Arrows, 0x2190, 0x21FF);
//    //  yield return (Block.MathematicalOperators, 0x2200, 0x22FF);
//    //  yield return (Block.MiscellaneousTechnical, 0x2300, 0x23FF);
//    //  yield return (Block.ControlPictures, 0x2400, 0x243F);
//    //  yield return (Block.OpticalCharacterRecognition, 0x2440, 0x245F);
//    //  yield return (Block.EnclosedAlphanumerics, 0x2460, 0x24FF);
//    //  yield return (Block.BoxDrawing, 0x2500, 0x257F);
//    //  yield return (Block.BlockElements, 0x2580, 0x259F);
//    //  yield return (Block.GeometricShapes, 0x25A0, 0x25FF);
//    //  yield return (Block.MiscellaneousSymbols, 0x2600, 0x26FF);
//    //  yield return (Block.Dingbats, 0x2700, 0x27BF);
//    //  yield return (Block.MiscellaneousMathematicalSymbolsA, 0x27C0, 0x27EF);
//    //  yield return (Block.SupplementalArrowsA, 0x27F0, 0x27FF);
//    //  yield return (Block.BraillePatterns, 0x2800, 0x28FF);
//    //  yield return (Block.SupplementalArrowsB, 0x2900, 0x297F);
//    //  yield return (Block.MiscellaneousMathematicalSymbolsB, 0x2980, 0x29FF);
//    //  yield return (Block.SupplementalMathematicalOperators, 0x2A00, 0x2AFF);
//    //  yield return (Block.MiscellaneousSymbolsandArrows, 0x2B00, 0x2BFF);
//    //  yield return (Block.Glagolitic, 0x2C00, 0x2C5F);
//    //  yield return (Block.LatinExtendedC, 0x2C60, 0x2C7F);
//    //  yield return (Block.Coptic, 0x2C80, 0x2CFF);
//    //  yield return (Block.GeorgianSupplement, 0x2D00, 0x2D2F);
//    //  yield return (Block.Tifinagh, 0x2D30, 0x2D7F);
//    //  yield return (Block.EthiopicExtended, 0x2D80, 0x2DDF);
//    //  yield return (Block.CyrillicExtendedA, 0x2DE0, 0x2DFF);
//    //  yield return (Block.SupplementalPunctuation, 0x2E00, 0x2E7F);
//    //  yield return (Block.CJKRadicalsSupplement, 0x2E80, 0x2EFF);
//    //  yield return (Block.KangxiRadicals, 0x2F00, 0x2FDF);
//    //  yield return (Block.IdeographicDescriptionCharacters, 0x2FF0, 0x2FFF);
//    //  yield return (Block.CJKSymbolsandPunctuation, 0x3000, 0x303F);
//    //  yield return (Block.Hiragana, 0x3040, 0x309F);
//    //  yield return (Block.Katakana, 0x30A0, 0x30FF);
//    //  yield return (Block.Bopomofo, 0x3100, 0x312F);
//    //  yield return (Block.HangulCompatibilityJamo, 0x3130, 0x318F);
//    //  yield return (Block.Kanbun, 0x3190, 0x319F);
//    //  yield return (Block.BopomofoExtended, 0x31A0, 0x31BF);
//    //  yield return (Block.CJKStrokes, 0x31C0, 0x31EF);
//    //  yield return (Block.KatakanaPhoneticExtensions, 0x31F0, 0x31FF);
//    //  yield return (Block.EnclosedCJKLettersandMonths, 0x3200, 0x32FF);
//    //  yield return (Block.CJKCompatibility, 0x3300, 0x33FF);
//    //  yield return (Block.CJKUnifiedIdeographsExtensionA, 0x3400, 0x4DBF);
//    //  yield return (Block.YijingHexagramSymbols, 0x4DC0, 0x4DFF);
//    //  yield return (Block.CJKUnifiedIdeographs, 0x4E00, 0x9FFF);
//    //  yield return (Block.YiSyllables, 0xA000, 0xA48F);
//    //  yield return (Block.YiRadicals, 0xA490, 0xA4CF);
//    //  yield return (Block.Lisu, 0xA4D0, 0xA4FF);
//    //  yield return (Block.Vai, 0xA500, 0xA63F);
//    //  yield return (Block.CyrillicExtendedB, 0xA640, 0xA69F);
//    //  yield return (Block.Bamum, 0xA6A0, 0xA6FF);
//    //  yield return (Block.ModifierToneLetters, 0xA700, 0xA71F);
//    //  yield return (Block.LatinExtendedD, 0xA720, 0xA7FF);
//    //  yield return (Block.SylotiNagri, 0xA800, 0xA82F);
//    //  yield return (Block.CommonIndicNumberForms, 0xA830, 0xA83F);
//    //  yield return (Block.Phagspa, 0xA840, 0xA87F);
//    //  yield return (Block.Saurashtra, 0xA880, 0xA8DF);
//    //  yield return (Block.DevanagariExtended, 0xA8E0, 0xA8FF);
//    //  yield return (Block.KayahLi, 0xA900, 0xA92F);
//    //  yield return (Block.Rejang, 0xA930, 0xA95F);
//    //  yield return (Block.HangulJamoExtendedA, 0xA960, 0xA97F);
//    //  yield return (Block.Javanese, 0xA980, 0xA9DF);
//    //  yield return (Block.MyanmarExtendedB, 0xA9E0, 0xA9FF);
//    //  yield return (Block.Cham, 0xAA00, 0xAA5F);
//    //  yield return (Block.MyanmarExtendedA, 0xAA60, 0xAA7F);
//    //  yield return (Block.TaiViet, 0xAA80, 0xAADF);
//    //  yield return (Block.MeeteiMayekExtensions, 0xAAE0, 0xAAFF);
//    //  yield return (Block.EthiopicExtendedA, 0xAB00, 0xAB2F);
//    //  yield return (Block.LatinExtendedE, 0xAB30, 0xAB6F);
//    //  yield return (Block.CherokeeSupplement, 0xAB70, 0xABBF);
//    //  yield return (Block.MeeteiMayek, 0xABC0, 0xABFF);
//    //  yield return (Block.HangulSyllables, 0xAC00, 0xD7AF);
//    //  yield return (Block.HangulJamoExtendedB, 0xD7B0, 0xD7FF);
//    //  yield return (Block.HighSurrogates, 0xD800, 0xDB7F);
//    //  yield return (Block.HighPrivateUseSurrogates, 0xDB80, 0xDBFF);
//    //  yield return (Block.LowSurrogates, 0xDC00, 0xDFFF);
//    //  yield return (Block.PrivateUseArea, 0xE000, 0xF8FF);
//    //  yield return (Block.CJKCompatibilityIdeographs, 0xF900, 0xFAFF);
//    //  yield return (Block.AlphabeticPresentationForms, 0xFB00, 0xFB4F);
//    //  yield return (Block.ArabicPresentationFormsA, 0xFB50, 0xFDFF);
//    //  yield return (Block.VariationSelectors, 0xFE00, 0xFE0F);
//    //  yield return (Block.VerticalForms, 0xFE10, 0xFE1F);
//    //  yield return (Block.CombiningHalfMarks, 0xFE20, 0xFE2F);
//    //  yield return (Block.CJKCompatibilityForms, 0xFE30, 0xFE4F);
//    //  yield return (Block.SmallFormVariants, 0xFE50, 0xFE6F);
//    //  yield return (Block.ArabicPresentationFormsB, 0xFE70, 0xFEFF);
//    //  yield return (Block.HalfwidthandFullwidthForms, 0xFF00, 0xFFEF);
//    //  yield return (Block.Specials, 0xFFF0, 0xFFFF);
//    //  yield return (Block.LinearBSyllabary, 0x10000, 0x1007F);
//    //  yield return (Block.LinearBIdeograms, 0x10080, 0x100FF);
//    //  yield return (Block.AegeanNumbers, 0x10100, 0x1013F);
//    //  yield return (Block.AncientGreekNumbers, 0x10140, 0x1018F);
//    //  yield return (Block.AncientSymbols, 0x10190, 0x101CF);
//    //  yield return (Block.PhaistosDisc, 0x101D0, 0x101FF);
//    //  yield return (Block.Lycian, 0x10280, 0x1029F);
//    //  yield return (Block.Carian, 0x102A0, 0x102DF);
//    //  yield return (Block.CopticEpactNumbers, 0x102E0, 0x102FF);
//    //  yield return (Block.OldItalic, 0x10300, 0x1032F);
//    //  yield return (Block.Gothic, 0x10330, 0x1034F);
//    //  yield return (Block.OldPermic, 0x10350, 0x1037F);
//    //  yield return (Block.Ugaritic, 0x10380, 0x1039F);
//    //  yield return (Block.OldPersian, 0x103A0, 0x103DF);
//    //  yield return (Block.Deseret, 0x10400, 0x1044F);
//    //  yield return (Block.Shavian, 0x10450, 0x1047F);
//    //  yield return (Block.Osmanya, 0x10480, 0x104AF);
//    //  yield return (Block.Osage, 0x104B0, 0x104FF);
//    //  yield return (Block.Elbasan, 0x10500, 0x1052F);
//    //  yield return (Block.CaucasianAlbanian, 0x10530, 0x1056F);
//    //  yield return (Block.LinearA, 0x10600, 0x1077F);
//    //  yield return (Block.CypriotSyllabary, 0x10800, 0x1083F);
//    //  yield return (Block.ImperialAramaic, 0x10840, 0x1085F);
//    //  yield return (Block.Palmyrene, 0x10860, 0x1087F);
//    //  yield return (Block.Nabataean, 0x10880, 0x108AF);
//    //  yield return (Block.Hatran, 0x108E0, 0x108FF);
//    //  yield return (Block.Phoenician, 0x10900, 0x1091F);
//    //  yield return (Block.Lydian, 0x10920, 0x1093F);
//    //  yield return (Block.MeroiticHieroglyphs, 0x10980, 0x1099F);
//    //  yield return (Block.MeroiticCursive, 0x109A0, 0x109FF);
//    //  yield return (Block.Kharoshthi, 0x10A00, 0x10A5F);
//    //  yield return (Block.OldSouthArabian, 0x10A60, 0x10A7F);
//    //  yield return (Block.OldNorthArabian, 0x10A80, 0x10A9F);
//    //  yield return (Block.Manichaean, 0x10AC0, 0x10AFF);
//    //  yield return (Block.Avestan, 0x10B00, 0x10B3F);
//    //  yield return (Block.InscriptionalParthian, 0x10B40, 0x10B5F);
//    //  yield return (Block.InscriptionalPahlavi, 0x10B60, 0x10B7F);
//    //  yield return (Block.PsalterPahlavi, 0x10B80, 0x10BAF);
//    //  yield return (Block.OldTurkic, 0x10C00, 0x10C4F);
//    //  yield return (Block.OldHungarian, 0x10C80, 0x10CFF);
//    //  yield return (Block.HanifiRohingya, 0x10D00, 0x10D3F);
//    //  yield return (Block.RumiNumeralSymbols, 0x10E60, 0x10E7F);
//    //  yield return (Block.OldSogdian, 0x10F00, 0x10F2F);
//    //  yield return (Block.Sogdian, 0x10F30, 0x10F6F);
//    //  yield return (Block.Elymaic, 0x10FE0, 0x10FFF);
//    //  yield return (Block.Brahmi, 0x11000, 0x1107F);
//    //  yield return (Block.Kaithi, 0x11080, 0x110CF);
//    //  yield return (Block.SoraSompeng, 0x110D0, 0x110FF);
//    //  yield return (Block.Chakma, 0x11100, 0x1114F);
//    //  yield return (Block.Mahajani, 0x11150, 0x1117F);
//    //  yield return (Block.Sharada, 0x11180, 0x111DF);
//    //  yield return (Block.SinhalaArchaicNumbers, 0x111E0, 0x111FF);
//    //  yield return (Block.Khojki, 0x11200, 0x1124F);
//    //  yield return (Block.Multani, 0x11280, 0x112AF);
//    //  yield return (Block.Khudawadi, 0x112B0, 0x112FF);
//    //  yield return (Block.Grantha, 0x11300, 0x1137F);
//    //  yield return (Block.Newa, 0x11400, 0x1147F);
//    //  yield return (Block.Tirhuta, 0x11480, 0x114DF);
//    //  yield return (Block.Siddham, 0x11580, 0x115FF);
//    //  yield return (Block.Modi, 0x11600, 0x1165F);
//    //  yield return (Block.MongolianSupplement, 0x11660, 0x1167F);
//    //  yield return (Block.Takri, 0x11680, 0x116CF);
//    //  yield return (Block.Ahom, 0x11700, 0x1173F);
//    //  yield return (Block.Dogra, 0x11800, 0x1184F);
//    //  yield return (Block.WarangCiti, 0x118A0, 0x118FF);
//    //  yield return (Block.Nandinagari, 0x119A0, 0x119FF);
//    //  yield return (Block.ZanabazarSquare, 0x11A00, 0x11A4F);
//    //  yield return (Block.Soyombo, 0x11A50, 0x11AAF);
//    //  yield return (Block.PauCinHau, 0x11AC0, 0x11AFF);
//    //  yield return (Block.Bhaiksuki, 0x11C00, 0x11C6F);
//    //  yield return (Block.Marchen, 0x11C70, 0x11CBF);
//    //  yield return (Block.MasaramGondi, 0x11D00, 0x11D5F);
//    //  yield return (Block.GunjalaGondi, 0x11D60, 0x11DAF);
//    //  yield return (Block.Makasar, 0x11EE0, 0x11EFF);
//    //  yield return (Block.TamilSupplement, 0x11FC0, 0x11FFF);
//    //  yield return (Block.Cuneiform, 0x12000, 0x123FF);
//    //  yield return (Block.CuneiformNumbersandPunctuation, 0x12400, 0x1247F);
//    //  yield return (Block.EarlyDynasticCuneiform, 0x12480, 0x1254F);
//    //  yield return (Block.EgyptianHieroglyphs, 0x13000, 0x1342F);
//    //  yield return (Block.EgyptianHieroglyphFormatControls, 0x13430, 0x1343F);
//    //  yield return (Block.AnatolianHieroglyphs, 0x14400, 0x1467F);
//    //  yield return (Block.BamumSupplement, 0x16800, 0x16A3F);
//    //  yield return (Block.Mro, 0x16A40, 0x16A6F);
//    //  yield return (Block.BassaVah, 0x16AD0, 0x16AFF);
//    //  yield return (Block.PahawhHmong, 0x16B00, 0x16B8F);
//    //  yield return (Block.Medefaidrin, 0x16E40, 0x16E9F);
//    //  yield return (Block.Miao, 0x16F00, 0x16F9F);
//    //  yield return (Block.IdeographicSymbolsandPunctuation, 0x16FE0, 0x16FFF);
//    //  yield return (Block.Tangut, 0x17000, 0x187FF);
//    //  yield return (Block.TangutComponents, 0x18800, 0x18AFF);
//    //  yield return (Block.KanaSupplement, 0x1B000, 0x1B0FF);
//    //  yield return (Block.KanaExtendedA, 0x1B100, 0x1B12F);
//    //  yield return (Block.SmallKanaExtension, 0x1B130, 0x1B16F);
//    //  yield return (Block.Nushu, 0x1B170, 0x1B2FF);
//    //  yield return (Block.Duployan, 0x1BC00, 0x1BC9F);
//    //  yield return (Block.ShorthandFormatControls, 0x1BCA0, 0x1BCAF);
//    //  yield return (Block.ByzantineMusicalSymbols, 0x1D000, 0x1D0FF);
//    //  yield return (Block.MusicalSymbols, 0x1D100, 0x1D1FF);
//    //  yield return (Block.AncientGreekMusicalNotation, 0x1D200, 0x1D24F);
//    //  yield return (Block.MayanNumerals, 0x1D2E0, 0x1D2FF);
//    //  yield return (Block.TaiXuanJingSymbols, 0x1D300, 0x1D35F);
//    //  yield return (Block.CountingRodNumerals, 0x1D360, 0x1D37F);
//    //  yield return (Block.MathematicalAlphanumericSymbols, 0x1D400, 0x1D7FF);
//    //  yield return (Block.SuttonSignWriting, 0x1D800, 0x1DAAF);
//    //  yield return (Block.GlagoliticSupplement, 0x1E000, 0x1E02F);
//    //  yield return (Block.NyiakengPuachueHmong, 0x1E100, 0x1E14F);
//    //  yield return (Block.Wancho, 0x1E2C0, 0x1E2FF);
//    //  yield return (Block.MendeKikakui, 0x1E800, 0x1E8DF);
//    //  yield return (Block.Adlam, 0x1E900, 0x1E95F);
//    //  yield return (Block.IndicSiyaqNumbers, 0x1EC70, 0x1ECBF);
//    //  yield return (Block.OttomanSiyaqNumbers, 0x1ED00, 0x1ED4F);
//    //  yield return (Block.ArabicMathematicalAlphabeticSymbols, 0x1EE00, 0x1EEFF);
//    //  yield return (Block.MahjongTiles, 0x1F000, 0x1F02F);
//    //  yield return (Block.DominoTiles, 0x1F030, 0x1F09F);
//    //  yield return (Block.PlayingCards, 0x1F0A0, 0x1F0FF);
//    //  yield return (Block.EnclosedAlphanumericSupplement, 0x1F100, 0x1F1FF);
//    //  yield return (Block.EnclosedIdeographicSupplement, 0x1F200, 0x1F2FF);
//    //  yield return (Block.MiscellaneousSymbolsandPictographs, 0x1F300, 0x1F5FF);
//    //  yield return (Block.Emoticons, 0x1F600, 0x1F64F);
//    //  yield return (Block.OrnamentalDingbats, 0x1F650, 0x1F67F);
//    //  yield return (Block.TransportandMapSymbols, 0x1F680, 0x1F6FF);
//    //  yield return (Block.AlchemicalSymbols, 0x1F700, 0x1F77F);
//    //  yield return (Block.GeometricShapesExtended, 0x1F780, 0x1F7FF);
//    //  yield return (Block.SupplementalArrowsC, 0x1F800, 0x1F8FF);
//    //  yield return (Block.SupplementalSymbolsandPictographs, 0x1F900, 0x1F9FF);
//    //  yield return (Block.ChessSymbols, 0x1FA00, 0x1FA6F);
//    //  yield return (Block.SymbolsandPictographsExtendedA, 0x1FA70, 0x1FAFF);
//    //  yield return (Block.CJKUnifiedIdeographsExtensionB, 0x20000, 0x2A6DF);
//    //  yield return (Block.CJKUnifiedIdeographsExtensionC, 0x2A700, 0x2B73F);
//    //  yield return (Block.CJKUnifiedIdeographsExtensionD, 0x2B740, 0x2B81F);
//    //  yield return (Block.CJKUnifiedIdeographsExtensionE, 0x2B820, 0x2CEAF);
//    //  yield return (Block.CJKUnifiedIdeographsExtensionF, 0x2CEB0, 0x2EBEF);
//    //  yield return (Block.CJKCompatibilityIdeographsSupplement, 0x2F800, 0x2FA1F);
//    //  yield return (Block.Tags, 0xE0000, 0xE007F);
//    //  yield return (Block.VariationSelectorsSupplement, 0xE0100, 0xE01EF);
//    //  yield return (Block.SupplementaryPrivateUseAreaA, 0xF0000, 0xFFFFF);
//    //  yield return (Block.SupplementaryPrivateUseAreaB, 0x100000, 0x10FFFF);
//    //}
//    //#endregion Unicode Blocks

//    public static System.Collections.Generic.IDictionary<System.Globalization.UnicodeCategory, System.Collections.Generic.List<char>> GetUnicodeCategoryCharacters()
//    {
//      var unicodeCategoryCharacters = new System.Collections.Generic.Dictionary<System.Globalization.UnicodeCategory, System.Collections.Generic.List<char>>();

//      foreach (var unicodeCategoryValue in System.Enum.GetValues(typeof(System.Globalization.UnicodeCategory)).Cast<System.Globalization.UnicodeCategory>())
//      {
//        unicodeCategoryCharacters.Add(unicodeCategoryValue, new System.Collections.Generic.List<char>());
//      }

//      var charValue = char.MinValue;

//      while (true)
//      {
//        unicodeCategoryCharacters[System.Globalization.CharUnicodeInfo.GetUnicodeCategory(charValue)].Add(charValue);

//        if (charValue++ == char.MaxValue) break;
//      }

//      return unicodeCategoryCharacters;
//    }

//    /// <summary>This is an aggregate derivation of the System.Globalization.UnicodeCategory (or MajorMinorCode) enum value.</summary>
//    /// <example>var allCharactersByCategoryMajorLabel = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
//    public enum CategoryMajor
//    {
//      Letter = 'L',
//      Mark = 'M',
//      Number = 'N',
//      Other = 'C',
//      Punctuation = 'P',
//      Separator = 'Z',
//      Symbol = 'S'
//    }

//    /// <summary>Translates a System.Globalization.UnicodeCategory enum value into a MajorLabel enum value.</summary>
//    /// <example>var allCharactersByCategoryMajorLabel = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
//    public static CategoryMajor ToCategoryMajor(this System.Globalization.UnicodeCategory unicodeCategory)
//    {
//      switch (unicodeCategory)
//      {
//        case System.Globalization.UnicodeCategory.LowercaseLetter:
//        case System.Globalization.UnicodeCategory.ModifierLetter:
//        case System.Globalization.UnicodeCategory.OtherLetter:
//        case System.Globalization.UnicodeCategory.TitlecaseLetter:
//        case System.Globalization.UnicodeCategory.UppercaseLetter:
//          return CategoryMajor.Letter;
//        case System.Globalization.UnicodeCategory.SpacingCombiningMark:
//        case System.Globalization.UnicodeCategory.EnclosingMark:
//        case System.Globalization.UnicodeCategory.NonSpacingMark:
//          return CategoryMajor.Mark;
//        case System.Globalization.UnicodeCategory.DecimalDigitNumber:
//        case System.Globalization.UnicodeCategory.LetterNumber:
//        case System.Globalization.UnicodeCategory.OtherNumber:
//          return CategoryMajor.Number;
//        case System.Globalization.UnicodeCategory.Control:
//        case System.Globalization.UnicodeCategory.Format:
//        case System.Globalization.UnicodeCategory.OtherNotAssigned:
//        case System.Globalization.UnicodeCategory.PrivateUse:
//        case System.Globalization.UnicodeCategory.Surrogate:
//          return CategoryMajor.Other;
//        case System.Globalization.UnicodeCategory.ConnectorPunctuation:
//        case System.Globalization.UnicodeCategory.DashPunctuation:
//        case System.Globalization.UnicodeCategory.ClosePunctuation:
//        case System.Globalization.UnicodeCategory.FinalQuotePunctuation:
//        case System.Globalization.UnicodeCategory.InitialQuotePunctuation:
//        case System.Globalization.UnicodeCategory.OtherPunctuation:
//        case System.Globalization.UnicodeCategory.OpenPunctuation:
//          return CategoryMajor.Punctuation;
//        case System.Globalization.UnicodeCategory.LineSeparator:
//        case System.Globalization.UnicodeCategory.ParagraphSeparator:
//        case System.Globalization.UnicodeCategory.SpaceSeparator:
//          return CategoryMajor.Separator;
//        case System.Globalization.UnicodeCategory.CurrencySymbol:
//        case System.Globalization.UnicodeCategory.ModifierSymbol:
//        case System.Globalization.UnicodeCategory.MathSymbol:
//        case System.Globalization.UnicodeCategory.OtherSymbol:
//          return CategoryMajor.Symbol;
//        default:
//          throw new System.ArgumentOutOfRangeException(nameof(unicodeCategory));
//      }
//    }

//    public static CategoryMajor ParseCategoryMajor(string categoryMajor)
//      => !(categoryMajor is null) ? (CategoryMajor)System.Enum.Parse(typeof(CategoryCode), categoryMajor) : throw new System.ArgumentOutOfRangeException(nameof(categoryMajor));
//    public static bool TryParseCategoryMajor(string categoryMajor, out CategoryMajor result)
//    {
//      try
//      {
//        result = ParseCategoryMajor(categoryMajor);
//        return true;
//      }
//#pragma warning disable CA1031 // Do not catch general exception types.
//      catch
//#pragma warning restore CA1031 // Do not catch general exception types.
//      { }

//      result = default;
//      return false;
//    }

//    /// <summary>This is a directly correlated enum of System.Globalization.UnicodeCategory to ease translation to the abbreviated two character code for the major and minor parts of the System.Globalization.UnicodeCategory enum values.</summary>
//    /// <example>var allCharactersByCategoryMajorMinorCode = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorMinorCode()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
//    public enum CategoryCode
//    {
//      Cc = System.Globalization.UnicodeCategory.Control,
//      Cf = System.Globalization.UnicodeCategory.Format,
//      Cn = System.Globalization.UnicodeCategory.OtherNotAssigned,
//      Co = System.Globalization.UnicodeCategory.PrivateUse,
//      Cs = System.Globalization.UnicodeCategory.Surrogate,

//      Ll = System.Globalization.UnicodeCategory.LowercaseLetter,
//      Lm = System.Globalization.UnicodeCategory.ModifierLetter,
//      Lo = System.Globalization.UnicodeCategory.OtherLetter,
//      Lt = System.Globalization.UnicodeCategory.TitlecaseLetter,
//      Lu = System.Globalization.UnicodeCategory.UppercaseLetter,

//      Mc = System.Globalization.UnicodeCategory.SpacingCombiningMark,
//      Me = System.Globalization.UnicodeCategory.EnclosingMark,
//      Mn = System.Globalization.UnicodeCategory.NonSpacingMark,

//      Nd = System.Globalization.UnicodeCategory.DecimalDigitNumber,
//      Nl = System.Globalization.UnicodeCategory.LetterNumber,
//      No = System.Globalization.UnicodeCategory.OtherNumber,

//      Pc = System.Globalization.UnicodeCategory.ConnectorPunctuation,
//      Pd = System.Globalization.UnicodeCategory.DashPunctuation,
//      Pe = System.Globalization.UnicodeCategory.ClosePunctuation,
//      Pf = System.Globalization.UnicodeCategory.FinalQuotePunctuation,
//      Pi = System.Globalization.UnicodeCategory.InitialQuotePunctuation,
//      Po = System.Globalization.UnicodeCategory.OtherPunctuation,
//      Ps = System.Globalization.UnicodeCategory.OpenPunctuation,

//      Sc = System.Globalization.UnicodeCategory.CurrencySymbol,
//      Sk = System.Globalization.UnicodeCategory.ModifierSymbol,
//      Sm = System.Globalization.UnicodeCategory.MathSymbol,
//      So = System.Globalization.UnicodeCategory.OtherSymbol,

//      Zl = System.Globalization.UnicodeCategory.LineSeparator,
//      Zp = System.Globalization.UnicodeCategory.ParagraphSeparator,
//      Zs = System.Globalization.UnicodeCategory.SpaceSeparator,
//    }

//    /// <summary>Translates a System.Globalization.UnicodeCategory enum value into a MajorMinorCode enum value.</summary>
//    /// <example>var allCharactersByCategoryMajorMinorCode = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorMinorCode()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
//    public static CategoryCode ToCategoryCode(this System.Globalization.UnicodeCategory unicodeCategory)
//      => (CategoryCode)unicodeCategory;
//    /// <summary>Translates a MajorMinorCode enum value into a System.Globalization.UnicodeCategory enum value.</summary>
//    public static System.Globalization.UnicodeCategory ToUnicodeCategory(this CategoryCode categoryCode)
//      => (System.Globalization.UnicodeCategory)categoryCode;

//    public static System.Globalization.UnicodeCategory ParseCategoryCode(string categoryCode)
//      => (!(categoryCode is null) && categoryCode.Length == 2) ? ((CategoryCode)System.Enum.Parse(typeof(CategoryCode), categoryCode)).ToUnicodeCategory() : throw new System.ArgumentOutOfRangeException(nameof(categoryCode));
//    public static bool TryParseCategoryCode(string categoryCode, out System.Globalization.UnicodeCategory result)
//    {
//      try
//      {
//        result = ParseCategoryCode(categoryCode);
//        return true;
//      }
//#pragma warning disable CA1031 // Do not catch general exception types.
//      catch
//#pragma warning restore CA1031 // Do not catch general exception types.
//      { }

//      result = default;
//      return false;
//    }

//    //public static bool IsLetter(this System.Globalization.UnicodeCategory unicodeCategory)
//    //{
//    //  switch (unicodeCategory)
//    //  {
//    //    case System.Globalization.UnicodeCategory.LowercaseLetter:
//    //    case System.Globalization.UnicodeCategory.ModifierLetter:
//    //    case System.Globalization.UnicodeCategory.OtherLetter:
//    //    case System.Globalization.UnicodeCategory.TitlecaseLetter:
//    //    case System.Globalization.UnicodeCategory.UppercaseLetter:
//    //      return true;
//    //    default:
//    //      return false;
//    //  }
//    //}
//    //public static bool IsMark(this System.Globalization.UnicodeCategory unicodeCategory)
//    //{
//    //  switch (unicodeCategory)
//    //  {
//    //    case System.Globalization.UnicodeCategory.SpacingCombiningMark:
//    //    case System.Globalization.UnicodeCategory.EnclosingMark:
//    //    case System.Globalization.UnicodeCategory.NonSpacingMark:
//    //      return true;
//    //    default:
//    //      return false;
//    //  }
//    //}
//    //public static bool IsNumber(this System.Globalization.UnicodeCategory unicodeCategory)
//    //{
//    //  switch (unicodeCategory)
//    //  {
//    //    case System.Globalization.UnicodeCategory.DecimalDigitNumber:
//    //    case System.Globalization.UnicodeCategory.LetterNumber:
//    //    case System.Globalization.UnicodeCategory.OtherNumber:
//    //      return true;
//    //    default:
//    //      return false;
//    //  }
//    //}
//    //public static bool IsOther(this System.Globalization.UnicodeCategory unicodeCategory)
//    //{
//    //  switch (unicodeCategory)
//    //  {
//    //    case System.Globalization.UnicodeCategory.Control:
//    //    case System.Globalization.UnicodeCategory.Format:
//    //    case System.Globalization.UnicodeCategory.OtherNotAssigned:
//    //    case System.Globalization.UnicodeCategory.PrivateUse:
//    //    case System.Globalization.UnicodeCategory.Surrogate:
//    //      return true;
//    //    default:
//    //      return false;
//    //  }
//    //}
//    //public static bool IsPunctuation(this System.Globalization.UnicodeCategory unicodeCategory)
//    //{
//    //  switch (unicodeCategory)
//    //  {
//    //    case System.Globalization.UnicodeCategory.ConnectorPunctuation:
//    //    case System.Globalization.UnicodeCategory.DashPunctuation:
//    //    case System.Globalization.UnicodeCategory.ClosePunctuation:
//    //    case System.Globalization.UnicodeCategory.FinalQuotePunctuation:
//    //    case System.Globalization.UnicodeCategory.InitialQuotePunctuation:
//    //    case System.Globalization.UnicodeCategory.OtherPunctuation:
//    //    case System.Globalization.UnicodeCategory.OpenPunctuation:
//    //      return true;
//    //    default:
//    //      return false;
//    //  }
//    //}
//    //public static bool IsSeparator(this System.Globalization.UnicodeCategory unicodeCategory)
//    //{
//    //  switch (unicodeCategory)
//    //  {
//    //    case System.Globalization.UnicodeCategory.LineSeparator:
//    //    case System.Globalization.UnicodeCategory.ParagraphSeparator:
//    //    case System.Globalization.UnicodeCategory.SpaceSeparator:
//    //      return true;
//    //    default:
//    //      return false;
//    //  }
//    //}
//    //public static bool IsSymbol(this System.Globalization.UnicodeCategory unicodeCategory)
//    //{
//    //  switch (unicodeCategory)
//    //  {
//    //    case System.Globalization.UnicodeCategory.CurrencySymbol:
//    //    case System.Globalization.UnicodeCategory.ModifierSymbol:
//    //    case System.Globalization.UnicodeCategory.MathSymbol:
//    //    case System.Globalization.UnicodeCategory.OtherSymbol:
//    //      return true;
//    //    default:
//    //      return false;
//    //  }
//    //}

//    /// <summary>Convert the Unicode codepoint to the string representation formats of "\uxxxx" (four hex characters) or "\UXXXXXXXX" (eight hex characters).</summary>
//    public static string ToStringLiteral(int codePoint) => codePoint < 0 ? throw new System.ArgumentOutOfRangeException(nameof(codePoint)) : codePoint <= 0xFFFF ? @"\u" + codePoint.ToString(@"x4", System.Globalization.CultureInfo.InvariantCulture) : @"\U" + codePoint.ToString(@"X8", System.Globalization.CultureInfo.InvariantCulture);

//    //public static int ParseUnicodeNotation(string text) => System.Text.RegularExpressions.Regex.Match(text, @"(?<=U\+)[0-9A-Fa-f]{4,}") is var m && m.Success && int.TryParse(m.Value, System.Globalization.NumberStyles.HexNumber, null, out var number) ? number : throw new System.ArgumentException(@"Could not parse unicode notation.");
//    //public static bool TryParseUnicodeNotation(string text, out int result)
//    //{
//    //  try
//    //  {
//    //    result = ParseUnicodeNotation(text);
//    //    return true;
//    //  }
//    //  catch { }

//    //  result = default;
//    //  return false;
//    //}

//    /// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
//    public static string ToUnicodeNotation(int codePoint) => @"U+" + codePoint.ToString(@"X4", System.Globalization.CultureInfo.InvariantCulture);

//    //public static System.Collections.Generic.IEnumerable<string[]> ParseData(System.Collections.Generic.IEnumerable<string> data, System.Func<string[], bool> predicate) => data.Select(s => s.Split(';')).Where(predicate);

//    //public static class Ucd
//    //{
//    //  public const string LocationLocal = @"file://\Flux\Resources\Ucd\";
//    //  public const string LocationRemote = @"https://www.unicode.org/Public/UCD/latest/ucd/";

//    //  public const string UnicodeDataTxt = "UnicodeData.txt";

//    //  /// <summary>Returns a sequence with lines (rows) from the specified database file at the specified location (local or from the offical unicode.org site).</summary>
//    //  public static System.Collections.Generic.IEnumerable<string> GetLines(string uri = LocationLocal + UnicodeDataTxt)
//    //    => new System.Uri(uri).ReadLines(System.Text.Encoding.UTF8);

//    //  /// <summary>Enumerates all lines as arrays of the specified database text file from the offical unicode.org site.</summary>
//    //  public static Flux.Data.EnumerableDataReader<string[]> GetDataReader(string uri = LocationLocal + UnicodeDataTxt)
//    //    => new Flux.Data.EnumerableDataReader<string[]>(GetLines(uri).Select(s => s.Split(';')), dr => dr) { FieldNames = new string[] { "CodePoint", "Name", "GeneralCategory", "CanonicalCombiningClass", "BidiClass", "DecompositionTypeMapping", "NumericType6", "NumericType7", "NumericType8", "BidiMirrored", "Unicode1Name", "IsoComment", "SimpleUppercaseMapping", "SimpleLowercaseMapping", "SimpleTitlecaseMapping" }, FieldTypes = System.Linq.Enumerable.Range(0, 15).Select(i => typeof(string)).ToList() };

//    //  public static System.Collections.Generic.IEnumerable<string[]> GetRecords(string uri = LocationLocal + UnicodeDataTxt)
//    //    => GetLines(uri).Select(s => s.Split(';'));
//    //}
//  }
//}
