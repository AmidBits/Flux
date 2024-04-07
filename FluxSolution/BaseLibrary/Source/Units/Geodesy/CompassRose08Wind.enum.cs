namespace Flux
{
  public static partial class Em
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 45, 90, 135, 180, 225, 270, 315] (every 45° notch, starting at 0), from an eight value compass point [0, 1, 2, 3, 4, 5, 6, 7].</summary>
    public static Quantities.Azimuth GetAzimuth(this Quantities.CompassRose08Wind source) => new(45 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this Quantities.CompassRose08Wind source) => ((Quantities.CompassRose32Wind)source).ToWords();
  }

  namespace Quantities
  {
    /// <summary>The eight principal winds (or main winds) are the four cardinals and four intercardinals considered together, that is: N, NE, E, SE, S, SW, W, NW. Each principal wind is 45° from its two neighbours.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Points_of_the_compass#8-wind_compass_rose"/>
    public enum CompassRose08Wind
    {
      N = CompassRose16Wind.N,
      NE = CompassRose16Wind.NE,
      E = CompassRose16Wind.E,
      SE = CompassRose16Wind.SE,
      S = CompassRose16Wind.S,
      SW = CompassRose16Wind.SW,
      W = CompassRose16Wind.W,
      NW = CompassRose16Wind.NW
    }
  }
}
