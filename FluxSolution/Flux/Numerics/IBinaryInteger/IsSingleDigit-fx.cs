namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>Indicates whether the <paramref name="value"/> is single digit using the base <paramref name="radix"/>, i.e. in the interval (-<paramref name="radix"/>, +<paramref name="radix"/>).</summary>
    public static bool IsSingleDigit<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TInteger.Abs(value) < TInteger.CreateChecked(Units.Radix.AssertMember(radix));
  }
}
