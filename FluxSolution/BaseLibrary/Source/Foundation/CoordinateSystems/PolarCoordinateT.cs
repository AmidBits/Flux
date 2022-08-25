#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Polar coordinate. Please note that polar coordinates are two dimensional.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct PolarCoordinate<T>
    : System.IEquatable<PolarCoordinate<T>>
    where T : System.Numerics.IFloatingPoint<T>
  {
    private readonly T m_radius;
    private readonly T m_radAzimuth;

    public PolarCoordinate(T radius, T radAzimuth)
    {
      m_radius = radius;
      m_radAzimuth = radAzimuth;
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public T Radius { get => m_radius; init => m_radius = value; }
    /// <summary>Polar angle or angular coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public T Azimuth { get => m_radAzimuth; init => m_radAzimuth = value; }

    /// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="CartesianCoordinate2"/>.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public CartesianCoordinate2 ToCartesianCoordinate2()
    //  => new(m_radius * System.Math.Cos(m_radAzimuth), m_radius * System.Math.Sin(m_radAzimuth));
    /// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="System.Numerics.Complex"/>.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public System.Numerics.Complex ToComplex()
    //  => System.Numerics.Complex.FromPolarCoordinates(m_radius, m_radAzimuth);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(PolarCoordinate<T> a, PolarCoordinate<T> b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(PolarCoordinate<T> a, PolarCoordinate<T> b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(PolarCoordinate<T> other) => m_radAzimuth == other.m_radAzimuth && m_radius == other.m_radius;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is PolarCoordinate<T> o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_radAzimuth, m_radius);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {m_radAzimuth} }}";
    #endregion Object overrides
  }
}
#endif
