namespace Flux
{
  public static partial class Maths
  {
    /// <summary>A logistic function or logistic curve is a common "S" shape (sigmoid curve).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logistic_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Sigmoid_function"/>
    /// <remarks>The standard logistic function is the logistic function with parameters (k = 1, x0 = 0, L = 1), a.k.a. sigmoid function.</remarks>
    /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity] (x).</param>
    /// <param name="k">The logistic growth rate or steepness of the curve (k).</param>
    /// <param name="x0">The x-value of the sigmoid's midpoint (x0).</param>
    /// <param name="L">The curve's maximum value (L).</param>
    public static double Logistic(double x, double k = 1, double x0 = 0, double L = 1)
      => L / (System.Math.Exp(-(k * (x - x0))) + 1);
  }
}
