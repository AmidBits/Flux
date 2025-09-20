//namespace Flux
//{
//  public static partial class IFloatingPointIeee754
//  {
//    extension<TFloat>(TFloat value)
//      where TFloat : System.Numerics.IFloatingPointIeee754<TFloat>
//    {
//      /// <summary>
//      /// <para><see href="https://en.wikipedia.org/wiki/Vincenty%27s_formulae"/></para>
//      /// </summary>
//      public TFloat HelmertsExpansionParameterK1()
//        => TFloat.Sqrt(TFloat.One + value * value) is var k ? (k - TFloat.One) / (k + TFloat.One) : throw new System.ArithmeticException();

//      /// <summary>
//      /// <para>Rescale logarithmic (Y) to linear (X).</para>
//      /// <example>
//      /// <code>var x = (1000.0).RescaleLogarithmicToLinear(300, 3000, 10, 12, 2);</code>
//      /// <code>x = 11.045757490560675</code>
//      /// </example>
//      /// </summary>
//      /// <typeparam name="TFloat"></typeparam>
//      /// <param name="value"></param>
//      /// <param name="y0"></param>
//      /// <param name="y1"></param>
//      /// <param name="x0"></param>
//      /// <param name="x1"></param>
//      /// <param name="radix"></param>
//      /// <returns></returns>
//      public TFloat RescaleLogarithmicToLinear(TFloat y0, TFloat y1, TFloat x0, TFloat x1, TFloat radix)
//        => TFloat.Log(value, radix).Rescale(TFloat.Log(y0, radix), TFloat.Log(y1, radix), x0, x1); // Extract the numbers and use the standard Rescale() function for the math.

//      /// <summary>
//      /// <para>Rescale linear (X) to logarithmic (Y).</para>
//      /// <example>
//      /// <code>var y = (7.5).RescaleLinearToLogarithmic(0.1, 10, 0.1, 10, 2);</code>
//      /// <code>y = 3.1257158496882371</code>
//      /// </example>
//      /// </summary>
//      /// <typeparam name="TFloat"></typeparam>
//      /// <param name="value"></param>
//      /// <param name="x0"></param>
//      /// <param name="x1"></param>
//      /// <param name="y0"></param>
//      /// <param name="y1"></param>
//      /// <param name="radix"></param>
//      /// <returns></returns>
//      public TFloat RescaleLinearToLogarithmic(TFloat x0, TFloat x1, TFloat y0, TFloat y1, TFloat radix)
//        => TFloat.Pow(radix, value.Rescale(x0, x1, TFloat.Log(y0, radix), TFloat.Log(y1, radix))); // Extract the numbers and use the standard Rescale() function for the math.

//    }
//  }
//}
