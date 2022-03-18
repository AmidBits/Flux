namespace Flux
{
  /// <summary>Polar coordinate. Please note that polar coordinates are two dimensional.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  public struct PolarCoordinate
    : System.IEquatable<PolarCoordinate>
  {
    private readonly double m_radius;
    private readonly Angle m_azimuth;

    public PolarCoordinate(double radius, double azimuthRad)
    {
      m_radius = radius;
      m_azimuth = new Angle(azimuthRad);
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    [System.Diagnostics.Contracts.Pure]
    public double Radius
      => m_radius;
    /// <summary>Polar angle or angular coordinate.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Angle Azimuth
      => m_azimuth;

    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate2 ToCartesianCoordinate2()
    {
      var radAzimuth = m_azimuth.Value;
      return new CartesianCoordinate2(m_radius * System.Math.Cos(radAzimuth), m_radius * System.Math.Sin(radAzimuth));
    }
    [System.Diagnostics.Contracts.Pure]
    public System.Numerics.Complex ToComplex()
      => System.Numerics.Complex.FromPolarCoordinates(m_radius, m_azimuth.Value);

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
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {m_azimuth.ToUnitValue(AngleUnit.Degree):N1}{Angle.DegreeSymbol} }}";
    #endregion Object overrides
  }
}
