namespace Flux
{
  public static partial class XtendSpan
  {
    /// <summary>Sorts the content of the list using bingo sort which is a variant of selection sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public static void BingoSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new BingoSort<T>(comparer).Sort(source);
    /// <summary>Sorts the content of the list using bingo sort which is a variant of selection sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public static void BingoSort<T>(this System.Span<T> source)
      => BingoSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  /// <summary>Sorts the content of the list using bingo sort which is a variant of selection sort. Uses the specified comparer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
  public class BingoSort<T>
    : ISortable<T>
  {
    private System.Collections.Generic.IComparer<T> m_comparer;

    public BingoSort(System.Collections.Generic.IComparer<T>? comparer)
      => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;
    public BingoSort()
      : this(null)
    {
    }

    public void Sort(System.Span<T> source)
    {
      var max = source.Length - 1;

      var nextValue = source[max];

      for (var i = max - 1; i >= 0; i--)
        if (m_comparer.Compare(source[i], nextValue) > 0)
          nextValue = source[i];

      while (max > 0 && m_comparer.Compare(source[max], nextValue) == 0) max--;

      while (max > 0)
      {
        var value = nextValue;
        nextValue = source[max];

        for (var i = max - 1; i >= 0; i--)
          if (m_comparer.Compare(source[i], value) == 0)
          {
            source.Swap(i, max);
            max--;
          }
          else if (m_comparer.Compare(source[i], nextValue) > 0)
            nextValue = source[i];

        while (max > 0 && m_comparer.Compare(source[max], nextValue) == 0) max--;
      }
    }
  }
}
