namespace Flux.Numerics.MapProjections
{
  // https://en.wikipedia.org/wiki/Equirectangular_projection
  public record struct EquirectangularProjection
    : IMapForwardProjectable<double>, IMapReverseProjectable<double>
  {
    public static readonly EquirectangularProjection Default;

    public Numerics.GeographicCoordinate CenterOfMap { get; init; }
    public double StandardParallels { get; init; }

    public Numerics.CartesianCoordinate3<double> ProjectForward(Numerics.IGeographicCoordinate<double> project)
      => new(
        project.Altitude * (Quantities.Angle.ConvertDegreeToRadian(project.Longitude) - Quantities.Angle.ConvertDegreeToRadian(CenterOfMap.Longitude)) * System.Math.Cos(StandardParallels),
        project.Altitude * (Quantities.Angle.ConvertDegreeToRadian(project.Latitude) - Quantities.Angle.ConvertDegreeToRadian(CenterOfMap.Latitude)),
        project.Altitude
      );
    public Numerics.IGeographicCoordinate<double> ProjectReverse(Numerics.ICartesianCoordinate3<double> project)
      => new Numerics.GeographicCoordinate(
        Quantities.Angle.ConvertRadianToDegree(project.X / (project.Z * System.Math.Cos(StandardParallels)) + Quantities.Angle.ConvertDegreeToRadian(CenterOfMap.Longitude)),
        Quantities.Angle.ConvertRadianToDegree(project.Y / project.Z + Quantities.Angle.ConvertDegreeToRadian(CenterOfMap.Latitude)),
        project.Z
      );
  }

}
