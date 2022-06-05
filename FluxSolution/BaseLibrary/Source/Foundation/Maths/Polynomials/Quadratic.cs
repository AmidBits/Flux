
namespace Flux
{
  public static partial class Polynomial
  {
    /// <summary>A univariate quadratic function (standard form), or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static double Quadratic(double x, double a, double b, double c)
      => (a * x * x) + (b * x) + c;
    /// <summary>A univariate quadratic function (factored form), or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static double QuadraticFactored(double x, double a, double r1, double r2)
      => a * (x - r1) * (x - r2);
    /// <summary>Compute the root r1 from the univariate function.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static double QuadraticRootR1(double a, double b, double c)
      => (-b - System.Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
    /// <summary>Compute the root r2 from the univariate function.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static double QuadraticRootR2(double a, double b, double c) => (-b + System.Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
    /// <summary>A bivariate quadratic function, or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static double Quadratic(double x, double y, double a, double b, double c, double d, double e, double f)
      => (a * x * x) + (b * y * y) + (c * x * y) + (d * x) + (e * y) + f;
    /// <summary>A multivariate quadratic function, or second-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quadratic_function"/>
    public static double Quadratic(double x, double y, double z, double a, double b, double c, double d, double e, double f, double g, double h, double i, double j)
      => (a * x * x) + (b * y * y) + (c * z * z) + (d * x * y) + (e * x * z) + (f * y * z) + (g * x) + (h * y) + (i * z) + j;
  }
}
