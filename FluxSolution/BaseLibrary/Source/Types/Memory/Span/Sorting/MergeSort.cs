namespace Flux
{
  public static partial class XtendSpan
  {
    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static void MergeSort<T>(this System.Span<T> source, MergeSortType type, System.Collections.Generic.IComparer<T> comparer)
      => new MergeSort<T>(type, comparer).Sort(source);
    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static void MergeSort<T>(this System.Span<T> source, MergeSortType type)
      => MergeSort(source, type, System.Collections.Generic.Comparer<T>.Default);
  }

  public enum MergeSortType
  {
    BottomUp,
    TopDown
  }

  /// <summary>Sorts the content of the list using merge sort. Uses the specified comparer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Merge_sort"/>
  public class MergeSort<T>
    : ISortable<T>
  {
    private System.Collections.Generic.IComparer<T> m_comparer;
    private MergeSortType m_type;

    public MergeSort(MergeSortType type, System.Collections.Generic.IComparer<T>? comparer)
    {
      m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;
      m_type = type;
    }
    public MergeSort()
      : this(MergeSortType.TopDown, null)
    {
    }

    public void Sort(System.Span<T> source)
    {
      var target = new T[source.Length];

      switch (m_type)
      {
        case MergeSortType.BottomUp:
          BottomUpMergeSort(source, target, m_comparer, source.Length);
          break;
        case MergeSortType.TopDown:
          TopDownMergeSort(source, target, m_comparer, source.Length);
          break;
        default:
          throw new System.Exception(nameof(m_type));
      }
    }

    #region MergeSort bottom-up helper functions
    // array A[] has the items to sort; array B[] is a work array
    private void BottomUpMergeSort(System.Span<T> source, System.Span<T> target, int n)
    {
      // Each 1-element run in A is already "sorted".
      // Make successively longer sorted runs of length 2, 4, 8, 16... until whole array is sorted.
      for (var width = 1; width < n; width = 2 * width)
      {
        // Array A is full of runs of length width.
        for (var i = 0; i < n; i = i + 2 * width)
        {
          // Merge two runs: A[i:i+width-1] and A[i+width:i+2*width-1] to B[]
          // or copy A[i:n-1] to B[] ( if(i+width >= n) )
          BottomUpMerge(source, target, i, System.Math.Min(i + width, n), System.Math.Min(i + 2 * width, n));
        }
        // Now work array B is full of runs of length 2*width.
        // Copy array B to array A for next iteration.
        // A more efficient implementation would swap the roles of A and B.
        CopyArray(target, source, n);
        // Now array A is full of runs of length 2*width.
      }
    }

    //  Left run is A[iLeft :iRight-1].
    // Right run is A[iRight:iEnd-1  ].
    private void BottomUpMerge(System.Span<T> source, System.Span<T> target, int iLeft, int iRight, int iEnd)
    {
      int i = iLeft, j = iRight;
      // While there are elements in the left or right runs...
      for (var k = iLeft; k < iEnd; k++)
      {
        // If left run head exists and is <= existing right run head.
        if (i < iRight && (j >= iEnd || m_comparer.Compare(source[i], source[j]) <= 0))
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
    #endregion MergeSort bottom-up helper functions

    #region MergeSort top-down helper functions
    private void TopDownMergeSort(System.Span<T> source, System.Span<T> target, int n)
    {
      CopyArray(source, target, 0, n);           // one time copy of A[] to B[]
      TopDownSplitMerge(target, source, 0, n);   // sort data from B[] into A[]
    }

    // Sort the given run of array A[] using array B[] as a source.
    // iBegin is inclusive; iEnd is exclusive (A[iEnd] is not in the set).
    private void TopDownSplitMerge(System.Span<T> target, System.Span<T> source, int iBegin, int iEnd)
    {
      if (iEnd - iBegin <= 1)                       // if run size == 1
        return;                                 //   consider it sorted
                                                // split the run longer than 1 item into halves
      var iMiddle = (iEnd + iBegin) / 2;              // iMiddle = mid point
                                                      // recursively sort both runs from array A[] into B[]
      TopDownSplitMerge(source, target, iBegin, iMiddle);  // sort the left  run
      TopDownSplitMerge(source, target, iMiddle, iEnd);  // sort the right run
                                                         // merge the resulting runs from array B[] into A[]
      TopDownMerge(target, source, iBegin, iMiddle, iEnd);
    }

    //  Left source half is A[ iBegin:iMiddle-1].
    // Right source half is A[iMiddle:iEnd-1   ].
    // Result is            B[ iBegin:iEnd-1   ].
    private void TopDownMerge(System.Span<T> source, System.Span<T> target, int iBegin, int iMiddle, int iEnd)
    {
      int i = iBegin, j = iMiddle;

      // While there are elements in the left or right runs...
      for (var k = iBegin; k < iEnd; k++)
      {
        // If left run head exists and is <= existing right run head.
        if (i < iMiddle && (j >= iEnd || m_comparer.Compare(source[i], source[j]) <= 0))
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
    #endregion MergeSort top-down helper functions
  }
}
