namespace Flux
{
  /// <summary>Cylindrical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
#if NET5_0
  public struct CylindricalCoordinate
    : System.IEquatable<CylindricalCoordinate>
#elif NET6_0
  public record struct CylindricalCoordinate
#endif
  {
    private readonly double m_radius;
    private readonly Quantity.Angle m_azimuth;
    private readonly double m_height;

    public CylindricalCoordinate(double radius, double azimuthRad, double height)
    {
      m_radius = radius;
      m_azimuth = new Quantity.Angle(azimuthRad);
      m_height = height;
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    public double Radius { get => m_radius; }
    /// <summary>Angular position or angular coordinate.</summary>
    public Quantity.Angle Azimuth { get => m_azimuth; }
    /// <summary>Also known as altitude.</summary>
    public double Height { get => m_height; }

    public CartesianCoordinate3 ToCartesianCoordinate3()
    {
      var radAzimuth = m_azimuth.Value;
      return new CartesianCoordinate3(m_radius * System.Math.Cos(radAzimuth), m_radius * System.Math.Sin(radAzimuth), m_height);
    }
    public SphericalCoordinate ToSphericalCoordinate()
      => new(System.Math.Sqrt(m_radius * m_radius + m_height * m_height), System.Math.Atan2(m_radius, m_height), m_azimuth.Value);

    #region Overloaded operators
#if NET5_0
    public static bool operator ==(CylindricalCoordinate a, CylindricalCoordinate b)
      => a.Equals(b);
    public static bool operator !=(CylindricalCoordinate a, CylindricalCoordinate b)
      => !a.Equals(b);
#endif
    #endregion Overloaded operators

    #region Implemented interfaces
#if NET5_0
    // IEquatable
    public bool Equals(CylindricalCoordinate other)
      => m_radius == other.m_radius && m_azimuth == other.m_azimuth && m_height == other.m_height;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is CylindricalCoordinate o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_radius, m_azimuth, m_height);
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {m_azimuth.ToUnitValue(Quantity.AngleUnit.Degree):N1}{Quantity.Angle.DegreeSymbol}, Height = {m_height} }}";
    #endregion Object overrides
  }
}
