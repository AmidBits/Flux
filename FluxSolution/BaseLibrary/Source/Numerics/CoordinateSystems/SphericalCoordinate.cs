
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Flux
{
  #region ExtensionMethods
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="Coordinates.SphericalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Coordinates.SphericalCoordinate ToSphericalCoordinate(this System.Numerics.Vector3 source)
    {
      var x = source.X;
      var y = source.Y;
      var z = source.Z;

      var x2y2 = x * x + y * y;

      return new(
        System.Math.Sqrt(x2y2 + z * z), Quantities.LengthUnit.Metre,
        System.Math.Atan2(System.Math.Sqrt(x2y2), z) + System.Math.PI, Quantities.AngleUnit.Radian,
        System.Math.Atan2(y, x) + System.Math.PI, Quantities.AngleUnit.Radian
      );
    }
  }
  #endregion

  namespace Coordinates
  {
    /// <summary>
    /// <para>Spherical coordinate.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/></para>
    /// </summary>
    /// <remarks>This implementation follows the ISO physics convention.</remarks>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct SphericalCoordinate
      : System.IFormattable
    {
      public static readonly SphericalCoordinate Zero;

      private readonly Quantities.Length m_radius;
      private readonly Quantities.Angle m_inclination;
      private readonly Quantities.Angle m_azimuth;

      public SphericalCoordinate(Quantities.Length radius, Quantities.Angle inclination, Quantities.Angle azimuth)
      {
        m_radius = radius;
        m_inclination = inclination;
        m_azimuth = azimuth;
      }

      public SphericalCoordinate(double radiusValue, Quantities.LengthUnit radiusUnit, double inclinationValue, Quantities.AngleUnit inclinationUnit, double azimuthValue, Quantities.AngleUnit azimuthUnit)
        : this(new Quantities.Length(radiusValue, radiusUnit), new Quantities.Angle(inclinationValue, inclinationUnit), new Quantities.Angle(azimuthValue, azimuthUnit)) { }

      public SphericalCoordinate(double radiusMeter, double inclinationRadian, double azimuthRadian)
        : this(radiusMeter, Quantities.LengthUnit.Metre, inclinationRadian, Quantities.AngleUnit.Radian, azimuthRadian, Quantities.AngleUnit.Radian) { }

      public void Deconstruct(out double radiusMeter, out double inclinationRadian, out double azimuthRadian)
      {
        radiusMeter = m_radius.Value;
        inclinationRadian = m_inclination.Value;
        azimuthRadian = m_azimuth.Value;
      }

      /// <summary>
      /// <para>The radius or radial distance is the Euclidean distance from the origin.</para>
      /// </summary>
      /// <remarks>If the radius is zero, both azimuth and inclination are arbitrary.</remarks>
      public Quantities.Length Radius { get => m_radius; init => m_radius = value; }

      /// <summary>
      /// <para>The inclination (or polar angle) is the signed angle from the zenith reference direction. (Elevation may be used as the polar angle instead of inclination). A.k.a. polar angle, colatitude, zenith angle, normal angle. This is equivalent to latitudinal or vertical angle in geographical coordinate systems.</para>
      /// </summary>
      /// <remarks>If the inclination is zero or 180 degrees (PI radians), the azimuth is arbitrary.</remarks>
      public Quantities.Angle Inclination { get => m_inclination; init => m_inclination = value; }

      /// <summary>
      /// <para>The azimuth (or azimuthal angle) is the signed angle measured from the azimuth reference direction to the orthogonal projection of the radial line segment on the reference plane. This is equivalent to longitudinal or horizontal angle in geographical coordinate systems.</para>
      /// </summary>
      public Quantities.Angle Azimuth { get => m_azimuth; init => m_azimuth = value; }

      /// <summary>
      /// <para>The elevation is the signed angle from the x-y reference plane to the radial line segment, where positive angles are designated as upward, towards the zenith reference. This is an option/alternative to <see cref="Inclination"/>.</para>
      /// </summary>
      /// <remarks>The elevation angle is 90 degrees (PI/2 radians) minus the <see cref="Inclination"/> angle.</remarks>
      public Quantities.Angle Elevation { get => new(ConvertInclinationToElevation(m_inclination.Value)); init => m_inclination = new(ConvertElevationToInclination(value.Value)); }

      public double SphereSurfaceArea => SphericalCoordinate.SurfaceAreaOfSphere(m_radius.Value);

      public CartesianCoordinate ToCartesianCoordinate()
      {
        var (x, y, z) = ToCartesianCoordinate3();

        return new(x, y, z);
      }

      /// <summary>Creates cartesian 3D coordinates from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public (double x, double y, double z) ToCartesianCoordinate3()
      {
        var (si, ci) = System.Math.SinCos(m_inclination.Value);
        var (sa, ca) = System.Math.SinCos(m_azimuth.Value);
        var r = m_radius.Value;

        return (
          r * si * ca,
          r * si * sa,
          r * ci
        );
      }

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public CylindricalCoordinate ToCylindricalCoordinate()
      {
        var (si, ci) = System.Math.SinCos(m_inclination.Value);
        var r = m_radius.Value;

        return new(
          r * si, Quantities.LengthUnit.Metre,
          m_azimuth.Value, Quantities.AngleUnit.Radian,
          r * ci, Quantities.LengthUnit.Metre
        );
      }

      /// <summary>Creates a new <see cref="GeographicCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public GeographicCoordinate ToGeographicCoordinate()
        // Translates the spherical coordinate to geographic coordinate transparently. I cannot recall the reason for the System.Math.PI involvement (see remarks).
        => new(
          /*System.Math.PI -*/ m_inclination.Value - System.Math.PI / 2, Quantities.AngleUnit.Radian,
          m_azimuth.Value /*- System.Math.PI*/, Quantities.AngleUnit.Radian,
          m_radius.Value, Quantities.LengthUnit.Metre
        );

      /// <summary>Creates a new <see cref="System.Numerics.Vector3"/> from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public System.Numerics.Vector3 ToVector3()
      {
        var (x, y, z) = ToCartesianCoordinate3();

        return new((float)x, (float)y, (float)z);
      }

      #region Static methods

      /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>
      public static double ConvertInclinationToElevation(double inclination)
        => System.Math.PI / 2 - inclination;

      /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>
      public static double ConvertElevationToInclination(double elevation)
        => System.Math.PI / 2 - elevation;

      /// <summary>
      /// <para>Computes the surface area of a hemisphere with the specified <paramref name="radius"/>.</para>
      /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
      /// </summary>
      /// <param name="radius">The radius of the hemisphere.</param>
      public static double SurfaceAreaOfHemisphere(double radius) => 3 * System.Math.PI * radius * radius;

      /// <summary>
      /// <para>Computes the surface area of a sphere with the specified <paramref name="radius"/>.</para>
      /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
      /// </summary>
      /// <param name="radius">The radius of the sphere.</param>
      public static double SurfaceAreaOfSphere(double radius) => 4 * System.Math.PI * radius * radius;

      #endregion // Static methods

      #region Implemented interfaces

      public string ToString(string? format, System.IFormatProvider? provider)
        => $"<{m_radius.Value.ToString(format ?? "N3", provider)}, {m_inclination.ToUnitValueString(Quantities.AngleUnit.Degree, format ?? "N3", provider, UnicodeSpacing.Space, true)} / {Elevation.ToUnitValueString(Quantities.AngleUnit.Degree, format ?? "N3", provider, UnicodeSpacing.Space, true)}, {m_azimuth.ToUnitValueString(Quantities.AngleUnit.Degree, format ?? "N3", provider, UnicodeSpacing.Space, true)}>";

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}