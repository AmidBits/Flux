namespace Flux
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  public struct SphericalCoordinate
    : System.IEquatable<SphericalCoordinate>
  {
    private readonly double m_radius;
    private readonly Quantity.Angle m_inclination;
    private readonly Quantity.Angle m_azimuth;

    public SphericalCoordinate(double radius, double inclinationRad, double azimuthRad)
    {
      m_radius = radius;
      m_inclination = new Quantity.Angle(inclinationRad);
      m_azimuth = new Quantity.Angle(azimuthRad);
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    public double Radius { get => m_radius; }
    /// <summary>Polar angle or angular coordinate.</summary>
    public Quantity.Angle Inclination { get => m_inclination; }
    /// <summary>Azimuthal angle.</summary>
    public Quantity.Angle Azimuth { get => m_azimuth; }

    public CartesianCoordinate3 ToCartesianCoordinate3()
    {
      var radInclination = m_inclination.Radian;
      var radAzimuth = m_azimuth.Radian;
      var sinInclination = System.Math.Sin(radInclination);
      return new CartesianCoordinate3(m_radius * System.Math.Cos(radAzimuth) * sinInclination, m_radius * System.Math.Sin(radAzimuth) * sinInclination, m_radius * System.Math.Cos(radInclination));
    }
    public CylindricalCoordinate ToCylindricalCoordinate()
    {
      var radInclination = m_inclination.Radian;
      return new CylindricalCoordinate(m_radius * System.Math.Sin(radInclination), m_azimuth.Radian, m_radius * System.Math.Cos(radInclination));
    }
    public GeographicCoordinate ToGeographicCoordinate()
      => new GeographicCoordinate(Quantity.Angle.ConvertRadianToDegree(System.Math.PI - m_inclination.Radian - Maths.PiOver2), Quantity.Angle.ConvertRadianToDegree(m_azimuth.Radian - System.Math.PI), m_radius);

    #region Static methods
    /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>
    public static double ConvertInclinationToElevation(double inclinationRad)
      => Maths.PiOver2 - inclinationRad;
    /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>
    public static double ConvertElevationToInclination(double elevationRad)
      => Maths.PiOver2 - elevationRad;
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(SphericalCoordinate a, SphericalCoordinate b)
      => a.Equals(b);
    public static bool operator !=(SphericalCoordinate a, SphericalCoordinate b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(SphericalCoordinate other)
      => m_radius == other.m_radius && m_inclination == other.m_inclination && m_azimuth == other.m_azimuth;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is SphericalCoordinate o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_radius, m_inclination, m_azimuth);
    public override string ToString()
      => $"<{GetType().Name}: {m_radius} radius, {m_inclination.Degree:N1}{Quantity.Angle.DegreeSymbol} inclination ({Quantity.Angle.ConvertRadianToDegree(ConvertInclinationToElevation(m_inclination.Value)):N1}{Quantity.Angle.DegreeSymbol} elevation), {m_azimuth.Degree:N1}{Quantity.Angle.DegreeSymbol} azimuth>";
    #endregion Object overrides
  }
}
