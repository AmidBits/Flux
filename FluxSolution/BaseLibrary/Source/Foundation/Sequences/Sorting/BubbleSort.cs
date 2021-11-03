namespace Flux.Sorting
{
  /// <summary>Sorts the content of the sequence using an optimized version.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Bubble_sort"/>
  public class BubbleSort<T>
    : ISortableInPlace<T>
  {
    public System.Collections.Generic.IComparer<T> Comparer { get; }

    public BubbleSort(System.Collections.Generic.IComparer<T> comparer)
      => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));
    public BubbleSort()
      : this(System.Collections.Generic.Comparer<T>.Default)
    { }

    public void SortInPlace(System.Span<T> source)
    {
      var length = source.Length;

      do
      {
        var newLength = 0;

        for (var i = 1; i < length; i++)
        {
          if (Comparer.Compare(source[i - 1], source[i]) > 0)
          {
            source.Swap(i - 1, i);

            newLength = i;
          }
        }

        length = newLength;
      }
      while (length > 1);
    }
  }
}
