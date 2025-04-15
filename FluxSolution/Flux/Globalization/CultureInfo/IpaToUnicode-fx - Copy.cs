//namespace Flux
//{
//  public static partial class Fx
//  {
//    public static string[] ConsonantsPulmonic = ["\u0070", "\u0062", "\u0074", "\u0064", "\u0288", "\u0256", "\u0063", "\u025F", "\u006B", "\u0261", "\u0071", "\u0262", "\u0294", "\u006D", "\u0271", "\u006E", "\u0273", "\u0272", "\u014B", "\u0274", "\u0299", "\u0072", "\u0280", "\u027E", "\u027D", "\u0278", "\u03B2", "\u0066", "\u0076", "\u03B8", "\u00F0", "\u0073", "\u007A", "\u0283", "\u0292", "\u0282", "\u0290", "\u00E7", "\u029D", "\u0078", "\u0263", "\u03C7", "\u0281", "\u0127", "\u0295", "\u0068", "\u0266", "\u026C", "\u026E", "\u028B", "\u0279", "\u027B", "\u006A", "\u0270", "\u006C", "\u026D", "\u028E", "\u029F", "\u2C71", "\u026B"];
//    public static string[] ConsonantsNonPulmonic = ["\u0298", "\u01C0", "\u01C3", "\u01C2", "\u01C1", "\u0253", "\u0257", "\u0284", "\u0260", "\u029B", "\u02BC", "\u0070\u02BC", "\u0074\u02BC", "\u006B\u02BC", "\u0073\u02BC"];
//    public static string[] OtherSymbols = ["\u028D", "\u0077", "\u0265", "\u029C", "\u02A2", "\u02A1", "\u0255", "\u0291", "\u027A", "\u0267", "\u035C", "\u0361"];
//    public static string[] Diacritics = ["\u0325", "\u030A", "\u032C", "\u02B0", "\u0339", "\u031C", "\u031F", "\u0320", "\u0308", "\u033D", "\u0329", "\u032F", "\u02DE", "\u0324", "\u0330", "\u033C", "\u02B7", "\u02B2", "\u02E0", "\u02E4", "\u0334", "\u031D", "\u031E", "\u0318", "\u0319", "\u032A", "\u033A", "\u033B", "\u0303", "\u207F", "\u02E1", "\u031A",];
//    public static string[] Vowels = ["\u0069", "\u0079", "\u026A", "\u028F", "\u0065", "\u00F8", "\u025B", "\u0153", "\u00E6", "\u0061", "\u0276", "\u0268", "\u0289", "\u0258", "\u0275", "\u0259", "\u025C", "\u025E", "\u0250", "\u026F", "\u0075", "\u028A", "\u0264", "\u006F", "\u028C", "\u0254", "\u0251", "\u0252", "\u025A"];
//    public static string[] Suprasegmentals = ["\u02C8", "\u02CC", "\u02D0", "\u02D1", "\u0306", "\u002E", "\u007C", "\u2016", "\u203F"];
//    public static string[] TonesAndWordAccents = ["\u030B", "\u02E5", "\u0301", "\u02E6", "\u0304", "\u02E7", "\u0300", "\u02E8", "\u030F", "\u02E9", "\uA71C", "\uA71B", "\u030C", "\u02E9\u02E5", "\u0302", "\u02E5\u02E9", "\u1DC4", "\u02E7\u02E5", "\u1DC5", "\u02E9\u02E7", "\u1DC8", "\u02E7\u02E6\u02E8", "\u2197", "\u2198"];
//    public static string[] AdditionalSymbols = ["\u02A6", "\u02A3", "\u02A7", "\u02A4", "\u02A8", "\u02A5", "\u025D", "\u02B1", "\u02B3", "\u02B4", "\u02B5", "\u02B6", "\u02C0", "\u0322", "\u1DC6", "\u02E7\u02E9", "\u1DC7", "\u02E5\u02E7", "\u1DC9", "\u02E7\u02E8\u02E6"];

//    public static string[] AllIpa = ConsonantsPulmonic.Concat(ConsonantsNonPulmonic).Concat(OtherSymbols).Concat(Diacritics).Concat(Vowels).Concat(Suprasegmentals).Concat(TonesAndWordAccents).Concat(AdditionalSymbols).OrderByDescending(s => s.Length).ThenBy(s => s[0]).ThenBy(s => s.Length > 1 ? s[1] : '\0').ThenBy(s => s.Length > 2 ? s[2] : '\0').ToArray();

//    //public static char IpaToUnicode(this int source)
//    //  => source switch
//    //  {
//    //    101 => "\u0070", // PC
//    //    102 => "\u0062", // PC
//    //    103 => "\u0074", // PC
//    //    104 => "\u0064", // PC
//    //    105 => "\u0288", // PC
//    //    106 => "\u0256", // PC
//    //    107 => "\u0063", // PC
//    //    108 => "\u025F", // PC
//    //    109 => "\u006B", // PC
//    //    110 => "\u0261", // PC
//    //    111 => "\u0071", // PC
//    //    112 => "\u0262", // PC
//    //    113 => "\u0294", // PC
//    //    114 => "\u006D", // PC
//    //    115 => "\u0271", // PC
//    //    116 => "\u006E", // PC
//    //    117 => "\u0273", // PC
//    //    118 => "\u0272", // PC
//    //    119 => "\u014B", // PC
//    //    120 => "\u0274", // PC
//    //    121 => "\u0299", // PC
//    //    122 => "\u0072", // PC
//    //    123 => "\u0280", // PC
//    //    124 => "\u027E", // PC
//    //    125 => "\u027D", // PC
//    //    126 => "\u0278", // PC
//    //    127 => "\u03B2", // PC
//    //    128 => "\u0066", // PC
//    //    129 => "\u0076", // PC
//    //    130 => "\u03B8", // PC
//    //    131 => "\u00F0", // PC
//    //    132 => "\u0073", // PC
//    //    133 => "\u007A", // PC
//    //    134 => "\u0283", // PC
//    //    135 => "\u0292", // PC
//    //    136 => "\u0282", // PC
//    //    137 => "\u0290", // PC
//    //    138 => "\u00E7", // PC
//    //    139 => "\u029D", // PC
//    //    140 => "\u0078", // PC
//    //    141 => "\u0263", // PC
//    //    142 => "\u03C7", // PC
//    //    143 => "\u0281", // PC
//    //    144 => "\u0127", // PC
//    //    145 => "\u0295", // PC
//    //    146 => "\u0068", // PC
//    //    147 => "\u0266", // PC
//    //    148 => "\u026C", // PC
//    //    149 => "\u026E", // PC
//    //    150 => "\u028B", // PC
//    //    151 => "\u0279", // PC
//    //    152 => "\u027B", // PC
//    //    153 => "\u006A", // PC
//    //    154 => "\u0270", // PC
//    //    155 => "\u006C", // PC
//    //    156 => "\u026D", // PC
//    //    157 => "\u028E", // PC
//    //    158 => "\u029F", // PC
//    //    184 => "\u2C71", // PC
//    //    209 => "\u026B", // PC
//    //    _ => throw new System.ArgumentOutOfRangeException(nameof(source))
//    //  };

//    /// <summary>
//    /// <para>Determines whether <paramref name="source"/> is an IPA number of a pulmonic consonant.</para>
//    /// </summary>
//    /// <param name="source"></param>
//    /// <returns></returns>
//    public static bool IsIpaPulmonicConsonant(this int source)
//      => (source >= 101 && source <= 158) || source == 184 || source == 209;
//  }
//}
