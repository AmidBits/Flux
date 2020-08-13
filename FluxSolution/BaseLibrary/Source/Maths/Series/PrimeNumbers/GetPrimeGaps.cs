using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Results in an ascending sequence of gaps between prime numbers starting with the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPrimeGaps(System.Numerics.BigInteger startAt) => GetAscendingPrimes(startAt).PartitionTuple(false, (leading, trailing, index) => trailing - leading);

    /// <summary>Results in an ascending sequence of gaps between prime numbers starting with the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetPrimeGaps(int startAt) => GetAscendingPrimes(startAt).PartitionTuple(false, (leading, trailing, index) => trailing - leading);
    /// <summary>Results in an ascending sequence of gaps between prime numbers starting with the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<long> GetPrimeGaps(long startAt) => GetAscendingPrimes(startAt).PartitionTuple(false, (leading, trailing, index) => trailing - leading);
  }
}
