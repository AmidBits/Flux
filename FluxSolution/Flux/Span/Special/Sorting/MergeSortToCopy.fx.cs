namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Sorts the content of the sequence using merge sort.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Merge_sort"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="type"></param>
    /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public static T[] MergeSort<T>(this System.Span<T> source, MergeSortType type, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var target = new T[source.Length];

      switch (type)
      {
        case MergeSortType.BottomUp:
          BottomUpMergeSort(source.ToArray(), target, source.Length, comparer);
          break;
        case MergeSortType.TopDown:
          TopDownMergeSort(source.ToArray(), target, source.Length, comparer);
          break;
        default:
          throw new System.Exception(nameof(type));
      }

      return target;

      #region MergeSort bottom-up helpers (Span)

      /// <summary>The <paramref name="source"/> array has the items to sort, and the <paramref name="target"/> is the work array.</summary>
      static void BottomUpMergeSort(System.Span<T> source, System.Span<T> target, int length, System.Collections.Generic.IComparer<T> comparer)
      {
        for (var width = 1; width < length; width = 2 * width) // Each 1-element run in A is already "sorted". Make successively longer sorted runs of length 2, 4, 8, 16... until whole array is sorted.
        {
          for (var i = 0; i < length; i += 2 * width) // Array A is full of runs of length width.
          {
            BottomUpMerge(source, target, i, int.Min(i + width, length), int.Min(i + 2 * width, length), comparer); // Merge two runs: source[i:i+width-1] and target[i+width:i+2*width-1] to B[] // or copy A[i:n-1] to B[] ( if(i+width >= n) )
          }

          CopyArray(target, source, length); // Now work array target is full of runs of length 2*width. Copy array target to array source for next iteration. A more efficient implementation would swap the roles of A and B. Now array source is full of runs of length 2*width.
        }
      }

      /// <summary>Left run is source[leftIndex:rightIndex-1] and right run is source[rightIndex:endIndex-1].</summary>
      static void BottomUpMerge(System.Span<T> source, System.Span<T> target, int leftIndex, int rightIndex, int endIndex, System.Collections.Generic.IComparer<T> comparer)
      {
        int l = leftIndex, r = rightIndex, k = leftIndex;
        while (k < endIndex) // While there are elements in either runs...
        {
          if (l < rightIndex && (r >= endIndex || comparer.Compare(source[l], source[r]) <= 0)) // If left run head exists and is <= existing right run head.
            target[k++] = source[l++];
          else
            target[k++] = source[r++];
        }
      }

      static void CopyArray(System.Span<T> source, System.Span<T> target, int length)
      {
        for (var index = 0; index < length; index++)
          target[index] = source[index];
      }

      #endregion MergeSort bottom-up helpers (Span)

      #region MergeSort top-down helpers (Span)

      static void TopDownMergeSort(System.Span<T> source, System.Span<T> target, int length, System.Collections.Generic.IComparer<T> comparer)
      {
        CopyArrayByIndexing(source, target, 0, length); // One time copy of source[] to target[].

        TopDownSplitMerge(target, source, 0, length, comparer); // Sort data from target[] into source[].
      }

      /// <summary>Sort the given run of array <paramref name="source"/> using array <paramref name="target"/> as source; beginIndex is inclusive; endIndex is exclusive (.e. source[endIndex] is not in the set).</summary>
      static void TopDownSplitMerge(System.Span<T> target, System.Span<T> source, int beginIndex, int endIndex, System.Collections.Generic.IComparer<T> comparer)
      {
        if (endIndex - beginIndex <= 1) // Already sorted?
          return;

        var splitIndex = (endIndex + beginIndex) / 2; // Split the run longer than 1 item into halves, where iMiddle = mid point.

        TopDownSplitMerge(source, target, beginIndex, splitIndex, comparer);  // Recursively sort the left run
        TopDownSplitMerge(source, target, splitIndex, endIndex, comparer);  // Recursively sort the right run

        TopDownMerge(target, source, beginIndex, splitIndex, endIndex, comparer); // merge the resulting runs from array B[] into A[]
      }

      /// <summary>Left half is source[begin:middle-1], right half is source[middle:end-1], result is target[begin:end-1].</summary>
      static void TopDownMerge(System.Span<T> source, System.Span<T> target, int beginIndex, int middleIndex, int endIndex, System.Collections.Generic.IComparer<T> comparer)
      {
        int i = beginIndex, j = middleIndex, k = beginIndex;
        while (k < endIndex) // While there are elements in either runs...
        {
          if (i < middleIndex && (j >= endIndex || comparer.Compare(source[i], source[j]) <= 0)) // If left run head exists and is <= existing right run head.
            target[k++] = source[i++];
          else
            target[k++] = source[j++];
        }
      }

      /// <summary>Copy <paramref name="source"/> to <paramref name="target"/> starting at beginIndex (inclusive) up to endIndex (exclusive).</summary>
      static void CopyArrayByIndexing(System.Span<T> source, System.Span<T> target, int beginIndex, int endIndex)
      {
        for (var index = beginIndex; index < endIndex; index++)
          target[index] = source[index];
      }

      #endregion MergeSort top-down helpers (Span)
    }
  }
}
