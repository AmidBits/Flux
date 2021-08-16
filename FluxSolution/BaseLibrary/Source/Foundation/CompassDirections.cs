namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns an Azimuth, i.e. a value in the range [0, 359].</summary>
    public static Quantity.Azimuth GetAzimuth(this ThirtytwoWindCompassRose thirtyTwoWindCompassRose)
      => new Quantity.Azimuth(thirtyTwoWindCompassRose switch
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

    public static Quantity.Azimuth GetAzimuth(SixteenWindCompassRose sixteenWindCompassRose)
      => GetAzimuth((ThirtytwoWindCompassRose)(int)sixteenWindCompassRose);

    public static Quantity.Azimuth GetAzimuth(EightWindCompassRose eightWindCompassRose)
      => GetAzimuth((ThirtytwoWindCompassRose)(int)eightWindCompassRose);

    public static Quantity.Azimuth GetAzimuth(InterCardinalDirection interCardinalDirection)
      => GetAzimuth((ThirtytwoWindCompassRose)(int)interCardinalDirection);

    public static Quantity.Azimuth GetAzimuth(CardinalDirection cardinalDirection)
      => GetAzimuth((ThirtytwoWindCompassRose)(int)cardinalDirection);
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
  /// <summary>The eight principal winds (or main winds) are the four cardinals and four intercardinals considered together, that is: N, NE, E, SE, S, SW, W, NW. Each principal wind is 45° from its two neighbours. The directional values are the degrees they represent.</summary>
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
  /// <summary>The eight principal winds (or main winds) are the four cardinals and four intercardinals considered together, that is: N, NE, E, SE, S, SW, W, NW. Each principal wind is 45° from its two neighbours. The directional values are the degrees they represent.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass#8-wind_compass_rose"/>
  public enum EightWindCompassRose
  {
    N = ThirtytwoWindCompassRose.N,
    NE = ThirtytwoWindCompassRose.NE,
    E = ThirtytwoWindCompassRose.E,
    SE = ThirtytwoWindCompassRose.SE,
    S = ThirtytwoWindCompassRose.S,
    SW = ThirtytwoWindCompassRose.SW,
    W = ThirtytwoWindCompassRose.W,
    NW = ThirtytwoWindCompassRose.NW
  }
  /// <summary>The intercardinal(intermediate, or, historically, ordinal[1]) directions are the four intermediate compass directions located halfway between each pair of cardinal directions.The directional values are the degrees they represent.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cardinal_direction#Additional_points"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum InterCardinalDirection
  {
    NE = ThirtytwoWindCompassRose.NE,
    SE = ThirtytwoWindCompassRose.SE,
    SW = ThirtytwoWindCompassRose.SW,
    NW = ThirtytwoWindCompassRose.NW
  }
  /// <summary>The four cardinal directions, or cardinal points, are the directions north, east, south, and west, commonly denoted by their initials N, E, S, and W. The directional values are the degrees they represent.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cardinal_direction"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum CardinalDirection
  {
    N = ThirtytwoWindCompassRose.N,
    E = ThirtytwoWindCompassRose.E,
    S = ThirtytwoWindCompassRose.S,
    W = ThirtytwoWindCompassRose.W
  }

  /// <summary>The compass point directions.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum PointsOfTheCompass
  {
    CardinalDirections = 4,
    EightWinds = 8,
    SixteenWinds = 16,
    ThirtytwoWinds = 32
  }
}