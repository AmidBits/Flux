namespace Flux
{
  public static partial class Fx
  {
    public static Geometry.Shapes.Hexagon.HexOrientation ToOrientation(this Geometry.Shapes.Hexagon.HexagonOrientation orientation)
      => orientation switch
      {
        Geometry.Shapes.Hexagon.HexagonOrientation.FlatTop => Geometry.Shapes.Hexagon.HexOrientation.FlatTopped,
        Geometry.Shapes.Hexagon.HexagonOrientation.PointyTop => Geometry.Shapes.Hexagon.HexOrientation.PointyTopped,
        _ => throw new NotImplementedException(),
      };
  }

  namespace Geometry.Shapes.Hexagon
  {
    public enum HexagonOrientation
    {
      /// <summary>The hexagon orientation has the flat parallel sides are vertical (at top/bottom) and the points are horizontal (on left/right).</summary>
      FlatTop,
      /// <summary>The hexagon orientation has the points are vertical (at top/bottom) and the flat parallel sides are horizontal (on left/right).</summary>
      PointyTop
    }
  }
}
