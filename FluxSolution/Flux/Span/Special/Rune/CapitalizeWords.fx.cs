namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Capitalize any lower-case rune at the beginning or with a whitespace on the left, and with a lower-case rune on the right.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
    /// <returns></returns>
    public static System.Span<System.Text.Rune> CapitalizeWords(this System.Span<System.Text.Rune> source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index >= 0; index--)
      {
        var r = source[index]; // Avoid multiple indexers.

        if (!System.Text.Rune.IsLower(r)) continue; // If, r is not lower-case, advance.

        if (index > 0 && !System.Text.Rune.IsWhiteSpace(source[index - 1])) continue; // If, (ensure left char exists) left is not white-space, advance.

        if (index < maxIndex && !System.Text.Rune.IsLower(source[index + 1])) continue; // If, (ensure right char exists) right is not lower-case, advance.

        source[index] = System.Text.Rune.ToUpper(r, culture);
      }

      return source;
    }
  }
}
