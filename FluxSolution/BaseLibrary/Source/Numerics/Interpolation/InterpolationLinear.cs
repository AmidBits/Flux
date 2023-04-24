namespace Flux.Interpolation
{
#if NET7_0_OR_GREATER

  /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class LinearInterpolation<TSelf>
    : I2NodeInterpolatable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    #region Static methods

    public static TSelf Interpolate(TSelf y1, TSelf y2, TSelf mu)
      => (TSelf.One - mu) * y1 + mu * y2;

    #endregion Static methods

    #region Implemented interfaces

    public TSelf Interpolate2Node(TSelf y1, TSelf y2, TSelf mu)
      => Interpolate(y1, y2, mu);

    #endregion Implemented interfaces
  }

#else

  /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class LinearInterpolation
    : I2NodeInterpolatable<double>
  {
    #region Static methods

    public static double Interpolate(double y1, double y2, double mu)
      => (1 - mu) * y1 + mu * y2;

    #endregion Static methods

    #region Implemented interfaces

    public double Interpolate2Node(double y1, double y2, double mu)
      => Interpolate(y1, y2, mu);

    #endregion Implemented interfaces
  }

#endif
}
