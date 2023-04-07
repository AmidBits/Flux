namespace Flux
{
  public static partial class GeodesyExtensionMethods
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 359] (every 11.25°, starting at 0), from a thirty-two value compass point [0, 31]. Each principal wind is 11.25° from its two neighbours.</summary>
    public static System.Text.Rune GetAbbreviation(this WordsOfTheCompassPoints source)
      => (System.Text.Rune)source.ToString()[0];
  }

  /// <summary>The compass point directions.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum WordsOfTheCompassPoints
  {
    North,
    East,
    South,
    West,
    By,
  }
}
