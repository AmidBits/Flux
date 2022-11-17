//namespace Flux
//{
//  // <seealso cref="http://aggregate.org/MAGIC/"/>
//  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

//  public static partial class BitOps
//  {
//    // The fold 'left' (or up towards MSB) function, is the opposite of (<see cref="FoldRight"/>), sets all bits from LS1B and 'up' (or 'left'), to 1.

//    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
//    /// <returns>All bits set from LSB up, or -1 if the value is less than zero.</returns>
//    public static System.Numerics.BigInteger FoldLeft(System.Numerics.BigInteger value)
//    {
//      if (value < 0) return -1;
//      //var values = value.ToString("X4");

//      var lzc = LeadingZeroCount(value);
//      var tzc = TrailingZeroCount(value);

//      //var v = (long)value;
//      //var vo = v.ToRadixString(2);
//      //var s0 = v << lzc;
//      //var v0 = s0.ToRadixString(2);
//      //var s1 = FoldRight(s0);
//      //var v1 = s1.ToRadixString(2);
//      //var s2 = s1 >> tzc;
//      //var v2 = s2.ToRadixString(2);
//      //var s3 = s2 << tzc;
//      //var v3 = s3.ToRadixString(2);

//      //var l3 = (long)s3;

//      return FoldRight(value << lzc) >> tzc << tzc;

//      //var fr = FoldRight(value << lzc) >> tzc << tzc;
//      //var frs = fr.ToString("X4");

//      //var i = (uint)fr;

//      //      return fr;

//      //return FoldRight(value) >> tzc << tzc; // Original code.
//    }

//    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
//    /// <returns>Returns all ones from the LSB up.</returns>
//    public static int FoldLeft(int value)
//      => unchecked((int)FoldLeft((uint)value));

//    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
//    /// <returns>Returns all ones from the LSB up.</returns>
//    public static long FoldLeft(long value)
//      => unchecked((long)FoldLeft((ulong)value));

//    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
//    /// <returns>Returns all ones from the LSB up.</returns>
//    [System.CLSCompliant(false)]
//    public static uint FoldLeft(uint value)
//    {
//      if (value != 0)
//      {
//        value |= value << 1;
//        value |= value << 2;
//        value |= value << 4;
//        value |= value << 8;
//        value |= value << 16;
//      }

//      return value;
//    }

//    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
//    /// <returns>Returns all ones from the LSB up.</returns>
//    [System.CLSCompliant(false)]
//    public static ulong FoldLeft(ulong value)
//    {
//      if (value != 0)
//      {
//        value |= value << 1;
//        value |= value << 2;
//        value |= value << 4;
//        value |= value << 8;
//        value |= value << 16;
//        value |= value << 32;
//      }

//      return value;
//    }
//  }
//}
