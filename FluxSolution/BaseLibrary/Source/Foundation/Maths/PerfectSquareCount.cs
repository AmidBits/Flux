namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the count of perfect squares between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    public static System.Numerics.BigInteger PerfectSquareCount(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
      => ISqrt(b) - (IsPerfectSquare(a) ? ISqrt(a) : ISqrt(a) + 1) + 1;

    /// <summary>Returns the count of perfect squares between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    public static int PerfectSquareCount(int a, int b)
      => (int)System.Math.Floor(System.Math.Sqrt(b)) - (int)System.Math.Ceiling(System.Math.Sqrt(a)) + 1;
    /// <summary>Returns the count of perfect squares between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    public static long PerfectSquareCount(long a, long b)
      => (long)System.Math.Floor(System.Math.Sqrt(b)) - (long)System.Math.Ceiling(System.Math.Sqrt(a)) + 1;

    /// <summary>Returns the count of perfect squares between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    [System.CLSCompliant(false)]
    public static uint PerfectSquareCount(uint a, uint b)
      => (uint)System.Math.Floor(System.Math.Sqrt(b)) - (uint)System.Math.Ceiling(System.Math.Sqrt(a)) + 1;
    /// <summary>Returns the count of perfect squares between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    [System.CLSCompliant(false)]
    public static ulong PerfectSquareCount(ulong a, ulong b)
      => (ulong)System.Math.Floor(System.Math.Sqrt(b)) - (ulong)System.Math.Ceiling(System.Math.Sqrt(a)) + 1;
  }
}