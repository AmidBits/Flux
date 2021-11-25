namespace Flux.MapProjections
{
  public interface IMapProjection
  {
    /// <summary>Converts the <see cref="GeographicCoordinate"/> to a <see cref="CartesianCoordinate3"/> (where the Z component equals the Altitude component, without any manipulations).</summary>
    public CartesianCoordinate3 Forward(GeographicCoordinate project);
  }
}
