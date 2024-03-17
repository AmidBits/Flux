namespace Flux
{
  #region ExtensionMethods
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Geometry.SphericalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Geometry.Coordinates.SphericalCoordinate ToSphericalCoordinate(this System.Numerics.Vector3 source)
    {
      var x2y2 = source.X * source.X + source.Y * source.Y;

      return new(
        System.Math.Sqrt(x2y2 + source.Z * source.Z),
        System.Math.Atan2(System.Math.Sqrt(x2y2), source.Z) + System.Math.PI,
        System.Math.Atan2(source.Y, source.X) + System.Math.PI
      );
    }
  }
  #endregion

  namespace Geometry.Coordinates
  {
    /// <summary>
    /// <para>Spherical coordinate.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/></para>
    /// </summary>
    /// <remarks>All angles in radians, unless noted otherwise.</remarks>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct SphericalCoordinate
      : System.IFormattable//, ISphericalCoordinate<double>
    {
      public static readonly SphericalCoordinate Zero;

      private readonly double m_radius;
      private readonly double m_inclination; // In radians.
      private readonly double m_azimuth; // In radians.

      public SphericalCoordinate(double radius, double inclination, double azimuth)
      {
        m_radius = radius;
        m_inclination = inclination;
        m_azimuth = azimuth;
      }

      public SphericalCoordinate(Units.Length radius, Units.Angle inclination, Units.Azimuth azimuth)
        : this(radius.Value, inclination.Value, azimuth.Angle.Value)
      { }

      public SphericalCoordinate(double radiusValue, Units.LengthUnit radiusUnit, double inclinationValue, Units.AngleUnit inclinationUnit, double azimuthValue, Units.AngleUnit azimuthUnit)
        : this(new Units.Length(radiusValue, radiusUnit), new Units.Angle(inclinationValue, inclinationUnit), new Units.Azimuth(azimuthValue, azimuthUnit))
      { }

      /// <summary>
      /// <para>Radius, (length) unit of meter. A.k.a. radial distance, radial coordinate.</para>
      /// </summary>
      /// <remarks>If the radius is zero, both azimuth and inclination are arbitrary.</remarks>
      public double Radius { get => m_radius; init => m_radius = value; }
      /// <summary>
      /// <para>Inclination angle, unit of radian. A.k.a. polar angle, colatitude, zenith angle, normal angle. This is equivalent to latitude in geographical coordinate systems.</para>
      /// </summary>
      /// <remarks>If the inclination is zero or 180 degrees (PI radians), the azimuth is arbitrary.</remarks>
      public double Inclination { get => m_inclination; init => m_inclination = value; }
      /// <summary>
      /// <para>Azimuth angle, unit of radian. This is equivalent to longitude in geographical coordinate systems.</para>
      /// </summary>
      public double Azimuth { get => m_azimuth; init => m_azimuth = value; }

      /// <summary>
      /// <para>Elevation angle, unit of radian. This is an option/alternative to <see cref="Inclination"/>.</para>
      /// </summary>
      /// <remarks>The elevation angle is 90 degrees (PI/2 radians) minus the <see cref="Inclination"/> angle.</remarks>
      public double Elevation { get => ConvertInclinationToElevation(m_inclination); init => m_inclination = ConvertElevationToInclination(value); }

      /// <summary>Creates cartesian 3D coordinates from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public (double x, double y, double z) ToCartesianCoordinate3()
      {
        var (si, ci) = System.Math.SinCos(m_inclination);
        var (sa, ca) = System.Math.SinCos(m_azimuth);

        return (
          m_radius * si * ca,
          m_radius * si * sa,
          m_radius * ci
        );
      }

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public CylindricalCoordinate ToCylindricalCoordinate()
      {
        var (si, ci) = System.Math.SinCos(m_inclination);

        return new(
          m_radius * si,
          m_azimuth,
          m_radius * ci
        );
      }

      /// <summary>Creates a new <see cref="GeographicCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public GeographicCoordinate ToGeographicCoordinate()
        => new(
          new Units.Latitude(System.Math.PI - m_inclination - System.Math.PI / 2, Units.AngleUnit.Radian),
          new Units.Longitude(m_azimuth - System.Math.PI, Units.AngleUnit.Radian),
          new Units.Length(m_radius)
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

      #endregion Static methods

      public string ToString(string? format, System.IFormatProvider? provider)
      {
        if (string.IsNullOrWhiteSpace(format)) format = "N6";

        return $"<{m_radius.ToString(format)}, {new Units.Angle(m_inclination).ToUnitValueString(Units.AngleUnit.Degree, format)} ({m_inclination.ToString(format)}) | {new Units.Angle(Elevation).ToUnitValueString(Units.AngleUnit.Degree, format)} ({Elevation.ToString(format)}), {new Units.Azimuth(m_azimuth, Units.AngleUnit.Radian).ToString(format, null)} ({m_azimuth.ToString(format)})>";
      }
    }
  }
}
