namespace Flux
{
  public static partial class Unicode
  {
    /// <summary>
    /// <para>Create a string containing the string representation of <paramref name="spacing"/>.</para>
    /// </summary>
    /// <param name="spacing"></param>
    /// <returns></returns>
    public static string ToSpacingString(this UnicodeSpacing spacing)
      => spacing == UnicodeSpacing.None ? string.Empty : ((char)(int)spacing).ToString();

    /// <summary>
    /// <para>Attempt to get the spacing character <paramref name="spaceChar"/> for <paramref name="spacing"/>.</para>
    /// </summary>
    /// <param name="spacing"></param>
    /// <param name="spaceChar"></param>
    /// <returns></returns>
    public static bool TryGetSpacingChar(this UnicodeSpacing spacing, out char spaceChar)
      => (spaceChar = (char)(int)spacing) != '\0';
  }
}
