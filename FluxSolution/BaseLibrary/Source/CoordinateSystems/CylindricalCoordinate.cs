namespace Flux
{
  /// <summary>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct CylindricalCoordinate
    : ICylindricalCoordinate<double>
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

    [System.Diagnostics.Contracts.Pure] public double Radius { get => m_radius; init => m_radius = value; }
    [System.Diagnostics.Contracts.Pure] public double Azimuth { get => m_azimuth; init => m_azimuth = value; }
    [System.Diagnostics.Contracts.Pure] public double Height { get => m_height; init => m_height = value; }

    /// <summary>Converts the <see cref="CylindricalCoordinate"/> to a <see cref="Vector3"/>.</summary>
    public Vector3 ToCartesianCoordinate3()
     => new(
       m_radius * System.Math.Cos(m_azimuth),
       m_radius * System.Math.Sin(m_azimuth),
       m_height
     );

    /// <summary>Converts the <see cref="CylindricalCoordinate"/> to a <see cref="PolarCoordinate"/>.</summary>
    public PolarCoordinate ToPolarCoordinate()
     => new(
       m_radius,
       m_azimuth
     );

    /// <summary>Converts the <see cref="CylindricalCoordinate"/> to a <see cref="SphericalCoordinate"/>.</summary>
    public SphericalCoordinate ToSphericalCoordinate()
     => new(
       System.Math.Sqrt(m_radius * m_radius + m_height * m_height),
       System.Math.Atan2(m_radius, m_height),
       m_azimuth
     );

    #region Static methods
    /// <summary>Return a <see cref="CylindricalCoordinate"/> from the specified components.</summary>
    public static CylindricalCoordinate From(Length radius, Azimuth azimuth, Length height)
      => new CylindricalCoordinate(radius.Value, azimuth.ToRadians(), height.Value);
    #endregion Static methods
  }
}
