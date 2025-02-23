namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Flux.SpanMaker<System.Text.Rune> ToSpanMakerOfRune(this System.ReadOnlySpan<char> source)
    {
      var sm = new Flux.SpanMaker<System.Text.Rune>();
      foreach (var rune in source.EnumerateRunes())
        sm = sm.Append(1, rune);
      return sm;
    }

    /// <summary>
    /// <para>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Flux.SpanMaker<Text.TextElement> ToSpanMakerOfTextElement(this System.ReadOnlySpan<char> source)
    {
      var sm = new Flux.SpanMaker<Text.TextElement>();
      foreach (var textElement in source.EnumerateTextElements())
        sm = sm.Append(1, textElement);
      return sm;
    }

    /// <summary>
    /// <para>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static SpanMaker<string> ToSpanMakerOfTextElements(this string source)
    {
      var sm = new SpanMaker<string>();
      var e = System.Globalization.StringInfo.GetTextElementEnumerator(source);
      while (e.MoveNext())
        if (e.Current.ToString() is string s)
          sm.Append(1, s);
      return sm;
    }
  }
}
