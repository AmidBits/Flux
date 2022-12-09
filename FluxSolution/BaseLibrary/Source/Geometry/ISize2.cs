namespace Flux
{
  #region ExtensionMethods
  public static partial class ExtensionMethods
  {
    public static CoordinateSystems.CartesianCoordinate2<TSelf> ToCartesianCoordinate2<TSelf>(this ISize2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(
        source.Width,
        source.Height
      );
  }
  #endregion ExtensionMethods

  public interface ISize2<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf Width { get; init; }
    TSelf Height { get; init; }
  }
}
