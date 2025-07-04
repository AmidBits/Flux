namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Sorts the content of the sequence using insertion sort.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Insertion_sort"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
    public static void InsertionSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      for (var i = 1; i < source.Length; i++)
      {
        var tmp = source[i];

        var j = i - 1;

        while (j >= 0 && comparer.Compare(source[j], tmp) > 0)
        {
          source[j + 1] = source[j];
          j--;
        }

        source[j + 1] = tmp;
      }
    }
  }
}
