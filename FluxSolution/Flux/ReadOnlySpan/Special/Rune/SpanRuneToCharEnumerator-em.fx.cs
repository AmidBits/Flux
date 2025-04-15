namespace Flux
{
  public static partial class ReadOnlySpans
  {
    public static SpanRuneToCharEnumerator EnumerateChars(this System.ReadOnlySpan<System.Text.Rune> source)
      => new(source);
  }
}
