namespace Flux
{
  public static partial class CoordinateSystemsExtensions
  {
    /// <summary>Creates a new <see cref="Geometry.CoordinateSystems.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector2"/>.</summary>
    public static CoordinateSystems.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector2 source, double z = 0, double w = 0)
      => new(source.X, source.Y, z, w);

    /// <summary>Creates a new <see cref="Geometry.CoordinateSystems.CartesianCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static CoordinateSystems.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector3 source, double w = 0)
      => new(source.X, source.Y, source.Z, w);

    /// <summary>Creates a new <see cref="Geometry.CoordinateSystems.CartesianCoordinate"/> from a <see cref="System.Numerics.Vector4"/>.</summary>
    public static CoordinateSystems.CartesianCoordinate ToCartesianCoordinate(this System.Numerics.Vector4 source)
      => new(source.X, source.Y, source.Z, source.W);

    ///// <summary>Creates a new <see cref="Geometry.HexCoordinate{TSelf}"/> from a <see cref="Geometry.CoordinateSystems.CartesianCoordinate"/>.</summary>
    //public static CoordinateSystems.HexCoordinate<TResult> ToHexCoordinate<TResult>(this CoordinateSystems.CartesianCoordinate source, UniversalRounding mode, out TResult q, out TResult r, out TResult s)
    //  where TResult : System.Numerics.INumber<TResult>
    //{
    //  var (x, y, z) = source;

    //  CoordinateSystems.HexCoordinate.Round<double, TResult>(x, y, z, mode, out q, out r, out s);

    //  return new(
    //    q,
    //    r,
    //    s
    //  );
    //}
  }
}
