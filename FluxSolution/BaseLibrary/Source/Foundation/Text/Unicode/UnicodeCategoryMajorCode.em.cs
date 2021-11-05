namespace Flux
{
  /// <summary>The functionality of this class relates to U+hhhh style formatting.</summary>
  public static partial class ExtensionMethods
  {
    /// <summary>Translates a System.Globalization.UnicodeCategory enum value into a MajorLabel enum value.</summary>
    /// <example>var allCharactersByCategoryMajorLabel = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0066:Convert switch statement to expression", Justification = "Easier to read switch statement")]
    public static Text.UnicodeCategoryMajorCode ToMajorCode(this System.Globalization.UnicodeCategory unicodeCategory)
    {
      switch (unicodeCategory)
      {
        case System.Globalization.UnicodeCategory.LowercaseLetter:
        case System.Globalization.UnicodeCategory.ModifierLetter:
        case System.Globalization.UnicodeCategory.OtherLetter:
        case System.Globalization.UnicodeCategory.TitlecaseLetter:
        case System.Globalization.UnicodeCategory.UppercaseLetter:
          return Text.UnicodeCategoryMajorCode.Letter;
        case System.Globalization.UnicodeCategory.SpacingCombiningMark:
        case System.Globalization.UnicodeCategory.EnclosingMark:
        case System.Globalization.UnicodeCategory.NonSpacingMark:
          return Text.UnicodeCategoryMajorCode.Mark;
        case System.Globalization.UnicodeCategory.DecimalDigitNumber:
        case System.Globalization.UnicodeCategory.LetterNumber:
        case System.Globalization.UnicodeCategory.OtherNumber:
          return Text.UnicodeCategoryMajorCode.Number;
        case System.Globalization.UnicodeCategory.ConnectorPunctuation:
        case System.Globalization.UnicodeCategory.DashPunctuation:
        case System.Globalization.UnicodeCategory.ClosePunctuation:
        case System.Globalization.UnicodeCategory.FinalQuotePunctuation:
        case System.Globalization.UnicodeCategory.InitialQuotePunctuation:
        case System.Globalization.UnicodeCategory.OtherPunctuation:
        case System.Globalization.UnicodeCategory.OpenPunctuation:
          return Text.UnicodeCategoryMajorCode.Punctuation;
        case System.Globalization.UnicodeCategory.CurrencySymbol:
        case System.Globalization.UnicodeCategory.ModifierSymbol:
        case System.Globalization.UnicodeCategory.MathSymbol:
        case System.Globalization.UnicodeCategory.OtherSymbol:
          return Text.UnicodeCategoryMajorCode.Symbol;
        case System.Globalization.UnicodeCategory.LineSeparator:
        case System.Globalization.UnicodeCategory.ParagraphSeparator:
        case System.Globalization.UnicodeCategory.SpaceSeparator:
          return Text.UnicodeCategoryMajorCode.Separator;
        case System.Globalization.UnicodeCategory.Control:
        case System.Globalization.UnicodeCategory.Format:
        case System.Globalization.UnicodeCategory.OtherNotAssigned:
        case System.Globalization.UnicodeCategory.PrivateUse:
        case System.Globalization.UnicodeCategory.Surrogate:
          return Text.UnicodeCategoryMajorCode.Other;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unicodeCategory));
      }
    }
    /// <summary>Translates a System.Globalization.UnicodeCategory enum value into a MajorLabel enum value.</summary>
    /// <example>var allCharactersByCategoryMajorLabel = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
    public static Text.UnicodeCategoryMajorCode ToMajorCode(this Text.UnicodeCategoryMajorMinorCode codeMajorMinor)
      => ((System.Globalization.UnicodeCategory)codeMajorMinor).ToMajorCode();
  }
}