namespace Flux
{
  public static partial class Em
  {
    public static (Geometry.Shapes.Line.LineIntersectTest Outcome, Geometry.CoordinateSystems.CartesianCoordinate Intersection) IntersectsWith(this Geometry.Shapes.Line.LineGeometry a, Geometry.Shapes.Line.LineGeometry b)
    {
      var (ax0, ay0, ax1, ay1) = a;
      var (bx0, by0, bx1, by1) = b;

      var (Outcome, X, Y) = Geometry.Shapes.Line.LineGeometry.GivenTwoPointsOnEach(ax0, ay0, ax1, ay1, bx0, by0, bx1, by1);

      return (Outcome, new(X, Y));
    }
  }
}
