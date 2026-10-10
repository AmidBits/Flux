namespace Flux.CoordinateSystems
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="ReferenceRelativeOrientationTAf"/> represents <see cref="ReferenceRelativeOrientationTAf.Toward"/> and <see cref="ReferenceRelativeOrientationTAf.AwayFrom"/>.</remarks>
  public enum ReferenceRelativeOrientationTAf
  {
    /// <summary>
    /// <para>Toward some reference point.</para>
    /// </summary>
    Toward = 1,
    /// <summary>
    /// <para>Away-from some reference point.</para>
    /// </summary>
    AwayFrom = 2,
  }

  //public enum RadialDirection
  //{
  //  /// <summary>
  //  /// <para>Radial inward; toward some reference point.</para>
  //  /// </summary>
  //  RadialInward = 1,

  //  /// <summary>
  //  /// <para>Radial outward; away-from some reference point.</para>
  //  /// </summary>
  //  RadialOutward = 2
  //}

  //public enum MotionType
  //{
  //  /// <summary>
  //  /// <para>Converge to; toward some reference point.</para>
  //  /// </summary>
  //  ConvergeTo = 1,

  //  /// <summary>
  //  /// <para>Diverge from; away-from some reference point.</para>
  //  /// </summary>
  //  DivergeFrom = 2
  //}
}
