#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    /// <summary>PREVIEW! Indicates whether the instance is single digit, i.e. in the range [-<paramref name="radix"/>, <paramref name="radix"/>].</summary>
    public static bool IsSingleDigit<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GenericMath.IsRadix(radix) && value < radix;
  }
}
#endif
