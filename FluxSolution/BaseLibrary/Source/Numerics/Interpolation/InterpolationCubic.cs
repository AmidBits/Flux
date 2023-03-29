namespace Flux.Interpolation
{
  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class CubicInterpolation<TSelf>
    : I4NodeInterpolatable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    #region Static methods
    public static TSelf Interpolate(TSelf y0, TSelf y1, TSelf y2, TSelf y3, TSelf mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }
    #endregion Static methods

    #region Implemented interfaces
    public TSelf Interpolate4Node(TSelf y0, TSelf y1, TSelf y2, TSelf y3, TSelf mu) => Interpolate(y0, y1, y2, y3, mu);
    #endregion Implemented interfaces
  }
}
