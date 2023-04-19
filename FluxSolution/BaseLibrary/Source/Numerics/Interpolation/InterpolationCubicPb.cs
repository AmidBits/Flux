namespace Flux.Interpolation
{
#if NET7_0_OR_GREATER

  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments. Generally referred to as Catmull-Rom splines.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class CubicInterpolationPb<TSelf>
    : I4NodeInterpolatable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    #region Static methods

    public static TSelf Interpolate(TSelf y0, TSelf y1, TSelf y2, TSelf y3, TSelf mu)
    {
      var two = TSelf.One + TSelf.One;
      var half = TSelf.One / two;
      var oneAndHalf = two - half;

      var mu2 = mu * mu;

      var a0 = -half * y0 + oneAndHalf * y1 - oneAndHalf * y2 + half * y3;
      var a1 = y0 - (two + half) * y1 + two * y2 - half * y3;
      var a2 = -half * y0 + half * y2;
      var a3 = y1;

      return mu * mu2 * a0 + mu2 * a1 + mu * a2 + a3;
    }

    #endregion Static methods

    #region Implemented interfaces

    public TSelf Interpolate4Node(TSelf y0, TSelf y1, TSelf y2, TSelf y3, TSelf mu)
      => Interpolate(y0, y1, y2, y3, mu);

    #endregion Implemented interfaces
  }

#else

  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments. Generally referred to as Catmull-Rom splines.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class CubicInterpolationPb
    : I4NodeInterpolatable
  {
    #region Static methods

    public static double Interpolate(double y0, double y1, double y2, double y3, double mu)
    {
      var two = 2d;
      var half = 1 / two;
      var oneAndHalf = two - half;

      var mu2 = mu * mu;

      var a0 = -half * y0 + oneAndHalf * y1 - oneAndHalf * y2 + half * y3;
      var a1 = y0 - (two + half) * y1 + two * y2 - half * y3;
      var a2 = -half * y0 + half * y2;
      var a3 = y1;

      return mu * mu2 * a0 + mu2 * a1 + mu * a2 + a3;
    }

    #endregion Static methods

    #region Implemented interfaces

    public double Interpolate4Node(double y0, double y1, double y2, double y3, double mu)
      => Interpolate(y0, y1, y2, y3, mu);

    #endregion Implemented interfaces
  }

#endif
}
