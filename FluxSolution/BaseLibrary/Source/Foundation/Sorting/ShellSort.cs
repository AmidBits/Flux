namespace Flux.Sorting
{
  /// <summary>Sorts the content of the sequence using Marcin Ciura's gap sequence, with an inner insertion sort.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Shellsort"/>
  public class ShellSort<T>
    : ASortable<T>, ISortableInPlace<T>
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
        for (var i = gap; i < sourceLength; i++)
        {
          var j = i;

          var tmp = source[j];

          while (j >= gap && Comparer.Compare(source[j - gap], tmp) > 0)
          {
            source[j] = source[j - gap];

            j -= gap;
          }

          source[j] = tmp;
        }
      }
    }
  }
}
