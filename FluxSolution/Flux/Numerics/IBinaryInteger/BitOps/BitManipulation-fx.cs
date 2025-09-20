namespace Flux
{
  public static partial class BitOps
  {
    //    #region Bit manipulations

    /// <summary>
    /// <para>Clear a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static TInteger ClearBit<TInteger>(this TInteger value, int index)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value & ~(TInteger.One << index);

    /// <summary>
    /// <para>Flip a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static TInteger FlipBit<TInteger>(this TInteger value, int index)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value ^ TInteger.One << index;

    /// <summary>
    /// <para>Determine the state of a bit in <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static bool GetBit<TInteger>(this TInteger value, int index)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => !TInteger.IsZero(value & TInteger.One << index);

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> carries the LSB (of bit-count, not bit-length).</para>
    /// <para>E.g. if <see cref="BitCheckLsb{TValue}(TValue)"/> returns true, the <paramref name="value"/> is odd, otherwise it's even.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool BitGetLs1b<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => !TInteger.IsZero(value & TInteger.One);

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> carries the MSB (of bit-count, not bit-length).</para>
    /// <para>E.g. if <paramref name="value"/> is a signed integer, and <see cref="BitCheckMsb{TValue}(TValue)"/> returns true, the <paramref name="value"/> is negative, otherwise positive.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool BitGetMs1b<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value.GetBit(value.GetBitCount() - 1);

    /// <summary>
    /// <para>Gets the bit-index of a power-of-2 number.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TInteger BitIndexOfPow2<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => TInteger.IsPow2(value) ? TInteger.Log2(value) : throw new System.ArgumentOutOfRangeException(nameof(value), "Not a power-of-2 number.");

    /// <summary>
    /// <para>Set a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static TInteger SetBit<TInteger>(this TInteger value, int index)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value | TInteger.One << index;

    //    #endregion // Bit manipulations
  }
}
