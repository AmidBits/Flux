namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Sorts the content of the sequence using insertion sort.</summary>
    public static void ApplyInsertionSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SpanSorting.InsertionSort<T>(comparer).SortInPlace((T[])source);
    /// <summary>Sorts the content of the sequence using insertion sort.</summary>
    public static void ApplyInsertionSort<T>(this System.Collections.Generic.IList<T> source)
      => ApplyInsertionSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the sequence using insertion sort.</summary>
    public static void ApplyInsertionSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SpanSorting.InsertionSort<T>(comparer).SortInPlace(source);
    /// <summary>Sorts the content of the sequence using insertion sort.</summary>
    public static void ApplyInsertionSort<T>(this System.Span<T> source)
      => ApplyInsertionSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace SpanSorting
  {
    /// <summary>Sorts the content of the sequence using insertion sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Insertion_sort"/>
    public class InsertionSort<T>
      : ASpanSorting<T>, ISortableInPlace<T>
    {
      public InsertionSort(System.Collections.Generic.IComparer<T> comparer)
        : base(comparer)
      {
      }
      public InsertionSort()
        : this(System.Collections.Generic.Comparer<T>.Default)
      {
      }

      public void SortInPlace(System.Span<T> source)
      {
        var sourceLength = source.Length;

        for (var i = 1; i < sourceLength; i++)
          for (var j = i; j > 0 && Comparer.Compare(source[j - 1], source[j]) > 0; j--)
            source.Swap(j, j - 1);
      }
    }
  }
}
