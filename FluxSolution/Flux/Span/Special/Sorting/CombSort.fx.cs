namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Sorts the content of the sequence using essentially an improved bubble sort.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Comb_sort"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
    public static void CombSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var gap = source.Length;
      var shrinkFactor = 1.3;
      var isSorted = false;

      while (isSorted == false)
      {
        gap = (int)double.Floor(gap / shrinkFactor);

        if (gap <= 1)
        {
          gap = 1;

          isSorted = true; // No swaps this pass, we are done.
        }

        // A single "comb" over the input list
        for (var i = 0; i + gap < source.Length; i++) // See Shell sort for a similar idea
        {
          if (comparer.Compare(source[i], source[i + gap]) > 0)
          {
            source.Swap(i, i + gap);

            isSorted = false; // If this assignment never happens within the loop, then there have been no swaps and the list is sorted.
          }
        }
      }
    }
  }
}
