namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Append the selected <paramref name="spacing"/> to a string.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="spacing"></param>
    /// <returns></returns>
    public static string AppendUnicodeSpacing(this string source, Unicode.UnicodeSpacing spacing = Unicode.UnicodeSpacing.Space)
      => source + spacing.ToSpacingString();

    /// <summary>
    /// <para>Prepend the selected <paramref name="spacing"/> to a string.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="spacing"></param>
    /// <returns></returns>
    public static string PrependUnicodeSpacing(this string source, Unicode.UnicodeSpacing spacing = Unicode.UnicodeSpacing.Space)
      => spacing.ToSpacingString() + source;

    /// <summary>
    /// <para>Create a string containing the string representation of <paramref name="spacing"/>.</para>
    /// </summary>
    /// <param name="spacing"></param>
    /// <returns></returns>
    public static string ToSpacingString(this Unicode.UnicodeSpacing spacing)
      => spacing == Unicode.UnicodeSpacing.None ? string.Empty : ((char)(int)spacing).ToString();

    /// <summary>
    /// <para>Attempt to get the spacing character <paramref name="spaceChar"/> for <paramref name="spacing"/>.</para>
    /// </summary>
    /// <param name="spacing"></param>
    /// <param name="spaceChar"></param>
    /// <returns></returns>
    public static bool TryGetSpacingChar(this Unicode.UnicodeSpacing spacing, out char spaceChar)
      => (spaceChar = (char)(int)spacing) != '\0';
  }
}
