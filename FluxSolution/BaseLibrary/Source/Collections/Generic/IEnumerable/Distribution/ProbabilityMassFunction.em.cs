using System.Linq;

namespace Flux
{
  public static partial class SystemCollectionsGenericEm
  {
    // http://www.greenteapress.com/thinkstats/thinkstats.pdf

    /// <summary>The PMF is a function that maps from values to probabilities. Uses the specified comparer.</summary>
    public static double ProbabilityMassFunction<TValue>(this System.Collections.Generic.IEnumerable<TValue> source, TValue value, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var countTotal = 0;
      var countLessOrEqual = 0;

      foreach (var item in source)
      {
        countTotal++;
        if (comparer.Compare(item, value) <= 0)
          countLessOrEqual++;
      }

      return (double)countLessOrEqual / (double)countTotal;
    }

    /// <summary>The PMF is a function that maps from values to probabilities. Uses the default comparer.</summary>
    public static double ProbabilityMassFunction<TValue>(this System.Collections.Generic.IEnumerable<TValue> source, TValue value)
      => ProbabilityMassFunction(source, value, System.Collections.Generic.Comparer<TValue>.Default);

    /// <summary>The PMF is a function that maps from values to probabilities.</summary>
    public static System.Collections.Generic.IDictionary<TValue, double> ProbabilityMassFunction<TValue>(this System.Collections.Generic.IDictionary<TValue, int> source, int sumOfAllFrequencies, double factor = 1)
      where TValue : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var pmf = new System.Collections.Generic.Dictionary<TValue, double>();

      var denominator = (double)sumOfAllFrequencies;

      foreach (var kvp in source.OrderBy(kvp => kvp.Key))
      {
        sumOfAllFrequencies += kvp.Value;

        pmf.Add(kvp.Key, kvp.Value / denominator * factor);
      }

      return pmf;
    }
  }
}
