namespace Flux
{
  public static partial class Em
  {
    public static (Geometry.Lines.LineIntersectTest Outcome, CoordinateSystems.CartesianCoordinate Intersection) IntersectsWith(this Geometry.Lines.Line a, Geometry.Lines.Line b)
    {
      var (ax0, ay0, ax1, ay1) = a;
      var (bx0, by0, bx1, by1) = b;

      var (Outcome, X, Y) = Geometry.Lines.Line.GivenTwoPointsOnEach(ax0, ay0, ax1, ay1, bx0, by0, bx1, by1);

      return (Outcome, new(X, Y));
    }
  }
}
