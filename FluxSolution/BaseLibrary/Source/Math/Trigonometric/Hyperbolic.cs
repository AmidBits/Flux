namespace Flux
{
  public static partial class Math
  {
    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Csch(double value)
      => 2 / (System.Math.Exp(value) - System.Math.Exp(-value));
    /// <summary>Returns the hyperbolic cosecant of the specified angle. Bonus result from exp(value) and exp(-value) in out parameters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Csch(double value, out double exp, out double expn)
      => 2 / ((exp = System.Math.Exp(value)) - (expn = System.Math.Exp(-value)));
    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Sech(double value)
      => 2 / (System.Math.Exp(value) + System.Math.Exp(-value));
    /// <summary>Returns the hyperbolic secant of the specified angle. Bonus result from exp(value) and exp(-value) in out parameters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Sech(double value, out double exp, out double expn)
      => 2 / ((exp = System.Math.Exp(value)) + (expn = System.Math.Exp(-value)));
    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Coth(double value)
      => System.Math.Exp(value) is var evp && System.Math.Exp(-value) is var evn ? (evp + evn) / (evp - evn) : throw new System.ArithmeticException();
    /// <summary>Returns the hyperbolic cotangent of the specified angle. Bonus result from exp(value) and exp(-value) in out parameters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Coth(double value, out double exp, out double expn)
      => ((exp = System.Math.Exp(value)) + (expn = System.Math.Exp(-value))) / (exp - expn);

    /// <summary>Returns the inverse hyperbolic sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Asinh(double value)
#if NETCOREAPP
      => System.Math.Asinh(value);
#else
      => System.Math.Log(value + System.Math.Sqrt(value * value + 1));
#endif
    /// <summary>Returns the inverse hyperbolic cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Acosh(double value)
#if NETCOREAPP
      => System.Math.Acosh(value);
#else
      => System.Math.Log(value + System.Math.Sqrt(value * value - 1));
#endif
    /// <summary>Returns the inverse hyperbolic tangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Atanh(double value)
#if NETCOREAPP
      => System.Math.Atanh(value);
#else
      => System.Math.Log((1 + value) / (1 - value)) / 2;
#endif

    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Acsch(double value)
      => System.Math.Log((System.Math.Sign(value) * System.Math.Sqrt(value * value + 1) + 1) / value);
    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Asech(double value)
      => System.Math.Log((System.Math.Sqrt(-value * value + 1) + 1) / value);
    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Acoth(double value)
      => System.Math.Log((value + 1) / (value - 1)) / 2;
  }
}
