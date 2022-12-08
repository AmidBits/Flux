namespace Flux
{
  #region ExtensionMethods
  public static partial class ExtensionMethods
  {
    public static CartesianCoordinate3<TSelf> ToCartesianCoordinate3<TSelf>(this IVector3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(source.X, source.Y, source.Z);
  }
  #endregion ExtensionMethods

  /// <summary>Cartesian 3D coordinate with real numbers.</summary>
  public interface IVector3<TSelf>
    : ICartesianCoordinate3<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
