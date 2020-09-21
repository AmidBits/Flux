namespace Flux
{
  public static partial class XtendSpan
  {
    /// <summary>Creates a new array with the range [minIndex, maxIndex] (inclusive) of elements in reverse order.</summary>
    public static void Reverse<T>(this System.Span<T> source, int minIndex, int maxIndex)
    {
      if (minIndex < 0 || minIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(minIndex));
      if (maxIndex < 0 || maxIndex >= source.Length || maxIndex < minIndex) throw new System.ArgumentOutOfRangeException(nameof(maxIndex));

      for (; minIndex < maxIndex; minIndex++, maxIndex--)
      {
        var tmp = source[minIndex];
        source[minIndex] = source[maxIndex];
        source[maxIndex] = tmp;
      }
    }
  }
}
