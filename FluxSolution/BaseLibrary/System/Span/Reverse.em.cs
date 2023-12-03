namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reverse all elements in the index range (<paramref name="startIndex"/>, <paramref name="endIndex"/>) of the <paramref name="source"/>.</summary>
    public static System.Span<T> ReverseRange<T>(ref this System.Span<T> source, int startIndex, int endIndex)
    {
      if (startIndex < 0 || startIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < 0 || endIndex >= source.Length || endIndex <= startIndex) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      for (; startIndex < endIndex; startIndex++, endIndex--)
        source.Swap(startIndex, endIndex);

      return source;
    }
  }
}
