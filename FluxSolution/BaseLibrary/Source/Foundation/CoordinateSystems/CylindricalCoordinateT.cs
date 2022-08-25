#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct CylindricalCoordinate<T>
    : System.IEquatable<CylindricalCoordinate<T>>
    where T : System.Numerics.IFloatingPoint<T>
  {
    private readonly T m_radius;
    private readonly T m_radAzimuth;
    private readonly T m_height;

    public CylindricalCoordinate(T radius, T radAzimuth, T height)
    {
      m_radius = radius;
      m_radAzimuth = radAzimuth;
      m_height = height;
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public T Radius { get => m_radius; init => m_radius = value; }
    /// <summary>Angular position or angular coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public T Azimuth { get => m_radAzimuth; init => m_radAzimuth = value; }
    /// <summary>Also known as altitude. For convention, this correspond to the cartesian z-axis.</summary>
    [System.Diagnostics.Contracts.Pure] public T Height { get => m_height; init => m_height = value; }

    //[System.Diagnostics.Contracts.Pure]
    //public CartesianCoordinate3 ToCartesianCoordinate3()
    //  => new(m_radius * T.Cos(m_radAzimuth), m_radius * T.Sin(m_radAzimuth), m_height);
    //[System.Diagnostics.Contracts.Pure]
    //public PolarCoordinate ToPolarCoordinate()
    //  => new(m_radius, m_radAzimuth);
    //[System.Diagnostics.Contracts.Pure]
    //public SphericalCoordinate ToSphericalCoordinate()
    //  => new(T.Sqrt(m_radius * m_radius + m_height * m_height), T.Atan2(m_radius, m_height), m_radAzimuth);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(CylindricalCoordinate<T> a, CylindricalCoordinate<T> b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(CylindricalCoordinate<T> a, CylindricalCoordinate<T> b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(CylindricalCoordinate<T> other) => m_radius == other.m_radius && m_radAzimuth == other.m_radAzimuth && m_height == other.m_height;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is CylindricalCoordinate<T> o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_radius, m_radAzimuth, m_height);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {m_radAzimuth}, Height = {m_height} }}";
    #endregion Object overrides
  }
}
#endif
