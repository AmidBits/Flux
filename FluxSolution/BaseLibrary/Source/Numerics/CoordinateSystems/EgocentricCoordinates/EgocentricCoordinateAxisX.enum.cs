namespace Flux.Coordinates
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="EgocentricCoordinateAxisX"/> represent the x-axis, i.e. <see cref="EgocentricCoordinateAxisX.Left"/>/<see cref="EgocentricCoordinateAxisX.Right"/>.</remarks>
  public enum EgocentricCoordinateAxisX
  {
    /// <summary>
    /// <para>Toward the proper left of some reference.</para>
    /// </summary>
    Left = 1,
    /// <summary>
    /// <para>Toward the proper right of some reference.</para>
    /// </summary>
    Right = 2,
  }
}
