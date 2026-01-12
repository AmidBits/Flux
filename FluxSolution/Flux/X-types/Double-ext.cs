namespace Flux
{
  public static partial class DoubleExtensions
  {
    public const double MaxDefaultTolerance = +1e-10d;
    public const double MinDefaultTolerance = -1e-10d;

    /// <summary>
    /// <para>The largest integer that can be stored in a <see cref="System.Double"/> without losing precision is <c>9,007,199,254,740,992</c>.</para>
    /// <para>This is because a <see cref="System.Double"/> is a base-2/binary double-precision floating point with a 53-bit significand and 15-16 digits of precision, which means it can precisely represent integers up to 9,007,199,254,740,992 = <c>(1 &lt;&lt; 53)</c> = 2⁵³, before precision starts to degrade.</para>
    /// </summary>
    public const double MaxPreciseInteger = 1L << SignificandBits;
    /// <summary>
    /// <para>The smallest integer that can be stored in a <see cref="System.Double"/> without losing precision is <c>-9,007,199,254,740,992</c>.</para>
    /// <para>This is because a <see cref="System.Double"/> is a base-2/binary double-precision floating point with a 53-bit significand and 15-16 digits of precision, which means it can precisely represent integers down to -9,007,199,254,740,992 = <c>-(1 &lt;&lt; 53)</c> = -2⁵³, before precision starts to degrade.</para>
    /// </summary>
    public const double MinPreciseInteger = -MaxPreciseInteger;

    /// <summary>
    /// <para>The largest prime integer that precisely fit in a double.</para>
    /// </summary>
    public const double MaxPrimeNumber = 9007199254740881;

    /// <summary>
    /// <para>The double type has a precision of about 15-17 significant digits.</para>
    /// </summary>
    /// <summary>
    /// <para>The double type has a precision of about 15-17 significant digits.</para>
    /// </summary>
    public const int SignificantDigits = 15;

    /// <summary>
    /// <para>The number of bits in the significand field.</para>
    /// </summary>
    /// <remarks>Although only 52 bits are explicitly stored, the complete significand is 53 bits (the extra bit is derived).</remarks>
    public const int SignificandBits = 53;

    extension(System.Double)
    {
      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Double"/> without losing precision is <c>9,007,199,254,740,992</c>.</para>
      /// <para>This is because a <see cref="System.Double"/> is a base-2/binary double-precision floating point with a 53-bit mantissa and 15-16 digits of precision, which means it can precisely represent integers up to 9,007,199,254,740,992 = <c>(1 &lt;&lt; 53)</c> = 2⁵³, before precision starts to degrade.</para>
      /// </summary>
      public static double MaxPreciseInteger => MaxPreciseInteger;

      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Double"/> without losing precision is <c>-9,007,199,254,740,992</c>.</para>
      /// <para>This is because a <see cref="System.Double"/> is a base-2/binary double-precision floating point with a 53-bit mantissa and 15-16 digits of precision, which means it can precisely represent integers down to -9,007,199,254,740,992 = <c>-(1 &lt;&lt; 53)</c> = -2⁵³, before precision starts to degrade.</para>
      /// </summary>
      public static double MinPreciseInteger => MinPreciseInteger;

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a double.</para>
      /// </summary>
      public static double MaxPrimeNumber => MaxPrimeNumber;

      /// <summary>
      /// <para>The total number of bits in the significand field of a <see cref="System.Double"/>.</para>
      /// </summary>
      /// <remarks>Although only 52 bits are explicitly stored, the complete significand is 53 bits (the extra bit is derived).</remarks>
      public static int SignificandBits => SignificandBits;

      /// <summary>
      /// <para>A <see cref="System.Double"/> has a precision of about 15-17 significant digits.</para>
      /// <para>The minimum number of significant digits of a <see cref="System.Double"/>.</para>
      /// </summary>
      public static int SignificantDigits => SignificantDigits;
    }

    extension(System.Double)
    {
      /// <summary>
      /// <para>Get the three binary64 parts of a 64-bit floating point both raw (but shifted to LSB) as out parameters and returned adjusted (see below).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Double-precision_floating-point_format"/></para>
      /// </summary>
      /// <param name="binary64SignBit">This is 1 single sign bit. 0 = positive, 1 = negative.</param>
      /// <param name="binary64ExponentBiased">This is an 8-bit exponent in biased form, where the values [1, 2046] (-1022 to +1023) represents the actual exponent. The two remaining values 0 (-1023) and 2047 (+1024) are reserved for special numbers.</param>
      /// <param name="binary64Significand52"></param>
      /// <returns>
      /// <para>The three adjusted binary64 parts as a tuple: <c>(int Binary64Sign = 1 or -1, int Binary64ExponentUnbiased = [−1022, +1023], long Binary64Significand53 = [0, <see cref="MaxPreciseInteger"/>])</c>.</para>
      /// </returns>
      public static (int Binary64Sign, int Binary64ExponentUnbiased, long Binary64Significand53) GetBinary64Components(System.Double value, out int binary64SignBit, out int binary64ExponentBiased, out long binary64Significand52)
      {
        var bits = System.BitConverter.DoubleToUInt64Bits(value);

        binary64SignBit = (int)((bits & 0x8000000000000000UL) >>> 63);

        binary64ExponentBiased = (int)((bits & 0x7FF0000000000000UL) >>> 52);

        binary64Significand52 = (long)(bits & 0x000FFFFFFFFFFFFFUL);

        var sign = binary64SignBit == 0 ? 1 : -1;

        var exponentUnbiased = binary64ExponentBiased - 1023;

        var significand53 = 0x0010000000000000L | binary64Significand52; // This is the significandPrecision above with the hidden 53-bit added.

        return (sign, exponentUnbiased, significand53);
      }

      /// <summary>
      /// <para>Get the integral part and the fractional part of a <see cref="System.Double"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Decimal"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_separator"/></para>
      /// <para><seealso href="https://stackoverflow.com/a/33996511/3178666"/></para>
      /// </summary>
      /// <returns>
      /// <para>The integral (integer) part and the fractional part of a 64-bit floating point value.</para>
      /// </returns>
      public static (double IntegralPart, double FractionalPart) GetParts(System.Double value)
      {
        var integralPart = double.Truncate(value);
        var fractionalPart = value - integralPart;

        return (integralPart, fractionalPart);
      }
    }
  }
}
