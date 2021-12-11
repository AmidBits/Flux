using System.Linq;

namespace Flux.Probabilities
{
  public sealed class Bernoulli
    : IDiscreteProbabilityDistribution<int>
  {
    public static IDiscreteProbabilityDistribution<int> Distribution(int zero, int one)
    {
      if (zero < 0 || one < 0 || zero == 0 && one == 0) throw new System.ArgumentException(@"Both zero and one must be greater than 0.");

      if (zero == 0)
        return Singleton<int>.Distribution(1);
      if (one == 0)
        return Singleton<int>.Distribution(0);

      return new Bernoulli(zero, one);
    }

    public int Zero { get; }

    public int One { get; }

    private Bernoulli(int zero, int one)
    {
      this.Zero = zero;
      this.One = one;
    }

    public int Sample()
      => (StandardContinuousUniform.Distribution.Sample() <= ((double)Zero) / (Zero + One)) ? 0 : 1;

    public System.Collections.Generic.IEnumerable<int> Support()
      => System.Linq.Enumerable.Range(0, 1);

    public int Weight(int x)
      => x == 0 ? Zero : x == 1 ? One : 0;

    public override string ToString()
      => $"Bernoulli[{this.Zero}, {this.One}]";
  }
}
