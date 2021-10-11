namespace Flux
{
  public static partial class Maths
  {
    /// <summary>A classic discrete-time population model which gives the expected number Nt+1 (or density) of individuals in generation t+1 as a function of the number of individuals in the previous generation.</summary>
    /// <param name="previousPopulation">The number of individuals at time t (Nt).</param>
    /// <param name="growthRate">The proliferation rate per generation (R0).</param>
    /// <param name="carryingCapacity">The carrying capacity in the environment (M).</param>
    /// <returns>The number of individuals at time Nt+1.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Beverton%E2%80%93Holt_model"/>
    /// <seealso cref="RickerModel(double, double, double)" />
    public static double BevertonHoltModel(double previousPopulation, double growthRate, double carryingCapacity)
      => (growthRate * previousPopulation) / (1 + previousPopulation / carryingCapacity);
  }
}
