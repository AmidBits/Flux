// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Computes the next power of 2 greater than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the result will always be greater than value. If false, it could be greater than or equal to value.</param>
    public static System.Numerics.BigInteger NextPowerOf2(System.Numerics.BigInteger value, bool strictlyGreaterThan)
      => value > 0 ? unchecked(System.Numerics.BigInteger.One << (Log2(strictlyGreaterThan ? value : value - 1) + 1)) : 0;

    /// <summary>Computes the next power of 2 greater than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the result will always be greater than value. If false, it could be greater than or equal to value.</param>
    public static int NextPowerOf2(int value, bool strictlyGreaterThan)
      => unchecked((int)NextPowerOf2((uint)value, strictlyGreaterThan));
    /// <summary>Computes the next power of 2 greater than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the result will always be greater than value. If false, it could be greater than or equal to value.</param>
    public static long NextPowerOf2(long value, bool strictlyGreaterThan)
      => unchecked((long)NextPowerOf2((ulong)value, strictlyGreaterThan));

    /// <summary>Computes the next power of 2 greater than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the result will always be greater than value. If false, it could be greater than or equal to value.</param>
    [System.CLSCompliant(false)]
    public static uint NextPowerOf2(uint value, bool strictlyGreaterThan)
    {
      if (!strictlyGreaterThan) 
        value--;

      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);

			return value + 1;
    }
    /// <summary>Computes the next power of 2 greater than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the result will always be greater than value. If false, it could be greater than or equal to value.</param>
    [System.CLSCompliant(false)]
    public static ulong NextPowerOf2(ulong value, bool strictlyGreaterThan)
    {
      if (!strictlyGreaterThan) 
        value--;

      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      value |= (value >> 32);

			return value + 1;
    }
  }
}
