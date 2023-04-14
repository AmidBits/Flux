namespace Flux.Numerics.MapProjections
{
  // https://en.wikipedia.org/wiki/Equirectangular_projection
  public readonly record struct EquirectangularProjection
    : IMapForwardProjectable<double>, IMapReverseProjectable<double>
  {
    public static readonly EquirectangularProjection Default;

    public Numerics.GeographicCoordinate CenterOfMap { get; init; }
    public double StandardParallels { get; init; }

    public Numerics.CartesianCoordinate3<double> ProjectForward(Numerics.IGeographicCoordinate<double> project)
      => new(
        project.Altitude * (Units.Angle.ConvertDegreeToRadian(project.Longitude) - Units.Angle.ConvertDegreeToRadian(CenterOfMap.Longitude)) * System.Math.Cos(StandardParallels),
        project.Altitude * (Units.Angle.ConvertDegreeToRadian(project.Latitude) - Units.Angle.ConvertDegreeToRadian(CenterOfMap.Latitude)),
        project.Altitude
      );
    public Numerics.IGeographicCoordinate<double> ProjectReverse(Numerics.ICartesianCoordinate3<double> project)
      => new Numerics.GeographicCoordinate(
        Units.Angle.ConvertRadianToDegree(project.X / (project.Z * System.Math.Cos(StandardParallels)) + Units.Angle.ConvertDegreeToRadian(CenterOfMap.Longitude)),
        Units.Angle.ConvertRadianToDegree(project.Y / project.Z + Units.Angle.ConvertDegreeToRadian(CenterOfMap.Latitude)),
        project.Z
      );
  }

}
