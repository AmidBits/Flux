namespace Flux
{
  /// <summary>Cylindrical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
  public struct CylindricalCoordinate
    : System.IEquatable<CylindricalCoordinate>
  {
    private readonly double m_radius;
    private readonly Angle m_azimuth;
    private readonly double m_height;

    public CylindricalCoordinate(double radius, double azimuthRad, double height)
    {
      m_radius = radius;
      m_azimuth = new Angle(azimuthRad);
      m_height = height;
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    [System.Diagnostics.Contracts.Pure]
    public double Radius
      => m_radius;
    /// <summary>Angular position or angular coordinate.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Angle Azimuth
      => m_azimuth;
    /// <summary>Also known as altitude.</summary>
    [System.Diagnostics.Contracts.Pure]
    public double Height
      => m_height;

    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3 ToCartesianCoordinate3()
    {
      var radAzimuth = m_azimuth.Value;
      return new CartesianCoordinate3(m_radius * System.Math.Cos(radAzimuth), m_radius * System.Math.Sin(radAzimuth), m_height);
    }
    [System.Diagnostics.Contracts.Pure]
    public SphericalCoordinate ToSphericalCoordinate()
      => new(System.Math.Sqrt(m_radius * m_radius + m_height * m_height), System.Math.Atan2(m_radius, m_height), m_azimuth.Value);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(CylindricalCoordinate a, CylindricalCoordinate b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(CylindricalCoordinate a, CylindricalCoordinate b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(CylindricalCoordinate other) => m_radius == other.m_radius && m_azimuth == other.m_azimuth && m_height == other.m_height;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is CylindricalCoordinate o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_radius, m_azimuth, m_height);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {m_azimuth.ToUnitValue(AngleUnit.Degree):N1}{Angle.DegreeSymbol}, Height = {m_height} }}";
    #endregion Object overrides
  }
}
