namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Determine the state of a bit in <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static bool BitIndexCheck<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => !TValue.IsZero(value & TValue.One << index);

    /// <summary>
    /// <para>Clear a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static TValue BitIndexClear<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value & ~(TValue.One << index);

    /// <summary>
    /// <para>Flip a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static TValue BitIndexFlip<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value ^ TValue.One << index;

    /// <summary>
    /// <para>Gets the bit-index of a power-of-2 number.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber BitIndexOfPow2<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsPow2(source) ? TNumber.Log2(source) : throw new System.ArgumentOutOfRangeException(nameof(source), "Not a power-of-2 number.");

    /// <summary>
    /// <para>Set a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static TValue BitIndexSet<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value | TValue.One << index;
  }
}
