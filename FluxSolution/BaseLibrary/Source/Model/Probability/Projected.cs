using System.Linq;

namespace Flux.Probability
{
  public sealed class Projected<TA, TR> 
    : IDiscreteDistribution<TR>
    where TR : notnull
  {
    private readonly IDiscreteDistribution<TA> underlying;
    private readonly System.Func<TA, TR> projection;
    private readonly System.Collections.Generic.Dictionary<TR, int> weights;

#pragma warning disable CA1000 // Do not declare static members on generic types
    public static IDiscreteDistribution<TR> Distribution(IDiscreteDistribution<TA> underlying, System.Func<TA, TR> projection)
#pragma warning restore CA1000 // Do not declare static members on generic types
    {
      if (underlying is null) throw new System.ArgumentNullException(nameof(underlying));

      var result = new Projected<TA, TR>(underlying, projection);
      if (result.Support().Count() == 1)
        return Singleton<TR>.Distribution(result.Support().First());
      return result;
    }

    private Projected(IDiscreteDistribution<TA> underlying, System.Func<TA, TR> projection)
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
