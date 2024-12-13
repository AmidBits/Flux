namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Clear a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static TValue BitIndexClear<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value &= ~(TValue.One << index);

    /// <summary>
    /// <para>Flip a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static TValue BitIndexFlip<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value ^= TValue.One << index;

    /// <summary>
    /// <para>Determine the state of a bit in <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static bool BitIndexGet<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => !TValue.IsZero(value &= TValue.One << index);

    /// <summary>
    /// <para>Set a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static TValue BitIndexSet<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value |= TValue.One << index;
  }
}
