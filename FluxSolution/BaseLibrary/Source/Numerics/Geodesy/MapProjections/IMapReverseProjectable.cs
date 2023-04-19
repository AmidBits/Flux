#if NET7_0_OR_GREATER
namespace Flux.Numerics.MapProjections
{
  public interface IMapReverseProjectable
  {
    /// <summary>Creates a new <see cref="IGeographicCoordinate"/> from the <see cref="ICartesianCoordinate3{double}"/> (where the Altitude component equals the Z component, without any manipulations).</summary>
    public Numerics.IGeographicCoordinate ProjectReverse(Numerics.ICartesianCoordinate3<double> project);
  }
}
#endif
