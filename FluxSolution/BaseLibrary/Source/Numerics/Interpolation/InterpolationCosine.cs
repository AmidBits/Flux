namespace Flux.Interpolation
{
#if NET7_0_OR_GREATER

  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <param name="n1">Source point.</param>
  /// <param name="n2">Target point.</param>
  /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class CosineInterpolation<TSelf>
    : I2NodeInterpolatable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
  {
    #region Static methods

    public static TSelf Interpolate(TSelf y1, TSelf y2, TSelf mu)
    {
      var mu2 = (TSelf.One - TSelf.CosPi(mu)).Divide(2);

      return LinearInterpolation<TSelf>.Interpolate(y1, y2, mu2);
    }

    #endregion Static methods

    #region Implemented interfaces

    public TSelf Interpolate2Node(TSelf y1, TSelf y2, TSelf mu)
      => Interpolate(y1, y2, mu);

    #endregion Implemented interfaces
  }

#else

  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <param name="n1">Source point.</param>
  /// <param name="n2">Target point.</param>
  /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class CosineInterpolation<Bogus>
    : I2NodeInterpolatable<double>
  {
    #region Static methods

    public static double Interpolate(double y1, double y2, double mu)
    {
      var mu2 = (1 - System.Math.Cos(mu * System.Math.PI)) / 2;

      return LinearInterpolation<Bogus>.Interpolate(y1, y2, mu2);
    }

    #endregion Static methods

    #region Implemented interfaces

    public double Interpolate2Node(double y1, double y2, double mu)
      => Interpolate(y1, y2, mu);

    #endregion Implemented interfaces
  }

#endif
}
