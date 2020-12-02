namespace Flux
{
  public static partial class SpanSortingEm
  {
    /// <summary>Sorts the content of the sequence using selection sort.</summary>
    public static void ApplySelectionSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SpanSorting.SelectionSort<T>(comparer).SortInPlace((T[])source);
    /// <summary>Sorts the content of the sequence using selection sort.</summary>
    public static void ApplySelectionSort<T>(this System.Collections.Generic.IList<T> source)
      => ApplySelectionSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the sequence using selection sort.</summary>
    public static void ApplySelectionSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SpanSorting.SelectionSort<T>(comparer).SortInPlace(source);
    /// <summary>Sorts the content of the sequence using selection sort.</summary>
    public static void ApplySelectionSort<T>(this System.Span<T> source)
      => ApplySelectionSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace SpanSorting
  {
    /// <summary>Sorts the content of the sequence using selection sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public class SelectionSort<T>
      : ASpanSorting<T>, ISortableInPlace<T>
    {
      public SelectionSort(System.Collections.Generic.IComparer<T> comparer)
        : base(comparer)
      {
      }
      public SelectionSort()
        : this(System.Collections.Generic.Comparer<T>.Default)
      {
      }

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
}
