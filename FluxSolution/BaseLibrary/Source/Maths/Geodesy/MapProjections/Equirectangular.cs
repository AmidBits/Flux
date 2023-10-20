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
        project.Altitude * (project.LongitudeInRadians - CenterOfMap.LongitudeInRadians) * System.Math.Cos(StandardParallels),
        project.Altitude * (project.LatitudeInRadians - CenterOfMap.LatitudeInRadians),
        project.Altitude
      );
    public IGeographicCoordinate ProjectReverse(ICartesianCoordinate3<double> project)
      => new GeographicCoordinate(
        Units.Angle.ConvertRadianToDegree(project.X / (project.Z * System.Math.Cos(StandardParallels)) + CenterOfMap.LongitudeInRadians),
        Units.Angle.ConvertRadianToDegree(project.Y / project.Z + CenterOfMap.LatitudeInRadians),
        project.Z
      );
  }

}
#endif
