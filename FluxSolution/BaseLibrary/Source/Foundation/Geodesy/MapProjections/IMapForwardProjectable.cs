namespace Flux.MapProjections
{
  public interface IMapForwardProjectable
  {
    /// <summary>Converts the <see cref="GeographicCoordinate"/> to a <see cref="CartesianCoordinateR3"/> (where the Z component equals the Altitude component, without any manipulations).</summary>
    public CartesianCoordinateR3 ProjectForward(GeographicCoordinate project);
  }
}
