namespace Flux
{
  /// <summary>
  /// <para><see href="https://rosettacode.org/wiki/Numerical_integration"/></para>
  /// </summary>
  public static partial class NumericalIntegration
  {
    public enum RectangularMethod
    {
      LeftRule,
      MidpointRule,
      RightRule,
    }

    /// <summary>
    /// <para>Riemann sum, a.k.a. rectangular rule.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Riemann_sum"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="n"></param>
    /// <param name="f"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static TFloat RiemannSum<TFloat, TInteger>(TFloat a, TFloat b, TInteger n, System.Func<TFloat, TFloat> f, RectangularMethod mode)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var r = b - a; // Range of interval.
      var t = TFloat.CreateChecked(n); // Parameter n in floating-point.
      var o = TFloat.CreateChecked((int)mode / 2); // Mode {0, 1, 2} offset, makes for {0, 0.5, 1}.

      var sum = TFloat.Zero;

      for (var i = TInteger.Zero; i < n; i++)
        sum += f(a + r * (TFloat.CreateChecked(i) + o) / t);

      return sum * r / t;
    }

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Simpson%27s_rule"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="n"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static TFloat SimpsonsRule<TFloat, TInteger>(TFloat a, TFloat b, TInteger n, System.Func<TFloat, TFloat> f)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var r = b - a; // Range of interval.
      var t = TFloat.CreateChecked(n); // Iteration threshold in floating-point.
      var h = TFloat.CreateChecked(0.5); // One half.

      var sum1 = f(a + r / (t * TFloat.CreateChecked(2)));
      var sum2 = TFloat.Zero;

      for (var i = TInteger.One; i < n; i++)
      {
        var ti = TFloat.CreateChecked(i);

        sum1 += f(a + r * (ti + h) / t);
        sum2 += f(a + r * ti / t);
      }

      return (f(a) + f(b) + sum1 * TFloat.CreateChecked(4) + sum2 * TFloat.CreateChecked(2)) * r / (t * TFloat.CreateChecked(6));
    }

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Trapezoidal_rule"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="n"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static TFloat TrapeziumRule<TFloat, TInteger>(TFloat a, TFloat b, TInteger n, System.Func<TFloat, TFloat> f)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var r = b - a; // Range of interval.
      var t = TFloat.CreateChecked(n); // Parameter n in floating-point.

      var sum = TFloat.Zero;

      for (var i = TInteger.One; i < n; i++)
        sum += f(a + r * TFloat.CreateChecked(i) / t);

      sum += (f(a) + f(b)) / TFloat.CreateChecked(2);

      return sum * r / t;
    }
  }
}
