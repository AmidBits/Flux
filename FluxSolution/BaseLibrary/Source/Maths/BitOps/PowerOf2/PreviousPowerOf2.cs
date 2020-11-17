// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Computes the next power of 2 greater than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the result will always be greater than value. If false, it could be greater than or equal to value.</param>
    public static System.Numerics.BigInteger PreviousPowerOf2(System.Numerics.BigInteger value, bool strictlyLessThan)
      => value > 1 ? unchecked(System.Numerics.BigInteger.One << Log2(strictlyLessThan ? value - 1 : value)) : 0;

    /// <summary>Computes the previous power of 2 less than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the power of 2 will always be less than value. If false, it could be the same as value.</param>
    public static int PreviousPowerOf2(int value, bool strictlyLessThan)
      => unchecked((int)PreviousPowerOf2((uint)value, strictlyLessThan));
    /// <summary>Computes the previous power of 2 less than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the power of 2 will always be less than value. If false, it could be the same as value.</param>
    public static long PreviousPowerOf2(long value, bool strictlyLessThan)
      => unchecked((long)PreviousPowerOf2((ulong)value, strictlyLessThan));

    /// <summary>Computes the previous power of 2 less than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the power of 2 will always be less than value. If false, it could be the same as value.</param>
    [System.CLSCompliant(false)]
    public static uint PreviousPowerOf2(uint value, bool strictlyLessThan)
    {
      if (strictlyLessThan) 
        value--;

      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);

      return value - (value >> 1);
    }
    /// <summary>Computes the previous power of 2 less than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the power of 2 will always be less than value. If false, it could be the same as value.</param>
    [System.CLSCompliant(false)]
    public static ulong PreviousPowerOf2(ulong value, bool strictlyLessThan)
    {
      if (strictlyLessThan) 
        value--;

      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      value |= (value >> 32);

      return value - (value >> 1);
    }
  }
}
