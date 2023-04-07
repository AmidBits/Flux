namespace Flux.Numerics.MapProjections
{
  // https://en.wikipedia.org/wiki/Cassini_projection
  public readonly record struct CassiniProjection
    : IMapForwardProjectable<double>, IMapReverseProjectable<double>
  {
    public static readonly CassiniProjection Default;

    //#pragma warning disable CA1822 // Mark members as static
    public Numerics.CartesianCoordinate3<double> ProjectForward(Numerics.IGeographicCoordinate<double> project)
      => new(
        System.Math.Asin(System.Math.Cos(Quantities.Angle.ConvertDegreeToRadian(project.Latitude)) * System.Math.Sin(Quantities.Angle.ConvertDegreeToRadian(project.Longitude))),
        System.Math.Atan(System.Math.Tan(Quantities.Angle.ConvertDegreeToRadian(project.Latitude)) / System.Math.Cos(Quantities.Angle.ConvertDegreeToRadian(project.Longitude))),
        project.Altitude
      );
    public Numerics.IGeographicCoordinate<double> ProjectReverse(Numerics.ICartesianCoordinate3<double> project)
      => new Numerics.GeographicCoordinate(
        Quantities.Angle.ConvertRadianToDegree(System.Math.Asin(System.Math.Sin(project.Y) * System.Math.Cos(project.X))),
        Quantities.Angle.ConvertRadianToDegree(System.Math.Atan2(System.Math.Tan(project.X), System.Math.Cos(project.Y))),
        project.Z
      );
    //#pragma warning restore CA1822 // Mark members as static
  }
}
