namespace Flux.Geometry
{
  /// <summary>Spherical coordinate.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  public interface ISphericalCoordinate<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.INumber<TSelf>
#endif
  {
    /// <summary>
    /// <para>Radius, (length) unit of meter. A.k.a. radial distance, radial coordinate.</para>
    /// </summary>
    /// <remarks>If the radius is zero, both azimuth and inclination are arbitrary.</remarks>
    TSelf Radius { get; }

    /// <summary>
    /// <para>Inclination angle, unit of radian. A.k.a. polar angle, colatitude, zenith angle, normal angle. This is equivalent to latitude in geographical coordinate systems.</para>
    /// </summary>
    /// <remarks>If the inclination is zero or 180 degrees (π radians), the azimuth is arbitrary.</remarks>
    TSelf Inclination { get; }

    /// <summary>
    /// <para>Azimuth angle, unit of radian. This is equivalent to longitude in geographical coordinate systems.</para>
    /// </summary>
    TSelf Azimuth { get; }

    /// <summary>
    /// <para>Elevation angle, unit of radian. This is an option/alternative to <see cref="Inclination"/>.</para>
    /// </summary>
    /// <remarks>The elevation angle is 90 degrees (π/2 radians) minus the <see cref="Inclination"/> angle.</remarks>
    TSelf Elevation { get; }
  }
}
