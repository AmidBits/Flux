namespace Flux
{
  public static partial class PlanetaryScienceExtensions
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 359] (every 22.5° notch, starting at 0), from a fifteen value compass point [0, 15].</summary>
    public static PlanetaryScience.Azimuth GetAzimuth(this PlanetaryScience.CompassRose16Wind source) => new(22.5 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this PlanetaryScience.CompassRose16Wind source) => ((PlanetaryScience.CompassRose32Wind)source).ToWords();
  }
}
