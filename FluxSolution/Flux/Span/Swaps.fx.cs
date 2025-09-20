namespace Flux
{
  public static partial class Spans
  {
    #region Swap

    /// <summary>In-place swap of two elements by the specified indices.</summary>
    public static bool Swap<T>(this System.Span<T> source, int indexA, int indexB)
    {
      if ((indexA != indexB) is var isUnequal && isUnequal) // No need to actually swap if the indices are the same.
        (source[indexB], source[indexA]) = (source[indexA], source[indexB]);

      return isUnequal;
    }

    /// <summary>In-place swap of two elements by the specified index and the first element.</summary>
    public static bool SwapFirstWith<T>(this System.Span<T> source, int index)
      => source.Swap(0, index);

    /// <summary>In-place swap of two elements by the specified index and the last element.</summary>
    public static bool SwapLastWith<T>(this System.Span<T> source, int index)
      => source.Swap(index, source.Length - 1);

    #endregion
  }
}
