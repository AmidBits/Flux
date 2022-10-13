#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>A classic discrete population model which gives the expected number N t+1 (or density) of individuals in generation t + 1 as a function of the number of individuals in the previous generation.</summary>
  /// <param name="population">The number of individuals in the previous generation (Nt).</param>
  /// <param name="growthRate">The intrinsic growth rate (r).</param>
  /// <param name="carryingCapacity">The carrying capacity of the environment (k).</param>
  /// <returns>The expected number (or density) of individuals in (the next) generation (Nt + 1).</returns>
  /// <see cref="https://en.wikipedia.org/wiki/Ricker_model"/>
  public record class PopulationModelRicker<TSelf>
    : IPopulationModelable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
  {
    public TSelf m_population;
    public TSelf m_growthRate;
    public TSelf m_carryingCapacity;

    public PopulationModelRicker(TSelf population, TSelf growthRate, TSelf carryingCapacity)
    {
      m_population = population;
      m_growthRate = growthRate;
      m_carryingCapacity = carryingCapacity;
    }


    /// <summary>The number of individuals at time t (Nt).</summary>
    public TSelf Population { get => m_population; init => m_population = value; }

    /// <summary>The number of births within the population between Nt and Nt+1 (B).</summary>
    public TSelf GrowthRate { get => m_growthRate; init => m_growthRate = value; }

    /// <summary>The number of individuals immigrating into the population between Nt and Nt+1 (I).</summary>
    public TSelf CarryingCapacity { get => m_carryingCapacity; init => m_carryingCapacity = value; }

    /// <returns>The number of individuals at time Nt+1.</returns>
    public IPopulationModelable<TSelf> ModelPopulation()
      => new PopulationModelRicker<TSelf>(Compute(m_population, m_growthRate, m_carryingCapacity), m_growthRate, m_carryingCapacity);

    /// <summary>A classic discrete population model which gives the expected number N t+1 (or density) of individuals in generation t + 1 as a function of the number of individuals in the previous generation.</summary>
    /// <param name="population">The number of individuals in the previous generation (Nt).</param>
    /// <param name="growthRate">The intrinsic growth rate (r).</param>
    /// <param name="carryingCapacity">The carrying capacity of the environment (k).</param>
    /// <returns>The expected number (or density) of individuals in (the next) generation (Nt + 1).</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Ricker_model"/>
    public static TSelf Compute(TSelf population, TSelf growthRate, TSelf carryingCapacity)
      => population * TSelf.Pow(TSelf.E, growthRate * (TSelf.One - (population / carryingCapacity)));
  }
}
#endif
