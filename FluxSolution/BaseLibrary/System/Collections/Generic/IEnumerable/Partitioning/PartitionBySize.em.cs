namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
    /// <summary>Partition the sequence into one or more lists of a specified size. The last partition may contain less elements than requested.</summary>
    /// <param name="resultSelector">Receives the elements in the partition and partition index (this is not the element index).</param>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    [System.Obsolete("Prefer the built-in System.Linq.Enumerable.Chunk()", false)]
    public static System.Collections.Generic.IEnumerable<TResult> PartitionBySize<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int partitionSize, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
      => source.Chunk(partitionSize).Select((e, i) => resultSelector(e, i));
    //{
    //  if (partitionSize <= 0) throw new System.ArgumentOutOfRangeException(nameof(partitionSize), $"Must be greater than or equal to 1 ({partitionSize}).");
    //  if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

    //  var enumerator = source.ThrowIfNull().GetEnumerator();

    //  var partitionIndex = 0;

    //  while (enumerator.MoveNext())
    //    yield return resultSelector(Yield(enumerator, partitionSize), partitionIndex);

    //  /// <summary>Yield count elements from the sequence.</summary>
    //  static System.Collections.Generic.IEnumerable<TSource> Yield(System.Collections.Generic.IEnumerator<TSource> e, int count)
    //  {
    //    yield return e.Current;

    //    for (var counter = 1; counter < count && e.MoveNext(); counter++)
    //      yield return e.Current;
    //  }
    //}
  }
}
