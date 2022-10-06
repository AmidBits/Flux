#if NET7_0_OR_GREATER
namespace Flux
{
  public interface IVector<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }

  public interface IVector2<TSelf>
    : IVector<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    public TSelf X { get; }
    public TSelf Y { get; }
  }


  public interface IVector3<TSelf>
    : IVector2<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    public TSelf Z { get; }
  }
}
#endif
