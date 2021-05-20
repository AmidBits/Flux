using System.Linq;

namespace Flux
{
  public static partial class SystemCollectionsGenericEm
  {
    // http://www.greenteapress.com/thinkstats/thinkstats.pdf

    /// <summary>The CDF is the function that maps values to their percentil rank, in a probability range [0, 1], in a distribution. Uses the specified comparer.</summary>
    public static double CumulativeMassFunction<TValue>(this System.Collections.Generic.IEnumerable<TValue> source, TValue value, System.Collections.Generic.IComparer<TValue> comparer)
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
    /// <summary>The CDF is the function that maps values to their percentil rank, in a probability range [0, 1], in a distribution. Uses the default comparer.</summary>
    public static double CumulativeMassFunction<TValue>(this System.Collections.Generic.IEnumerable<TValue> source, TValue value)
      => CumulativeMassFunction(source, value, System.Collections.Generic.Comparer<TValue>.Default);

    /// <summary>The CDF is the function that maps values to their percentile rank, in a probability range [0, 1], in a distribution.</summary>
    public static System.Collections.Generic.IDictionary<TValue, double> CumulativeMassFunction<TValue>(this System.Collections.Generic.IDictionary<TValue, int> source, int sumOfAllFrequencies, double factor = 1)
      where TValue : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var pmf = new System.Collections.Generic.Dictionary<TValue, double>();

      var counter = 0;

      foreach (var kvp in source.OrderBy(kvp => kvp.Key))
      {
        counter += kvp.Value;

        pmf.Add(kvp.Key, counter / (double)sumOfAllFrequencies * factor);
      }

      return pmf;
    }
  }
}
