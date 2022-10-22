using Flux.Formatting;

namespace Flux
{
  /// <summary>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct CylindricalCoordinate
    : System.IEquatable<CylindricalCoordinate>, ICylindricalCoordinate
  {
    private readonly double m_radius;
    private readonly double m_azimuth;
    private readonly double m_height;

    public CylindricalCoordinate(double radius, double azimuth, double height)
    {
      m_radius = radius;
      m_azimuth = azimuth;
      m_height = height;
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public double Radius { get => m_radius; init => m_radius = value; }
    /// <summary>Angular position or angular coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public double Azimuth { get => m_azimuth; init => m_azimuth = value; }
    /// <summary>Also known as altitude. For convention, this correspond to the cartesian z-axis.</summary>
    [System.Diagnostics.Contracts.Pure] public double Height { get => m_height; init => m_height = value; }

    //[System.Diagnostics.Contracts.Pure]
    //public ICartesianCoordinate3 ToCartesianCoordinate3R()
    //  => new CartesianCoordinate3R(m_radius * System.Math.Cos(m_azimuth), m_radius * System.Math.Sin(m_azimuth), m_height);

    //[System.Diagnostics.Contracts.Pure]
    //public IPolarCoordinate ToPolarCoordinate()
    //  => new PolarCoordinate(m_radius, m_azimuth);

    //[System.Diagnostics.Contracts.Pure]
    //public ISphericalCoordinate ToSphericalCoordinate()
    //  => new SphericalCoordinate(System.Math.Sqrt(m_radius * m_radius + m_height * m_height), System.Math.Atan2(m_radius, m_height), m_azimuth);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(CylindricalCoordinate a, CylindricalCoordinate b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(CylindricalCoordinate a, CylindricalCoordinate b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(CylindricalCoordinate other) => m_radius == other.m_radius && m_azimuth == other.m_azimuth && m_height == other.m_height;

    // ICylindricalCoordinate
    public ICylindricalCoordinate Create(double radius, double azimuth, double height)
      => new CylindricalCoordinate(radius, azimuth, height);
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is CylindricalCoordinate o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_radius, m_azimuth, m_height);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {new Angle(m_azimuth).ToUnitString(AngleUnit.Degree, "N1")}, Height = {m_height} }}";
    #endregion Object overrides
  }
}
