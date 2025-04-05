namespace Flux.Geometry.Lines
{
  public enum LineSegmentIntersectTest
  {
    /// <summary>
    /// <para>The line segment intersect test outcome is not known.</para>
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// <para>The intersection point is within the first line segment.</para>
    /// <see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment"/>
    /// </summary>
    IntersectWithinFirstLineSegment,
    /// <summary>
    /// <para>The intersection point is within the second line segment.</para>
    /// <see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment"/>
    /// </summary>
    IntersectWithinSecondLineSegment,
    /// <summary>
    /// <para>The lines are parallel or coincident, and will not intersect.</para>
    /// <see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line"/>
    /// </summary>
    NonIntersectingLineSegments,
    /// <summary>
    /// <para>The line segments are coincident to each other.</para>
    /// <see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment"/>
    /// </summary>
    CoincidentLineSegments,
    /// <summary>
    /// <para>The line segments are parallel to each other.</para>
    /// <see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment"/>
    /// </summary>
    ParallelLineSegments,
  }
}

