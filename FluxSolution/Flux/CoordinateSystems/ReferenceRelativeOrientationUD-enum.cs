namespace Flux.CoordinateSystems
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="ReferenceRelativeOrientationUD"/> represents <see cref="ReferenceRelativeOrientationUD.Up"/> and <see cref="ReferenceRelativeOrientationUD.Down"/>.</remarks>
  /// ReferenceRelativeOrientationUD
  public enum ReferenceRelativeOrientationUD
  {
    /// <summary>
    /// <para>In the direction above some reference.</para>
    /// </summary>
    Up = 16,
    /// <summary>
    /// <para>In the direction below some reference.</para>
    /// </summary>
    Down = 32,
  }
}
