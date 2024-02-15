namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>
    /// <para>Recursively "folds" the lower bits into the upper bits (left) from the least-significant-1-bit. The process yields a bit vector with the same least-significant-1-bit as the value, and all 1's above it.</para>
    /// </summary>
    /// <returns>All bits set from LSB up, or -1 if the value is less than zero.</returns>
    public static TSelf BitFoldLeft<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var tzc = value.GetTrailingZeroCount();

      return BitFoldRight(value << value.GetLeadingZeroCount()) >> tzc << tzc;
    }

    /// <summary>
    /// <para>"Folds" the upper bits into the lower bits, by taking the most-significant-1-bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most-significant-1-bit as the value, and all 1's below it.</para>
    /// </summary>
    /// <returns>All bits set from MSB down, or -1 (all bits) if the value is less than zero.</returns>
    public static TSelf BitFoldRight<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(value)
      ? TSelf.Zero
      : (((value.MostSignificant1Bit() - TSelf.One) << 1) | TSelf.One);
  }
}
