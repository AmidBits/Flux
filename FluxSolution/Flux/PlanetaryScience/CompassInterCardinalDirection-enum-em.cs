namespace Flux
{
  public static partial class Em
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [45, 135, 225, 315], from a four value inter-cardinal direction compass point [0, 1, 2, 3].</summary>
    public static PlanetaryScience.Azimuth GetAzimuth(this PlanetaryScience.CompassInterCardinalDirection source) => new(45 + 90 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this PlanetaryScience.CompassInterCardinalDirection source) => ((PlanetaryScience.CompassRose32Wind)source).ToWords();
  }
}
