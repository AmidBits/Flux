namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Converts an integer <paramref name="value"/> using base <paramref name="radix"/> to a decimal fraction, e.g. "123 => 0.123".</summary>
    public static double ConvertToDecimalFraction<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var fil = value.FastDigitCount(radix);

      var fip = radix.FastIntegerPow(fil, UniversalRounding.HalfTowardZero, out var _);

      return double.CreateChecked(value) / double.CreateChecked(fip);
    }
  }
}
