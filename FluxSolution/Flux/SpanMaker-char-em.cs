namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Appends any object as a string to a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<char> Append(this SpanMaker<char> source, object value) => source.Append(1, value.ToString());

    public static SpanMaker<char> Append(this SpanMaker<char> source, params object[] values)
      => source.Append(1, values.SelectMany(v => v.ToString() ?? string.Empty).ToList());

    /// <summary>
    /// <para>Concatenates and appends the members of a collection, using the specified separator between each member.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="separator"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static SpanMaker<char> AppendJoin<T>(this SpanMaker<char> source, string separator, System.Collections.Generic.IEnumerable<T> collection)
      => source.Append(string.Join(separator, collection));

    /// <summary>
    /// <para>Appends a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static SpanMaker<char> AppendLine(this SpanMaker<char> source)
      => source.Append(System.Environment.NewLine);

#if XNET9_0_OR_GREATER

    /// <summary>
    /// <para>Appends <paramref name="count"/> <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<char> AppendLine(this SpanMaker<char> source, int count, params System.ReadOnlySpan<char> value)
    {
      source = source.Append(count, value);
      source = source.AppendLine();
      return source;
    }

    /// <summary>
    /// <para>Appends <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<char> AppendLine(this SpanMaker<char> source, params System.ReadOnlySpan<char> value)
    {
      source = source.Append(value);
      source = source.AppendLine();
      return source;
    }

#else

    /// <summary>
    /// <para>Appends <paramref name="count"/> <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<char> AppendLine(this SpanMaker<char> source, int count, System.ReadOnlySpan<char> value)
    {
      source = source.Append(count, value);
      source = source.AppendLine();
      return source;
    }

    /// <summary>
    /// <para>Appends <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<char> AppendLine(this SpanMaker<char> source, System.ReadOnlySpan<char> value)
    {
      source = source.Append(value);
      source = source.AppendLine();
      return source;
    }

#endif

    //    public static System.Collections.Generic.List<System.Text.Rune> EnumerateRunes(this SpanBuilder<char> source)
    //    {
    //      var list = new System.Collections.Generic.List<System.Text.Rune>();

    //      for (var index = 0; index < source.Length; index++)
    //      {
    //        var ri = source[index];

    //        if (char.IsHighSurrogate(ri))
    //        {
    //          var riP1 = source[++index];

    //          if (!char.IsLowSurrogate(riP1))
    //            throw new System.InvalidOperationException("Missing low surrogate.");

    //          list.Add(new System.Text.Rune(ri, riP1));
    //        }
    //        else if (char.IsLowSurrogate(ri))
    //          throw new System.InvalidOperationException("Unexpected low surrogate (missing high surrogate).");
    //        else
    //          list.Add(new System.Text.Rune(ri));
    //      }

    //      return list;
    //    }

    /// <summary>
    /// <para>Appends ordinal extensions (e.g. rd, th, etc.) to any sequence of digits (e.g. 3, 12, etc.) in a <see cref="SpanMaker{T}"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Ordinal_indicator"/></para>
    /// </summary>
    public static SpanMaker<char> InsertOrdinalIndicatorSuffix(this SpanMaker<char> source)
      => source.AppendRegex(@"\d+", d => int.Parse(d, System.Globalization.NumberStyles.Integer).GetOrdinalIndicatorSuffix());

    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static void JoinToCamelCase(this ref SpanMaker<char> source, System.Func<char, bool> predicate, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var sm = source;

      for (var index = 0; index < sm.Length; index++)
        if (index == 0 || predicate(sm[index]))
        {
          if (index == 0)
            sm[index] = char.ToLower(sm[index], culture);

          while (predicate(sm[index]))
            sm = sm.Remove(index, 1);

          if (index > 0 && index < sm.Length)
            sm[index] = char.ToUpper(sm[index], culture);
        }

      source = sm;
    }

    /// <summary>
    /// <para>Make any consecutive sequence of digits (any number) into <paramref name="fixedLength"/> sequences using <paramref name="padding"/> in a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fixedLength"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public static SpanMaker<char> MakeNumbersFixedLength(this SpanMaker<char> source, int fixedLength, char padding = '0')
      => source.ReplaceRegex(@"\d+", d => new SpanMaker<char>(d).PadLeft(fixedLength, padding).ToString());

    /// <summary>
    /// <para>Inserts a prefix in front of any single upper case character, except the first one in the string.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public static SpanMaker<char> PrefixCapWords(this SpanMaker<char> source, char prefix = ' ')
    {
      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index >= 0; index--)
      {
        if (index == 0 || !char.IsUpper(source[index])) continue; // If, on first or c is not upper-case, then advance.

        if (!char.IsLower(source[index - 1]) && (index < maxIndex) && !char.IsLower(source[index + 1])) continue; // If, (above ensured previous) previous is not lower-case and (ensure next) next is not lower-case, then advance.

        source = source.Insert(index, 1, prefix);
      }

      return source;
    }

    /// <summary>
    /// <para>Removes all occurrences of <paramref name="regexPattern"/> in <paramref name="source"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regexPattern"></param>
    /// <returns></returns>
    public static SpanMaker<char> RemoveRegex(this SpanMaker<char> source, string regexPattern)
    {
      var sm = source;
      var mrs = sm.AsReadOnlySpan().RegexMatches(regexPattern);
      for (var i = mrs.Count - 1; i >= 0; i--)
        if (mrs.TryGetKey(i, out var slice))
          sm = sm.Remove(slice);
      return sm;
    }

