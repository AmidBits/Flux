namespace Flux.Geometry
{
  public enum LineIntersectTest
  {
    /// <summary>
    /// <para>The line intersect test outcome is not known.</para>
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// <para>The lines will intersect.</para>
    /// <see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line"/>
    /// </summary>
    LinesIntersect,
    /// <summary>
    /// <para>The lines are parallel or coincident, and will not intersect.</para>
    /// <see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line"/>
    /// </summary>
    NonIntersectingLines,
  }
}
