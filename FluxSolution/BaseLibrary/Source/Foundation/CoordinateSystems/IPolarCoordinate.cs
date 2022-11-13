#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>The polar coordinate system is a two-dimensional coordinate system in which each point on a plane is determined by a distance from a reference point and an angle from a reference direction.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  public interface IPolarCoordinate
  {
    /// <summary>Radius in meters.</summary>
    Length Radius { get; init; }
    /// <summary>Azimuth in radians.</summary>
    Azimuth Azimuth { get; init; }
  }
}
#endif
