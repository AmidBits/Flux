namespace Flux
{
  public static partial class Fx
  {
    public static SpanCharToTextElementEnumerator EnumerateTextElements(this System.ReadOnlySpan<char> source)
      => new(source);
  }
}
