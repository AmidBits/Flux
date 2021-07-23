namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns an Azimuth, i.e. a value in the range [0, 359].</summary>
    public static Quantity.Azimuth GetAzimuth(this ThirtyTwoWindCompassRose thirtyTwoWindCompassRose)
      => new Quantity.Azimuth(thirtyTwoWindCompassRose switch
      {
        ThirtyTwoWindCompassRose.N => 0,
        ThirtyTwoWindCompassRose.NbE => 11.25,
        ThirtyTwoWindCompassRose.NNE => 22.5,
        ThirtyTwoWindCompassRose.NEbN => 33.75,
        ThirtyTwoWindCompassRose.NE => 45,
        ThirtyTwoWindCompassRose.NEbE => 56.25,
        ThirtyTwoWindCompassRose.ENE => 67.5,
        ThirtyTwoWindCompassRose.EbN => 78.75,
        ThirtyTwoWindCompassRose.E => 90,
        ThirtyTwoWindCompassRose.EbS => 101.25,
        ThirtyTwoWindCompassRose.ESE => 112.5,
        ThirtyTwoWindCompassRose.SEbE => 123.75,
        ThirtyTwoWindCompassRose.SE => 135,
        ThirtyTwoWindCompassRose.SEbS => 146.25,
        ThirtyTwoWindCompassRose.SSE => 157.5,
        ThirtyTwoWindCompassRose.SbE => 168.75,
        ThirtyTwoWindCompassRose.S => 180,
        ThirtyTwoWindCompassRose.SbW => 191.25,
        ThirtyTwoWindCompassRose.SSW => 202.5,
        ThirtyTwoWindCompassRose.SWbS => 213.75,
        ThirtyTwoWindCompassRose.SW => 225,
        ThirtyTwoWindCompassRose.SWbW => 236.25,
        ThirtyTwoWindCompassRose.WSW => 247.5,
        ThirtyTwoWindCompassRose.WbS => 258.75,
        ThirtyTwoWindCompassRose.W => 270,
        ThirtyTwoWindCompassRose.WbN => 281.25,
        ThirtyTwoWindCompassRose.WNW => 292.5,
        ThirtyTwoWindCompassRose.NWbW => 303.75,
        ThirtyTwoWindCompassRose.NW => 315,
        ThirtyTwoWindCompassRose.NWbN => 326.25,
        ThirtyTwoWindCompassRose.NNW => 337.5,
        ThirtyTwoWindCompassRose.NbW => 348.75,
        _ => throw new System.NotImplementedException(),
      });

    public static Quantity.Azimuth GetAzimuth(CardinalDirection cardinalDirection)
      => GetAzimuth((ThirtyTwoWindCompassRose)(int)cardinalDirection);

    public static Quantity.Azimuth GetAzimuth(InterCardinalDirection interCardinalDirection)
      => GetAzimuth((ThirtyTwoWindCompassRose)(int)interCardinalDirection);

    public static Quantity.Azimuth GetAzimuth(EightWindCompassRose eightWindCompassRose)
      => GetAzimuth((ThirtyTwoWindCompassRose)(int)eightWindCompassRose);
  }

  /// <summary>The 32-wind compass rose is yielded from the eight principal winds, eight half-winds and sixteen quarter-winds combined together, with each compass direction point at an 11.25° angle from the next.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass#8-wind_compass_rose"/>
  public enum ThirtyTwoWindCompassRose
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
  /// <summary>The eight principal winds (or main winds) are the four cardinals and four intercardinals considered together, that is: N, NE, E, SE, S, SW, W, NW. Each principal wind is 45° from its two neighbours. The directional values are the degrees they represent.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass#8-wind_compass_rose"/>
  public enum EightWindCompassRose
  {
    E = ThirtyTwoWindCompassRose.E,
    NE = ThirtyTwoWindCompassRose.NE,
    N = ThirtyTwoWindCompassRose.N,
    NW = ThirtyTwoWindCompassRose.NW,
    W = ThirtyTwoWindCompassRose.W,
    SW = ThirtyTwoWindCompassRose.SW,
    S = ThirtyTwoWindCompassRose.S,
    SE = ThirtyTwoWindCompassRose.SE,
  }
  /// <summary>The intercardinal (intermediate, or, historically, ordinal[1]) directions are the four intermediate compass directions located halfway between each pair of cardinal directions. The directional values are the degrees they represent.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cardinal_direction#Additional_points"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum InterCardinalDirection
  {
    NE = EightWindCompassRose.NE,
    SE = EightWindCompassRose.SE,
    SW = EightWindCompassRose.SW,
    NW = EightWindCompassRose.NW,
  }
  /// <summary>The four cardinal directions, or cardinal points, are the directions north, east, south, and west, commonly denoted by their initials N, E, S, and W. The directional values are the degrees they represent.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cardinal_direction"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum CardinalDirection
  {
    N = EightWindCompassRose.N,
    E = EightWindCompassRose.E,
    S = EightWindCompassRose.S,
    W = EightWindCompassRose.W,
  }

  /// <summary>The compass point directions.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum PointsOfTheCompass
  {
    CardinalDirections = 4,
    EightWinds = 8,
    ThirtyTwiWinds = 32,
  }
}