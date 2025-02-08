namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Returns an extrapolated Azimuth, i.e. a value in the set [0, 90, 180, 270] (every 90° notch, starting at 0°), from <paramref name="source"/> (<see cref="Units.CardinalAxis"/>) and <paramref name="isNegative"/>, converted into one of the four <see cref="Units.CardinalDirection"/> compass points.</para>
    /// </summary>
    public static Geometry.Geodesy.Azimuth GetAzimuth(this Geometry.Geodesy.CompassCardinalAxis source, bool isNegative)
      => source.ToCardinalDirection(isNegative).GetAzimuth();

    /// <summary>
    /// <para>Returns a <see cref="CardinalDirection"/> from <paramref name="source"/> (<see cref="CardinalAxis"/>) and <paramref name="isNegative"/>.</para>
    /// </summary>
    public static Geometry.Geodesy.CompassCardinalDirection ToCardinalDirection(this Geometry.Geodesy.CompassCardinalAxis source, bool isNegative)
      => source switch
      {
        Geometry.Geodesy.CompassCardinalAxis.EastWest => isNegative ? Geometry.Geodesy.CompassCardinalDirection.W : Geometry.Geodesy.CompassCardinalDirection.E,
        Geometry.Geodesy.CompassCardinalAxis.NorthSouth => isNegative ? Geometry.Geodesy.CompassCardinalDirection.S : Geometry.Geodesy.CompassCardinalDirection.N,
        _ => throw new NotImplementedException()
      };
  }
}
