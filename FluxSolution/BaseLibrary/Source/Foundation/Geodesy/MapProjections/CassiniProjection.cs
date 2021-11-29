namespace Flux.MapProjections
{
  // https://en.wikipedia.org/wiki/Cassini_projection
#if NET5_0
  public struct CassiniProjection
    : IMapForwardProjectable, IMapReverseProjectable
#elif NET6_0_OR_GREATER
  public record struct CassiniProjection
#endif
  {
    public static readonly CassiniProjection Default;

    public CartesianCoordinate3 ProjectForward(GeographicCoordinate project)
      => new(System.Math.Asin(project.Latitude.MathCos * project.Longitude.MathSin), System.Math.Atan(project.Latitude.MathTan / project.Longitude.MathCos), project.Altitude.Value);
    public GeographicCoordinate ProjectReverse(CartesianCoordinate3 project)
      => new(System.Math.Asin(project.MathSinY * project.MathCosX), System.Math.Atan2(project.MathTanX, project.MathCosY), project.Z);
  }
}
