namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Convert all runes, in the specified range, to lower case. Uses the specified culture, or the current culture if null.</summary>
    public static System.Span<System.Text.Rune> ToLowerCase(this System.Span<System.Text.Rune> source, int startIndex, int length, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = startIndex + length - 1; index >= startIndex; index--)
      {
        var sourceRune = source[index];
        var targetRune = System.Text.Rune.ToLower(sourceRune, culture);

        if (sourceRune != targetRune) source[index] = targetRune;
      }

      return source;
    }

    /// <summary>Convert all runes, in the specified range, to lower case. Uses the specified culture, or the invariant culture if null.</summary>
    public static System.Span<System.Text.Rune> ToLowerCase(this System.Span<System.Text.Rune> source, System.Globalization.CultureInfo? culture = null)
      => source.ToLowerCase(0, source.Length, culture ?? System.Globalization.CultureInfo.InvariantCulture);
  }
}
