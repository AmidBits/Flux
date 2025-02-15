namespace Flux.Geometry.MapProjections
{
  public interface IMapForwardProjectable
  {
    /// <summary>Creates a new <see cref="System.Numerics.Vector3"/> from the <see cref="CoordinateSystems.GeographicCoordinate"/> (where the Z component equals the Altitude component, without any manipulations).</summary>
    public System.Numerics.Vector3 ProjectForward(CoordinateSystems.GeographicCoordinate project);
  }
}
