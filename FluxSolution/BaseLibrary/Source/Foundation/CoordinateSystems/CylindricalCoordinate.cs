using Flux.Formatting;

namespace Flux
{
  /// <summary>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public record struct CylindricalCoordinate
    : ICylindricalCoordinate
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

    [System.Diagnostics.Contracts.Pure]
    public ICartesianCoordinate3 ToCartesianCoordinate3()
      => ((ICylindricalCoordinate)this).ToCartesianCoordinate3();

    [System.Diagnostics.Contracts.Pure]
    public IPolarCoordinate ToPolarCoordinate()
      => ((ICylindricalCoordinate)this).ToPolarCoordinate();

    [System.Diagnostics.Contracts.Pure]
    public ISphericalCoordinate ToSphericalCoordinate()
      => ((ICylindricalCoordinate)this).ToSphericalCoordinate();

    #region Implemented interfaces
    // ICylindricalCoordinate
    public ICylindricalCoordinate Create(double radius, double azimuth, double height)
      => new CylindricalCoordinate(radius, azimuth, height);
    #endregion Implemented interfaces
  }
}
