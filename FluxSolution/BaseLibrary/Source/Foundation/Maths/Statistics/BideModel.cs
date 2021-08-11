namespace Flux
{
  public static partial class Maths
  {
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
  }
}
