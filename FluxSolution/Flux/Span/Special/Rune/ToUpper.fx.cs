namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Convert the entire span to upper-case. Uses the specified <paramref name="culture"/>, or current-culture if null.</para>
    /// </summary>
    public static System.Span<System.Text.Rune> ToUpper(this System.Span<System.Text.Rune> source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = source.Length - 1; index >= 0; index--)
        if (source[index] is var sourceRune && System.Text.Rune.ToUpper(sourceRune, culture) is var targetRune && sourceRune != targetRune)
          source[index] = targetRune;

      return source;
    }
  }
}
