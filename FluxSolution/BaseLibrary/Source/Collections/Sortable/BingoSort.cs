namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void BingoSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new Sorting.BingoSort<T>(comparer).Sort(source);
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    public static void BingoSort<T>(this System.Span<T> source)
      => BingoSort(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace Sorting
  {
    /// <summary>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public class BingoSort<T>
      : ASortable<T>, ISortable<T>
    {
      public BingoSort(System.Collections.Generic.IComparer<T> comparer)
        : base(comparer)
      {
      }
      public BingoSort()
        : this(System.Collections.Generic.Comparer<T>.Default)
      {
      }

      public void Sort(System.Collections.Generic.List<T> source)
      {
        if (source is null) throw new System.ArgumentNullException(nameof(source));

        var max = source.Count - 1;

        var nextValue = source[max];

        for (var i = max - 1; i >= 0; i--)
          if (Comparer.Compare(source[i], nextValue) > 0)
            nextValue = source[i];

        while (max > 0 && Comparer.Compare(source[max], nextValue) == 0) max--;

        while (max > 0)
        {
          var value = nextValue;
          nextValue = source[max];

          for (var i = max - 1; i >= 0; i--)
            if (Comparer.Compare(source[i], value) == 0)
            {
              source.Swap(i, max);
              max--;
            }
            else if (Comparer.Compare(source[i], nextValue) > 0)
              nextValue = source[i];

          while (max > 0 && Comparer.Compare(source[max], nextValue) == 0) max--;
        }
      }
      public void Sort(System.Span<T> source)
      {
        var max = source.Length - 1;

        var nextValue = source[max];

        for (var i = max - 1; i >= 0; i--)
          if (Comparer.Compare(source[i], nextValue) > 0)
            nextValue = source[i];

        while (max > 0 && Comparer.Compare(source[max], nextValue) == 0) max--;

        while (max > 0)
        {
          var value = nextValue;
          nextValue = source[max];

          for (var i = max - 1; i >= 0; i--)
            if (Comparer.Compare(source[i], value) == 0)
            {
              source.Swap(i, max);
              max--;
            }
            else if (Comparer.Compare(source[i], nextValue) > 0)
              nextValue = source[i];

          while (max > 0 && Comparer.Compare(source[max], nextValue) == 0) max--;
        }
      }
    }
  }
}
