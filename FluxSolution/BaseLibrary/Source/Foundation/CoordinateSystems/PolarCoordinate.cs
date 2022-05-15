namespace Flux
{
  /// <summary>Polar coordinate. Please note that polar coordinates are two dimensional.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct PolarCoordinate
    : System.IEquatable<PolarCoordinate>
  {
    private readonly double m_radius;
    private readonly double m_azimuth;

    public PolarCoordinate(double radius, double radAzimuth)
    {
      m_radius = radius;
      m_azimuth = radAzimuth;
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public double Radius => m_radius;
    /// <summary>Polar angle or angular coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public Angle Azimuth => new(m_azimuth);

    /// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="CartesianCoordinateR2"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinateR2 ToCartesianCoordinateR2()
      => new(m_radius * System.Math.Cos(m_azimuth), m_radius * System.Math.Sin(m_azimuth));
    /// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="System.Numerics.Complex"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.Numerics.Complex ToComplex()
      => System.Numerics.Complex.FromPolarCoordinates(m_radius, m_azimuth);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(PolarCoordinate a, PolarCoordinate b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(PolarCoordinate a, PolarCoordinate b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(PolarCoordinate other) => m_azimuth == other.m_azimuth && m_radius == other.m_radius;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is PolarCoordinate o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_azimuth, m_radius);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {Azimuth.ToUnitValue(AngleUnit.Degree):N1}{Angle.DegreeSymbol} }}";
    #endregion Object overrides
  }
}
