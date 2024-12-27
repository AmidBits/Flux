namespace Flux.MapProjections
{
  // https://en.wikipedia.org/wiki/Cassini_projection
  public readonly record struct CassiniProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly CassiniProjection Default;

    public System.Numerics.Vector3 ProjectForward(Coordinates.GeographicCoordinate project)
      => new(
        (float)System.Math.Asin(System.Math.Cos(project.Latitude.Value) * System.Math.Sin(project.Longitude.Value)),
        (float)System.Math.Atan(System.Math.Tan(project.Latitude.Value) / System.Math.Cos(project.Longitude.Value)),
        (float)project.Altitude.Value
      );

    public Coordinates.GeographicCoordinate ProjectReverse(System.Numerics.Vector3 project)
      => new(
        System.Math.Asin(System.Math.Sin(project.Y) * System.Math.Cos(project.X)),
        Quantities.AngleUnit.Radian,
        System.Math.Atan2(System.Math.Tan(project.X), System.Math.Cos(project.Y)),
        Quantities.AngleUnit.Radian,
        project.Z,
        Quantities.LengthUnit.Meter
      );
  }
}
