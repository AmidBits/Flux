namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Rescale logarithmic (Y) to linear (X).</para>
    /// </summary>
    /// <param name="y"></param>
    /// <param name="y0"></param>
    /// <param name="y1"></param>
    /// <param name="x0"></param>
    /// <param name="x1"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    // var x = (1000.0).RescaleLogarithmicToLinear(300, 3000, 10, 12, 2);
    // x = 11.045757490560675
    public static TNumber RescaleLogarithmicToLinear<TNumber>(this TNumber y, TNumber y0, TNumber y1, TNumber x0, TNumber x1, TNumber radix)
      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ILogarithmicFunctions<TNumber>
      => TNumber.Log(y, radix).Rescale(TNumber.Log(y0, radix), TNumber.Log(y1, radix), x0, x1); // Extract the numbers and use the standard Rescale() function for the math.

    /// <summary>
    /// <para>Rescale linear (X) to logarithmic (Y).</para>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="x0"></param>
    /// <param name="x1"></param>
    /// <param name="y0"></param>
    /// <param name="y1"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    // var y = (7.5).RescaleLinearToLogarithmic(0.1, 10, 0.1, 10, 2);
    // y = 3.1257158496882371
    public static TNumber RescaleLinearToLogarithmic<TNumber>(this TNumber x, TNumber x0, TNumber x1, TNumber y0, TNumber y1, TNumber radix)
      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ILogarithmicFunctions<TNumber>, System.Numerics.IPowerFunctions<TNumber>
      => TNumber.Pow(radix, x.Rescale(x0, x1, TNumber.Log(y0, radix), TNumber.Log(y1, radix))); // Extract the numbers and use the standard Rescale() function for the math.
  }
}
