namespace Flux
{
  public static partial class CharEm
  {
    /// <summary>Determines whether the character is in the specified unicode category.</summary>
    public static bool IsUnicodeCategory(this char source, System.Globalization.UnicodeCategory category)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == category;

    /// <summary>Determines whether the character is in any of the specified unicode categories.</summary>
    public static bool IsUnicodeCategory(this char source, params System.Globalization.UnicodeCategory[] category)
      => System.Array.Exists(category, uc => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == uc);

    public static bool IsUnicodeLetter(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Letter;
    public static bool IsUnicodeMark(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Mark;
    public static bool IsUnicodeNumber(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Number;
    public static bool IsUnicodePunctuation(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Punctuation;
    public static bool IsUnicodeSymbol(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Symbol;
    public static bool IsUnicodeSeparator(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Separator;
    public static bool IsUnicodeOther(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Other;

    public static bool IsUnicodeLowercaseLetter(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.LowercaseLetter;
    public static bool IsUnicodeModifierLetter(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.ModifierLetter;
    public static bool IsUnicodeOtherLetter(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OtherLetter;
    public static bool IsUnicodeTitlecaseLetter(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.TitlecaseLetter;
    public static bool IsUnicodeUppercaseLetter(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.UppercaseLetter;

    public static bool IsUnicodeEnclosingMark(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.EnclosingMark;
    public static bool IsUnicodeNonSpacingMark(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.NonSpacingMark;
    public static bool IsUnicodeSpacingCombiningMark(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.SpacingCombiningMark;

    public static bool IsUnicodeDecimalDigitNumber(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.DecimalDigitNumber;
    public static bool IsUnicodeLetterNumber(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.LetterNumber;
    public static bool IsUnicodeOtherNumber(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OtherNumber;

    public static bool IsUnicodeClosePunctuation(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.ClosePunctuation;
    public static bool IsUnicodeConnectorPunctuation(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.ConnectorPunctuation;
    public static bool IsUnicodeDashPunctuation(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.DashPunctuation;
    public static bool IsUnicodeFinalQuotePunctuation(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.FinalQuotePunctuation;
    public static bool IsUnicodeInitialQuotePunctuation(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.InitialQuotePunctuation;
    public static bool IsUnicodeOpenPunctuation(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OpenPunctuation;
    public static bool IsUnicodeOtherPunctuation(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OtherPunctuation;

    public static bool IsUnicodeCurrencySymbol(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.CurrencySymbol;
    public static bool IsUnicodeMathSymbol(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.MathSymbol;
    public static bool IsUnicodeModifierSymbol(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.ModifierSymbol;
    public static bool IsUnicodeOtherSymbol(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OtherSymbol;

    public static bool IsUnicodeLineSeparator(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.LineSeparator;
    public static bool IsUnicodeParagraphSeparator(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.ParagraphSeparator;
    public static bool IsUnicodeSpaceSeparator(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.SpaceSeparator;

    public static bool IsUnicodeControlOther(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.Control;
    public static bool IsUnicodeFormatOther(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.Format;
    public static bool IsUnicodeNotAssignedOther(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OtherNotAssigned;
    public static bool IsUnicodePrivateUseOther(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.PrivateUse;
    public static bool IsUnicodeSurrogateOther(this char source)
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.Surrogate;
  }
}
