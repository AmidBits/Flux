
namespace Flux
{
  public static partial class Polynomial
  {
    /// <summary>A univariate quintic function, or fifth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Sextic_function"/>
    public static double Quintic(double x, double a, double b, double c, double d, double e, double f)
      => (a * x * x * x * x * x) + (b * x * x * x * x) + (c * x * x * x) + (d * x * x) + (e * x) + f;
    public static double QuinticX(double x, double a, double b, double c, double d, double e, double f)
      => (a * x * x * x * x * x) + QuarticX(x, b, c, d, e, f);
  }
}
