using System.Linq;

namespace Flux
{
  public static class XtendRune
  {
    #region Unicode Blocks
    public enum Block
    {
      #region All Unicode Blocks
      BasicLatin,
      Latin1Supplement,
      LatinExtendedA,
      LatinExtendedB,
      IPAExtensions,
      SpacingModifierLetters,
      CombiningDiacriticalMarks,
      GreekandCoptic,
      Cyrillic,
      CyrillicSupplement,
      Armenian,
      Hebrew,
      Arabic,
      Syriac,
      ArabicSupplement,
      Thaana,
      NKo,
      Samaritan,
      Mandaic,
      SyriacSupplement,
      ArabicExtendedA,
      Devanagari,
      Bengali,
      Gurmukhi,
      Gujarati,
      Oriya,
      Tamil,
      Telugu,
      Kannada,
      Malayalam,
      Sinhala,
      Thai,
      Lao,
      Tibetan,
      Myanmar,
      Georgian,
      HangulJamo,
      Ethiopic,
      EthiopicSupplement,
      Cherokee,
      UnifiedCanadianAboriginalSyllabics,
      Ogham,
      Runic,
      Tagalog,
      Hanunoo,
      Buhid,
      Tagbanwa,
      Khmer,
      Mongolian,
      UnifiedCanadianAboriginalSyllabicsExtended,
      Limbu,
      TaiLe,
      NewTaiLue,
      KhmerSymbols,
      Buginese,
      TaiTham,
      CombiningDiacriticalMarksExtended,
      Balinese,
      Sundanese,
      Batak,
      Lepcha,
      OlChiki,
      CyrillicExtendedC,
      GeorgianExtended,
      SundaneseSupplement,
      VedicExtensions,
      PhoneticExtensions,
      PhoneticExtensionsSupplement,
      CombiningDiacriticalMarksSupplement,
      LatinExtendedAdditional,
      GreekExtended,
      GeneralPunctuation,
      SuperscriptsandSubscripts,
      CurrencySymbols,
      CombiningDiacriticalMarksforSymbols,
      LetterlikeSymbols,
      NumberForms,
      Arrows,
      MathematicalOperators,
      MiscellaneousTechnical,
      ControlPictures,
      OpticalCharacterRecognition,
      EnclosedAlphanumerics,
      BoxDrawing,
      BlockElements,
      GeometricShapes,
      MiscellaneousSymbols,
      Dingbats,
      MiscellaneousMathematicalSymbolsA,
      SupplementalArrowsA,
      BraillePatterns,
      SupplementalArrowsB,
      MiscellaneousMathematicalSymbolsB,
      SupplementalMathematicalOperators,
      MiscellaneousSymbolsandArrows,
      Glagolitic,
      LatinExtendedC,
      Coptic,
      GeorgianSupplement,
      Tifinagh,
      EthiopicExtended,
      CyrillicExtendedA,
      SupplementalPunctuation,
      CJKRadicalsSupplement,
      KangxiRadicals,
      IdeographicDescriptionCharacters,
      CJKSymbolsandPunctuation,
      Hiragana,
      Katakana,
      Bopomofo,
      HangulCompatibilityJamo,
      Kanbun,
      BopomofoExtended,
      CJKStrokes,
      KatakanaPhoneticExtensions,
      EnclosedCJKLettersandMonths,
      CJKCompatibility,
      CJKUnifiedIdeographsExtensionA,
      YijingHexagramSymbols,
      CJKUnifiedIdeographs,
      YiSyllables,
      YiRadicals,
      Lisu,
      Vai,
      CyrillicExtendedB,
      Bamum,
      ModifierToneLetters,
      LatinExtendedD,
      SylotiNagri,
      CommonIndicNumberForms,
      Phagspa,
      Saurashtra,
      DevanagariExtended,
      KayahLi,
      Rejang,
      HangulJamoExtendedA,
      Javanese,
      MyanmarExtendedB,
      Cham,
      MyanmarExtendedA,
      TaiViet,
      MeeteiMayekExtensions,
      EthiopicExtendedA,
      LatinExtendedE,
      CherokeeSupplement,
      MeeteiMayek,
      HangulSyllables,
      HangulJamoExtendedB,
      HighSurrogates,
      HighPrivateUseSurrogates,
      LowSurrogates,
      PrivateUseArea,
      CJKCompatibilityIdeographs,
      AlphabeticPresentationForms,
      ArabicPresentationFormsA,
      VariationSelectors,
      VerticalForms,
      CombiningHalfMarks,
      CJKCompatibilityForms,
      SmallFormVariants,
      ArabicPresentationFormsB,
      HalfwidthandFullwidthForms,
      Specials,
      LinearBSyllabary,
      LinearBIdeograms,
      AegeanNumbers,
      AncientGreekNumbers,
      AncientSymbols,
      PhaistosDisc,
      Lycian,
      Carian,
      CopticEpactNumbers,
      OldItalic,
      Gothic,
      OldPermic,
      Ugaritic,
      OldPersian,
      Deseret,
      Shavian,
      Osmanya,
      Osage,
      Elbasan,
      CaucasianAlbanian,
      LinearA,
      CypriotSyllabary,
      ImperialAramaic,
      Palmyrene,
      Nabataean,
      Hatran,
      Phoenician,
      Lydian,
      MeroiticHieroglyphs,
      MeroiticCursive,
      Kharoshthi,
      OldSouthArabian,
      OldNorthArabian,
      Manichaean,
      Avestan,
      InscriptionalParthian,
      InscriptionalPahlavi,
      PsalterPahlavi,
      OldTurkic,
      OldHungarian,
      HanifiRohingya,
      RumiNumeralSymbols,
      OldSogdian,
      Sogdian,
      Elymaic,
      Brahmi,
      Kaithi,
      SoraSompeng,
      Chakma,
      Mahajani,
      Sharada,
      SinhalaArchaicNumbers,
      Khojki,
      Multani,
      Khudawadi,
      Grantha,
      Newa,
      Tirhuta,
      Siddham,
      Modi,
      MongolianSupplement,
      Takri,
      Ahom,
      Dogra,
      WarangCiti,
      Nandinagari,
      ZanabazarSquare,
      Soyombo,
      PauCinHau,
      Bhaiksuki,
      Marchen,
      MasaramGondi,
      GunjalaGondi,
      Makasar,
      TamilSupplement,
      Cuneiform,
      CuneiformNumbersandPunctuation,
      EarlyDynasticCuneiform,
      EgyptianHieroglyphs,
      EgyptianHieroglyphFormatControls,
      AnatolianHieroglyphs,
      BamumSupplement,
      Mro,
      BassaVah,
      PahawhHmong,
      Medefaidrin,
      Miao,
      IdeographicSymbolsandPunctuation,
      Tangut,
      TangutComponents,
      KanaSupplement,
      KanaExtendedA,
      SmallKanaExtension,
      Nushu,
      Duployan,
      ShorthandFormatControls,
      ByzantineMusicalSymbols,
      MusicalSymbols,
      AncientGreekMusicalNotation,
      MayanNumerals,
      TaiXuanJingSymbols,
      CountingRodNumerals,
      MathematicalAlphanumericSymbols,
      SuttonSignWriting,
      GlagoliticSupplement,
      NyiakengPuachueHmong,
      Wancho,
      MendeKikakui,
      Adlam,
      IndicSiyaqNumbers,
      OttomanSiyaqNumbers,
      ArabicMathematicalAlphabeticSymbols,
      MahjongTiles,
      DominoTiles,
      PlayingCards,
      EnclosedAlphanumericSupplement,
      EnclosedIdeographicSupplement,
      MiscellaneousSymbolsandPictographs,
      Emoticons,
      OrnamentalDingbats,
      TransportandMapSymbols,
      AlchemicalSymbols,
      GeometricShapesExtended,
      SupplementalArrowsC,
      SupplementalSymbolsandPictographs,
      ChessSymbols,
      SymbolsandPictographsExtendedA,
      CJKUnifiedIdeographsExtensionB,
      CJKUnifiedIdeographsExtensionC,
      CJKUnifiedIdeographsExtensionD,
      CJKUnifiedIdeographsExtensionE,
      CJKUnifiedIdeographsExtensionF,
      CJKCompatibilityIdeographsSupplement,
      Tags,
      VariationSelectorsSupplement,
      SupplementaryPrivateUseAreaA,
      SupplementaryPrivateUseAreaB,

