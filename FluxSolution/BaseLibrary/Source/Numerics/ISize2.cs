namespace Flux
{
  #region ExtensionMethods
  public static partial class ExtensionMethods
  {
    public static Numerics.CartesianCoordinate2<TSelf> ToCartesianCoordinate2<TSelf>(this Numerics.ISize2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(
        source.Width,
        source.Height
      );
  }
  #endregion ExtensionMethods

  namespace Numerics
  {
    public interface ISize2<TSelf>
      where TSelf : System.Numerics.INumber<TSelf>
    {
      TSelf Width { get; init; }
      TSelf Height { get; init; }
    }
  }
}
