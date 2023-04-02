namespace Flux
{
  public static partial class GeometryExtensionMethods
  {
    public static HexOrientation ToOrientation(this HexagonOrientation orientation)
      => orientation switch
      {
        HexagonOrientation.FlatTopped => HexOrientation.FlatTopped,
        HexagonOrientation.PointyTopped => HexOrientation.PointyTopped,
        _ => throw new NotImplementedException(),
      };
  }

  public enum HexagonOrientation
  {
    /// <summary>The hexagon orientation has the flat parallel sides are vertical (at top/bottom) and the points are horizontal (on left/right).</summary>
    FlatTopped,
    /// <summary>The hexagon orientation has the points are vertical (at top/bottom) and the flat parallel sides are horizontal (on left/right).</summary>
    PointyTopped
  }
}
