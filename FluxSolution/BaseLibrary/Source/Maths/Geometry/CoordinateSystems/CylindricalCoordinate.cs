namespace Flux.Geometry
{
  /// <summary>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct CylindricalCoordinate
    : System.IFormattable, ICylindricalCoordinate
  {
    public static readonly CylindricalCoordinate Zero;

    private readonly double m_radius;
    private readonly double m_azimuth;
    private readonly double m_height;

    public CylindricalCoordinate(double radius, double azimuth, double height)
    {
      m_radius = radius;
      m_azimuth = azimuth;
      m_height = height;
    }

    public void Deconstruct(out double radius, out double azimuth, out double height) { radius = m_radius; azimuth = m_azimuth; height = m_height; }

    public double Radius { get => m_radius; init => m_radius = value; }
    public double Azimuth { get => m_azimuth; init => m_azimuth = value; }
    public double Height { get => m_height; init => m_height = value; }

    ///// <summary>Converts the <see cref="CylindricalCoordinate{TSelf}"/> to a <see cref="CartesianCoordinate3{TSelf}"/>.</summary>
    //public CartesianCoordinate3<double> ToCartesianCoordinate3()
    //{
    //  var (sa, ca) = System.Math.SinCos(m_azimuth);

    //  return new(
    //       m_radius * ca,
    //       m_radius * sa,
    //       m_height
    //     );
    //}

    ///// <summary>Converts the <see cref="CylindricalCoordinate{TSelf}"/> to a <see cref="PolarCoordinate{TSelf}"/>.</summary>
    //public PolarCoordinate ToPolarCoordinate()
    // => new(
    //   m_radius,
    //   m_azimuth
    // );

    ///// <summary>Converts the <see cref="CylindricalCoordinate"/> to a <see cref="SphericalCoordinate"/>.</summary>
    //public SphericalCoordinate ToSphericalCoordinate()
    // => new(
    //   System.Math.Sqrt(m_radius * m_radius + m_height * m_height),
    //   System.Math.Atan2(m_radius, m_height),
    //   m_azimuth
    // );

    //#region Static methods
    ///// <summary>Return a <see cref="CylindricalCoordinate"/> from the specified components.</summary>
    //public static CylindricalCoordinate<TSelf> From(Quantities.Length radius, Azimuth azimuth, Quantities.Length height)
    //  => new(
    //    TSelf.CreateChecked(radius.Value),
    //    TSelf.CreateChecked(azimuth.ToRadians()),
    //    TSelf.CreateChecked(height.Value)
    //  );
    //#endregion Static methods

    public string ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().GetNameEx()} {{ Radius = {string.Format($"{{0:{format ?? "N1"}}}", Radius)}, Azimuth = {new Units.Angle(Azimuth).ToUnitString(Units.AngleUnit.Degree, format ?? "N3", true)}, Height = {string.Format($"{{0:{format ?? "N1"}}}", Height)} }}";

    public override string ToString() => ToString(null, null);
  }
}