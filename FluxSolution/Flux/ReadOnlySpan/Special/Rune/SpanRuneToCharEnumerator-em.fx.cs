namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    public static SpanRuneToCharEnumerator EnumerateChars(this System.ReadOnlySpan<System.Text.Rune> source)
      => new(source);
  }
}
