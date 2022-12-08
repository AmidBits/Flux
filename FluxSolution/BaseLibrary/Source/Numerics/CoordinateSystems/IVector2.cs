namespace Flux
{
  #region ExtensionMethods
  public static partial class ExtensionMethods
  {
    public static CartesianCoordinate2<TSelf> ToCartesianCoordinate2<TSelf>(this IVector2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(source.X, source.Y);
  }
  #endregion ExtensionMethods

  /// <summary>Cartesian 2D coordinate with real numbers.</summary>
  public interface IVector2<TSelf>
    : ICartesianCoordinate2<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
