using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
    /// <summary>Partition the sequence into a specified number of list. The last partition may contain less elements than the other partitions.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionByCount<TSource, TResult>(this System.Collections.Generic.ICollection<TSource> source, int partitionsCount, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
    {
      if (partitionsCount <= 0) throw new System.ArgumentOutOfRangeException(nameof(partitionsCount), $"Must be greater than or equal to 1 ({partitionsCount}).");
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var quotient = System.Math.DivRem(source.ThrowOnNull().Count(), partitionsCount, out var remainder);

      //return source.PartitionBySize(quotient + System.Math.Sign(remainder), resultSelector);
      return source.Chunk(quotient + System.Math.Sign(remainder)).Select(resultSelector);
    }

    /// <summary>Partition the sequence into a specified number of list. The last partition may contain less elements than the other partitions.</summary>
    /// <remarks>Enumerates the entire sequence to a list.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionByCount<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int countOfPartitions, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
      => PartitionByCount(new System.Collections.Generic.List<TSource>(source), countOfPartitions, resultSelector);
  }
}
