namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 45, 90, 135, 180, 225, 270, 315], from an eight value compass point [0, 1, 2, 3, 4, 5, 6, 7].</summary>
    public static Quantity.Azimuth GetAzimuthAngle(this EightWindCompassRose eightWindCompassRose)
      => GetAzimuthAngle((ThirtytwoWindCompassRose)(int)eightWindCompassRose);
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
}
