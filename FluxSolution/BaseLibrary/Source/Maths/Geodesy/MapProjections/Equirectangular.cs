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

    public System.Numerics.Vector3 ProjectForward(IGeographicCoordinate project)
      => new(
        (float)(project.Altitude * (project.LongitudeInRadians - CenterOfMap.LongitudeInRadians) * System.Math.Cos(StandardParallels)),
        (float)(project.Altitude * (project.LatitudeInRadians - CenterOfMap.LatitudeInRadians)),
        (float)project.Altitude
      );
    public IGeographicCoordinate ProjectReverse(System.Numerics.Vector3 project)
      => new GeographicCoordinate(
        project.X / (project.Z * System.Math.Cos(StandardParallels)) + CenterOfMap.LongitudeInRadians,
        Units.AngleUnit.Radian,
        project.Y / project.Z + CenterOfMap.LatitudeInRadians,
        Units.AngleUnit.Radian,
        project.Z
      );
  }

}
#endif
