namespace Flux
{
  /// <summary>Polar coordinate. Please note that polar coordinates are two dimensional.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct PolarCoordinate
    : System.IEquatable<PolarCoordinate>, IPolarCoordinate
  {
    private readonly double m_radius;
    private readonly double m_radAzimuth;

    public PolarCoordinate(double radiusInMeter, double azimuthInRadian)
    {
      m_radius = radiusInMeter;
      m_radAzimuth = azimuthInRadian;
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public Length Radius { get => new(m_radius); init => m_radius = value.Value; }
    /// <summary>Polar angle or angular coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public Azimuth Azimuth { get => Azimuth.FromRadians(m_radAzimuth); init => m_radAzimuth = value.ToRadians(); }

    /// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="CartesianCoordinate2R"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate2R ToCartesianCoordinate2R()
      => new(m_radius * System.Math.Cos(m_radAzimuth), m_radius * System.Math.Sin(m_radAzimuth));

    /// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="System.Numerics.Complex"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.Numerics.Complex ToComplex()
      => System.Numerics.Complex.FromPolarCoordinates(m_radius, m_radAzimuth);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(PolarCoordinate a, PolarCoordinate b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(PolarCoordinate a, PolarCoordinate b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(PolarCoordinate other) => m_radAzimuth == other.m_radAzimuth && m_radius == other.m_radius;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is PolarCoordinate o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_radAzimuth, m_radius);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {Azimuth.ToAngle().ToUnitValue(AngleUnit.Degree):N1}\u00B0 }}";
    #endregion Object overrides
  }
}
