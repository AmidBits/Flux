namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Converts the <paramref name="source"/> integer using base <paramref name="radix"/> to a decimal fraction, e.g. "123 => 0.123".</summary>
    public static double ConvertToDecimalFraction<TNumber, TRadix>(this TNumber source, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var fil = source.FastDigitCount(radix);

      var fip = radix.FastIntegerPow(fil, UniversalRounding.HalfTowardZero, out var _);

      return double.CreateChecked(source) / double.CreateChecked(fip);
    }
  }
}
