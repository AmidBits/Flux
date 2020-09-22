namespace Flux
{
  public static partial class XtendSpan
  {
    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static void InsertionSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new InsertionSort<T>(comparer).Sort(source);
    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static void InsertionSort<T>(this System.Span<T> source)
      => InsertionSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  /// <summary>Sorts the content of the list using bubble sort. Uses the specified comparer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Insertion_sort"/>
  public class InsertionSort<T>
    : ISortable<T>
  {
    private System.Collections.Generic.IComparer<T> m_comparer;

    public InsertionSort(System.Collections.Generic.IComparer<T>? comparer)
      => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;
    public InsertionSort()
      : this(null)
    {
    }

    public void Sort(System.Span<T> source)
    {
      var sourceLength = source.Length;

      for (var i = 1; i < sourceLength; i++)
        for (var j = i; j > 0 && m_comparer.Compare(source[j - 1], source[j]) > 0; j--)
          source.Swap(j, j - 1);
    }
  }
}
