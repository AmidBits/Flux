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
    FlatTopped,
    PointyTopped
  }
}
