namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Clear a bit of <paramref name="source"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static TSelf BitIndexClear<TSelf>(this TSelf source, int index)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => source &= ~(TSelf.One << index);

    /// <summary>
    /// <para>Flip a bit of <paramref name="source"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static TSelf BitIndexFlip<TSelf>(this TSelf source, int index)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => source ^= TSelf.One << index;

    /// <summary>
    /// <para>Determine the state of a bit in <paramref name="source"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static bool BitIndexGet<TSelf>(this TSelf source, int index)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (source &= TSelf.One << index) != TSelf.Zero;

    /// <summary>
    /// <para>Set a bit of <paramref name="source"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static TSelf BitIndexSet<TSelf>(this TSelf source, int index)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => source |= TSelf.One << index;
  }
}
