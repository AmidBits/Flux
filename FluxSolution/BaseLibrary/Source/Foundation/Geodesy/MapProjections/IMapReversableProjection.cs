namespace Flux.MapProjections
{
  public interface IMapReversableProjection
  {
    /// <summary>Converts the <see cref="CartesianCoordinate3"/> to a <see cref="GeographicCoordinate"/> (where the Altitude component equals the Z component, without any manipulations).</summary>
    public GeographicCoordinate Reverse(CartesianCoordinate3 project);
  }
}
