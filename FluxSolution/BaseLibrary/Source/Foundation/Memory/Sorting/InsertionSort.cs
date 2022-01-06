namespace Flux.Sorting
{
  /// <summary>Sorts the content of the sequence using insertion sort.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Insertion_sort"/>
  public sealed class InsertionSort<T>
    : ISortableInPlace<T>
  {
    public System.Collections.Generic.IComparer<T> Comparer { get; }

    public InsertionSort(System.Collections.Generic.IComparer<T> comparer)
      => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));
    public InsertionSort()
      : this(System.Collections.Generic.Comparer<T>.Default)
    { }

    public void SortInPlaceOld(System.Span<T> source)
    {
      var sourceLength = source.Length;

      for (var i = 1; i < sourceLength; i++)
        for (var j = i; j > 0 && Comparer.Compare(source[j - 1], source[j]) > 0; j--)
          source.Swap(j, j - 1);
    }

    public void SortInPlace(System.Span<T> source)
    {
      var sourceLength = source.Length;

      for (var i = 1; i < sourceLength; i++)
      {
        var tmp = source[i];

        var j = i - 1;

        while (j >= 0 && Comparer.Compare(source[j], tmp) > 0)
        {
          source[j + 1] = source[j];
          j--;
        }

        source[j + 1] = tmp;
      }
    }
  }
}
