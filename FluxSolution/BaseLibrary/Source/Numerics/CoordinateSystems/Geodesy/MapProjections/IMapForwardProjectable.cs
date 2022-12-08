namespace Flux.MapProjections
{
  public interface IMapForwardProjectable
  {
    /// <summary>Creates a new <see cref=" CartesianCoordinate3{double}"/> from the <see cref="GeographicCoordinate"/> (where the Z component equals the Altitude component, without any manipulations).</summary>
    public CartesianCoordinate3<double> ProjectForward(GeographicCoordinate project);
  }
}
