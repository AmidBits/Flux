namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Convert lower-case letters to upper-case. Uses the specified <paramref name="cultureInfo"/>, or current-culture if null.</para>
    /// </summary>
    public static System.Span<System.Text.Rune> ToUpper(this System.Span<System.Text.Rune> source, System.Globalization.CultureInfo? culture = null)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        if (source[index] is var sourceRune && System.Text.Rune.ToUpper(sourceRune, culture ?? System.Globalization.CultureInfo.CurrentCulture) is var targetRune && sourceRune != targetRune)
          source[index] = targetRune;

      return source;
    }
  }
}
