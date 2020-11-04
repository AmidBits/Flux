namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Sorts the content of the sequence using insertion sort.</summary>
    public static void InsertionSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new Sorting.InsertionSort<T>(comparer).Sort(source);
    /// <summary>Sorts the content of the sequence using insertion sort.</summary>
    public static void InsertionSort<T>(this System.Span<T> source)
      => InsertionSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace Sorting
  {
    /// <summary>Sorts the content of the sequence using insertion sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Insertion_sort"/>
    public class InsertionSort<T>
      : ASortable<T>, ISortable<T>
    {
      public InsertionSort(System.Collections.Generic.IComparer<T> comparer)
        : base(comparer)
      {
      }
      public InsertionSort()
        : this(System.Collections.Generic.Comparer<T>.Default)
      {
      }

      public void Sort(System.Collections.Generic.List<T> source)
      {
        if (source is null) throw new System.ArgumentNullException(nameof(source));

        var sourceLength = source.Count;

        for (var i = 1; i < sourceLength; i++)
          for (var j = i; j > 0 && Comparer.Compare(source[j - 1], source[j]) > 0; j--)
            source.Swap(j, j - 1);
      }
      public void Sort(System.Span<T> source)
      {
        var sourceLength = source.Length;

        for (var i = 1; i < sourceLength; i++)
          for (var j = i; j > 0 && Comparer.Compare(source[j - 1], source[j]) > 0; j--)
            source.Swap(j, j - 1);
      }
    }
  }
}
