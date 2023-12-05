namespace Flux.Geometry
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  public interface ISphericalCoordinate<TSelf>
      where TSelf : System.Numerics.INumber<TSelf>
  {
    /// <summary>Radius. A.k.a. radial distance, radial coordinate.</summary>
    /// <remarks>If the radius is zero, both azimuth and inclination are arbitrary.</remarks>
    TSelf Radius { get; init; }

    /// <summary>The inclination angle in radians. A.k.a. polar angle, colatitude, zenith angle, normal angle. This is equivalent to latitude in geographical coordinate systems.</summary>
    /// <remarks>If the inclination is zero or 180 degrees (π radians), the azimuth is arbitrary.</remarks>
    TSelf Inclination { get; init; }

    /// <summary>The azimuthal angle in radians. This is equivalent to longitude in geographical coordinate systems.</summary>
    TSelf Azimuth { get; init; }

    /// <summary>The elevation angle in radians. This is an option/alternative to <see cref="Inclination"/>.</summary>
    /// <remarks>The elevation angle is 90 degrees (π/2 radians) minus the <see cref="Inclination"/> angle.</remarks>
    TSelf Elevation { get; init; }
  }
}
