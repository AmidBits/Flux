#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
    public static TSelf InterpolateLinear<TSelf, TMu>(this TSelf v1, TSelf v2, TMu mu)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IMultiplyOperators<TSelf, TMu, TSelf>
      where TMu : System.Numerics.IFloatingPoint<TMu>
      => v1 * (TMu.One - mu) + v2 * mu;
  }
}
#endif
