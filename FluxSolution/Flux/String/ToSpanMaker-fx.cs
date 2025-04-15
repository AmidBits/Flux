namespace Flux
{
  public static partial class Strings
  {
    /// <summary>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</summary>
    public static SpanMaker<char> ToSpanMaker(this string source)
      => new(source);
  }
}
