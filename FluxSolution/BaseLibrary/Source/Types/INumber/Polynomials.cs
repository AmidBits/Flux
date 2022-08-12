#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>A third-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Cubic_function"/>
    public static TSelf Cubic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c, TSelf d)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (a * x * x * x) + (b * x * x) + (c * x) + d;

    /// <summary>A degree one polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Linear_function"/>
    public static TSelf Linear<TSelf>(this TSelf x, TSelf a, TSelf b)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (a * x) + b;

    /// <summary>A univariate quadratic function (standard form), or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf Quadratic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (a * x * x) + (b * x) + c;

    /// <summary>A univariate quadratic function (factored form), or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf QuadraticFactored<TSelf>(this TSelf x, TSelf a, TSelf r1, TSelf r2)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => a * (x - r1) * (x - r2);

    /// <summary>Compute the root r1 from the univariate function.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf QuadraticRootR1<TSelf>(this TSelf a, TSelf b, TSelf c)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => (-b - TSelf.Sqrt(b * b - (TSelf.One + TSelf.One + TSelf.One + TSelf.One) * a * c)) / ((TSelf.One + TSelf.One) * a);

    /// <summary>Compute the root r2 from the univariate function.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf QuadraticRootR2<TSelf>(this TSelf a, TSelf b, TSelf c)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => (-b + TSelf.Sqrt(b * b - (TSelf.One + TSelf.One + TSelf.One + TSelf.One) * a * c)) / ((TSelf.One + TSelf.One) * a);

    /// <summary>A bivariate quadratic function, or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf Quadratic<TSelf>(this TSelf x, TSelf y, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e, TSelf f)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (a * x * x) + (b * y * y) + (c * x * y) + (d * x) + (e * y) + f;

    /// <summary>A multivariate quadratic function, or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static TSelf Quadratic<TSelf>(this TSelf x, TSelf y, TSelf z, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e, TSelf f, TSelf g, TSelf h, TSelf i, TSelf j)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (a * x * x) + (b * y * y) + (c * z * z) + (d * x * y) + (e * x * z) + (f * y * z) + (g * x) + (h * y) + (i * z) + j;

    /// <summary>A univariate quartic function, or fourth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quartic_function"/>
    public static TSelf Quartic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (a * x * x * x * x) + (b * x * x * x) + (c * x * x) + (d * x) + e;

    /// <summary>A univariate quintic function, or fifth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Sextic_function"/>
    public static TSelf Quintic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e, TSelf f)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (a * x * x * x * x * x) + (b * x * x * x * x) + (c * x * x * x) + (d * x * x) + (e * x) + f;

    /// <summary>A univariate septic function, or seventh-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Septic_function"/>
    public static TSelf Septic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e, TSelf f, TSelf g, TSelf h)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => (a * x * x * x * x * x * x * x) + (b * x * x * x * x * x * x) + (c * x * x * x * x * x) + (d * x * x * x * x) + (e * x * x * x) + (f * x * x) + (g * x) + h;

    /// <summary>A univariate sextic function, or sixth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quintic_function"/>
    public static TSelf Sextic<TSelf>(this TSelf x, TSelf a, TSelf b, TSelf c, TSelf d, TSelf e, TSelf f, TSelf g)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (a * x * x * x * x * x * x) + (b * x * x * x * x * x) + (c * x * x * x * x) + (d * x * x * x) + (e * x * x) + (f * x) + g;

    /// <summary>A polynomial in one indeterminate.</summary>
    /// <param name="x"></param>
    /// <param name="coefficients"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf Univariate<TSelf>(this TSelf x, params TSelf[] coefficients)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
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
