namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Creates a new list of <see cref="System.Text.Rune"/> from <paramref name="source"/>.</para>
    /// </summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<System.Text.Rune> ToListOfRune(this System.ReadOnlySpan<char> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();
      foreach (var rune in source.EnumerateRunes())
        list.Add(rune);
      return list;
    }

    /// <summary>
    /// <para>Creates a new list of <see cref="Text.TextElement"/> from the <paramref name="source"/>.</para>
    /// </summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<Text.TextElement> ToListOfTextElement(this System.ReadOnlySpan<char> source)
    {
      var list = new System.Collections.Generic.List<Text.TextElement>();
      foreach (var textElement in source.EnumerateTextElements())
        list.Add(textElement);
      return list;
    }

    /// <summary>
    /// <para>Creates a new list of text elements (strings) from the <paramref name="source"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<string> ToListOfTextElements(this string source)
    {
      var list = new System.Collections.Generic.List<string>();
      var e = System.Globalization.StringInfo.GetTextElementEnumerator(source);
      while (e.MoveNext())
        if (e.Current.ToString() is string s)
          list.Add(s);
      return list;
    }
  }
}
