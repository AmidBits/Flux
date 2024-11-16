namespace Flux.Geometry
{
  public readonly record struct TriaxialEllipsoid
  {
    private readonly double m_a;
    private readonly double m_b;
    private readonly double m_c;

    public TriaxialEllipsoid(double a, double b, double c)
    {
      m_a = a;
      m_b = b;
      m_c = c;
    }

    public double A => m_a;
    public double B => m_b;
    public double C => m_c;

    public System.Numerics.Vector3 ToVector3() => new((float)m_a, (float)m_b, (float)m_c);

    /// <summary>Creates cartesian 3D coordinates from the <see cref="SphericalCoordinate"/>.</summary>
    /// <remarks>All angles in radians.</remarks>
    public (double x, double y, double z) PolarToCartesianCoordinate3(double inclination, double azimuth)
    {
      var (sp, cp) = double.SinCos(inclination);
      var (sa, ca) = double.SinCos(azimuth);

      return (
        m_a * sp * ca,
        m_b * sp * sa,
        m_c * cp
      );
    }

    /// <summary>Creates cartesian 3D coordinates from the <see cref="SphericalCoordinate"/>.</summary>
    /// <remarks>All angles in radians.</remarks>
    public (double x, double y, double z) EquatorToCartesianCoordinate3(double lat, double lon)
    {
      var (sp, cp) = double.SinCos(lat);
      var (sa, ca) = double.SinCos(lon);

      return (
        m_a * cp * ca,
        m_b * cp * sa,
        m_c * sp
      );
    }
  }
}
