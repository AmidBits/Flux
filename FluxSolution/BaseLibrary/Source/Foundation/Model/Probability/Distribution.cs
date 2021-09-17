using System.Linq;

namespace Flux.Probability
{
  // Extension methods on distributions
  public static class Distribution
  {
    public static System.Collections.Generic.IEnumerable<T> Samples<T>(this IDistribution<T> d)
    {
      if (d is null) throw new System.ArgumentNullException(nameof(d));

      while (true)
        yield return d.Sample();
    }

    public static string Histogram(this IDistribution<double> d, double low, double high) =>
      d.Samples().Histogram(low, high);

    public static string Histogram<T>(this IDiscreteProbabilityDistribution<T> d)
      where T : notnull
      => d.Samples().DiscreteHistogram();

    public static string ShowWeights<T>(this IDiscreteProbabilityDistribution<T> d)
    {
      if (d is null) throw new System.ArgumentNullException(nameof(d));

      int labelMax = d.Support().Select(x => x?.ToString()?.Length ?? 0).Max();
      return d.Support().Select(s => $"{ToLabel(s)}:{d.Weight(s)}").NewlineSeparated();
      string ToLabel(T t) => t?.ToString()?.PadLeft(labelMax) ?? throw new System.ArgumentNullException(nameof(T));
    }

    public static IDiscreteProbabilityDistribution<TR> Select<TA, TR>(this IDiscreteProbabilityDistribution<TA> d, System.Func<TA, TR> projection)
      where TR : notnull
      => Projected<TA, TR>.Distribution(d, projection);

    public static IDiscreteProbabilityDistribution<T> ToUniform<T>(this System.Collections.Generic.IEnumerable<T> items)
      where T : notnull
    {
      var list = items.ToList();

      return StandardDiscreteUniform.Distribution(0, list.Count - 1).Select(i => list[i]);
    }
  }
}
