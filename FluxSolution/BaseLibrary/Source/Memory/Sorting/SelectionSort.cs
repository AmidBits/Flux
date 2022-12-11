namespace Flux.Sorting
{
  /// <summary>Sorts the content of the sequence using selection sort.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
  public sealed class SelectionSort<T>
    : ISortableInPlace<T>
  {
    public System.Collections.Generic.IComparer<T> Comparer { get; }

    public SelectionSort(System.Collections.Generic.IComparer<T> comparer)
      => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));
    public SelectionSort()
      : this(System.Collections.Generic.Comparer<T>.Default)
    { }

    
    public void SortInPlace(System.Span<T> source)
    {
      for (var i = 0; i < source.Length - 1; i++)
      {
        var min = i;
        for (var j = i + 1; j < source.Length; j++)
          if (Comparer.Compare(source[j], source[min]) < 0)
            min = j;

        var x = source[min];
        for (var j = min; j > i; j--)
          source[j] = source[j - 1];
        source[i] = x;
      }
    }
  }
}
