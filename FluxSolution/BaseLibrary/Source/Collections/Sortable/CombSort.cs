namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void CombSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new Sorting.CombSort<T>(comparer).Sort(source);
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void CombSort<T>(this System.Span<T> source)
      => CombSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace Sorting
  {
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public class CombSort<T>
    : ISortable<T>
    {
      private System.Collections.Generic.IComparer<T> m_comparer;

      public CombSort(System.Collections.Generic.IComparer<T>? comparer)
        => m_comparer = comparer ?? System.Collections.Generic.Comparer<T>.Default;
      public CombSort()
        : this(null)
      {
      }

      public void Sort(System.Collections.Generic.List<T> source)
      {
        if (source is null) throw new System.ArgumentNullException(nameof(source));

        var gapSize = source.Count;
        var shrinkFactor = 1.3;
        var isSorted = false;

        while (isSorted == false)
        {
          gapSize = (int)System.Math.Floor(gapSize / shrinkFactor);

          if (gapSize <= 1)
          {
            gapSize = 1;
            isSorted = true; // No swaps this pass, we are done.
          }

          // A single "comb" over the input list
          for (var i = 0; i + gapSize < source.Count; i++) // See Shell sort for a similar idea
          {
            if (m_comparer.Compare(source[i], source[i + gapSize]) > 0)
            {
              source.Swap(i, i + gapSize);
              isSorted = false; // If this assignment never happens within the loop, then there have been no swaps and the list is sorted.
            }
          }
        }
      }
      public void Sort(System.Span<T> source)
      {
        var gapSize = source.Length;
        var shrinkFactor = 1.3;
        var isSorted = false;

        while (isSorted == false)
        {
          gapSize = (int)System.Math.Floor(gapSize / shrinkFactor);

          if (gapSize <= 1)
          {
            gapSize = 1;
            isSorted = true; // No swaps this pass, we are done.
          }

          // A single "comb" over the input list
          for (var i = 0; i + gapSize < source.Length; i++) // See Shell sort for a similar idea
          {
            if (m_comparer.Compare(source[i], source[i + gapSize]) > 0)
            {
              source.Swap(i, i + gapSize);
              isSorted = false; // If this assignment never happens within the loop, then there have been no swaps and the list is sorted.
            }
          }
        }
      }
    }
  }
}