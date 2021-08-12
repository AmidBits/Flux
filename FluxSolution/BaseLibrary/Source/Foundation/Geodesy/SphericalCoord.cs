namespace Flux
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  public struct SphericalCoord
    : System.IEquatable<SphericalCoord>
  {
    private readonly double m_radius;
    private readonly Quantity.Angle m_inclination;
    private readonly Quantity.Angle m_azimuth;

    public SphericalCoord(double radius, Quantity.Angle inclination, Quantity.Angle azimuth)
    {
      m_radius = radius;
      m_inclination = inclination;
      m_azimuth = azimuth;
    }

    public double Radius
      => m_radius;
    public Quantity.Angle Inclination
      => m_inclination;
    public Quantity.Angle Azimuth
      => m_azimuth;

    public CartesianCoord ToCartesianCoord()
    {
      var (x, y, z) = ConvertToCartesianCoordinate(m_radius, m_inclination.Radian, m_azimuth.Radian);

      return new CartesianCoord(x, y, z);
    }
    public CylindricalCoord ToCylindricalCoord()
    {
      var (radius, azimuthRad, height) = ConvertToCylindricalCoordinate(m_radius, m_inclination.Radian, m_azimuth.Radian);

      return new CylindricalCoord(radius, new Quantity.Angle(azimuthRad), height);
    }

    #region Static methods
    public static (double x, double y, double z) ConvertToCartesianCoordinate(double radius, double inclination, double azimuth)
      => (radius * System.Math.Cos(azimuth) * System.Math.Sin(inclination), radius * System.Math.Sin(azimuth) * System.Math.Cos(inclination), radius * System.Math.Cos(inclination));
    public static (double radius, double azimuthRad, double height) ConvertToCylindricalCoordinate(double radius, double inclinationRad, double azimuthRad)
      => (radius * System.Math.Cos(inclinationRad), azimuthRad, radius * System.Math.Sin(inclinationRad));
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(SphericalCoord a, SphericalCoord b)
      => a.Equals(b);
    public static bool operator !=(SphericalCoord a, SphericalCoord b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(SphericalCoord other)
      => m_radius == other.m_radius && m_inclination == other.m_inclination && m_azimuth == other.m_azimuth;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is SphericalCoord o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_radius, m_inclination, m_azimuth);
    public override string ToString()
      => $"<{GetType().Name}: {m_radius}, {m_inclination.Degree}{Quantity.Angle.DegreeSymbol}, {m_azimuth.Degree}{Quantity.Angle.DegreeSymbol}>";
    #endregion Object overrides
  }
}
