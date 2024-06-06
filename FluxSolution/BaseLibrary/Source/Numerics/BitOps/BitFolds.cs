namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>
    /// <para>Recursively "folds" all 1-bits into upper (left) 0-bits, ending with top (left) bits (from LS1B on) set to 1.</para>
    /// <para>The process yields a bit vector with the same least-significant-1-bit as <paramref name="value"/>, and all 1's above it.</para>
    /// </summary>
    /// <returns>All bits set from LSB up, or -1 if the value is less than zero.</returns>
    public static TSelf BitFoldLeft<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsZero(value)) return value;

      var tzc = value.GetTrailingZeroCount();

      return BitFoldRight(value << value.GetLeadingZeroCount()) >> tzc << tzc;
    }

    /// <summary>
    /// <para>Recursively "folds" all 1-bits into lower (right) 0-bits, by taking the most-significant-1-bit (MS1B) and OR it with (MS1B - 1), ending with bottom (right) bits (from MS1B on) set to 1.</para>
    /// <para>The process yields a bit vector with the same most-significant-1-bit as <paramref name="value"/>, and all 1's below it.</para>
    /// </summary>
    /// <returns>All bits set from MSB down, or -1 (all bits) if the value is less than zero.</returns>
    public static TSelf BitFoldRight<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(value)
      ? value
      : (((value.MostSignificant1Bit() - TSelf.One) << 1) | TSelf.One);
  }
}
