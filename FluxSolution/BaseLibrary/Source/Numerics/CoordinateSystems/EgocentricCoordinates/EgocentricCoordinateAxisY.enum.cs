namespace Flux.Coordinates
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="EgocentricCoordinateAxisFB"/> represent the y-axis, i.e. <see cref="EgocentricCoordinateAxisFB.Forward"/>/<see cref="EgocentricCoordinateAxisFB.Backward"/>.</remarks>
  public enum EgocentricCoordinateAxisFB
  {
    /// <summary>
    /// <para>In the direction in front (proper ahead) of some reference.</para>
    /// </summary>
    Forward = 4,
    /// <summary>
    /// <para>In the direction in back of (proper behind) some reference.</para>
    /// </summary>
    Backward = 8,
  }
}
