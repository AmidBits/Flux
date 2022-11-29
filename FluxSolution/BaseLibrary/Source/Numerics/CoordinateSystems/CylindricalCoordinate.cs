namespace Flux
{
  /// <summary>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct CylindricalCoordinate<TSelf>
    : ICylindricalCoordinate<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  {
    private readonly TSelf m_radius;
    private readonly TSelf m_azimuth;
    private readonly TSelf m_height;

    public CylindricalCoordinate(TSelf radius, TSelf azimuth, TSelf height)
    {
      m_radius = radius;
      m_azimuth = azimuth;
      m_height = height;
    }

    public TSelf Radius { get => m_radius; init => m_radius = value; }
    public TSelf Azimuth { get => m_azimuth; init => m_azimuth = value; }
    public TSelf Height { get => m_height; init => m_height = value; }

    /// <summary>Converts the <see cref="CylindricalCoordinate"/> to a <see cref="Vector3"/>.</summary>
    public CartesianCoordinate3<TSelf> ToCartesianCoordinate3()
     => new(
       m_radius * TSelf.Cos(m_azimuth),
       m_radius * TSelf.Sin(m_azimuth),
       m_height
     );

    /// <summary>Converts the <see cref="CylindricalCoordinate"/> to a <see cref="PolarCoordinate"/>.</summary>
    public PolarCoordinate<TSelf> ToPolarCoordinate()
     => new(
       m_radius,
       m_azimuth
     );

    /// <summary>Converts the <see cref="CylindricalCoordinate"/> to a <see cref="SphericalCoordinate"/>.</summary>
    public SphericalCoordinate<TSelf> ToSphericalCoordinate()
     => new(
       TSelf.Sqrt(m_radius * m_radius + m_height * m_height),
       TSelf.Atan2(m_radius, m_height),
       m_azimuth
     );

    #region Static methods
    /// <summary>Return a <see cref="CylindricalCoordinate"/> from the specified components.</summary>
    public static CylindricalCoordinate<TSelf> From(Length radius, Azimuth azimuth, Length height)
      => new(
        TSelf.CreateChecked(radius.Value),
        TSelf.CreateChecked(azimuth.ToRadians()),
        TSelf.CreateChecked(height.Value)
      );
    #endregion Static methods
  }
}
