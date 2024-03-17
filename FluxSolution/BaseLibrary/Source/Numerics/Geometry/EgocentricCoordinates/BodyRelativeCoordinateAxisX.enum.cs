namespace Flux.Geometry
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="BodyRelativeCoordinateAxisX"/> represent the x-axis, i.e. <see cref="BodyRelativeCoordinateAxisX.Left"/>/<see cref="BodyRelativeCoordinateAxisX.Right"/>.</remarks>
  public enum BodyRelativeCoordinateAxisX
  {
    Left = 1,
    Right = 2,
  }
}
