namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Returns whether <paramref name="value"/> carries the LSB (of bit-count, not bit-length).</para>
    /// <para>E.g. if <see cref="BitFlagCarryLsb{TValue}(TValue)"/> returns true, the <paramref name="value"/> is odd, otherwise it's even.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool BitFlagCarryLsb<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => !TNumber.IsZero(value & TNumber.One);

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> carries the MSB (of bit-count, not bit-length).</para>
    /// <para>E.g. if <typeparamref name="TNumber"/> is signed integer, and <see cref="BitFlagCarryMsb{TValue}(TValue)"/> returns true, the <paramref name="value"/> is negative, otherwise positive.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool BitFlagCarryMsb<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => !TNumber.IsZero(value & (TNumber.One << (value.GetBitCount() - 1)));
  }
}
