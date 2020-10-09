namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Partition the sequence into a specified number of list. The last partition may contain less elements than the other partitions.</summary>
    /// <remarks>Enumerates the entire sequence to a list.</remarks>
    /// <param name="resultSelector">Receives the elements in the partition and partition index.</param>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionByCount<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int countOfPartitions, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
    {
      if (countOfPartitions <= 0) throw new System.ArgumentOutOfRangeException(nameof(countOfPartitions), $"Must be greater than or equal to 1 ({countOfPartitions}).");
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var list = new System.Collections.Generic.List<TSource>(source);

      var quotient = System.Math.DivRem(list.Count, countOfPartitions, out var remainder);

      return list.PartitionBySize(quotient + System.Math.Sign(remainder), resultSelector);
    }
  }
}
