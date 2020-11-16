namespace Flux
{
  public static partial class Maths
  {
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
