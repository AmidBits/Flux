namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Sorts the content of the sequence using selection sort.</summary>
    public static void SelectionSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new Sorting.SelectionSort<T>(comparer).Sort(source);
    /// <summary>Sorts the content of the sequence using selection sort.</summary>
    public static void SelectionSort<T>(this System.Span<T> source)
      => SelectionSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace Sorting
  {
    /// <summary>Sorts the content of the sequence using selection sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public class SelectionSort<T>
      : ASortable<T>, ISortable<T>
    {
      public SelectionSort(System.Collections.Generic.IComparer<T> comparer)
        : base(comparer)
      {
      }
      public SelectionSort()
        : this(System.Collections.Generic.Comparer<T>.Default)
      {
      }

      public void Sort(System.Collections.Generic.List<T> source)
      {
        if (source is null) throw new System.ArgumentNullException(nameof(source));

        for (var i = 0; i < source.Count - 1; i++)
        {
          var min = i;
          for (var j = i + 1; j < source.Count; j++)
            if (Comparer.Compare(source[j], source[min]) < 0)
              min = j;

          var x = source[min];
          for (var j = min; j > i; j--)
            source[j] = source[j - 1];
          source[i] = x;
        }
      }
      public void Sort(System.Span<T> source)
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
