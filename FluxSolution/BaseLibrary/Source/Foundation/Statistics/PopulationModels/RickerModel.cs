namespace Flux.Model
{
  /// <summary>A classic discrete population model which gives the expected number N t+1 (or density) of individuals in generation t + 1 as a function of the number of individuals in the previous generation.</summary>
  public struct RickerModel
    : IPopulationModel
  {
    public RickerModel(double population, double growthRate, double carryingCapacity)
    {
      Population = population;

      GrowthRate = growthRate;
      CarryingCapacity = carryingCapacity;
    }

    /// <summary>The number of individuals in the previous generation (Nt).</summary>
    public double Population { get; init; }

    /// <summary>The intrinsic growth rate (r).</summary>
    public double GrowthRate { get; init; }
    /// <summary>The carrying capacity of the environment (k).</summary>
    public double CarryingCapacity { get; init; }

    /// <returns>The expected number (or density) of individuals in (the next) generation (Nt + 1).</returns>
    public IPopulationModel NextGeneration()
      => new RickerModel(Model(Population, GrowthRate, CarryingCapacity), GrowthRate, CarryingCapacity);

    /// <summary>A classic discrete population model which gives the expected number N t+1 (or density) of individuals in generation t + 1 as a function of the number of individuals in the previous generation.</summary>
    /// <param name="population">The number of individuals in the previous generation (Nt).</param>
    /// <param name="growthRate">The intrinsic growth rate (r).</param>
    /// <param name="carryingCapacity">The carrying capacity of the environment (k).</param>
    /// <returns>The expected number (or density) of individuals in (the next) generation (Nt + 1).</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Ricker_model"/>
    public static double Model(double population, double growthRate, double carryingCapacity)
      => population * System.Math.Pow(System.Math.E, growthRate * (1 - (population / carryingCapacity)));
  }
}

