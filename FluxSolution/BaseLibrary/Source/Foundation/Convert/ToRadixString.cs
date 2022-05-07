namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Returns the string formatted using the specified base, 2 for binary, 10 for decimal, 16 for hexadecimal, etc.</summary>
    public static string ToRadixString(this System.Numerics.BigInteger number, int radix)
      => PositionalNotation.ForRadix(radix).NumberToText(number).ToString();

    /// <summary>Returns the string formatted using the specified base, 2 for binary, 10 for decimal, 16 for hexadecimal, etc.</summary>
    public static string ToRadixString(this int number, int radix)
      => PositionalNotation.ForRadix(radix).NumberToText(number).ToString();
    /// <summary>Returns the string formatted using the specified base, 2 for binary, 10 for decimal, 16 for hexadecimal, etc.</summary>
    public static string ToRadixString(this long number, int radix)
      => PositionalNotation.ForRadix(radix).NumberToText(number).ToString();
  }
}
