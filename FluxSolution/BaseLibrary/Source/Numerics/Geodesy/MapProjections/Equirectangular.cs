#if NET7_0_OR_GREATER
namespace Flux.Geometry.MapProjections
{
  // https://en.wikipedia.org/wiki/Equirectangular_projection
  public readonly record struct EquirectangularProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly EquirectangularProjection Default;

    public Coordinates.GeographicCoordinate CenterOfMap { get; init; }
    public double StandardParallels { get; init; }

    public System.Numerics.Vector3 ProjectForward(Coordinates.GeographicCoordinate project)
      => new(
        (float)(project.Altitude.Value * (project.Longitude.Value - CenterOfMap.Longitude.Value) * System.Math.Cos(StandardParallels)),
        (float)(project.Altitude.Value * (project.Latitude.Value - CenterOfMap.Latitude.Value)),
        (float)project.Altitude.Value
      );
    public Coordinates.GeographicCoordinate ProjectReverse(System.Numerics.Vector3 project)
      => new Coordinates.GeographicCoordinate(
        project.X / (project.Z * System.Math.Cos(StandardParallels)) + CenterOfMap.Longitude.Value,
        Quantities.AngleUnit.Radian,
        project.Y / project.Z + CenterOfMap.Latitude.Value,
        Quantities.AngleUnit.Radian,
        project.Z,
        Quantities.LengthUnit.Metre
      );
  }

}
#endif
