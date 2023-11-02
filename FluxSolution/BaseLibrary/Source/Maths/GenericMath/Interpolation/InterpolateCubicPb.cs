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
    public static TSelf InterpolateCubicPb<TSelf>(this TSelf y0, TSelf y1, TSelf y2, TSelf y3, TSelf mu)
      where TSelf : System.Numerics.INumberBase<TSelf>
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

#else

    public static double InterpolateCubicPb(this double y0, double y1, double y2, double y3, double mu)
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

#endif
  }
}

