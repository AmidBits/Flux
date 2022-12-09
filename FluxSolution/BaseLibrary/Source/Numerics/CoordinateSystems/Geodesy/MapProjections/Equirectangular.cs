namespace Flux.MapProjections
{
  // https://en.wikipedia.org/wiki/Equirectangular_projection
  public record struct EquirectangularProjection
    : IMapForwardProjectable<double>, IMapReverseProjectable<double>
  {
    public static readonly EquirectangularProjection Default;

    public CoordinateSystems.GeographicCoordinate CenterOfMap { get; init; }
    public double StandardParallels { get; init; }

    public CoordinateSystems.CartesianCoordinate3<double> ProjectForward(IGeographicCoordinate<double> project)
      => new(
        project.Altitude * (Quantities.Angle.ConvertDegreeToRadian(project.Longitude) - Quantities.Angle.ConvertDegreeToRadian(CenterOfMap.Longitude)) * System.Math.Cos(StandardParallels),
        project.Altitude * (Quantities.Angle.ConvertDegreeToRadian(project.Latitude) - Quantities.Angle.ConvertDegreeToRadian(CenterOfMap.Latitude)),
        project.Altitude
      );
    public IGeographicCoordinate<double> ProjectReverse(ICartesianCoordinate3<double> project)
      => new CoordinateSystems.GeographicCoordinate(
        Quantities.Angle.ConvertRadianToDegree(project.X / (project.Z * System.Math.Cos(StandardParallels)) + Quantities.Angle.ConvertDegreeToRadian(CenterOfMap.Longitude)),
        Quantities.Angle.ConvertRadianToDegree(project.Y / project.Z + Quantities.Angle.ConvertDegreeToRadian(CenterOfMap.Latitude)),
        project.Z
      );
  }

}
