//namespace Flux
//{
//  public static partial class Unicode
//  {
//    public static string ToUnicodeCategoryMajorMinor(this System.Globalization.UnicodeCategory unicodeCategory)
//      => unicodeCategory switch
//      {
//        System.Globalization.UnicodeCategory.Control => @"Cc",
//        System.Globalization.UnicodeCategory.Format => @"Cf",
//        System.Globalization.UnicodeCategory.OtherNotAssigned => @"Cn",
//        System.Globalization.UnicodeCategory.PrivateUse => @"Co",
//        System.Globalization.UnicodeCategory.Surrogate => @"Cs",

//        System.Globalization.UnicodeCategory.LowercaseLetter => @"Ll",
//        System.Globalization.UnicodeCategory.ModifierLetter => @"Lm",
//        System.Globalization.UnicodeCategory.OtherLetter => @"Lo",
//        System.Globalization.UnicodeCategory.TitlecaseLetter => @"Lt",
//        System.Globalization.UnicodeCategory.UppercaseLetter => @"Lu",

//        System.Globalization.UnicodeCategory.SpacingCombiningMark => @"Mc",
//        System.Globalization.UnicodeCategory.EnclosingMark => @"Me",
//        System.Globalization.UnicodeCategory.NonSpacingMark => @"Mn",

//        System.Globalization.UnicodeCategory.DecimalDigitNumber => @"Nd",
//        System.Globalization.UnicodeCategory.LetterNumber => @"Nl",
//        System.Globalization.UnicodeCategory.OtherNumber => @"No",

//        System.Globalization.UnicodeCategory.ConnectorPunctuation => @"Pc",
//        System.Globalization.UnicodeCategory.DashPunctuation => @"Pd",
//        System.Globalization.UnicodeCategory.ClosePunctuation => @"Pe",
//        System.Globalization.UnicodeCategory.FinalQuotePunctuation => @"Pf",
//        System.Globalization.UnicodeCategory.InitialQuotePunctuation => @"Pi",
//        System.Globalization.UnicodeCategory.OtherPunctuation => @"Po",
//        System.Globalization.UnicodeCategory.OpenPunctuation => @"Ps",

//        System.Globalization.UnicodeCategory.CurrencySymbol => @"Sc",
//        System.Globalization.UnicodeCategory.ModifierSymbol => @"Sk",
//        System.Globalization.UnicodeCategory.MathSymbol => @"Sm",
//        System.Globalization.UnicodeCategory.OtherSymbol => @"So",

//        System.Globalization.UnicodeCategory.LineSeparator => @"Zl",
//        System.Globalization.UnicodeCategory.ParagraphSeparator => @"Zp",
//        System.Globalization.UnicodeCategory.SpaceSeparator => @"Zs",

//        _ => throw new System.ArgumentOutOfRangeException(nameof(unicodeCategory)),
//      };
//  }
//}
