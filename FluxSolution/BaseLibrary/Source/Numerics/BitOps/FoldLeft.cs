namespace Flux.Numerics
{
  public static partial class BitOps
  {
    /// The fold 'left' (or up towards MSB) function is the opposite of the recursive fold of all bits right (<see cref="FoldRight"/> ), until all bits, lower than the MS1B, will have been set to 1.

    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    public static System.Numerics.BigInteger FoldLeft(System.Numerics.BigInteger value)
    {
      var tzc = TrailingZeroCount(value);

      return FoldRight(value) >> tzc << tzc;
    }

    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    public static int FoldLeft(int value)
      => unchecked((int)FoldLeft((uint)value));
    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    public static long FoldLeft(long value)
      => unchecked((long)FoldLeft((ulong)value));

    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    [System.CLSCompliant(false)]
    public static void FoldLeft(ref uint value)
    {
      if (value != 0)
      {
        value |= (value << 1);
        value |= (value << 2);
        value |= (value << 4);
        value |= (value << 8);
        value |= (value << 16);
      }
    }
    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    [System.CLSCompliant(false)]
    public static uint FoldLeft(uint value)
    {
      FoldLeft(ref value);
      return value;
    }

    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    [System.CLSCompliant(false)]
    public static void FoldLeft(ref ulong value)
    {
      if (value != 0)
      {
        value |= (value << 1);
        value |= (value << 2);
        value |= (value << 4);
        value |= (value << 8);
        value |= (value << 16);
        value |= (value << 32);
      }
    }
    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>Returns all ones from the LSB up.</returns>
    [System.CLSCompliant(false)]
    public static ulong FoldLeft(ulong value)
    {
      FoldLeft(ref value);
      return value;
    }
  }
}