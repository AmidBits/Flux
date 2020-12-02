namespace Flux
{
  /// <summary>The functionality of this class relates to U+hhhh style formatting.</summary>
  public static partial class UnicodeEm
  {
    public static Text.UnicodeCategoryMajorMinorCode ToMajorMinorCode(this System.Globalization.UnicodeCategory unicodeCategory)
      => (Text.UnicodeCategoryMajorMinorCode)unicodeCategory;

    /// <summary>Converts a MajorMinorCode enum to a UnicodeCategory enum.</summary>
    public static System.Globalization.UnicodeCategory ToUnicodeCategory(this Text.UnicodeCategoryMajorMinorCode categoryCode)
      => (System.Globalization.UnicodeCategory)categoryCode;
  }

  namespace Text
  {
    /// <summary>This is a directly correlated enum of System.Globalization.UnicodeCategory to ease translation to the abbreviated two character code for the major and minor parts of the System.Globalization.UnicodeCategory enum values.</summary>
    /// <example>var allCharactersByCategoryMajorMinorCode = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorMinorCode()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
    public enum UnicodeCategoryMajorMinorCode
    {
      Ll = System.Globalization.UnicodeCategory.LowercaseLetter,
      Lm = System.Globalization.UnicodeCategory.ModifierLetter,
      Lo = System.Globalization.UnicodeCategory.OtherLetter,
      Lt = System.Globalization.UnicodeCategory.TitlecaseLetter,
      Lu = System.Globalization.UnicodeCategory.UppercaseLetter,

      Mc = System.Globalization.UnicodeCategory.SpacingCombiningMark,
      Me = System.Globalization.UnicodeCategory.EnclosingMark,
      Mn = System.Globalization.UnicodeCategory.NonSpacingMark,

      Nd = System.Globalization.UnicodeCategory.DecimalDigitNumber,
      Nl = System.Globalization.UnicodeCategory.LetterNumber,
      No = System.Globalization.UnicodeCategory.OtherNumber,

      Pc = System.Globalization.UnicodeCategory.ConnectorPunctuation,
      Pd = System.Globalization.UnicodeCategory.DashPunctuation,
      Pe = System.Globalization.UnicodeCategory.ClosePunctuation,
      Pf = System.Globalization.UnicodeCategory.FinalQuotePunctuation,
      Pi = System.Globalization.UnicodeCategory.InitialQuotePunctuation,
      Po = System.Globalization.UnicodeCategory.OtherPunctuation,
      Ps = System.Globalization.UnicodeCategory.OpenPunctuation,

      Sc = System.Globalization.UnicodeCategory.CurrencySymbol,
      Sk = System.Globalization.UnicodeCategory.ModifierSymbol,
      Sm = System.Globalization.UnicodeCategory.MathSymbol,
      So = System.Globalization.UnicodeCategory.OtherSymbol,

      Zl = System.Globalization.UnicodeCategory.LineSeparator,
      Zp = System.Globalization.UnicodeCategory.ParagraphSeparator,
      Zs = System.Globalization.UnicodeCategory.SpaceSeparator,

      Cc = System.Globalization.UnicodeCategory.Control,
      Cf = System.Globalization.UnicodeCategory.Format,
      Cn = System.Globalization.UnicodeCategory.OtherNotAssigned,
      Co = System.Globalization.UnicodeCategory.PrivateUse,
      Cs = System.Globalization.UnicodeCategory.Surrogate,
    }

    /// <summary>The functionality of this class relates to U+hhhh style formatting.</summary>
    public static class UnicodeCategoryMajorMinor
    {
      /// <summary>Parses two characters representing the major and minor portions.</summary>
      public static UnicodeCategoryMajorMinorCode Parse(char codeMajor, char codeMinor)
        => ((UnicodeCategoryMajorMinorCode)System.Enum.Parse(typeof(UnicodeCategoryMajorMinorCode), $"{codeMajor}{codeMinor}"));
      /// <summary>Parses a string with both major and minor portions.</summary>
      public static UnicodeCategoryMajorMinorCode Parse(string codeMajorMinor)
        => (codeMajorMinor ?? throw new System.ArgumentNullException(nameof(codeMajorMinor))).Length == 2 ? Parse(codeMajorMinor[0], codeMajorMinor[1]) : throw new System.ArgumentOutOfRangeException(nameof(codeMajorMinor));
      /// <summary>Try to parse a string with both major and minor portions.</summary>
      public static bool TryParse(string categoryCode, out UnicodeCategoryMajorMinorCode result)
      {
        try
        {
          result = Parse(categoryCode);
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