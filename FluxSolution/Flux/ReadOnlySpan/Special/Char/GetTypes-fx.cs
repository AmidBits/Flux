namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    #region GetChars, GetRunes & GetTextElements

    /// <summary>Creates a new list of <see cref="System.Char"/> from the <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<char> GetChars(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var list = new System.Collections.Generic.List<char>();
      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].ToString());
      return list;
    }

    /// <summary>Creates a new list of <see cref="System.Char"/> from the source.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<char> GetChars(this System.ReadOnlySpan<string> source)
    {
      var list = new System.Collections.Generic.List<char>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index]);

      return list;
    }

    /// <summary>Creates a new list of <see cref="System.Char"/> from the source.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<char> GetChars(this System.ReadOnlySpan<Text.TextElement> source)
    {
      var list = new System.Collections.Generic.List<char>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].AsReadOnlyListOfChar);

      return list;
    }

    /// <summary>
    /// <para>Creates a new list of <see cref="System.Text.Rune"/> from <paramref name="source"/>.</para>
    /// </summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<System.Text.Rune> GetRunes(this System.ReadOnlySpan<char> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();
      foreach (var rune in source.EnumerateRunes())
        list.Add(rune);
      return list;
    }

    /// <summary>Creates a new list of <see cref="System.Text.Rune"/> from the <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<System.Text.Rune> GetRunes(this System.ReadOnlySpan<string> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].AsSpan().GetRunes());

      return list;
    }

    /// <summary>Creates a new list of <see cref="System.Text.Rune"/> from the <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<System.Text.Rune> GetRunes(this System.ReadOnlySpan<Text.TextElement> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].AsReadOnlySpan().GetRunes());

      return list;
    }

    /// <summary>
    /// <para>Creates a new list of <see cref="Text.TextElement"/> from the <paramref name="source"/>.</para>
    /// </summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<Text.TextElement> GetTextElements(this System.ReadOnlySpan<char> source)
    {
      var list = new System.Collections.Generic.List<Text.TextElement>();
      foreach (var range in source.EnumerateTextElements())
        list.Add(new Text.TextElement(source[range].ToString()));
      return list;
    }

    /// <summary>
    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as the content.</para>
    /// </summary>
    public static System.Collections.Generic.List<Text.TextElement> GetTextElements(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var list = new System.Collections.Generic.List<Text.TextElement>();
      var e = System.Globalization.StringInfo.GetTextElementEnumerator(source.GetChars().AsSpan().ToString());
      while (e.MoveNext())
        if (e.Current.ToString() is string s)
          list.Add(new Text.TextElement(s));
      return list;
    }

    ///// <summary>
    ///// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as the content.</para>
    ///// </summary>
    //public static string GetString(this System.ReadOnlySpan<System.Text.Rune> source)
    //{
    //  var sb = new System.Text.StringBuilder();
    //  for (var i = 0; i < source.Length; i++)
    //    sb.Append(source[i].ToString());
    //  return sb.ToString();
    //}

    /// <summary>
    /// <para>Creates a new list of text elements (strings) from the <paramref name="source"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<Text.TextElement> GetTextElements(this string source)
    {
      var list = new System.Collections.Generic.List<Text.TextElement>();
      var e = System.Globalization.StringInfo.GetTextElementEnumerator(source);
      while (e.MoveNext())
        if (e.Current.ToString() is string s && !string.IsNullOrEmpty(s))
          list.Add(new Text.TextElement(s));
      return list;
    }

    #endregion
  }
}
