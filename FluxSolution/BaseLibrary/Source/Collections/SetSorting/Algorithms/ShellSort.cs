namespace Flux
{
  public static partial class SortingEm
  {
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void ApplyShellSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SetSorting.ShellSort<T>(comparer).SortInPlace((T[])source);
    /// <summary>Sorts the content of the sequence using shell sort.</summary>
    public static void ApplyShellSort<T>(this System.Collections.Generic.IList<T> source)
      => ApplyShellSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort. This makes the shell sort basically an improved insertion sort.</summary>
    public static void ApplyShellSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SetSorting.ShellSort<T>(comparer).SortInPlace(source);
    /// <summary>Sorts the content of the sequence using shell sort.</summary>
    public static void ApplyShellSort<T>(this System.Span<T> source)
      => ApplyShellSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace SetSorting
  {
    /// <summary>Sorts the content of the sequence using Marcin Ciura's gap sequence, with an inner insertion sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Shellsort"/>
    public class ShellSort<T>
      : ASetSorting<T>, ISortableInPlace<T>
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
        var sourceLength = source.Length;

        foreach (var gap in m_gaps)
        {
          for (var o = gap; o < sourceLength; o++)
          {
            var i = o;

            var tmp = source[i];

            while (i >= gap && Comparer.Compare(source[i - gap], tmp) > 0)
            {
              source[i] = source[i - gap];

              i -= gap;
            }

            source[i] = tmp;
          }
        }
      }
    }
  }
}