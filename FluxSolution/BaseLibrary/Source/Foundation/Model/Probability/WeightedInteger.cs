using System.Linq;

namespace Flux.Probabilities
{
  // Weighted integer distribution using alias method.
  public sealed class WeightedInteger
    : IDiscreteProbabilityDistribution<int>
  {
    private readonly System.Collections.Generic.List<int> weights;
    private readonly IDistribution<int>[] distributions;

    public static IDiscreteProbabilityDistribution<int> Distribution(params int[] weights)
      => Distribution((System.Collections.Generic.IEnumerable<int>)weights);

    public static IDiscreteProbabilityDistribution<int> Distribution(System.Collections.Generic.IEnumerable<int> weights)
    {
      System.Collections.Generic.List<int> w = weights.ToList();
      if (w.Any(x => x < 0) || !w.Any(x => x > 0))
        throw new System.ArgumentOutOfRangeException(nameof(weights));
      if (w.Count == 1)
        return Singleton<int>.Distribution(0);
      if (w.Count == 2)
        return Bernoulli.Distribution(w[0], w[1]);
      return new WeightedInteger(w);
    }

    private WeightedInteger(System.Collections.Generic.IEnumerable<int> weights)
    {
      this.weights = weights.ToList();
      int s = this.weights.Sum();
      int n = this.weights.Count;
      this.distributions = new IDistribution<int>[n];
      var lows = new System.Collections.Generic.Dictionary<int, int>();
      var highs = new System.Collections.Generic.Dictionary<int, int>();
      for (int i = 0; i < n; i += 1)
      {
        int w = this.weights[i] * n;
        if (w == s)
          distributions[i] = Singleton<int>.Distribution(i);
        else if (w < s)
          lows.Add(i, w);
        else
          highs.Add(i, w);
      }
      while (lows.Any())
      {
        var low = lows.First();
        lows.Remove(low.Key);
        var high = highs.First();
        highs.Remove(high.Key);
        int lowNeeds = s - low.Value;
        this.distributions[low.Key] = Bernoulli.Distribution(low.Value, lowNeeds).Select(x => x == 0 ? low.Key : high.Key);
        int newHigh = high.Value - lowNeeds;
        if (newHigh == s)
          this.distributions[high.Key] = Singleton<int>.Distribution(high.Key);
        else if (newHigh < s)
          lows[high.Key] = newHigh;
        else
          highs[high.Key] = newHigh;
      }
    }

    public System.Collections.Generic.IEnumerable<int> Support() => System.Linq.Enumerable.Range(0, weights.Count).Where(x => weights[x] != 0);

    public int Weight(int i) =>
        0 <= i && i < weights.Count ? weights[i] : 0;

    public int Sample()
    {
      int i = StandardDiscreteUniform.Distribution(0, weights.Count - 1).Sample();
      return distributions[i].Sample();
    }

    public override string ToString() => $"WeightedInteger[{weights.CommaSeparated()}]";
  }
}
