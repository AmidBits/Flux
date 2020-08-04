namespace Flux.Probability
{
  public sealed class StandardDiscreteUniform : IDiscreteDistribution<int>
  {
    public static StandardDiscreteUniform Distribution(int min, int max)
    {
      if (min > max)
        throw new System.ArgumentException();
      return new StandardDiscreteUniform(min, max);
    }
    public int Min { get; }
    public int Max { get; }
    private StandardDiscreteUniform(int min, int max)
    {
      this.Min = min;
      this.Max = max;
    }
    public System.Collections.Generic.IEnumerable<int> Support() => System.Linq.Enumerable.Range(Min, 1 + Max - Min);
    public int Sample() => (int)((StandardContinuousUniform.Distribution.Sample() * (1.0 + Max - Min)) + Min);
    public int Weight(int i) => (Min <= i && i <= Max) ? 1 : 0;
    public override string ToString() => $"StandardDiscreteUniform[{Min}, {Max}]";
  }
}
