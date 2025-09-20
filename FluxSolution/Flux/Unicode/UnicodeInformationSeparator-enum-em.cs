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
}
