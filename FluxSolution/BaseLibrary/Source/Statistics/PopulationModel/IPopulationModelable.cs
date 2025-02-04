namespace Flux.Statistics.PopulationModel
{
  // https://en.wikipedia.org/wiki/Population_model
  /// <summary>A population model is a type of mathematical model that is applied to the study of population dynamics. A population model gives the expected number Nt+1 (or density) of individuals in generation t+1 as a function of the number of individuals in the previous generation.</summary>
  public interface IPopulationModelable
  {
    /// <summary>The number of individuals at time t (Nt).</summary>
    public double Population { get; }

    /// <returns>The number of individuals at time t+1 (Nt+1).</returns>
    public IPopulationModelable ModelPopulation();
  }
}
