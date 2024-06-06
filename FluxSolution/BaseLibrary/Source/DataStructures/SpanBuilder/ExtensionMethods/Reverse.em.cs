namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reverse all ranged elements in-place.</summary>
    public static Flux.SpanBuilder<T> Reverse<T>(this Flux.SpanBuilder<T> source, int startIndex, int endIndex)
    {
      if (startIndex < 0 || startIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < startIndex || endIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      for (; startIndex < endIndex; startIndex++, endIndex--)
        source.Swap(startIndex, endIndex);

      return source;
    }
    /// <summary>Reverse all elements in-place.</summary>
    public static Flux.SpanBuilder<T> Reverse<T>(this Flux.SpanBuilder<T> source)
      => source.Reverse(0, source.Length - 1);
  }
}
