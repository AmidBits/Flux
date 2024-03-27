namespace Flux.Geometry
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="EgocentricCoordinateAxes"/> represents the most common ones, i.e. the ones from <see cref="EgocentricCoordinateAxisX"/>, <see cref="EgocentricCoordinateAxisY"/> and <see cref="EgocentricCoordinateAxisZ"/>. They form three pairs of orthogonal axes.</remarks>
  public enum EgocentricCoordinateAxes
  {
    Left = EgocentricCoordinateAxisX.Left,
    Right = EgocentricCoordinateAxisX.Right,
    Forward = EgocentricCoordinateAxisY.Forward,
    Backward = EgocentricCoordinateAxisY.Backward,
    Up = EgocentricCoordinateAxisZ.Up,
    Down = EgocentricCoordinateAxisZ.Down,
  };
}
