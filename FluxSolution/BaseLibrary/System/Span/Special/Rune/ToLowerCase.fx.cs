namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Convert all runes, in the specified range, to lower case. Uses the specified culture, or the current culture if null.</summary>
    public static System.Span<System.Text.Rune> ToLowerCase(this System.Span<System.Text.Rune> source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = source.Length - 1; index >= 0; index--)
        if (source[index] is var sourceRune && System.Text.Rune.ToLower(sourceRune, culture) is var targetRune && sourceRune != targetRune)
          source[index] = targetRune;

      return source;
    }
  }
}
