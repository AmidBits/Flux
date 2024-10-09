namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new string with the first letter being lower-case using the specified <paramref name="cultureInfo"/>.</para>
    /// </summary>
    public static System.Span<System.Text.Rune> LetterAtToLower(this System.Span<System.Text.Rune> source, System.Globalization.CultureInfo? cultureInfo = null)
    {
      source[0] = System.Text.Rune.ToLower(source[0], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture);

      return source;
    }

    /// <summary>
    /// <para>Creates a new string with the first letter being lower-case using the invariant culture.</para>
    /// </summary>
    public static System.Span<System.Text.Rune> LetterAtToLowerInvariant(this System.Span<System.Text.Rune> source)
    {
      source[0] = System.Text.Rune.ToLowerInvariant(source[0]);

      return source;
    }

    /// <summary>
    /// <para>Creates a new string with the first letter being lower-case using the specified <paramref name="cultureInfo"/>.</para>
    /// </summary>
    public static System.Span<System.Text.Rune> LetterAtToUpper(this System.Span<System.Text.Rune> source, System.Globalization.CultureInfo? cultureInfo = null)
    {
      source[0] = System.Text.Rune.ToUpper(source[0], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture);

      return source;
    }

    /// <summary>
    /// <para>Creates a new string with the first letter being lower-case using the invariant culture.</para>
    /// </summary>
    public static System.Span<System.Text.Rune> LetterAtToUpperInvariant(this System.Span<System.Text.Rune> source)
    {
      source[0] = System.Text.Rune.ToUpperInvariant(source[0]);

      return source;
    }
  }
}
