namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reverse all ranged characters sort of in-place. Handles surrogates.</summary>
    public static Flux.SpanBuilder<char> Reverse(this ref Flux.SpanBuilder<char> source, int startIndex, int endIndex)
    {
      var s = source.AsSpan();
      s = s.Slice(startIndex, endIndex - startIndex + 1);
      s.Reverse();

      return source;
    }
    /// <summary>Reverse all characters sort of in-place. Handles surrogates.</summary>
    public static Flux.SpanBuilder<char> Reverse(this ref Flux.SpanBuilder<char> source)
      => source.Reverse(0, source.Length - 1);
  }
}
