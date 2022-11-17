//namespace Flux
//{
//  public static partial class BitOps
//  {
//    /// <summary>Computes the smaller and larger power of 2, as well as the nearest of the two power of 2 values computed.</summary>
//    /// <param name="value"></param>
//    /// <param name="proper">When true, the nearest power of 2 is truly greater or less than, when false, it's greater or less than OR EQUAL.</param>
//    /// <param name="greaterThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
//    /// <param name="lessThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
//    /// <returns>The nearest power of 2, and both greater than and less than powers of 2.</returns>
//    public static System.Numerics.BigInteger RoundToNearestPowerOf2(System.Numerics.BigInteger value, bool proper, out System.Numerics.BigInteger greaterThan, out System.Numerics.BigInteger lessThan)
//    {
//      if (IsPowerOf2(value))
//      {
//        greaterThan = (proper ? value << 1 : value);
//        lessThan = (proper ? value >> 1 : value);
//      }
//      else
//      {
//        greaterThan = FoldRight(value - 1) + 1;
//        lessThan = greaterThan >> 1;
//      }

//      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
//    }

//    /// <summary>Computes the smaller and larger power of 2, as well as the nearest of the two power of 2 values computed.</summary>
//    /// <param name="value"></param>
//    /// <param name="proper">When true, the nearest power of 2 is truly greater or less than, when false, it's greater or less than OR EQUAL.</param>
//    /// <param name="greaterThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
//    /// <param name="lessThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
//    /// <returns>The nearest power of 2, and both greater than and less than powers of 2.</returns>
//    public static int RoundToNearestPowerOf2(int value, bool proper, out int greaterThan, out int lessThan)
//    {
//      if (IsPowerOf2(value))
//      {
//        greaterThan = (proper ? value << 1 : value);
//        lessThan = (proper ? value >> 1 : value);
//      }
//      else
//      {
//        greaterThan = FoldRight(value - 1) + 1;
//        lessThan = greaterThan >> 1;
//      }

//      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
//    }

//    /// <summary>Computes the smaller and larger power of 2, as well as the nearest of the two power of 2 values computed.</summary>
//    /// <param name="value"></param>
//    /// <param name="proper">When true, the nearest power of 2 is truly greater or less than, when false, it's greater or less than OR EQUAL.</param>
//    /// <param name="greaterThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
//    /// <param name="lessThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
//    /// <returns>The nearest power of 2, and both greater than and less than powers of 2.</returns>
//    public static long RoundToNearestPowerOf2(long value, bool proper, out long greaterThan, out long lessThan)
//    {
//      if (IsPowerOf2(value))
//      {
//        greaterThan = (proper ? value << 1 : value);
//        lessThan = (proper ? value >> 1 : value);
//      }
//      else
//      {
//        greaterThan = FoldRight(value - 1) + 1;
//        lessThan = greaterThan >> 1;
//      }

//      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
//    }

//    /// <summary>Computes the smaller and larger power of 2, as well as the nearest of the two power of 2 values computed.</summary>
//    /// <param name="value"></param>
//    /// <param name="proper">When true, the nearest power of 2 is truly greater or less than, when false, it's greater or less than OR EQUAL.</param>
//    /// <param name="greaterThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
//    /// <param name="lessThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
//    /// <returns>The nearest power of 2, and both greater than and less than powers of 2.</returns>
//    [System.CLSCompliant(false)]
//    public static uint RoundToNearestPowerOf2(uint value, bool proper, out uint greaterThan, out uint lessThan)
//    {
//      if (IsPowerOf2(value))
//      {
//        greaterThan = (proper ? value << 1 : value);
//        lessThan = (proper ? value >> 1 : value);
//      }
//      else
//      {
//        greaterThan = FoldRight(value - 1) + 1;
//        lessThan = greaterThan >> 1;
//      }

//      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
//    }

//    /// <summary>Computes the smaller and larger power of 2, as well as the nearest of the two power of 2 values computed.</summary>
//    /// <param name="value"></param>
//    /// <param name="proper">When true, the nearest power of 2 is truly greater or less than, when false, it's greater or less than OR EQUAL.</param>
//    /// <param name="greaterThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
//    /// <param name="lessThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
//    /// <returns>The nearest power of 2, and both greater than and less than powers of 2.</returns>
//    [System.CLSCompliant(false)]
//    public static ulong RoundToNearestPowerOf2(ulong value, bool proper, out ulong greaterThan, out ulong lessThan)
//    {
//      if (IsPowerOf2(value))
//      {
//        greaterThan = (proper ? value << 1 : value);
//        lessThan = (proper ? value >> 1 : value);
//      }
//      else
//      {
//        greaterThan = FoldRight(value - 1) + 1;
//        lessThan = greaterThan >> 1;
//      }

//      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
//    }
//  }
//}
