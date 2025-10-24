namespace Flux
{
  public static partial class XtensionString
  {
    extension(System.String source)
    {
      #region GetTextElements

      /// <summary>
      /// <para>Gets a list of <see cref="Text.TextElement"/>s from <paramref name="source"/>, optionally in a <paramref name="reversed"/> order.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="reversed"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<Text.TextElement> GetTextElements()
      {
        var list = new System.Collections.Generic.List<Text.TextElement>();

        var si = new System.Globalization.StringInfo(source);

        for (var index = 0; index < si.LengthInTextElements; index++)
          list.Add(new Text.TextElement(si.SubstringByTextElements(index, 1)));

        return list;
      }

      #endregion

      #region ToSpanMaker

      /// <summary>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</summary>
      public SpanMaker<char> ToSpanMaker()
        => new(source);

      #endregion

      #region ToSpanMakerOfRune

      /// <summary>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</summary>
      public SpanMaker<System.Text.Rune> ToSpanMakerOfRune()
      {
        var sm = new SpanMaker<System.Text.Rune>(source.Length);
        if (!string.IsNullOrEmpty(source))
          foreach (var rune in source.EnumerateRunes())
            sm = sm.Append(rune);
        return sm;
      }

      #endregion

      #region Wrapping

      /// <summary>Indicates whether the source is already wrapped in the specified characters. E.g. in SQL brackets, or parenthesis.</summary>
      public bool IsWrapped(char left, char right)
        => source is not null && source.Length >= 2 && source[0] == left && source[^1] == right;
      /// <summary>Remove the specified wrapping characters from the source, if they exist. E.g. SQL brackets, or parenthesis.</summary>
      public string Unwrap(char left, char right)
        => IsWrapped(source, left, right) ? source[1..^1] : source;
      /// <summary>Add the specified characters to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped. E.g. in SQL brackets, or parenthesis.</summary>
      public string Wrap(char left, char right, bool force = false)
        => force || !IsWrapped(source, left, right) ? left.ToString() + source + right.ToString() : source;

      /// <summary>Indicates whether the source is already wrapped in the strings.</summary>
      public bool IsWrapped(string left, string right)
        => (source ?? throw new System.ArgumentNullException(nameof(source))).Length >= ((left ?? throw new System.ArgumentNullException(nameof(left))).Length + (right ?? throw new System.ArgumentNullException(nameof(right))).Length) && source.StartsWith(left, System.StringComparison.Ordinal) && source.EndsWith(right, System.StringComparison.Ordinal);
      /// <summary>Remove the specified wrap strings from the source, if they exist.</summary>
      public string Unwrap(string left, string right)
        => IsWrapped(source, left, right) ? source.Substring(left.Length, source.Length - (left.Length + right.Length)) : source;
      /// <summary>Add the specified wrap strings to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped.</summary>
      public string Wrap(string left, string right, bool force = false)
        => force || !IsWrapped(source, left, right) ? left + source + right : source;

      #endregion
    }
  }
}
