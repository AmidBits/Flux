namespace Flux
{
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Geometry.Coordinates.CylindricalCoordinate ToCylindricalCoordinate(this System.Numerics.Vector3 source)
      => Geometry.Coordinates.CylindricalCoordinate.FromCartesianCoordinates(source.X, source.Y, source.Z);

    /// <summary>Creates a new <see cref="Geometry.Coordinates.CylindricalCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector256{T}"/> (with x, y, z components).</summary>
    public static Geometry.Coordinates.CylindricalCoordinate ToCylindricalCoordinate(this System.Runtime.Intrinsics.Vector256<double> source)
      => Geometry.Coordinates.CylindricalCoordinate.FromCartesianCoordinates(source[0], source[1], source[2]);
  }
}
