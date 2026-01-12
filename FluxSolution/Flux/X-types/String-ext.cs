namespace Flux
{
  public static partial class StringExtensions
  {
    //extension(System.String)
    //{
    //  public static string Subscripts =>
    //    "\u2080\u2081\u2082\u2083\u2084\u2085\u2086\u2087\u2088\u2089" + // ₀₁₂₃₄₅₆₇₈₉
    //    "\u208A\u208B\u208C\u208D\u208E" +                             // ₊₋₌₍₎
    //    "\u2090\u2091\u2092\u2093\u2094" +                             // ₐₑₒₓₔ
    //    "\u2095\u2096\u2097\u2098\u2099" +                             // ₕₖₗₘₙ
    //    "\u209A\u209B\u209C";                                          // ₚₛₜ

    //  public static string Superscripts =>
    //    "\u2070\u00B9\u00B2\u00B3\u2074\u2075\u2076\u2077\u2078\u2079" + // ⁰¹²³⁴⁵⁶⁷⁸⁹
    //    "\u00AA" + // ª  Feminine ordinal indicator (often used as superscript 'a')
    //    "\u00BA" + // º  Masculine ordinal indicator (often used as superscript 'o')
    //    "\u02B0" + // ʰ  Modifier letter small h
    //    "\u02B1" + // ʱ
    //    "\u02B2" + // ʲ
    //    "\u02B3" + // ʳ
    //    "\u02B4" + // ʴ
    //    "\u02B5" + // ʵ
    //    "\u02B6" + // ʶ
    //    "\u02B7" + // ʷ
    //    "\u02B8" + // ʸ
    //    "\u02E0" + // ˠ
    //    "\u02E1" + // ˡ
    //    "\u02E2" + // ˢ
    //    "\u02E3" + // ˣ
    //    "\u02E4" + // ˤ
    //    "\u1D2C" + // ᴬ
    //    "\u1D2D" + // ᴭ
    //    "\u1D2E" + // ᴮ
    //    "\u1D2F" + // ᴯ
    //    "\u1D30" + // ᴰ
    //    "\u1D31" + // ᴱ
    //    "\u1D32" + // ᴲ
    //    "\u1D33" + // ᴳ
    //    "\u1D34" + // ᴴ
    //    "\u1D35" + // ᴵ
    //    "\u1D36" + // ᴶ
    //    "\u1D37" + // ᴷ
    //    "\u1D38" + // ᴸ
    //    "\u1D39" + // ᴹ
    //    "\u1D3A" + // ᴺ
    //    "\u1D3B" + // ᴻ
    //    "\u1D3C" + // ᴼ
    //    "\u1D3D" + // ᴽ
    //    "\u1D3E" + // ᴾ
    //    "\u1D3F" + // ᴿ
    //    "\u1D40" + // ᵀ
    //    "\u1D41" + // ᵁ
    //    "\u1D42" + // ᵂ
    //    "\u1D43" + // ᵃ
    //    "\u1D44" + // ᵄ
    //    "\u1D45" + // ᵅ
    //    "\u1D46" + // ᵆ
    //    "\u1D47" + // ᵇ
    //    "\u1D48" + // ᵈ
    //    "\u1D49" + // ᵉ
    //    "\u1D4A" + // ᵊ
    //    "\u1D4B" + // ᵋ
    //    "\u1D4C" + // ᵌ
    //    "\u1D4D" + // ᵍ
    //    "\u1D4E" + // ᵎ
    //    "\u1D4F" + // ᵏ
    //    "\u1D50" + // ᵐ
    //    "\u1D51" + // ᵑ
    //    "\u1D52" + // ᵒ
    //    "\u1D53" + // ᵓ
    //    "\u1D54" + // ᵔ
    //    "\u1D55" + // ᵕ
    //    "\u1D56" + // ᵖ
    //    "\u1D57" + // ᵗ
    //    "\u1D58" + // ᵘ
    //    "\u1D59" + // ᵙ
    //    "\u1D5A" + // ᵚ
    //    "\u1D5B" + // ᵛ
    //    "\u1D5C" + // ᵜ
    //    "\u1D5D" + // ᵝ
    //    "\u1D5E" + // ᵞ
    //    "\u1D5F" + // ᵟ
    //    "\u1D60" + // ᵠ
    //    "\u1D61" + // ᵡ
    //    "\u2071" + // ⁱ
    //    "\u207A" + // ⁺
    //    "\u207B" + // ⁻
    //    "\u207C" + // ⁼
    //    "\u207D" + // ⁽
    //    "\u207E" + // ⁾
    //    "\u207F" + // ⁿ
    //    "\u2C7D" + // ⱽ
    //    "\uA770" + // ꝰ
    //    "\uAB5C" + // ꭜ
    //    "\uAB5D" + // ꭝ
    //    "\uAB5E" + // ꭞ
    //    "\uAB5F";  // ꭟ
    //}

    extension(System.String source)
    {
      /// <summary>Indicates whether the content of the string is possibly of slavo/germanic origin.</summary>
      public bool ContainsSlavoGermanic()
      {
        System.ArgumentNullException.ThrowIfNullOrEmpty(source);

        return source.Contains('k', System.StringComparison.OrdinalIgnoreCase)
          || source.Contains('w', System.StringComparison.OrdinalIgnoreCase)
          || source.Contains("cz", System.StringComparison.OrdinalIgnoreCase);
      }

      #region Deserialize..

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <returns></returns>
      /// <exception cref="System.InvalidOperationException"></exception>
      public T DeserializeFromJson<T>()
      {
        var o = System.Text.Json.JsonSerializer.Deserialize<T>(source);

        return o ?? throw new System.InvalidOperationException("Unable to parse JSON into an object.");
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <returns></returns>
      /// <exception cref="System.InvalidOperationException"></exception>
      public T DeserializeFromXml<T>()
      {
        var xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
        using var sr = new System.IO.StringReader(source);
        var o = (T)(xs.Deserialize(sr) ?? throw new System.InvalidOperationException($"Unable to serialize string to typeof(T)."));
        return o;
      }

      #endregion

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

      //#region ToSpanMaker

      ///// <summary>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</summary>
      //public SpanMaker<char> ToSpanMaker()
      //  => new(source);

      //#endregion

      //#region ToSpanMakerOfRune

      ///// <summary>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</summary>
      //public SpanMaker<System.Text.Rune> ToSpanMakerOfRune()
      //{
      //  var sm = new SpanMaker<System.Text.Rune>(source.Length);
      //  if (!string.IsNullOrEmpty(source))
      //    foreach (var rune in source.EnumerateRunes())
      //      sm = sm.Append(rune);
      //  return sm;
      //}

      //#endregion

      #region To..

      public System.IO.DirectoryInfo ToDirectoryInfo()
        => new(source.TrimCommonPrefix(['/']).ToString());

      public System.Uri ToUri(System.UriKind uriKind = System.UriKind.RelativeOrAbsolute)
        => new(source, uriKind);

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

    public static System.IO.FileInfo ToFileInfo(this string source)
    {
      source = source.AsSpan().TrimCommonPrefix(c => c is '/' or '\\').TrimCommonSuffix(c => c is '/' or '\\').ToString();
      return new(source);
    }
  }
}
