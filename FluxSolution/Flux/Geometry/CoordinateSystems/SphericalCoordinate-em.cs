namespace Flux
{
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Geometry.Coordinates.SphericalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Geometry.Coordinates.SphericalCoordinate ToSphericalCoordinate(this System.Numerics.Vector3 source)
      => Geometry.Coordinates.SphericalCoordinate.FromCartesianCoordinates(source.X, source.Y, source.Z);

    /// <summary>Creates a new <see cref="Geometry.Coordinates.SphericalCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector256{T}"/> (with x, y, z components).</summary>
    public static Geometry.Coordinates.SphericalCoordinate ToSphericalCoordinate(this System.Runtime.Intrinsics.Vector256<double> source)
      => Geometry.Coordinates.SphericalCoordinate.FromCartesianCoordinates(source[0], source[1], source[2]);
  }
}
