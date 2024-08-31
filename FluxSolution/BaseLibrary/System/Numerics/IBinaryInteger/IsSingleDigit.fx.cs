namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether the <paramref name="value"/> is single digit using the base <paramref name="radix"/>, i.e. in the range [-<paramref name="radix"/>, <paramref name="radix"/>].</summary>
    public static bool IsSingleDigit<TValue>(this TValue value, TValue radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => Quantities.Radix.AssertMember(radix) > TValue.Abs(value);
  }
}
