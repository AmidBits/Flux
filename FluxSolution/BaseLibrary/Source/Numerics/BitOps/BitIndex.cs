namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Clear a bit of <paramref name="source"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static TSelf ClearBitIndex<TSelf>(this TSelf source, int index)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => source &= ~(TSelf.One << index);

    /// <summary>
    /// <para>Flip a bit of <paramref name="source"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static TSelf FlipBitIndex<TSelf>(this TSelf source, int index)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => source ^= TSelf.One << index;

    /// <summary>
    /// <para>Determine the state of a bit in <paramref name="source"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static bool GetBitIndex<TSelf>(this TSelf source, int index)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (source &= TSelf.One << index) != TSelf.Zero;

    /// <summary>
    /// <para>Set a bit of <paramref name="source"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    public static TSelf SetBitIndex<TSelf>(this TSelf source, int index)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => source |= TSelf.One << index;
  }
}
