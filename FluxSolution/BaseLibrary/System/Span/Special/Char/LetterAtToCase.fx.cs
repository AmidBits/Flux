namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new string with the first letter being lower-case using the specified <paramref name="cultureInfo"/>.</para>
    /// </summary>
    public static System.Span<char> LetterAtToLower(this System.Span<char> source, System.Globalization.CultureInfo? cultureInfo = null)
    {
      source[0] = char.ToLower(source[0], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture);

      return source;
    }

    /// <summary>
    /// <para>Creates a new string with the first letter being lower-case using the invariant culture.</para>
    /// </summary>
    public static System.Span<char> LetterAtToLower(this System.Span<char> source)
    {
      source[0] = char.ToLowerInvariant(source[0]);

      return source;
    }

    /// <summary>
    /// <para>Creates a new string with the first letter being upper-case using the specified <paramref name="cultureInfo"/>.</para>
    /// </summary>
    public static System.Span<char> LetterAtToUpper(this System.Span<char> source, System.Globalization.CultureInfo? cultureInfo = null)
    {
      source[0] = char.ToUpper(source[0], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture);

      return source;
    }

    /// <summary>
    /// <para>Creates a new string with the first letter being upper-case using the invariant culture.</para>
    /// </summary>
    public static System.Span<char> LetterAtToUpper(this System.Span<char> source)
    {
      source[0] = char.ToUpperInvariant(source[0]);

      return source;
    }
  }
}
