namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the set [0, 90, 180, 270], from a four value cardinal direction compass point [0, 1, 2, 3].</summary>
    public static Quantity.Azimuth GetAzimuthAngle(this CardinalDirection cardinalDirection)
      => GetAzimuthAngle((ThirtytwoWindCompassRose)(int)cardinalDirection);
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
}