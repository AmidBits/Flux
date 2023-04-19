#if NET7_0_OR_GREATER
namespace Flux.Numerics.MapProjections
{
  // https://en.wikipedia.org/wiki/Cassini_projection
  public readonly record struct CassiniProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly CassiniProjection Default;

    //#pragma warning disable CA1822 // Mark members as static
    public Numerics.CartesianCoordinate3<double> ProjectForward(Numerics.IGeographicCoordinate project)
      => new(
        System.Math.Asin(System.Math.Cos(Units.Angle.ConvertDegreeToRadian(project.Latitude)) * System.Math.Sin(Units.Angle.ConvertDegreeToRadian(project.Longitude))),
        System.Math.Atan(System.Math.Tan(Units.Angle.ConvertDegreeToRadian(project.Latitude)) / System.Math.Cos(Units.Angle.ConvertDegreeToRadian(project.Longitude))),
        project.Altitude
      );
    public Numerics.IGeographicCoordinate ProjectReverse(Numerics.ICartesianCoordinate3<double> project)
      => new Numerics.GeographicCoordinate(
        Units.Angle.ConvertRadianToDegree(System.Math.Asin(System.Math.Sin(project.Y) * System.Math.Cos(project.X))),
        Units.Angle.ConvertRadianToDegree(System.Math.Atan2(System.Math.Tan(project.X), System.Math.Cos(project.Y))),
        project.Z
      );
    //#pragma warning restore CA1822 // Mark members as static
  }
}
#endif
