namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Get the three binary IEEE parts of the floating point both as binary (shifted to LSB) and adjusted (see below).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns>
    /// <para>The three adjusted IEEE parts: <c>(int Sign = 1 or -1, int ExponentUnbiased = [−1022, +1023], long Mantissa53 = [0, 9007199254740991])</c>.</para>
    /// <para>Also returns the three binary IEEE parts, only shifted to LSB, as out parameters: <c><paramref name="ieeeSignBit"/> = 0 or 1 (1-bit as int), <paramref name="ieeeExponentBiased"/> = [0, 2047] (11-bits as int) and <paramref name="ieeeSignificandPrecision52"/> = [0, 4503599627370495]</c>.</para>
    /// </returns>
    public static (int IeeeSign, int IeeeExponentUnbiased, long IeeeMantissa53) GetIeeeParts(this double source, out int ieeeSignBit, out int ieeeExponentBiased, out long ieeeSignificandPrecision52)
    {
      var bits = System.BitConverter.DoubleToUInt64Bits(source);

      ieeeSignBit = (int)((bits & 0x8000000000000000UL) >>> 63);

      ieeeExponentBiased = (int)((bits & 0x7FF0000000000000UL) >>> 52);

      ieeeSignificandPrecision52 = (long)(bits & 0x000FFFFFFFFFFFFFUL);

      var sign = ieeeSignBit == 0 ? 1 : -1;

      var exponentUnbiased = ieeeExponentBiased - 1023;

      var mantissa53 = 0x0010000000000000L | ieeeSignificandPrecision52; // This is the significandPrecision above with the hidden 53-bit added.

      return (sign, exponentUnbiased, mantissa53);
    }

    [System.Obsolete("This functionality exists only for specific use.", false)]
    public static (long IeeeIntegerPart, double IeeeFractionalPart, long IeeeFractionalPartAsWhole) GetPartsIeee(this double source)
    {
      var (ieeeSign, ieeeExponentUnbiased, ieeeMantissa53) = source.GetIeeeParts(out var _, out var _, out var _);

      var exponentScaleValue = 1L << int.Abs(ieeeExponentUnbiased - 52); // Used to scale the full-mantissa below.

      var integerPart = ieeeSign * ieeeMantissa53 / exponentScaleValue; // This is the same as ( sign * full-mantissa * 2^(6-52) ).

      var fractionalPart = source - integerPart; // Get the fractional part by subtracting the integer-part.

      var fractionalPartAsWhole = double.Round(fractionalPart * (long)System.Numerics.BigInteger.Pow(10, 15 - (int)integerPart.DigitCount(10))); // Rounding because truncating potentially distort the last digit.

      return (integerPart, fractionalPart, (long)fractionalPartAsWhole);
    }

    /// <summary>
    /// <para>Get the <paramref name="integralPart"/> and the <paramref name="fractionalPart"/> of the <paramref name="source"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Decimal"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_separator"/></para>
    /// <para><seealso href="https://stackoverflow.com/a/33996511/3178666"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (long IntegerPart, double FractionalPart) GetParts(this double source)
    {
      var integerPart = (long)double.Truncate(source);

      var fractionalPart = source - integerPart;

      return (integerPart, fractionalPart);
    }
  }
}
