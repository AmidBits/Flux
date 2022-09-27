#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>A classic discrete population model which gives the expected number N t+1 (or density) of individuals in generation t + 1 as a function of the number of individuals in the previous generation.</summary>
    /// <param name="population">The number of individuals in the previous generation (Nt).</param>
    /// <param name="growthRate">The intrinsic growth rate (r).</param>
    /// <param name="carryingCapacity">The carrying capacity of the environment (k).</param>
    /// <returns>The expected number (or density) of individuals in (the next) generation (Nt + 1).</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Ricker_model"/>
    public static TSelf RickerPopulationModel<TSelf>(TSelf population, TSelf growthRate, TSelf carryingCapacity)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => population * TSelf.Pow(TSelf.E, growthRate * (TSelf.One - (population / carryingCapacity)));
  }
}
#endif
