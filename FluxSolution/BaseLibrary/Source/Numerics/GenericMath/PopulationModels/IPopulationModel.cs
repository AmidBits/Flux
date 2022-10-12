#if NET7_0_OR_GREATER
namespace Flux
{
  // https://en.wikipedia.org/wiki/Population_model
  /// <summary>A population model which gives the expected number Nt+1 (or density) of individuals in generation t+1 as a function of the number of individuals in the previous generation.</summary>
  public interface IPopulationModelable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    /// <summary>The number of individuals at time t (Nt).</summary>
    public TSelf Population { get; }

    /// <returns>The number of individuals at time t+1 (Nt+1).</returns>
    public IPopulationModelable<TSelf> ModelPopulation();
  }
}
#endif
