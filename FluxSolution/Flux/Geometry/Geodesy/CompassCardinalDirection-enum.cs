namespace Flux.Geometry.Geodesy
{
  /// <summary>The four cardinal directions, or cardinal points, are the directions north, east, south, and west, commonly denoted by their initials N, E, S, and W. Each principal wind is 90° from its two neighbours.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Cardinal_direction"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
  public enum CompassCardinalDirection
  {
    N = CompassRose32Wind.N,
    E = CompassRose32Wind.E,
    S = CompassRose32Wind.S,
    W = CompassRose32Wind.W
  }
}
