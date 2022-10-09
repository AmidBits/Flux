#if NET7_0_OR_GREATER
namespace Flux
{
  public interface ISphericalCoordinate
  {
    Length Radius { get; }
    Angle Inclination { get; }
    Azimuth Azimuth { get; }
  }
}
#endif
