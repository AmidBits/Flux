namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the set [0, 90, 180, 270] (every 90� notch, starting at 0), from a four value cardinal direction compass point [0, 1, 2, 3].</summary>
    public static Geometry.Geodesy.Azimuth GetAzimuth(this CompassCardinalDirection source) => new(90 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this CompassCardinalDirection source) => ((CompassRose32Wind)source).ToWords();
  }

  /// <summary>The four cardinal directions, or cardinal points, are the directions north, east, south, and west, commonly denoted by their initials N, E, S, and W. Each principal wind is 90� from its two neighbours.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Cardinal_direction"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum CompassCardinalDirection
  {
    N = CompassRose32Wind.N,
    E = CompassRose32Wind.E,
    S = CompassRose32Wind.S,
    W = CompassRose32Wind.W
  }
}
