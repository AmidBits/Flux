using System.Linq;

namespace Flux.Probability
{
  public sealed class Projected<TA, TR> 
    : IDiscreteProbabilityDistribution<TR>
    where TR : notnull
  {
    private readonly IDiscreteProbabilityDistribution<TA> underlying;
    private readonly System.Func<TA, TR> projection;
    private readonly System.Collections.Generic.Dictionary<TR, int> weights;

    public static IDiscreteProbabilityDistribution<TR> Distribution(IDiscreteProbabilityDistribution<TA> underlying, System.Func<TA, TR> projection)
    {
      if (underlying is null) throw new System.ArgumentNullException(nameof(underlying));

      var result = new Projected<TA, TR>(underlying, projection);
      if (result.Support().Count() == 1)
        return Singleton<TR>.Distribution(result.Support().First());
      return result;
    }

    private Projected(IDiscreteProbabilityDistribution<TA> underlying, System.Func<TA, TR> projection)
    {
      this.underlying = underlying;
      this.projection = projection;
      this.weights = underlying.Support().GroupBy(projection, a => underlying.Weight(a)).ToDictionary(g => g.Key, g => g.Sum());
    }

    public TR Sample() => projection(underlying.Sample());
    public System.Collections.Generic.IEnumerable<TR> Support() => this.weights.Keys;
    public int Weight(TR r) => 0;// this.weights.GetValueOrDefault(r, 0);
  }
}
