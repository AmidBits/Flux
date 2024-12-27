namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the set [0, 90, 180, 270] (every 90° notch, starting at 0°), from <paramref name="source"/> (<see cref="Quantities.CardinalAxis"/>) and <paramref name="isNegative"/>, converted into one of the four <see cref="Quantities.CardinalDirection"/> compass points.</summary>
    public static Quantities.Azimuth GetAzimuth(this CompassCardinalAxis source, bool isNegative)
      => source.ToCardinalDirection(isNegative).GetAzimuth();

    /// <summary>Returns a <see cref="CardinalDirection"/> from <paramref name="source"/> (<see cref="CardinalAxis"/>) and <paramref name="isNegative"/>.</summary>
    public static CompassCardinalDirection ToCardinalDirection(this CompassCardinalAxis source, bool isNegative)
      => source switch
      {
        CompassCardinalAxis.EastWest => isNegative ? CompassCardinalDirection.W : CompassCardinalDirection.E,
        CompassCardinalAxis.NorthSouth => isNegative ? CompassCardinalDirection.S : CompassCardinalDirection.N,
        _ => throw new NotImplementedException()
      };
  }

  /// <summary>The two cardinal axes are the latitudinal north-south and the longitudinal east-west.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Cardinal_direction"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum CompassCardinalAxis
  {
    /// <summary>Ranges from negative (west, -180 degrees) to positive (east, +180 degrees). The prime meridian is zero, the center (of two halfs of Earth/planetary body) between east and west, ranging (-180, +180).</summary>
    EastWest = 1,
    /// <summary>Ranges from negative (south, -90 degrees) to positive (north, +90 degrees). The equator is zero, the center between south and north, ranging (-90, +90).</summary>
    NorthSouth = 2
  }
}
