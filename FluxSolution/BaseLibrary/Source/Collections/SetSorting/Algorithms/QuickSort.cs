namespace Flux
{
  public static partial class SortingEm
  {
    /// <summary>Sorts the content of the sequence using quick sort.</summary>
    public static void ApplyQuickSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new Sorting.QuickSort<T>(comparer).SortInPlace((T[])source);
    /// <summary>Sorts the content of the sequence using quick sort.</summary>
    public static void ApplyQuickSort<T>(this System.Collections.Generic.IList<T> source)
      => ApplyQuickSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the sequence using quick sort.</summary>
    public static void ApplyQuickSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new Sorting.QuickSort<T>(comparer).SortInPlace(source);
    /// <summary>Sorts the content of the sequence using quick sort.</summary>
    public static void ApplyQuickSort<T>(this System.Span<T> source)
      => ApplyQuickSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace Sorting
  {
    /// <summary>Sorts the content of the sequence using quick sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quick_sort"/>
    public class QuickSort<T>
      : ASortable<T>, ISortableInPlace<T>
    {
      public QuickSort(System.Collections.Generic.IComparer<T> comparer)
        : base(comparer)
      {
      }
      public QuickSort()
        : this(System.Collections.Generic.Comparer<T>.Default)
      {
      }

      public void SortInPlace(System.Span<T> source)
        => QuickSortImpl(source, 0, source.Length - 1);

      #region Quick sort helpers
      private void QuickSortImpl(System.Span<T> source, int lowIndex, int highIndex)
      {
        if (lowIndex < highIndex)
        {
          var pivotIndex = QuickPartition(source, lowIndex, highIndex);

          QuickSortImpl(source, lowIndex, pivotIndex - 1);
          QuickSortImpl(source, pivotIndex + 1, highIndex);
        }
      }

      private int QuickPartition(System.Span<T> source, int lowIndex, int highIndex)
      {
        var pivotValue = source[highIndex];
        var i = lowIndex;

        for (var j = lowIndex; j < highIndex; j++)
        {
          if (Comparer.Compare(source[j], pivotValue) < 0)
          {
            source.Swap(i, j);
            i++;
          }
        }

        source.Swap(i, highIndex);

        return i;
      }
      #endregion Quick sort helpers
    }
  }
}
