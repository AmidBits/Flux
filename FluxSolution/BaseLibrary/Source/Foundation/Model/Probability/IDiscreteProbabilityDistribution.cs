namespace Flux.Probabilities
{
  public interface IDiscreteProbabilityDistribution<T>
    : IDistribution<T>
  {
    System.Collections.Generic.IEnumerable<T> Support();

    int Weight(T t);
  }
}
