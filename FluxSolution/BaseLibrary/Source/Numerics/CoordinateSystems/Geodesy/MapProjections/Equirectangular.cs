namespace Flux.MapProjections
{
  // https://en.wikipedia.org/wiki/Equirectangular_projection
  public record struct EquirectangularProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly EquirectangularProjection Default;

    public GeographicCoordinate CenterOfMap { get; init; }
    public double StandardParallels { get; init; }

    public CartesianCoordinate3<double> ProjectForward(GeographicCoordinate project)
      => new(project.Altitude.Value * (project.Longitude.ToRadians() - CenterOfMap.Longitude.ToRadians()) * System.Math.Cos(StandardParallels), project.Altitude.Value * (project.Latitude.ToRadians() - CenterOfMap.Latitude.ToRadians()), project.Altitude.Value);
    public GeographicCoordinate ProjectReverse(ICartesianCoordinate3<double> project)
      => new(project.X / (project.Z * System.Math.Cos(StandardParallels)) + CenterOfMap.Longitude.Value, project.Y / project.Z + CenterOfMap.Latitude.Value, project.Z);
  }

}
