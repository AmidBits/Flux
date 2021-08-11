
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>A univariate quintic function, or fifth-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Quintic_function"/>
    public static double Quintic(double x, double a, double b, double c, double d, double e, double f)
      => (a * x * x * x * x * x) + (b * x * x * x * x) + (c * x * x * x) + (d * x * x) + (e * x) + f;
  }
}
