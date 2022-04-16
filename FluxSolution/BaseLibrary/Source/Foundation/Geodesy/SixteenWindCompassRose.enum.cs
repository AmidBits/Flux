namespace Flux
{
  public static partial class SixteenWindCompassRoseEm
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 359] (every 22.5° notch, starting at 0), from a fifteen value compass point [0, 15].</summary>
    public static Azimuth GetAzimuthAngle(this SixteenWindCompassRose sixteenWindCompassRose)
      => ((ThirtytwoWindCompassRose)(int)sixteenWindCompassRose).GetAzimuthAngle();
  }

  /// <summary>The eight principal winds (or main winds) are the four cardinals and four intercardinals considered together, that is: N, NE, E, SE, S, SW, W, NW. Each principal wind is 22.5° from its two neighbours. The directional values are the degrees they represent.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass#8-wind_compass_rose"/>
  public enum SixteenWindCompassRose
  {
    N = ThirtytwoWindCompassRose.N,
    NNE = ThirtytwoWindCompassRose.NNE,
    NE = ThirtytwoWindCompassRose.NE,
    ENE = ThirtytwoWindCompassRose.ENE,
    E = ThirtytwoWindCompassRose.E,
    ESE = ThirtytwoWindCompassRose.ESE,
    SE = ThirtytwoWindCompassRose.SE,
    SSE = ThirtytwoWindCompassRose.SSE,
    S = ThirtytwoWindCompassRose.S,
    SSW = ThirtytwoWindCompassRose.SSW,
    SW = ThirtytwoWindCompassRose.SW,
    WSW = ThirtytwoWindCompassRose.WSW,
    W = ThirtytwoWindCompassRose.W,
    WNW = ThirtytwoWindCompassRose.WNW,
    NW = ThirtytwoWindCompassRose.NW,
    NNW = ThirtytwoWindCompassRose.NNW
  }
}
