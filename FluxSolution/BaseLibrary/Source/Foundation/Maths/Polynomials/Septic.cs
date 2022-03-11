
namespace Flux
{
  public static partial class Polynomial
  {
    /// <summary>A univariate septic function, or seventh-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Septic_function"/>
    public static double Septic(double x, double a, double b, double c, double d, double e, double f, double g, double h)
      => (a * x * x * x * x * x * x * x) + (b * x * x * x * x * x * x) + (c * x * x * x * x * x) + (d * x * x * x * x) + (e * x * x * x) + (f * x * x) + (g * x) + h;
    public static double SepticX(double x, double a, double b, double c, double d, double e, double f, double g, double h)
      => (a * x * x * x * x * x * x * x) + SexticX(x, b, c, d, e, f, g, h);
  }
}
