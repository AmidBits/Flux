namespace Flux
{
  #region ExtensionMethods

  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Geometry.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Geometry.Coordinates.CylindricalCoordinate ToCylindricalCoordinate(this System.Numerics.Vector3 source)
      => new(
        System.Math.Sqrt(source.X * source.X + source.Y * source.Y),
        (System.Math.Atan2(source.Y, source.X) + System.Math.Tau) % System.Math.Tau,
        source.Z
      );
  }

  #endregion

  namespace Geometry.Coordinates
  {
    /// <summary>
    /// <para>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/></para>
    /// </summary>
    /// <remarks>All angles in radians, unless noted otherwise.</remarks>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct CylindricalCoordinate
      : System.IFormattable//, ICylindricalCoordinate<double>
    {
      public static readonly CylindricalCoordinate Zero;

      private readonly double m_radius;
      private readonly double m_azimuth; // In radians.
      private readonly double m_height;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="radius"></param>
      /// <param name="azimuth"></param>
      /// <param name="height"></param>
      public CylindricalCoordinate(double radius, double azimuth, double height)
      {
        m_radius = radius;
        m_azimuth = azimuth;
        m_height = height;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="radius"></param>
      /// <param name="azimuth"></param>
      /// <param name="height"></param>
      public CylindricalCoordinate(Units.Length radius, Units.Azimuth azimuth, Units.Length height)
        : this(radius.Value, azimuth.Angle.Value, height.Value)
      { }

      public CylindricalCoordinate(double radiusValue, Units.LengthUnit radiusUnit, double azimuthValue, Units.AngleUnit azimuthUnit, double heightValue, Units.LengthUnit heightUnit)
        : this(new Units.Length(radiusValue, radiusUnit), new Units.Azimuth(azimuthValue, azimuthUnit), new Units.Length(heightValue, heightUnit))
      { }

      /// <summary>
      /// <para>Radius, (length) unit of meter. A.k.a. radial distance, or axial distance.</para>
      /// </summary>
      public double Radius { get => m_radius; init => m_radius = value; }
      /// <summary>
      /// <para>Azimuth angle, unit of radian. A.k.a. angular position.</para>
      /// </summary>
      public double Azimuth { get => m_azimuth; init => m_azimuth = value; }
      /// <summary>
      /// <para>Height, (length) unit of meter. A.k.a. altitude (if the reference plane is considered horizontal), longitudinal position, axial position, or axial coordinate.</para>
      /// </summary>
      public double Height { get => m_height; init => m_height = value; }

      /// <summary>Creates cartesian 3D coordinates from the <see cref="CylindricalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public (double x, double y, double z) ToCartesianCoordinate3()
      {
        var (sin, cos) = System.Math.SinCos(m_azimuth);

        return (
          m_radius * cos,
          m_radius * sin,
          m_height
        );
      }

      /// <summary>Creates a new <see cref="PolarCoordinate"/> from the <see cref="CylindricalCoordinate"/> by removing the third component "height".</summary>
      /// <remarks>All angles in radians.</remarks>
      public PolarCoordinate ToPolarCoordinate()
        => new(
          m_radius,
          m_azimuth
        );

      /// <summary>
      /// <para>Creates a new <see cref="SphericalCoordinate"/> from the <see cref="CylindricalCoordinate"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system#Spherical_coordinates"/></para>
      /// </summary>
      /// <remarks>All angles in radians.</remarks>
      public SphericalCoordinate ToSphericalCoordinate()
        => new(
          System.Math.Sqrt(m_radius * m_radius + m_height * m_height),
          (System.Math.PI / 2) - System.Math.Atan(m_height / m_radius), // "System.Math.Atan(m_radius / m_height);", does NOT work for Takapau, New Zealand. Have to use elevation math instead of inclination, and investigate.
          m_azimuth
        );

      /// <summary>Creates a new <see cref="System.Numerics.Vector3"/> from the <see cref="CylindricalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public System.Numerics.Vector3 ToVector3()
      {
        var (x, y, z) = ToCartesianCoordinate3();

        return new((float)x, (float)y, (float)z);
      }

      #region Static methods

      #endregion // Static methods

      public string ToString(string? format, System.IFormatProvider? provider)
      {
        if (string.IsNullOrWhiteSpace(format)) format = "N3";

        return $"<{m_radius.ToString(format)}, {new Units.Azimuth(m_azimuth, Units.AngleUnit.Radian).ToString(format, null)} ({m_azimuth.ToString(format)}), {m_height.ToString(format)}>";
      }
    }
  }
}