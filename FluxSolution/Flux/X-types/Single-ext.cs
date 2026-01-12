namespace Flux
{
  public static partial class SingleExtensions
  {
    public const float MaxDefaultTolerance = +1e-6f;
    public const float MinDefaultTolerance = -1e-6f;

    /// <summary>
    /// <para>The largest integer that can be stored in a <see cref="System.Single"/> without losing precision is <c>16,777,216</c>.</para>
    /// <para>This is because a <see cref="System.Single"/> is a base-2/binary single-precision floating point with a 24-bit significand, which means it can precisely represent integers up to 16,777,216 = <c>(1 &lt;&lt; 24)</c> = 2²⁴, before precision starts to degrade.</para>
    /// </summary>
    public const float MaxPreciseInteger = 1 << SignificandBits;
    /// <summary>
    /// <para>The smallest integer that can be stored in a <see cref="System.Single"/> without losing precision is <c>-16,777,216</c>.</para>
    /// <para>This is because a <see cref="System.Single"/> is a base-2/binary single-precision floating point with a 24-bit significand, which means it can precisely represent integers down to -16,777,216 = <c>-(1 &lt;&lt; 24)</c> = -2²⁴, before precision starts to degrade.</para>
    /// </summary>
    public const float MinPreciseInteger = -MaxPreciseInteger;

    /// <summary>
    /// <para>The largest prime integer that precisely fit in a float.</para>
    /// </summary>
    public const float MaxPrimeNumber = 16777213;

    /// <summary>
    /// <para>The number of bits in the significand field.</para>
    /// </summary>
    /// <remarks>Although only 23 bits are explicitly stored, the complete significand is 24 bits (the extra bit is derived).</remarks>
    public const int SignificandBits = 24;

    /// <summary>
    /// <para>The single type has a precision of about 6-9 significant digits.</para>
    /// </summary>
    public const int SignificantDigits = 6;

    extension(System.Single)
    {
      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Single"/> without losing precision is <c>16,777,216</c>.</para>
      /// <para>This is because a <see cref="System.Single"/> is a base-2/binary single-precision floating point with a 24-bit mantissa, which means it can precisely represent integers up to 16,777,216 = <c>(1 &lt;&lt; 24)</c> = 2²⁴, before precision starts to degrade.</para>
      /// </summary>
      public static float MaxPreciseInteger => MaxPreciseInteger;
      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Single"/> without losing precision is <c>-16,777,216</c>.</para>
      /// <para>This is because a <see cref="System.Single"/> is a base-2/binary single-precision floating point with a 24-bit mantissa, which means it can precisely represent integers down to -16,777,216 = <c>-(1 &lt;&lt; 24)</c> = -2²⁴, before precision starts to degrade.</para>
      /// </summary>
      public static float MinPreciseInteger => MinPreciseInteger;

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a float.</para>
      /// </summary>
      public static float MaxPrimeNumber => MaxPrimeNumber;

      /// <summary>
      /// <para>The number of bits in the significand field of a <see cref="System.Single"/>.</para>
      /// </summary>
      /// <remarks>Although only 23 bits are explicitly stored, the complete significand is 24 bits (the extra bit is derived).</remarks>
      public static int SignificandBits => SignificandBits;

      /// <summary>
      /// <para>A <see cref="System.Single"/> has a precision of about 6-9 significant digits.</para>
      /// <para>The minimum number of significant digits of a <see cref="System.Single"/>, i.e. 6.</para>
      /// </summary>
      public static int SignificantDigits => SignificantDigits;
    }

    extension(System.Single)
    {
      /// <summary>
      /// <para>Get the three binary32 parts of a 32-bit floating point both raw (but shifted to LSB) as out parameters and returned adjusted (see below).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Single-precision_floating-point_format"/></para>
      /// </summary>
      /// <param name="binary32SignBit">This is 1 single sign bit. 0 = positive, 1 = negative.</param>
      /// <param name="binary32ExponentBiased">This is an 8-bit exponent in biased form, where the values [1, 254] (-126 to +127) represents the actual exponent. The two remaining values 0 (-127) and 255 (+128) are reserved for special numbers.</param>
      /// <param name="binary32Significand23">These are the stored 23 fraction bits of the significand. Please note that the total precision is actually 24 bits.</param>
      /// <returns>
      /// <para>The three adjusted binary32 parts as a tuple: <c>(int Binary32Sign = 1 or -1, int Binary32ExponentUnbiased = [−126, +127], long Binary32Significand24 = [0, <see cref="MaxPreciseInteger"/>])</c>.</para>
      /// </returns>
      public static (int Binary32Sign, int Binary32ExponentUnbiased, long Binary32Significand24) GetBinary32Components(System.Single value, out int binary32SignBit, out int binary32ExponentBiased, out int binary32Significand23)
      {
        var bits = System.BitConverter.SingleToUInt32Bits(value);

        binary32SignBit = (int)((bits & 0x80000000U) >>> 31);
        binary32ExponentBiased = (int)((bits & 0x7FF00000U) >>> 23);
        binary32Significand23 = (int)(bits & 0x007FFFFFU);

        return (
          binary32SignBit == 0 ? 1 : -1,
          binary32ExponentBiased - 127,
          (binary32ExponentBiased & 0x00100000) | binary32Significand23
        );
      }

      /// <summary>
      /// <para>Get the integral part and the fractional part of a <see cref="System.Single"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Decimal"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_separator"/></para>
      /// <para><seealso href="https://stackoverflow.com/a/33996511/3178666"/></para>
      /// </summary>
      /// <returns>
      /// <para>The integral (integer) part and the fractional part of a 32-bit floating point value.</para>
      /// </returns>
      public static (float IntegerPart, float FractionalPart) GetParts(System.Single value)
      {
        var integralPart = float.Truncate(value);
        var fractionalPart = value - integralPart;

        return (integralPart, fractionalPart);
      }
    }
  }
}
