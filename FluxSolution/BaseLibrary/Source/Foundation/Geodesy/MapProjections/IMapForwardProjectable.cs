namespace Flux.MapProjections
{
  public interface IMapForwardProjectable
  {
    /// <summary>Converts the <see cref="GeographicCoordinate"/> to a <see cref="CartesianCoordinate3"/> (where the Z component equals the Altitude component, without any manipulations).</summary>
    public CartesianCoordinate3 ProjectForward(GeographicCoordinate project);
  }
}
