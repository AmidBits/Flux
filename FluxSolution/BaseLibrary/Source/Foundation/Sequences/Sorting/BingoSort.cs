namespace Flux.Sorting
{
  /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Bingo_sort"/>
  public sealed class BingoSort<T>
    : ISortableInPlace<T>
  {
    public System.Collections.Generic.IComparer<T> Comparer { get; }

    public BingoSort(System.Collections.Generic.IComparer<T> comparer)
      => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));
    public BingoSort()
      : this(System.Collections.Generic.Comparer<T>.Default)
    { }

    public void SortInPlace(System.Span<T> source)
    {
      var max = source.Length - 1;

      var nextValue = source[max];

      for (var i = max - 1; i >= 0; i--)
        if (Comparer.Compare(source[i], nextValue) > 0)
          nextValue = source[i];

      while (max > 0 && Comparer.Compare(source[max], nextValue) == 0)
        max--;

      while (max > 0)
      {
        var value = nextValue;

        nextValue = source[max];

        for (var i = max - 1; i >= 0; i--)
        {
          if (Comparer.Compare(source[i], value) == 0)
          {
            source.Swap(i, max);

            max--;
          }
          else if (Comparer.Compare(source[i], nextValue) > 0)
            nextValue = source[i];
        }

        while (max > 0 && Comparer.Compare(source[max], nextValue) == 0)
          max--;
      }
    }
  }
}
