#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns whether two numbers are co-prime.</summary>
    public static bool IsCoprime<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GreatestCommonDivisor(a, b) == TSelf.One;
  }
}
#endif