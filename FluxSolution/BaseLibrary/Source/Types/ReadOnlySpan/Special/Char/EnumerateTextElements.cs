namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    public static SpanCharToTextElementEnumerator EnumerateTextElements(this System.ReadOnlySpan<char> source) => new(source);
  }
}
