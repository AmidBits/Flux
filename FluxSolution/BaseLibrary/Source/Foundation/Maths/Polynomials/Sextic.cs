
namespace Flux
{
  public static partial class Polynomial
  {
    /// <summary>A univariate sextic function, or sixth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quintic_function"/>
    public static double Sextic(double x, double a, double b, double c, double d, double e, double f, double g)
      => (a * x * x * x * x * x * x) + (b * x * x * x * x * x) + (c * x * x * x * x) + (d * x * x * x) + (e * x * x) + (f * x) + g;
    public static double SexticX(double x, double a, double b, double c, double d, double e, double f, double g)
      => (a * x * x * x * x * x * x) + QuinticX(x, b, c, d, e, f, g);
  }
}
