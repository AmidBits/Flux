namespace Flux
{
  #region ExtensionMethods
  public static partial class NumericsExtensionMethods
  {
    /// <summary>Converts the polar coordinates to cartesian 2D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Numerics.CartesianCoordinate2<double> ToCartesianCoordinate2(this Numerics.IPolarCoordinate source)
    {
      var (sa, ca) = System.Math.SinCos(source.Azimuth);

      return new(
        source.Radius * ca,
        source.Radius * sa
      );
    }

    /// <summary>Converts the polar coordinates to a complex number.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static System.Numerics.Complex ToComplex(this Numerics.IPolarCoordinate source)
      => System.Numerics.Complex.FromPolarCoordinates(
        source.Radius,
        source.Azimuth
      );
  }
  #endregion ExtensionMethods

  namespace Numerics
  {
    /// <summary>The polar coordinate system is a two-dimensional coordinate system in which each point on a plane is determined by a distance from a reference point and an angle from a reference direction.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
    public interface IPolarCoordinate
    {
      /// <summary>Radius. A.k.a. radial coordinate, or radial distance.</summary>
      double Radius { get; init; }
      /// <summary>The azimuth angle in radians. A.k.a. angular coordinate, or polar angle.</summary>
      /// <remarks>The angle φ is defined to start at 0° from a reference direction, and to increase for rotations in either clockwise (cw) or counterclockwise (ccw) orientation.</remarks>
      double Azimuth { get; init; }
    }
  }
}
