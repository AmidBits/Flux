namespace Flux
{
  /// <summary>The polar coordinate system is a two-dimensional coordinate system in which each point on a plane is determined by a distance from a reference point and an angle from a reference direction.</summary>
  public interface IPolarCoordinate
  {
    double Radius { get; }
    double Azimuth { get; }
  }
}