      Unknown = int.MinValue
      #endregion All Unicode Blocks
    }

    public static System.Collections.Generic.IEnumerable<(Block block, int firstCodeUnit, int lastCodeUnit)> GetBlockRanges()
    {
      yield return (Block.BasicLatin, 0x00, 0x7F);
      yield return (Block.Latin1Supplement, 0x80, 0xFF);
      yield return (Block.LatinExtendedA, 0x100, 0x17F);
      yield return (Block.LatinExtendedB, 0x180, 0x24F);
      yield return (Block.IPAExtensions, 0x250, 0x2AF);
      yield return (Block.SpacingModifierLetters, 0x2B0, 0x2FF);
      yield return (Block.CombiningDiacriticalMarks, 0x300, 0x36F);
      yield return (Block.GreekandCoptic, 0x370, 0x3FF);
      yield return (Block.Cyrillic, 0x400, 0x4FF);
      yield return (Block.CyrillicSupplement, 0x500, 0x52F);
      yield return (Block.Armenian, 0x530, 0x58F);
      yield return (Block.Hebrew, 0x590, 0x5FF);
      yield return (Block.Arabic, 0x600, 0x6FF);
      yield return (Block.Syriac, 0x700, 0x74F);
      yield return (Block.ArabicSupplement, 0x750, 0x77F);
      yield return (Block.Thaana, 0x780, 0x7BF);
      yield return (Block.NKo, 0x7C0, 0x7FF);
      yield return (Block.Samaritan, 0x800, 0x83F);
      yield return (Block.Mandaic, 0x840, 0x85F);
      yield return (Block.SyriacSupplement, 0x860, 0x86F);
      yield return (Block.ArabicExtendedA, 0x8A0, 0x8FF);
      yield return (Block.Devanagari, 0x900, 0x97F);
      yield return (Block.Bengali, 0x980, 0x9FF);
      yield return (Block.Gurmukhi, 0xA00, 0xA7F);
      yield return (Block.Gujarati, 0xA80, 0xAFF);
      yield return (Block.Oriya, 0xB00, 0xB7F);
      yield return (Block.Tamil, 0xB80, 0xBFF);
      yield return (Block.Telugu, 0xC00, 0xC7F);
      yield return (Block.Kannada, 0xC80, 0xCFF);
      yield return (Block.Malayalam, 0xD00, 0xD7F);
      yield return (Block.Sinhala, 0xD80, 0xDFF);
      yield return (Block.Thai, 0xE00, 0xE7F);
      yield return (Block.Lao, 0xE80, 0xEFF);
      yield return (Block.Tibetan, 0xF00, 0xFFF);
      yield return (Block.Myanmar, 0x1000, 0x109F);
      yield return (Block.Georgian, 0x10A0, 0x10FF);
      yield return (Block.HangulJamo, 0x1100, 0x11FF);
      yield return (Block.Ethiopic, 0x1200, 0x137F);
      yield return (Block.EthiopicSupplement, 0x1380, 0x139F);
      yield return (Block.Cherokee, 0x13A0, 0x13FF);
      yield return (Block.UnifiedCanadianAboriginalSyllabics, 0x1400, 0x167F);
      yield return (Block.Ogham, 0x1680, 0x169F);
      yield return (Block.Runic, 0x16A0, 0x16FF);
      yield return (Block.Tagalog, 0x1700, 0x171F);
      yield return (Block.Hanunoo, 0x1720, 0x173F);
      yield return (Block.Buhid, 0x1740, 0x175F);
      yield return (Block.Tagbanwa, 0x1760, 0x177F);
      yield return (Block.Khmer, 0x1780, 0x17FF);
      yield return (Block.Mongolian, 0x1800, 0x18AF);
      yield return (Block.UnifiedCanadianAboriginalSyllabicsExtended, 0x18B0, 0x18FF);
      yield return (Block.Limbu, 0x1900, 0x194F);
      yield return (Block.TaiLe, 0x1950, 0x197F);
      yield return (Block.NewTaiLue, 0x1980, 0x19DF);
      yield return (Block.KhmerSymbols, 0x19E0, 0x19FF);
      yield return (Block.Buginese, 0x1A00, 0x1A1F);
      yield return (Block.TaiTham, 0x1A20, 0x1AAF);
      yield return (Block.CombiningDiacriticalMarksExtended, 0x1AB0, 0x1AFF);
      yield return (Block.Balinese, 0x1B00, 0x1B7F);
      yield return (Block.Sundanese, 0x1B80, 0x1BBF);
      yield return (Block.Batak, 0x1BC0, 0x1BFF);
      yield return (Block.Lepcha, 0x1C00, 0x1C4F);
      yield return (Block.OlChiki, 0x1C50, 0x1C7F);
      yield return (Block.CyrillicExtendedC, 0x1C80, 0x1C8F);
      yield return (Block.GeorgianExtended, 0x1C90, 0x1CBF);
      yield return (Block.SundaneseSupplement, 0x1CC0, 0x1CCF);
      yield return (Block.VedicExtensions, 0x1CD0, 0x1CFF);
      yield return (Block.PhoneticExtensions, 0x1D00, 0x1D7F);
      yield return (Block.PhoneticExtensionsSupplement, 0x1D80, 0x1DBF);
      yield return (Block.CombiningDiacriticalMarksSupplement, 0x1DC0, 0x1DFF);
      yield return (Block.LatinExtendedAdditional, 0x1E00, 0x1EFF);
      yield return (Block.GreekExtended, 0x1F00, 0x1FFF);
      yield return (Block.GeneralPunctuation, 0x2000, 0x206F);
      yield return (Block.SuperscriptsandSubscripts, 0x2070, 0x209F);
      yield return (Block.CurrencySymbols, 0x20A0, 0x20CF);
      yield return (Block.CombiningDiacriticalMarksforSymbols, 0x20D0, 0x20FF);
      yield return (Block.LetterlikeSymbols, 0x2100, 0x214F);
      yield return (Block.NumberForms, 0x2150, 0x218F);
      yield return (Block.Arrows, 0x2190, 0x21FF);
      yield return (Block.MathematicalOperators, 0x2200, 0x22FF);
      yield return (Block.MiscellaneousTechnical, 0x2300, 0x23FF);
      yield return (Block.ControlPictures, 0x2400, 0x243F);
      yield return (Block.OpticalCharacterRecognition, 0x2440, 0x245F);
      yield return (Block.EnclosedAlphanumerics, 0x2460, 0x24FF);
      yield return (Block.BoxDrawing, 0x2500, 0x257F);
      yield return (Block.BlockElements, 0x2580, 0x259F);
      yield return (Block.GeometricShapes, 0x25A0, 0x25FF);
      yield return (Block.MiscellaneousSymbols, 0x2600, 0x26FF);
      yield return (Block.Dingbats, 0x2700, 0x27BF);
      yield return (Block.MiscellaneousMathematicalSymbolsA, 0x27C0, 0x27EF);
      yield return (Block.SupplementalArrowsA, 0x27F0, 0x27FF);
      yield return (Block.BraillePatterns, 0x2800, 0x28FF);
      yield return (Block.SupplementalArrowsB, 0x2900, 0x297F);
      yield return (Block.MiscellaneousMathematicalSymbolsB, 0x2980, 0x29FF);
      yield return (Block.SupplementalMathematicalOperators, 0x2A00, 0x2AFF);
      yield return (Block.MiscellaneousSymbolsandArrows, 0x2B00, 0x2BFF);
      yield return (Block.Glagolitic, 0x2C00, 0x2C5F);
      yield return (Block.LatinExtendedC, 0x2C60, 0x2C7F);
      yield return (Block.Coptic, 0x2C80, 0x2CFF);
      yield return (Block.GeorgianSupplement, 0x2D00, 0x2D2F);
      yield return (Block.Tifinagh, 0x2D30, 0x2D7F);
      yield return (Block.EthiopicExtended, 0x2D80, 0x2DDF);
      yield return (Block.CyrillicExtendedA, 0x2DE0, 0x2DFF);
      yield return (Block.SupplementalPunctuation, 0x2E00, 0x2E7F);
      yield return (Block.CJKRadicalsSupplement, 0x2E80, 0x2EFF);
      yield return (Block.KangxiRadicals, 0x2F00, 0x2FDF);
      yield return (Block.IdeographicDescriptionCharacters, 0x2FF0, 0x2FFF);
      yield return (Block.CJKSymbolsandPunctuation, 0x3000, 0x303F);
      yield return (Block.Hiragana, 0x3040, 0x309F);
      yield return (Block.Katakana, 0x30A0, 0x30FF);
      yield return (Block.Bopomofo, 0x3100, 0x312F);
      yield return (Block.HangulCompatibilityJamo, 0x3130, 0x318F);
      yield return (Block.Kanbun, 0x3190, 0x319F);
      yield return (Block.BopomofoExtended, 0x31A0, 0x31BF);
      yield return (Block.CJKStrokes, 0x31C0, 0x31EF);
      yield return (Block.KatakanaPhoneticExtensions, 0x31F0, 0x31FF);
      yield return (Block.EnclosedCJKLettersandMonths, 0x3200, 0x32FF);
      yield return (Block.CJKCompatibility, 0x3300, 0x33FF);
      yield return (Block.CJKUnifiedIdeographsExtensionA, 0x3400, 0x4DBF);
      yield return (Block.YijingHexagramSymbols, 0x4DC0, 0x4DFF);
      yield return (Block.CJKUnifiedIdeographs, 0x4E00, 0x9FFF);
      yield return (Block.YiSyllables, 0xA000, 0xA48F);
      yield return (Block.YiRadicals, 0xA490, 0xA4CF);
      yield return (Block.Lisu, 0xA4D0, 0xA4FF);
      yield return (Block.Vai, 0xA500, 0xA63F);
      yield return (Block.CyrillicExtendedB, 0xA640, 0xA69F);
      yield return (Block.Bamum, 0xA6A0, 0xA6FF);
      yield return (Block.ModifierToneLetters, 0xA700, 0xA71F);
      yield return (Block.LatinExtendedD, 0xA720, 0xA7FF);
      yield return (Block.SylotiNagri, 0xA800, 0xA82F);
      yield return (Block.CommonIndicNumberForms, 0xA830, 0xA83F);
      yield return (Block.Phagspa, 0xA840, 0xA87F);
      yield return (Block.Saurashtra, 0xA880, 0xA8DF);
      yield return (Block.DevanagariExtended, 0xA8E0, 0xA8FF);
      yield return (Block.KayahLi, 0xA900, 0xA92F);
      yield return (Block.Rejang, 0xA930, 0xA95F);
      yield return (Block.HangulJamoExtendedA, 0xA960, 0xA97F);
      yield return (Block.Javanese, 0xA980, 0xA9DF);
      yield return (Block.MyanmarExtendedB, 0xA9E0, 0xA9FF);
      yield return (Block.Cham, 0xAA00, 0xAA5F);
      yield return (Block.MyanmarExtendedA, 0xAA60, 0xAA7F);
      yield return (Block.TaiViet, 0xAA80, 0xAADF);
      yield return (Block.MeeteiMayekExtensions, 0xAAE0, 0xAAFF);
      yield return (Block.EthiopicExtendedA, 0xAB00, 0xAB2F);
      yield return (Block.LatinExtendedE, 0xAB30, 0xAB6F);
      yield return (Block.CherokeeSupplement, 0xAB70, 0xABBF);
      yield return (Block.MeeteiMayek, 0xABC0, 0xABFF);
      yield return (Block.HangulSyllables, 0xAC00, 0xD7AF);
      yield return (Block.HangulJamoExtendedB, 0xD7B0, 0xD7FF);
      yield return (Block.HighSurrogates, 0xD800, 0xDB7F);
      yield return (Block.HighPrivateUseSurrogates, 0xDB80, 0xDBFF);
      yield return (Block.LowSurrogates, 0xDC00, 0xDFFF);
      yield return (Block.PrivateUseArea, 0xE000, 0xF8FF);
      yield return (Block.CJKCompatibilityIdeographs, 0xF900, 0xFAFF);
      yield return (Block.AlphabeticPresentationForms, 0xFB00, 0xFB4F);
      yield return (Block.ArabicPresentationFormsA, 0xFB50, 0xFDFF);
      yield return (Block.VariationSelectors, 0xFE00, 0xFE0F);
      yield return (Block.VerticalForms, 0xFE10, 0xFE1F);
      yield return (Block.CombiningHalfMarks, 0xFE20, 0xFE2F);
      yield return (Block.CJKCompatibilityForms, 0xFE30, 0xFE4F);
      yield return (Block.SmallFormVariants, 0xFE50, 0xFE6F);
      yield return (Block.ArabicPresentationFormsB, 0xFE70, 0xFEFF);
      yield return (Block.HalfwidthandFullwidthForms, 0xFF00, 0xFFEF);
      yield return (Block.Specials, 0xFFF0, 0xFFFF);
      yield return (Block.LinearBSyllabary, 0x10000, 0x1007F);
      yield return (Block.LinearBIdeograms, 0x10080, 0x100FF);
      yield return (Block.AegeanNumbers, 0x10100, 0x1013F);
      yield return (Block.AncientGreekNumbers, 0x10140, 0x1018F);
      yield return (Block.AncientSymbols, 0x10190, 0x101CF);
      yield return (Block.PhaistosDisc, 0x101D0, 0x101FF);
      yield return (Block.Lycian, 0x10280, 0x1029F);
      yield return (Block.Carian, 0x102A0, 0x102DF);
      yield return (Block.CopticEpactNumbers, 0x102E0, 0x102FF);
      yield return (Block.OldItalic, 0x10300, 0x1032F);
      yield return (Block.Gothic, 0x10330, 0x1034F);
      yield return (Block.OldPermic, 0x10350, 0x1037F);
      yield return (Block.Ugaritic, 0x10380, 0x1039F);
      yield return (Block.OldPersian, 0x103A0, 0x103DF);
      yield return (Block.Deseret, 0x10400, 0x1044F);
      yield return (Block.Shavian, 0x10450, 0x1047F);
      yield return (Block.Osmanya, 0x10480, 0x104AF);
      yield return (Block.Osage, 0x104B0, 0x104FF);
      yield return (Block.Elbasan, 0x10500, 0x1052F);
      yield return (Block.CaucasianAlbanian, 0x10530, 0x1056F);
      yield return (Block.LinearA, 0x10600, 0x1077F);
      yield return (Block.CypriotSyllabary, 0x10800, 0x1083F);
      yield return (Block.ImperialAramaic, 0x10840, 0x1085F);
      yield return (Block.Palmyrene, 0x10860, 0x1087F);
      yield return (Block.Nabataean, 0x10880, 0x108AF);
      yield return (Block.Hatran, 0x108E0, 0x108FF);
      yield return (Block.Phoenician, 0x10900, 0x1091F);
      yield return (Block.Lydian, 0x10920, 0x1093F);
      yield return (Block.MeroiticHieroglyphs, 0x10980, 0x1099F);
      yield return (Block.MeroiticCursive, 0x109A0, 0x109FF);
      yield return (Block.Kharoshthi, 0x10A00, 0x10A5F);
      yield return (Block.OldSouthArabian, 0x10A60, 0x10A7F);
      yield return (Block.OldNorthArabian, 0x10A80, 0x10A9F);
      yield return (Block.Manichaean, 0x10AC0, 0x10AFF);
      yield return (Block.Avestan, 0x10B00, 0x10B3F);
      yield return (Block.InscriptionalParthian, 0x10B40, 0x10B5F);
      yield return (Block.InscriptionalPahlavi, 0x10B60, 0x10B7F);
      yield return (Block.PsalterPahlavi, 0x10B80, 0x10BAF);
      yield return (Block.OldTurkic, 0x10C00, 0x10C4F);
      yield return (Block.OldHungarian, 0x10C80, 0x10CFF);
      yield return (Block.HanifiRohingya, 0x10D00, 0x10D3F);
      yield return (Block.RumiNumeralSymbols, 0x10E60, 0x10E7F);
      yield return (Block.OldSogdian, 0x10F00, 0x10F2F);
      yield return (Block.Sogdian, 0x10F30, 0x10F6F);
      yield return (Block.Elymaic, 0x10FE0, 0x10FFF);
      yield return (Block.Brahmi, 0x11000, 0x1107F);
      yield return (Block.Kaithi, 0x11080, 0x110CF);
      yield return (Block.SoraSompeng, 0x110D0, 0x110FF);
      yield return (Block.Chakma, 0x11100, 0x1114F);
      yield return (Block.Mahajani, 0x11150, 0x1117F);
      yield return (Block.Sharada, 0x11180, 0x111DF);
      yield return (Block.SinhalaArchaicNumbers, 0x111E0, 0x111FF);
      yield return (Block.Khojki, 0x11200, 0x1124F);
      yield return (Block.Multani, 0x11280, 0x112AF);
      yield return (Block.Khudawadi, 0x112B0, 0x112FF);
      yield return (Block.Grantha, 0x11300, 0x1137F);
      yield return (Block.Newa, 0x11400, 0x1147F);
      yield return (Block.Tirhuta, 0x11480, 0x114DF);
      yield return (Block.Siddham, 0x11580, 0x115FF);
      yield return (Block.Modi, 0x11600, 0x1165F);
      yield return (Block.MongolianSupplement, 0x11660, 0x1167F);
      yield return (Block.Takri, 0x11680, 0x116CF);
      yield return (Block.Ahom, 0x11700, 0x1173F);
      yield return (Block.Dogra, 0x11800, 0x1184F);
      yield return (Block.WarangCiti, 0x118A0, 0x118FF);
      yield return (Block.Nandinagari, 0x119A0, 0x119FF);
      yield return (Block.ZanabazarSquare, 0x11A00, 0x11A4F);
      yield return (Block.Soyombo, 0x11A50, 0x11AAF);
      yield return (Block.PauCinHau, 0x11AC0, 0x11AFF);
      yield return (Block.Bhaiksuki, 0x11C00, 0x11C6F);
      yield return (Block.Marchen, 0x11C70, 0x11CBF);
      yield return (Block.MasaramGondi, 0x11D00, 0x11D5F);
      yield return (Block.GunjalaGondi, 0x11D60, 0x11DAF);
      yield return (Block.Makasar, 0x11EE0, 0x11EFF);
      yield return (Block.TamilSupplement, 0x11FC0, 0x11FFF);
      yield return (Block.Cuneiform, 0x12000, 0x123FF);
      yield return (Block.CuneiformNumbersandPunctuation, 0x12400, 0x1247F);
      yield return (Block.EarlyDynasticCuneiform, 0x12480, 0x1254F);
      yield return (Block.EgyptianHieroglyphs, 0x13000, 0x1342F);
      yield return (Block.EgyptianHieroglyphFormatControls, 0x13430, 0x1343F);
      yield return (Block.AnatolianHieroglyphs, 0x14400, 0x1467F);
      yield return (Block.BamumSupplement, 0x16800, 0x16A3F);
      yield return (Block.Mro, 0x16A40, 0x16A6F);
      yield return (Block.BassaVah, 0x16AD0, 0x16AFF);
      yield return (Block.PahawhHmong, 0x16B00, 0x16B8F);
      yield return (Block.Medefaidrin, 0x16E40, 0x16E9F);
      yield return (Block.Miao, 0x16F00, 0x16F9F);
      yield return (Block.IdeographicSymbolsandPunctuation, 0x16FE0, 0x16FFF);
      yield return (Block.Tangut, 0x17000, 0x187FF);
      yield return (Block.TangutComponents, 0x18800, 0x18AFF);
      yield return (Block.KanaSupplement, 0x1B000, 0x1B0FF);
      yield return (Block.KanaExtendedA, 0x1B100, 0x1B12F);
      yield return (Block.SmallKanaExtension, 0x1B130, 0x1B16F);
      yield return (Block.Nushu, 0x1B170, 0x1B2FF);
      yield return (Block.Duployan, 0x1BC00, 0x1BC9F);
      yield return (Block.ShorthandFormatControls, 0x1BCA0, 0x1BCAF);
      yield return (Block.ByzantineMusicalSymbols, 0x1D000, 0x1D0FF);
      yield return (Block.MusicalSymbols, 0x1D100, 0x1D1FF);
      yield return (Block.AncientGreekMusicalNotation, 0x1D200, 0x1D24F);
      yield return (Block.MayanNumerals, 0x1D2E0, 0x1D2FF);
      yield return (Block.TaiXuanJingSymbols, 0x1D300, 0x1D35F);
      yield return (Block.CountingRodNumerals, 0x1D360, 0x1D37F);
      yield return (Block.MathematicalAlphanumericSymbols, 0x1D400, 0x1D7FF);
      yield return (Block.SuttonSignWriting, 0x1D800, 0x1DAAF);
      yield return (Block.GlagoliticSupplement, 0x1E000, 0x1E02F);
      yield return (Block.NyiakengPuachueHmong, 0x1E100, 0x1E14F);
      yield return (Block.Wancho, 0x1E2C0, 0x1E2FF);
      yield return (Block.MendeKikakui, 0x1E800, 0x1E8DF);
      yield return (Block.Adlam, 0x1E900, 0x1E95F);
      yield return (Block.IndicSiyaqNumbers, 0x1EC70, 0x1ECBF);
      yield return (Block.OttomanSiyaqNumbers, 0x1ED00, 0x1ED4F);
      yield return (Block.ArabicMathematicalAlphabeticSymbols, 0x1EE00, 0x1EEFF);
      yield return (Block.MahjongTiles, 0x1F000, 0x1F02F);
      yield return (Block.DominoTiles, 0x1F030, 0x1F09F);
      yield return (Block.PlayingCards, 0x1F0A0, 0x1F0FF);
      yield return (Block.EnclosedAlphanumericSupplement, 0x1F100, 0x1F1FF);
      yield return (Block.EnclosedIdeographicSupplement, 0x1F200, 0x1F2FF);
      yield return (Block.MiscellaneousSymbolsandPictographs, 0x1F300, 0x1F5FF);
      yield return (Block.Emoticons, 0x1F600, 0x1F64F);
      yield return (Block.OrnamentalDingbats, 0x1F650, 0x1F67F);
      yield return (Block.TransportandMapSymbols, 0x1F680, 0x1F6FF);
      yield return (Block.AlchemicalSymbols, 0x1F700, 0x1F77F);
      yield return (Block.GeometricShapesExtended, 0x1F780, 0x1F7FF);
      yield return (Block.SupplementalArrowsC, 0x1F800, 0x1F8FF);
      yield return (Block.SupplementalSymbolsandPictographs, 0x1F900, 0x1F9FF);
      yield return (Block.ChessSymbols, 0x1FA00, 0x1FA6F);
      yield return (Block.SymbolsandPictographsExtendedA, 0x1FA70, 0x1FAFF);
      yield return (Block.CJKUnifiedIdeographsExtensionB, 0x20000, 0x2A6DF);
      yield return (Block.CJKUnifiedIdeographsExtensionC, 0x2A700, 0x2B73F);
      yield return (Block.CJKUnifiedIdeographsExtensionD, 0x2B740, 0x2B81F);
      yield return (Block.CJKUnifiedIdeographsExtensionE, 0x2B820, 0x2CEAF);
      yield return (Block.CJKUnifiedIdeographsExtensionF, 0x2CEB0, 0x2EBEF);
      yield return (Block.CJKCompatibilityIdeographsSupplement, 0x2F800, 0x2FA1F);
      yield return (Block.Tags, 0xE0000, 0xE007F);
      yield return (Block.VariationSelectorsSupplement, 0xE0100, 0xE01EF);
      yield return (Block.SupplementaryPrivateUseAreaA, 0xF0000, 0xFFFFF);
      yield return (Block.SupplementaryPrivateUseAreaB, 0x100000, 0x10FFFF);
    }
    #endregion Unicode Blocks

    public static Block GetBlock(this System.Text.Rune source)
      => GetBlockRanges().Where(r => source.Value >= r.firstCodeUnit && source.Value <= r.lastCodeUnit).Select(b => b.block).SingleOrValue(Block.Unknown);

    //public static void ToLower(this System.Text.Rune source)
    //  => new char[ source.EncodeToUtf16
  }
}
