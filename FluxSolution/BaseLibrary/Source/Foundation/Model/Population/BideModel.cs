namespace Flux.Model
{
  /// <summary>Although BIDE models are conceptually simple, reliable estimates of the 5 variables contained therein (N, B, D, I and E) are often difficult to obtain.</summary>
#if NET5_0
  public struct BideModel
#else
  public record struct BideModel
#endif
    : IPopulationModel
  {
    public BideModel(double population, double births, double immigrated, double deaths, double emigrated)
    {
      Population = population;
      Births = births;
      Immigrated = immigrated;
      Deaths = deaths;
      Emigrated = emigrated;
    }

    /// <summary>The number of individuals at time t (Nt).</summary>
    public double Population { get; set; }
    /// <summary>The number of births within the population between Nt and Nt+1 (B).</summary>
    public double Births { get; set; }
    /// <summary>The number of individuals immigrating into the population between Nt and Nt+1 (I).</summary>
    public double Immigrated { get; set; }
    /// <summary>The number of deaths within the population between Nt and Nt+1 (D).</summary>
    public double Deaths { get; set; }
    /// <summary>The number of individuals emigrating into the population between Nt and Nt+1 (E).</summary>
    public double Emigrated { get; set; }

    /// <returns>The number of individuals at time Nt+1.</returns>
    public IPopulationModel NextGeneration()
#if NET5_0
      => new BideModel(Model(Population, Births, Immigrated, Deaths, Emigrated), Births, Immigrated, Deaths, Emigrated);
#else
      => this with { Population = Model(Population, Births, Immigrated, Deaths, Emigrated) };
#endif

    /// <summary>Although BIDE models are conceptually simple, reliable estimates of the 5 variables contained therein (N, B, D, I and E) are often difficult to obtain.</summary>
    /// <param name="population">The number of individuals at time t (Nt).</param>
    /// <param name="births">The number of births within the population between Nt and Nt+1 (B).</param>
    /// <param name="deaths">The number of deaths within the population between Nt and Nt+1 (D).</param>
    /// <param name="immigrated">The number of individuals immigrating into the population between Nt and Nt+1 (I).</param>
    /// <param name="emigrated">The number of individuals emigrating into the population between Nt and Nt+1 (E).</param>
    /// <returns>The number of individuals at time Nt+1.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Matrix_population_models"/>
    public static double Model(double population, double births, double immigrated, double deaths, double emigrated)
      => population + births - deaths + immigrated - emigrated;
  }
}
