namespace Flux.Geometry
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="BodyRelativeCoordinateAxisY"/> represent the y-axis, i.e. <see cref="BodyRelativeCoordinateAxisY.Forward"/>/<see cref="BodyRelativeCoordinateAxisY.Backward"/>.</remarks>
  public enum BodyRelativeCoordinateAxisY
  {
    Forward = 4,
    Backward = 8,
  }
}
