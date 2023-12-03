namespace Flux
{
  public static partial class Em
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [45, 135, 225, 315], from a four value inter-cardinal direction compass point [0, 1, 2, 3].</summary>
    public static Units.Azimuth GetAzimuth(this Units.InterCardinalDirection source)
      => ((Units.ThirtytwoWindCompassRose)(int)source).GetAzimuth();
    public static string ToStringOfWords(this Units.InterCardinalDirection source)
      => ((Units.ThirtytwoWindCompassRose)source).ToStringOfWords();
  }

  namespace Units
  {
    /// <summary>The intercardinal(intermediate, or, historically, ordinal[1]) directions are the four intermediate compass directions located halfway between each pair of cardinal directions.</summary>
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
}
