namespace Flux
{
  public static partial class StringExtensions
  {
    extension(System.String source)
    {
      /// <summary>
      /// <para>Indicates whether the content of the string is possibly of slavo/germanic origin.</para>
      /// </summary>
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

      public System.Collections.Generic.IEnumerable<System.ReadOnlyMemory<char>> EnumerateLines()
      {
        var start = 0;

        for (var i = 0; i < source.Length; i++)
        {
          if (source[i] == '\r')
          {
            yield return source.AsMemory(start, i - start);

            if (i + 1 < source.Length && source[i + 1] == '\n')
              i++;

            start = i + 1;
          }
          else if (source[i] == '\n')
          {
            yield return source.AsMemory(start, i - start);
            start = i + 1;
          }
        }

        if (start <= source.Length)
          yield return source.AsMemory(start);
      }

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

      /// <summary>
      /// <para>Remove diacritical marks.</para>
      /// </summary>
      public System.Text.StringBuilder RemoveUnicodeMarks(bool alsoReplaceLatinStrokes = false)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var sb = new System.Text.StringBuilder();

        foreach (var c in source.Normalize(System.Text.NormalizationForm.FormKD))
          if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) is not System.Globalization.UnicodeCategory.NonSpacingMark and not System.Globalization.UnicodeCategory.SpacingCombiningMark and not System.Globalization.UnicodeCategory.EnclosingMark)
            sb.Append(c);

        if (alsoReplaceLatinStrokes)
          sb.ReplaceUnicodeLatinStrokes();

        return sb;
      }

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
