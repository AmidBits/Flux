#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Indicates whether the instance is single digit, i.e. in the range [-<paramref name="b"/>, <paramref name="b"/>].</summary>
    public static bool IsSingleDigit<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => IsRadix(b) && x < b;
  }
}
#endif
