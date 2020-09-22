namespace Flux
{
  public static partial class XtendSpan
  {
    /// <summary>Sorts the content of the sequence using quick sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quick_sort"/>
    public static void QuickSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new QuickSort<T>(comparer).Sort(source);
    /// <summary>Sorts the content of the sequence using quick sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quick_sort"/>
    public static void QuickSort<T>(this System.Span<T> source)
      => QuickSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  /// <summary>Sorts the content of the sequence using quick sort.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Quick_sort"/>
  public class QuickSort<T>
    : ISortable<T>
  {
    private System.Collections.Generic.IComparer<T> m_comparer;

    public QuickSort(System.Collections.Generic.IComparer<T>? comparer)
      => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;
    public QuickSort()
      : this(null)
    {
    }

    //public void Sort(System.Collections.Generic.List<T> source)
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  QuickSortImpl(source, 0, source.Count - 1);
    //}
    public void Sort(System.Span<T> source)
    {
      QuickSortImpl(source, 0, source.Length - 1);
    }

    #region QuickSort helper functions
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
        if (m_comparer.Compare(source[j], pivotValue) < 0)
        {
          source.Swap(i, j);
          i++;
        }
      }

      source.Swap(i, highIndex);

      return i;
    }
    #endregion QuickSort helper functions
  }
}
