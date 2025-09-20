namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>Converts an integer <paramref name="value"/> using base <paramref name="radix"/> to a decimal fraction, e.g. "123 => 0.123".</summary>
    public static double ConvertToDecimalFraction<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var fdc = value.FastDigitCount(radix);

      var fip = radix.FastIntegerPow(fdc, out var _).IpowTz;

      return double.CreateChecked(value) / double.CreateChecked(fip);
    }
  }
}
