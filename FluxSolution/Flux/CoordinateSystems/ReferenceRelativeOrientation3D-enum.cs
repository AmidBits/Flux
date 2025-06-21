namespace Flux.CoordinateSystems
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="ReferenceRelativeOrientation3D"/> represents <see cref="ReferenceRelativeOrientationLR"/>, <see cref="ReferenceRelativeOrientationUD"/> and <see cref="ReferenceRelativeOrientationFB"/>. They form three orthogonal axes.</remarks>
  [System.Flags]
  public enum ReferenceRelativeOrientation3D
  {
    Left = ReferenceRelativeOrientationLR.Left,
    Right = ReferenceRelativeOrientationLR.Right,
    Forward = ReferenceRelativeOrientationFB.Forward,
    Backward = ReferenceRelativeOrientationFB.Backward,
    Up = ReferenceRelativeOrientationUD.Up,
    Down = ReferenceRelativeOrientationUD.Down,
  };
}
