namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Sorts the content of the list using bingo sort which is a variant of selection sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public static void BingoSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var max = source.Count - 1;

      var nextValue = source[max];

      for (var i = max - 1; i >= 0; i--)
        if (comparer.Compare(source[i], nextValue) > 0)
          nextValue = source[i];

      while (max > 0 && comparer.Compare(source[max], nextValue) == 0) max--;

      while (max > 0)
      {
        var value = nextValue;
        nextValue = source[max];

        for (var i = max - 1; i >= 0; i--)
          if (comparer.Compare(source[i], value) == 0)
          {
            source.Swap(i, max);
            max--;
          }
          else if (comparer.Compare(source[i], nextValue) > 0)
            nextValue = source[i];

        while (max > 0 && comparer.Compare(source[max], nextValue) == 0) max--;
      }
    }
    /// <summary>Sorts the content of the list using bingo sort which is a variant of selection sort. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public static void BingoSort<T>(this System.Collections.Generic.IList<T> source)
      => BingoSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the list using bubble sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bubble_sort"/>
    public static void BubbleSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var length = source.Count;

      do
      {
        var newLength = 0;

        for (var i = 1; i < length; i++)
        {
          if (comparer.Compare(source[i - 1], source[i]) > 0)
          {
            source.Swap(i - 1, i);
            newLength = i;
          }
        }

        length = newLength;
      }
      while (length > 1);
    }
    /// <summary>Sorts the content of the list using bubble sort. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bubble_sort"/>
    public static void BubbleSort<T>(this System.Collections.Generic.IList<T> source)
    => BubbleSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the list using bubble sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Insertion_sort"/>
    public static void InsertionSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var sourceLength = source.Count;

      for (var i = 1; i < sourceLength; i++)
        for (var j = i; j > 0 && comparer.Compare(source[j - 1], source[j]) > 0; j--)
          source.Swap(j, j - 1);
    }
    /// <summary>Sorts the content of the list using bubble sort. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Insertion_sort"/>
    public static void InsertionSort<T>(this System.Collections.Generic.IList<T> source)
      => InsertionSort(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Sorts the content of the list using quick sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quick_sort"/>
    public static void QuickSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
      => QuickSort(source, comparer, 0, (source ?? throw new System.ArgumentNullException(nameof(source))).Count - 1);
    /// <summary>Sorts the content of the list using quick sort. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quick_sort"/>
    public static void QuickSort<T>(this System.Collections.Generic.IList<T> source)
      => QuickSort(source, System.Collections.Generic.Comparer<T>.Default);

    internal static void QuickSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer, int lowIndex, int highIndex)
    {
      if (lowIndex < highIndex)
      {
        var pivotIndex = QuickPartition(source, comparer, lowIndex, highIndex);

        QuickSort(source, comparer, lowIndex, pivotIndex - 1);
        QuickSort(source, comparer, pivotIndex + 1, highIndex);
      }
    }
    internal static int QuickPartition<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer, int lowIndex, int highIndex)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var pivotValue = source[highIndex];
      var i = lowIndex;

      for (var j = lowIndex; j < highIndex; j++)
      {
        if (comparer.Compare(source[j], pivotValue) < 0)
        {
          source.Swap(i, j);
          i++;
        }
      }

      source.Swap(i, highIndex);

      return i;
    }

    /// <summary>Sorts the content of the list using selection sort. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public static void SelectionSort<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      for (var i = 0; i < source.Count - 1; i++)
      {
        var min = i;
        for (var j = i + 1; j < source.Count; j++)
          if (comparer.Compare(source[j], source[min]) < 0)
            min = j;

        var x = source[min];
        for (var j = min; j > i; j--)
          source[j] = source[j - 1];
        source[i] = x;
      }
    }
    /// <summary>Sorts the content of the list using selection sort. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
    public static void SelectionSort<T>(this System.Collections.Generic.IList<T> source)
      => SelectionSort(source, System.Collections.Generic.Comparer<T>.Default);
  }
}
