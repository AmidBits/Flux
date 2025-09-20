namespace Flux
{
  public static partial class Doubles
  {
    /// <summary>
    /// <para>Get the <paramref name="integralPart"/> and the <paramref name="fractionalPart"/> of the <paramref name="source"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Decimal"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_separator"/></para>
    /// <para><seealso href="https://stackoverflow.com/a/33996511/3178666"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (System.Numerics.BigInteger IntegerPart, double FractionalPart) GetParts(this double source)
    {
      var integerPart = double.Truncate(source);

      var fractionalPart = source - integerPart;

      return (System.Numerics.BigInteger.CreateChecked(integerPart), fractionalPart);
    }
  }
}
