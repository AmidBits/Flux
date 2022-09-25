#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    public static TSelf InterpolateCubicPb<TSelf, TMu>(this TSelf v0, TSelf v1, TSelf v2, TSelf v3, TMu mu)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IMultiplyOperators<TSelf, TMu, TSelf>
      where TMu : System.Numerics.IFloatingPoint<TMu>
    {
      var two = TSelf.One + TSelf.One;
      var half = TSelf.One / two;
      var oneAndHalf = two - half;

      var mu2 = mu * mu;

      var a0 = -half * v0 + oneAndHalf * v1 - oneAndHalf * v2 + half * v3;
      var a1 = v0 - (two + half) * v1 + two * v2 - half * v3;
      var a2 = -half * v0 + half * v2;
      var a3 = v1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }
  }
}
#endif
