namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as the content.</summary>
    public static SpanMaker<char> ToSpanMaker(this System.ReadOnlySpan<System.Text.Rune> source, string? prepend = null)
    {
      var sm = new SpanMaker<char>(prepend);
      for (var index = 0; index < source.Length; index++)
        sm = sm.Append(source[index].ToString());
      return sm;
    }
  }
}
