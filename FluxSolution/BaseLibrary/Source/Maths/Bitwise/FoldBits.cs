using System.Linq;

// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Maths
  {
    private static System.Collections.Generic.IReadOnlyList<int>? m_byteFoldBits;
    /// <summary></summary>
    public static System.Collections.Generic.IReadOnlyList<int> ByteFoldBits
      => m_byteFoldBits ??= System.Linq.Enumerable.Range(0, 256).Select(n => FoldBits(n)).ToList();

    /// <summary>"Folds" the upper bits into the lower bits, by taking the most significant 1 bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most significant 1 as the value, but all 1's below it.</summary>
    /// <returns>Returns all ones from the MSB down.</returns>
    public static System.Numerics.BigInteger FoldBits(System.Numerics.BigInteger value)
      => (System.Numerics.BigInteger.One << BitLength(value)) - 1;

    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
    /// <returns>Returns all ones from the MSB down.</returns>
    public static int FoldBits(int value)
    {
      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);

      return value;
    }
    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
    /// <returns>Returns all ones from the MSB down.</returns>
    public static long FoldBits(long value)
    {
      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      value |= (value >> 32);

      return value;
    }

    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
    /// <returns>Returns all ones from the MSB down.</returns>
    [System.CLSCompliant(false)]
    public static uint FoldBits(uint value)
    {
      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);

      return value;
    }
    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
    /// <returns>Returns all ones from the MSB down.</returns>
    [System.CLSCompliant(false)]
    public static ulong FoldBits(ulong value)
    {
      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      value |= (value >> 32);

      return value;
    }
  }
}
