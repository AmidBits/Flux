#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Generic
  {
    /// <summary>Although BIDE models are conceptually simple, reliable estimates of the 5 variables contained therein (N, B, D, I and E) are often difficult to obtain.</summary>
    /// <param name="population">The number of individuals at time t (Nt).</param>
    /// <param name="births">The number of births within the population between Nt and Nt+1 (B).</param>
    /// <param name="deaths">The number of deaths within the population between Nt and Nt+1 (D).</param>
    /// <param name="immigrated">The number of individuals immigrating into the population between Nt and Nt+1 (I).</param>
    /// <param name="emigrated">The number of individuals emigrating into the population between Nt and Nt+1 (E).</param>
    /// <returns>The number of individuals at time Nt+1.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Matrix_population_models"/>
    public static TSelf PopulationModelBide<TSelf>(this TSelf population, TSelf births, TSelf immigrated, TSelf deaths, TSelf emigrated)
      where TSelf : System.Numerics.INumber<TSelf>
      => population + births - deaths + immigrated - emigrated;
  }
}
#endif
