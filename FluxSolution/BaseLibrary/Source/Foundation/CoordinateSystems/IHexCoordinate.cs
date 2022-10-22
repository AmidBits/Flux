#if NET7_0_OR_GREATER
namespace Flux
{
  public interface IHexCoordinate<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf Q { get; }
    TSelf R { get; }
    TSelf S { get; }

    abstract IHexCoordinate<TSelf> Create(TSelf q, TSelf r, TSelf s);
  }
}
#endif
