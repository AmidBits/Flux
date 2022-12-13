namespace Flux.Numerics.MapProjections
{
  public interface IMapForwardProjectable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    /// <summary>Creates a new <see cref="CartesianCoordinate3{TSelf}"/> from the <see cref="IGeographicCoordinate{TSelf}"/> (where the Z component equals the Altitude component, without any manipulations).</summary>
    public Numerics.CartesianCoordinate3<TSelf> ProjectForward(Numerics.IGeographicCoordinate<TSelf> project);
  }
}
