namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Sorts the content of the sequence using selection sort.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Selection_sort"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
    public static void SelectionSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      for (var i = 0; i < source.Length - 1; i++)
      {
        var min = i;
        for (var j = i + 1; j < source.Length; j++)
          if (comparer.Compare(source[j], source[min]) < 0)
            min = j;

        var x = source[min];
        for (var j = min; j > i; j--)
          source[j] = source[j - 1];
        source[i] = x;
      }
    }
  }
}
