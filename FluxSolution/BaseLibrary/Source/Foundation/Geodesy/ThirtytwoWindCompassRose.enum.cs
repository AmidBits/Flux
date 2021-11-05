namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 359] (every 32 degrees from 0), from a thirty-two value compass point [0, 31].</summary>
    public static Azimuth GetAzimuthAngle(this ThirtytwoWindCompassRose thirtyTwoWindCompassRose)
      => new(thirtyTwoWindCompassRose switch
      {
        ThirtytwoWindCompassRose.N => 0,
        ThirtytwoWindCompassRose.NbE => 11.25,
        ThirtytwoWindCompassRose.NNE => 22.5,
        ThirtytwoWindCompassRose.NEbN => 33.75,
        ThirtytwoWindCompassRose.NE => 45,
        ThirtytwoWindCompassRose.NEbE => 56.25,
        ThirtytwoWindCompassRose.ENE => 67.5,
        ThirtytwoWindCompassRose.EbN => 78.75,
        ThirtytwoWindCompassRose.E => 90,
        ThirtytwoWindCompassRose.EbS => 101.25,
        ThirtytwoWindCompassRose.ESE => 112.5,
        ThirtytwoWindCompassRose.SEbE => 123.75,
        ThirtytwoWindCompassRose.SE => 135,
        ThirtytwoWindCompassRose.SEbS => 146.25,
        ThirtytwoWindCompassRose.SSE => 157.5,
        ThirtytwoWindCompassRose.SbE => 168.75,
        ThirtytwoWindCompassRose.S => 180,
        ThirtytwoWindCompassRose.SbW => 191.25,
        ThirtytwoWindCompassRose.SSW => 202.5,
        ThirtytwoWindCompassRose.SWbS => 213.75,
        ThirtytwoWindCompassRose.SW => 225,
        ThirtytwoWindCompassRose.SWbW => 236.25,
        ThirtytwoWindCompassRose.WSW => 247.5,
        ThirtytwoWindCompassRose.WbS => 258.75,
        ThirtytwoWindCompassRose.W => 270,
        ThirtytwoWindCompassRose.WbN => 281.25,
        ThirtytwoWindCompassRose.WNW => 292.5,
        ThirtytwoWindCompassRose.NWbW => 303.75,
        ThirtytwoWindCompassRose.NW => 315,
        ThirtytwoWindCompassRose.NWbN => 326.25,
        ThirtytwoWindCompassRose.NNW => 337.5,
        ThirtytwoWindCompassRose.NbW => 348.75,
        _ => throw new System.NotImplementedException(),
      });
  }

  /// <summary>The 32-wind compass rose is yielded from the eight principal winds, eight half-winds and sixteen quarter-winds combined together, with each compass direction point at an 11.25° angle from the next.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass#8-wind_compass_rose"/>
  public enum ThirtytwoWindCompassRose
  {
    N,
    NbE,
    NNE,
    NEbN,
    NE,
    NEbE,
    ENE,
    EbN,
    E,
    EbS,
    ESE,
    SEbE,
    SE,
    SEbS,
    SSE,
    SbE,
    S,
    SbW,
    SSW,
    SWbS,
    SW,
    SWbW,
    WSW,
    WbS,
    W,
    WbN,
    WNW,
    NWbW,
    NW,
    NWbN,
    NNW,
    NbW
  }
}
