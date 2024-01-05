namespace Flux
{
  public static partial class Fx
  {
    /// <summary>In-place swap of two elements by the specified indices.</summary>
    public static System.Span<T> Swap<T>(this System.Span<T> source, int indexA, int indexB)
    {
      if (indexA != indexB) // No need to actually swap if the indices are the same.
        (source[indexB], source[indexA]) = (source[indexA], source[indexB]);

      return source;
    }

    /// <summary>In-place swap of two elements by the specified index and the first element.</summary>
    public static System.Span<T> SwapFirstWith<T>(this System.Span<T> source, int index) => source.Swap(0, index);

    /// <summary>In-place swap of two elements by the specified index and the last element.</summary>
    public static System.Span<T> SwapLastWith<T>(this System.Span<T> source, int index) => source.Swap(index, source.Length - 1);
  }
}
