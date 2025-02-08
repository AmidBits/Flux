namespace Flux
{
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Geometry.Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector2"/>.</summary>
    public static Geometry.Coordinates.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector2 source, double z = 0, double w = 0)
      => new(
        source.X, Units.LengthUnit.Meter,
        source.Y, Units.LengthUnit.Meter,
        z, Units.LengthUnit.Meter,
        w, Units.LengthUnit.Meter
      );

    /// <summary>Creates a new <see cref="Geometry.Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Geometry.Coordinates.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector3 source, double w = 0)
      => new(
        source.X, Units.LengthUnit.Meter,
        source.Y, Units.LengthUnit.Meter,
        source.Z, Units.LengthUnit.Meter,
        w, Units.LengthUnit.Meter
      );

    /// <summary>Creates a new <see cref="Geometry.Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector4"/>.</summary>
    public static Geometry.Coordinates.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector4 source)
      => new(
        source.X, Units.LengthUnit.Meter,
        source.Y, Units.LengthUnit.Meter,
        source.Z, Units.LengthUnit.Meter,
        source.W, Units.LengthUnit.Meter
      );

    /// <summary>Creates a new <see cref="Geometry.Coordinates.CartesianCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector256{T}"/> (with all components).</summary>
    public static Geometry.Coordinates.CartesianCoordinate ToCartesianCoordinate(this System.Runtime.Intrinsics.Vector256<double> source)
      => new(source[0], source[1], source[2], source[3]);

    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector256{T}"/> (with 3 [indices 0, 1 and 2] + 1 optional component).</summary>
    public static Geometry.Coordinates.CartesianCoordinate ToCartesianCoordinate3(this System.Runtime.Intrinsics.Vector256<double> source, double w)
      => new(source[0], source[1], source[2], w);

    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector256{T}"/> (with 2 [indices 0 and 1] + 2 optional components).</summary>
    public static Geometry.Coordinates.CartesianCoordinate ToCartesianCoordinate2(this System.Runtime.Intrinsics.Vector256<double> source, double z, double w)
      => new(source[0], source[1], z, w);

    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector256{T}"/> (with 1 [index 0] + 3 optional components).</summary>
    public static Geometry.Coordinates.CartesianCoordinate ToCartesianCoordinate1(this System.Runtime.Intrinsics.Vector256<double> source, double y, double z, double w)
      => new(source[0], y, z, w);
  }
}
