namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Uncapitalize any upper-case char at the beginning or with a whitespace on the left, and with a lower-case char on the right.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
    /// <returns></returns>
    public static System.Span<char> UncapitalizeWords(this System.Span<char> source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index >= 0; index--)
      {
        var c = source[index]; // Avoid multiple indexers.

        if (!char.IsUpper(c)) continue; // If, c is not upper-case, advance.

        if (index > 0 && !char.IsWhiteSpace(source[index - 1])) continue; // If, (ensure left char exists) left is not white-space, advance.

        if (index < maxIndex && !char.IsLower(source[index + 1])) continue; // If, (ensure right char exists) right is not lower-case, advance.

        source[index] = char.ToLower(c, culture);
      }

      return source;
    }
  }
}
