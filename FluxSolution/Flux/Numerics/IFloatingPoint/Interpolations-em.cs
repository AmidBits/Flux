namespace Flux
{
  public static partial class Interpolations
  {
    /// <summary>
    /// <para>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</para>
    /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="y1">Source point.</param>
    /// <param name="y2">Target point.</param>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
    /// <returns></returns>
    public static TFloat InterpolateCosine<TFloat>(this TFloat y1, TFloat y2, TFloat mu)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
    {
      var mu2 = (TFloat.One - TFloat.CosPi(mu)) / TFloat.CreateChecked(2);

      return InterpolateLinear(y1, y2, mu2);
    }

    /// <summary>
    /// <para></para>
    /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="y0"></param>
    /// <param name="y1"></param>
    /// <param name="y2"></param>
    /// <param name="y3"></param>
    /// <param name="mu"></param>
    /// <returns></returns>
    public static TFloat InterpolateCubic<TFloat>(this TFloat y0, TFloat y1, TFloat y2, TFloat y3, TFloat mu)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

    /// <summary>
    /// <para></para>
    /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="y0"></param>
    /// <param name="y1"></param>
    /// <param name="y2"></param>
    /// <param name="y3"></param>
    /// <param name="mu"></param>
    /// <returns></returns>
    public static TFloat InterpolateCubicPb<TFloat>(this TFloat y0, TFloat y1, TFloat y2, TFloat y3, TFloat mu)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var two = TFloat.CreateChecked(2);
      var half = TFloat.One / two;
      var oneAndHalf = two - half;

      var mu2 = mu * mu;

      var a0 = -half * y0 + oneAndHalf * y1 - oneAndHalf * y2 + half * y3;
      var a1 = y0 - (two + half) * y1 + two * y2 - half * y3;
      var a2 = -half * y0 + half * y2;
      var a3 = y1;

      return mu * mu2 * a0 + mu2 * a1 + mu * a2 + a3;
    }

    /// <summary>
    /// <para></para>
    /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="y0"></param>
    /// <param name="y1"></param>
    /// <param name="y2"></param>
    /// <param name="y3"></param>
    /// <param name="mu"></param>
    /// <param name="tension"></param>
    /// <param name="bias"></param>
    /// <returns></returns>
    public static TFloat InterpolateHermite<TFloat>(this TFloat y0, TFloat y1, TFloat y2, TFloat y3, TFloat mu, TFloat tension, TFloat bias)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var one = TFloat.One;
      var two = one + one;
      var three = two + one;

      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var biasP = (TFloat.One + bias) * (TFloat.One - tension);
      var biasN = (TFloat.One - bias) * (TFloat.One - tension);

      var m0 = (y1 - y0) * biasP / two + (y2 - y1) * biasN / two;
      var m1 = (y2 - y1) * biasP / two + (y3 - y2) * biasN / two;

      var a0 = two * mu3 - three * mu2 + one;
      var a1 = mu3 - two * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -two * mu3 + three * mu2;

      return a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2;
    }

    /// <summary>
    /// <para>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</para>
    /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
    /// </summary>
    public static TFloat InterpolateLinear<TFloat>(this TFloat y1, TFloat y2, TFloat mu)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => (TFloat.One - mu) * y1 + mu * y2;
  }
}
