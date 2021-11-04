namespace Flux.Sorting
{
  /// <summary>Sorts the content of the sequence using quick sort.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Quick_sort"/>
  public sealed class QuickSort<T>
    : ISortableInPlace<T>
  {
    public System.Collections.Generic.IComparer<T> Comparer { get; }

    public QuickSort(System.Collections.Generic.IComparer<T> comparer)
      => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));
    public QuickSort()
      : this(System.Collections.Generic.Comparer<T>.Default)
    { }

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
