namespace Flux
{
  public static partial class Tools
  {
    public enum IntersectTestLineSegment
    {
      /// <summary>
      /// <para>The line segment intersect test outcome is not known.</para>
      /// </summary>
      Unknown = 0,
      /// <summary>
      /// <para>The intersection point is within the first line segment.</para>
      /// <see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment"/>
      /// </summary>
      IntersectWithinFirst,
      /// <summary>
      /// <para>The intersection point is within the second line segment.</para>
      /// <see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment"/>
      /// </summary>
      IntersectWithinSecond,
      /// <summary>
      /// <para>The line segments are coincident to each other.</para>
      /// <see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment"/>
      /// </summary>
      Coincident,
      /// <summary>
      /// <para>The line segments are parallel to each other.</para>
      /// <see href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line_segment"/>
      /// </summary>
      Parallel,
    }

    public static (IntersectTestLineSegment Outcome, double X, double Y) GivenTwoPointsOnEachLineSegment(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
    {
      var sx1x2 = x1 - x2;
      var sx1x3 = x1 - x3;
      var sy1y2 = y1 - y2;
      var sy1y3 = y1 - y3;
      var sx3x4 = x3 - x4;
      var sy3y4 = y3 - y4;

      var d = sx1x2 * sy3y4 - sy1y2 * sx3x4;

      var t = sx1x3 * sy3y4 - sy1y3 * sx3x4;
      var u = sx1x2 * sy1y3 - sy1y2 * sx1x3;

      if (d != 0)
      {
        t /= d;
        u /= d;

        var ptx = x1 + t * (x2 - x1);
        var pty = y1 + t * (y2 - y1);
        var pux = x3 + u * (x4 - x3);
        var puy = y3 + u * (y4 - y3);

        if (t >= 0 && t <= 1)
          return (IntersectTestLineSegment.IntersectWithinFirst, ptx, pty);
        else if (u >= 0 && u <= 1)
          return (IntersectTestLineSegment.IntersectWithinSecond, pux, puy);
        else // Not intersecting.
          return (IntersectTestLineSegment.Unknown, double.NaN, double.NaN);
      }
      else
      {
        if (t == 0 || u == 0) // Coincident lines.
          return (IntersectTestLineSegment.Coincident, double.NaN, double.NaN);
        else // Parallel lines.
          return (IntersectTestLineSegment.Parallel, double.NaN, double.NaN);
      }
    }

    public static (IntersectTestLineSegment Outcome, System.Numerics.Vector2 Intersection) GivenTwoPointsOnEachLineSegment(System.Numerics.Vector2 v1, System.Numerics.Vector2 v2, System.Numerics.Vector2 v3, System.Numerics.Vector2 v4)
    {
      var (Outcome, X, Y) = GivenTwoPointsOnEachLineSegment(v1.X, v1.Y, v2.X, v2.Y, v3.X, v3.Y, v4.X, v4.Y);

      return (Outcome, new((float)X, (float)Y));
    }
  }
}

