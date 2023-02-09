namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new <see cref="System.Text.StringBuilder"/> from the source.</summary>
    public static System.ReadOnlySpan<char> ToSpanChar(this System.ReadOnlySpan<System.Text.Rune> source)
      => source.ToListChar().AsReadOnlySpan();
  }
}
