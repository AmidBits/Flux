namespace Flux.MapProjections
{
  public interface IMapForwardProjectable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    /// <summary>Creates a new <see cref="CartesianCoordinate3{TSelf}"/> from the <see cref="IGeographicCoordinate{TSelf}"/> (where the Z component equals the Altitude component, without any manipulations).</summary>
    public CoordinateSystems.CartesianCoordinate3<TSelf> ProjectForward(CoordinateSystems.IGeographicCoordinate<TSelf> project);
  }
}
