namespace Flux.Geometry.Coordinates
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="EgocentricCoordinateAxes"/> represents the most common ones, i.e. the ones from <see cref="EgocentricCoordinateAxisLR"/>, <see cref="EgocentricCoordinateAxisFB"/> and <see cref="EgocentricCoordinateAxisUD"/>. They form three pairs of orthogonal axes.</remarks>
  [System.Flags]
  public enum EgocentricCoordinateAxes
  {
    Left = EgocentricCoordinateAxisLR.Left,
    Right = EgocentricCoordinateAxisLR.Right,
    Forward = EgocentricCoordinateAxisFB.Forward,
    Backward = EgocentricCoordinateAxisFB.Backward,
    Up = EgocentricCoordinateAxisUD.Up,
    Down = EgocentricCoordinateAxisUD.Down,
  };
}
