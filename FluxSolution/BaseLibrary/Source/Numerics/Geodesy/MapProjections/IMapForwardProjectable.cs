#if NET7_0_OR_GREATER
namespace Flux.Numerics.MapProjections
{
  public interface IMapForwardProjectable
  {
    /// <summary>Creates a new <see cref="CartesianCoordinate3{double}"/> from the <see cref="IGeographicCoordinate"/> (where the Z component equals the Altitude component, without any manipulations).</summary>
    public Numerics.CartesianCoordinate3<double> ProjectForward(Numerics.IGeographicCoordinate project);
  }
}
#endif
