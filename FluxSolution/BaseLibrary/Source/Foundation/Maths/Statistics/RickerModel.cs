namespace Flux
{
  public static partial class Maths
  {
    /// <summary></summary>
    /// <param name="previousPopulation">The number of individuals in the previous generation (Nt).</param>
    /// <param name="growthRate">The intrinsic growth rate (r).</param>
    /// <param name="carryingCapacity">The carrying capacity of the environment (k).</param>
    /// <returns>The expected number (or density) of individuals in (the next) generation (Nt + 1).</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Ricker_model"/>
    public static double RickerModel(double previousPopulation, double growthRate, double carryingCapacity)
      => previousPopulation * System.Math.Pow(System.Math.E, growthRate * (1 - (previousPopulation / carryingCapacity)));
  }
}
