//namespace Flux.Geometry
//{
//  /// <summary>
//  /// <para>An ellipsoid is a surface that can be obtained from a sphere by deforming it by means of directional scalings, or more generally, of an affine transformation.</para>
//  /// <para>An ellipsoid has three pairwise perpendicular axes of symmetry which intersect at a center of symmetry, called the center of the ellipsoid.</para>
//  /// <para>If the three axes have different lengths, the figure is a triaxial ellipsoid.</para>
//  /// <para><see href="https://en.wikipedia.org/wiki/Ellipsoid"/></para>
//  /// </summary>
//  public readonly record struct TriaxialEllipsoid
//    : IEllipsoid
//  {
//    private readonly double m_a;
//    private readonly double m_b;
//    private readonly double m_c;

//    public TriaxialEllipsoid(double a, double b, double c)
//    {
//      m_a = a;
//      m_b = b;
//      m_c = c;
//    }

//    public double A => m_a;
//    public double B => m_b;
//    public double C => m_c;

//    /// <summary>
//    /// <para>Creates cartesian 3D coordinates from spherical coordinates using <paramref name="inclination"/> angle [ 0, +Pi ] from the zenith reference direction as latitude and <paramref name="azimuth"/> as longitude.</para>
//    /// </summary>
//    /// <remarks>All angles in radians.</remarks>
//    /// <param name="inclination"></param>
//    /// <param name="azimuth"></param>
//    /// <returns></returns>
//    public CoordinateSystems.CartesianCoordinate ByInclinationToCartesianCoordinate3(double inclination, double azimuth)
//    {
//      var (x, y, z) = CoordinateSystems.SphericalCoordinate.ConvertSphericalByInclinationToCartesianCoordinate3(inclination, azimuth, m_a, m_b, m_c);

//      return new(x, y, z, 0);
//    }

//    /// <summary>
//    /// <para>Creates cartesian 3D coordinates from spherical coordinates using elevation angle [ -Pi/2, +Pi/2 ] relative the equator (positive being upwards) as <paramref name="lat"/>itude and <paramref name="lon"/>gitude.</para>
//    /// </summary>
//    /// <remarks>All angles in radians.</remarks>
//    /// <param name="lat"></param>
//    /// <param name="lon"></param>
//    /// <returns></returns>
//    public CoordinateSystems.CartesianCoordinate ByElevationToCartesianCoordinate3(double lat, double lon)
//    {
//      var (x, y, z) = CoordinateSystems.SphericalCoordinate.ConvertSphericalByElevationToCartesianCoordinate3(lat, lon, m_a, m_b, m_c);

//      return new(x, y, z, 0);
//    }
//  }
//}
