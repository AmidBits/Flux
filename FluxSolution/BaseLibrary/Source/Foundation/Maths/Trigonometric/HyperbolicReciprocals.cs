namespace Flux
{
  public static partial class Maths
  {
    // Hyperbolic reciprocals:

    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Csch(double v)
      => 1 / System.Math.Sinh(v);
    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Sech(double v)
      => 1 / System.Math.Cosh(v);
    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Coth(double v)
      => System.Math.Cosh(v) / System.Math.Sinh(v);

    // Inverse hyperbolic reciprocals:

    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Acsch(double v)
      => System.Math.Asinh(1 / v); // System.Math.Log((1 / x) + System.Math.Sqrt((1 / x * x) + 1));
    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Asech(double v)
      => System.Math.Acosh(1 / v); // System.Math.Log((1 + System.Math.Sqrt(1 - x * x)) / x);
    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Acoth(double v)
      => System.Math.Atanh(1 / v); // System.Math.Log((x + 1) / (x - 1)) / 2;
  }
}
