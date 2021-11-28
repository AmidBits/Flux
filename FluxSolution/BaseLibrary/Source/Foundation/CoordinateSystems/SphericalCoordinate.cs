namespace Flux
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
#if NET5_0
  public struct SphericalCoordinate
    : System.IEquatable<SphericalCoordinate>
#elif NET6_0_OR_GREATER
  public record struct SphericalCoordinate
#endif
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
      var radInclination = m_inclination.Value;
      var radAzimuth = m_azimuth.Value;
      var sinInclination = System.Math.Sin(radInclination);
      return new CartesianCoordinate3(m_radius * System.Math.Cos(radAzimuth) * sinInclination, m_radius * System.Math.Sin(radAzimuth) * sinInclination, m_radius * System.Math.Cos(radInclination));
    }
    public CylindricalCoordinate ToCylindricalCoordinate()
    {
      var radInclination = m_inclination.Value;
      return new CylindricalCoordinate(m_radius * System.Math.Sin(radInclination), m_azimuth.Value, m_radius * System.Math.Cos(radInclination));
    }
    public GeographicCoordinate ToGeographicCoordinate()
      => new(Quantity.Angle.ConvertRadianToDegree(System.Math.PI - m_inclination.Value - Maths.PiOver2), Quantity.Angle.ConvertRadianToDegree(m_azimuth.Value - System.Math.PI), m_radius);

    #region Static methods
    /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>
    public static double ConvertInclinationToElevation(double inclinationRad)
      => Maths.PiOver2 - inclinationRad;
    /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>
    public static double ConvertElevationToInclination(double elevationRad)
      => Maths.PiOver2 - elevationRad;
    #endregion Static methods

    #region Overloaded operators
#if NET5_0
    public static bool operator ==(SphericalCoordinate a, SphericalCoordinate b)
      => a.Equals(b);
    public static bool operator !=(SphericalCoordinate a, SphericalCoordinate b)
      => !a.Equals(b);
#endif
    #endregion Overloaded operators

    #region Implemented interfaces
#if NET5_0
    // IEquatable
    public bool Equals(SphericalCoordinate other)
      => m_radius == other.m_radius && m_inclination == other.m_inclination && m_azimuth == other.m_azimuth;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is SphericalCoordinate o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_radius, m_inclination, m_azimuth);
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Radius = {m_radius}, Inclination = {m_inclination.ToUnitValue(Quantity.AngleUnit.Degree):N1}{Quantity.Angle.DegreeSymbol} (Elevation = {Quantity.Angle.ConvertRadianToDegree(ConvertInclinationToElevation(m_inclination.Value)):N1}{Quantity.Angle.DegreeSymbol}), Azimuth = {m_azimuth.ToUnitValue(Quantity.AngleUnit.Degree):N1}{Quantity.Angle.DegreeSymbol} }}";
    #endregion Object overrides
  }
}
