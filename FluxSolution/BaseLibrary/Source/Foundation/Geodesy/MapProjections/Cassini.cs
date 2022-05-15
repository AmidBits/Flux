namespace Flux.MapProjections
{
  // https://en.wikipedia.org/wiki/Cassini_projection
  public struct CassiniProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly CassiniProjection Default;

    //#pragma warning disable CA1822 // Mark members as static
    public CartesianCoordinate3 ProjectForward(GeographicCoordinate project)
      => new(System.Math.Asin(System.Math.Cos(project.Latitude.Radian) * System.Math.Sin(project.Longitude.Radian)), System.Math.Atan(System.Math.Tan(project.Latitude.Radian) / System.Math.Cos(project.Longitude.Radian)), project.Altitude.Value);
    public GeographicCoordinate ProjectReverse(CartesianCoordinate3 project)
      => new(System.Math.Asin(System.Math.Sin(project.Y) * System.Math.Cos(project.X)), System.Math.Atan2(System.Math.Tan(project.X), System.Math.Cos(project.Y)), project.Z);
    //#pragma warning restore CA1822 // Mark members as static
  }
}
