﻿namespace Flux.Coordinates
{
  /// <summary>
  /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
  /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
  /// </summary>
  /// <remarks><see cref="EgocentricCoordinateAxisZ"/> represent the z-axis, i.e. <see cref="EgocentricCoordinateAxisZ.Up"/>/<see cref="EgocentricCoordinateAxisZ.Down"/>.</remarks>
  public enum EgocentricCoordinateAxisZ
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