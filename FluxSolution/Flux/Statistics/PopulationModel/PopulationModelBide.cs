namespace Flux.Statistics.PopulationModel
{
  /// <summary>Although BIDE models are conceptually simple, reliable estimates of the 5 variables contained therein (N, B, D, I and E) are often difficult to obtain.</summary>
  public record class PopulationModelBide
    : IPopulationModelable
  {
    public double m_population;
    public double m_births;
    public double m_immigrated;
    public double m_deaths;
    public double m_emigrated;

    public PopulationModelBide(double population, double births, double immigrated, double deaths, double emigrated)
    {
      m_population = population;
      m_births = births;
      m_immigrated = immigrated;
      m_deaths = deaths;
      m_emigrated = emigrated;
    }

    /// <summary>The number of individuals at time t (Nt).</summary>
    public double Population { get => m_population; init => m_population = value; }

    /// <summary>The number of births within the population between Nt and Nt+1 (B).</summary>
    public double Births { get => m_births; init => m_births = value; }

    /// <summary>The number of individuals immigrating into the population between Nt and Nt+1 (I).</summary>
    public double Immigrated { get => m_immigrated; init => m_immigrated = value; }

    /// <summary>The number of deaths within the population between Nt and Nt+1 (D).</summary>
    public double Deaths { get => m_deaths; init => m_deaths = value; }

    /// <summary>The number of individuals emigrating into the population between Nt and Nt+1 (E).</summary>
    public double Emigrated { get => m_emigrated; init => m_emigrated = value; }

    public IPopulationModelable ModelPopulation()
      => new PopulationModelBide(Compute(m_population, m_births, m_immigrated, m_deaths, m_emigrated), m_births, m_immigrated, m_deaths, m_emigrated);

    /// <summary>Although BIDE models are conceptually simple, reliable estimates of the 5 variables contained therein (N, B, D, I and E) are often difficult to obtain.</summary>
    /// <param name="population">The number of individuals at time t (Nt).</param>
    /// <param name="births">The number of births within the population between Nt and Nt+1 (B).</param>
    /// <param name="deaths">The number of deaths within the population between Nt and Nt+1 (D).</param>
    /// <param name="immigrated">The number of individuals immigrating into the population between Nt and Nt+1 (I).</param>
    /// <param name="emigrated">The number of individuals emigrating into the population between Nt and Nt+1 (E).</param>
    /// <returns>The number of individuals at time Nt+1.</returns>
    /// <see href="https://en.wikipedia.org/wiki/Matrix_population_models"/>
    public static double Compute(double population, double births, double immigrated, double deaths, double emigrated)
      => population + births - deaths + immigrated - emigrated;
  }
}
