namespace Flux.MapProjections
{
  public interface IMapReverseProjectable
  {
    /// <summary>Converts the <see cref="CartesianCoordinate3R"/> to a <see cref="GeographicCoordinate"/> (where the Altitude component equals the Z component, without any manipulations).</summary>
    public GeographicCoordinate ProjectReverse(CartesianCoordinate3R project);
  }
}
