namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Determines whether the character is any of the specified unicode category major codes.</summary>
    public static bool IsUnicodeMajorCode(this System.Text.Rune source, params Text.UnicodeCategoryMajorCode[] majorCodes)
      => System.Text.Rune.GetUnicodeCategory(source).ToMajorCode() is var majorCode && System.Array.Exists(majorCodes, mc => mc == majorCode);

    public static bool IsUnicodeLetter(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Letter;
    public static bool IsUnicodeMark(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Mark;
    public static bool IsUnicodeNumber(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Number;
    public static bool IsUnicodePunctuation(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Punctuation;
    public static bool IsUnicodeSymbol(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Symbol;
    public static bool IsUnicodeSeparator(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Separator;
    public static bool IsUnicodeOther(this System.Text.Rune source)
      => System.Text.Rune.GetUnicodeCategory(source).ToMajorCode() == Text.UnicodeCategoryMajorCode.Other;
  }
}
