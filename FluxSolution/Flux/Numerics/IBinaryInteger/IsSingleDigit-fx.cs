namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether the <paramref name="value"/> is single digit using the base <paramref name="radix"/>, i.e. in the interval (-<paramref name="radix"/>, +<paramref name="radix"/>).</summary>
    public static bool IsSingleDigit<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TValue.Abs(value) < TValue.CreateChecked(Units.Radix.AssertWithin(radix));
  }
}
