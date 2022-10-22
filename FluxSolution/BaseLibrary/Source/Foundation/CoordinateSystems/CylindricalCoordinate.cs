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
    private readonly double m_radAzimuth;
    private readonly double m_height;

    public CylindricalCoordinate(double radius, double radAzimuth, double height)
    {
      m_radius = radius;
      m_radAzimuth = radAzimuth;
      m_height = height;
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public Length Radius { get => new(m_radius); init => m_radius = value.Value; }
    /// <summary>Angular position or angular coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public Azimuth Azimuth { get => Azimuth.FromRadians(m_radAzimuth); init => m_radAzimuth = value.ToRadians(); }
    /// <summary>Also known as altitude. For convention, this correspond to the cartesian z-axis.</summary>
    [System.Diagnostics.Contracts.Pure] public Length Height { get => new(m_height); init => m_height = value.Value; }

    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3R ToCartesianCoordinate3R()
      => new(m_radius * System.Math.Cos(m_radAzimuth), m_radius * System.Math.Sin(m_radAzimuth), m_height);

    [System.Diagnostics.Contracts.Pure]
    public PolarCoordinate ToPolarCoordinate()
      => new(m_radius, m_radAzimuth);

    [System.Diagnostics.Contracts.Pure]
    public SphericalCoordinate ToSphericalCoordinate()
      => new(System.Math.Sqrt(m_radius * m_radius + m_height * m_height), System.Math.Atan2(m_radius, m_height), m_radAzimuth);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(CylindricalCoordinate a, CylindricalCoordinate b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(CylindricalCoordinate a, CylindricalCoordinate b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(CylindricalCoordinate other) => m_radius == other.m_radius && m_radAzimuth == other.m_radAzimuth && m_height == other.m_height;

#if NET7_0_OR_GREATER
    // ICylindricalCoordinate
    public ICylindricalCoordinate Create(Length radius, Azimuth azimuth, Length height)
      => new CylindricalCoordinate(radius.Value, azimuth.ToRadians(), height.Value);
#endif
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is CylindricalCoordinate o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_radius, m_radAzimuth, m_height);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {Azimuth.ToAngle().ToUnitValue(AngleUnit.Degree):N1}\u00B0, Height = {m_height} }}";
    #endregion Object overrides
  }
}
