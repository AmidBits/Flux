namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>"Folds" the upper bits into the lower bits, by taking the most significant 1 bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most significant 1 as the value, but all 1's below it.</summary>
    /// <returns>All bits set from MSB down, or -1 (all bits) if the value is less than zero.</returns>
    public static TSelf BitFoldRight<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(value) ? TSelf.Zero : (((value.GetMostSignificant1Bit() - TSelf.One) << 1) | TSelf.One);

#else

    /// <summary>"Folds" the upper bits into the lower bits, by taking the most significant 1 bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most significant 1 as the value, but all 1's below it.</summary>
    /// <returns>All bits set from MSB down, or -1 (all bits) if the value is less than zero.</returns>
    public static System.Numerics.BigInteger BitFoldRight(this System.Numerics.BigInteger value)
      => value.IsZero ? System.Numerics.BigInteger.Zero : (((value.GetMostSignificant1Bit() - System.Numerics.BigInteger.One) << 1) | System.Numerics.BigInteger.One);

    public static int BitFoldRight(this int value)
      => unchecked((int)((uint)value).BitFoldRight());
    public static long BitFoldRight(this long value)
      => unchecked((long)((ulong)value).BitFoldRight());

    [System.CLSCompliant(false)]
    public static uint BitFoldRight(this uint value)
    {
      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      return value;
    }

    [System.CLSCompliant(false)]
    public static ulong BitFoldRight(this ulong value)
    {
      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      value |= (value >> 32);
      return value;
    }

#endif
  }
}
