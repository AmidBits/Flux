namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new array with the letter at <paramref name="index"/> converted to lower-case, using the specified <paramref name="cultureInfo"/> (current-culture if null).</summary>
    public static System.Span<char> LetterAtToLower(this System.ReadOnlySpan<char> source, int index, System.Globalization.CultureInfo? cultureInfo = null)
    {
      var charArray = source.ToArray();

      charArray[index] = char.ToLower(charArray[index], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture);

      return charArray;
    }

    /// <summary>Creates a new array with the letter at <paramref name="index"/> converted to invariant lower-case.</summary>
    public static System.Span<char> LetterAtToLowerInvariant(this System.ReadOnlySpan<char> source, int index)
    {
      var charArray = source.ToArray();

      charArray[index] = char.ToLowerInvariant(charArray[index]);

      return charArray;
    }

    /// <summary>Creates a new array with the letter at <paramref name="index"/> converted to upper-case, using the specified <paramref name="cultureInfo"/> (current-culture if null).</summary>
    public static System.Span<char> LetterAtToUpper(this System.ReadOnlySpan<char> source, int index, System.Globalization.CultureInfo? cultureInfo = null)
    {
      var charArray = source.ToArray();

      charArray[index] = char.ToUpper(charArray[index], cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture);

      return charArray;
    }

    /// <summary>Creates a new array with the letter at <paramref name="index"/> converted to invariant upper-case.</summary>
    public static System.Span<char> LetterAtToUpperInvariant(this System.ReadOnlySpan<char> source, int index)
    {
      var charArray = source.ToArray();

      charArray[index] = char.ToUpperInvariant(charArray[index]);

      return charArray;
    }
  }
}
