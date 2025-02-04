namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as the content.</summary>
    public static Flux.SpanMaker<char> ToSpanMakerOfChar(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var sm = new Flux.SpanMaker<char>();
      for (var index = 0; index < source.Length; index++)
        sm = sm.Append(1, source[index].ToString());
      return sm;
    }

    /// <summary>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as the content.</summary>
    public static Flux.SpanMaker<Text.TextElement> ToSpanMakerOfTextElement(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var si = new System.Globalization.StringInfo(source.ToListOfChar().AsSpan().ToString());
      var sm = new Flux.SpanMaker<Text.TextElement>();
      for (var index = 0; index < si.LengthInTextElements; index++)
        sm = sm.Append(1, new Text.TextElement(si.SubstringByTextElements(index, 1)));
      return sm;
    }
  }
}
