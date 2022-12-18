namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Determines whether the character is in any of the specified unicode categories.</summary>
    public static bool IsUnicodeCategory(this System.Text.Rune source, params System.Globalization.UnicodeCategory[] category)
      => System.Text.Rune.GetUnicodeCategory(source) is var unicodeCategory && System.Array.Exists(category, uc => uc == unicodeCategory);

    public static bool IsUnicodeLowercaseLetter(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.LowercaseLetter;
    public static bool IsUnicodeModifierLetter(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.ModifierLetter;
    public static bool IsUnicodeOtherLetter(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OtherLetter;
    public static bool IsUnicodeTitlecaseLetter(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.TitlecaseLetter;
    public static bool IsUnicodeUppercaseLetter(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.UppercaseLetter;

    public static bool IsUnicodeEnclosingMark(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.EnclosingMark;
    public static bool IsUnicodeNonSpacingMark(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.NonSpacingMark;
    public static bool IsUnicodeSpacingCombiningMark(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.SpacingCombiningMark;

    public static bool IsUnicodeDecimalDigitNumber(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.DecimalDigitNumber;
    public static bool IsUnicodeLetterNumber(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.LetterNumber;
    public static bool IsUnicodeOtherNumber(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OtherNumber;

    public static bool IsUnicodeClosePunctuation(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.ClosePunctuation;
    public static bool IsUnicodeConnectorPunctuation(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.ConnectorPunctuation;
    public static bool IsUnicodeDashPunctuation(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.DashPunctuation;
    public static bool IsUnicodeFinalQuotePunctuation(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.FinalQuotePunctuation;
    public static bool IsUnicodeInitialQuotePunctuation(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.InitialQuotePunctuation;
    public static bool IsUnicodeOpenPunctuation(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OpenPunctuation;
    public static bool IsUnicodeOtherPunctuation(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OtherPunctuation;

    public static bool IsUnicodeCurrencySymbol(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.CurrencySymbol;
    public static bool IsUnicodeMathSymbol(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.MathSymbol;
    public static bool IsUnicodeModifierSymbol(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.ModifierSymbol;
    public static bool IsUnicodeOtherSymbol(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OtherSymbol;

    public static bool IsUnicodeLineSeparator(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.LineSeparator;
    public static bool IsUnicodeParagraphSeparator(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.ParagraphSeparator;
    public static bool IsUnicodeSpaceSeparator(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.SpaceSeparator;

    public static bool IsUnicodeControlOther(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.Control;
    public static bool IsUnicodeFormatOther(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.Format;
    public static bool IsUnicodeNotAssignedOther(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.OtherNotAssigned;
    public static bool IsUnicodePrivateUseOther(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.PrivateUse;
    public static bool IsUnicodeSurrogateOther(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source) == System.Globalization.UnicodeCategory.Surrogate;
  }
}
