namespace Flux.Geometry
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="BodyRelativeCoordinateAxisZ"/> represent the z-axis, i.e. <see cref="BodyRelativeCoordinateAxisZ.Up"/>/<see cref="BodyRelativeCoordinateAxisZ.Down"/>.</remarks>
  public enum BodyRelativeCoordinateAxisZ
  {
    Up = 16,
    Down = 32,
  }
}
