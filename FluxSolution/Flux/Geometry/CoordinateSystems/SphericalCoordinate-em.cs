namespace Flux
{
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Geometry.CoordinateSystems.SphericalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Geometry.CoordinateSystems.SphericalCoordinate ToSphericalCoordinate(this System.Numerics.Vector3 source)
      => Geometry.CoordinateSystems.SphericalCoordinate.FromCartesianCoordinates(source.X, source.Y, source.Z);
  }
}
