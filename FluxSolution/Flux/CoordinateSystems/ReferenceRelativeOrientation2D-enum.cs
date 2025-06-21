namespace Flux.CoordinateSystems
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="ReferenceRelativeOrientation2D"/> represents <see cref="ReferenceRelativeOrientationLR"/> and <see cref="ReferenceRelativeOrientationUD"/>. They form two orthogonal axes.</remarks>
  [System.Flags]
  public enum ReferenceRelativeOrientation2D
  {
    Left = ReferenceRelativeOrientationLR.Left,
    Right = ReferenceRelativeOrientationLR.Right,
    Up = ReferenceRelativeOrientationUD.Up,
    Down = ReferenceRelativeOrientationUD.Down,
  };
}
