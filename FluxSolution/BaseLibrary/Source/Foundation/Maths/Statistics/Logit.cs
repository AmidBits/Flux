namespace Flux
{
  public static partial class Maths
  {
    /// <summary>The logit function, which is the inverser of expit (or the logistic function), is the logarithm of the odds (p / (1 − p)) where p is the probability. Creates a map of probability values from [0, 1] to [-infinity, +infinity].</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logit"/>
    /// <param name="probability">The probability in the range [0, 1].</param>
    /// <returns>The odds of the specified probablility in the range [-infinity, +infinity].</returns>
    public static double Logit(double probability)
      => System.Math.Log(probability / (1 - probability));
  }
}
