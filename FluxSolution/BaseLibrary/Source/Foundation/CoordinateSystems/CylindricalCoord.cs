namespace Flux
{
  /// <summary>Cylindrical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
  public struct CylindricalCoord
    : System.IEquatable<CylindricalCoord>
  {
    private readonly double m_radius;
    private readonly Quantity.Angle m_azimuth;
    private readonly double m_height;

    public CylindricalCoord(double radius, Quantity.Angle azimuth, double height)
    {
      m_radius = radius;
      m_azimuth = azimuth;
      m_height = height;
    }

    public double Radius
      => m_radius;
    public Quantity.Angle Azimuth
      => m_azimuth;
    public double Height
      => m_height;

    public CartesianCoord ToCartesianCoord()
    {
      var (x, y, z) = ConvertToCartesianCoordinate(m_radius, m_azimuth.Radian, m_height);
      return new CartesianCoord(x, y, z);
    }
    public SphericalCoord ToSphericalCoord()
    {
      var (radius, inclinationRad, azimuthRad) = ConvertToSphericalCoordinate(m_radius, m_azimuth.Radian, m_height);
      return new SphericalCoord(radius, new Quantity.Angle(inclinationRad), new Quantity.Angle(azimuthRad));
    }

    #region Static methods
    public static (double x, double y, double z) ConvertToCartesianCoordinate(double radius, double azimuthRad, double height)
      => (radius * System.Math.Cos(azimuthRad), radius * System.Math.Sin(azimuthRad), height);
    public static (double radius, double inclinationRad, double azimuthRad) ConvertToSphericalCoordinate(double radius, double azimuthRad, double height)
      => (System.Math.Sqrt(radius * radius + height * height), System.Math.Atan2(radius, height), azimuthRad);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(CylindricalCoord a, CylindricalCoord b)
      => a.Equals(b);
    public static bool operator !=(CylindricalCoord a, CylindricalCoord b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(CylindricalCoord other)
      => m_radius == other.m_radius && m_azimuth == other.m_azimuth && m_height == other.m_height;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is CylindricalCoord o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_radius, m_azimuth, m_height);
    public override string ToString()
      => $"<{GetType().Name}: {m_radius}, {m_azimuth.Degree}{Quantity.Angle.DegreeSymbol}, {m_height}>";
    #endregion Object overrides
  }
}
