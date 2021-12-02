namespace Flux.Model
{
  /// <summary>A classic discrete-time population model which gives the expected number Nt+1 (or density) of individuals in generation t+1 as a function of the number of individuals in the previous generation.</summary>
#if NET5_0
  public struct BevertonHoltModel
#else
  public record struct BevertonHoltModel
#endif
    : IPopulationModel
  {
    public BevertonHoltModel(double population, double growthRate, double carryingCapacity)
    {
      Population = population;
      GrowthRate = growthRate;
      CarryingCapacity = carryingCapacity;
    }

    /// <summary>The number of individuals at time t (Nt).</summary>
    public double Population { get; set; }
    /// <summary>The proliferation rate per generation (R0).</summary>
    public double GrowthRate { get; set; }
    /// <summary>The carrying capacity in the environment (M).</summary>
    public double CarryingCapacity { get; set; }

    /// <returns>The number of individuals at time Nt+1.</returns>
    public IPopulationModel NextGeneration()
#if NET5_0
      => new BevertonHoltModel(Model(Population, GrowthRate, CarryingCapacity), GrowthRate, CarryingCapacity);
#else
      => this with { Population = Model(Population, GrowthRate, CarryingCapacity) };
#endif

    /// <summary>A classic discrete-time population model which gives the expected number Nt+1 (or density) of individuals in generation t+1 as a function of the number of individuals in the previous generation.</summary>
    /// <param name="population">The number of individuals at time t (Nt).</param>
    /// <param name="growthRate">The proliferation rate per generation (R0).</param>
    /// <param name="carryingCapacity">The carrying capacity in the environment (M).</param>
    /// <returns>The number of individuals at time Nt+1.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Beverton%E2%80%93Holt_model"/>
    /// <seealso cref="RickerModel(double, double, double)" />
    public static double Model(double population, double growthRate, double carryingCapacity)
      => (growthRate * population) / (1 + population / carryingCapacity);
  }
}
