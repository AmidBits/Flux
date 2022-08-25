#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct SphericalCoordinate<T>
    : System.IEquatable<SphericalCoordinate<T>>
    where T : System.Numerics.IFloatingPoint<T>
  {
    private readonly T m_radius;
    private readonly T m_radInclination;
    private readonly T m_radAzimuth;

    public SphericalCoordinate(T radius, T radInclination, T radAzimuth)
    {
      m_radius = radius;
      m_radInclination = radInclination;
      m_radAzimuth = radAzimuth;
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public T Radius { get => m_radius; init => m_radius = value; }
    /// <summary>Polar angle or angular coordinate, in radians.</summary>
    [System.Diagnostics.Contracts.Pure] public T Inclination { get => m_radInclination; init => m_radInclination = value; }
    /// <summary>Azimuthal angle, in radians.</summary>
    [System.Diagnostics.Contracts.Pure] public T Azimuth { get => m_radAzimuth; init => m_radAzimuth = value; }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CartesianCoordinate3"/>.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public CartesianCoordinate3 ToCartesianCoordinate3()
    //{
    //  var sinInclination = System.Math.Sin(m_radInclination);
    //  return new(m_radius * System.Math.Cos(m_radAzimuth) * sinInclination, m_radius * System.Math.Sin(m_radAzimuth) * sinInclination, m_radius * System.Math.Cos(m_radInclination));
    //}
    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public CylindricalCoordinate ToCylindricalCoordinate()
    //  => new(m_radius * System.Math.Sin(m_radInclination), m_radAzimuth, m_radius * System.Math.Cos(m_radInclination));
    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="GeographicCoordinate"/>.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public GeographicCoordinate ToGeographicCoordinate()
    //  => new(Angle.ConvertRadianToDegree(System.Math.PI - m_radInclination - Maths.PiOver2), Angle.ConvertRadianToDegree(m_radAzimuth - System.Math.PI), m_radius);

    #region Static methods
    /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static double ConvertInclinationToElevation(double inclinationRad)
    //  => Maths.PiOver2 - inclinationRad;
    /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static double ConvertElevationToInclination(double elevationRad)
    //  => Maths.PiOver2 - elevationRad;
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(SphericalCoordinate<T> a, SphericalCoordinate<T> b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(SphericalCoordinate<T> a, SphericalCoordinate<T> b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(SphericalCoordinate<T> other) => m_radius == other.m_radius && m_radInclination == other.m_radInclination && m_radAzimuth == other.m_radAzimuth;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is SphericalCoordinate<T> o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_radius, m_radInclination, m_radAzimuth);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Inclination = {m_radInclination} (Elevation = ), Azimuth = {m_radAzimuth} }}";
    #endregion Object overrides
  }
}
#endif
