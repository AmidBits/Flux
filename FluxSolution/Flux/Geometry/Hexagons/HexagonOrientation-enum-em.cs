namespace Flux
{
  public static partial class Em
  {
    public static Geometry.Hexagons.HexOrientation ToOrientation(this Geometry.Hexagons.HexagonOrientation orientation)
      => orientation switch
      {
        Geometry.Hexagons.HexagonOrientation.FlatTop => Geometry.Hexagons.HexOrientation.FlatTopped,
        Geometry.Hexagons.HexagonOrientation.PointyTop => Geometry.Hexagons.HexOrientation.PointyTopped,
        _ => throw new NotImplementedException(),
      };
  }
}
