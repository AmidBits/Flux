namespace Flux
{
  public static partial class XtendSpan
  {
    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static void BubbleSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new BubbleSort<T>(comparer).Sort(source);
    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static void BubbleSort<T>(this System.Span<T> source)
      => BubbleSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  /// <summary>Sorts the content of the list using bubble sort. Uses the specified comparer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Bubble_sort"/>
  public class BubbleSort<T>
    : ISortable<T>
  {
    private System.Collections.Generic.IComparer<T> m_comparer;

    public BubbleSort(System.Collections.Generic.IComparer<T>? comparer)
      => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;
    public BubbleSort()
      : this(null)
    {
    }

    public void Sort(System.Span<T> source)
    {
      var length = source.Length;

      do
      {
        var newLength = 0;

        for (var i = 1; i < length; i++)
        {
          if (m_comparer.Compare(source[i - 1], source[i]) > 0)
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
