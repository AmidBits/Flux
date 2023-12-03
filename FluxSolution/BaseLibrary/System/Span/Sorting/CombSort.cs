namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Sorts the content of the sequence using essentially an improved bubble sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Comb_sort"/>
    public static void CombSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var gap = source.Length;
      var shrinkFactor = 1.3;
      var isSorted = false;

      while (isSorted == false)
      {
        gap = (int)System.Math.Floor(gap / shrinkFactor);

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
