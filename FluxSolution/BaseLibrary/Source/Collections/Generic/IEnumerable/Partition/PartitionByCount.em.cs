using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Partition the sequence into a specified number of list. The last partition may contain less elements than the other partitions.</summary>
    /// <remarks>Enumerates the sequence twice, because it needs the Count() to compute the partition size, FYI.</remarks>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionByCount<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int countOfPartitions, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
    {
      if (countOfPartitions <= 0) throw new System.ArgumentOutOfRangeException(nameof(countOfPartitions), $"Must be greater than or equal to 1 ({countOfPartitions}).");
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var quotient = System.Math.DivRem(source?.Count() ?? throw new System.ArgumentNullException(nameof(source)), countOfPartitions, out var remainder);

      return source.PartitionBySize(quotient + System.Math.Sign(remainder), resultSelector);
    }
  }
}
