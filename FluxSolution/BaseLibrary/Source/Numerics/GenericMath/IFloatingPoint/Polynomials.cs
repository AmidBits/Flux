#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class FloatingPoint
  {
    /// <summary>PREVIEW! A third-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Cubic_function"/>
    public static TSelf PolynomialCubic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c, TSelf d)
      where TSelf : System.Numerics.INumberBase<TSelf>
        => (a * x * x * x) + (b * x * x) + (c * x) + d;
    //=> (a * x * x * x) + QuadraticPolynomial(x, b, c, d);

    /// <summary>PREVIEW! A degree one polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Linear_function"/>
    public static TSelf PolynomialLinear<TSelf>(this TSelf x, TSelf a, TSelf b)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => (a * x) + b;

    /// <summary>PREVIEW! A univariate quadratic function (standard form), or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf PolynomialQuadratic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => (a * x * x) + (b * x) + c;
    //=> (a * x * x) + LinearPolynomial(x, b, c);

    /// <summary>PREVIEW! A univariate quadratic function (factored form), or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf PolynomialQuadraticFactored<TSelf>(this TSelf x, TSelf a, TSelf r1, TSelf r2)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => a * (x - r1) * (x - r2);

    /// <summary>PREVIEW! Compute the root r1 from the univariate function.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf PolynomialQuadraticRootR1<TSelf>(this TSelf a, TSelf b, TSelf c)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => (-b - TSelf.Sqrt(b * b - (TSelf.One + TSelf.One + TSelf.One + TSelf.One) * a * c)) / ((TSelf.One + TSelf.One) * a);

    /// <summary>PREVIEW! Compute the root r2 from the univariate function.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf PolynomialQuadraticRootR2<TSelf>(this TSelf a, TSelf b, TSelf c)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => (-b + TSelf.Sqrt(b * b - (TSelf.One + TSelf.One + TSelf.One + TSelf.One) * a * c)) / ((TSelf.One + TSelf.One) * a);

    /// <summary>PREVIEW! A bivariate quadratic function, or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf PolynomialQuadratic<TSelf>(this TSelf x, TSelf y, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e, TSelf f)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => (a * x * x) + (b * y * y) + (c * x * y) + (d * x) + (e * y) + f;

    /// <summary>PREVIEW! A multivariate quadratic function, or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf PolynomialQuadratic<TSelf>(this TSelf x, TSelf y, TSelf z, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e, TSelf f, TSelf g, TSelf h, TSelf i, TSelf j)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => (a * x * x) + (b * y * y) + (c * z * z) + (d * x * y) + (e * x * z) + (f * y * z) + (g * x) + (h * y) + (i * z) + j;

    /// <summary>PREVIEW! A univariate quartic function, or fourth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quartic_function"/>
    public static TSelf PolynomialQuartic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => (a * x * x * x * x) + (b * x * x * x) + (c * x * x) + (d * x) + e;
    //=> (a * x * x * x * x) + CubicPolynomial(x, b, c, d, e);

    /// <summary>PREVIEW! A univariate quintic function, or fifth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Sextic_function"/>
    public static TSelf PolynomialQuintic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e, TSelf f)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => (a * x * x * x * x * x) + (b * x * x * x * x) + (c * x * x * x) + (d * x * x) + (e * x) + f;
    //=> (a * x * x * x * x * x) + QuarticPolynomial(x, b, c, d, e, f);

    /// <summary>PREVIEW! A univariate septic function, or seventh-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Septic_function"/>
    public static TSelf PolynomialSeptic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e, TSelf f, TSelf g, TSelf h)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => (a * x * x * x * x * x * x * x) + (b * x * x * x * x * x * x) + (c * x * x * x * x * x) + (d * x * x * x * x) + (e * x * x * x) + (f * x * x) + (g * x) + h;
    //=> (a * x * x * x * x * x * x * x) + SexticPolynomial(x, b, c, d, e, f, g, h);

    /// <summary>PREVIEW! A univariate sextic function, or sixth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quintic_function"/>
    public static TSelf PolynomialSextic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e, TSelf f, TSelf g)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => (a * x * x * x * x * x * x) + (b * x * x * x * x * x) + (c * x * x * x * x) + (d * x * x * x) + (e * x * x) + (f * x) + g;
    //=> (a * x * x * x * x * x * x) + QuinticPolynomial(x, b, c, d, e, f, g);

    /// <summary>PREVIEW! A polynomial in one indeterminate.</summary>
    /// <param name="x"></param>
    /// <param name="coefficients"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf PolynomialUnivariate<TSelf>(this TSelf x, params TSelf[] coefficients)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    {
      if (coefficients.Length < 2) throw new System.ArgumentOutOfRangeException(nameof(coefficients));

      var index = coefficients.Length - 1;

      var sum = coefficients[index--] + (coefficients[index--] * x);

      var exponent = TSelf.One + TSelf.One; // Use Pow() with 3 or more coefficients (i.e. with an exponent of 2 or more). More multiplications, potentially decrease accuracy.

      while (index >= 0)
        sum += (coefficients[index--] * TSelf.Pow(x, exponent++));

      return sum;
    }
  }
}
#endif
