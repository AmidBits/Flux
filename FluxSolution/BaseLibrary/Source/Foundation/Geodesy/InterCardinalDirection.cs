namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [45, 135, 225, 315], from a four value inter-cardinal direction compass point [0, 1, 2, 3].</summary>
    public static Quantity.Azimuth GetAzimuthAngle(this InterCardinalDirection interCardinalDirection)
      => GetAzimuthAngle((ThirtytwoWindCompassRose)(int)interCardinalDirection);
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
}