namespace Flux
{
  public static partial class ReadOnlySpans
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
    public static Flux.SpanMaker<string> ToSpanMakerOfTextElement(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var sm = new Flux.SpanMaker<string>();
      var e = System.Globalization.StringInfo.GetTextElementEnumerator(source.ToListOfChar().AsSpan().ToString());
      while (e.MoveNext())
        if (e.Current.ToString() is string s)
          sm = sm.Append(1, s);
      return sm;
    }
  }
}
