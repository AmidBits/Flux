using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Partition the sequence into one or more lists of a specified size. The last partition may contain less elements than requested.</summary>
    /// <remarks>The integers passed to the predicates contains (in order) elementCount (within partition), elementIndex (within partition) and partitionIndex.</remarks>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionByPredicate<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, int, bool> elementPredicate, System.Func<int, int, int, bool> partitionPredicate, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
    {
      if (elementPredicate is null) throw new System.ArgumentNullException(nameof(elementPredicate));
      if (partitionPredicate is null) throw new System.ArgumentNullException(nameof(partitionPredicate));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var partitionIndex = 0;

      using (var enumerator = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source)))
      {
        while (enumerator.MoveNext())
        {
          yield return resultSelector(Yield(enumerator), partitionIndex++);
        }
      }

      /// <summary>Yield count elements from the sequence.</summary>
      System.Collections.Generic.IEnumerable<TSource> Yield(System.Collections.Generic.IEnumerator<TSource> e)
      {
        int elementCount = 0, elementIndex = 0;

        do
        {
          if (elementPredicate(e.Current, elementIndex, elementCount) )
          {
            yield return e.Current;

            elementCount++;
          }

          elementIndex++;
        }
        while (partitionPredicate(partitionIndex, elementIndex, elementCount)  && e.MoveNext());

        yield break;
      }
    }
  }
}
