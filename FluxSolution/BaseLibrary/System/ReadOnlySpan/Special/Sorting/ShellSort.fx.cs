namespace Flux
{
  public static partial class Fx
  {
    private static readonly int[] ShellSortMarcinCiuraGapSequence = new int[] { 701, 301, 132, 57, 23, 10, 4, 1 }; // Marcin Ciura's gap sequence.

    /// <summary>
    /// <para>Sorts the content of the sequence using Marcin Ciura's gap sequence, with an inner insertion sort.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Shellsort"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparer"></param>
    /// <param name="gapSequence"></param>
    public static void ShellSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T>? comparer = null, int[]? gapSequence = null)
    {
      gapSequence ??= ShellSortMarcinCiuraGapSequence;

      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var sourceLength = source.Length;

      foreach (var gap in gapSequence)
      {
        for (var i = gap; i < sourceLength; i++)
        {
          var j = i;

          var tmp = source[j];

          while (j >= gap && comparer.Compare(source[j - gap], tmp) > 0)
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
