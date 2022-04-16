namespace Flux
{
  public static partial class SpanEm
  {
    public static System.Span<T> SwapImpl<T>(this System.Span<T> source, int indexA, int indexB)
    {
      (source[indexB], source[indexA]) = (source[indexA], source[indexB]);

      return source;
    }

    /// <summary>Swap two elements by the specified indices.</summary>
    public static System.Span<T> Swap<T>(this System.Span<T> source, int indexA, int indexB)
    {
      if (source.Length == 0) throw new System.ArgumentException(@"The sequence is empty.");
      if (indexA < 0 || indexA >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(indexA));
      if (indexB < 0 || indexB >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(indexB));

      if (indexA != indexB) // No need to actually swap if the indices are the same.
        return SwapImpl(source, indexA, indexB);

      return source;
    }

    public static System.Span<T> SwapFirstWith<T>(this System.Span<T> source, int index)
    {
      if (index <= 0 && index >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      return SwapImpl(source, 0, index);
    }

    public static System.Span<T> SwapLastWith<T>(this System.Span<T> source, int index)
    {
      var lastIndex = source.Length - 1;

      if (index < 0 && index >= lastIndex) throw new System.ArgumentOutOfRangeException(nameof(index));

      return SwapImpl(source, index, lastIndex);
    }
  }
}
