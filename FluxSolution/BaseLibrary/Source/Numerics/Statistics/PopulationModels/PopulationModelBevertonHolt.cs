namespace Flux.Numerics
{
  /// <summary>A classic discrete-time population model which gives the expected number Nt+1 (or density) of individuals in generation t+1 as a function of the number of individuals in the previous generation.</summary>
  /// <param name="population">The number of individuals at time t (Nt).</param>
  /// <param name="growthRate">The proliferation rate per generation (R0).</param>
  /// <param name="carryingCapacity">The carrying capacity in the environment (M).</param>
  /// <returns>The number of individuals at time Nt+1.</returns>
  /// <see cref="https://en.wikipedia.org/wiki/Beverton%E2%80%93Holt_model"/>
  /// <seealso cref="RickerModel(double, double, double)" />
  public record class PopulationModelBevertonHolt<TSelf>
    : IPopulationModelable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    public TSelf m_population;
    public TSelf m_growthRate;
    public TSelf m_carryingCapacity;

    public PopulationModelBevertonHolt(TSelf population, TSelf growthRate, TSelf carryingCapacity)
    {
      m_population = population;
      m_growthRate = growthRate;
      m_carryingCapacity = carryingCapacity;
    }

    /// <summary>The number of individuals at time t (Nt).</summary>
    public TSelf Population { get => m_population; init => m_population = value; }

    /// <summary>The proliferation rate per generation (R0).</summary>
    public TSelf GrowthRate { get => m_growthRate; init => m_growthRate = value; }

    /// <summary>The carrying capacity in the environment (M).</summary>
    public TSelf CarryingCapacity { get => m_carryingCapacity; init => m_carryingCapacity = value; }

    /// <returns>The number of individuals at time Nt+1.</returns>
    public IPopulationModelable<TSelf> ModelPopulation()
      => new PopulationModelBevertonHolt<TSelf>(Compute(m_population, m_growthRate, m_carryingCapacity), m_growthRate, m_carryingCapacity);

    /// <summary>A classic discrete-time population model which gives the expected number Nt+1 (or density) of individuals in generation t+1 as a function of the number of individuals in the previous generation.</summary>
    /// <param name="population">The number of individuals at time t (Nt).</param>
    /// <param name="growthRate">The proliferation rate per generation (R0).</param>
    /// <param name="carryingCapacity">The carrying capacity in the environment (M).</param>
    /// <returns>The number of individuals at time Nt+1.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Beverton%E2%80%93Holt_model"/>
    /// <seealso cref="RickerModel(double, double, double)" />
    public static TSelf Compute(TSelf population, TSelf growthRate, TSelf carryingCapacity)
      => (growthRate * population) / (TSelf.One + population / carryingCapacity);
  }
}
