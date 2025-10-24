namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    #region ToSpanMaker & ToStringBuilder

    /// <summary>
    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as the content.</para>
    /// </summary>
    public static Flux.SpanMaker<char> ToSpanMakerOfChar(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var sm = new Flux.SpanMaker<char>();
      for (var index = 0; index < source.Length; index++)
        sm = sm.Append(1, source[index].ToString());
      return sm;
    }

    /// <summary>
    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as content.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Flux.SpanMaker<char> ToSpanMakerOfChar(this System.ReadOnlySpan<string> source)
    {
      var sm = new Flux.SpanMaker<char>();
      for (var index = 0; index < source.Length; index++)
        sm = sm.Append(1, source[index].AsSpan().GetRunes());
      return sm;
    }

    /// <summary>
    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as content.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Flux.SpanMaker<char> ToSpanMakerOfChar(this System.ReadOnlySpan<Text.TextElement> source)
    {
      var sm = new Flux.SpanMaker<char>();
      for (var index = 0; index < source.Length; index++)
        sm = sm.Append(1, (System.Collections.Generic.ICollection<char>)source[index].AsReadOnlyListOfChar);
      return sm;
    }

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
    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as content.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Flux.SpanMaker<System.Text.Rune> ToSpanMakerOfRune(this System.ReadOnlySpan<string> source)
    {
      var sm = new Flux.SpanMaker<System.Text.Rune>();
      for (var index = 0; index < source.Length; index++)
        sm = sm.Append(1, source[index].AsSpan().GetRunes());
      return sm;
    }

    /// <summary>
    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as content.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Flux.SpanMaker<System.Text.Rune> ToSpanMakerOfRune(this System.ReadOnlySpan<Text.TextElement> source)
    {
      var sm = new Flux.SpanMaker<System.Text.Rune>();
      for (var index = 0; index < source.Length; index++)
        sm = sm.Append(1, source[index].AsReadOnlySpan().GetRunes());
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
      foreach (var range in source.EnumerateTextElements())
        sm = sm.Append(1, new Text.TextElement(source[range].ToString()));
      return sm;
    }

    /// <summary>
    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as the content.</para>
    /// </summary>
    public static Flux.SpanMaker<string> ToSpanMakerOfTextElement(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var sm = new Flux.SpanMaker<string>();
      var e = System.Globalization.StringInfo.GetTextElementEnumerator(source.GetChars().AsSpan().ToString());
      while (e.MoveNext())
        if (e.Current.ToString() is string s)
          sm = sm.Append(1, s);
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

    /// <summary>
    /// <para>Creates a new <see cref="System.Text.StringBuilder"/> with all characters from <paramref name="source"/>.</para>
    /// </summary>
    public static System.Text.StringBuilder ToStringBuilder(this System.ReadOnlySpan<char> source)
      => new System.Text.StringBuilder().Append(source);

    #endregion
  }
}
