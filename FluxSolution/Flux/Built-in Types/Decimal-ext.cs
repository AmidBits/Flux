namespace Flux
{
  public static partial class DecimalExtensions
  {
    /// <summary>
    /// <para>The largest integer that can be stored in a <see cref="System.Decimal"/> without losing precision is 79,228,162,514,264,337,593,543,950,335.</para>
    /// <para>The <see cref="System.Decimal"/> type is a base-10 high-precision decimal, which means it can precisely represent integers up to 79,228,162,514,264,337,593,543,950,335 (approximately 7.9×10²⁸). This is because the decimal type has a precision of 28-29 significant digits and does not use floating-point approximations for integers within this range. Beyond this value, precision may be lost.</para>
    /// </summary>
    public const decimal MaxPreciseInteger = 79228162514264337593543950335m;
    /// <summary>
    /// <para>The smallest integer that can be stored in a <see cref="System.Decimal"/> without losing precision is -79,228,162,514,264,337,593,543,950,335.</para>
    /// <para>The <see cref="System.Decimal"/> type is a base-10 high-precision decimal, which means it can precisely represent integers down to -79,228,162,514,264,337,593,543,950,335 (approximately -7.9×10²⁸). This is because the decimal type has a precision of 28-29 significant digits and does not use floating-point approximations for integers within this range. Beyond this value, precision may be lost.</para>
    /// </summary>
    public const decimal MinPreciseInteger = -79228162514264337593543950335m;

    /// <summary>
    /// <para>The largest prime integer that precisely fit in a decimal.</para>
    /// </summary>
    public const decimal MaxPrimeNumber = 79228162514264337593543950297m;

    //public const int SignificandBits = 96;

    ///// <summary>
    ///// <para>The decimal type has a precision of about 28-29 significant digits.</para>
    ///// </summary>
    //public const int SignificantDigits = 28;

    /// <summary>
    /// <para>The default epsilon scalar used for near-integer functions.</para>
    /// </summary>
    public const decimal DefaultEpsilonScalar = 1e-27m;

    extension(System.Decimal)
    {
      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Decimal"/> without losing precision is 79,228,162,514,264,337,593,543,950,335.</para>
      /// <para>The <see cref="System.Decimal"/> type is a base-10 high-precision decimal, which means it can precisely represent integers up to 79,228,162,514,264,337,593,543,950,335 (approximately 7.9×10²⁸). This is because the decimal type has a precision of 28-29 significant digits and does not use floating-point approximations for integers within this range. Beyond this value, precision may be lost.</para>
      /// </summary>
      public static decimal MaxPreciseInteger => MaxPreciseInteger;
      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Decimal"/> without losing precision is -79,228,162,514,264,337,593,543,950,335.</para>
      /// <para>The <see cref="System.Decimal"/> type is a base-10 high-precision decimal, which means it can precisely represent integers down to -79,228,162,514,264,337,593,543,950,335 (approximately -7.9×10²⁸). This is because the decimal type has a precision of 28-29 significant digits and does not use floating-point approximations for integers within this range. Beyond this value, precision may be lost.</para>
      /// </summary>
      public static decimal MinPreciseInteger => MinPreciseInteger;

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a decimal.</para>
      /// </summary>
      public static decimal MaxPrimeNumber => MaxPrimeNumber;

      ///// <summary>
      ///// <para>The decimal type has a precision of 28-29 significant digits.</para>
      ///// </summary>
      //public static int SignificantDigits => SignificantDigits;

      /// <summary>
      /// <para>The default epsilon scalar (1e-27m) used for near-integer functions.</para>
      /// </summary>
      public static decimal DefaultEpsilonScalar => DefaultEpsilonScalar;

      #region GetComponents

      /// <summary>
      /// <para>Gets the components that make up a decimal type, i.e. mantissa, scale and sign.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static (System.Numerics.BigInteger Significand, byte Scale, bool Sign) GetComponents(System.Decimal value)
      {
        var bits = decimal.GetBits(value);

        var bytes = System.Runtime.InteropServices.MemoryMarshal.Cast<int, byte>(bits);

        var significand = new System.Numerics.BigInteger(bytes[..12]);
        var scale = bytes[14];
        var sign = bytes[15] != 0;

        return (significand, scale, sign);
      }

      #endregion

      #region GetParts

      /// <summary>
      /// <para>Gets the parts that make up a decimal number, i.e. integer, fraction and fraction as an integer.</para>
      /// <para>Also returns the type components as out parameters, i.e. <paramref name="significand"/>, <paramref name="scale"/> and <paramref name="sign"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="significand"></param>
      /// <param name="scale"></param>
      /// <param name="sign"></param>
      /// <returns></returns>
      public static (System.Numerics.BigInteger IntegerPart, decimal FractionalPart, System.Numerics.BigInteger FractionalPartAsInteger) GetParts(System.Decimal value, out System.Numerics.BigInteger significand, out byte scale, out System.Numerics.BigInteger scaleFactor, out bool sign)
      {
        (significand, scale, sign) = GetComponents(value);

        scaleFactor = System.Numerics.BigInteger.Pow(10, scale);

        var integerPart = significand / scaleFactor;

        var fractionalPartAsInteger = significand - (integerPart * scaleFactor);

        return (
          integerPart,
          value - (decimal)integerPart,
          fractionalPartAsInteger
        );
      }

      /// <summary>
      /// <para>Get the <paramref name="integralPart"/> and the <paramref name="fractionalPart"/> as out parameters, and return the fractional part as a whole number (i.e. with the decimal point stripped).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Decimal"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_separator"/></para>
      /// </summary>
      /// <param name="number"></param>
      /// <returns>The parts of the decimal number (whole number or integer part, fractional part, fractional part as a whole number).</returns>
      /// <remarks>This operation is unique to decimal.</remarks>
      public static (System.Numerics.BigInteger IntegerPart, decimal FractionalPart, System.Numerics.BigInteger FractionalPartAsWholeNumber) GetParts(System.Decimal value)
        => GetParts(value, out var _, out var _, out var _, out var _);

      #endregion
    }
  }
}
