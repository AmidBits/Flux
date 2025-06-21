namespace Flux.CoordinateSystems
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="ReferenceRelativeOrientationLR"/> represents <see cref="ReferenceRelativeOrientationLR.Left"/> and <see cref="ReferenceRelativeOrientationLR.Right"/>.</remarks>
  /// ReferenceRelativeOrientationLR
  public enum ReferenceRelativeOrientationLR
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
