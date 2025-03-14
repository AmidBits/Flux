namespace Flux
{
  public static partial class Em
  {
    public static (Geometry.Shapes.Line.LineSegmentIntersectTest Outcome, Geometry.CoordinateSystems.CartesianCoordinate Intersection) IntersectsWith(this Geometry.Shapes.Line.LineSegmentGeometry a, Geometry.Shapes.Line.LineSegmentGeometry b)
    {
      var (Outcome, Intersection) = Geometry.Shapes.Line.LineSegmentGeometry.GivenTwoPointsOnEach(a.V1, a.V2, b.V1, b.V2);

      return (Outcome, new(Intersection));
    }
  }
}
