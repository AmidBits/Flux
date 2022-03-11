
namespace Flux
{
  public static partial class Polynomial
  {
    /// <summary>A degree one polynomial.</summary>
    /// <param name="a">a != 0</param>
    /// <see cref="https://en.wikipedia.org/wiki/Linear_function"/>
    public static double Linear(double x, double a, double b)
      => (a * x) + b;
  }
}
