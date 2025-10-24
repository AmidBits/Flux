namespace Flux
{
  /// <summary>
  /// <para></para>
  /// </summary>
  public enum UnicodeSymbolForInformationSeparator
  {
    /// <summary>
    /// <para>The character "&#x241C;" (Symbol For File Separator) is represented by the Unicode codepoint U+241C.</para>
    /// </summary>
    SymbolForFileSeparator = '\u241C',
    /// <summary>
    /// <para>The character "&#x241D;" (Symbol For Group Separator) is represented by the Unicode codepoint U+241D.</para>
    /// </summary>
    SymbolForGroupSeparator = '\u241D',
    /// <summary>
    /// <para>The character "&#x241E;" (Symbol For Record Separator) is represented by the Unicode codepoint U+241E.</para>
    /// </summary>
    SymbolForRecordSeparator = '\u241E',
    /// <summary>
    /// <para>The character "&#x241F;" (Symbol For Unit Separator) is represented by the Unicode codepoint U+241F.</para>
    /// </summary>
    SymbolForUnitSeparator = '\u241F',
  }

  public static partial class Unicode
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static char GetSeparatorChar(this UnicodeSymbolForInformationSeparator separator)
      => (char)(int)separator;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string ToSeparatorString(this UnicodeSymbolForInformationSeparator separator)
      => separator.GetSeparatorChar().ToString();

    /// <summary>
    /// <para>Gets the "<c>Information Separator ..</c>" (control character) of <paramref name="separator"/>.</para>
    /// </summary>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static UnicodeInformationSeparator GetUnicodeInformationSeparator(this UnicodeSymbolForInformationSeparator separator)
      => (UnicodeInformationSeparator)((int)separator - 0x2400);

    /// <summary>
    /// <para>Attempts to get the <see cref="UnicodeSymbolForInformationSeparator"/> of the <paramref name="character"/> as the out parameter <paramref name="separator"/> and returns whether successful.</para>
    /// </summary>
    /// <param name="character"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static bool TryGetUnicodeSymbolForInformationSeparator(int character, out UnicodeSymbolForInformationSeparator separator)
    {
      if (character >= (int)UnicodeSymbolForInformationSeparator.SymbolForFileSeparator && character <= (int)UnicodeSymbolForInformationSeparator.SymbolForUnitSeparator)
      {
        separator = (UnicodeSymbolForInformationSeparator)character;
        return true;
      }
      else
      {
        separator = default;
        return false;
      }
    }
  }
}
