namespace Flux
{
  public static partial class SpanSortingEm
  {
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void ApplyShellSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SpanSorting.ShellSort<T>(comparer).SortInPlace((T[])source);
    /// <summary>Sorts the content of the sequence using shell sort.</summary>
    public static void ApplyShellSort<T>(this System.Collections.Generic.IList<T> source)
      => ApplyShellSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void ApplyShellSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SpanSorting.ShellSort<T>(comparer).SortInPlace(source);
    /// <summary>Sorts the content of the sequence using shell sort.</summary>
    public static void ApplyShellSort<T>(this System.Span<T> source)
      => ApplyShellSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace SpanSorting
  {
    /// <summary>Sorts the content of the sequence using shell sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Shellsort"/>
    public class ShellSort<T>
      : ASpanSorting<T>, ISortableInPlace<T>
    {
      private readonly int[] m_gaps = new int[] { 701, 301, 132, 57, 23, 10, 4, 1 }; // Marcin Ciura's gap sequence.

      public ShellSort(System.Collections.Generic.IComparer<T> comparer)
        : base(comparer)
      {
      }
      public ShellSort()
        : this(System.Collections.Generic.Comparer<T>.Default)
      {
      }

      public void SortInPlace(System.Span<T> source)
      {
        foreach (var gap in m_gaps)
        {
          for (var i = gap; i < source.Length; i += 1)
          {
            var j = i;

            var temp = source[j];

            while (j >= gap && Comparer.Compare(source[j - gap], temp) > 0)
            {
              source[j] = source[j - gap];

              j -= gap;
            }

            source[j] = temp;
          }
        }
      }
    }
  }
}
