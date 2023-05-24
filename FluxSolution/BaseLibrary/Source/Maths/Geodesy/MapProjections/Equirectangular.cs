#if NET7_0_OR_GREATER
namespace Flux.Geometry.MapProjections
{
  // https://en.wikipedia.org/wiki/Equirectangular_projection
  public readonly record struct EquirectangularProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly EquirectangularProjection Default;

    public GeographicCoordinate CenterOfMap { get; init; }
    public double StandardParallels { get; init; }

    public CartesianCoordinate3<double> ProjectForward(IGeographicCoordinate project)
      => new(
        project.Altitude * (Units.Angle.ConvertDegreeToRadian(project.Longitude) - Units.Angle.ConvertDegreeToRadian(CenterOfMap.Longitude)) * System.Math.Cos(StandardParallels),
        project.Altitude * (Units.Angle.ConvertDegreeToRadian(project.Latitude) - Units.Angle.ConvertDegreeToRadian(CenterOfMap.Latitude)),
        project.Altitude
      );
    public IGeographicCoordinate ProjectReverse(ICartesianCoordinate3<double> project)
      => new GeographicCoordinate(
        Units.Angle.ConvertRadianToDegree(project.X / (project.Z * System.Math.Cos(StandardParallels)) + Units.Angle.ConvertDegreeToRadian(CenterOfMap.Longitude)),
        Units.Angle.ConvertRadianToDegree(project.Y / project.Z + Units.Angle.ConvertDegreeToRadian(CenterOfMap.Latitude)),
        project.Z
      );
  }

}
#endif
