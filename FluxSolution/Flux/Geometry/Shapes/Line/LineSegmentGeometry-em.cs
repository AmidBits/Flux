namespace Flux
{
  public static partial class Em
  {
    public static (Geometry.Shapes.Line.LineSegmentIntersectTest Outcome, Geometry.CoordinateSystems.CartesianCoordinate Intersection) IntersectsWith(this Geometry.Shapes.Line.LineSegmentGeometry a, Geometry.Shapes.Line.LineSegmentGeometry b)
    {
      var (ax1, ay1, ax2, ay2) = a;
      var (bx1, by1, bx2, by2) = b;

      var (Outcome, X, Y) = Geometry.Shapes.Line.LineSegmentGeometry.GivenTwoPointsOnEach(ax1, ay1, ax2, ay2, bx1, by1, bx2, by2);

      return (Outcome, new(X, Y));
    }
  }
}
