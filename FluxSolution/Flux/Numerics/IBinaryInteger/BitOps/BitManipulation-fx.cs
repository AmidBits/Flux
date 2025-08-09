//namespace Flux
//{
//  public static partial class BitOps
//  {
//    /// <summary>
//    /// <para>Clear a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
//    /// </summary>
//    /// <typeparam name="TValue"></typeparam>
//    /// <param name="value"></param>
//    /// <param name="index"></param>
//    /// <returns></returns>
//    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//    public static TValue BitClear<TValue>(this TValue value, int index)
//      where TValue : System.Numerics.IBinaryInteger<TValue>
//      => value & ~(TValue.One << index);

//    /// <summary>
//    /// <para>Flip a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
//    /// </summary>
//    /// <typeparam name="TValue"></typeparam>
//    /// <param name="value"></param>
//    /// <param name="index"></param>
//    /// <returns></returns>
//    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//    public static TValue BitFlip<TValue>(this TValue value, int index)
//      where TValue : System.Numerics.IBinaryInteger<TValue>
//      => value ^ TValue.One << index;

//    /// <summary>
//    /// <para>Determine the state of a bit in <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
//    /// </summary>
//    /// <typeparam name="TValue"></typeparam>
//    /// <param name="value"></param>
//    /// <param name="index"></param>
//    /// <returns></returns>
//    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//    public static bool BitGet<TValue>(this TValue value, int index)
//      where TValue : System.Numerics.IBinaryInteger<TValue>
//      => !TValue.IsZero(value & TValue.One << index);

//    /// <summary>
//    /// <para>Returns whether <paramref name="value"/> carries the LSB (of bit-count, not bit-length).</para>
//    /// <para>E.g. if <see cref="BitCheckLsb{TValue}(TValue)"/> returns true, the <paramref name="value"/> is odd, otherwise it's even.</para>
//    /// </summary>
//    /// <typeparam name="TNumber"></typeparam>
//    /// <param name="value"></param>
//    /// <returns></returns>
//    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//    public static bool BitGetLsb<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IBinaryInteger<TNumber>
//      => !TNumber.IsZero(value & TNumber.One);

//    /// <summary>
//    /// <para>Returns whether <paramref name="value"/> carries the MSB (of bit-count, not bit-length).</para>
//    /// <para>E.g. if <paramref name="value"/> is a signed integer, and <see cref="BitCheckMsb{TValue}(TValue)"/> returns true, the <paramref name="value"/> is negative, otherwise positive.</para>
//    /// </summary>
//    /// <typeparam name="TNumber"></typeparam>
//    /// <param name="value"></param>
//    /// <returns></returns>
//    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//    public static bool BitGetMsb<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IBinaryInteger<TNumber>
//      => value.BitGet(value.GetBitCount() - 1);

//    /// <summary>
//    /// <para>Gets the bit-index of a power-of-2 number.</para>
//    /// </summary>
//    /// <typeparam name="TNumber"></typeparam>
//    /// <param name="value"></param>
//    /// <returns></returns>
//    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
//    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//    public static TNumber BitIndexOfPow2<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IBinaryInteger<TNumber>
//      => TNumber.IsPow2(value) ? TNumber.Log2(value) : throw new System.ArgumentOutOfRangeException(nameof(value), "Not a power-of-2 number.");

//    /// <summary>
//    /// <para>Set a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
//    /// </summary>
//    /// <typeparam name="TValue"></typeparam>
//    /// <param name="value"></param>
//    /// <param name="index"></param>
//    /// <returns></returns>
//    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//    public static TValue BitSet<TValue>(this TValue value, int index)
//      where TValue : System.Numerics.IBinaryInteger<TValue>
//      => value | TValue.One << index;
//  }
//}
