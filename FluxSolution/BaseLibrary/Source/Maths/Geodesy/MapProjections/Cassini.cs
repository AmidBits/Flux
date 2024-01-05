#if NET7_0_OR_GREATER
namespace Flux.Geometry.MapProjections
{
  // https://en.wikipedia.org/wiki/Cassini_projection
  public readonly record struct CassiniProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly CassiniProjection Default;

    //#pragma warning disable CA1822 // Mark members as static
    public System.Numerics.Vector3 ProjectForward(IGeographicCoordinate project)
      => new(
        (float)System.Math.Asin(System.Math.Cos(project.LatitudeInRadians) * System.Math.Sin(project.LongitudeInRadians)),
        (float)System.Math.Atan(System.Math.Tan(project.LatitudeInRadians) / System.Math.Cos(project.LongitudeInRadians)),
        (float)project.Altitude
      );
    public IGeographicCoordinate ProjectReverse(System.Numerics.Vector3 project)
      => new GeographicCoordinate(
        Units.Angle.RadianToDegree(System.Math.Asin(System.Math.Sin(project.Y) * System.Math.Cos(project.X))),
        Units.Angle.RadianToDegree(System.Math.Atan2(System.Math.Tan(project.X), System.Math.Cos(project.Y))),
        project.Z
      );
    //#pragma warning restore CA1822 // Mark members as static
  }
}
#endif
