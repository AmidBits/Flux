namespace Flux.Interpolation
{
  public interface I2NodeInterpolatable<TNode, TMu>
    where TNode : System.Numerics.INumber<TNode>
    where TMu : System.Numerics.IFloatingPoint<TMu>
  {
    TMu Interpolate2Node(TNode n1, TNode n2, TMu mu);
  }
}
