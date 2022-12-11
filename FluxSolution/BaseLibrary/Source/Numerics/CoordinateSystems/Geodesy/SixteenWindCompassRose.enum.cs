namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 359] (every 22.5° notch, starting at 0), from a fifteen value compass point [0, 15].</summary>
    public static Quantities.Azimuth GetAzimuth(this SixteenWindCompassRose source)
      => ((ThirtytwoWindCompassRose)source).GetAzimuth();
    public static string ToStringOfWords(this SixteenWindCompassRose source)
      => ((ThirtytwoWindCompassRose)source).ToStringOfWords();
  }

  /// <summary>The eight principal winds (or main winds) are the four cardinals and four intercardinals considered together, that is: N, NE, E, SE, S, SW, W, NW. Each principal wind is 22.5° from its two neighbours. The directional values are the degrees they represent.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass#8-wind_compass_rose"/>
  public enum SixteenWindCompassRose
  {
    N = (int)ThirtytwoWindCompassRose.N,
    NNE = (int)ThirtytwoWindCompassRose.NNE,
    NE = (int)ThirtytwoWindCompassRose.NE,
    ENE = (int)ThirtytwoWindCompassRose.ENE,
    E = (int)ThirtytwoWindCompassRose.E,
    ESE = (int)ThirtytwoWindCompassRose.ESE,
    SE = (int)ThirtytwoWindCompassRose.SE,
    SSE = (int)ThirtytwoWindCompassRose.SSE,
    S = (int)ThirtytwoWindCompassRose.S,
    SSW = (int)ThirtytwoWindCompassRose.SSW,
    SW = (int)ThirtytwoWindCompassRose.SW,
    WSW = (int)ThirtytwoWindCompassRose.WSW,
    W = (int)ThirtytwoWindCompassRose.W,
    WNW = (int)ThirtytwoWindCompassRose.WNW,
    NW = (int)ThirtytwoWindCompassRose.NW,
    NNW = (int)ThirtytwoWindCompassRose.NNW
  }
}
