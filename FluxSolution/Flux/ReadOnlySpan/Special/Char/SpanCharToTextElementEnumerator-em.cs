namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    public static SpanCharToTextElementEnumerator EnumerateTextElements(this System.ReadOnlySpan<char> source)
      => new(source);
  }
}
