namespace Flux
{
  public static partial class Fx
  {
    /// <summary>A third-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Cubic_function"/>
    public static TNumber PolynomialCubic<TNumber>(this TNumber x, TNumber a, TNumber b, TNumber c, TNumber d)
      where TNumber : System.Numerics.INumber<TNumber>
      => a * x * x * x + PolynomialQuadratic(x, b, c, d);

    /// <summary>A degree one polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Linear_function"/>
    public static TNumber PolynomialLinear<TNumber>(this TNumber x, TNumber a, TNumber b)
      where TNumber : System.Numerics.INumber<TNumber>
      => a * x + b;

    /// <summary>A univariate quadratic function (standard form), or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TNumber PolynomialQuadratic<TNumber>(this TNumber x, TNumber a, TNumber b, TNumber c)
      where TNumber : System.Numerics.INumber<TNumber>
      => a * x * x + PolynomialLinear(x, b, c);

    /// <summary>A univariate quadratic function (factored form), or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TNumber PolynomialQuadraticFactored<TNumber>(this TNumber x, TNumber a, TNumber r1, TNumber r2)
      where TNumber : System.Numerics.INumber<TNumber>
      => a * (x - r1) * (x - r2);

    /// <summary>Compute the root r1 from the univariate function.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TNumber PolynomialQuadraticRootR1<TNumber>(this TNumber a, TNumber b, TNumber c)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IRootFunctions<TNumber>
      => (-b - TNumber.Sqrt(b * b - TNumber.CreateChecked(4) * a * c)) / (a + a);

    /// <summary>Compute the root r2 from the univariate function.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TNumber PolynomialQuadraticRootR2<TNumber>(this TNumber a, TNumber b, TNumber c)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IRootFunctions<TNumber>
      => (-b + TNumber.Sqrt(b * b - TNumber.CreateChecked(4) * a * c)) / (a + a);

    /// <summary>A bivariate quadratic function, or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TNumber PolynomialQuadratic<TNumber>(this TNumber x, TNumber y, TNumber a, TNumber b, TNumber c, TNumber d, TNumber e, TNumber f)
      where TNumber : System.Numerics.INumber<TNumber>
      => (a * x * x) + (b * y * y) + (c * x * y) + (d * x) + (e * y) + f;

    /// <summary>A multivariate quadratic function, or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TNumber PolynomialQuadratic<TNumber>(this TNumber x, TNumber y, TNumber z, TNumber a, TNumber b, TNumber c, TNumber d, TNumber e, TNumber f, TNumber g, TNumber h, TNumber i, TNumber j)
      where TNumber : System.Numerics.INumber<TNumber>
      => (a * x * x) + (b * y * y) + (c * z * z) + (d * x * y) + (e * x * z) + (f * y * z) + (g * x) + (h * y) + (i * z) + j;

    /// <summary>A univariate quartic function, or fourth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Quartic_function"/>
    public static TNumber PolynomialQuartic<TNumber>(this TNumber x, TNumber a, TNumber b, TNumber c, TNumber d, TNumber e)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IPowerFunctions<TNumber>
      => (a * x * x * x * x) + PolynomialCubic(x, b, c, d, e);

    /// <summary>A univariate quintic function, or fifth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Sextic_function"/>
    public static TNumber PolynomialQuintic<TNumber>(this TNumber x, TNumber a, TNumber b, TNumber c, TNumber d, TNumber e, TNumber f)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IPowerFunctions<TNumber>
      => (a * x * x * x * x * x) + PolynomialQuartic(x, b, c, d, e, f);

    /// <summary>A univariate septic function, or seventh-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Septic_function"/>
    public static TNumber PolynomialSeptic<TNumber>(this TNumber x, TNumber a, TNumber b, TNumber c, TNumber d, TNumber e, TNumber f, TNumber g, TNumber h)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IPowerFunctions<TNumber>
      => (a * x * x * x * x * x * x * x) + PolynomialSextic(x, b, c, d, e, f, g, h);

    /// <summary>A univariate sextic function, or sixth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see href="https://en.wikipedia.org/wiki/Quintic_function"/>
    public static TNumber PolynomialSextic<TNumber>(this TNumber x, TNumber a, TNumber b, TNumber c, TNumber d, TNumber e, TNumber f, TNumber g)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IPowerFunctions<TNumber>
      => (a * x * x * x * x * x * x) + PolynomialQuintic(x, b, c, d, e, f, g);

    /// <summary>A polynomial in one indeterminate.</summary>
    /// <param name="x"></param>
    /// <param name="coefficients"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber PolynomialUnivariate<TNumber>(this TNumber x, params TNumber[] coefficients)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IPowerFunctions<TNumber>
    {
      if (coefficients.Length <= 1) throw new System.ArgumentOutOfRangeException(nameof(coefficients)); // Must have at least 2 coefficients.

      var index = coefficients.Length - 1;

      var sum = coefficients[index--] + (coefficients[index--] * x);

      var exponent = TNumber.One + TNumber.One; // Use Pow() with 3 or more coefficients (i.e. with an exponent of 2 or more). More multiplications, potentially decrease accuracy.

      while (index >= 0)
        sum += (coefficients[index--] * TNumber.Pow(x, exponent++));

      return sum;
    }
  }
}
