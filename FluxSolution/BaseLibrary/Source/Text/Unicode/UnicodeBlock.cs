namespace Flux.Text
{
#pragma warning disable CA1028 // Enum Storage should be Int32
  public enum UnicodeBlock
    : long
  {
    BasicLatin = 0x000000000000007F,
    Latin1Supplement = 0x00000080000000FF,
    LatinExtendedA = 0x000001000000017F,
    LatinExtendedB = 0x000001800000024F,
    IPAExtensions = 0x00000250000002AF,
    SpacingModifierLetters = 0x000002B0000002FF,
    CombiningDiacriticalMarks = 0x000003000000036F,
    GreekandCoptic = 0x00000370000003FF,
    Cyrillic = 0x00000400000004FF,
    CyrillicSupplement = 0x000005000000052F,
    Armenian = 0x000005300000058F,
    Hebrew = 0x00000590000005FF,
    Arabic = 0x00000600000006FF,
    Syriac = 0x000007000000074F,
    ArabicSupplement = 0x000007500000077F,
    Thaana = 0x00000780000007BF,
    NKo = 0x000007C0000007FF,
    Samaritan = 0x000008000000083F,
    Mandaic = 0x000008400000085F,
    SyriacSupplement = 0x000008600000086F,
    ArabicExtendedA = 0x000008A0000008FF,
    Devanagari = 0x000009000000097F,
    Bengali = 0x00000980000009FF,
    Gurmukhi = 0x00000A0000000A7F,
    Gujarati = 0x00000A8000000AFF,
    Oriya = 0x00000B0000000B7F,
    Tamil = 0x00000B8000000BFF,
    Telugu = 0x00000C0000000C7F,
    Kannada = 0x00000C8000000CFF,
    Malayalam = 0x00000D0000000D7F,
    Sinhala = 0x00000D8000000DFF,
    Thai = 0x00000E0000000E7F,
    Lao = 0x00000E8000000EFF,
    Tibetan = 0x00000F0000000FFF,
    Myanmar = 0x000010000000109F,
    Georgian = 0x000010A0000010FF,
    HangulJamo = 0x00001100000011FF,
    Ethiopic = 0x000012000000137F,
    EthiopicSupplement = 0x000013800000139F,
    Cherokee = 0x000013A0000013FF,
    UnifiedCanadianAboriginalSyllabics = 0x000014000000167F,
    Ogham = 0x000016800000169F,
    Runic = 0x000016A0000016FF,
    Tagalog = 0x000017000000171F,
    Hanunoo = 0x000017200000173F,
    Buhid = 0x000017400000175F,
    Tagbanwa = 0x000017600000177F,
    Khmer = 0x00001780000017FF,
    Mongolian = 0x00001800000018AF,
    UnifiedCanadianAboriginalSyllabicsExtended = 0x000018B0000018FF,
    Limbu = 0x000019000000194F,
    TaiLe = 0x000019500000197F,
    NewTaiLue = 0x00001980000019DF,
    KhmerSymbols = 0x000019E0000019FF,
    Buginese = 0x00001A0000001A1F,
    TaiTham = 0x00001A2000001AAF,
    CombiningDiacriticalMarksExtended = 0x00001AB000001AFF,
    Balinese = 0x00001B0000001B7F,
    Sundanese = 0x00001B8000001BBF,
    Batak = 0x00001BC000001BFF,
    Lepcha = 0x00001C0000001C4F,
    OlChiki = 0x00001C5000001C7F,
    CyrillicExtendedC = 0x00001C8000001C8F,
    GeorgianExtended = 0x00001C9000001CBF,
    SundaneseSupplement = 0x00001CC000001CCF,
    VedicExtensions = 0x00001CD000001CFF,
    PhoneticExtensions = 0x00001D0000001D7F,
    PhoneticExtensionsSupplement = 0x00001D8000001DBF,
    CombiningDiacriticalMarksSupplement = 0x00001DC000001DFF,
    LatinExtendedAdditional = 0x00001E0000001EFF,
    GreekExtended = 0x00001F0000001FFF,
    GeneralPunctuation = 0x000020000000206F,
    SuperscriptsandSubscripts = 0x000020700000209F,
    CurrencySymbols = 0x000020A0000020CF,
    CombiningDiacriticalMarksforSymbols = 0x000020D0000020FF,
    LetterlikeSymbols = 0x000021000000214F,
    NumberForms = 0x000021500000218F,
    Arrows = 0x00002190000021FF,
    MathematicalOperators = 0x00002200000022FF,
    MiscellaneousTechnical = 0x00002300000023FF,
    ControlPictures = 0x000024000000243F,
    OpticalCharacterRecognition = 0x000024400000245F,
    EnclosedAlphanumerics = 0x00002460000024FF,
    BoxDrawing = 0x000025000000257F,
    BlockElements = 0x000025800000259F,
    GeometricShapes = 0x000025A0000025FF,
    MiscellaneousSymbols = 0x00002600000026FF,
    Dingbats = 0x00002700000027BF,
    MiscellaneousMathematicalSymbolsA = 0x000027C0000027EF,
    SupplementalArrowsA = 0x000027F0000027FF,
    BraillePatterns = 0x00002800000028FF,
    SupplementalArrowsB = 0x000029000000297F,
    MiscellaneousMathematicalSymbolsB = 0x00002980000029FF,
    SupplementalMathematicalOperators = 0x00002A0000002AFF,
    MiscellaneousSymbolsandArrows = 0x00002B0000002BFF,
    Glagolitic = 0x00002C0000002C5F,
    LatinExtendedC = 0x00002C6000002C7F,
    Coptic = 0x00002C8000002CFF,
    GeorgianSupplement = 0x00002D0000002D2F,
    Tifinagh = 0x00002D3000002D7F,
    EthiopicExtended = 0x00002D8000002DDF,
    CyrillicExtendedA = 0x00002DE000002DFF,
    SupplementalPunctuation = 0x00002E0000002E7F,
    CJKRadicalsSupplement = 0x00002E8000002EFF,
    KangxiRadicals = 0x00002F0000002FDF,
    IdeographicDescriptionCharacters = 0x00002FF000002FFF,
    CJKSymbolsandPunctuation = 0x000030000000303F,
    Hiragana = 0x000030400000309F,
    Katakana = 0x000030A0000030FF,
    Bopomofo = 0x000031000000312F,
    HangulCompatibilityJamo = 0x000031300000318F,
    Kanbun = 0x000031900000319F,
    BopomofoExtended = 0x000031A0000031BF,
    CJKStrokes = 0x000031C0000031EF,
    KatakanaPhoneticExtensions = 0x000031F0000031FF,
    EnclosedCJKLettersandMonths = 0x00003200000032FF,
    CJKCompatibility = 0x00003300000033FF,
    CJKUnifiedIdeographsExtensionA = 0x0000340000004DBF,
    YijingHexagramSymbols = 0x00004DC000004DFF,
    CJKUnifiedIdeographs = 0x00004E0000009FFF,
    YiSyllables = 0x0000A0000000A48F,
    YiRadicals = 0x0000A4900000A4CF,
    Lisu = 0x0000A4D00000A4FF,
    Vai = 0x0000A5000000A63F,
    CyrillicExtendedB = 0x0000A6400000A69F,
    Bamum = 0x0000A6A00000A6FF,
    ModifierToneLetters = 0x0000A7000000A71F,
    LatinExtendedD = 0x0000A7200000A7FF,
    SylotiNagri = 0x0000A8000000A82F,
    CommonIndicNumberForms = 0x0000A8300000A83F,
    Phagspa = 0x0000A8400000A87F,
    Saurashtra = 0x0000A8800000A8DF,
    DevanagariExtended = 0x0000A8E00000A8FF,
    KayahLi = 0x0000A9000000A92F,
    Rejang = 0x0000A9300000A95F,
    HangulJamoExtendedA = 0x0000A9600000A97F,
    Javanese = 0x0000A9800000A9DF,
    MyanmarExtendedB = 0x0000A9E00000A9FF,
    Cham = 0x0000AA000000AA5F,
    MyanmarExtendedA = 0x0000AA600000AA7F,
    TaiViet = 0x0000AA800000AADF,
    MeeteiMayekExtensions = 0x0000AAE00000AAFF,
    EthiopicExtendedA = 0x0000AB000000AB2F,
    LatinExtendedE = 0x0000AB300000AB6F,
    CherokeeSupplement = 0x0000AB700000ABBF,
    MeeteiMayek = 0x0000ABC00000ABFF,
    HangulSyllables = 0x0000AC000000D7AF,
    HangulJamoExtendedB = 0x0000D7B00000D7FF,
    HighSurrogates = 0x0000D8000000DB7F,
    HighPrivateUseSurrogates = 0x0000DB800000DBFF,
    LowSurrogates = 0x0000DC000000DFFF,
    PrivateUseArea = 0x0000E0000000F8FF,
    CJKCompatibilityIdeographs = 0x0000F9000000FAFF,
    AlphabeticPresentationForms = 0x0000FB000000FB4F,
    ArabicPresentationFormsA = 0x0000FB500000FDFF,
    VariationSelectors = 0x0000FE000000FE0F,
    VerticalForms = 0x0000FE100000FE1F,
    CombiningHalfMarks = 0x0000FE200000FE2F,
    CJKCompatibilityForms = 0x0000FE300000FE4F,
    SmallFormVariants = 0x0000FE500000FE6F,
    ArabicPresentationFormsB = 0x0000FE700000FEFF,
    HalfwidthandFullwidthForms = 0x0000FF000000FFEF,
    Specials = 0x0000FFF00000FFFF,
    LinearBSyllabary = 0x000100000001007F,
    LinearBIdeograms = 0x00010080000100FF,
    AegeanNumbers = 0x000101000001013F,
    AncientGreekNumbers = 0x000101400001018F,
    AncientSymbols = 0x00010190000101CF,
    PhaistosDisc = 0x000101D0000101FF,
    Lycian = 0x000102800001029F,
    Carian = 0x000102A0000102DF,
    CopticEpactNumbers = 0x000102E0000102FF,
    OldItalic = 0x000103000001032F,
    Gothic = 0x000103300001034F,
    OldPermic = 0x000103500001037F,
    Ugaritic = 0x000103800001039F,
    OldPersian = 0x000103A0000103DF,
    Deseret = 0x000104000001044F,
    Shavian = 0x000104500001047F,
    Osmanya = 0x00010480000104AF,
    Osage = 0x000104B0000104FF,
    Elbasan = 0x000105000001052F,
    CaucasianAlbanian = 0x000105300001056F,
    LinearA = 0x000106000001077F,
    CypriotSyllabary = 0x000108000001083F,
    ImperialAramaic = 0x000108400001085F,
    Palmyrene = 0x000108600001087F,
    Nabataean = 0x00010880000108AF,
    Hatran = 0x000108E0000108FF,
    Phoenician = 0x000109000001091F,
    Lydian = 0x000109200001093F,
    MeroiticHieroglyphs = 0x000109800001099F,
    MeroiticCursive = 0x000109A0000109FF,
    Kharoshthi = 0x00010A0000010A5F,
    OldSouthArabian = 0x00010A6000010A7F,
    OldNorthArabian = 0x00010A8000010A9F,
    Manichaean = 0x00010AC000010AFF,
    Avestan = 0x00010B0000010B3F,
    InscriptionalParthian = 0x00010B4000010B5F,
    InscriptionalPahlavi = 0x00010B6000010B7F,
    PsalterPahlavi = 0x00010B8000010BAF,
    OldTurkic = 0x00010C0000010C4F,
    OldHungarian = 0x00010C8000010CFF,
    HanifiRohingya = 0x00010D0000010D3F,
    RumiNumeralSymbols = 0x00010E6000010E7F,
    OldSogdian = 0x00010F0000010F2F,
    Sogdian = 0x00010F3000010F6F,
    Elymaic = 0x00010FE000010FFF,
    Brahmi = 0x000110000001107F,
    Kaithi = 0x00011080000110CF,
    SoraSompeng = 0x000110D0000110FF,
    Chakma = 0x000111000001114F,
    Mahajani = 0x000111500001117F,
    Sharada = 0x00011180000111DF,
    SinhalaArchaicNumbers = 0x000111E0000111FF,
    Khojki = 0x000112000001124F,
    Multani = 0x00011280000112AF,
    Khudawadi = 0x000112B0000112FF,
    Grantha = 0x000113000001137F,
    Newa = 0x000114000001147F,
    Tirhuta = 0x00011480000114DF,
    Siddham = 0x00011580000115FF,
    Modi = 0x000116000001165F,
    MongolianSupplement = 0x000116600001167F,
    Takri = 0x00011680000116CF,
    Ahom = 0x000117000001173F,
    Dogra = 0x000118000001184F,
    WarangCiti = 0x000118A0000118FF,
    Nandinagari = 0x000119A0000119FF,
    ZanabazarSquare = 0x00011A0000011A4F,
    Soyombo = 0x00011A5000011AAF,
    PauCinHau = 0x00011AC000011AFF,
    Bhaiksuki = 0x00011C0000011C6F,
    Marchen = 0x00011C7000011CBF,
    MasaramGondi = 0x00011D0000011D5F,
    GunjalaGondi = 0x00011D6000011DAF,
    Makasar = 0x00011EE000011EFF,
    TamilSupplement = 0x00011FC000011FFF,
    Cuneiform = 0x00012000000123FF,
    CuneiformNumbersandPunctuation = 0x000124000001247F,
    EarlyDynasticCuneiform = 0x000124800001254F,
    EgyptianHieroglyphs = 0x000130000001342F,
    EgyptianHieroglyphFormatControls = 0x000134300001343F,
    AnatolianHieroglyphs = 0x000144000001467F,
    BamumSupplement = 0x0001680000016A3F,
    Mro = 0x00016A4000016A6F,
    BassaVah = 0x00016AD000016AFF,
    PahawhHmong = 0x00016B0000016B8F,
    Medefaidrin = 0x00016E4000016E9F,
    Miao = 0x00016F0000016F9F,
    IdeographicSymbolsandPunctuation = 0x00016FE000016FFF,
    Tangut = 0x00017000000187FF,
    TangutComponents = 0x0001880000018AFF,
    KanaSupplement = 0x0001B0000001B0FF,
    KanaExtendedA = 0x0001B1000001B12F,
    SmallKanaExtension = 0x0001B1300001B16F,
    Nushu = 0x0001B1700001B2FF,
    Duployan = 0x0001BC000001BC9F,
    ShorthandFormatControls = 0x0001BCA00001BCAF,
    ByzantineMusicalSymbols = 0x0001D0000001D0FF,
    MusicalSymbols = 0x0001D1000001D1FF,
    AncientGreekMusicalNotation = 0x0001D2000001D24F,
    MayanNumerals = 0x0001D2E00001D2FF,
    TaiXuanJingSymbols = 0x0001D3000001D35F,
    CountingRodNumerals = 0x0001D3600001D37F,
    MathematicalAlphanumericSymbols = 0x0001D4000001D7FF,
    SuttonSignWriting = 0x0001D8000001DAAF,
    GlagoliticSupplement = 0x0001E0000001E02F,
    NyiakengPuachueHmong = 0x0001E1000001E14F,
    Wancho = 0x0001E2C00001E2FF,
    MendeKikakui = 0x0001E8000001E8DF,
    Adlam = 0x0001E9000001E95F,
    IndicSiyaqNumbers = 0x0001EC700001ECBF,
    OttomanSiyaqNumbers = 0x0001ED000001ED4F,
    ArabicMathematicalAlphabeticSymbols = 0x0001EE000001EEFF,
    MahjongTiles = 0x0001F0000001F02F,
    DominoTiles = 0x0001F0300001F09F,
    PlayingCards = 0x0001F0A00001F0FF,
    EnclosedAlphanumericSupplement = 0x0001F1000001F1FF,
    EnclosedIdeographicSupplement = 0x0001F2000001F2FF,
    MiscellaneousSymbolsandPictographs = 0x0001F3000001F5FF,
    Emoticons = 0x0001F6000001F64F,
    OrnamentalDingbats = 0x0001F6500001F67F,
    TransportandMapSymbols = 0x0001F6800001F6FF,
    AlchemicalSymbols = 0x0001F7000001F77F,
    GeometricShapesExtended = 0x0001F7800001F7FF,
    SupplementalArrowsC = 0x0001F8000001F8FF,
    SupplementalSymbolsandPictographs = 0x0001F9000001F9FF,
    ChessSymbols = 0x0001FA000001FA6F,
    SymbolsandPictographsExtendedA = 0x0001FA700001FAFF,
    CJKUnifiedIdeographsExtensionB = 0x000200000002A6DF,
    CJKUnifiedIdeographsExtensionC = 0x0002A7000002B73F,
    CJKUnifiedIdeographsExtensionD = 0x0002B7400002B81F,
    CJKUnifiedIdeographsExtensionE = 0x0002B8200002CEAF,
    CJKUnifiedIdeographsExtensionF = 0x0002CEB00002EBEF,
    CJKCompatibilityIdeographsSupplement = 0x0002F8000002FA1F,
    Tags = 0x000E0000000E007F,
    VariationSelectorsSupplement = 0x000E0100000E01EF,
    SupplementaryPrivateUseAreaA = 0x000F0000000FFFFF,
    SupplementaryPrivateUseAreaB = 0x001000000010FFFF
  }
#pragma warning restore CA1028 // Enum Storage should be Int32

  namespace Text
  {
    public static partial class UnicodeBlock
    {
      //public static (UnicodeBlockFirst first, UnicodeBlockLast last) GetBlock(System.Text.Rune rune)
      //{
      //  rune.GetBlock
      //}

      //public static System.Text.Rune GetFirstRuneOf(UnicodeBlock blockStart)
      //  => new System.Text.Rune((int)blockStart);
      //public static System.Text.Rune GetLastRuneOf(UnicodeBlock blockEnd)
      //  => new System.Text.Rune((int)blockEnd);

      //public static System.Collections.Generic.IEnumerable<System.Text.Rune> GetCodePoints(this UnicodeBlockFirst source)
      //{
      //  var start = (char)(int)source;
      //  var stop = (char)(int)(UnicodeBlockLast)(int)source;

      //  for (var codePoint = start; codePoint <= stop; codePoint++)
      //    yield return new System.Text.Rune(codePoint);
      //}
      //public static System.Collections.Generic.IEnumerable<char> GetCodeUnits(this Block source)
      //{
      //  if (source > Block.Specials) throw new System.ArgumentOutOfRangeException(nameof(source));

      //  var lastCodeUnit = (char)source.GetLastCodePoint();
      //  for (var codeUnit = (char)source.GetFirstCodePoint(); codeUnit < lastCodeUnit; codeUnit++)
      //    yield return codeUnit;
      //  yield return lastCodeUnit;
      //}


      //public static int GetFirstCodePoint(this Block source) => source switch
      //  {
      //    Block.BasicLatin => 0x00,
      //    Block.Latin1Supplement => 0x80,
      //    Block.LatinExtendedA => 0x100,
      //    Block.LatinExtendedB => 0x180,
      //    Block.IPAExtensions => 0x250,
      //    Block.SpacingModifierLetters => 0x2B0,
      //    Block.CombiningDiacriticalMarks => 0x300,
      //    Block.GreekandCoptic => 0x370,
      //    Block.Cyrillic => 0x400,
      //    Block.CyrillicSupplement => 0x500,
      //    Block.Armenian => 0x530,
      //    Block.Hebrew => 0x590,
      //    Block.Arabic => 0x600,
      //    Block.Syriac => 0x700,
      //    Block.ArabicSupplement => 0x750,
      //    Block.Thaana => 0x780,
      //    Block.NKo => 0x7C0,
      //    Block.Samaritan => 0x800,
      //    Block.Mandaic => 0x840,
      //    Block.SyriacSupplement => 0x860,
      //    Block.ArabicExtendedA => 0x8A0,
      //    Block.Devanagari => 0x900,
      //    Block.Bengali => 0x980,
      //    Block.Gurmukhi => 0xA00,
      //    Block.Gujarati => 0xA80,
      //    Block.Oriya => 0xB00,
      //    Block.Tamil => 0xB80,
      //    Block.Telugu => 0xC00,
      //    Block.Kannada => 0xC80,
      //    Block.Malayalam => 0xD00,
      //    Block.Sinhala => 0xD80,
      //    Block.Thai => 0xE00,
      //    Block.Lao => 0xE80,
      //    Block.Tibetan => 0xF00,
      //    Block.Myanmar => 0x1000,
      //    Block.Georgian => 0x10A0,
      //    Block.HangulJamo => 0x1100,
      //    Block.Ethiopic => 0x1200,
      //    Block.EthiopicSupplement => 0x1380,
      //    Block.Cherokee => 0x13A0,
      //    Block.UnifiedCanadianAboriginalSyllabics => 0x1400,
      //    Block.Ogham => 0x1680,
      //    Block.Runic => 0x16A0,
      //    Block.Tagalog => 0x1700,
      //    Block.Hanunoo => 0x1720,
      //    Block.Buhid => 0x1740,
      //    Block.Tagbanwa => 0x1760,
      //    Block.Khmer => 0x1780,
      //    Block.Mongolian => 0x1800,
      //    Block.UnifiedCanadianAboriginalSyllabicsExtended => 0x18B0,
      //    Block.Limbu => 0x1900,
      //    Block.TaiLe => 0x1950,
      //    Block.NewTaiLue => 0x1980,
      //    Block.KhmerSymbols => 0x19E0,
      //    Block.Buginese => 0x1A00,
      //    Block.TaiTham => 0x1A20,
      //    Block.CombiningDiacriticalMarksExtended => 0x1AB0,
      //    Block.Balinese => 0x1B00,
      //    Block.Sundanese => 0x1B80,
      //    Block.Batak => 0x1BC0,
      //    Block.Lepcha => 0x1C00,
      //    Block.OlChiki => 0x1C50,
      //    Block.CyrillicExtendedC => 0x1C80,
      //    Block.GeorgianExtended => 0x1C90,
      //    Block.SundaneseSupplement => 0x1CC0,
      //    Block.VedicExtensions => 0x1CD0,
      //    Block.PhoneticExtensions => 0x1D00,
      //    Block.PhoneticExtensionsSupplement => 0x1D80,
      //    Block.CombiningDiacriticalMarksSupplement => 0x1DC0,
      //    Block.LatinExtendedAdditional => 0x1E00,
      //    Block.GreekExtended => 0x1F00,
      //    Block.GeneralPunctuation => 0x2000,
      //    Block.SuperscriptsandSubscripts => 0x2070,
      //    Block.CurrencySymbols => 0x20A0,
      //    Block.CombiningDiacriticalMarksforSymbols => 0x20D0,
      //    Block.LetterlikeSymbols => 0x2100,
      //    Block.NumberForms => 0x2150,
      //    Block.Arrows => 0x2190,
      //    Block.MathematicalOperators => 0x2200,
      //    Block.MiscellaneousTechnical => 0x2300,
      //    Block.ControlPictures => 0x2400,
      //    Block.OpticalCharacterRecognition => 0x2440,
      //    Block.EnclosedAlphanumerics => 0x2460,
      //    Block.BoxDrawing => 0x2500,
      //    Block.BlockElements => 0x2580,
      //    Block.GeometricShapes => 0x25A0,
      //    Block.MiscellaneousSymbols => 0x2600,
      //    Block.Dingbats => 0x2700,
      //    Block.MiscellaneousMathematicalSymbolsA => 0x27C0,
      //    Block.SupplementalArrowsA => 0x27F0,
      //    Block.BraillePatterns => 0x2800,
      //    Block.SupplementalArrowsB => 0x2900,
      //    Block.MiscellaneousMathematicalSymbolsB => 0x2980,
      //    Block.SupplementalMathematicalOperators => 0x2A00,
      //    Block.MiscellaneousSymbolsandArrows => 0x2B00,
      //    Block.Glagolitic => 0x2C00,
      //    Block.LatinExtendedC => 0x2C60,
      //    Block.Coptic => 0x2C80,
      //    Block.GeorgianSupplement => 0x2D00,
      //    Block.Tifinagh => 0x2D30,
      //    Block.EthiopicExtended => 0x2D80,
      //    Block.CyrillicExtendedA => 0x2DE0,
      //    Block.SupplementalPunctuation => 0x2E00,
      //    Block.CJKRadicalsSupplement => 0x2E80,
      //    Block.KangxiRadicals => 0x2F00,
      //    Block.IdeographicDescriptionCharacters => 0x2FF0,
      //    Block.CJKSymbolsandPunctuation => 0x3000,
      //    Block.Hiragana => 0x3040,
      //    Block.Katakana => 0x30A0,
      //    Block.Bopomofo => 0x3100,
      //    Block.HangulCompatibilityJamo => 0x3130,
      //    Block.Kanbun => 0x3190,
      //    Block.BopomofoExtended => 0x31A0,
      //    Block.CJKStrokes => 0x31C0,
      //    Block.KatakanaPhoneticExtensions => 0x31F0,
      //    Block.EnclosedCJKLettersandMonths => 0x3200,
      //    Block.CJKCompatibility => 0x3300,
      //    Block.CJKUnifiedIdeographsExtensionA => 0x3400,
      //    Block.YijingHexagramSymbols => 0x4DC0,
      //    Block.CJKUnifiedIdeographs => 0x4E00,
      //    Block.YiSyllables => 0xA000,
      //    Block.YiRadicals => 0xA490,
      //    Block.Lisu => 0xA4D0,
      //    Block.Vai => 0xA500,
      //    Block.CyrillicExtendedB => 0xA640,
      //    Block.Bamum => 0xA6A0,
      //    Block.ModifierToneLetters => 0xA700,
      //    Block.LatinExtendedD => 0xA720,
      //    Block.SylotiNagri => 0xA800,
      //    Block.CommonIndicNumberForms => 0xA830,
      //    Block.Phagspa => 0xA840,
      //    Block.Saurashtra => 0xA880,
      //    Block.DevanagariExtended => 0xA8E0,
      //    Block.KayahLi => 0xA900,
      //    Block.Rejang => 0xA930,
      //    Block.HangulJamoExtendedA => 0xA960,
      //    Block.Javanese => 0xA980,
      //    Block.MyanmarExtendedB => 0xA9E0,
      //    Block.Cham => 0xAA00,
      //    Block.MyanmarExtendedA => 0xAA60,
      //    Block.TaiViet => 0xAA80,
      //    Block.MeeteiMayekExtensions => 0xAAE0,
      //    Block.EthiopicExtendedA => 0xAB00,
      //    Block.LatinExtendedE => 0xAB30,
      //    Block.CherokeeSupplement => 0xAB70,
      //    Block.MeeteiMayek => 0xABC0,
      //    Block.HangulSyllables => 0xAC00,
      //    Block.HangulJamoExtendedB => 0xD7B0,
      //    Block.HighSurrogates => 0xD800,
      //    Block.HighPrivateUseSurrogates => 0xDB80,
      //    Block.LowSurrogates => 0xDC00,
      //    Block.PrivateUseArea => 0xE000,
      //    Block.CJKCompatibilityIdeographs => 0xF900,
      //    Block.AlphabeticPresentationForms => 0xFB00,
      //    Block.ArabicPresentationFormsA => 0xFB50,
      //    Block.VariationSelectors => 0xFE00,
      //    Block.VerticalForms => 0xFE10,
      //    Block.CombiningHalfMarks => 0xFE20,
      //    Block.CJKCompatibilityForms => 0xFE30,
      //    Block.SmallFormVariants => 0xFE50,
      //    Block.ArabicPresentationFormsB => 0xFE70,
      //    Block.HalfwidthandFullwidthForms => 0xFF00,
      //    Block.Specials => 0xFFF0,
      //    Block.LinearBSyllabary => 0x10000,
      //    Block.LinearBIdeograms => 0x10080,
      //    Block.AegeanNumbers => 0x10100,
      //    Block.AncientGreekNumbers => 0x10140,
      //    Block.AncientSymbols => 0x10190,
      //    Block.PhaistosDisc => 0x101D0,
      //    Block.Lycian => 0x10280,
      //    Block.Carian => 0x102A0,
      //    Block.CopticEpactNumbers => 0x102E0,
      //    Block.OldItalic => 0x10300,
      //    Block.Gothic => 0x10330,
      //    Block.OldPermic => 0x10350,
      //    Block.Ugaritic => 0x10380,
      //    Block.OldPersian => 0x103A0,
      //    Block.Deseret => 0x10400,
      //    Block.Shavian => 0x10450,
      //    Block.Osmanya => 0x10480,
      //    Block.Osage => 0x104B0,
      //    Block.Elbasan => 0x10500,
      //    Block.CaucasianAlbanian => 0x10530,
      //    Block.LinearA => 0x10600,
      //    Block.CypriotSyllabary => 0x10800,
      //    Block.ImperialAramaic => 0x10840,
      //    Block.Palmyrene => 0x10860,
      //    Block.Nabataean => 0x10880,
      //    Block.Hatran => 0x108E0,
      //    Block.Phoenician => 0x10900,
      //    Block.Lydian => 0x10920,
      //    Block.MeroiticHieroglyphs => 0x10980,
      //    Block.MeroiticCursive => 0x109A0,
      //    Block.Kharoshthi => 0x10A00,
      //    Block.OldSouthArabian => 0x10A60,
      //    Block.OldNorthArabian => 0x10A80,
      //    Block.Manichaean => 0x10AC0,
      //    Block.Avestan => 0x10B00,
      //    Block.InscriptionalParthian => 0x10B40,
      //    Block.InscriptionalPahlavi => 0x10B60,
      //    Block.PsalterPahlavi => 0x10B80,
      //    Block.OldTurkic => 0x10C00,
      //    Block.OldHungarian => 0x10C80,
      //    Block.HanifiRohingya => 0x10D00,
      //    Block.RumiNumeralSymbols => 0x10E60,
      //    Block.OldSogdian => 0x10F00,
      //    Block.Sogdian => 0x10F30,
      //    Block.Elymaic => 0x10FE0,
      //    Block.Brahmi => 0x11000,
      //    Block.Kaithi => 0x11080,
      //    Block.SoraSompeng => 0x110D0,
      //    Block.Chakma => 0x11100,
      //    Block.Mahajani => 0x11150,
      //    Block.Sharada => 0x11180,
      //    Block.SinhalaArchaicNumbers => 0x111E0,
      //    Block.Khojki => 0x11200,
      //    Block.Multani => 0x11280,
      //    Block.Khudawadi => 0x112B0,
      //    Block.Grantha => 0x11300,
      //    Block.Newa => 0x11400,
      //    Block.Tirhuta => 0x11480,
      //    Block.Siddham => 0x11580,
      //    Block.Modi => 0x11600,
      //    Block.MongolianSupplement => 0x11660,
      //    Block.Takri => 0x11680,
      //    Block.Ahom => 0x11700,
      //    Block.Dogra => 0x11800,
      //    Block.WarangCiti => 0x118A0,
      //    Block.Nandinagari => 0x119A0,
      //    Block.ZanabazarSquare => 0x11A00,
      //    Block.Soyombo => 0x11A50,
      //    Block.PauCinHau => 0x11AC0,
      //    Block.Bhaiksuki => 0x11C00,
      //    Block.Marchen => 0x11C70,
      //    Block.MasaramGondi => 0x11D00,
      //    Block.GunjalaGondi => 0x11D60,
      //    Block.Makasar => 0x11EE0,
      //    Block.TamilSupplement => 0x11FC0,
      //    Block.Cuneiform => 0x12000,
      //    Block.CuneiformNumbersandPunctuation => 0x12400,
      //    Block.EarlyDynasticCuneiform => 0x12480,
      //    Block.EgyptianHieroglyphs => 0x13000,
      //    Block.EgyptianHieroglyphFormatControls => 0x13430,
      //    Block.AnatolianHieroglyphs => 0x14400,
      //    Block.BamumSupplement => 0x16800,
      //    Block.Mro => 0x16A40,
      //    Block.BassaVah => 0x16AD0,
      //    Block.PahawhHmong => 0x16B00,
      //    Block.Medefaidrin => 0x16E40,
      //    Block.Miao => 0x16F00,
      //    Block.IdeographicSymbolsandPunctuation => 0x16FE0,
      //    Block.Tangut => 0x17000,
      //    Block.TangutComponents => 0x18800,
      //    Block.KanaSupplement => 0x1B000,
      //    Block.KanaExtendedA => 0x1B100,
      //    Block.SmallKanaExtension => 0x1B130,
      //    Block.Nushu => 0x1B170,
      //    Block.Duployan => 0x1BC00,
      //    Block.ShorthandFormatControls => 0x1BCA0,
      //    Block.ByzantineMusicalSymbols => 0x1D000,
      //    Block.MusicalSymbols => 0x1D100,
      //    Block.AncientGreekMusicalNotation => 0x1D200,
      //    Block.MayanNumerals => 0x1D2E0,
      //    Block.TaiXuanJingSymbols => 0x1D300,
      //    Block.CountingRodNumerals => 0x1D360,
      //    Block.MathematicalAlphanumericSymbols => 0x1D400,
      //    Block.SuttonSignWriting => 0x1D800,
      //    Block.GlagoliticSupplement => 0x1E000,
      //    Block.NyiakengPuachueHmong => 0x1E100,
      //    Block.Wancho => 0x1E2C0,
      //    Block.MendeKikakui => 0x1E800,
      //    Block.Adlam => 0x1E900,
      //    Block.IndicSiyaqNumbers => 0x1EC70,
      //    Block.OttomanSiyaqNumbers => 0x1ED00,
      //    Block.ArabicMathematicalAlphabeticSymbols => 0x1EE00,
      //    Block.MahjongTiles => 0x1F000,
      //    Block.DominoTiles => 0x1F030,
      //    Block.PlayingCards => 0x1F0A0,
      //    Block.EnclosedAlphanumericSupplement => 0x1F100,
      //    Block.EnclosedIdeographicSupplement => 0x1F200,
      //    Block.MiscellaneousSymbolsandPictographs => 0x1F300,
      //    Block.Emoticons => 0x1F600,
      //    Block.OrnamentalDingbats => 0x1F650,
      //    Block.TransportandMapSymbols => 0x1F680,
      //    Block.AlchemicalSymbols => 0x1F700,
      //    Block.GeometricShapesExtended => 0x1F780,
      //    Block.SupplementalArrowsC => 0x1F800,
      //    Block.SupplementalSymbolsandPictographs => 0x1F900,
      //    Block.ChessSymbols => 0x1FA00,
      //    Block.SymbolsandPictographsExtendedA => 0x1FA70,
      //    Block.CJKUnifiedIdeographsExtensionB => 0x20000,
      //    Block.CJKUnifiedIdeographsExtensionC => 0x2A700,
      //    Block.CJKUnifiedIdeographsExtensionD => 0x2B740,
      //    Block.CJKUnifiedIdeographsExtensionE => 0x2B820,
      //    Block.CJKUnifiedIdeographsExtensionF => 0x2CEB0,
      //    Block.CJKCompatibilityIdeographsSupplement => 0x2F800,
      //    Block.Tags => 0xE0000,
      //    Block.VariationSelectorsSupplement => 0xE0100,
      //    Block.SupplementaryPrivateUseAreaA => 0xF0000,
      //    Block.SupplementaryPrivateUseAreaB => 0x100000,
      //    _ => throw new System.ArgumentOutOfRangeException(nameof(source)),
      //  };
      //  public static int GetLastCodePoint(this Block source) => source switch
      //  {
      //    Block.BasicLatin => 0x7F,
      //    Block.Latin1Supplement => 0xFF,
      //    Block.LatinExtendedA => 0x17F,
      //    Block.LatinExtendedB => 0x24F,
      //    Block.IPAExtensions => 0x2AF,
      //    Block.SpacingModifierLetters => 0x2FF,
      //    Block.CombiningDiacriticalMarks => 0x36F,
      //    Block.GreekandCoptic => 0x3FF,
      //    Block.Cyrillic => 0x4FF,
      //    Block.CyrillicSupplement => 0x52F,
      //    Block.Armenian => 0x58F,
      //    Block.Hebrew => 0x5FF,
      //    Block.Arabic => 0x6FF,
      //    Block.Syriac => 0x74F,
      //    Block.ArabicSupplement => 0x77F,
      //    Block.Thaana => 0x7BF,
      //    Block.NKo => 0x7FF,
      //    Block.Samaritan => 0x83F,
      //    Block.Mandaic => 0x85F,
      //    Block.SyriacSupplement => 0x86F,
      //    Block.ArabicExtendedA => 0x8FF,
      //    Block.Devanagari => 0x97F,
      //    Block.Bengali => 0x9FF,
      //    Block.Gurmukhi => 0xA7F,
      //    Block.Gujarati => 0xAFF,
      //    Block.Oriya => 0xB7F,
      //    Block.Tamil => 0xBFF,
      //    Block.Telugu => 0xC7F,
      //    Block.Kannada => 0xCFF,
      //    Block.Malayalam => 0xD7F,
      //    Block.Sinhala => 0xDFF,
      //    Block.Thai => 0xE7F,
      //    Block.Lao => 0xEFF,
      //    Block.Tibetan => 0xFFF,
      //    Block.Myanmar => 0x109F,
      //    Block.Georgian => 0x10FF,
      //    Block.HangulJamo => 0x11FF,
      //    Block.Ethiopic => 0x137F,
      //    Block.EthiopicSupplement => 0x139F,
      //    Block.Cherokee => 0x13FF,
      //    Block.UnifiedCanadianAboriginalSyllabics => 0x167F,
      //    Block.Ogham => 0x169F,
      //    Block.Runic => 0x16FF,
      //    Block.Tagalog => 0x171F,
      //    Block.Hanunoo => 0x173F,
      //    Block.Buhid => 0x175F,
      //    Block.Tagbanwa => 0x177F,
      //    Block.Khmer => 0x17FF,
      //    Block.Mongolian => 0x18AF,
      //    Block.UnifiedCanadianAboriginalSyllabicsExtended => 0x18FF,
      //    Block.Limbu => 0x194F,
      //    Block.TaiLe => 0x197F,
      //    Block.NewTaiLue => 0x19DF,
      //    Block.KhmerSymbols => 0x19FF,
      //    Block.Buginese => 0x1A1F,
      //    Block.TaiTham => 0x1AAF,
      //    Block.CombiningDiacriticalMarksExtended => 0x1AFF,
      //    Block.Balinese => 0x1B7F,
      //    Block.Sundanese => 0x1BBF,
      //    Block.Batak => 0x1BFF,
      //    Block.Lepcha => 0x1C4F,
      //    Block.OlChiki => 0x1C7F,
      //    Block.CyrillicExtendedC => 0x1C8F,
      //    Block.GeorgianExtended => 0x1CBF,
      //    Block.SundaneseSupplement => 0x1CCF,
      //    Block.VedicExtensions => 0x1CFF,
      //    Block.PhoneticExtensions => 0x1D7F,
      //    Block.PhoneticExtensionsSupplement => 0x1DBF,
      //    Block.CombiningDiacriticalMarksSupplement => 0x1DFF,
      //    Block.LatinExtendedAdditional => 0x1EFF,
      //    Block.GreekExtended => 0x1FFF,
      //    Block.GeneralPunctuation => 0x206F,
      //    Block.SuperscriptsandSubscripts => 0x209F,
      //    Block.CurrencySymbols => 0x20CF,
      //    Block.CombiningDiacriticalMarksforSymbols => 0x20FF,
      //    Block.LetterlikeSymbols => 0x214F,
      //    Block.NumberForms => 0x218F,
      //    Block.Arrows => 0x21FF,
      //    Block.MathematicalOperators => 0x22FF,
      //    Block.MiscellaneousTechnical => 0x23FF,
      //    Block.ControlPictures => 0x243F,
      //    Block.OpticalCharacterRecognition => 0x245F,
      //    Block.EnclosedAlphanumerics => 0x24FF,
      //    Block.BoxDrawing => 0x257F,
      //    Block.BlockElements => 0x259F,
      //    Block.GeometricShapes => 0x25FF,
      //    Block.MiscellaneousSymbols => 0x26FF,
      //    Block.Dingbats => 0x27BF,
      //    Block.MiscellaneousMathematicalSymbolsA => 0x27EF,
      //    Block.SupplementalArrowsA => 0x27FF,
      //    Block.BraillePatterns => 0x28FF,
      //    Block.SupplementalArrowsB => 0x297F,
      //    Block.MiscellaneousMathematicalSymbolsB => 0x29FF,
      //    Block.SupplementalMathematicalOperators => 0x2AFF,
      //    Block.MiscellaneousSymbolsandArrows => 0x2BFF,
      //    Block.Glagolitic => 0x2C5F,
      //    Block.LatinExtendedC => 0x2C7F,
      //    Block.Coptic => 0x2CFF,
      //    Block.GeorgianSupplement => 0x2D2F,
      //    Block.Tifinagh => 0x2D7F,
      //    Block.EthiopicExtended => 0x2DDF,
      //    Block.CyrillicExtendedA => 0x2DFF,
      //    Block.SupplementalPunctuation => 0x2E7F,
      //    Block.CJKRadicalsSupplement => 0x2EFF,
      //    Block.KangxiRadicals => 0x2FDF,
      //    Block.IdeographicDescriptionCharacters => 0x2FFF,
      //    Block.CJKSymbolsandPunctuation => 0x303F,
      //    Block.Hiragana => 0x309F,
      //    Block.Katakana => 0x30FF,
      //    Block.Bopomofo => 0x312F,
      //    Block.HangulCompatibilityJamo => 0x318F,
      //    Block.Kanbun => 0x319F,
      //    Block.BopomofoExtended => 0x31BF,
      //    Block.CJKStrokes => 0x31EF,
      //    Block.KatakanaPhoneticExtensions => 0x31FF,
      //    Block.EnclosedCJKLettersandMonths => 0x32FF,
      //    Block.CJKCompatibility => 0x33FF,
      //    Block.CJKUnifiedIdeographsExtensionA => 0x4DBF,
      //    Block.YijingHexagramSymbols => 0x4DFF,
      //    Block.CJKUnifiedIdeographs => 0x9FFF,
      //    Block.YiSyllables => 0xA48F,
      //    Block.YiRadicals => 0xA4CF,
      //    Block.Lisu => 0xA4FF,
      //    Block.Vai => 0xA63F,
      //    Block.CyrillicExtendedB => 0xA69F,
      //    Block.Bamum => 0xA6FF,
      //    Block.ModifierToneLetters => 0xA71F,
      //    Block.LatinExtendedD => 0xA7FF,
      //    Block.SylotiNagri => 0xA82F,
      //    Block.CommonIndicNumberForms => 0xA83F,
      //    Block.Phagspa => 0xA87F,
      //    Block.Saurashtra => 0xA8DF,
      //    Block.DevanagariExtended => 0xA8FF,
      //    Block.KayahLi => 0xA92F,
      //    Block.Rejang => 0xA95F,
      //    Block.HangulJamoExtendedA => 0xA97F,
      //    Block.Javanese => 0xA9DF,
      //    Block.MyanmarExtendedB => 0xA9FF,
      //    Block.Cham => 0xAA5F,
      //    Block.MyanmarExtendedA => 0xAA7F,
      //    Block.TaiViet => 0xAADF,
      //    Block.MeeteiMayekExtensions => 0xAAFF,
      //    Block.EthiopicExtendedA => 0xAB2F,
      //    Block.LatinExtendedE => 0xAB6F,
      //    Block.CherokeeSupplement => 0xABBF,
      //    Block.MeeteiMayek => 0xABFF,
      //    Block.HangulSyllables => 0xD7AF,
      //    Block.HangulJamoExtendedB => 0xD7FF,
      //    Block.HighSurrogates => 0xDB7F,
      //    Block.HighPrivateUseSurrogates => 0xDBFF,
      //    Block.LowSurrogates => 0xDFFF,
      //    Block.PrivateUseArea => 0xF8FF,
      //    Block.CJKCompatibilityIdeographs => 0xFAFF,
      //    Block.AlphabeticPresentationForms => 0xFB4F,
      //    Block.ArabicPresentationFormsA => 0xFDFF,
      //    Block.VariationSelectors => 0xFE0F,
      //    Block.VerticalForms => 0xFE1F,
      //    Block.CombiningHalfMarks => 0xFE2F,
      //    Block.CJKCompatibilityForms => 0xFE4F,
      //    Block.SmallFormVariants => 0xFE6F,
      //    Block.ArabicPresentationFormsB => 0xFEFF,
      //    Block.HalfwidthandFullwidthForms => 0xFFEF,
      //    Block.Specials => 0xFFFF,
      //    Block.LinearBSyllabary => 0x1007F,
      //    Block.LinearBIdeograms => 0x100FF,
      //    Block.AegeanNumbers => 0x1013F,
      //    Block.AncientGreekNumbers => 0x1018F,
      //    Block.AncientSymbols => 0x101CF,
      //    Block.PhaistosDisc => 0x101FF,
      //    Block.Lycian => 0x1029F,
      //    Block.Carian => 0x102DF,
      //    Block.CopticEpactNumbers => 0x102FF,
      //    Block.OldItalic => 0x1032F,
      //    Block.Gothic => 0x1034F,
      //    Block.OldPermic => 0x1037F,
      //    Block.Ugaritic => 0x1039F,
      //    Block.OldPersian => 0x103DF,
      //    Block.Deseret => 0x1044F,
      //    Block.Shavian => 0x1047F,
      //    Block.Osmanya => 0x104AF,
      //    Block.Osage => 0x104FF,
      //    Block.Elbasan => 0x1052F,
      //    Block.CaucasianAlbanian => 0x1056F,
      //    Block.LinearA => 0x1077F,
      //    Block.CypriotSyllabary => 0x1083F,
      //    Block.ImperialAramaic => 0x1085F,
      //    Block.Palmyrene => 0x1087F,
      //    Block.Nabataean => 0x108AF,
      //    Block.Hatran => 0x108FF,
      //    Block.Phoenician => 0x1091F,
      //    Block.Lydian => 0x1093F,
      //    Block.MeroiticHieroglyphs => 0x1099F,
      //    Block.MeroiticCursive => 0x109FF,
      //    Block.Kharoshthi => 0x10A5F,
      //    Block.OldSouthArabian => 0x10A7F,
      //    Block.OldNorthArabian => 0x10A9F,
      //    Block.Manichaean => 0x10AFF,
      //    Block.Avestan => 0x10B3F,
      //    Block.InscriptionalParthian => 0x10B5F,
      //    Block.InscriptionalPahlavi => 0x10B7F,
      //    Block.PsalterPahlavi => 0x10BAF,
      //    Block.OldTurkic => 0x10C4F,
      //    Block.OldHungarian => 0x10CFF,
      //    Block.HanifiRohingya => 0x10D3F,
      //    Block.RumiNumeralSymbols => 0x10E7F,
      //    Block.OldSogdian => 0x10F2F,
      //    Block.Sogdian => 0x10F6F,
      //    Block.Elymaic => 0x10FFF,
      //    Block.Brahmi => 0x1107F,
      //    Block.Kaithi => 0x110CF,
      //    Block.SoraSompeng => 0x110FF,
      //    Block.Chakma => 0x1114F,
      //    Block.Mahajani => 0x1117F,
      //    Block.Sharada => 0x111DF,
      //    Block.SinhalaArchaicNumbers => 0x111FF,
      //    Block.Khojki => 0x1124F,
      //    Block.Multani => 0x112AF,
      //    Block.Khudawadi => 0x112FF,
      //    Block.Grantha => 0x1137F,
      //    Block.Newa => 0x1147F,
      //    Block.Tirhuta => 0x114DF,
      //    Block.Siddham => 0x115FF,
      //    Block.Modi => 0x1165F,
      //    Block.MongolianSupplement => 0x1167F,
      //    Block.Takri => 0x116CF,
      //    Block.Ahom => 0x1173F,
      //    Block.Dogra => 0x1184F,
      //    Block.WarangCiti => 0x118FF,
      //    Block.Nandinagari => 0x119FF,
      //    Block.ZanabazarSquare => 0x11A4F,
      //    Block.Soyombo => 0x11AAF,
      //    Block.PauCinHau => 0x11AFF,
      //    Block.Bhaiksuki => 0x11C6F,
      //    Block.Marchen => 0x11CBF,
      //    Block.MasaramGondi => 0x11D5F,
      //    Block.GunjalaGondi => 0x11DAF,
      //    Block.Makasar => 0x11EFF,
      //    Block.TamilSupplement => 0x11FFF,
      //    Block.Cuneiform => 0x123FF,
      //    Block.CuneiformNumbersandPunctuation => 0x1247F,
      //    Block.EarlyDynasticCuneiform => 0x1254F,
      //    Block.EgyptianHieroglyphs => 0x1342F,
      //    Block.EgyptianHieroglyphFormatControls => 0x1343F,
      //    Block.AnatolianHieroglyphs => 0x1467F,
      //    Block.BamumSupplement => 0x16A3F,
      //    Block.Mro => 0x16A6F,
      //    Block.BassaVah => 0x16AFF,
      //    Block.PahawhHmong => 0x16B8F,
      //    Block.Medefaidrin => 0x16E9F,
      //    Block.Miao => 0x16F9F,
      //    Block.IdeographicSymbolsandPunctuation => 0x16FFF,
      //    Block.Tangut => 0x187FF,
      //    Block.TangutComponents => 0x18AFF,
      //    Block.KanaSupplement => 0x1B0FF,
      //    Block.KanaExtendedA => 0x1B12F,
      //    Block.SmallKanaExtension => 0x1B16F,
      //    Block.Nushu => 0x1B2FF,
      //    Block.Duployan => 0x1BC9F,
      //    Block.ShorthandFormatControls => 0x1BCAF,
      //    Block.ByzantineMusicalSymbols => 0x1D0FF,
      //    Block.MusicalSymbols => 0x1D1FF,
      //    Block.AncientGreekMusicalNotation => 0x1D24F,
      //    Block.MayanNumerals => 0x1D2FF,
      //    Block.TaiXuanJingSymbols => 0x1D35F,
      //    Block.CountingRodNumerals => 0x1D37F,
      //    Block.MathematicalAlphanumericSymbols => 0x1D7FF,
      //    Block.SuttonSignWriting => 0x1DAAF,
      //    Block.GlagoliticSupplement => 0x1E02F,
      //    Block.NyiakengPuachueHmong => 0x1E14F,
      //    Block.Wancho => 0x1E2FF,
      //    Block.MendeKikakui => 0x1E8DF,
      //    Block.Adlam => 0x1E95F,
      //    Block.IndicSiyaqNumbers => 0x1ECBF,
      //    Block.OttomanSiyaqNumbers => 0x1ED4F,
      //    Block.ArabicMathematicalAlphabeticSymbols => 0x1EEFF,
      //    Block.MahjongTiles => 0x1F02F,
      //    Block.DominoTiles => 0x1F09F,
      //    Block.PlayingCards => 0x1F0FF,
      //    Block.EnclosedAlphanumericSupplement => 0x1F1FF,
      //    Block.EnclosedIdeographicSupplement => 0x1F2FF,
      //    Block.MiscellaneousSymbolsandPictographs => 0x1F5FF,
      //    Block.Emoticons => 0x1F64F,
      //    Block.OrnamentalDingbats => 0x1F67F,
      //    Block.TransportandMapSymbols => 0x1F6FF,
      //    Block.AlchemicalSymbols => 0x1F77F,
      //    Block.GeometricShapesExtended => 0x1F7FF,
      //    Block.SupplementalArrowsC => 0x1F8FF,
      //    Block.SupplementalSymbolsandPictographs => 0x1F9FF,
      //    Block.ChessSymbols => 0x1FA6F,
      //    Block.SymbolsandPictographsExtendedA => 0x1FAFF,
      //    Block.CJKUnifiedIdeographsExtensionB => 0x2A6DF,
      //    Block.CJKUnifiedIdeographsExtensionC => 0x2B73F,
      //    Block.CJKUnifiedIdeographsExtensionD => 0x2B81F,
      //    Block.CJKUnifiedIdeographsExtensionE => 0x2CEAF,
      //    Block.CJKUnifiedIdeographsExtensionF => 0x2EBEF,
      //    Block.CJKCompatibilityIdeographsSupplement => 0x2FA1F,
      //    Block.Tags => 0xE007F,
      //    Block.VariationSelectorsSupplement => 0xE01EF,
      //    Block.SupplementaryPrivateUseAreaA => 0xFFFFF,
      //    Block.SupplementaryPrivateUseAreaB => 0x10FFFF,
      //    _ => throw new System.ArgumentOutOfRangeException(nameof(source))
      //  };

      //#region Unicode Blocks
      //public enum Block
      //{
      //  #region All Unicode Blocks
      //  BasicLatin,
      //  Latin1Supplement,
      //  LatinExtendedA,
      //  LatinExtendedB,
      //  IPAExtensions,
      //  SpacingModifierLetters,
      //  CombiningDiacriticalMarks,
      //  GreekandCoptic,
      //  Cyrillic,
      //  CyrillicSupplement,
      //  Armenian,
      //  Hebrew,
      //  Arabic,
      //  Syriac,
      //  ArabicSupplement,
      //  Thaana,
      //  NKo,
      //  Samaritan,
      //  Mandaic,
      //  SyriacSupplement,
      //  ArabicExtendedA,
      //  Devanagari,
      //  Bengali,
      //  Gurmukhi,
      //  Gujarati,
      //  Oriya,
      //  Tamil,
      //  Telugu,
      //  Kannada,
      //  Malayalam,
      //  Sinhala,
      //  Thai,
      //  Lao,
      //  Tibetan,
      //  Myanmar,
      //  Georgian,
      //  HangulJamo,
      //  Ethiopic,
      //  EthiopicSupplement,
      //  Cherokee,
      //  UnifiedCanadianAboriginalSyllabics,
      //  Ogham,
      //  Runic,
      //  Tagalog,
      //  Hanunoo,
      //  Buhid,
      //  Tagbanwa,
      //  Khmer,
      //  Mongolian,
      //  UnifiedCanadianAboriginalSyllabicsExtended,
      //  Limbu,
      //  TaiLe,
      //  NewTaiLue,
      //  KhmerSymbols,
      //  Buginese,
      //  TaiTham,
      //  CombiningDiacriticalMarksExtended,
      //  Balinese,
      //  Sundanese,
      //  Batak,
      //  Lepcha,
      //  OlChiki,
      //  CyrillicExtendedC,
      //  GeorgianExtended,
      //  SundaneseSupplement,
      //  VedicExtensions,
      //  PhoneticExtensions,
      //  PhoneticExtensionsSupplement,
      //  CombiningDiacriticalMarksSupplement,
      //  LatinExtendedAdditional,
      //  GreekExtended,
      //  GeneralPunctuation,
      //  SuperscriptsandSubscripts,
      //  CurrencySymbols,
      //  CombiningDiacriticalMarksforSymbols,
      //  LetterlikeSymbols,
      //  NumberForms,
      //  Arrows,
      //  MathematicalOperators,
      //  MiscellaneousTechnical,
      //  ControlPictures,
      //  OpticalCharacterRecognition,
      //  EnclosedAlphanumerics,
      //  BoxDrawing,
      //  BlockElements,
      //  GeometricShapes,
      //  MiscellaneousSymbols,
      //  Dingbats,
      //  MiscellaneousMathematicalSymbolsA,
      //  SupplementalArrowsA,
      //  BraillePatterns,
      //  SupplementalArrowsB,
      //  MiscellaneousMathematicalSymbolsB,
      //  SupplementalMathematicalOperators,
      //  MiscellaneousSymbolsandArrows,
      //  Glagolitic,
      //  LatinExtendedC,
      //  Coptic,
      //  GeorgianSupplement,
      //  Tifinagh,
      //  EthiopicExtended,
      //  CyrillicExtendedA,
      //  SupplementalPunctuation,
      //  CJKRadicalsSupplement,
      //  KangxiRadicals,
      //  IdeographicDescriptionCharacters,
      //  CJKSymbolsandPunctuation,
      //  Hiragana,
      //  Katakana,
      //  Bopomofo,
      //  HangulCompatibilityJamo,
      //  Kanbun,
      //  BopomofoExtended,
      //  CJKStrokes,
      //  KatakanaPhoneticExtensions,
      //  EnclosedCJKLettersandMonths,
      //  CJKCompatibility,
      //  CJKUnifiedIdeographsExtensionA,
      //  YijingHexagramSymbols,
      //  CJKUnifiedIdeographs,
      //  YiSyllables,
      //  YiRadicals,
      //  Lisu,
      //  Vai,
      //  CyrillicExtendedB,
      //  Bamum,
      //  ModifierToneLetters,
      //  LatinExtendedD,
      //  SylotiNagri,
      //  CommonIndicNumberForms,
      //  Phagspa,
      //  Saurashtra,
      //  DevanagariExtended,
      //  KayahLi,
      //  Rejang,
      //  HangulJamoExtendedA,
      //  Javanese,
      //  MyanmarExtendedB,
      //  Cham,
      //  MyanmarExtendedA,
      //  TaiViet,
      //  MeeteiMayekExtensions,
      //  EthiopicExtendedA,
      //  LatinExtendedE,
      //  CherokeeSupplement,
      //  MeeteiMayek,
      //  HangulSyllables,
      //  HangulJamoExtendedB,
      //  HighSurrogates,
      //  HighPrivateUseSurrogates,
      //  LowSurrogates,
      //  PrivateUseArea,
      //  CJKCompatibilityIdeographs,
      //  AlphabeticPresentationForms,
      //  ArabicPresentationFormsA,
      //  VariationSelectors,
      //  VerticalForms,
      //  CombiningHalfMarks,
      //  CJKCompatibilityForms,
      //  SmallFormVariants,
      //  ArabicPresentationFormsB,
      //  HalfwidthandFullwidthForms,
      //  Specials,
      //  LinearBSyllabary,
      //  LinearBIdeograms,
      //  AegeanNumbers,
      //  AncientGreekNumbers,
      //  AncientSymbols,
      //  PhaistosDisc,
      //  Lycian,
      //  Carian,
      //  CopticEpactNumbers,
      //  OldItalic,
      //  Gothic,
      //  OldPermic,
      //  Ugaritic,
      //  OldPersian,
      //  Deseret,
      //  Shavian,
      //  Osmanya,
      //  Osage,
      //  Elbasan,
      //  CaucasianAlbanian,
      //  LinearA,
      //  CypriotSyllabary,
      //  ImperialAramaic,
      //  Palmyrene,
      //  Nabataean,
      //  Hatran,
      //  Phoenician,
      //  Lydian,
      //  MeroiticHieroglyphs,
      //  MeroiticCursive,
      //  Kharoshthi,
      //  OldSouthArabian,
      //  OldNorthArabian,
      //  Manichaean,
      //  Avestan,
      //  InscriptionalParthian,
      //  InscriptionalPahlavi,
      //  PsalterPahlavi,
      //  OldTurkic,
      //  OldHungarian,
      //  HanifiRohingya,
      //  RumiNumeralSymbols,
      //  OldSogdian,
      //  Sogdian,
      //  Elymaic,
      //  Brahmi,
      //  Kaithi,
      //  SoraSompeng,
      //  Chakma,
      //  Mahajani,
      //  Sharada,
      //  SinhalaArchaicNumbers,
      //  Khojki,
      //  Multani,
      //  Khudawadi,
      //  Grantha,
      //  Newa,
      //  Tirhuta,
      //  Siddham,
      //  Modi,
      //  MongolianSupplement,
      //  Takri,
      //  Ahom,
      //  Dogra,
      //  WarangCiti,
      //  Nandinagari,
      //  ZanabazarSquare,
      //  Soyombo,
      //  PauCinHau,
      //  Bhaiksuki,
      //  Marchen,
      //  MasaramGondi,
      //  GunjalaGondi,
      //  Makasar,
      //  TamilSupplement,
      //  Cuneiform,
      //  CuneiformNumbersandPunctuation,
      //  EarlyDynasticCuneiform,
      //  EgyptianHieroglyphs,
      //  EgyptianHieroglyphFormatControls,
      //  AnatolianHieroglyphs,
      //  BamumSupplement,
      //  Mro,
      //  BassaVah,
      //  PahawhHmong,
      //  Medefaidrin,
      //  Miao,
      //  IdeographicSymbolsandPunctuation,
      //  Tangut,
      //  TangutComponents,
      //  KanaSupplement,
      //  KanaExtendedA,
      //  SmallKanaExtension,
      //  Nushu,
      //  Duployan,
      //  ShorthandFormatControls,
      //  ByzantineMusicalSymbols,
      //  MusicalSymbols,
      //  AncientGreekMusicalNotation,
      //  MayanNumerals,
      //  TaiXuanJingSymbols,
      //  CountingRodNumerals,
      //  MathematicalAlphanumericSymbols,
      //  SuttonSignWriting,
      //  GlagoliticSupplement,
      //  NyiakengPuachueHmong,
      //  Wancho,
      //  MendeKikakui,
      //  Adlam,
      //  IndicSiyaqNumbers,
      //  OttomanSiyaqNumbers,
      //  ArabicMathematicalAlphabeticSymbols,
      //  MahjongTiles,
      //  DominoTiles,
      //  PlayingCards,
      //  EnclosedAlphanumericSupplement,
      //  EnclosedIdeographicSupplement,
      //  MiscellaneousSymbolsandPictographs,
      //  Emoticons,
      //  OrnamentalDingbats,
      //  TransportandMapSymbols,
      //  AlchemicalSymbols,
      //  GeometricShapesExtended,
      //  SupplementalArrowsC,
      //  SupplementalSymbolsandPictographs,
      //  ChessSymbols,
      //  SymbolsandPictographsExtendedA,
      //  CJKUnifiedIdeographsExtensionB,
      //  CJKUnifiedIdeographsExtensionC,
      //  CJKUnifiedIdeographsExtensionD,
      //  CJKUnifiedIdeographsExtensionE,
      //  CJKUnifiedIdeographsExtensionF,
      //  CJKCompatibilityIdeographsSupplement,
      //  Tags,
      //  VariationSelectorsSupplement,
      //  SupplementaryPrivateUseAreaA,
      //  SupplementaryPrivateUseAreaB
      //  #endregion All Unicode Blocks
      //}

      //public static System.Collections.Generic.IEnumerable<(Block, int firstCodeUnit, int lastCodeUnit)> GetBlockRanges()
      //{
      //  yield return (Block.BasicLatin, 0x00, 0x7F);
      //  yield return (Block.Latin1Supplement, 0x80, 0xFF);
      //  yield return (Block.LatinExtendedA, 0x100, 0x17F);
      //  yield return (Block.LatinExtendedB, 0x180, 0x24F);
      //  yield return (Block.IPAExtensions, 0x250, 0x2AF);
      //  yield return (Block.SpacingModifierLetters, 0x2B0, 0x2FF);
      //  yield return (Block.CombiningDiacriticalMarks, 0x300, 0x36F);
      //  yield return (Block.GreekandCoptic, 0x370, 0x3FF);
      //  yield return (Block.Cyrillic, 0x400, 0x4FF);
      //  yield return (Block.CyrillicSupplement, 0x500, 0x52F);
      //  yield return (Block.Armenian, 0x530, 0x58F);
      //  yield return (Block.Hebrew, 0x590, 0x5FF);
      //  yield return (Block.Arabic, 0x600, 0x6FF);
      //  yield return (Block.Syriac, 0x700, 0x74F);
      //  yield return (Block.ArabicSupplement, 0x750, 0x77F);
      //  yield return (Block.Thaana, 0x780, 0x7BF);
      //  yield return (Block.NKo, 0x7C0, 0x7FF);
      //  yield return (Block.Samaritan, 0x800, 0x83F);
      //  yield return (Block.Mandaic, 0x840, 0x85F);
      //  yield return (Block.SyriacSupplement, 0x860, 0x86F);
      //  yield return (Block.ArabicExtendedA, 0x8A0, 0x8FF);
      //  yield return (Block.Devanagari, 0x900, 0x97F);
      //  yield return (Block.Bengali, 0x980, 0x9FF);
      //  yield return (Block.Gurmukhi, 0xA00, 0xA7F);
      //  yield return (Block.Gujarati, 0xA80, 0xAFF);
      //  yield return (Block.Oriya, 0xB00, 0xB7F);
      //  yield return (Block.Tamil, 0xB80, 0xBFF);
      //  yield return (Block.Telugu, 0xC00, 0xC7F);
      //  yield return (Block.Kannada, 0xC80, 0xCFF);
      //  yield return (Block.Malayalam, 0xD00, 0xD7F);
      //  yield return (Block.Sinhala, 0xD80, 0xDFF);
      //  yield return (Block.Thai, 0xE00, 0xE7F);
      //  yield return (Block.Lao, 0xE80, 0xEFF);
      //  yield return (Block.Tibetan, 0xF00, 0xFFF);
      //  yield return (Block.Myanmar, 0x1000, 0x109F);
      //  yield return (Block.Georgian, 0x10A0, 0x10FF);
      //  yield return (Block.HangulJamo, 0x1100, 0x11FF);
      //  yield return (Block.Ethiopic, 0x1200, 0x137F);
      //  yield return (Block.EthiopicSupplement, 0x1380, 0x139F);
      //  yield return (Block.Cherokee, 0x13A0, 0x13FF);
      //  yield return (Block.UnifiedCanadianAboriginalSyllabics, 0x1400, 0x167F);
      //  yield return (Block.Ogham, 0x1680, 0x169F);
      //  yield return (Block.Runic, 0x16A0, 0x16FF);
      //  yield return (Block.Tagalog, 0x1700, 0x171F);
      //  yield return (Block.Hanunoo, 0x1720, 0x173F);
      //  yield return (Block.Buhid, 0x1740, 0x175F);
      //  yield return (Block.Tagbanwa, 0x1760, 0x177F);
      //  yield return (Block.Khmer, 0x1780, 0x17FF);
      //  yield return (Block.Mongolian, 0x1800, 0x18AF);
      //  yield return (Block.UnifiedCanadianAboriginalSyllabicsExtended, 0x18B0, 0x18FF);
      //  yield return (Block.Limbu, 0x1900, 0x194F);
      //  yield return (Block.TaiLe, 0x1950, 0x197F);
      //  yield return (Block.NewTaiLue, 0x1980, 0x19DF);
      //  yield return (Block.KhmerSymbols, 0x19E0, 0x19FF);
      //  yield return (Block.Buginese, 0x1A00, 0x1A1F);
      //  yield return (Block.TaiTham, 0x1A20, 0x1AAF);
      //  yield return (Block.CombiningDiacriticalMarksExtended, 0x1AB0, 0x1AFF);
      //  yield return (Block.Balinese, 0x1B00, 0x1B7F);
      //  yield return (Block.Sundanese, 0x1B80, 0x1BBF);
      //  yield return (Block.Batak, 0x1BC0, 0x1BFF);
      //  yield return (Block.Lepcha, 0x1C00, 0x1C4F);
      //  yield return (Block.OlChiki, 0x1C50, 0x1C7F);
      //  yield return (Block.CyrillicExtendedC, 0x1C80, 0x1C8F);
      //  yield return (Block.GeorgianExtended, 0x1C90, 0x1CBF);
      //  yield return (Block.SundaneseSupplement, 0x1CC0, 0x1CCF);
      //  yield return (Block.VedicExtensions, 0x1CD0, 0x1CFF);
      //  yield return (Block.PhoneticExtensions, 0x1D00, 0x1D7F);
      //  yield return (Block.PhoneticExtensionsSupplement, 0x1D80, 0x1DBF);
      //  yield return (Block.CombiningDiacriticalMarksSupplement, 0x1DC0, 0x1DFF);
      //  yield return (Block.LatinExtendedAdditional, 0x1E00, 0x1EFF);
      //  yield return (Block.GreekExtended, 0x1F00, 0x1FFF);
      //  yield return (Block.GeneralPunctuation, 0x2000, 0x206F);
      //  yield return (Block.SuperscriptsandSubscripts, 0x2070, 0x209F);
      //  yield return (Block.CurrencySymbols, 0x20A0, 0x20CF);
      //  yield return (Block.CombiningDiacriticalMarksforSymbols, 0x20D0, 0x20FF);
      //  yield return (Block.LetterlikeSymbols, 0x2100, 0x214F);
      //  yield return (Block.NumberForms, 0x2150, 0x218F);
      //  yield return (Block.Arrows, 0x2190, 0x21FF);
      //  yield return (Block.MathematicalOperators, 0x2200, 0x22FF);
      //  yield return (Block.MiscellaneousTechnical, 0x2300, 0x23FF);
      //  yield return (Block.ControlPictures, 0x2400, 0x243F);
      //  yield return (Block.OpticalCharacterRecognition, 0x2440, 0x245F);
      //  yield return (Block.EnclosedAlphanumerics, 0x2460, 0x24FF);
      //  yield return (Block.BoxDrawing, 0x2500, 0x257F);
      //  yield return (Block.BlockElements, 0x2580, 0x259F);
      //  yield return (Block.GeometricShapes, 0x25A0, 0x25FF);
      //  yield return (Block.MiscellaneousSymbols, 0x2600, 0x26FF);
      //  yield return (Block.Dingbats, 0x2700, 0x27BF);
      //  yield return (Block.MiscellaneousMathematicalSymbolsA, 0x27C0, 0x27EF);
      //  yield return (Block.SupplementalArrowsA, 0x27F0, 0x27FF);
      //  yield return (Block.BraillePatterns, 0x2800, 0x28FF);
      //  yield return (Block.SupplementalArrowsB, 0x2900, 0x297F);
      //  yield return (Block.MiscellaneousMathematicalSymbolsB, 0x2980, 0x29FF);
      //  yield return (Block.SupplementalMathematicalOperators, 0x2A00, 0x2AFF);
      //  yield return (Block.MiscellaneousSymbolsandArrows, 0x2B00, 0x2BFF);
      //  yield return (Block.Glagolitic, 0x2C00, 0x2C5F);
      //  yield return (Block.LatinExtendedC, 0x2C60, 0x2C7F);
      //  yield return (Block.Coptic, 0x2C80, 0x2CFF);
      //  yield return (Block.GeorgianSupplement, 0x2D00, 0x2D2F);
      //  yield return (Block.Tifinagh, 0x2D30, 0x2D7F);
      //  yield return (Block.EthiopicExtended, 0x2D80, 0x2DDF);
      //  yield return (Block.CyrillicExtendedA, 0x2DE0, 0x2DFF);
      //  yield return (Block.SupplementalPunctuation, 0x2E00, 0x2E7F);
      //  yield return (Block.CJKRadicalsSupplement, 0x2E80, 0x2EFF);
      //  yield return (Block.KangxiRadicals, 0x2F00, 0x2FDF);
      //  yield return (Block.IdeographicDescriptionCharacters, 0x2FF0, 0x2FFF);
      //  yield return (Block.CJKSymbolsandPunctuation, 0x3000, 0x303F);
      //  yield return (Block.Hiragana, 0x3040, 0x309F);
      //  yield return (Block.Katakana, 0x30A0, 0x30FF);
      //  yield return (Block.Bopomofo, 0x3100, 0x312F);
      //  yield return (Block.HangulCompatibilityJamo, 0x3130, 0x318F);
      //  yield return (Block.Kanbun, 0x3190, 0x319F);
      //  yield return (Block.BopomofoExtended, 0x31A0, 0x31BF);
      //  yield return (Block.CJKStrokes, 0x31C0, 0x31EF);
      //  yield return (Block.KatakanaPhoneticExtensions, 0x31F0, 0x31FF);
      //  yield return (Block.EnclosedCJKLettersandMonths, 0x3200, 0x32FF);
      //  yield return (Block.CJKCompatibility, 0x3300, 0x33FF);
      //  yield return (Block.CJKUnifiedIdeographsExtensionA, 0x3400, 0x4DBF);
      //  yield return (Block.YijingHexagramSymbols, 0x4DC0, 0x4DFF);
      //  yield return (Block.CJKUnifiedIdeographs, 0x4E00, 0x9FFF);
      //  yield return (Block.YiSyllables, 0xA000, 0xA48F);
      //  yield return (Block.YiRadicals, 0xA490, 0xA4CF);
      //  yield return (Block.Lisu, 0xA4D0, 0xA4FF);
      //  yield return (Block.Vai, 0xA500, 0xA63F);
      //  yield return (Block.CyrillicExtendedB, 0xA640, 0xA69F);
      //  yield return (Block.Bamum, 0xA6A0, 0xA6FF);
      //  yield return (Block.ModifierToneLetters, 0xA700, 0xA71F);
      //  yield return (Block.LatinExtendedD, 0xA720, 0xA7FF);
      //  yield return (Block.SylotiNagri, 0xA800, 0xA82F);
      //  yield return (Block.CommonIndicNumberForms, 0xA830, 0xA83F);
      //  yield return (Block.Phagspa, 0xA840, 0xA87F);
      //  yield return (Block.Saurashtra, 0xA880, 0xA8DF);
      //  yield return (Block.DevanagariExtended, 0xA8E0, 0xA8FF);
      //  yield return (Block.KayahLi, 0xA900, 0xA92F);
      //  yield return (Block.Rejang, 0xA930, 0xA95F);
      //  yield return (Block.HangulJamoExtendedA, 0xA960, 0xA97F);
      //  yield return (Block.Javanese, 0xA980, 0xA9DF);
      //  yield return (Block.MyanmarExtendedB, 0xA9E0, 0xA9FF);
      //  yield return (Block.Cham, 0xAA00, 0xAA5F);
      //  yield return (Block.MyanmarExtendedA, 0xAA60, 0xAA7F);
      //  yield return (Block.TaiViet, 0xAA80, 0xAADF);
      //  yield return (Block.MeeteiMayekExtensions, 0xAAE0, 0xAAFF);
      //  yield return (Block.EthiopicExtendedA, 0xAB00, 0xAB2F);
      //  yield return (Block.LatinExtendedE, 0xAB30, 0xAB6F);
      //  yield return (Block.CherokeeSupplement, 0xAB70, 0xABBF);
      //  yield return (Block.MeeteiMayek, 0xABC0, 0xABFF);
      //  yield return (Block.HangulSyllables, 0xAC00, 0xD7AF);
      //  yield return (Block.HangulJamoExtendedB, 0xD7B0, 0xD7FF);
      //  yield return (Block.HighSurrogates, 0xD800, 0xDB7F);
      //  yield return (Block.HighPrivateUseSurrogates, 0xDB80, 0xDBFF);
      //  yield return (Block.LowSurrogates, 0xDC00, 0xDFFF);
      //  yield return (Block.PrivateUseArea, 0xE000, 0xF8FF);
      //  yield return (Block.CJKCompatibilityIdeographs, 0xF900, 0xFAFF);
      //  yield return (Block.AlphabeticPresentationForms, 0xFB00, 0xFB4F);
      //  yield return (Block.ArabicPresentationFormsA, 0xFB50, 0xFDFF);
      //  yield return (Block.VariationSelectors, 0xFE00, 0xFE0F);
      //  yield return (Block.VerticalForms, 0xFE10, 0xFE1F);
      //  yield return (Block.CombiningHalfMarks, 0xFE20, 0xFE2F);
      //  yield return (Block.CJKCompatibilityForms, 0xFE30, 0xFE4F);
      //  yield return (Block.SmallFormVariants, 0xFE50, 0xFE6F);
      //  yield return (Block.ArabicPresentationFormsB, 0xFE70, 0xFEFF);
      //  yield return (Block.HalfwidthandFullwidthForms, 0xFF00, 0xFFEF);
      //  yield return (Block.Specials, 0xFFF0, 0xFFFF);
      //  yield return (Block.LinearBSyllabary, 0x10000, 0x1007F);
      //  yield return (Block.LinearBIdeograms, 0x10080, 0x100FF);
      //  yield return (Block.AegeanNumbers, 0x10100, 0x1013F);
      //  yield return (Block.AncientGreekNumbers, 0x10140, 0x1018F);
      //  yield return (Block.AncientSymbols, 0x10190, 0x101CF);
      //  yield return (Block.PhaistosDisc, 0x101D0, 0x101FF);
      //  yield return (Block.Lycian, 0x10280, 0x1029F);
      //  yield return (Block.Carian, 0x102A0, 0x102DF);
      //  yield return (Block.CopticEpactNumbers, 0x102E0, 0x102FF);
      //  yield return (Block.OldItalic, 0x10300, 0x1032F);
      //  yield return (Block.Gothic, 0x10330, 0x1034F);
      //  yield return (Block.OldPermic, 0x10350, 0x1037F);
      //  yield return (Block.Ugaritic, 0x10380, 0x1039F);
      //  yield return (Block.OldPersian, 0x103A0, 0x103DF);
      //  yield return (Block.Deseret, 0x10400, 0x1044F);
      //  yield return (Block.Shavian, 0x10450, 0x1047F);
      //  yield return (Block.Osmanya, 0x10480, 0x104AF);
      //  yield return (Block.Osage, 0x104B0, 0x104FF);
      //  yield return (Block.Elbasan, 0x10500, 0x1052F);
      //  yield return (Block.CaucasianAlbanian, 0x10530, 0x1056F);
      //  yield return (Block.LinearA, 0x10600, 0x1077F);
      //  yield return (Block.CypriotSyllabary, 0x10800, 0x1083F);
      //  yield return (Block.ImperialAramaic, 0x10840, 0x1085F);
      //  yield return (Block.Palmyrene, 0x10860, 0x1087F);
      //  yield return (Block.Nabataean, 0x10880, 0x108AF);
      //  yield return (Block.Hatran, 0x108E0, 0x108FF);
      //  yield return (Block.Phoenician, 0x10900, 0x1091F);
      //  yield return (Block.Lydian, 0x10920, 0x1093F);
      //  yield return (Block.MeroiticHieroglyphs, 0x10980, 0x1099F);
      //  yield return (Block.MeroiticCursive, 0x109A0, 0x109FF);
      //  yield return (Block.Kharoshthi, 0x10A00, 0x10A5F);
      //  yield return (Block.OldSouthArabian, 0x10A60, 0x10A7F);
      //  yield return (Block.OldNorthArabian, 0x10A80, 0x10A9F);
      //  yield return (Block.Manichaean, 0x10AC0, 0x10AFF);
      //  yield return (Block.Avestan, 0x10B00, 0x10B3F);
      //  yield return (Block.InscriptionalParthian, 0x10B40, 0x10B5F);
      //  yield return (Block.InscriptionalPahlavi, 0x10B60, 0x10B7F);
      //  yield return (Block.PsalterPahlavi, 0x10B80, 0x10BAF);
      //  yield return (Block.OldTurkic, 0x10C00, 0x10C4F);
      //  yield return (Block.OldHungarian, 0x10C80, 0x10CFF);
      //  yield return (Block.HanifiRohingya, 0x10D00, 0x10D3F);
      //  yield return (Block.RumiNumeralSymbols, 0x10E60, 0x10E7F);
      //  yield return (Block.OldSogdian, 0x10F00, 0x10F2F);
      //  yield return (Block.Sogdian, 0x10F30, 0x10F6F);
      //  yield return (Block.Elymaic, 0x10FE0, 0x10FFF);
      //  yield return (Block.Brahmi, 0x11000, 0x1107F);
      //  yield return (Block.Kaithi, 0x11080, 0x110CF);
      //  yield return (Block.SoraSompeng, 0x110D0, 0x110FF);
      //  yield return (Block.Chakma, 0x11100, 0x1114F);
      //  yield return (Block.Mahajani, 0x11150, 0x1117F);
      //  yield return (Block.Sharada, 0x11180, 0x111DF);
      //  yield return (Block.SinhalaArchaicNumbers, 0x111E0, 0x111FF);
      //  yield return (Block.Khojki, 0x11200, 0x1124F);
      //  yield return (Block.Multani, 0x11280, 0x112AF);
      //  yield return (Block.Khudawadi, 0x112B0, 0x112FF);
      //  yield return (Block.Grantha, 0x11300, 0x1137F);
      //  yield return (Block.Newa, 0x11400, 0x1147F);
      //  yield return (Block.Tirhuta, 0x11480, 0x114DF);
      //  yield return (Block.Siddham, 0x11580, 0x115FF);
      //  yield return (Block.Modi, 0x11600, 0x1165F);
      //  yield return (Block.MongolianSupplement, 0x11660, 0x1167F);
      //  yield return (Block.Takri, 0x11680, 0x116CF);
      //  yield return (Block.Ahom, 0x11700, 0x1173F);
      //  yield return (Block.Dogra, 0x11800, 0x1184F);
      //  yield return (Block.WarangCiti, 0x118A0, 0x118FF);
      //  yield return (Block.Nandinagari, 0x119A0, 0x119FF);
      //  yield return (Block.ZanabazarSquare, 0x11A00, 0x11A4F);
      //  yield return (Block.Soyombo, 0x11A50, 0x11AAF);
      //  yield return (Block.PauCinHau, 0x11AC0, 0x11AFF);
      //  yield return (Block.Bhaiksuki, 0x11C00, 0x11C6F);
      //  yield return (Block.Marchen, 0x11C70, 0x11CBF);
      //  yield return (Block.MasaramGondi, 0x11D00, 0x11D5F);
      //  yield return (Block.GunjalaGondi, 0x11D60, 0x11DAF);
      //  yield return (Block.Makasar, 0x11EE0, 0x11EFF);
      //  yield return (Block.TamilSupplement, 0x11FC0, 0x11FFF);
      //  yield return (Block.Cuneiform, 0x12000, 0x123FF);
      //  yield return (Block.CuneiformNumbersandPunctuation, 0x12400, 0x1247F);
      //  yield return (Block.EarlyDynasticCuneiform, 0x12480, 0x1254F);
      //  yield return (Block.EgyptianHieroglyphs, 0x13000, 0x1342F);
      //  yield return (Block.EgyptianHieroglyphFormatControls, 0x13430, 0x1343F);
      //  yield return (Block.AnatolianHieroglyphs, 0x14400, 0x1467F);
      //  yield return (Block.BamumSupplement, 0x16800, 0x16A3F);
      //  yield return (Block.Mro, 0x16A40, 0x16A6F);
      //  yield return (Block.BassaVah, 0x16AD0, 0x16AFF);
      //  yield return (Block.PahawhHmong, 0x16B00, 0x16B8F);
      //  yield return (Block.Medefaidrin, 0x16E40, 0x16E9F);
      //  yield return (Block.Miao, 0x16F00, 0x16F9F);
      //  yield return (Block.IdeographicSymbolsandPunctuation, 0x16FE0, 0x16FFF);
      //  yield return (Block.Tangut, 0x17000, 0x187FF);
      //  yield return (Block.TangutComponents, 0x18800, 0x18AFF);
      //  yield return (Block.KanaSupplement, 0x1B000, 0x1B0FF);
      //  yield return (Block.KanaExtendedA, 0x1B100, 0x1B12F);
      //  yield return (Block.SmallKanaExtension, 0x1B130, 0x1B16F);
      //  yield return (Block.Nushu, 0x1B170, 0x1B2FF);
      //  yield return (Block.Duployan, 0x1BC00, 0x1BC9F);
      //  yield return (Block.ShorthandFormatControls, 0x1BCA0, 0x1BCAF);
      //  yield return (Block.ByzantineMusicalSymbols, 0x1D000, 0x1D0FF);
      //  yield return (Block.MusicalSymbols, 0x1D100, 0x1D1FF);
      //  yield return (Block.AncientGreekMusicalNotation, 0x1D200, 0x1D24F);
      //  yield return (Block.MayanNumerals, 0x1D2E0, 0x1D2FF);
      //  yield return (Block.TaiXuanJingSymbols, 0x1D300, 0x1D35F);
      //  yield return (Block.CountingRodNumerals, 0x1D360, 0x1D37F);
      //  yield return (Block.MathematicalAlphanumericSymbols, 0x1D400, 0x1D7FF);
      //  yield return (Block.SuttonSignWriting, 0x1D800, 0x1DAAF);
      //  yield return (Block.GlagoliticSupplement, 0x1E000, 0x1E02F);
      //  yield return (Block.NyiakengPuachueHmong, 0x1E100, 0x1E14F);
      //  yield return (Block.Wancho, 0x1E2C0, 0x1E2FF);
      //  yield return (Block.MendeKikakui, 0x1E800, 0x1E8DF);
      //  yield return (Block.Adlam, 0x1E900, 0x1E95F);
      //  yield return (Block.IndicSiyaqNumbers, 0x1EC70, 0x1ECBF);
      //  yield return (Block.OttomanSiyaqNumbers, 0x1ED00, 0x1ED4F);
      //  yield return (Block.ArabicMathematicalAlphabeticSymbols, 0x1EE00, 0x1EEFF);
      //  yield return (Block.MahjongTiles, 0x1F000, 0x1F02F);
      //  yield return (Block.DominoTiles, 0x1F030, 0x1F09F);
      //  yield return (Block.PlayingCards, 0x1F0A0, 0x1F0FF);
      //  yield return (Block.EnclosedAlphanumericSupplement, 0x1F100, 0x1F1FF);
      //  yield return (Block.EnclosedIdeographicSupplement, 0x1F200, 0x1F2FF);
      //  yield return (Block.MiscellaneousSymbolsandPictographs, 0x1F300, 0x1F5FF);
      //  yield return (Block.Emoticons, 0x1F600, 0x1F64F);
      //  yield return (Block.OrnamentalDingbats, 0x1F650, 0x1F67F);
      //  yield return (Block.TransportandMapSymbols, 0x1F680, 0x1F6FF);
      //  yield return (Block.AlchemicalSymbols, 0x1F700, 0x1F77F);
      //  yield return (Block.GeometricShapesExtended, 0x1F780, 0x1F7FF);
      //  yield return (Block.SupplementalArrowsC, 0x1F800, 0x1F8FF);
      //  yield return (Block.SupplementalSymbolsandPictographs, 0x1F900, 0x1F9FF);
      //  yield return (Block.ChessSymbols, 0x1FA00, 0x1FA6F);
      //  yield return (Block.SymbolsandPictographsExtendedA, 0x1FA70, 0x1FAFF);
      //  yield return (Block.CJKUnifiedIdeographsExtensionB, 0x20000, 0x2A6DF);
      //  yield return (Block.CJKUnifiedIdeographsExtensionC, 0x2A700, 0x2B73F);
      //  yield return (Block.CJKUnifiedIdeographsExtensionD, 0x2B740, 0x2B81F);
      //  yield return (Block.CJKUnifiedIdeographsExtensionE, 0x2B820, 0x2CEAF);
      //  yield return (Block.CJKUnifiedIdeographsExtensionF, 0x2CEB0, 0x2EBEF);
      //  yield return (Block.CJKCompatibilityIdeographsSupplement, 0x2F800, 0x2FA1F);
      //  yield return (Block.Tags, 0xE0000, 0xE007F);
      //  yield return (Block.VariationSelectorsSupplement, 0xE0100, 0xE01EF);
      //  yield return (Block.SupplementaryPrivateUseAreaA, 0xF0000, 0xFFFFF);
      //  yield return (Block.SupplementaryPrivateUseAreaB, 0x100000, 0x10FFFF);
      //}
      //#endregion Unicode Blocks
    }
  }
}
