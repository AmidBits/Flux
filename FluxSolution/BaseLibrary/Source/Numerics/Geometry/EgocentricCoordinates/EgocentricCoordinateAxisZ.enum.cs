namespace Flux.Geometry
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="EgocentricCoordinateAxisZ"/> represent the z-axis, i.e. <see cref="EgocentricCoordinateAxisZ.Up"/>/<see cref="EgocentricCoordinateAxisZ.Down"/>.</remarks>
  public enum EgocentricCoordinateAxisZ
  {
    Up = 16,
    Down = 32,
  }
}
