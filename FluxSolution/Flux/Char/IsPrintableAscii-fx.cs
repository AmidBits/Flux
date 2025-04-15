namespace Flux
{
  public static partial class Chars
  {
    /// <summary>
    /// <para>Returns whether a character is a printable from the ASCII table, i.e. any character from U+0020 (32 = ' ' space) to U+007E (126 = '~' tilde), both inclusive.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsPrintableAscii(this char source)
      => source is >= '\u0020' and <= '\u007E';
  }
}
