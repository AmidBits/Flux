
namespace Flux
{
  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumberBase<TNumber>
    {
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
        => minTarget + (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource);

      #endregion
    }
  }

  public static partial class FloatingPoint
  {
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
}
