namespace Flux
{
  public static partial class GeodesyExtensionMethods
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the set [0, 90, 180, 270] (every 90° notch, starting at 0), from a four value cardinal direction compass point [0, 1, 2, 3].</summary>
    public static Units.Azimuth GetAzimuth(this CardinalDirection source)
      => ((ThirtytwoWindCompassRose)(int)source).GetAzimuth();
    public static string ToStringOfWords(this CardinalDirection source)
      => ((ThirtytwoWindCompassRose)source).ToStringOfWords();
  }

  /// <summary>The four cardinal directions, or cardinal points, are the directions north, east, south, and west, commonly denoted by their initials N, E, S, and W. Each principal wind is 90° from its two neighbours.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cardinal_direction"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum CardinalDirection
  {
    N = ThirtytwoWindCompassRose.N,
    E = ThirtytwoWindCompassRose.E,
    S = ThirtytwoWindCompassRose.S,
    W = ThirtytwoWindCompassRose.W
  }
}