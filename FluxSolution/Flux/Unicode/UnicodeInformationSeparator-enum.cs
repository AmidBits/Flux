namespace Flux
{
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
