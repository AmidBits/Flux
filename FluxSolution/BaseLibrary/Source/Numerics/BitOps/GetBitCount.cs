namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Returns the storage size, in number of bits, needed for the current <paramref name="value"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>Note that some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetBitCount{TSelf}"/> dynamic also.</para>
    /// </remarks>
    public static int GetBitCount<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.GetByteCount() * 8;
  }
}
