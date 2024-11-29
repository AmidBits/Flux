using Flux.Coordinates;

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

    /// <summary>
    /// <para>Creates cartesian 3D coordinates from spherical coordinates using <paramref name="inclination"/> angle [ 0, +Pi ] from the zenith reference direction as latitude and <paramref name="azimuth"/> as longitude.</para>
    /// </summary>
    /// <remarks>All angles in radians.</remarks>
    /// <param name="inclination"></param>
    /// <param name="azimuth"></param>
    /// <returns></returns>
    public Coordinates.CartesianCoordinate PolarToCartesianCoordinate3(double inclination, double azimuth)
    {
      var (x, y, z) = SphericalCoordinate.ConvertSphericalByInclinationToCartesianCoordinate3(inclination, azimuth, m_a, m_b, m_c);

      return new(x, y, z, 0);
    }

    /// <summary>
    /// <para>Creates cartesian 3D coordinates from spherical coordinates using elevation angle [ -Pi/2, +Pi/2 ] relative the equator (positive being upwards) as <paramref name="lat"/>itude and <paramref name="lon"/>gitude.</para>
    /// </summary>
    /// <remarks>All angles in radians.</remarks>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    /// <returns></returns>
    public Coordinates.CartesianCoordinate EquatorToCartesianCoordinate3(double lat, double lon)
    {
      var (x, y, z) = SphericalCoordinate.ConvertSphericalByElevationToCartesianCoordinate3(lat, lon, m_a, m_b, m_c);

      return new(x, y, z, 0);
    }
  }
}
