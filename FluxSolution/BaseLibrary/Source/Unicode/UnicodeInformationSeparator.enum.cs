namespace Flux
{
  public static partial class Unicode
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static char GetSeparatorChar(this UnicodeInformationSeparator separator) => (char)(int)separator;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string ToSeparatorString(this UnicodeInformationSeparator separator) => separator.GetSeparatorChar().ToString();

    /// <summary>
    /// <para>Gets the "<c>Symbol For .. Separator</c>" (visual character) of <paramref name="separator"/>.</para>
    /// </summary>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static UnicodeSymbolForInformationSeparator GetUnicodeSymbolForInformationSeparator(this UnicodeInformationSeparator separator)
      => (UnicodeSymbolForInformationSeparator)((int)separator + 0x2400);

    /// <summary>
    /// <para>Attempts to get the <see cref="UnicodeInformationSeparator"/> of the <paramref name="character"/> as the out parameter <paramref name="separator"/> and returns whether successful.</para>
    /// </summary>
    /// <param name="character"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static bool TryGetUnicodeInformationSeparator(int character, out UnicodeInformationSeparator separator)
    {
      if (character >= (int)UnicodeInformationSeparator.FileSeparator && character <= (int)UnicodeInformationSeparator.UnitSeparator)
      {
        separator = (UnicodeInformationSeparator)character;
        return true;
      }
      else
      {
        separator = default;
        return false;
      }
    }
  }

  /// <summary>
  /// <para></para>
  /// </summary>
  public enum UnicodeInformationSeparator
  {
    /// <summary>
    /// <para>The control character u001c (named <c>Information Separator Four</c>) is represented by the Unicode codepoint U+001C.</para>
    /// <para>A separator of files or databases.</para>
    /// </summary>
    FileSeparator = '\u001C',
    /// <summary>
    /// <para>The control character u001d (named <c>Information Separator Three</c>) is represented by the Unicode codepoint U+001D.</para>
    /// <para>A separator of groups, tables or record-sets (i.e. a collection of records, e.g. rows).</para>
    /// </summary>
    GroupSeparator = '\u001D',
    /// <summary>
    /// <para>The control character u001e (named <c>Information Separator Two</c>) is represented by the Unicode codepoint U+001E.</para>
    /// <para>A separator of records, rows, lines, etc.</para>
    /// </summary>
    RecordSeparator = '\u001E',
    /// <summary>
    /// <para>The control character u001f (named <c>Information Separator One</c>) is represented by the Unicode codepoint U+001F.</para>
    /// <para>A separator of units, columns, fields, etc.</para>
    /// </summary>
    UnitSeparator = '\u001F',
  }
}
