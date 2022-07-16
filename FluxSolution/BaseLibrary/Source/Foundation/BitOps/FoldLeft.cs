namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    // The fold 'left' (or up towards MSB) function, is the opposite of (<see cref="FoldRight"/>), sets all bits from LS1B and 'up' (or 'left'), to 1.

    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    public static System.Numerics.BigInteger FoldLeft(this System.Numerics.BigInteger value)
    {
      var tzc = TrailingZeroCount(value);

      return FoldRight(value) >> tzc << tzc;
    }

    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    public static int FoldLeft(this int value)
      => unchecked((int)FoldLeft((uint)value));

    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    public static long FoldLeft(this long value)
      => unchecked((long)FoldLeft((ulong)value));

    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    [System.CLSCompliant(false)]
    public static uint FoldLeft(this uint value)
    {
      if (value != 0)
      {
        value |= value << 1;
        value |= value << 2;
        value |= value << 4;
        value |= value << 8;
        value |= value << 16;
      }

      return value;
    }

    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    [System.CLSCompliant(false)]
    public static ulong FoldLeft(this ulong value)
    {
      if (value != 0)
      {
        value |= value << 1;
        value |= value << 2;
        value |= value << 4;
        value |= value << 8;
        value |= value << 16;
        value |= value << 32;
      }

      return value;
    }
  }
}
