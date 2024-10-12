namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Convert upper-case letters to lower-case. Uses the specified <paramref name="cultureInfo"/>, or current-culture if null.</para>
    /// </summary>
    public static System.Span<char> ToLower(this System.Span<char> source, System.Globalization.CultureInfo? cultureInfo = null)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        if (source[index] is var sourceChar && char.ToLower(sourceChar, cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture) is var targetChar && sourceChar != targetChar)
          source[index] = targetChar;

      return source;
    }
  }
}
