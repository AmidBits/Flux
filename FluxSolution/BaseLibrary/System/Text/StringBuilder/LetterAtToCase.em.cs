namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Converts the letter at <paramref name="index"/> to lower-case, using the specified <paramref name="cultureInfo"/> (current-culture if null).</summary>
    public static System.Text.StringBuilder LetterAtToLower(this System.Text.StringBuilder source, int index, System.Globalization.CultureInfo? cultureInfo = null)
    {
      source[index] = char.ToLower(source[index], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture);

      return source;
    }

    /// <summary>Converts the letter at <paramref name="index"/> to invariant lower-case.</summary>
    public static System.Text.StringBuilder LetterAtToLowerInvariant(this System.Text.StringBuilder source, int index)
    {
      source[index] = char.ToLowerInvariant(source[index]);

      return source;
    }

    /// <summary>Converts the letter at <paramref name="index"/> to upper-case, using the specified <paramref name="cultureInfo"/> (current-culture if null).</summary>
    public static System.Text.StringBuilder LetterAtToUpper(this System.Text.StringBuilder source, int index, System.Globalization.CultureInfo? cultureInfo = null)
    {
      source[index] = char.ToUpper(source[index], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture);

      return source;
    }

    /// <summary>Converts with the letter at <paramref name="index"/> to invariant upper-case.</summary>
    public static System.Text.StringBuilder LetterAtToUpperInvariant(this System.Text.StringBuilder source, int index)
    {
      source[index] = char.ToUpperInvariant(source[index]);

      return source;
    }
  }
}
