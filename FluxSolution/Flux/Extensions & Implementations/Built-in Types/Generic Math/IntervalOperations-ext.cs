namespace Flux
{
  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region InterpolateCubic

      /// <summary>
      /// <para>Cubic interpolation.</para>
      /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="y0"></param>
      /// <param name="y1"></param>
      /// <param name="y2"></param>
      /// <param name="y3"></param>
      /// <param name="mu"></param>
      /// <returns></returns>
      public static TFloat InterpolateCubic(TFloat y0, TFloat y1, TFloat y2, TFloat y3, TFloat mu)
      {
        var mu2 = mu * mu;

        var a0 = y3 - y2 - y0 + y1;
        var a1 = y0 - y1 - a0;
        var a2 = y2 - y0;
        var a3 = y1;

        return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
      }

      #endregion

      #region InterpolateCubicPb

      /// <summary>
      /// <para>Cubic interpolation a'la Paul Burke.</para>
      /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="y0"></param>
      /// <param name="y1"></param>
      /// <param name="y2"></param>
      /// <param name="y3"></param>
      /// <param name="mu"></param>
      /// <returns></returns>
      public static TFloat InterpolateCubicPb(TFloat y0, TFloat y1, TFloat y2, TFloat y3, TFloat mu)
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

      #endregion

      #region InterpolateHermite

      /// <summary>
      /// <para>Hermite interpolation.</para>
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
      public static TFloat InterpolateHermite(TFloat y0, TFloat y1, TFloat y2, TFloat y3, TFloat mu, TFloat tension, TFloat bias)
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

      #endregion
    }

    extension<TFloat>(TFloat)
    where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>
    {
      #region RescaleLogarithmicToLinear

      /// <summary>
      /// <para>Rescale logarithmic (Y) to linear (X).</para>
      /// <example>
      /// <code>var x = (1000.0).RescaleLogarithmicToLinear(300, 3000, 10, 12, 2);</code>
      /// <code>x = 11.045757490560675</code>
      /// </example>
      /// </summary>
      /// <param name="y0"></param>
      /// <param name="y1"></param>
      /// <param name="x0"></param>
      /// <param name="x1"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static TFloat RescaleLogarithmicToLinear(TFloat y, TFloat y0, TFloat y1, TFloat x0, TFloat x1, TFloat radix)
        => Number.Rescale(TFloat.Log(y, radix), TFloat.Log(y0, radix), TFloat.Log(y1, radix), x0, x1); // Extract the numbers and use the standard Rescale() function for the math.

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>, System.Numerics.IPowerFunctions<TFloat>
    {
      #region RescaleLinearToLogarithmic

      /// <summary>
      /// <para>Rescale linear (X) to logarithmic (Y).</para>
      /// <example>
      /// <code>var y = (7.5).RescaleLinearToLogarithmic(0.1, 10, 0.1, 10, 2);</code>
      /// <code>y = 3.1257158496882371</code>
      /// </example>
      /// </summary>
      /// <param name="x0"></param>
      /// <param name="x1"></param>
      /// <param name="y0"></param>
      /// <param name="y1"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static TFloat RescaleLinearToLogarithmic(TFloat x, TFloat x0, TFloat x1, TFloat y0, TFloat y1, TFloat radix)
      => TFloat.Pow(radix, Number.Rescale(x, x0, x1, TFloat.Log(y0, radix), TFloat.Log(y1, radix))); // Extract the numbers and use the standard Rescale() function for the math.

      #endregion
    }
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region InterpolateLinear

      /// <summary>
      /// <para>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</para>
      /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
      /// </summary>
      /// <param name="y0"></param>
      /// <param name="y1"></param>
      /// <param name="mu"></param>
      /// <returns></returns>
      public static TFloat InterpolateLinear<TFloat>(TNumber y0, TNumber y1, TFloat mu)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
        => (TFloat.One - mu) * TFloat.CreateChecked(y0) + mu * TFloat.CreateChecked(y1);

      #endregion

      #region InterpolateCosine

      /// <summary>
      /// <para>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</para>
      /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="y0">Source point.</param>
      /// <param name="y1">Target point.</param>
      /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
      /// <returns></returns>
      public static TFloat InterpolateCosine<TFloat>(TNumber y0, TNumber y1, TFloat mu)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
      {
        var mu2 = (TFloat.One - TFloat.CosPi(mu)) / TFloat.CreateChecked(2);

        return InterpolateLinear(y0, y1, mu2);
      }

      #endregion

      #region Rescale

      /// <summary>
      /// <para>Proportionally rescale the <paramref name="value"/> from the closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to the closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>].</para>
      /// <para>The <paramref name="value"/> retains its proportional interval ratio, e.g. a 5 from the closed interval [0, 10] becomes 50 when rescaled to the closed interval [0, 100].</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="minSource"></param>
      /// <param name="maxSource"></param>
      /// <param name="minTarget"></param>
      /// <param name="maxTarget"></param>
      /// <returns></returns>
      public static TNumber Rescale(TNumber value, TNumber minSource, TNumber maxSource, TNumber minTarget, TNumber maxTarget)
        => (value - minSource) / (maxSource - minSource) * (maxTarget - minTarget) + minTarget;

      #endregion

      #region Repel.. functions

      /// <summary>
      /// <para>Repels a value contained in the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>] to the exterior of the interval by moving it beyond the nearest endpoint by the specified margin.</para>
      /// <para>This function is the opposite of Clamp(<paramref name="minValue"/>, <paramref name="maxValue"/>). While clamping projects a value onto the closed interval, repelling projects interior values onto the exterior of the interval by moving them beyond the nearest endpoint.</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="rule"></param>
      /// <param name="margin"></param>
      /// <returns></returns>
      public static TNumber Repel(TNumber value, TNumber minValue, TNumber maxValue, NearestRoundingRule rule, TNumber margin)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(margin);

        if (value < minValue || value > maxValue)
          return value; // If number is already not in the interval, nothing to do but return it.

        var nearestValue = RoundToNearestValue(value, rule, false, minValue, maxValue);

        return (nearestValue == minValue)
          ? minValue - margin
          : (nearestValue == maxValue)
          ? maxValue + margin
          : nearestValue;
      }

      /// <summary>
      /// <para>Repels a value contained in the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>] to the exterior of the interval by moving it beyond the nearest endpoint by the specified ULPs.</para>
      /// <para>This function is the opposite of Clamp(<paramref name="minValue"/>, <paramref name="maxValue"/>). While clamping projects a value onto the closed interval, repelling projects interior values onto the exterior of the interval by moving them beyond the nearest endpoint.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="rule"></param>
      /// <param name="ulps"></param>
      /// <returns></returns>
      public static TNumber UlpRepel(TNumber value, TNumber minValue, TNumber maxValue, NearestRoundingRule rule, long ulps)
      {
        if (value < minValue || value > maxValue)
          return value; // If number is already spread, nothing to do but return it.

        var nearestValue = RoundToNearestValue(value, rule, false, minValue, maxValue);

        return (nearestValue == minValue)
          ? UlpDecrement(minValue, ulps)
          : (nearestValue == maxValue)
          ? UlpIncrement(maxValue, ulps)
          : nearestValue;
      }

      #endregion
    }
  }
}
