namespace Flux.Numerics
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="BodyRelativeCoordinateAxisX"/> represent the most common ones, i.e. <see cref="BodyRelativeCoordinateAxisX.Left"/>/<see cref="BodyRelativeCoordinateAxisX.Right"/>.</remarks>
  public enum BodyRelativeCoordinateAxisX
  {
    Left = 1,
    Right = 2,
  }

  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="BodyRelativeCoordinateAxisY"/> represent the most common ones, i.e. <see cref="BodyRelativeCoordinateAxisY.Forward"/>/<see cref="BodyRelativeCoordinateAxisY.Backward"/>.</remarks>
  public enum BodyRelativeCoordinateAxisY
  {
    Forward = 4,
    Backward = 8,
  }

  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="BodyRelativeCoordinateAxisZ"/> represent the most common ones, i.e. <see cref="BodyRelativeCoordinateAxisZ.Up"/>/<see cref="BodyRelativeCoordinateAxisZ.Down"/>.</remarks>
  public enum BodyRelativeCoordinateAxisZ
  {
    Up = 16,
    Down = 32,
  }

  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="BodyRelativeCoordinateAxes"/> represents the most common ones, i.e. <see cref="BodyRelativeCoordinateAxes.Left"/>/<see cref="BodyRelativeCoordinateAxes.Right"/> (x-axis), <see cref="BodyRelativeCoordinateAxes.Forward"/>/<see cref="BodyRelativeCoordinateAxes.Backward"/> and <see cref="BodyRelativeCoordinateAxes.Up"/>/<see cref="BodyRelativeCoordinateAxes.Down"/>. They form three pairs of orthogonal axes.</remarks>
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
