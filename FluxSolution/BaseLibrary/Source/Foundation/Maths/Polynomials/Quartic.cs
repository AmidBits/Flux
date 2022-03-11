
namespace Flux
{
  public static partial class Polynomial
  {
    /// <summary>A univariate quartic function, or fourth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quartic_function"/>
    public static double Quartic(double x, double a, double b, double c, double d, double e)
      => (a * x * x * x * x) + (b * x * x * x) + (c * x * x) + (d * x) + e;
    public static double QuarticX(double x, double a, double b, double c, double d, double e)
      => (a * x * x * x * x) + CubicX(x, b, c, d, e);
  }
}
