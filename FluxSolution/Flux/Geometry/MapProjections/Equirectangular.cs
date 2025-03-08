namespace Flux.Geometry.MapProjections
{
  // https://en.wikipedia.org/wiki/Equirectangular_projection
  public readonly record struct EquirectangularProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly EquirectangularProjection Default;

    public CoordinateSystems.GeographicCoordinate CenterOfMap { get; init; }
    public double StandardParallels { get; init; }

    public System.Numerics.Vector3 ProjectForward(CoordinateSystems.GeographicCoordinate project)
      => new(
        (float)(project.Altitude.Value * (project.Longitude.Value - CenterOfMap.Longitude.Value) * double.Cos(StandardParallels)),
        (float)(project.Altitude.Value * (project.Latitude.Value - CenterOfMap.Latitude.Value)),
        (float)project.Altitude.Value
      );

    public CoordinateSystems.GeographicCoordinate ProjectReverse(System.Numerics.Vector3 project)
      => new(
        project.X / (project.Z * double.Cos(StandardParallels)) + CenterOfMap.Longitude.Value,
        Units.AngleUnit.Radian,
        project.Y / project.Z + CenterOfMap.Latitude.Value,
        Units.AngleUnit.Radian,
        project.Z,
        Units.LengthUnit.Meter
      );
  }

}
