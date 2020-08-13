using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns a sequence of prime triplets, each of which is a set of three prime numbers of the form (p, p + 2, p + 6) or (p, p + 4, p + 6).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Prime_triplet"/>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger)> GetPrimeTriplets() => 0 is var index ? GetAscendingPrimes(SmallestPrime.ToBigInteger()).PartitionTuple(0, (leading, midling, trailing, index) => (leading, midling, trailing)).Where((t) => t.trailing - t.leading is var gap3to1 && gap3to1 == 6 && t.midling - t.leading is var gap2to1 && (gap2to1 == 2 || gap2to1 == 4)) : throw new System.Exception();
  }
}
