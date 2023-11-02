namespace Flux
{
  public static partial class Interpolation
  {
#if NET7_0_OR_GREATER

    /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
    /// <param name="y1">Source point.</param>
    /// <param name="y2">Target point.</param>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static TSelf InterpolateCosine<TSelf>(this TSelf y1, TSelf y2, TSelf mu)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
    {
      var mu2 = (TSelf.One - TSelf.CosPi(mu)).Divide(2);

      return InterpolateLinear(y1, y2, mu2);
    }

#else

    /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
    /// <param name="y1">Source point.</param>
    /// <param name="y2">Target point.</param>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
   public static double InterpolateCosine(this double y1, double y2, double mu)
    {
      var mu2 = (1 - System.Math.Cos(mu * System.Math.PI)) / 2;

      return InterpolateLinear(y1, y2, mu2);
    }

#endif
  }
}
