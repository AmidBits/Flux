namespace Flux
{
  public static partial class InterpolationExtensionMethods
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
    /// <param name="tension"></param>
    /// <param name="bias"></param>
    /// <returns></returns>
    public static TSelf InterpolateHermite<TSelf>(this TSelf y0, TSelf y1, TSelf y2, TSelf y3, TSelf mu, TSelf tension, TSelf bias)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var one = TSelf.One;
      var two = one + one;
      var three = two + one;

      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var biasP = (TSelf.One + bias) * (TSelf.One - tension);
      var biasN = (TSelf.One - bias) * (TSelf.One - tension);

      var m0 = (y1 - y0) * biasP / two + (y2 - y1) * biasN / two;
      var m1 = (y2 - y1) * biasP / two + (y3 - y2) * biasN / two;

      var a0 = two * mu3 - three * mu2 + one;
      var a1 = mu3 - two * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -two * mu3 + three * mu2;

      return a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2;
    }

#else

    public static double InterpolateHermite(this double y0, double y1, double y2, double y3, double mu, double tension, double bias)
    {
      var one = 1d;
      var two = 2d;
      var three = two + one;

      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var biasP = (1 + bias) * (1 - tension);
      var biasN = (1 - bias) * (1 - tension);

      var m0 = (y1 - y0) * biasP / two + (y2 - y1) * biasN / two;
      var m1 = (y2 - y1) * biasP / two + (y3 - y2) * biasN / two;

      var a0 = two * mu3 - three * mu2 + one;
      var a1 = mu3 - two * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -two * mu3 + three * mu2;

      return a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2;
    }

#endif
  }
}

