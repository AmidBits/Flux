namespace Flux
{
  public interface IFactorialComputable<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.IBinaryInteger<TSelf>
#endif
  {
    TSelf ComputeFactorial(TSelf source);
  }
}
