namespace Flux
{
  public static partial class PlanetaryScienceExtensions
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 45, 90, 135, 180, 225, 270, 315] (every 45° notch, starting at 0), from an eight value compass point [0, 1, 2, 3, 4, 5, 6, 7].</summary>
    public static PlanetaryScience.Azimuth GetAzimuth(this PlanetaryScience.CompassRose08Wind source) => new(45 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this PlanetaryScience.CompassRose08Wind source) => ((PlanetaryScience.CompassRose32Wind)source).ToWords();
  }
}