#if NET9_0_OR_GREATER

    public static SpanMaker<char> AppendRegex(this SpanMaker<char> source, string regexPattern, System.Func<System.ReadOnlySpan<char>, System.ReadOnlySpan<char>> appendSelector)
    {
      System.ArgumentNullException.ThrowIfNull(appendSelector);

      var mrs = source.AsReadOnlySpan().RegexMatches(regexPattern);

      for (var i = mrs.Count - 1; i >= 0; i--)
      {
        mrs.TryGetIndexKeyValue(i, out var ikv);

        var appendText = appendSelector(ikv.Value);

        source = source.Insert(ikv.Key.GetFollowingIndex(), 1, appendText);
      }

      return source;
    }

    public static SpanMaker<char> PrependRegex(this SpanMaker<char> source, string regexPattern, System.Func<System.ReadOnlySpan<char>, System.ReadOnlySpan<char>> prependSelector)
    {
      System.ArgumentNullException.ThrowIfNull(prependSelector);

      var mrs = source.AsReadOnlySpan().RegexMatches(regexPattern);

      for (var i = mrs.Count - 1; i >= 0; i--)
      {
        mrs.TryGetIndexKeyValue(i, out var ikv);

        var prependText = prependSelector(ikv.Value);

        source = source.Insert(ikv.Key.ToRange().Start.Value, 1, prependText);
      }

      return source;
    }

    /// <summary>
    /// <para>Replaces all occurences in <paramref name="source"/> matching <paramref name="regexPattern"/> with the result from <paramref name="replacementSelector"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regexPattern"></param>
    /// <param name="replacementSelector"></param>
    /// <returns></returns>
    public static SpanMaker<char> ReplaceRegex(this SpanMaker<char> source, string regexPattern, System.Func<System.ReadOnlySpan<char>, System.ReadOnlySpan<char>> replacementSelector)
    {
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

      var mrs = source.AsReadOnlySpan().RegexMatches(regexPattern);

      for (var i = mrs.Count - 1; i >= 0; i--)
      {
        mrs.TryGetIndexKeyValue(i, out var ikv);

        source = source.Replace(ikv.Key, replacementSelector(ikv.Value));
      }

      return source;
    }

#else

    public static SpanMaker<char> ReplaceRegex( this SpanMaker<char> source, string pattern, System.Func<string, string> replacementSelector)
    {
      var vms = source.AsReadOnlySpan().RegexMatches(pattern);

      for (var i = vms.Count - 1; i >= 0; i--)
      {
        var (range, text) = vms[i];

        var replacement = replacementSelector(source.AsReadOnlySpan()[range].ToString());

        source = source.Remove(range);
        source = source.Insert(range.Start.Value, 1, replacement);
      }

      return source;
    }

#endif

    /// <summary>
    /// <para>Replaces all occurences in <paramref name="source"/> matching <paramref name="regexPattern"/> with <paramref name="replacement"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regexPattern"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public static SpanMaker<char> ReplaceRegex(this SpanMaker<char> source, string regexPattern, System.ReadOnlySpan<char> replacement)
    {
      var sm = source;
      var mrs = sm.AsReadOnlySpan().RegexMatches(regexPattern);
      for (var i = mrs.Count - 1; i >= 0; i--)
        if (mrs.TryGetKey(i, out var slice))
          sm = sm.Replace(slice, replacement);
      return sm;
    }

    /// <summary>Inserts a space in front of any single upper case character, except the first character in the string.</summary>
    public static void SplitFromCamelCase(this ref SpanMaker<char> source, char separator = ' ', System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.InvariantCulture;

      var sm = source;

      for (var index = sm.Length - 1; index >= 0; index--)
      {
        var left = index > 0 ? sm[index - 1] : default;
        var current = sm[index];
        var right = index < sm.Length - 1 ? sm[index + 1] : default;

        if (char.IsUpper(current))
        {
          if (char.IsLower(right))
            sm[index] = char.ToLower(current, culture);

          if (index > 0 && (char.IsLower(left) || char.IsLower(right)))
            sm = sm.Insert(index, 1, separator);
        }
      }

      source = sm;
    }

    /// <summary>
    /// <para>Removes a prefix in front of a single upper case character.</para>
    /// </summary>
    public static SpanMaker<char> UnprefixCapWords(this SpanMaker<char> source, char prefix = ' ')
    {
      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index > 0; index--)
      {
        if (source[index] != prefix) continue; // If, c is not prefix, then advance.

        if ((index < maxIndex) && !char.IsUpper(source[index + 1])) continue; // If, (ensure next) next is not upper-case, then advance.

        source = source.Remove(index, 1);
      }

      return source;
    }
  }
}
