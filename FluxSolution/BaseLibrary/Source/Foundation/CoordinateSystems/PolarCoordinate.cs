namespace Flux
{
  /// <summary>Polar coordinate. Please note that polar coordinates are two dimensional.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct PolarCoordinate
    : System.IEquatable<PolarCoordinate>, IPolarCoordinate
  {
    private readonly double m_radius;
    private readonly double m_azimuth;

    public PolarCoordinate(double radius, double azimuth)
    {
      m_radius = radius;
      m_azimuth = azimuth;
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public double Radius { get => m_radius; init => m_radius = value; }
    /// <summary>Polar angle or angular coordinate.</summary>
    [System.Diagnostics.Contracts.Pure] public double Azimuth { get => m_azimuth; init => m_azimuth = value; }

    //public ICartesianCoordinate2 ToCartesianCoordinate2() => ((IPolarCoordinate)this).ToCartesianCoordinate2();
    //public System.Numerics.Complex ToComplex() => ((IPolarCoordinate)this).ToComplex();

    ///// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="CartesianCoordinate2R"/>.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public ICartesianCoordinate2 ToCartesianCoordinate2R()
    //  => new CartesianCoordinate2R(m_radius * System.Math.Cos(m_azimuth), m_radius * System.Math.Sin(m_azimuth));

    ///// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="System.Numerics.Complex"/>.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public System.Numerics.Complex ToComplex()
    //  => System.Numerics.Complex.FromPolarCoordinates(
    //    m_radius,
    //    m_azimuth
    //  );

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(PolarCoordinate a, PolarCoordinate b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(PolarCoordinate a, PolarCoordinate b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(PolarCoordinate other) => m_azimuth == other.m_azimuth && m_radius == other.m_radius;

    // IPolarCoordinate
    public IPolarCoordinate Create(double radius, double azimuth)
     => new PolarCoordinate(radius, azimuth);
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is PolarCoordinate o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_azimuth, m_radius);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {new Angle(m_azimuth).ToUnitString(AngleUnit.Degree, "N1")} }}";
    #endregion Object overrides
  }
}
