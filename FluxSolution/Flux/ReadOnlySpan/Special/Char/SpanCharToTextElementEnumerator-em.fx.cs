namespace Flux
{
  public static partial class ReadOnlySpans
  {
    public static SpanCharToTextElementEnumerator EnumerateTextElements(this System.ReadOnlySpan<char> source)
      => new(source);
  }
}
