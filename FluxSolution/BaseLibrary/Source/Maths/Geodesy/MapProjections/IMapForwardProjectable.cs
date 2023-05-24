#if NET7_0_OR_GREATER
namespace Flux.Geometry.MapProjections
{
  public interface IMapForwardProjectable
  {
    /// <summary>Creates a new <see cref="CartesianCoordinate3{double}"/> from the <see cref="IGeographicCoordinate"/> (where the Z component equals the Altitude component, without any manipulations).</summary>
    public CartesianCoordinate3<double> ProjectForward(IGeographicCoordinate project);
  }
}
#endif
