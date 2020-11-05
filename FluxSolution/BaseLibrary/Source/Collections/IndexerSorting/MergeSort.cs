namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Sorts the content of the sequence using merge sort.</summary>
    public static void ApplyMergeSort<T>(this System.Collections.Generic.IList<T> source, IndexedSorting.MergeSortType type, System.Collections.Generic.IComparer<T> comparer)
      => new IndexedSorting.MergeSort<T>(type, comparer).SortInline((T[])source);
    /// <summary>Sorts the content of the sequence using merge sort.</summary>
    public static void ApplyMergeSort<T>(this System.Collections.Generic.IList<T> source, IndexedSorting.MergeSortType type)
      => ApplyMergeSort(source, type, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the sequence using merge sort.</summary>
    public static void ApplyMergeSort<T>(this System.Span<T> source, IndexedSorting.MergeSortType type, System.Collections.Generic.IComparer<T> comparer)
      => new IndexedSorting.MergeSort<T>(type, comparer).SortInline(source);
    /// <summary>Sorts the content of the sequence using merge sort.</summary>
    public static void ApplyMergeSort<T>(this System.Span<T> source, IndexedSorting.MergeSortType type)
      => ApplyMergeSort(source, type, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace IndexedSorting
  {
    public enum MergeSortType
    {
      BottomUp,
      TopDown
    }

    /// <summary>Sorts the content of the sequence using merge sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Merge_sort"/>
    public class MergeSort<T>
      : ASpanSorting<T>
    {
      private readonly MergeSortType m_type;

      public MergeSort(MergeSortType type, System.Collections.Generic.IComparer<T> comparer)
        : base(comparer)
      {
        m_type = type;
      }
      public MergeSort()
        : this(MergeSortType.TopDown, System.Collections.Generic.Comparer<T>.Default)
      {
      }

      public override void SortInline(System.Span<T> source)
      {
        var target = SortToCopy(new System.ReadOnlySpan<T>(source.ToArray()));
        for (var i = source.Length - 1; i >= 0; i--)
          source[i] = target[i];
      }
      public override T[] SortToCopy(System.ReadOnlySpan<T> source)
      {
        var target = new T[source.Length];

        switch (m_type)
        {
          case MergeSortType.BottomUp:
            BottomUpMergeSort(source.ToArray(), target, source.Length);
            break;
          case MergeSortType.TopDown:
            TopDownMergeSort(source.ToArray(), target, source.Length);
            break;
          default:
            throw new System.Exception(nameof(m_type));
        }

        return target;
      }

      #region MergeSort bottom-up helpers (Span)
      // array A[] has the items to sort; array B[] is a work array
      private void BottomUpMergeSort(System.Span<T> source, System.Span<T> target, int n)
      {

        for (var width = 1; width < n; width = 2 * width) // Each 1-element run in A is already "sorted". Make successively longer sorted runs of length 2, 4, 8, 16... until whole array is sorted.
        {
          for (var i = 0; i < n; i += 2 * width) // Array A is full of runs of length width.
          {
            BottomUpMerge(source, target, i, System.Math.Min(i + width, n), System.Math.Min(i + 2 * width, n)); // Merge two runs: A[i:i+width-1] and A[i+width:i+2*width-1] to B[] // or copy A[i:n-1] to B[] ( if(i+width >= n) )
          }

          CopyArray(target, source, n); // Now work array B is full of runs of length 2*width. Copy array B to array A for next iteration. A more efficient implementation would swap the roles of A and B. Now array A is full of runs of length 2*width.
        }
      }

      //  Left run is A[iLeft : iRight-1].
      // Right run is A[iRight : iEnd-1 ].
      private void BottomUpMerge(System.Span<T> source, System.Span<T> target, int iLeft, int iRight, int iEnd)
      {
        for (int i = iLeft, j = iRight, k = iLeft; k < iEnd; k++) // While there are elements in either runs...
        {
          if (i < iRight && (j >= iEnd || Comparer.Compare(source[i], source[j]) <= 0)) // If left run head exists and is <= existing right run head.
          {
            target[k] = source[i];
            i++;
          }
          else
          {
            target[k] = source[j];
            j++;
          }
        }
      }

      private static void CopyArray(System.Span<T> target, System.Span<T> source, int n)
      {
        for (var i = 0; i < n; i++)
          source[i] = target[i];
      }
      #endregion MergeSort bottom-up helpers (Span)

      #region MergeSort top-down helpers (Span)
      private void TopDownMergeSort(System.Span<T> source, System.Span<T> target, int n)
      {
        CopyArray(source, target, 0, n);           // one time copy of A[] to B[]
        TopDownSplitMerge(target, source, 0, n);   // sort data from B[] into A[]
      }

      // Sort the given run of array A[] using array B[] as a source.
      // iBegin is inclusive; iEnd is exclusive (A[iEnd] is not in the set).
      private void TopDownSplitMerge(System.Span<T> target, System.Span<T> source, int iBegin, int iEnd)
      {
        if (iEnd - iBegin <= 1) // Already sorted?
          return;

        var iMiddle = (iEnd + iBegin) / 2; // Split the run longer than 1 item into halves, where iMiddle = mid point.

        TopDownSplitMerge(source, target, iBegin, iMiddle);  // Recursively sort the left run
        TopDownSplitMerge(source, target, iMiddle, iEnd);  // Recursively sort the right run

        TopDownMerge(target, source, iBegin, iMiddle, iEnd); // merge the resulting runs from array B[] into A[]
      }

      //  Left source half is A[ iBegin:iMiddle-1].
      // Right source half is A[iMiddle:iEnd-1   ].
      // Result is            B[ iBegin:iEnd-1   ].
      private void TopDownMerge(System.Span<T> source, System.Span<T> target, int iBegin, int iMiddle, int iEnd)
      {
        int i = iBegin, j = iMiddle;

        for (var k = iBegin; k < iEnd; k++) // While there are elements in either runs...
        {
          if (i < iMiddle && (j >= iEnd || Comparer.Compare(source[i], source[j]) <= 0)) // If left run head exists and is <= existing right run head.
          {
            target[k] = source[i];
            i++;
          }
          else
          {
            target[k] = source[j];
            j++;
          }
        }
      }

      private static void CopyArray(System.Span<T> source, System.Span<T> target, int iBegin, int iEnd)
      {
        for (var k = iBegin; k < iEnd; k++)
          target[k] = source[k];
      }
      #endregion MergeSort top-down helpers (Span)
    }
  }
}
