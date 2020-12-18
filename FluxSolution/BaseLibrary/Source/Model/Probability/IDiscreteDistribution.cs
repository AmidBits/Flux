namespace Flux.Probability
{
  public interface IDiscreteDistribution<T>
    : IDistribution<T>
  {
    System.Collections.Generic.IEnumerable<T> Support();

    int Weight(T t);
  }
}
