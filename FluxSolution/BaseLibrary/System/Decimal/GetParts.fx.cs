namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Get the <paramref name="integralPart"/> and the <paramref name="fractionalPart"/> as out parameters, and return the fractional part as a whole number (i.e. with the decimal point stripped).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Decimal"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_separator"/></para>
    /// </summary>
    /// <param name="number"></param>
    /// <returns>The parts of the decimal number (whole number or integer part, fractional part, fractional part as a whole number).</returns>
    /// <remarks>This operation is unique to decimal.</remarks>
    public static (decimal IntegralPart, decimal FractionalPart, decimal FractionalPartAsWholeNumber) GetParts(this decimal number)
    {
      var integralPart = decimal.Truncate(number);
      var fractionalPart = number - integralPart;

      return (integralPart, fractionalPart, fractionalPart * (decimal)System.Math.Pow(10, System.BitConverter.GetBytes(decimal.GetBits(number)[3])[2]));
    }
  }
}
