
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the probability that specified event count in a group of total event count are all different (or unique). It's complementary (1 - ProbabilityOfNoDuplicates) yields the probability that at least 2 events are the equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfNoDuplicates(System.Numerics.BigInteger whenCount, System.Numerics.BigInteger ofTotalCount)
    {
      double accumulation = 1;

      for (var index = ofTotalCount - whenCount + 1; index < ofTotalCount; index++)
        accumulation *= (double)index / (double)ofTotalCount;

      return accumulation;
    }
    /// <summary>Returns the probability that at least 2 events are equal.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static double ProbabilityOfDuplicates(System.Numerics.BigInteger whenCount, System.Numerics.BigInteger ofTotalCount)
      => 1 - ProbabilityOfNoDuplicates(whenCount, ofTotalCount);
  }
}
