namespace Flux
{
  public static partial class Runes
  {
    /// <summary>
    /// <para>The set of Unicode character categories containing non-rendering, unknown, or incomplete characters.</para>
    /// <para>Unicode.Format and Unicode.PrivateUse can NOT be included in this set, because they may (private-use) or do (format) contain at least *some* rendering characters.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsNonRenderingCategory(this System.Text.Rune source, bool nonRenderCategoryFormat = false, bool nonRenderCategoryPrivateUse = false)
      => System.Text.Rune.GetUnicodeCategory(source) is var uc
      && uc is System.Globalization.UnicodeCategory.Control or System.Globalization.UnicodeCategory.OtherNotAssigned or System.Globalization.UnicodeCategory.Surrogate
      || (nonRenderCategoryFormat && uc == System.Globalization.UnicodeCategory.Format)
      || (nonRenderCategoryPrivateUse && uc == System.Globalization.UnicodeCategory.PrivateUse);

    /// <summary>
    /// <para>Returns whether a character is a printable from the ASCII table, i.e. any character from U+0020 (32 = ' ' space) to U+007E (126 = '~' tilde), both inclusive.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsPrintableAscii(this System.Text.Rune source)
      => source.Value is >= '\u0020' and <= '\u007E';

    /// <summary>
    /// <para>Returns whether a rune (Unicode codepoint) is a "printable" character. There is no simple solution, so please read the entire blurb of this method.</para>
    /// <para>The following code assumes that all characters in Unicode categories PrivateUse and Format are printable, which means that at least some characters will be misclassified.</para>
    /// <para><see href="https://stackoverflow.com/a/45928048/3178666"/></para>
    /// <para>IsWhiteSpace() includes the whitespace characters that are categorized as control characters. Any other character is printable, unless it falls into the non-rendering categories.</para>
    /// <para>Exclude Unicode categories containing non-rendering, unknown, or incomplete characters. Unicode categories Format and PrivateUse can NOT be included in this set, because they may (private-use) or do (format) contain at least *some* rendering characters.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsPrintableUnicode(this System.Text.Rune source)
      => System.Text.Rune.IsWhiteSpace(source) || !source.IsNonRenderingCategory();
  }
}
