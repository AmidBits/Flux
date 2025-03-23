namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Returns an extrapolated Azimuth, i.e. a value in the set [0, 90, 180, 270] (every 90° notch, starting at 0°), from <paramref name="source"/> (<see cref="Units.CardinalAxis"/>) and <paramref name="isNegative"/>, converted into one of the four <see cref="Units.CardinalDirection"/> compass points.</para>
    /// </summary>
    public static Geodesy.Azimuth GetAzimuth(this Geodesy.CompassCardinalAxis source, bool isNegative)
      => source.ToCardinalDirection(isNegative).GetAzimuth();

    /// <summary>
    /// <para>Returns a <see cref="CardinalDirection"/> from <paramref name="source"/> (<see cref="CardinalAxis"/>) and <paramref name="isNegative"/>.</para>
    /// </summary>
    public static Geodesy.CompassCardinalDirection ToCardinalDirection(this Geodesy.CompassCardinalAxis source, bool isNegative)
      => source switch
      {
        Geodesy.CompassCardinalAxis.EastWest => isNegative ? Geodesy.CompassCardinalDirection.W : Geodesy.CompassCardinalDirection.E,
        Geodesy.CompassCardinalAxis.NorthSouth => isNegative ? Geodesy.CompassCardinalDirection.S : Geodesy.CompassCardinalDirection.N,
        _ => throw new NotImplementedException()
      };
  }
}
