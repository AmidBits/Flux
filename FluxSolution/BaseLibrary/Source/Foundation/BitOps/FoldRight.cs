namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    // The fold 'right' (or down towards LSB) function, is the opposite (<see cref="FoldLeft"/>), sets all bits from the MS1B bit 'down' (or 'right'), to 1.

    /// <summary>"Folds" the upper bits into the lower bits, by taking the most significant 1 bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most significant 1 as the value, but all 1's below it.</summary>
    /// <returns>Returns all ones from the MSB down.</returns>
    public static System.Numerics.BigInteger FoldRight(System.Numerics.BigInteger value)
      => (System.Numerics.BigInteger.One << BitLength(value)) - 1;

    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
    /// <returns>Returns all ones from the MSB down.</returns>
    public static int FoldRight(int value)
      => value < 0
      ? unchecked((int)uint.MaxValue)
      : unchecked((int)FoldRight((uint)value));
    
    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
    /// <returns>Returns all ones from the MSB down.</returns>
    public static long FoldRight(long value)
      => value < 0
      ? unchecked((long)ulong.MaxValue)
      : unchecked((long)FoldRight((ulong)value));

    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
    /// <returns>Returns all ones from the MSB down.</returns>
    [System.CLSCompliant(false)]
    public static uint FoldRight(uint value)
    {
      if (value != 0)
      {
        value |= value >> 1;
        value |= value >> 2;
        value |= value >> 4;
        value |= value >> 8;
        value |= value >> 16;
      }

      return value;
    }

    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
    /// <returns>Returns all ones from the MSB down.</returns>
    [System.CLSCompliant(false)]
    public static ulong FoldRight(ulong value)
    {
      if (value != 0)
      {
        value |= value >> 1;
        value |= value >> 2;
        value |= value >> 4;
        value |= value >> 8;
        value |= value >> 16;
        value |= value >> 32;
      }

      return value;
    }
  }
}
