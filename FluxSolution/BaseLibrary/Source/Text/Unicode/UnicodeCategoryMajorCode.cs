namespace Flux
{
  /// <summary>The functionality of this class relates to U+hhhh style formatting.</summary>
  public static partial class UnicodeEm
  {
    /// <summary>Translates a System.Globalization.UnicodeCategory enum value into a MajorLabel enum value.</summary>
    /// <example>var allCharactersByCategoryMajorLabel = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
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

  namespace Text
  {
    /// <summary>This is an aggregate derivation of the System.Globalization.UnicodeCategory (or MajorMinorCode) enum value. The values represents the character of the first letter in the major code name, e.g. 'P' for Puncuation.</summary>
    /// <example>var allCharactersByCategoryMajorLabel = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
    public enum UnicodeCategoryMajorCode
    {
      Letter = 'L',
      Mark = 'M',
      Number = 'N',
      Punctuation = 'P',
      Symbol = 'S',
      Separator = 'Z',
      Other = 'C',
    }

    /// <summary>The functionality of this class relates to U+hhhh style formatting.</summary>
    public static class UnicodeCategoryMajor
    {
      public static UnicodeCategoryMajorCode Parse(char majorCode)
        => (UnicodeCategoryMajorCode)System.Enum.Parse(typeof(UnicodeCategoryMajorCode), majorCode.ToString());
      public static bool TryParse(char majorCode, out UnicodeCategoryMajorCode result)
      {
        try
        {
          result = Parse(majorCode);
          return true;
        }
#pragma warning disable CA1031 // Do not catch general exception types.
        catch { }
#pragma warning restore CA1031 // Do not catch general exception types.

        result = default;
        return false;
      }
    }
  }
}