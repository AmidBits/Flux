namespace Flux
{
  public static partial class Em
  {
    public static Geometry.Shapes.Hexagon.HexOrientation ToOrientation(this Geometry.Shapes.Hexagon.HexagonOrientation orientation)
      => orientation switch
      {
        Geometry.Shapes.Hexagon.HexagonOrientation.FlatTop => Geometry.Shapes.Hexagon.HexOrientation.FlatTopped,
        Geometry.Shapes.Hexagon.HexagonOrientation.PointyTop => Geometry.Shapes.Hexagon.HexOrientation.PointyTopped,
        _ => throw new NotImplementedException(),
      };
  }
}
