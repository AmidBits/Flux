namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Partition the sequence into one or more lists of a specified size. The last partition may contain less elements than requested.</summary>
    /// <param name="resultSelector">Receives the elements in the partition and partition index (this is not the element index).</param>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionBySize<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int sizeOfEachPartition, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
    {
      if (sizeOfEachPartition <= 0) throw new System.ArgumentOutOfRangeException(nameof(sizeOfEachPartition), $"Must be greater than or equal to 1 ({sizeOfEachPartition}).");
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var enumerator = source.ThrowIfNull().GetEnumerator();

      var partitionIndex = 0;

      while (enumerator.MoveNext())
        yield return resultSelector(Yield(enumerator, sizeOfEachPartition), partitionIndex);

      /// <summary>Yield count elements from the sequence.</summary>
      static System.Collections.Generic.IEnumerable<TSource> Yield(System.Collections.Generic.IEnumerator<TSource> e, int count)
      {
        yield return e.Current;

        for (var counter = 1; counter < count && e.MoveNext(); counter++)
          yield return e.Current;
      }
    }
  }
}
