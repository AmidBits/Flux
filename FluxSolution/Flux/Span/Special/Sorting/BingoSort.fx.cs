namespace Flux
{
  public static partial class Spans
  {
    /// <summary>
    /// <para>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Bingo_sort"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
    public static void BingoSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var max = source.Length - 1;

      var nextValue = source[max];

      for (var i = max - 1; i >= 0; i--)
        if (comparer.Compare(source[i], nextValue) > 0)
          nextValue = source[i];

      while (max > 0 && comparer.Compare(source[max], nextValue) == 0)
        max--;

      while (max > 0)
      {
        var value = nextValue;

        nextValue = source[max];

        for (var i = max - 1; i >= 0; i--)
        {
          if (comparer.Compare(source[i], value) == 0)
          {
            source.Swap(i, max);

            max--;
          }
          else if (comparer.Compare(source[i], nextValue) > 0)
            nextValue = source[i];
        }

        while (max > 0 && comparer.Compare(source[max], nextValue) == 0)
          max--;
      }
    }
  }
}
