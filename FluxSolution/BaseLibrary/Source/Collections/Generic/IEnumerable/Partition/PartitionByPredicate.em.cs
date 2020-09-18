using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Partition the sequence into sub-results, separated when the predicate is satisfied.</summary>
    /// <remarks>The initial element for each sub-result is always returned.</remarks>
    /// <param name="partitionPredicate">Receives the element, the source index of the element and the partition index.</param>
    /// <param name="resultSelector">Receives a sequence of elements and the partition index if the sequence.</param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionByPredicate<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, int, bool> partitionPredicate, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
    {
      if (partitionPredicate is null) throw new System.ArgumentNullException(nameof(partitionPredicate));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var partitionIndex = 0;
      var elementIndex = 0;

      using (var enumerator = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source)))
      {
        while (enumerator.MoveNext())
        {
          yield return resultSelector(Yield(enumerator), partitionIndex);

          partitionIndex++;
        }
      }

      /// <summary>Yield count elements from the sequence.</summary>
      System.Collections.Generic.IEnumerable<TSource> Yield(System.Collections.Generic.IEnumerator<TSource> e)
      {
        do
        {
          yield return e.Current;
        }
        while (!partitionPredicate(e.Current, elementIndex++, partitionIndex) && e.MoveNext());

        yield break;
      }
    }
  }
}
