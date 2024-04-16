namespace Flux.Coordinates
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="EgocentricCoordinateAxisY"/> represent the y-axis, i.e. <see cref="EgocentricCoordinateAxisY.Forward"/>/<see cref="EgocentricCoordinateAxisY.Backward"/>.</remarks>
  public enum EgocentricCoordinateAxisY
  {
    Forward = 4,
    Backward = 8,
  }
}
