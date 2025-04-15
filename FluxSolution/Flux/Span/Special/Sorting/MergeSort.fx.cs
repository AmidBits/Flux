namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Sorts the content of the sequence using merge sort.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Merge_sort"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparer"></param>
    public static void MergeSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      Sort(source, 0, source.Length - 1, comparer);

      #region MergeSort helpers.

      // Sort the elements between min and max index (inclusive).
      static void Sort(System.Span<T> source, int minIndex, int maxIndex, System.Collections.Generic.IComparer<T> comparer)
      {
        if (minIndex < maxIndex)
        {
          var centerIndex = minIndex + (maxIndex - minIndex) / 2;

          Sort(source, minIndex, centerIndex, comparer);
          Sort(source, centerIndex + 1, maxIndex, comparer);

          Merge(source, minIndex, centerIndex, maxIndex, comparer);
        }
      }

      static void Merge(System.Span<T> source, int minIndex, int centerIndex, int maxIndex, System.Collections.Generic.IComparer<T> comparer)
      {
        var minIndex2 = centerIndex + 1;

        if (comparer.Compare(source[centerIndex], source[minIndex2]) <= 0)
          return;

        while (minIndex <= centerIndex && minIndex2 <= maxIndex)
        {
          if (comparer.Compare(source[minIndex], source[minIndex2]) <= 0)
            minIndex++;
          else
          {
            var value = source[minIndex2];
            var index = minIndex2;

            while (index != minIndex) // Shift all the elements between element 1 element 2, right by 1.
            {
              source[index] = source[index - 1];
              index--;
            }

            source[minIndex] = value;

            minIndex++;
            centerIndex++;
            minIndex2++;
          }
        }
      }

      #endregion MergeSort helpers.
    }
  }
}
