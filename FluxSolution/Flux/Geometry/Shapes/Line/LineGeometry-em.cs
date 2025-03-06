namespace Flux
{
  public static partial class Em
  {
    public static (Geometry.Shapes.Line.LineIntersectTest Outcome, Geometry.CoordinateSystems.CartesianCoordinate Intersection) IntersectsWith(this Geometry.Shapes.Line.LineGeometry a, Geometry.Shapes.Line.LineGeometry b)
    {
      var (Outcome, Intersection) = Geometry.Shapes.Line.LineGeometry.GivenTwoPointsOnEach(a.V1, a.V2, b.V1, b.V2);

      return (Outcome, new(Intersection));
    }
  }
}
