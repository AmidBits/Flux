namespace Flux
{
  public static partial class PlanetaryScienceExtensions
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the set [0, 90, 180, 270] (every 90° notch, starting at 0), from a four value cardinal direction compass point [0, 1, 2, 3].</summary>
    public static PlanetaryScience.Azimuth GetAzimuth(this PlanetaryScience.CompassCardinalDirection source)
      => new(90 * (int)source);

    public static PlanetaryScience.CompassCardinalAxis ToCardinalAxis(this PlanetaryScience.CompassCardinalDirection source)
      => source is PlanetaryScience.CompassCardinalDirection.N or PlanetaryScience.CompassCardinalDirection.S
      ? PlanetaryScience.CompassCardinalAxis.NorthSouth
      : source is PlanetaryScience.CompassCardinalDirection.E or PlanetaryScience.CompassCardinalDirection.W
      ? PlanetaryScience.CompassCardinalAxis.EastWest
      : throw new System.NotImplementedException();

    public static System.Collections.Generic.List<string> ToWords(this PlanetaryScience.CompassCardinalDirection source)
      => ((PlanetaryScience.CompassRose32Wind)source).ToWords();
  }
}
