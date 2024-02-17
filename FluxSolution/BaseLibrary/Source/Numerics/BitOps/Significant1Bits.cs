namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
    /// </summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TSelf ClearLeastSignificant1Bit<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value & (value - TSelf.One);

    /// <summary>
    /// <para>Extracts the highest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the most-significant-1-bit.</para>
    /// <list type="bullet">
    /// <item>If <paramref name="value"/> equal zero, zero is returned.</item>
    /// <item>If <paramref name="value"/> is negative, min-value of the signed type is returned (i.e. the top most-significant-bit that the type is able to represent).</item>
    /// <item>Otherwise the most-significant-1-bit is returned, which also happens to be the same as Log2(<paramref name="value"/>).</item>
    /// </list>
    /// </summary>
    /// <remarks>Note that for dynamic types, e.g. <see cref="System.Numerics.BigInteger"/>, the number of bits depends on the storage size used for the <paramref name="value"/>.</remarks>
    public static TSelf MostSignificant1Bit<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(value) ? value : TSelf.One << (value.GetBitLengthEx() - 1);

    /// <summary>
    /// <para>Extracts the lowest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the least-significant-1-bit.</para>
    /// </summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TSelf LeastSignificant1Bit<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value & ((~value) + TSelf.One);
  }
}
