namespace Flux
{
  public static partial class Math
  {
    /// <summary>Returns the cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static double Csc(double value)
      => 1 / System.Math.Sin(value);
    /// <summary>Returns the cosecant of the specified angle. Bonus result from sin(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static double Csc(double value, out double sin)
      => 1 / (sin = System.Math.Sin(value));
    /// <summary>Returns the secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static double Sec(double value)
      => 1 / System.Math.Cos(value);
    /// <summary>Returns the secant of the specified angle. Bonus result from cos(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static double Sec(double value, out double cos)
      => 1 / (cos = System.Math.Cos(value));
    /// <summary>Returns the cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static double Cot(double value)
      => 1 / System.Math.Tan(value);
    /// <summary>Returns the cotangent of the specified angle. Bonus result from tan(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static double Cot(double value, out double tan)
      => 1 / (tan = System.Math.Tan(value));

    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static double Acsc(double value)
      => System.Math.Asin(1 / value);
    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static double Acsc(double value, out double inverseValue)
      => System.Math.Asin(inverseValue = (1 / value));
    /// <summary>Returns the inverse secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static double Asec(double value)
      => System.Math.Acos(1 / value);
    /// <summary>Returns the inverse secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static double Asec(double value, out double inverseOfValue)
      => System.Math.Acos(inverseOfValue = (1 / value));
    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static double Acot(double value)
      => System.Math.Atan(1 / value);
    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static double Acot(double value, out double inverseOfValue)
      => System.Math.Atan(inverseOfValue = (1 / value));
  }
}
