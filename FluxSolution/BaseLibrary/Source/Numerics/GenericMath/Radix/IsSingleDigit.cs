#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    /// <summary>PREVIEW! Indicates whether the instance is single digit, i.e. in the range [-9, 9].</summary>
    public static bool IsSingleDigit<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GenericMath.AssertRadix(radix) == radix && value < radix;
  }
}
#endif
