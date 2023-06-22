namespace Flux
{
  public static partial class Maths
  {
    #region Hyperbolic with inverse trigonometric functionality.

#if NET7_0_OR_GREATER

    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Coth<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Cosh(v) / TSelf.Sinh(v);

    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Sech<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.One / TSelf.Cosh(v);

    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Csch<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.One / TSelf.Sinh(v);

    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acoth<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ILogarithmicFunctions<TSelf>
      => TSelf.Log((x + TSelf.One) / (x - TSelf.One)).Divide(2);

    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Asech<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log((TSelf.One + TSelf.Sqrt(TSelf.One - (x * x))) / x);

    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acsch<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log(TSelf.One / x + TSelf.Sqrt(TSelf.One / (x * x) + TSelf.One));

#else

    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Coth(this double v)
      => System.Math.Cosh(v) / System.Math.Sinh(v);

    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Sech(this double v)
      => 1 / System.Math.Cosh(v);

    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Csch(this double v)
      => 1 / System.Math.Sinh(v);

    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Acoth(this double x)
      => System.Math.Log((x + 1) / (x - 1)) / 2;

    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Asech(this double x)
      => System.Math.Log((1 + System.Math.Sqrt(1 - (x * x))) / x);

    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Acsch(this double x)
      => System.Math.Log(1 / x + System.Math.Sqrt(1 / (x * x) + 1));

#endif

    #endregion // Hyperbolic with inverse trigonometric functionality.
  }
}
