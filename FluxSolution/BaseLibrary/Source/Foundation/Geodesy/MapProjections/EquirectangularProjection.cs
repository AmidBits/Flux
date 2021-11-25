namespace Flux.MapProjections
{
  public record class EquirectangularProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public GeographicCoordinate CenterOfMap { get; init; }
    public double StandardParallels { get; init; }

    public CartesianCoordinate3 ProjectForward(GeographicCoordinate project)
      => new(project.Altitude.Value * (project.Longitude.Radian - CenterOfMap.Longitude.Radian) * System.Math.Cos(StandardParallels), project.Altitude.Value * (project.Latitude.Radian - CenterOfMap.Latitude.Radian), project.Altitude.Value);
    public GeographicCoordinate ProjectReverse(CartesianCoordinate3 project)
      => new(project.X / (project.Z * System.Math.Cos(StandardParallels)) + CenterOfMap.Longitude.Value, project.Y / project.Z + CenterOfMap.Latitude.Value, project.Z);
  }

}
