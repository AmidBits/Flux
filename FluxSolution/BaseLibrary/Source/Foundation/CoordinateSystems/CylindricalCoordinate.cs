namespace Flux.CoordinateSystems
{
  /// <summary>Cylindrical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
  public struct CylindricalCoordinate
    : System.IEquatable<CylindricalCoordinate>
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
    public CylindricalCoordinate(System.ValueTuple<double, double, double> radius_azimuthRad_height)
      : this(radius_azimuthRad_height.Item1, radius_azimuthRad_height.Item2, radius_azimuthRad_height.Item3)
    { }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    public double Radius
      => m_radius;
    /// <summary>Angular position or angular coordinate.</summary>
    public Quantity.Angle Azimuth
      => m_azimuth;
    /// <summary>Also known as altitude.</summary>
    public double Height
      => m_height;

    public CartesianCoordinate3 ToCartesianCoordinate3()
      => new CartesianCoordinate3(ConvertToCartesianCoordinate(m_radius, m_azimuth.Radian, m_height));
    public SphericalCoordinate ToSphericalCoordinate()
      => new SphericalCoordinate(ConvertToSphericalCoordinate(m_radius, m_azimuth.Radian, m_height));

    #region Static methods
    public static (double x, double y, double z) ConvertToCartesianCoordinate(double radius, double azimuthRad, double height)
      => (radius * System.Math.Cos(azimuthRad), radius * System.Math.Sin(azimuthRad), height);
    public static (double radius, double inclinationRad, double azimuthRad) ConvertToSphericalCoordinate(double radius, double azimuthRad, double height)
      => (System.Math.Sqrt(radius * radius + height * height), System.Math.Atan2(radius, height), azimuthRad);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(CylindricalCoordinate a, CylindricalCoordinate b)
      => a.Equals(b);
    public static bool operator !=(CylindricalCoordinate a, CylindricalCoordinate b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(CylindricalCoordinate other)
      => m_radius == other.m_radius && m_azimuth == other.m_azimuth && m_height == other.m_height;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is CylindricalCoordinate o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_radius, m_azimuth, m_height);
    public override string ToString()
      => $"<{GetType().Name}: {m_radius}, {m_azimuth.Degree}{Quantity.Angle.DegreeSymbol}, {m_height}>";
    #endregion Object overrides
  }
}
