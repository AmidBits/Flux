namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Indicates whether the <paramref name="value"/> is single digit using the base <paramref name="radix"/>, i.e. in the interval (-<paramref name="radix"/>, +<paramref name="radix"/>).</summary>
    public static bool IsSingleDigit<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.Abs(value) < TNumber.CreateChecked(Units.Radix.AssertWithin(radix));
  }
}
