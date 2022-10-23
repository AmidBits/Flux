namespace Flux
{
  /// <summary>Polar coordinate. Please note that polar coordinates are two dimensional.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public record struct PolarCoordinate
    : IPolarCoordinate
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

    /// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="CartesianCoordinate2R"/>.</summary>
    [System.Diagnostics.Contracts.Pure] public ICartesianCoordinate2 ToCartesianCoordinate2()
     => new CartesianCoordinate2R(
       m_radius * System.Math.Cos(m_azimuth), 
       m_radius * System.Math.Sin(m_azimuth)
     );

    /// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="System.Numerics.Complex"/>.</summary>
    [System.Diagnostics.Contracts.Pure] public System.Numerics.Complex ToComplex()
     => System.Numerics.Complex.FromPolarCoordinates(
       m_radius,
       m_azimuth
     );

    #region Implemented interfaces
    // IPolarCoordinate
    public IPolarCoordinate Create(double radius, double azimuth)
     => new PolarCoordinate(radius, azimuth);
    #endregion Implemented interfaces
  }
}
