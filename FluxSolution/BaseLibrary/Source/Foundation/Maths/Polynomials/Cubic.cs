
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>A third-degree polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Cubic_function"/>
    public static double Cubic(double x, double a, double b, double c, double d)
      => (a * x * x * x) + (b * x * x) + (c * x) + d;
  }
}