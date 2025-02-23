namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Converts the <paramref name="source"/> integer using base <paramref name="radix"/> to a decimal fraction, e.g. "123 => 0.123".</summary>
    public static double ConvertToDecimalFraction<TNumber, TRadix>(this TNumber source, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      source.FastDigitCount(radix, out var pow);

      return double.CreateChecked(source) / (double)pow;
    }
  }
}
