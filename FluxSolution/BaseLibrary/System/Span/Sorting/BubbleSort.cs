namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Sorts the content of the sequence using an optimized version.</para>
    /// <see href="https://en.wikipedia.org/wiki/Bubble_sort"/>
    /// </summary>
    public static void BubbleSort<T>(this System.Span<T> source, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var length = source.Length;

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
  }
}
