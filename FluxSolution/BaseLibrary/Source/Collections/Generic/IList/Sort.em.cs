using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Sorts the content of the list using bingo sort which is a variant of selection sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public static void BingoSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new BingoSort<T>(comparer).Sort(source.ToArray());
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  comparer ??= System.Collections.Generic.Comparer<T>.Default;

    //  var max = source.Count - 1;

    //  var nextValue = source[max];

    //  for (var i = max - 1; i >= 0; i--)
    //    if (comparer.Compare(source[i], nextValue) > 0)
    //      nextValue = source[i];

    //  while (max > 0 && comparer.Compare(source[max], nextValue) == 0) max--;

    //  while (max > 0)
    //  {
    //    var value = nextValue;
    //    nextValue = source[max];

    //    for (var i = max - 1; i >= 0; i--)
    //      if (comparer.Compare(source[i], value) == 0)
    //      {
    //        source.Swap(i, max);
    //        max--;
    //      }
    //      else if (comparer.Compare(source[i], nextValue) > 0)
    //        nextValue = source[i];

    //    while (max > 0 && comparer.Compare(source[max], nextValue) == 0) max--;
    //  }
    //}
    /// <summary>Sorts the content of the list using bingo sort which is a variant of selection sort. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public static void BingoSort<T>(this System.Collections.Generic.IList<T> source)
      => BingoSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the list using bubble sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bubble_sort"/>
    public static void BubbleSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new BubbleSort<T>(comparer).Sort(source.ToArray());
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  comparer ??= System.Collections.Generic.Comparer<T>.Default;

    //  var length = source.Count;

    //  do
    //  {
    //    var newLength = 0;

    //    for (var i = 1; i < length; i++)
    //    {
    //      if (comparer.Compare(source[i - 1], source[i]) > 0)
    //      {
    //        source.Swap(i - 1, i);
    //        newLength = i;
    //      }
    //    }

    //    length = newLength;
    //  }
    //  while (length > 1);
    //}
    /// <summary>Sorts the content of the list using bubble sort. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bubble_sort"/>
    public static void BubbleSort<T>(this System.Collections.Generic.IList<T> source)
    => BubbleSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the list using bubble sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Insertion_sort"/>
    public static void InsertionSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new InsertionSort<T>(comparer).Sort(source.ToArray());
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  comparer ??= System.Collections.Generic.Comparer<T>.Default;

    //  var sourceLength = source.Count;

    //  for (var i = 1; i < sourceLength; i++)
    //    for (var j = i; j > 0 && comparer.Compare(source[j - 1], source[j]) > 0; j--)
    //      source.Swap(j, j - 1);
    //}
    /// <summary>Sorts the content of the list using bubble sort. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Insertion_sort"/>
    public static void InsertionSort<T>(this System.Collections.Generic.IList<T> source)
      => InsertionSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the list using merge sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Merge_sort"/>
    public static void MergeSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new MergeSort<T>(MergeSortType.BottomUp, comparer).Sort(source.ToArray());
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  var target = new T[source.Count];

    //  TopDownMergeSort(source, target, comparer, source.Count);
    //}
    /// <summary>Sorts the content of the list using merge sort. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Merge_sort"/>
    public static void MergeSort<T>(this System.Collections.Generic.IList<T> source)
      => MergeSort(source, System.Collections.Generic.Comparer<T>.Default);

    //#region MergeSort top-down helper functions
    //internal static void TopDownMergeSort<T>(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IComparer<T> comparer, int n)
    //{
    //  CopyArray(source, target, 0, n);           // one time copy of A[] to B[]
    //  TopDownSplitMerge(target, source, comparer, 0, n);   // sort data from B[] into A[]
    //}

    //// Sort the given run of array A[] using array B[] as a source.
    //// iBegin is inclusive; iEnd is exclusive (A[iEnd] is not in the set).
    //internal static void TopDownSplitMerge<T>(System.Collections.Generic.IList<T> target, System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer, int iBegin, int iEnd)
    //{
    //  if (iEnd - iBegin <= 1)                       // if run size == 1
    //    return;                                 //   consider it sorted
    //                                            // split the run longer than 1 item into halves
    //  var iMiddle = (iEnd + iBegin) / 2;              // iMiddle = mid point
    //                                                  // recursively sort both runs from array A[] into B[]
    //  TopDownSplitMerge(source, target, comparer, iBegin, iMiddle);  // sort the left  run
    //  TopDownSplitMerge(source, target, comparer, iMiddle, iEnd);  // sort the right run
    //                                                               // merge the resulting runs from array B[] into A[]
    //  TopDownMerge(target, source, comparer, iBegin, iMiddle, iEnd);
    //}

    ////  Left source half is A[ iBegin:iMiddle-1].
    //// Right source half is A[iMiddle:iEnd-1   ].
    //// Result is            B[ iBegin:iEnd-1   ].
    //internal static void TopDownMerge<T>(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IComparer<T> comparer, int iBegin, int iMiddle, int iEnd)
    //{
    //  int i = iBegin, j = iMiddle;

    //  // While there are elements in the left or right runs...
    //  for (var k = iBegin; k < iEnd; k++)
    //  {
    //    // If left run head exists and is <= existing right run head.
    //    if (i < iMiddle && (j >= iEnd || comparer.Compare(source[i], source[j]) <= 0))
    //    {
    //      target[k] = source[i];
    //      i++;
    //    }
    //    else
    //    {
    //      target[k] = source[j];
    //      j++;
    //    }
    //  }
    //}

    //internal static void CopyArray<T>(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, int iBegin, int iEnd)
    //{
    //  for (var k = iBegin; k < iEnd; k++)
    //    target[k] = source[k];
    //}
    //#endregion MergeSort top-down helper functions

    //#region MergeSort bottom-up helper functions
    //// array A[] has the items to sort; array B[] is a work array
    //internal static void BottomUpMergeSort<T>(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IComparer<T> comparer, int n)
    //{
    //  // Each 1-element run in A is already "sorted".
    //  // Make successively longer sorted runs of length 2, 4, 8, 16... until whole array is sorted.
    //  for (var width = 1; width < n; width = 2 * width)
    //  {
    //    // Array A is full of runs of length width.
    //    for (var i = 0; i < n; i = i + 2 * width)
    //    {
    //      // Merge two runs: A[i:i+width-1] and A[i+width:i+2*width-1] to B[]
    //      // or copy A[i:n-1] to B[] ( if(i+width >= n) )
    //      BottomUpMerge(source, target, comparer, i, System.Math.Min(i + width, n), System.Math.Min(i + 2 * width, n));
    //    }
    //    // Now work array B is full of runs of length 2*width.
    //    // Copy array B to array A for next iteration.
    //    // A more efficient implementation would swap the roles of A and B.
    //    CopyArray(target, source, n);
    //    // Now array A is full of runs of length 2*width.
    //  }
    //}

    ////  Left run is A[iLeft :iRight-1].
    //// Right run is A[iRight:iEnd-1  ].
    //internal static void BottomUpMerge<T>(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IComparer<T> comparer, int iLeft, int iRight, int iEnd)
    //{
    //  int i = iLeft, j = iRight;
    //  // While there are elements in the left or right runs...
    //  for (var k = iLeft; k < iEnd; k++)
    //  {
    //    // If left run head exists and is <= existing right run head.
    //    if (i < iRight && (j >= iEnd || comparer.Compare(source[i], source[j]) <= 0))
    //    {
    //      target[k] = source[i];
    //      i++;
    //    }
    //    else
    //    {
    //      target[k] = source[j];
    //      j++;
    //    }
    //  }
    //}

    //internal static void CopyArray<T>(System.Collections.Generic.IList<T> target, System.Collections.Generic.IList<T> source, int n)
    //{
    //  for (var i = 0; i < n; i++)
    //    source[i] = target[i];
    //}
    //#endregion MergeSort bottom-up helper functions

    /// <summary>Sorts the content of the list using quick sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quick_sort"/>
    public static void QuickSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new QuickSort<T>(comparer).Sort(source.ToArray());
    //=> QuickSort(source, comparer, 0, (source ?? throw new System.ArgumentNullException(nameof(source))).Count - 1);
    /// <summary>Sorts the content of the list using quick sort. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quick_sort"/>
    public static void QuickSort<T>(this System.Collections.Generic.IList<T> source)
      => QuickSort(source, System.Collections.Generic.Comparer<T>.Default);

    //#region QuickSort helper functions
    //internal static void QuickSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer, int lowIndex, int highIndex)
    //{
    //  if (lowIndex < highIndex)
    //  {
    //    var pivotIndex = QuickPartition(source, comparer, lowIndex, highIndex);

    //    QuickSort(source, comparer, lowIndex, pivotIndex - 1);
    //    QuickSort(source, comparer, pivotIndex + 1, highIndex);
    //  }
    //}
    //internal static int QuickPartition<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer, int lowIndex, int highIndex)
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  comparer ??= System.Collections.Generic.Comparer<T>.Default;

    //  var pivotValue = source[highIndex];
    //  var i = lowIndex;

    //  for (var j = lowIndex; j < highIndex; j++)
    //  {
    //    if (comparer.Compare(source[j], pivotValue) < 0)
    //    {
    //      source.Swap(i, j);
    //      i++;
    //    }
    //  }

    //  source.Swap(i, highIndex);

    //  return i;
    //}
    //#endregion QuickSort helper functions

    /// <summary>Sorts the content of the list using selection sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public static void SelectionSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SelectionSort<T>(comparer).Sort(source.ToArray());
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  comparer ??= System.Collections.Generic.Comparer<T>.Default;

    //  for (var i = 0; i < source.Count - 1; i++)
    //  {
    //    var min = i;
    //    for (var j = i + 1; j < source.Count; j++)
    //      if (comparer.Compare(source[j], source[min]) < 0)
    //        min = j;

    //    var x = source[min];
    //    for (var j = min; j > i; j--)
    //      source[j] = source[j - 1];
    //    source[i] = x;
    //  }
    //}
    /// <summary>Sorts the content of the list using selection sort. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public static void SelectionSort<T>(this System.Collections.Generic.IList<T> source)
      => SelectionSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  //public class BingoSort<T>
  //{
  //  private System.Collections.Generic.IComparer<T> m_comparer;

  //  public BingoSort(System.Collections.Generic.IComparer<T>? comparer)
  //    => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;

  //  public void Sort(System.Collections.Generic.IList<T> source)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));

  //    var max = source.Count - 1;

  //    var nextValue = source[max];

  //    for (var i = max - 1; i >= 0; i--)
  //      if (m_comparer.Compare(source[i], nextValue) > 0)
  //        nextValue = source[i];

  //    while (max > 0 && m_comparer.Compare(source[max], nextValue) == 0) max--;

  //    while (max > 0)
  //    {
  //      var value = nextValue;
  //      nextValue = source[max];

  //      for (var i = max - 1; i >= 0; i--)
  //        if (m_comparer.Compare(source[i], value) == 0)
  //        {
  //          source.Swap(i, max);
  //          max--;
  //        }
  //        else if (m_comparer.Compare(source[i], nextValue) > 0)
  //          nextValue = source[i];

  //      while (max > 0 && m_comparer.Compare(source[max], nextValue) == 0) max--;
  //    }
  //  }
  //}

  //public class BubbleSort<T>
  //{
  //  private System.Collections.Generic.IComparer<T> m_comparer;

  //  public BubbleSort(System.Collections.Generic.IComparer<T>? comparer)
  //    => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;

  //  public void Sort(System.Collections.Generic.IList<T> source)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));

  //    var length = source.Count;

  //    do
  //    {
  //      var newLength = 0;

  //      for (var i = 1; i < length; i++)
  //      {
  //        if (m_comparer.Compare(source[i - 1], source[i]) > 0)
  //        {
  //          source.Swap(i - 1, i);
  //          newLength = i;
  //        }
  //      }

  //      length = newLength;
  //    }
  //    while (length > 1);
  //  }
  //}

  //public class InsertionSort<T>
  //{
  //  private System.Collections.Generic.IComparer<T> m_comparer;

  //  public InsertionSort(System.Collections.Generic.IComparer<T>? comparer)
  //    => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;

  //  public void Sort(System.Collections.Generic.IList<T> source)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));

  //    var sourceLength = source.Count;

  //    for (var i = 1; i < sourceLength; i++)
  //      for (var j = i; j > 0 && m_comparer.Compare(source[j - 1], source[j]) > 0; j--)
  //        source.Swap(j, j - 1);
  //  }
  //}

  //public enum MergeSortType
  //{
  //  BottomUp,
  //  TopDown
  //}

  //public class MergeSort<T>
  //{
  //  private System.Collections.Generic.IComparer<T> m_comparer;
  //  private MergeSortType m_type;

  //  public MergeSort(MergeSortType type, System.Collections.Generic.IComparer<T>? comparer)
  //  {
  //    m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;
  //    m_type = type;
  //  }

  //  public void Sort(System.Collections.Generic.IList<T> source)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));

  //    var target = new T[source.Count];

  //    switch (m_type)
  //    {
  //      case MergeSortType.BottomUp:
  //        BottomUpMergeSort(source, target, m_comparer, source.Count);
  //        break;
  //      case MergeSortType.TopDown:
  //        TopDownMergeSort(source, target, m_comparer, source.Count);
  //        break;
  //      default:
  //        throw new System.Exception(nameof(m_type));
  //    }
  //  }

  //  #region MergeSort bottom-up helper functions
  //  // array A[] has the items to sort; array B[] is a work array
  //  internal static void BottomUpMergeSort(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IComparer<T> comparer, int n)
  //  {
  //    // Each 1-element run in A is already "sorted".
  //    // Make successively longer sorted runs of length 2, 4, 8, 16... until whole array is sorted.
  //    for (var width = 1; width < n; width = 2 * width)
  //    {
  //      // Array A is full of runs of length width.
  //      for (var i = 0; i < n; i = i + 2 * width)
  //      {
  //        // Merge two runs: A[i:i+width-1] and A[i+width:i+2*width-1] to B[]
  //        // or copy A[i:n-1] to B[] ( if(i+width >= n) )
  //        BottomUpMerge(source, target, comparer, i, System.Math.Min(i + width, n), System.Math.Min(i + 2 * width, n));
  //      }
  //      // Now work array B is full of runs of length 2*width.
  //      // Copy array B to array A for next iteration.
  //      // A more efficient implementation would swap the roles of A and B.
  //      CopyArray(target, source, n);
  //      // Now array A is full of runs of length 2*width.
  //    }
  //  }

  //  //  Left run is A[iLeft :iRight-1].
  //  // Right run is A[iRight:iEnd-1  ].
  //  internal static void BottomUpMerge(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IComparer<T> comparer, int iLeft, int iRight, int iEnd)
  //  {
  //    int i = iLeft, j = iRight;
  //    // While there are elements in the left or right runs...
  //    for (var k = iLeft; k < iEnd; k++)
  //    {
  //      // If left run head exists and is <= existing right run head.
  //      if (i < iRight && (j >= iEnd || comparer.Compare(source[i], source[j]) <= 0))
  //      {
  //        target[k] = source[i];
  //        i++;
  //      }
  //      else
  //      {
  //        target[k] = source[j];
  //        j++;
  //      }
  //    }
  //  }

  //  internal static void CopyArray(System.Collections.Generic.IList<T> target, System.Collections.Generic.IList<T> source, int n)
  //  {
  //    for (var i = 0; i < n; i++)
  //      source[i] = target[i];
  //  }
  //  #endregion MergeSort bottom-up helper functions

  //  #region MergeSort top-down helper functions
  //  internal static void TopDownMergeSort(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IComparer<T> comparer, int n)
  //  {
  //    CopyArray(source, target, 0, n);           // one time copy of A[] to B[]
  //    TopDownSplitMerge(target, source, comparer, 0, n);   // sort data from B[] into A[]
  //  }

  //  // Sort the given run of array A[] using array B[] as a source.
  //  // iBegin is inclusive; iEnd is exclusive (A[iEnd] is not in the set).
  //  internal static void TopDownSplitMerge(System.Collections.Generic.IList<T> target, System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer, int iBegin, int iEnd)
  //  {
  //    if (iEnd - iBegin <= 1)                       // if run size == 1
  //      return;                                 //   consider it sorted
  //                                              // split the run longer than 1 item into halves
  //    var iMiddle = (iEnd + iBegin) / 2;              // iMiddle = mid point
  //                                                    // recursively sort both runs from array A[] into B[]
  //    TopDownSplitMerge(source, target, comparer, iBegin, iMiddle);  // sort the left  run
  //    TopDownSplitMerge(source, target, comparer, iMiddle, iEnd);  // sort the right run
  //                                                                 // merge the resulting runs from array B[] into A[]
  //    TopDownMerge(target, source, comparer, iBegin, iMiddle, iEnd);
  //  }

  //  //  Left source half is A[ iBegin:iMiddle-1].
  //  // Right source half is A[iMiddle:iEnd-1   ].
  //  // Result is            B[ iBegin:iEnd-1   ].
  //  internal static void TopDownMerge(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IComparer<T> comparer, int iBegin, int iMiddle, int iEnd)
  //  {
  //    int i = iBegin, j = iMiddle;

  //    // While there are elements in the left or right runs...
  //    for (var k = iBegin; k < iEnd; k++)
  //    {
  //      // If left run head exists and is <= existing right run head.
  //      if (i < iMiddle && (j >= iEnd || comparer.Compare(source[i], source[j]) <= 0))
  //      {
  //        target[k] = source[i];
  //        i++;
  //      }
  //      else
  //      {
  //        target[k] = source[j];
  //        j++;
  //      }
  //    }
  //  }

  //  internal static void CopyArray(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, int iBegin, int iEnd)
  //  {
  //    for (var k = iBegin; k < iEnd; k++)
  //      target[k] = source[k];
  //  }
  //  #endregion MergeSort top-down helper functions
  //}

  //public class QuickSort<T>
  //{
  //  private System.Collections.Generic.IComparer<T> m_comparer;

  //  public QuickSort(System.Collections.Generic.IComparer<T>? comparer)
  //    => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;

  //  public void Sort(System.Collections.Generic.IList<T> source)
  //  {
  //    QuickSortImpl(source, m_comparer, 0, (source ?? throw new System.ArgumentNullException(nameof(source))).Count - 1);
  //  }

  //  #region QuickSort helper functions
  //  private void QuickSortImpl(System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer, int lowIndex, int highIndex)
  //  {
  //    if (lowIndex < highIndex)
  //    {
  //      var pivotIndex = QuickPartition(source, comparer, lowIndex, highIndex);

  //      QuickSortImpl(source, comparer, lowIndex, pivotIndex - 1);
  //      QuickSortImpl(source, comparer, pivotIndex + 1, highIndex);
  //    }
  //  }
  //  private static int QuickPartition(System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer, int lowIndex, int highIndex)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));

  //    comparer ??= System.Collections.Generic.Comparer<T>.Default;

  //    var pivotValue = source[highIndex];
  //    var i = lowIndex;

  //    for (var j = lowIndex; j < highIndex; j++)
  //    {
  //      if (comparer.Compare(source[j], pivotValue) < 0)
  //      {
  //        source.Swap(i, j);
  //        i++;
  //      }
  //    }

  //    source.Swap(i, highIndex);

  //    return i;
  //  }
  //  #endregion QuickSort helper functions
  //}

  //public class SelectionSort<T>
  //{
  //  private System.Collections.Generic.IComparer<T> m_comparer;

  //  public SelectionSort(System.Collections.Generic.IComparer<T>? comparer)
  //    => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;

  //  public void Sort(System.Collections.Generic.IList<T> source)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));

  //    for (var i = 0; i < source.Count - 1; i++)
  //    {
  //      var min = i;
  //      for (var j = i + 1; j < source.Count; j++)
  //        if (m_comparer.Compare(source[j], source[min]) < 0)
  //          min = j;

  //      var x = source[min];
  //      for (var j = min; j > i; j--)
  //        source[j] = source[j - 1];
  //      source[i] = x;
  //    }
  //  }
  //}
}
