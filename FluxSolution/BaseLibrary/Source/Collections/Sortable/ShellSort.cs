namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void ShellSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new Sorting.ShellSort<T>(comparer).Sort(source);
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void ShellSort<T>(this System.Span<T> source)
      => ShellSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace Sorting
  {
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Shellsort"/>
    public class ShellSort<T>
    : ISortable<T>
    {
      private System.Collections.Generic.IComparer<T> m_comparer;
      private int[] m_gaps = new int[] { 701, 301, 132, 57, 23, 10, 4, 1 }; // Marcin Ciura's gap sequence.

      public ShellSort(System.Collections.Generic.IComparer<T>? comparer)
        => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;
      public ShellSort()
        : this(null)
      {
      }

      public void Sort(System.Collections.Generic.List<T> source)
      {
        if (source is null) throw new System.ArgumentNullException(nameof(source));

        foreach (var gap in m_gaps)
        {
          for (var i = gap; i < source.Count; i += 1)
          {
            var j = i;

            var temp = source[j];

            while (j >= gap && m_comparer.Compare(source[j - gap], temp) > 0)
            {
              source[j] = source[j - gap];

              j -= gap;
            }

            source[j] = temp;
          }
        }
      }
      public void Sort(System.Span<T> source)
      {
        foreach (var gap in m_gaps)
        {
          for (var i = gap; i < source.Length; i += 1)
          {
            var j = i;

            var temp = source[j];

            while (j >= gap && m_comparer.Compare(source[j - gap], temp) > 0)
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
