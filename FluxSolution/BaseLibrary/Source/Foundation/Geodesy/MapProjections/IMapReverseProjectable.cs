namespace Flux.MapProjections
{
  public interface IMapReverseProjectable
  {
    /// <summary>Converts the <see cref="Vector3"/> to a <see cref="GeographicCoordinate"/> (where the Altitude component equals the Z component, without any manipulations).</summary>
    public GeographicCoordinate ProjectReverse(Vector3 project);
  }
}
