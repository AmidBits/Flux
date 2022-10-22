namespace Flux
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct SphericalCoordinate
    : System.IEquatable<SphericalCoordinate>, ISphericalCoordinate
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

    /// <summary>Radial distance (to origin) or radial coordinate, in meters.</summary>
    [System.Diagnostics.Contracts.Pure] public double Radius { get => m_radius; init => m_radius = value; }
    /// <summary>Polar angle or angular coordinate, in radians.</summary>
    [System.Diagnostics.Contracts.Pure] public double Inclination { get => m_inclination; init => m_inclination = value; }
    /// <summary>Azimuthal angle, in radians.</summary>
    [System.Diagnostics.Contracts.Pure] public double Azimuth { get => m_azimuth; init => m_azimuth = value; }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CartesianCoordinate3R"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public ICartesianCoordinate3 ToCartesianCoordinate3R()
    {
      var sinInclination = System.Math.Sin(m_inclination);

      return new CartesianCoordinate3R(
        m_radius * System.Math.Cos(m_azimuth) * sinInclination,
        m_radius * System.Math.Sin(m_azimuth) * sinInclination,
        m_radius * System.Math.Cos(m_inclination)
      );
    }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public ICylindricalCoordinate ToCylindricalCoordinate()
      => new CylindricalCoordinate(
        m_radius * System.Math.Sin(m_inclination),
        m_azimuth,
        m_radius * System.Math.Cos(m_inclination)
      );

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="GeographicCoordinate"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public IGeographicCoordinate ToGeographicCoordinate()
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
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(SphericalCoordinate a, SphericalCoordinate b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(SphericalCoordinate a, SphericalCoordinate b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(SphericalCoordinate other) => m_radius == other.m_radius && m_inclination == other.m_inclination && m_azimuth == other.m_azimuth;

    // ISphericalCoordinate
    public ISphericalCoordinate Create(double radius, double inclination, double azimuth)
     => new SphericalCoordinate(radius, inclination, azimuth);
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is SphericalCoordinate o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_radius, m_inclination, m_azimuth);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Inclination = {new Angle(m_inclination).ToUnitString(AngleUnit.Degree, "N1")} (Elevation = {new Angle(ConvertInclinationToElevation(m_inclination)).ToUnitString(AngleUnit.Degree, "N1")}, Azimuth = {new Angle(m_azimuth).ToUnitString(AngleUnit.Degree, "N1")} }}";
    #endregion Object overrides
  }
}
