#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public sealed class InterpolationLinear<TNode, TMu>
    : I2NodeInterpolatable<TNode, TMu>
    where TNode : System.Numerics.INumber<TNode>
    where TMu : System.Numerics.IFloatingPoint<TMu>, System.Numerics.IMultiplyOperators<TMu, TNode, TMu>
  {
    public TMu Interpolate(TNode n1, TNode n2, TMu mu)
      => (TMu.One - mu) * n1 + mu * n2;
  }
}
#endif