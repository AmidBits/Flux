#if NET7_0_OR_GREATER
namespace Flux.MapProjections
{
  public interface IMapReverseProjectable
  {
    /// <summary>Creates a new <see cref="Coordinates.GeographicCoordinate"/> from the <see cref="System.Numerics.Vector3"/> (where the Altitude component equals the Z component, without any manipulations).</summary>
    public Coordinates.GeographicCoordinate ProjectReverse(System.Numerics.Vector3 project);
  }
}
#endif
