namespace Flux
{
  public static partial class ExtensionMethodsSpan
  {
    /// <summary>
    /// <para>Sorts the content of the sequence using quick sort.</para>
    /// <see href="https://en.wikipedia.org/wiki/Quick_sort"/>
    /// </summary>
    public static void QuickSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      QuickSortImpl(source, 0, source.Length - 1, comparer);

      #region Quick sort helpers

      static void QuickSortImpl(System.Span<T> source, int lowIndex, int highIndex, System.Collections.Generic.IComparer<T> comparer)
      {
        if (lowIndex < highIndex)
        {
          var pivotIndex = QuickPartition(source, lowIndex, highIndex, comparer);

          QuickSortImpl(source, lowIndex, pivotIndex - 1, comparer);
          QuickSortImpl(source, pivotIndex + 1, highIndex, comparer);
        }
      }

      static int QuickPartition(System.Span<T> source, int lowIndex, int highIndex, System.Collections.Generic.IComparer<T> comparer)
      {
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

      #endregion Quick sort helpers
    }
  }
}
