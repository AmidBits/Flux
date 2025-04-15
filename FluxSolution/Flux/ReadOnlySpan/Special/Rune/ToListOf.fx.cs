namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>Creates a new list of <see cref="System.Char"/> from the <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<char> ToListOfChar(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var list = new System.Collections.Generic.List<char>();
      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].ToString());
      return list;
    }

    /// <summary>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as the content.</summary>
    public static System.Collections.Generic.List<string> ToListOfTextElement(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var list = new System.Collections.Generic.List<string>();
      var e = System.Globalization.StringInfo.GetTextElementEnumerator(source.ToListOfChar().AsSpan().ToString());
      while (e.MoveNext())
        if (e.Current.ToString() is string s)
          list.Add(s);
      return list;
    }
  }
}
