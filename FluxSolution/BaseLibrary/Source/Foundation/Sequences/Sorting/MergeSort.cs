namespace Flux.Sorting
{
  /// <summary>Sorts the content of the sequence using merge sort.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Merge_sort"/>
  public class MergeSort<T>
    : ASortable<T>, ISortableInPlace<T>
  {
    public MergeSort(System.Collections.Generic.IComparer<T> comparer)
      : base(comparer)
    { }
    public MergeSort()
      : base()
    { }

    public void SortInPlace(System.Span<T> source)
      => Sort(source, 0, source.Length - 1);

    #region MergeSort helpers.
    // Sort the elements between min and max index (inclusive).
    public void Sort(System.Span<T> source, int minIndex, int maxIndex)
    {
      if (minIndex < maxIndex)
      {
        var centerIndex = minIndex + (maxIndex - minIndex) / 2;

        Sort(source, minIndex, centerIndex);
        Sort(source, centerIndex + 1, maxIndex);

        Merge(source, minIndex, centerIndex, maxIndex);
      }
    }

    private void Merge(System.Span<T> source, int minIndex, int centerIndex, int maxIndex)
    {
      var minIndex2 = centerIndex + 1;

      if (Comparer.Compare(source[centerIndex], source[minIndex2]) <= 0)
        return;

      while (minIndex <= centerIndex && minIndex2 <= maxIndex)
      {
        if (Comparer.Compare(source[minIndex], source[minIndex2]) <= 0)
          minIndex++;
        else
        {
          var value = source[minIndex2];
          var index = minIndex2;

          while (index != minIndex) // Shift all the elements between element 1 element 2, right by 1.
          {
            source[index] = source[index - 1];
            index--;
          }

          source[minIndex] = value;

          minIndex++;
          centerIndex++;
          minIndex2++;
        }
      }
    }
    #endregion MergeSort helpers.
  }
}
