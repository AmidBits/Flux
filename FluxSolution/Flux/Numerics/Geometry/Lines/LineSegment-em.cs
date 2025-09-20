namespace Flux
{
  public static partial class Em
  {
    public static (Numerics.Geometry.Lines.LineSegmentIntersectTest Outcome, CoordinateSystems.CartesianCoordinate Intersection) IntersectsWith(this Numerics.Geometry.Lines.LineSegment a, Numerics.Geometry.Lines.LineSegment b)
    {
      var (ax1, ay1, ax2, ay2) = a;
      var (bx1, by1, bx2, by2) = b;

      var (Outcome, X, Y) = Numerics.Geometry.Lines.LineSegment.GivenTwoPointsOnEach(ax1, ay1, ax2, ay2, bx1, by1, bx2, by2);

      return (Outcome, new(X, Y));
    }
  }
}
