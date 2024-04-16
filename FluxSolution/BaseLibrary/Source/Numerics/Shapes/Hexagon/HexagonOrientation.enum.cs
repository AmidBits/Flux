namespace Flux
{
  public static partial class Em
  {
    public static Geometry.HexOrientation ToOrientation(this Geometry.HexagonOrientation orientation)
      => orientation switch
      {
        Geometry.HexagonOrientation.FlatTop => Geometry.HexOrientation.FlatTopped,
        Geometry.HexagonOrientation.PointyTop => Geometry.HexOrientation.PointyTopped,
        _ => throw new NotImplementedException(),
      };
  }

  namespace Geometry
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
