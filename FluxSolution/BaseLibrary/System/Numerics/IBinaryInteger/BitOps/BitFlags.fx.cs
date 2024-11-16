namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Returns whether <paramref name="value"/> carries the LSB (of bit-count, not bit-length).</para>
    /// <para>E.g. if <see cref="BitFlagCarryLsb{TValue}(TValue)"/> returns true, the <paramref name="value"/> is odd, otherwise it's even.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool BitFlagCarryLsb<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => !TValue.IsZero(value & TValue.One);

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> carries the MSB (of bit-count, not bit-length).</para>
    /// <para>E.g. if <typeparamref name="TValue"/> is signed integer, and <see cref="BitFlagCarryMsb{TValue}(TValue)"/> returns true, the <paramref name="value"/> is negative, otherwise positive.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool BitFlagCarryMsb<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => !TValue.IsZero(value & (TValue.One << (value.GetBitCount() - 1)));
  }
}
