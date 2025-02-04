namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</summary>
    public static Flux.SpanMaker<System.Text.Rune> ToSpanMakerOfRune(this System.ReadOnlySpan<char> source)
    {
      var sm = new Flux.SpanMaker<System.Text.Rune>();
      foreach (var rune in source.EnumerateRunes())
        sm = sm.Append(1, rune);
      return sm;
    }

    /// <summary>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</summary>
    public static Flux.SpanMaker<Text.TextElement> ToSpanMakerOfTextElement(this System.ReadOnlySpan<char> source)
    {
      var sm = new Flux.SpanMaker<Text.TextElement>();
      foreach (var textElement in source.EnumerateTextElements())
        sm = sm.Append(1, textElement);
      return sm;
    }
  }
}
