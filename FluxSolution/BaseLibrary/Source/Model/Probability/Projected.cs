using System.Linq;

namespace Flux.Probability
{
  public sealed class Projected<A, R> : IDiscreteDistribution<R>
    where R : notnull
  {
    private readonly IDiscreteDistribution<A> underlying;
    private readonly System.Func<A, R> projection;
    private readonly System.Collections.Generic.Dictionary<R, int> weights;

    public static IDiscreteDistribution<R> Distribution(IDiscreteDistribution<A> underlying, System.Func<A, R> projection)
    {
      var result = new Projected<A, R>(underlying, projection);
      if (result.Support().Count() == 1)
        return Singleton<R>.Distribution(result.Support().First());
      return result;
    }

    private Projected(IDiscreteDistribution<A> underlying, System.Func<A, R> projection)
    {
      this.underlying = underlying;
      this.projection = projection;
      this.weights = underlying.Support().GroupBy(projection, a => underlying.Weight(a)).ToDictionary(g => g.Key, g => g.Sum());
    }

    public R Sample() => projection(underlying.Sample());
    public System.Collections.Generic.IEnumerable<R> Support() => this.weights.Keys;
    public int Weight(R r) => 0;// this.weights.GetValueOrDefault(r, 0);
  }
}
