
namespace Flux
{
  public static partial class Polynomial
  {
    /// <summary>A third-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Cubic_function"/>
    public static double Cubic(double x, double a, double b, double c, double d)
      => (a * x * x * x) + (b * x * x) + (c * x) + d;
    public static double CubicX(double x, double a, double b, double c, double d)
      => (a * x * x * x) + QuadraticX(x, b, c, d);
  }
}
