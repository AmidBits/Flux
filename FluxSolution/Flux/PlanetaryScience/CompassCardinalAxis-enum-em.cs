namespace Flux
{
  public static partial class PlanetaryScienceExtensions
  {
    /// <summary>
    /// <para>Returns an extrapolated Azimuth, i.e. a value in the set [0, 90, 180, 270] (every 90° notch, starting at 0°), from <paramref name="source"/> (<see cref="Units.CardinalAxis"/>) and <paramref name="isNegative"/>, converted into one of the four <see cref="Units.CardinalDirection"/> compass points.</para>
    /// </summary>
    public static PlanetaryScience.Azimuth GetAzimuth(this PlanetaryScience.CompassCardinalAxis source, bool isNegative)
      => source.ToCardinalDirection(isNegative).GetAzimuth();

    /// <summary>
    /// <para>Returns a <see cref="CardinalDirection"/> from <paramref name="source"/> (<see cref="CardinalAxis"/>) and <paramref name="isNegative"/>.</para>
    /// </summary>
    public static PlanetaryScience.CompassCardinalDirection ToCardinalDirection(this PlanetaryScience.CompassCardinalAxis source, bool isNegative)
      => source switch
      {
        PlanetaryScience.CompassCardinalAxis.EastWest => isNegative ? PlanetaryScience.CompassCardinalDirection.W : PlanetaryScience.CompassCardinalDirection.E,
        PlanetaryScience.CompassCardinalAxis.NorthSouth => isNegative ? PlanetaryScience.CompassCardinalDirection.S : PlanetaryScience.CompassCardinalDirection.N,
        _ => throw new System.NotImplementedException()
      };
  }
}
