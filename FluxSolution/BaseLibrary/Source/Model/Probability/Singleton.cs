namespace Flux.Probability
{
  public sealed class Singleton<T>
    : IDiscreteProbabilityDistribution<T>
  {
    private readonly T t;

#pragma warning disable CA1000 // Do not declare static members on generic types
    public static Singleton<T> Distribution(T t)
      => new Singleton<T>(t);
#pragma warning restore CA1000 // Do not declare static members on generic types

    private Singleton(T t)
      => this.t = t;
    public T Sample()
      => t;
    public System.Collections.Generic.IEnumerable<T> Support()
      => System.Linq.Enumerable.Repeat(this.t, 1);
    public int Weight(T t)
      => System.Collections.Generic.EqualityComparer<T>.Default.Equals(this.t, t) ? 1 : 0;
    public override string ToString()
      => $"Singleton[{t}]";
  }
}
