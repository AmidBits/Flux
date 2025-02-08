namespace Flux.Geometry.Geodesy
{
  /// <summary>The two cardinal axes are the latitudinal north-south and the longitudinal east-west.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Cardinal_direction"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum CompassCardinalAxis
  {
    /// <summary>Ranges from negative (west, -180 degrees) to positive (east, +180 degrees). The prime meridian is zero, the center (of two halfs of Earth/planetary body) between east and west, ranging (-180, +180).</summary>
    EastWest = 1,
    /// <summary>Ranges from negative (south, -90 degrees) to positive (north, +90 degrees). The equator is zero, the center between south and north, ranging (-90, +90).</summary>
    NorthSouth = 2
  }
}
