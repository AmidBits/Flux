namespace Flux
{
  public static partial class ExtensionMethodsSpan
  {
    /// <summary>Sorts the content of the sequence using selection sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Selection_sort"/>
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
