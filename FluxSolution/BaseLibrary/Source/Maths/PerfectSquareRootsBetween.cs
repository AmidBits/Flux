namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Creates a sequence of "perfect" roots (of perfect squares) between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> PerfectSquareRootsBetween(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
    {
      for (System.Numerics.BigInteger root = IsPerfectSquare(a) ? ISqrt(a) : ISqrt(a) + 1, maxRoot = ISqrt(b) + 1; root < maxRoot; root++)
        yield return root;
    }

    /// <summary>Creates a sequence of "perfect" roots (of perfect squares) between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    public static System.Collections.Generic.IEnumerable<int> PerfectSquareRootsBetween(int a, int b)
    {
      for (int root = (int)System.Math.Floor(System.Math.Sqrt(a)), maxRoot = (int)System.Math.Floor(System.Math.Sqrt(b)) + 1; root < maxRoot; root++)
        yield return root;
    }
    /// <summary>Creates a sequence of "perfect" roots (of perfect squares) between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    public static System.Collections.Generic.IEnumerable<long> PerfectSquareRootsBetween(long a, long b)
    {
      for (long root = (long)System.Math.Floor(System.Math.Sqrt(a)), maxRoot = (long)System.Math.Floor(System.Math.Sqrt(b)) + 1; root < maxRoot; root++)
        yield return root;
    }

    /// <summary>Creates a sequence of "perfect" roots (of perfect squares) between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<uint> PerfectSquareRootsBetween(uint a, uint b)
    {
      for (uint root = (uint)System.Math.Floor(System.Math.Sqrt(a)), maxRoot = (uint)System.Math.Floor(System.Math.Sqrt(b)) + 1; root < maxRoot; root++)
        yield return root;
    }
    /// <summary>Creates a sequence of "perfect" roots (of perfect squares) between the two specified numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<ulong> PerfectSquareRootsBetween(ulong a, ulong b)
    {
      for (ulong root = (ulong)System.Math.Floor(System.Math.Sqrt(a)), maxRoot = (ulong)System.Math.Floor(System.Math.Sqrt(b)) + 1; root < maxRoot; root++)
        yield return root;
    }
  }
}
