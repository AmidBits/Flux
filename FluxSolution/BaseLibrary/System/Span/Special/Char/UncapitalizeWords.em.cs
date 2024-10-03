namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Uncapitalize any upper-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</summary>
    public static System.Span<char> UncapitalizeWords(this System.Span<char> source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index >= 0; index--)
      {
        var c = source[index]; // Avoid multiple indexers.

        if (!char.IsUpper(c)) continue; // If, c is not upper-case, advance.

        if ((index > 0) && !char.IsWhiteSpace(source[index - 1])) continue; // If, (ensure previous) previous is not white-space, advance.

        if ((index < maxIndex) && !char.IsLower(source[index + 1])) continue; // If, (ensure next) next is not lower-case, advance.

        source[index] = char.ToLower(c, culture);
      }

      return source;
    }
  }
}
