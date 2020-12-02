namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>Creates a new array with the range [minIndex, maxIndex] (inclusive) of elements in reverse order.</summary>
    public static void Reverse<T>(this System.Span<T> source, int minIndex, int maxIndex)
    {
      if (minIndex < 0 || minIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(minIndex));
      if (maxIndex < 0 || maxIndex >= source.Length || maxIndex < minIndex) throw new System.ArgumentOutOfRangeException(nameof(maxIndex));

      while (minIndex < maxIndex)
      {
        var tmp = source[minIndex];
        source[minIndex++] = source[maxIndex];
        source[maxIndex--] = tmp;
      }
    }
    /// <summary>Creates a new array with all elements in reverse order.</summary>
    public static void Reverse<T>(this System.Span<T> source)
      => Reverse(source, 0, source.Length - 1);

    /// <summary>Creates a new array with the range [minIndex, maxIndex] (inclusive) of elements in reverse order.</summary>
    public static void Reverse<T>(this System.Collections.Generic.IList<T> source, int minIndex, int maxIndex)
      => Reverse((System.Span<T>)(T[])source, minIndex, maxIndex);
    /// <summary>Creates a new array with all elements in reverse order.</summary>
    public static void Reverse<T>(this System.Collections.Generic.IList<T> source)
      => Reverse((System.Span<T>)(T[])source, 0, (source ?? throw new System.ArgumentNullException(nameof(source))).Count - 1);
  }
}
