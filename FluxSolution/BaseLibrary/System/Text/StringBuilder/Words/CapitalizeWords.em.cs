namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Capitalize any lower-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</summary>
    public static System.Text.StringBuilder CapitalizeWords(this System.Text.StringBuilder source, System.Globalization.CultureInfo? culture = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = source.Length - 1; index >= 0; index--)
        if (char.IsLower(source[index]) && (index == 0 || char.IsWhiteSpace(source[index - 1])) && char.IsLower(source[index + 1]))
          source[index] = char.ToUpper(source[index], culture);

      return source;
    }
  }
}
