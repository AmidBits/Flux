namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Computes the odds (p / (1 − p)) of a probability p.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logit"/>
    /// <param name="probability">The probability in the range [0, 1].</param>
    /// <returns>The odds of the specified probablility in the range [-infinity, +infinity].</returns>
    public static double Odds(double probability)
      => probability / (1 - probability);
  }
}
