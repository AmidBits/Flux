namespace Flux.Probabilities
{
  public interface IDistribution<T>
  {
    T Sample();
  }
}
