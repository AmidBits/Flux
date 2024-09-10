namespace Flux.Numerics
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Create a bit-mask with <paramref name="oneBitCount"/> most-significant-bits set to 1.</para>
    /// </summary>
    /// <typeparam name="TBitCount"></typeparam>
    /// <param name="oneBitCount">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitCount"/>.</param>
    /// <returns></returns>
    public static TBitCount BitMaskLeft<TBitCount>(TBitCount oneBitCount)
      where TBitCount : System.Numerics.IBinaryInteger<TBitCount>
      => BitMaskRight(oneBitCount) << (oneBitCount.GetBitCount() - int.CreateChecked(oneBitCount));

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="oneBitCount"/> least-significant-bits set to 1, and the number of <paramref name="trailingZeroBitCount"/> (least-significant-bits set to zero).</para>
    /// </summary>
    /// <typeparam name="TBitCount"></typeparam>
    /// <param name="oneBitCount">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitCount"/>.</param>
    /// <param name="trailingZeroBitCount">The number of least-significant-bits set to zero after the most-significant-1-bits set by <paramref name="oneBitCount"/>.</param>
    /// <returns></returns>
    /// <remarks>This is a specialized version for <see cref="System.Numerics.BigInteger"/> which has a dynamic bit-storage.</remarks>
    public static TBitCount BitMaskRight<TBitCount>(TBitCount oneBitCount, int trailingZeroBitCount)
      where TBitCount : System.Numerics.IBinaryInteger<TBitCount>
      => BitMaskRight(oneBitCount) << trailingZeroBitCount;

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="oneBitCount"/> least-significant-bits set to 1.</para>
    /// </summary>
    /// <typeparam name="TBitCount"></typeparam>
    /// <param name="oneBitCount">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitCount"/>.</param>
    /// <returns></returns>
    public static TBitCount BitMaskRight<TBitCount>(TBitCount oneBitCount)
      where TBitCount : System.Numerics.IBinaryInteger<TBitCount>
      => (((TBitCount.One << (int.CreateChecked(oneBitCount) - 1)) - TBitCount.One) << 1) | TBitCount.One;
  }
}
