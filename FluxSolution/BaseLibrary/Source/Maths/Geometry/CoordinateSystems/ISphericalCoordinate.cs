namespace Flux
{
  #region ExtensionMethods
  public static partial class GeometryExtensionMethods
  {
    /// <summary>Converts the spherical coordinates to cartesian 3D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Geometry.CartesianCoordinate3<double> ToCartesianCoordinate3(this Geometry.ISphericalCoordinate source)
    {

      var (si, ci) = System.Math.SinCos(source.Inclination);
      var (sa, ca) = System.Math.SinCos(source.Azimuth);

      return new(
        source.Radius * si * ca,
        source.Radius * si * sa,
        source.Radius * ci
      );
    }

    /// <summary>Converts the spherical coordinates to cylindrical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Geometry.CylindricalCoordinate ToCylindricalCoordinate(this Geometry.ISphericalCoordinate source)
    {
      var (si, ci) = System.Math.SinCos(source.Inclination);

      return new(
        source.Radius * si,
        source.Azimuth,
        source.Radius * ci
      );
    }

    /// <summary>Converts the spherical coordinates to a geographic coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Geometry.GeographicCoordinate ToGeographicCoordinate(this Geometry.ISphericalCoordinate source)
      => new(
        Units.Angle.ConvertRadianToDegree(System.Math.PI - source.Inclination - System.Math.PI / 2),
        Units.Angle.ConvertRadianToDegree(source.Azimuth - System.Math.PI),
        source.Radius
      );
  }
  #endregion ExtensionMethods

  namespace Geometry
  {
    /// <summary>Spherical coordinate.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
    public interface ISphericalCoordinate
    {
      /// <summary>Radius. A.k.a. radial distance, radial coordinate.</summary>
      /// <remarks>If the radius is zero, both azimuth and inclination are arbitrary.</remarks>
      double Radius { get; init; }
      /// <summary>The inclination angle in radians. A.k.a. polar angle, colatitude, zenith angle, normal angle.</summary>
      /// <remarks>If the inclination is zero or 180 degrees (π radians), the azimuth is arbitrary.</remarks>
      double Inclination { get; init; }
      /// <summary>The azimuthal angle in radians.</summary>
      double Azimuth { get; init; }

      /// <summary>The elevation angle in radians. This is an option/alternative to <see cref="Inclination"/>.</summary>
      /// <remarks>The elevation angle is 90 degrees (π/2 radians) minus the <see cref="Inclination"/> angle.</remarks>
      double Elevation { get; init; }
    }
  }
}
