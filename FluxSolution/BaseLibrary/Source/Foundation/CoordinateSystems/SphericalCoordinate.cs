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
    public SphericalCoordinate(System.ValueTuple<double, double, double> radius_inclinationRad_azimuthRad)
      : this(radius_inclinationRad_azimuthRad.Item1, radius_inclinationRad_azimuthRad.Item2, radius_inclinationRad_azimuthRad.Item3)
    { }

    public double Radius
      => m_radius;
    public Quantity.Angle Inclination
      => m_inclination;
    public Quantity.Angle Azimuth
      => m_azimuth;

    public CartesianCoordinate3 ToCartesianCoordinate3()
      => new CartesianCoordinate3(ConvertToCartesianCoordinate3(m_radius, m_inclination.Radian, m_azimuth.Radian));
    public CylindricalCoordinate ToCylindricalCoordinate()
      => new CylindricalCoordinate(ConvertToCylindricalCoordinate(m_radius, m_inclination.Radian, m_azimuth.Radian));
    public GeographicCoordinate ToGeographicCoordinate()
      => new GeographicCoordinate(ConvertToGeographicCoordinate(m_radius, m_inclination.Radian, m_azimuth.Radian));

    #region Static methods
    public static (double x, double y, double z) ConvertToCartesianCoordinate3(double radius, double inclinationRad, double azimuthRad)
    {
      var sinInclination = System.Math.Sin(inclinationRad);
      return (radius * System.Math.Cos(azimuthRad) * sinInclination, radius * System.Math.Sin(azimuthRad) * sinInclination, radius * System.Math.Cos(inclinationRad));
    }
    public static (double radius, double azimuthRad, double height) ConvertToCylindricalCoordinate(double radius, double inclinationRad, double azimuthRad)
      => (radius * System.Math.Sin(inclinationRad), azimuthRad, radius * System.Math.Cos(inclinationRad));
    public static (double latitudeRad, double longitudeRad, double altitude) ConvertToGeographicCoordinate(double radius, double inclinationRad, double azimuthRad)
      => ((System.Math.PI - inclinationRad) - Maths.PiOver2, azimuthRad - System.Math.PI, radius);
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
      => $"<{GetType().Name}: {m_radius}, {m_inclination.Degree}{Quantity.Angle.DegreeSymbol}, {m_azimuth.Degree}{Quantity.Angle.DegreeSymbol}>";
    #endregion Object overrides
  }
}
