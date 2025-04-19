namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>Capitalize any lower-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</summary>
    public static System.Text.StringBuilder CapitalizeWords(this System.Text.StringBuilder source, System.Globalization.CultureInfo? culture = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index >= 0; index--)
      {
        var c = source[index]; // Avoid multiple indexers.

        if (!char.IsLower(c)) continue; // If, c is not lower-case, advance.

        if ((index > 0) && !char.IsWhiteSpace(source[index - 1])) continue; // If, (ensure previous) previous is not white-space, advance.

        if ((index < maxIndex) && !char.IsLower(source[index + 1])) continue; // If, (ensure next) next is not lower-case, advance.

        source[index] = char.ToUpper(c, culture);
      }

      return source;
    }
  }
}
