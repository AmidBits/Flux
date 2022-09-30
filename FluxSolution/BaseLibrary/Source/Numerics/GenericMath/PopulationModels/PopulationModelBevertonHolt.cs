#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>A classic discrete-time population model which gives the expected number Nt+1 (or density) of individuals in generation t+1 as a function of the number of individuals in the previous generation.</summary>
    /// <param name="population">The number of individuals at time t (Nt).</param>
    /// <param name="growthRate">The proliferation rate per generation (R0).</param>
    /// <param name="carryingCapacity">The carrying capacity in the environment (M).</param>
    /// <returns>The number of individuals at time Nt+1.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Beverton%E2%80%93Holt_model"/>
    /// <seealso cref="RickerModel(double, double, double)" />
    public static TSelf PopulationModelBevertonHolt<TSelf>(TSelf population, TSelf growthRate, TSelf carryingCapacity)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (growthRate * population) / (TSelf.One + population / carryingCapacity);
  }
}
#endif
