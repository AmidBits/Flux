namespace Flux
{
  public static partial class Decimals
  {
    /// <summary>
    /// <para>Gets the parts that make up a decimal number, i.e. integer, fraction and fraction as an integer.</para>
    /// <para>Also returns the type components as out parameters, i.e. <paramref name="mantissa"/>, <paramref name="scale"/> and <paramref name="sign"/>.</para>
    /// </summary>
    /// <param name="number"></param>
    /// <param name="mantissa"></param>
    /// <param name="scale"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    public static (System.Numerics.BigInteger IntegerPart, decimal FractionalPart, System.Numerics.BigInteger FractionalPartAsInteger) GetParts(this decimal number, out System.Numerics.BigInteger mantissa, out byte scale, out bool sign)
    {
      (mantissa, scale, sign) = number.GetComponents();

      var scalingFactor = System.Numerics.BigInteger.Pow(10, scale);

      var integerPart = mantissa / scalingFactor;

      var fractionalPartAsInteger = mantissa - (integerPart * scalingFactor);

      return (
        integerPart,
        (decimal)fractionalPartAsInteger / (decimal)scalingFactor,
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
    public static (System.Numerics.BigInteger IntegerPart, decimal FractionalPart, System.Numerics.BigInteger FractionalPartAsWholeNumber) GetParts(this decimal number)
      => number.GetParts(out var _, out var _, out var _);
  }
}
