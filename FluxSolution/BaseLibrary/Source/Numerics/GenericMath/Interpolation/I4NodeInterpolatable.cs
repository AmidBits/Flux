namespace Flux
{
  public interface I4NodeInterpolatable<TNode, TMu>
    where TNode : System.Numerics.INumber<TNode>
    where TMu : System.Numerics.IFloatingPoint<TMu>
  {
    TMu Interpolate4Node(TNode n0, TNode n1, TNode n2, TNode n3, TMu mu);
  }
}
