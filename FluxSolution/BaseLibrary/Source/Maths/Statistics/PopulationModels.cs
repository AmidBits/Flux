
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>A classic discrete-time population model which gives the expected number Nt+1 (or density) of individuals in generation t+1 as a function of the number of individuals in the previous generation.</summary>
    /// <param name="Nt">The number of individuals at time t.</param>
    /// <param name="R0">The proliferation rate per generation.</param>
    /// <param name="M">The carrying capacity in the environment.</param>
    /// <returns>The number of individuals at time Nt+1.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Beverton%E2%80%93Holt_model"/>
    /// <seealso cref="RickerModel(double, double, double)" />
    public static double BevertonHoltModel(double Nt, double R0, double M)
      => (R0 * Nt) / (1 + Nt / M);

    /// <summary>Although BIDE models are conceptually simple, reliable estimates of the 5 variables contained therein (N, B, D, I and E) are often difficult to obtain.</summary>
    /// <param name="Nt">The number of individuals at time t.</param>
    /// <param name="B">The number of births within the population between Nt and Nt+1.</param>
    /// <param name="D">The number of deaths within the population between Nt and Nt+1.</param>
    /// <param name="I">The number of individuals immigrating into the population between Nt and Nt+1.</param>
    /// <param name="E">The number of individuals emigrating into the population between Nt and Nt+1.</param>
    /// <returns>The number of individuals at time Nt+1.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Matrix_population_models"/>
    public static double BideModel(double Nt, double B, double D, double I, double E)
      => Nt + B - D + I - E;

    /// <summary></summary>
    /// <param name="Nt">The number of individuals in the previous generation.</param>
    /// <param name="r">The intrinsic growth rate.</param>
    /// <param name="k">The carrying capacity of the environment.</param>
    /// <returns>The expected number (or density) of individuals in (the next) generation (Nt + 1).</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Ricker_model"/>
    public static double RickerModel(double Nt, double r, double k)
      => Nt * System.Math.Pow(System.Math.E, r * (1 - (Nt / k)));
  }
}
