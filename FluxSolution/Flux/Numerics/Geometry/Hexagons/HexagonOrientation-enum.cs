namespace Flux
{
  public static partial class HexagonExtension
  {
    public static Numerics.Geometry.Hexagons.HexOrientation ToOrientation(this Numerics.Geometry.Hexagons.HexagonOrientation orientation)
      => orientation switch
      {
        Numerics.Geometry.Hexagons.HexagonOrientation.FlatTop => Numerics.Geometry.Hexagons.HexOrientation.FlatTopped,
        Numerics.Geometry.Hexagons.HexagonOrientation.PointyTop => Numerics.Geometry.Hexagons.HexOrientation.PointyTopped,
        _ => throw new NotImplementedException(),
      };
  }

  namespace Numerics.Geometry.Hexagons
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
