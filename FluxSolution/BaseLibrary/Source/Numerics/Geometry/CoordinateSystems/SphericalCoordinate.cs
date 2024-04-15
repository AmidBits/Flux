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
        System.Math.Sqrt(x2y2 + source.Z * source.Z), Quantities.LengthUnit.Metre,
        System.Math.Atan2(System.Math.Sqrt(x2y2), source.Z) + System.Math.PI, Quantities.AngleUnit.Radian,
        System.Math.Atan2(source.Y, source.X) + System.Math.PI, Quantities.AngleUnit.Radian
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
    /// <remarks>This implementation follows the ISO physics convention. All angles in radians, unless noted otherwise.</remarks>
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

      //public SphericalCoordinate(Units.Length radius, Units.Angle inclination, Units.Azimuth azimuth)
      //  : this(radius.Value, inclination.Value, azimuth.Angle.Value)
      //{ }

      public SphericalCoordinate(double radiusValue, Quantities.LengthUnit radiusUnit, double inclinationValue, Quantities.AngleUnit inclinationUnit, double azimuthValue, Quantities.AngleUnit azimuthUnit)
        : this(new Quantities.Length(radiusValue, radiusUnit), new Quantities.Angle(inclinationValue, inclinationUnit), new Quantities.Angle(azimuthValue, azimuthUnit))
      { }

      /// <summary>
      /// <para>Radius, (length) unit of meter. A.k.a. radial distance, radial coordinate.</para>
      /// </summary>
      /// <remarks>If the radius is zero, both azimuth and inclination are arbitrary.</remarks>
      public Quantities.Length Radius { get => m_radius; init => m_radius = value; }
      /// <summary>
      /// <para>Inclination angle, unit of radian. A.k.a. polar angle, colatitude, zenith angle, normal angle. This is equivalent to latitude in geographical coordinate systems.</para>
      /// </summary>
      /// <remarks>If the inclination is zero or 180 degrees (PI radians), the azimuth is arbitrary.</remarks>
      public Quantities.Angle Inclination { get => m_inclination; init => m_inclination = value; }
      /// <summary>
      /// <para>Azimuth angle, unit of radian. This is equivalent to longitude in geographical coordinate systems.</para>
      /// </summary>
      public Quantities.Angle Azimuth { get => m_azimuth; init => m_azimuth = value; }

      /// <summary>
      /// <para>Elevation angle, unit of radian. This is an option/alternative to <see cref="Inclination"/>.</para>
      /// </summary>
      /// <remarks>The elevation angle is 90 degrees (PI/2 radians) minus the <see cref="Inclination"/> angle.</remarks>
      public Quantities.Angle Elevation { get => new(ConvertInclinationToElevation(m_inclination.Value)); init => m_inclination = new(ConvertElevationToInclination(value.Value)); }

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
        => new(
          System.Math.PI - m_inclination.Value - System.Math.PI / 2, Quantities.AngleUnit.Radian,
          m_azimuth.Value - System.Math.PI, Quantities.AngleUnit.Radian,
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

      /// <summary>Computes the area of the specified sphere.</summary>
      /// <param name="radius">The radius of a sphere.</param>
      public static double AreaOfSphere(double radius) => 4d * System.Math.PI * System.Math.Pow(radius, 2);

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

        return $"<{m_radius.Value.ToString(format)}, {m_inclination.ToUnitValueString(Quantities.AngleUnit.Degree, format)} ({m_inclination.Value.ToString(format)}) | {Elevation.ToUnitValueString(Quantities.AngleUnit.Degree, format)} ({Elevation.Value.ToString(format)}), {new Quantities.Azimuth(m_azimuth.Value, Quantities.AngleUnit.Radian).ToString(format, null)} ({m_azimuth.Value.ToString(format)})>";
      }
    }
  }
}
