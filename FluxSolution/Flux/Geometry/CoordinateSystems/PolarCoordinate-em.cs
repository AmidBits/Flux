namespace Flux
{
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Coordinates.PolarCoordinate"/> from a <see cref="System.Numerics.Vector2"/>.</summary>
    public static Geometry.Coordinates.PolarCoordinate ToPolarCoordinate(this System.Numerics.Vector2 source)
      => Geometry.Coordinates.PolarCoordinate.FromCartesianCoordinates(source.X, source.Y);

    /// <summary>Creates a new <see cref="Geometry.Coordinates.PolarCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector128{T}"/> (using the x, y components).</summary>
    public static Geometry.Coordinates.PolarCoordinate ToPolarCoordinate(this System.Runtime.Intrinsics.Vector128<double> source)
      => Geometry.Coordinates.PolarCoordinate.FromCartesianCoordinates(source[0], source[1]);

    /// <summary>Creates a new <see cref="Geometry.Coordinates.PolarCoordinate"/> from a <see cref="System.Runtime.Intrinsics.Vector256{T}"/> (using only the x, y components).</summary>
    public static Geometry.Coordinates.PolarCoordinate ToPolarCoordinate(this System.Runtime.Intrinsics.Vector256<double> source)
      => Geometry.Coordinates.PolarCoordinate.FromCartesianCoordinates(source[0], source[1]);
  }
}
