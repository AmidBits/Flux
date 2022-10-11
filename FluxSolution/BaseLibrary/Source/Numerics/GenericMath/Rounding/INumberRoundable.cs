#if NET7_0_OR_GREATER
namespace Flux
{
  public interface INumberRoundable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf RoundNumber(TSelf value);
  }
}
#endif
