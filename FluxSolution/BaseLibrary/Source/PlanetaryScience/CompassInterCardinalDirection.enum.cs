namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [45, 135, 225, 315], from a four value inter-cardinal direction compass point [0, 1, 2, 3].</summary>
    public static Quantities.Azimuth GetAzimuth(this CompassInterCardinalDirection source) => new(45 + 90 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this CompassInterCardinalDirection source) => ((CompassRose32Wind)source).ToWords();
  }

  /// <summary>The intercardinal(intermediate, or, historically, ordinal[1]) directions are the four intermediate compass directions located halfway between each pair of cardinal directions.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Cardinal_direction#Additional_points"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum CompassInterCardinalDirection
  {
    NE = CompassRose08Wind.NE,
    SE = CompassRose08Wind.SE,
    SW = CompassRose08Wind.SW,
    NW = CompassRose08Wind.NW
  }
}
