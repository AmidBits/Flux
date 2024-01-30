using Flux.Random;

namespace Flux
{
  public static partial class Tools
  {
    public enum IntersectTestLine
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

    public static (IntersectTestLine Outcome, double X, double Y) GivenTwoPointsOnEachLine(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
    {
      var sx1x2 = x1 - x2;
      var sy1y2 = y1 - y2;
      var sx3x4 = x3 - x4;
      var sy3y4 = y3 - y4;

      var nl = x1 * y2 - y1 * x2; // P numerator-left component.
      var nr = x3 * y4 - y3 * x4; // P numerator-right component.

      var d = sx1x2 * sy3y4 - sy1y2 * sx3x4;

      var px = (nl * sx3x4 - sx1x2 * nr);
      var py = (nl * sy3y4 - sy1y2 * nr);

      // if(d == 0)return // coincident or parallel, i.e. NOT intersecting.
      if (d != 0)
      {
        px /= d;
        py /= d;

        return (IntersectTestLine.LinesIntersect, px, py);
      }
      else
        return (IntersectTestLine.NonIntersectingLines, double.NaN, double.NaN);
    }

    public static (IntersectTestLine Outcome, System.Numerics.Vector2 Intersection) GivenTwoPointsOnEachLine(System.Numerics.Vector2 v1, System.Numerics.Vector2 v2, System.Numerics.Vector2 v3, System.Numerics.Vector2 v4)
    {
      var result = GivenTwoPointsOnEachLine(v1.X, v1.Y, v2.X, v2.Y, v3.X, v3.Y, v4.X, v4.Y);

      return (result.Outcome, new((float)result.X, (float)result.Y));
    }

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    /// <returns>On left side if greater than zero, on right side if less than zero, and on the line if zero.</returns>
    public static double SideTestLine(double x, double y, double x1, double y1, double x2, double y2)
      => (x2 - x1) * (y - y1) - (y2 - y1) * (x - x1);

  }
}
