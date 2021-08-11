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
  }
}
