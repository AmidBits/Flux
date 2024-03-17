namespace Flux.Geometry
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="BodyRelativeCoordinateAxes"/> represents the most common ones, i.e. the ones from <see cref="BodyRelativeCoordinateAxisX"/>, <see cref="BodyRelativeCoordinateAxisY"/> and <see cref="BodyRelativeCoordinateAxisZ"/>. They form three pairs of orthogonal axes.</remarks>
  public enum BodyRelativeCoordinateAxes
  {
    Left = BodyRelativeCoordinateAxisX.Left,
    Right = BodyRelativeCoordinateAxisX.Right,
    Forward = BodyRelativeCoordinateAxisY.Forward,
    Backward = BodyRelativeCoordinateAxisY.Backward,
    Up = BodyRelativeCoordinateAxisZ.Up,
    Down = BodyRelativeCoordinateAxisZ.Down,
  };
}
