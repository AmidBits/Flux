namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static char GetSeparatorChar(this Unicode.UnicodeInformationSeparator separator) => (char)(int)separator;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string ToSeparatorString(this Unicode.UnicodeInformationSeparator separator) => separator.GetSeparatorChar().ToString();

    /// <summary>
    /// <para>Gets the "<c>Symbol For .. Separator</c>" (visual character) of <paramref name="separator"/>.</para>
    /// </summary>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static Unicode.UnicodeSymbolForInformationSeparator GetUnicodeSymbolForInformationSeparator(this Unicode.UnicodeInformationSeparator separator)
      => (Unicode.UnicodeSymbolForInformationSeparator)((int)separator + 0x2400);

    /// <summary>
    /// <para>Attempts to get the <see cref="UnicodeInformationSeparator"/> of the <paramref name="character"/> as the out parameter <paramref name="separator"/> and returns whether successful.</para>
    /// </summary>
    /// <param name="character"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static bool TryGetUnicodeInformationSeparator(int character, out Unicode.UnicodeInformationSeparator separator)
    {
      if (character >= (int)Unicode.UnicodeInformationSeparator.FileSeparator && character <= (int)Unicode.UnicodeInformationSeparator.UnitSeparator)
      {
        separator = (Unicode.UnicodeInformationSeparator)character;
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
