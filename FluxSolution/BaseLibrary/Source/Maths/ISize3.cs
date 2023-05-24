//namespace Flux
//{
//  #region ExtensionMethods
//  public static partial class ExtensionMethods
//  {
//    public static Numerics.CartesianCoordinate3<TSelf> ToCartesianCoordinate3<TSelf>(this Numerics.ISize3<TSelf> source)
//      where TSelf : System.Numerics.INumber<TSelf>
//      => new(
//        source.Width,
//        source.Height,
//        source.Depth
//      );
//  }
//  #endregion ExtensionMethods

//  namespace Numerics
//  {
//    public interface ISize3<TSelf>
//      where TSelf : System.Numerics.INumber<TSelf>
//    {
//      TSelf Width { get; init; }
//      TSelf Height { get; init; }
//      TSelf Depth { get; init; }
//    }
//  }
//}
