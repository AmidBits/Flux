//using Flux.Hashing;
//using System.Numerics;

using Flux.AmbOps;
using Flux.Hashing;

namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class ExtensionMethods
  {
    //public static TSelf Sign2<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ISignedNumber<TSelf>
    //  => value < TSelf.Zero ? TSelf.NegativeOne : TSelf.One;
    public static TSelf Sign<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ISignedNumber<TSelf>
      => value < TSelf.Zero ? TSelf.NegativeOne : value > TSelf.Zero ? TSelf.One : TSelf.Zero;

    public static TSelf CopySign<TSelf>(this TSelf value, TSelf sign)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ISignedNumber<TSelf>
      => TSelf.Abs(value) * sign.Sign();
    
    /// <summary>PREVIEW! Finds the bit index of a power-of-2 value (i.e. only a single bit can be set to 1).</summary>
    public static int BitIndex<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => IsPowerOf2(value)
      ? ILog2(value)
      : -1;

    /// <summary>PREVIEW! Returns the count of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>The number of bits needed to represent the number, if value is positive. If value is negative then -1. A value of zero needs 0 bits.</remarks>
    public static int BitLength<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value > TSelf.Zero ? ILog2(value) + 1
      : value < TSelf.Zero ? -1
      : 0;

    // The fold 'left' (or up towards MSB) function, is the opposite of (<see cref="FoldRight"/>), sets all bits from LS1B and 'up' (or 'left'), to 1.
    /// <summary>PREVIEW! Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>All bits set from LSB up, or -1 if the value is less than zero.</returns>
    public static TSelf FoldLeft<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (value < TSelf.Zero) return -TSelf.One;
      //      TSelf.Count
      //var x =       TSelf.One << value.GetShortestBitLength();

      var tzc = TrailingZeroCount(value);

      value <<= LeadingZeroCount(value);
      value = FoldRight(value);
      value >>= tzc;
      value <<= tzc;
      return value;
    }

    // The fold 'right' (or down towards LSB) function, is the opposite (<see cref="FoldLeft"/>), sets all bits from the MS1B bit 'down' (or 'right'), to 1.
    /// <summary>PREVIEW! "Folds" the upper bits into the lower bits, by taking the most significant 1 bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most significant 1 as the value, but all 1's below it.</summary>
    /// <returns>All bits set from MSB down, or -1 if the value is less than zero.</returns>
    public static TSelf FoldRight<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value < TSelf.Zero ? -TSelf.One
      : value > TSelf.Zero ? (TSelf.One << value.GetShortestBitLength()) - TSelf.One
      : TSelf.Zero;

    /// <summary>PREVIEW! Computes the smallest power of 2 storage size, that is greater or equal to <paramref name="startingStorageSizeInPowerOf2"/>, that would fit the value.</summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="startingStorageSizeInPowerOf2"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf GetSmallestPowerOf2StorageSize<TSelf>(this TSelf value, TSelf startingStorageSizeInPowerOf2)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (!TSelf.IsPow2(startingStorageSizeInPowerOf2)) throw new System.ArgumentOutOfRangeException(nameof(startingStorageSizeInPowerOf2), "Must be a power of 2.");

      while (startingStorageSizeInPowerOf2 < value)
        startingStorageSizeInPowerOf2 <<= 1;

      return startingStorageSizeInPowerOf2;
    }

    /// <summary>PREVIEW! Computes the floor or ceiling (depending on the <paramref name="ceiling"/> argument) of the base 2 log of the value.</summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="ceiling"></param>
    /// <returns></returns>
    public static int ILog2<TSelf>(this TSelf value, bool ceiling = false)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value <= TSelf.Zero ? 0 : value.GetShortestBitLength() is var log2 && (IsPowerOf2(value) || !ceiling) ? log2 - 1 : log2;

    /// <summary>PREVIEW! Determines if the number is a power of 2. A non-negative binary integer value x is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</summary>
    /// <remarks>The implementation is extremely fast for huge BigInteger values.</remarks>
    public static bool IsPowerOf2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>, System.Numerics.IBitwiseOperators<TSelf, TSelf, TSelf>
      => value > TSelf.Zero && (value & (value - TSelf.One)) == TSelf.Zero;

    /// <summary>PREVIEW! Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary this version finds and subtracts from the nearest power-of-two bit-length that the value fits in.</remarks>
    public static int LeadingZeroCount<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (value.GetByteCount() * 8) - ILog2(MostSignificant1Bit(value)) - 1;

    /// <summary>PREVIEW! Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    public static TSelf LeastSignificant1Bit<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>, System.Numerics.IBitwiseOperators<TSelf, TSelf, TSelf>
      => value & ((~value) + TSelf.One);

    /// <summary>PREVIEW! Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    public static TSelf MostSignificant1Bit<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.One << ILog2(value);

    /// <summary>PREVIEW! Find the next smaller power of 2 that is less than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always less than value, othewise it could be less than or equal to.</param>
    /// <returns>The smaller power of 2 that is less than (or equal to, depending on the proper flag).</returns>
    public static TSelf RoundDownToPowerOf2<TSelf>(this TSelf value, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : FoldRight(value - TSelf.One) + TSelf.One >> 1;

    /// <summary>PREVIEW! Computes the smaller and larger power of 2, as well as the nearest of the two power of 2 values computed.</summary>
    /// <param name="value"></param>
    /// <param name="proper">When true, the nearest power of 2 is truly greater or less than, when false, it's greater or less than OR EQUAL.</param>
    /// <param name="greaterThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
    /// <param name="lessThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
    /// <returns>The nearest power of 2, and both greater than and less than powers of 2.</returns>
    public static TSelf RoundToNearestPowerOf2<TSelf>(this TSelf value, bool proper, out TSelf greaterThan, out TSelf lessThan)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (IsPowerOf2(value))
      {
        greaterThan = (proper ? value << 1 : value);
        lessThan = (proper ? value >> 1 : value);
      }
      else
      {
        greaterThan = FoldRight(value - TSelf.One) + TSelf.One;
        lessThan = greaterThan >> 1;
      }

      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
    }

    /// <summary>PREVIEW! Find the next larger power of 2 that is greater than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always greater than value, othewise it could be greater than or equal to.</param>
    /// <returns>The larger power of 2 that is greater than (or equal to, depending on the proper flag).</returns>
    public static TSelf RoundUpToPowerOf2<TSelf>(this TSelf value, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : FoldRight(value - TSelf.One) + TSelf.One;

    /// <summary>PREVIEW! Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    public static int TrailingZeroCount<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ILog2(LeastSignificant1Bit(value));
  }
}
