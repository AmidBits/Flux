namespace Flux
{
  public static partial class XtendSequencing
  {
    /// <summary>Sorts the content of the sequence using insertion sort.</summary>
    public static void InsertionSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new InsertionSort<T>(comparer).Sort(source);
    /// <summary>Sorts the content of the sequence using insertion sort.</summary>
    public static void InsertionSort<T>(this System.Span<T> source)
      => InsertionSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  /// <summary>Sorts the content of the sequence using insertion sort.</summary>
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

    public void Sort(System.Collections.Generic.List<T> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sourceLength = source.Count;

      for (var i = 1; i < sourceLength; i++)
        for (var j = i; j > 0 && m_comparer.Compare(source[j - 1], source[j]) > 0; j--)
          source.Swap(j, j - 1);
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
