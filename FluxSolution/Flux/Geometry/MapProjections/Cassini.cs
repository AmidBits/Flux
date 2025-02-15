namespace Flux.Geometry.MapProjections
{
  // https://en.wikipedia.org/wiki/Cassini_projection
  public readonly record struct CassiniProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly CassiniProjection Default;

    public System.Numerics.Vector3 ProjectForward(CoordinateSystems.GeographicCoordinate project)
      => new(
        (float)System.Math.Asin(System.Math.Cos(project.Latitude.Value) * System.Math.Sin(project.Longitude.Value)),
        (float)System.Math.Atan(System.Math.Tan(project.Latitude.Value) / System.Math.Cos(project.Longitude.Value)),
        (float)project.Altitude.Value
      );

    public CoordinateSystems.GeographicCoordinate ProjectReverse(System.Numerics.Vector3 project)
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
