namespace Flux
{
  public static partial class Em
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the set [0, 90, 180, 270] (every 90° notch, starting at 0), from a four value cardinal direction compass point [0, 1, 2, 3].</summary>
    public static PlanetaryScience.Azimuth GetAzimuth(this PlanetaryScience.CompassCardinalDirection source) => new(90 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this PlanetaryScience.CompassCardinalDirection source) => ((PlanetaryScience.CompassRose32Wind)source).ToWords();
  }
}
