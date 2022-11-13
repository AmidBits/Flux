#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
  public interface ICylindricalCoordinate
  {
    /// <summary>Radius as <see cref="Flux.Length"/>.</summary>
    Length Radius { get; init; }
    /// <summary><see cref="Flux.Azimuth"/>.</summary>
    Azimuth Azimuth { get; init; }
    /// <summary>Height as <see cref="Flux.Length"/>.</summary>
    Length Height { get; init; }
  }
}
#endif
