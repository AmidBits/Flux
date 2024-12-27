namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 45, 90, 135, 180, 225, 270, 315] (every 45° notch, starting at 0), from an eight value compass point [0, 1, 2, 3, 4, 5, 6, 7].</summary>
    public static Quantities.Azimuth GetAzimuth(this CompassRose08Wind source) => new(45 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this CompassRose08Wind source) => ((CompassRose32Wind)source).ToWords();
  }

  /// <summary>The eight principal winds (or main winds) are the four cardinals and four intercardinals considered together, that is: N, NE, E, SE, S, SW, W, NW. Each principal wind is 45° from its two neighbours.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Points_of_the_compass#8-wind_compass_rose"/>
  public enum CompassRose08Wind
  {
    N = CompassRose32Wind.N,
    NE = CompassRose32Wind.NE,
    E = CompassRose32Wind.E,
    SE = CompassRose32Wind.SE,
    S = CompassRose32Wind.S,
    SW = CompassRose32Wind.SW,
    W = CompassRose32Wind.W,
    NW = CompassRose32Wind.NW
  }
}
