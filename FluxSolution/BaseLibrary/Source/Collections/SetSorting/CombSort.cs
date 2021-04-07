namespace Flux
{
  public static partial class SortingEm
  {
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void ApplyCombSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new Sorting.CombSort<T>(comparer).SortInPlace((T[])source);
    /// <summary>Sorts the content of the sequence using comb sort.</summary>
    public static void ApplyCombSort<T>(this System.Collections.Generic.IList<T> source)
      => ApplyCombSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void ApplyCombSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new Sorting.CombSort<T>(comparer).SortInPlace(source);
    /// <summary>Sorts the content of the sequence using comb sort.</summary>
    public static void ApplyCombSort<T>(this System.Span<T> source)
      => ApplyCombSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace Sorting
  {
    /// <summary>Sorts the content of the sequence using essentially an improved bubble sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Comb_sort"/>
    public class CombSort<T>
      : ASortable<T>, ISortableInPlace<T>
    {
      public CombSort(System.Collections.Generic.IComparer<T> comparer)
        : base(comparer)
      {
      }
      public CombSort()
        : this(System.Collections.Generic.Comparer<T>.Default)
      {
      }

      public void SortInPlace(System.Span<T> source)
      {
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
            if (Comparer.Compare(source[i], source[i + gap]) > 0)
            {
              source.Swap(i, i + gap);

              isSorted = false; // If this assignment never happens within the loop, then there have been no swaps and the list is sorted.
            }
          }
        }
      }
    }
  }
}