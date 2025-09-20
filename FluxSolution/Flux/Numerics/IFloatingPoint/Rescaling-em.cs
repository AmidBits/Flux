namespace Flux
{
  public static partial class Rescaling
  {
    /// <summary>
    /// <para>Rescale logarithmic (Y) to linear (X).</para>
    /// <example>
    /// <code>var x = (1000.0).RescaleLogarithmicToLinear(300, 3000, 10, 12, 2);</code>
    /// <code>x = 11.045757490560675</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="y"></param>
    /// <param name="y0"></param>
    /// <param name="y1"></param>
    /// <param name="x0"></param>
    /// <param name="x1"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TFloat RescaleLogarithmicToLinear<TFloat>(this TFloat y, TFloat y0, TFloat y1, TFloat x0, TFloat x1, TFloat radix)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>
      => TFloat.Log(y, radix).Rescale(TFloat.Log(y0, radix), TFloat.Log(y1, radix), x0, x1); // Extract the numbers and use the standard Rescale() function for the math.

    /// <summary>
    /// <para>Rescale linear (X) to logarithmic (Y).</para>
    /// <example>
    /// <code>var y = (7.5).RescaleLinearToLogarithmic(0.1, 10, 0.1, 10, 2);</code>
    /// <code>y = 3.1257158496882371</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="x"></param>
    /// <param name="x0"></param>
    /// <param name="x1"></param>
    /// <param name="y0"></param>
    /// <param name="y1"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TFloat RescaleLinearToLogarithmic<TFloat>(this TFloat x, TFloat x0, TFloat x1, TFloat y0, TFloat y1, TFloat radix)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>, System.Numerics.IPowerFunctions<TFloat>
      => TFloat.Pow(radix, x.Rescale(x0, x1, TFloat.Log(y0, radix), TFloat.Log(y1, radix))); // Extract the numbers and use the standard Rescale() function for the math.
  }
}
