//namespace Flux
//{
//  public static partial class Maths
//  {
//    /// <summary>Determines whether the number is a perfect square.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
//    public static bool IsPerfectSquare(System.Numerics.BigInteger value)
//      => ISqrt(value) is var sqrt && sqrt * sqrt == value;

//    /// <summary>Determines whether the number is a perfect square.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
//    public static bool IsPerfectSquare(int value)
//      => (0x02030213 & (1 << (int)(value & 31))) > 0 && (int)System.Math.Sqrt(value) is var sqrt && sqrt * sqrt == value;
//    /// <summary>Determines whether the number is a perfect square.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
//    public static bool IsPerfectSquare(long value)
//      => (0x202021202030213L & (1L << (int)(value & 63))) > 0 && (long)System.Math.Sqrt(value) is var sqrt && sqrt * sqrt == value;

//    /// <summary>Determines whether the number is a perfect square.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
//    [System.CLSCompliant(false)]
//    public static bool IsPerfectSquare(uint value)
//      => (0x02030213U & (1U << (int)(value & 31))) > 0 && (uint)System.Math.Sqrt(value) is var sqrt && sqrt * sqrt == value;
//    /// <summary>Determines whether the number is a perfect square.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
//    [System.CLSCompliant(false)]
//    public static bool IsPerfectSquare(ulong value)
//      => (0x202021202030213UL & (1UL << (int)(value & 63))) > 0 && (ulong)System.Math.Sqrt(value) is var sqrt && sqrt * sqrt == value;
//  }
//}
