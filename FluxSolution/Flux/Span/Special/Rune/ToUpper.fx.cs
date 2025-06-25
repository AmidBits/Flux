namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Convert all runes in the span to upper-case.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
    /// <returns></returns>
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
