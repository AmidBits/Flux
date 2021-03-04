namespace Flux
{
  public static partial class SortingEm
  {
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void ApplyCombSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SetSorting.CombSort<T>(comparer).SortInPlace((T[])source);
    /// <summary>Sorts the content of the sequence using comb sort.</summary>
    public static void ApplyCombSort<T>(this System.Collections.Generic.IList<T> source)
      => ApplyCombSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void ApplyCombSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new SetSorting.CombSort<T>(comparer).SortInPlace(source);
    /// <summary>Sorts the content of the sequence using comb sort.</summary>
    public static void ApplyCombSort<T>(this System.Span<T> source)
      => ApplyCombSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace SetSorting
  {
    /// <summary>Sorts the content of the sequence using essentially an improved bubble sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Comb_sort"/>
    public class CombSort<T>
      : ASetSorting<T>, ISortableInPlace<T>
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
            if (Comparer.Compare(source[i], source[i + gapSize]) > 0)
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