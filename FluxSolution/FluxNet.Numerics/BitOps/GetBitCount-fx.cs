namespace FluxNet.Numerics
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Returns the size, in number of bits, needed to store <paramref name="value"/>.</para>
    /// <para>Most types returns the underlying storage size of the type itself, e.g. <see langword="int"/> = 32 or <see langword="long"/> = 64.</para>
    /// </summary>
    /// <remarks>
    /// <para>Some data types, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetBitCount{TValue}"/> dynamic, and depends on the actual number stored.</para>
    /// </remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int GetBitCount<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value.GetByteCount() * 8;
  }
}
