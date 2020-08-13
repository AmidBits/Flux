namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the count of perfect squares between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    public static System.Numerics.BigInteger PerfectSquareCountBetween(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
      => b.Sqrt() - (IsPerfectSquare(a) ? a.Sqrt() : a.Sqrt() + 1) + 1;

    /// <summary>Returns the count of perfect squares between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    public static int PerfectSquareCountBetween(int a, int b)
      => (int)System.Math.Floor(System.Math.Sqrt(b)) - (int)System.Math.Ceiling(System.Math.Sqrt(a)) + 1;
    /// <summary>Returns the count of perfect squares between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    public static long PerfectSquareCountBetween(long a, long b)
      => (long)System.Math.Floor(System.Math.Sqrt(b)) - (long)System.Math.Ceiling(System.Math.Sqrt(a)) + 1;

    /// <summary>Returns the count of perfect squares between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    [System.CLSCompliant(false)]
    public static uint PerfectSquareCountBetween(uint a, uint b)
      => (uint)System.Math.Floor(System.Math.Sqrt(b)) - (uint)System.Math.Ceiling(System.Math.Sqrt(a)) + 1;
    /// <summary>Returns the count of perfect squares between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    [System.CLSCompliant(false)]
    public static ulong PerfectSquareCountBetween(ulong a, ulong b)
      => (ulong)System.Math.Floor(System.Math.Sqrt(b)) - (ulong)System.Math.Ceiling(System.Math.Sqrt(a)) + 1;
  }
}
