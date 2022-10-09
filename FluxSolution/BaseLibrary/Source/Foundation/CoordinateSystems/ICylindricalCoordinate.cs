#if NET7_0_OR_GREATER
namespace Flux
{
  public interface ICylindricalCoordinate
  {
    Length Radius { get; }
    Azimuth Azimuth { get; }
    Length Height { get; }
  }
}
#endif
