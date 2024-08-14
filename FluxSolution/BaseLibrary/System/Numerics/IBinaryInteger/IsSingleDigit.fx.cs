namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether the <paramref name="number"/> is single digit using the base <paramref name="radix"/>, i.e. in the range [-<paramref name="radix"/>, <paramref name="radix"/>].</summary>
    public static bool IsSingleDigit<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => Quantities.Radix.AssertMember(radix) > TSelf.Abs(number);
  }
}
