namespace Flux.MapProjections
{
  public interface IMapReverseProjectable
  {
    /// <summary>Creates a new <see cref="GeographicCoordinate"/> from the <see cref=" CartesianCoordinate3{double}"/> (where the Altitude component equals the Z component, without any manipulations).</summary>
    public GeographicCoordinate ProjectReverse(ICartesianCoordinate3<double> project);
  }
}
