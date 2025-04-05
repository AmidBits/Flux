namespace Flux.Cartography
{
  public interface IMapReverseProjectable
  {
    /// <summary>Creates a new <see cref="CoordinateSystems.GeographicCoordinate"/> from the <see cref="System.Numerics.Vector3"/> (where the Altitude component equals the Z component, without any manipulations).</summary>
    public CoordinateSystems.GeographicCoordinate ProjectReverse(System.Numerics.Vector3 project);
  }
}
