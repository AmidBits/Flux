namespace Flux.Cartography
{
  // https://en.wikipedia.org/wiki/Cassini_projection
  public readonly record struct CassiniProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly CassiniProjection Default;

    public System.Numerics.Vector3 ProjectForward(Geometry.CoordinateSystems.GeographicCoordinate project)
      => new(
        (float)double.Asin(double.Cos(project.Latitude.Value) * double.Sin(project.Longitude.Value)),
        (float)double.Atan(double.Tan(project.Latitude.Value) / double.Cos(project.Longitude.Value)),
        (float)project.Altitude.Value
      );

    public Geometry.CoordinateSystems.GeographicCoordinate ProjectReverse(System.Numerics.Vector3 project)
      => new(
        double.Asin(double.Sin(project.Y) * double.Cos(project.X)),
        Units.AngleUnit.Radian,
        double.Atan2(double.Tan(project.X), double.Cos(project.Y)),
        Units.AngleUnit.Radian,
        project.Z,
        Units.LengthUnit.Meter
      );
  }
}
