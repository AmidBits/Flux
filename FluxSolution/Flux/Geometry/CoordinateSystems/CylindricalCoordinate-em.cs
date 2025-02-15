namespace Flux
{
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Geometry.CoordinateSystems.CylindricalCoordinate ToCylindricalCoordinate(this System.Numerics.Vector3 source)
      => Geometry.CoordinateSystems.CylindricalCoordinate.FromCartesianCoordinates(source.X, source.Y, source.Z);
  }
}
