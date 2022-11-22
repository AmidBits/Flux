namespace Flux
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct SphericalCoordinate
    : ISphericalCoordinate<double>
  {
    private readonly double m_radius;
    private readonly double m_inclination;
    private readonly double m_azimuth;

    public SphericalCoordinate(double radius, double inclination, double azimuth)
    {
      m_radius = radius;
      m_inclination = inclination;
      m_azimuth = azimuth;
    }

    [System.Diagnostics.Contracts.Pure] public double Radius { get => m_radius; init => m_radius = value; }
    [System.Diagnostics.Contracts.Pure] public double Inclination { get => m_inclination; init => m_inclination = value; }
    [System.Diagnostics.Contracts.Pure] public double Azimuth { get => m_azimuth; init => m_azimuth = value; }

    [System.Diagnostics.Contracts.Pure] public double Elevation { get => ConvertInclinationToElevation(m_inclination); init => m_inclination = ConvertElevationToInclination(value); }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref=ICartesianCoordinate3{T}">CartesianCoordinate3</see>.</summary>
    public (double x, double y, double z) ToCartesianCoordinate()
    {
      var sinInclination = System.Math.Sin(m_inclination);

      return (
        m_radius * System.Math.Cos(m_azimuth) * sinInclination,
        m_radius * System.Math.Sin(m_azimuth) * sinInclination,
        m_radius * System.Math.Cos(m_inclination)
      );
    }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="Vector3">CartesianCoordinate3</see>.</summary>
    public Vector3 ToCartesianCoordinate3()
    {
      var sinInclination = System.Math.Sin(m_inclination);

      return new Vector3(
        m_radius * System.Math.Cos(m_azimuth) * sinInclination,
        m_radius * System.Math.Sin(m_azimuth) * sinInclination,
        m_radius * System.Math.Cos(m_inclination)
      );
    }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    public CylindricalCoordinate ToCylindricalCoordinate()
      => new CylindricalCoordinate(
        m_radius * System.Math.Sin(m_inclination),
        m_azimuth,
        m_radius * System.Math.Cos(m_inclination)
      );

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="GeographicCoordinate"/>.</summary>
    public GeographicCoordinate ToGeographicCoordinate()
     => new GeographicCoordinate(
       Angle.ConvertRadianToDegree(System.Math.PI - m_inclination - System.Math.PI / 2),
       Angle.ConvertRadianToDegree(m_azimuth - System.Math.PI),
       m_radius
     );

    #region Static methods
    /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertInclinationToElevation(double inclination)
      => System.Math.PI / 2 - inclination;

    /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertElevationToInclination(double elevation)
      => System.Math.PI / 2 - elevation;

    /// <summary>Return the <see cref="ISphericalCoordinate"/> from the specified components.</summary>
    public static SphericalCoordinate From(Length radius, Angle inclination, Azimuth azimuth)
      => new SphericalCoordinate(radius.Value, inclination.Value, azimuth.ToRadians());

    public static SphericalCoordinate FromCartesianCoordinate(double x, double y, double z)
    {
      var x2y2 = x * x + y * y;

      return new(System.Math.Sqrt(x2y2 + z * z), System.Math.Atan2(System.Math.Sqrt(x2y2), z) + System.Math.PI, System.Math.Atan2(y, x) + System.Math.PI);
    }

    #endregion Static methods
  }
}
