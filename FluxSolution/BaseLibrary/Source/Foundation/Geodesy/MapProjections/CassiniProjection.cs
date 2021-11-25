namespace Flux.MapProjections
{
  public record class CassiniProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public CartesianCoordinate3 ProjectForward(GeographicCoordinate project)
      => new(System.Math.Asin(project.Latitude.MathCos * project.Longitude.MathSin), System.Math.Atan(project.Latitude.MathTan / project.Longitude.MathCos), project.Altitude.Value);
    public GeographicCoordinate ProjectReverse(CartesianCoordinate3 project)
      => new(System.Math.Asin(project.MathSinY * project.MathCosX), System.Math.Atan2(project.MathTanX, project.MathCosY), project.Z);
  }
}
