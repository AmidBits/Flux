namespace Flux.CoordinateSystems
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="EgocentricCoordinateCenter"/> represent the directions, i.e. <see cref="EgocentricCoordinateCenter.Toward"/>/<see cref="EgocentricCoordinateCenter.AwayFrom"/>.</remarks>
  public enum EgocentricCoordinateCenter
  {
    /// <summary>
    /// <para>Toward the center of some reference self.</para>
    /// </summary>
    Toward,
    /// <summary>
    /// <para>Away from the center of some reference self.</para>
    /// </summary>
    AwayFrom,
  }
}
