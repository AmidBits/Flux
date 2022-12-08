namespace Flux
{
  #region ExtensionMethods
  public static partial class ExtensionMethods
  {
    public static CartesianCoordinate3<TSelf> ToCartesianCoordinate3<TSelf>(this ISize3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(source.Width, source.Height, source.Depth);
  }
  #endregion ExtensionMethods

  public interface ISize3<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf Width { get; init; }
    TSelf Height { get; init; }
    TSelf Depth { get; init; }
  }
}
