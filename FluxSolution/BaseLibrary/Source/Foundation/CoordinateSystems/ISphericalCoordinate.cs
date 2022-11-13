#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  public interface ISphericalCoordinate
  {
    /// <summary>Radius in meters.</summary>
    Length Radius { get; init; }
    /// <summary>Inclination in radians.</summary>
    Angle Inclination { get; init; }
    /// <summary>Azimuth in radians.</summary>
    Azimuth Azimuth { get; init; }

    /// <summary>Elevation in radians. This is an option/alternative to <see cref="Inclination"/>.</summary>
    Angle Elevatiuon { get; init; }
  }
}
#endif
