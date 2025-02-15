namespace Flux
{
  public static partial class Fx
  {
    public static SpanRuneToCharEnumerator EnumerateChars(this System.ReadOnlySpan<System.Text.Rune> source)
      => new(source);
  }
}
