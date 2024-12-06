namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Capitalize any lower-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</summary>
    public static System.Span<System.Text.Rune> CapitalizeWords(this System.Span<System.Text.Rune> source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index >= 0; index--)
      {
        var r = source[index]; // Avoid multiple indexers.

        if (!System.Text.Rune.IsLower(r)) continue; // If, r is not lower-case, advance.

        if ((index > 0) && !System.Text.Rune.IsWhiteSpace(source[index - 1])) continue; // If, (ensure previous) previous is not white-space, advance.

        if ((index < maxIndex) && !System.Text.Rune.IsLower(source[index + 1])) continue; // If, (ensure next) next is not lower-case, advance.

        source[index] = System.Text.Rune.ToUpper(r, culture);
      }

      return source;
    }
  }
}
