namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new string with the letter at <paramref name="index"/> converted to lower-case, using the specified <paramref name="cultureInfo"/> (current-culture if null).</summary>
    public static string LetterAtToLower(this string source, int index, System.Globalization.CultureInfo? cultureInfo = null)
     => (index > 0 ? source[..index] : string.Empty) + char.ToLower(source[index], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture) + (index < source.Length - 1 ? source[(index + 1)..] : string.Empty);

    /// <summary>Creates a new string with the letter at <paramref name="index"/> converted to invariant lower-case.</summary>
    public static string LetterAtToLowerInvariant(this string source, int index)
     => (index > 0 ? source[..index] : string.Empty) + char.ToLowerInvariant(source[index]) + (index < source.Length - 1 ? source[(index + 1)..] : string.Empty);

    /// <summary>Creates a new string with the letter at <paramref name="index"/> converted to upper-case, using the specified <paramref name="cultureInfo"/> (current-culture if null).</summary>
    public static string LetterAtToUpper(this string source, int index, System.Globalization.CultureInfo? cultureInfo = null)
     => (index > 0 ? source[..index] : string.Empty) + char.ToUpper(source[index], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture) + (index < source.Length - 1 ? source[(index + 1)..] : string.Empty);

    /// <summary>Creates a new string with the letter at <paramref name="index"/> converted to invariant upper-case.</summary>
    public static string LetterAtToUpperInvariant(this string source, int index)
     => (index > 0 ? source[..index] : string.Empty) + char.ToUpperInvariant(source[index]) + (index < source.Length - 1 ? source[(index + 1)..] : string.Empty);
  }
}
