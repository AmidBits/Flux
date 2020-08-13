using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Results in a new sequence of numbers representing the gaps between the numbers in the sequence, including the last and the first number.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetGaps(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source, bool includeGapBetweenLastAndFirst) => source.PartitionTuple(includeGapBetweenLastAndFirst, (leading, trailing, index) => trailing - leading);

    /// <summary>Results in a new sequence of numbers representing the gaps between the numbers in the sequence, including the last and the first number.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetGaps(this System.Collections.Generic.IEnumerable<int> source, bool includeGapBetweenLastAndFirst) => source.PartitionTuple(includeGapBetweenLastAndFirst, (leading, trailing, index) => trailing - leading);
    /// <summary>Results in a new sequence of numbers representing the gaps between the numbers in the sequence, including the last and the first number.</summary>
    public static System.Collections.Generic.IEnumerable<long> GetGaps(this System.Collections.Generic.IEnumerable<long> source, bool includeGapBetweenLastAndFirst) => source.PartitionTuple(includeGapBetweenLastAndFirst, (leading, trailing, index) => trailing - leading);

    /// <summary>Results in a new sequence of numbers representing the gaps between the numbers in the sequence, including the last and the first number.</summary>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<uint> GetGaps(this System.Collections.Generic.IEnumerable<uint> source, bool includeGapBetweenLastAndFirst) => source.PartitionTuple(includeGapBetweenLastAndFirst, (leading, trailing, index) => trailing - leading);
    /// <summary>Results in a new sequence of numbers representing the gaps between the numbers in the sequence, including the last and the first number.</summary>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<ulong> GetGaps(this System.Collections.Generic.IEnumerable<ulong> source, bool includeGapBetweenLastAndFirst) => source.PartitionTuple(includeGapBetweenLastAndFirst, (leading, trailing, index) => trailing - leading);
  }
}
