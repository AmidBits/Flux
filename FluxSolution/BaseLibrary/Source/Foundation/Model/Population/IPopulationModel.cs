namespace Flux.Model
{
  // https://en.wikipedia.org/wiki/Population_model
  /// <summary>A population model which gives the expected number Nt+1 (or density) of individuals in generation t+1 as a function of the number of individuals in the previous generation.</summary>
  public interface IPopulationModel
  {
    /// <summary>The number of individuals at time t (Nt).</summary>
    public double Population { get; set; }

    /// <returns>The number of individuals at time t+1 (Nt+1).</returns>
    public IPopulationModel NextGeneration();
  }
}
