namespace Flux
{
  public static partial class Bits
  {
    /// <summary>Create a bit mask with <paramref name="oneCount"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static TResult CreateBitMask<TSelf, TResult>(this TSelf oneCount, out TResult result)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
      => result = BitFoldRight(TResult.One << (int.CreateChecked(oneCount) - 1));

    /// <summary>Create a bit mask with <paramref name="oneCount"/> bits set to 1 then <paramref name="trailingZeroCount"/> bits set to 0, in that order, on the LSB (Least Significant Bit) side.</summary>
    public static TResult CreateBitMask<TSelf, TResult>(this TSelf oneCount, TSelf trailingZeroCount, out TResult result)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
      => result = CreateBitMask(oneCount, out TResult _) << int.CreateChecked(trailingZeroCount);
  }
}
