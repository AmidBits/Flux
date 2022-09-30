#if NET7_0_OR_GREATER
namespace Flux
{
  public interface I2NodeInterpolatable<TNode, TMu>
    where TNode : System.Numerics.INumber<TNode>
    where TMu : System.Numerics.IFloatingPoint<TMu>
  {
    TMu Interpolate(TNode n1, TNode n2, TMu mu);
  }

  public interface I4NodeInterpolatable<TNode, TMu>
    where TNode : System.Numerics.INumber<TNode>
    where TMu : System.Numerics.IFloatingPoint<TMu>
  {
    TMu Interpolate(TNode n0, TNode n1, TNode n2, TNode n3, TMu mu);
  }
}
#endif
