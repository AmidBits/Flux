namespace Flux
{
  public interface ICylindricalCoordinate
  {
    Length Radius { get; }
    Azimuth Azimuth { get; }
    Length Height { get; }

    /// <summary>Converts the <see cref="ICylindricalCoordinate"/> to a CartesianCoordinate3.</summary>
    public (double x, double y, double z) ToCartesianCoordinate3()
    {
      var azimuth = Angle.ConvertDegreeToRadian(Azimuth.Value);

      return (
        Radius.Value * System.Math.Cos(azimuth),
        Radius.Value * System.Math.Sin(azimuth),
        Height.Value
      );
    }

    /// <summary>Converts the <see cref="ICylindricalCoordinate"/> to a <see cref="IPolarCoordinate"/>.</summary>
    public IPolarCoordinate ToPolarCoordinate()
      => new PolarCoordinate(
        Radius.Value,
        Angle.ConvertDegreeToRadian(Azimuth.Value)
      );

    /// <summary>Converts the <see cref="ICylindricalCoordinate"/> to a <see cref="ISphericalCoordinate"/>.</summary>
    public ISphericalCoordinate ToSphericalCoordinate()
      => new SphericalCoordinate(
        System.Math.Sqrt(Radius.Value * Radius.Value + Height.Value * Height.Value),
        System.Math.Atan2(Radius.Value, Height.Value),
        Angle.ConvertDegreeToRadian(Azimuth.Value)
      );
  }
}
