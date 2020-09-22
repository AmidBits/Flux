namespace Flux
{
  public static partial class XtendSpan
  {
    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static void SelectionSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SelectionSort<T>(comparer).Sort(source);
    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static void SelectionSort<T>(this System.Span<T> source)
      => SelectionSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  /// <summary>Sorts the content of the list using selection sort. Uses the specified comparer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
  public class SelectionSort<T>
    : ISortable<T>
  {
    private System.Collections.Generic.IComparer<T> m_comparer;

    public SelectionSort(System.Collections.Generic.IComparer<T>? comparer)
      => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;
    public SelectionSort()
      : this(null)
    {
    }

    public void Sort(System.Span<T> source)
    {
      for (var i = 0; i < source.Length - 1; i++)
      {
        var min = i;
        for (var j = i + 1; j < source.Length; j++)
          if (m_comparer.Compare(source[j], source[min]) < 0)
            min = j;

        var x = source[min];
        for (var j = min; j > i; j--)
          source[j] = source[j - 1];
        source[i] = x;
      }
    }
  }
}
