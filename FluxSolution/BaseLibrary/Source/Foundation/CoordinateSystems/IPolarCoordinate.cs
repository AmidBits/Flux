namespace Flux
{
  /// <summary>The polar coordinate system is a two-dimensional coordinate system in which each point on a plane is determined by a distance from a reference point and an angle from a reference direction.</summary>
  public interface IPolarCoordinate
  {
    Length Radius { get; }
    Azimuth Azimuth { get; }

#if NET7_0_OR_GREATER
    abstract IPolarCoordinate Create(Length radius, Azimuth azimuth);
#endif

    /// <summary>Converts the <see cref="IPolarCoordinate"/> to a <see cref="System.ValueTuple{double,double}">CartesianCoordinate2</see>.</summary>
    public (double x, double y) ToCartesianCoordinate2()
    {
      var azimuth = Azimuth.ToRadians();

      return new(Radius.Value * System.Math.Cos(azimuth), Radius.Value * System.Math.Sin(azimuth));
    }

    /// <summary>Converts the <see cref="IPolarCoordinate"/> to a <see cref="System.Numerics.Complex"/>.</summary>
    public System.Numerics.Complex ToComplex()
      => System.Numerics.Complex.FromPolarCoordinates(
        Radius.Value,
        Azimuth.ToRadians()
      );
  }
}
