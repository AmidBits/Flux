namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    // The fold 'right' (or down towards LSB) function, is the opposite (<see cref="FoldLeft"/>), sets all bits from the MS1B bit 'down' (or 'right'), to 1.
    /// <summary>"Folds" the upper bits into the lower bits, by taking the most significant 1 bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most significant 1 as the value, but all 1's below it.</summary>
    /// <returns>All bits set from MSB down, or -1 (all bits) if the value is less than zero.</returns>
    public static TSelf BitFoldRight<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsNegative(value)
      ? -TSelf.One
      : TSelf.IsZero(value)
      ? TSelf.Zero
      : (((MostSignificant1Bit(value) - TSelf.One) << 1) | TSelf.One);

#else

    // The fold 'right' (or down towards LSB) function, is the opposite (<see cref="FoldLeft"/>), sets all bits from the MS1B bit 'down' (or 'right'), to 1.
    /// <summary>"Folds" the upper bits into the lower bits, by taking the most significant 1 bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most significant 1 as the value, but all 1's below it.</summary>
    /// <returns>All bits set from MSB down, or -1 (all bits) if the value is less than zero.</returns>
    public static System.Numerics.BigInteger BitFoldRight(this System.Numerics.BigInteger value)
      => value < System.Numerics.BigInteger.Zero
      ? -System.Numerics.BigInteger.One
      : value.IsZero
      ? System.Numerics.BigInteger.Zero
      : (((value.MostSignificant1Bit() - System.Numerics.BigInteger.One) << 1) | System.Numerics.BigInteger.One);

    public static int BitFoldRight(this int value) => unchecked((int)((uint)value).BitFoldRight());
    public static long BitFoldRight(this long value) => unchecked((long)((ulong)value).BitFoldRight());

    [System.CLSCompliant(false)] public static uint BitFoldRight(this uint value) => value == 0 ? 0 : (((MostSignificant1Bit(value) - 1) << 1) | 1);
    [System.CLSCompliant(false)] public static ulong BitFoldRight(this ulong value) => value == 0 ? 0 : (((MostSignificant1Bit(value) - 1) << 1) | 1);

#endif
  }
}
