namespace Flux
{
  public static partial class Interpolation
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para></para>
    /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
    /// </summary>
    /// <param name="y0"></param>
    /// <param name="y1"></param>
    /// <param name="y2"></param>
    /// <param name="y3"></param>
    /// <param name="mu"></param>
    /// <returns></returns>
    public static TSelf InterpolateCubic<TSelf>(this TSelf y0, TSelf y1, TSelf y2, TSelf y3, TSelf mu)
      where TSelf : System.Numerics.INumberBase<TSelf>
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

#else

    /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubic(this double y0, double y1, double y2, double y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

#endif
  }
}

