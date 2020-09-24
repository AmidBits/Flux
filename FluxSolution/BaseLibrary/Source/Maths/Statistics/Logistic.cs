namespace Flux
{
  public static partial class Maths
  {
    /// <summary>A logistic function or logistic curve is a common "S" shape (sigmoid curve).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logistic_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Sigmoid_function"/>
    /// <remarks>The standard logistic function is the logistic function with parameters (k = 1, x0 = 0, L = 1), a.k.a. sigmoid function.</remarks>
    /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity].</param>
    /// <param name="k">The logistic growth rate or steepness of the curve.</param>
    /// <param name="x0">The x-value of the sigmoid's midpoint.</param>
    /// <param name="L">The curve's maximum value.</param>
    public static double Logistic(double x, double k = 1, double x0 = 0, double L = 1)
      => L / (System.Math.Exp(-(k * (x - x0))) + 1);

    /// <summary>The inverse logit also yields the logistic function of any number x (i.e. this is the same as the logistic function with default arguments).</summary>
    /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity].</param>
    public static double Logistic(double x)
      => 1 / (System.Math.Exp(-x) + 1);

    /// <summary>The logit function is the logarithm of the odds (p / (1 − p)) where p is the probability. Creates a map of probability values from [0, 1] to [-infinity, +infinity]. It is the inverse of the Logistic function (above).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logit"/>
    /// <param name="probability">The probability in the range [0, 1].</param>
    /// <returns>The odds of the specified probablility in the range [-infinity, +infinity].</returns>
    public static double Logit(double probability)
      => System.Math.Log(Odds(probability));

    public static double Odds(double probability)
      => probability / (1.0 - probability);
  }
}
