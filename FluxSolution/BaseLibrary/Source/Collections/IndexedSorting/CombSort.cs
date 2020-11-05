namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void ApplyCombSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new IndexedSorting.CombSort<T>(comparer).SortInline(source);
    /// <summary>Sorts the content of the sequence using comb sort.</summary>
    public static void ApplyCombSort<T>(this System.Collections.Generic.IList<T> source)
      => ApplyCombSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void ApplyCombSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new IndexedSorting.CombSort<T>(comparer).SortInline(source);
    /// <summary>Sorts the content of the sequence using comb sort.</summary>
    public static void ApplyCombSort<T>(this System.Span<T> source)
      => ApplyCombSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace IndexedSorting
  {
    /// <summary>Sorts the content of the sequence using comb sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public class CombSort<T>
      : AIndexedSorting<T>
    {
      public CombSort(System.Collections.Generic.IComparer<T> comparer)
        : base(comparer)
      {
      }
      public CombSort()
        : this(System.Collections.Generic.Comparer<T>.Default)
      {
      }

      public override void SortInline(System.Span<T> source)
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
      public override T[] SortToCopy(System.ReadOnlySpan<T> source)
      {
        var target = source.ToArray();
        SortInline(new System.Span<T>(target));
        return target;
      }
    }
  }
}