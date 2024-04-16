namespace Flux.Coordinates
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="EgocentricCoordinateIO"/> represent the directions, i.e. <see cref="EgocentricCoordinateIO.Inward"/>/<see cref="EgocentricCoordinateIO.Outward"/>.</remarks>
  public enum EgocentricCoordinateIO
  {
    Inward,
    Outward,
  }
}
