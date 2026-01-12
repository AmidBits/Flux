namespace Flux
{
  public static class SpanBuilderExtensions
  {
    extension(SpanBuilder<char> source)
    {
      //#region Un/Capitalize
      ///// <summary>
      ///// <para>Capitalize any lower-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</para>
      ///// </summary>
      ///// <param name="index"></param>
      ///// <param name="length"></param>
      ///// <param name="cultureInfo"></param>
      ///// <returns></returns>

      //public SpanBuilder<char> CapitalizeWords(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().CapitalizeWords(index, length, cultureInfo);

      //  return source;
      //}

      ///// <summary>
      ///// <para>Capitalize any lower-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</para>
      ///// </summary>
      ///// <param name="range"></param>
      ///// <param name="cultureInfo"></param>
      ///// <returns></returns>
      //public SpanBuilder<char> CapitalizeWords(System.Range range, System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().CapitalizeWords(range, cultureInfo);

      //  return source;
      //}

      ///// <summary>
      ///// <para>Capitalize any lower-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</para>
      ///// </summary>
      ///// <param name="cultureInfo"></param>
      ///// <returns></returns>
      //public SpanBuilder<char> CapitalizeWords(System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().CapitalizeWords(0, source.Length, cultureInfo);

      //  return source;
      //}

      ///// <summary>
      ///// <para>Uncapitalize any upper-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</para>
      ///// </summary>
      ///// <param name="index"></param>
      ///// <param name="length"></param>
      ///// <param name="cultureInfo"></param>
      ///// <returns></returns>
      //public SpanBuilder<char> UncapitalizeWords(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().UncapitalizeWords(index, length, cultureInfo);

      //  return source;
      //}

      ///// <summary>
      ///// <para>Uncapitalize any upper-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</para>
      ///// </summary>
      ///// <param name="range"></param>
      ///// <param name="cultureInfo"></param>
      ///// <returns></returns>
      //public SpanBuilder<char> UncapitalizeWords(System.Range range, System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().UncapitalizeWords(range, cultureInfo);

      //  return source;
      //}

      ///// <summary>
      ///// <para>Uncapitalize any upper-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</para>
      ///// </summary>
      ///// <param name="cultureInfo"></param>
      ///// <returns></returns>
      //public SpanBuilder<char> UncapitalizeWords(System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().UncapitalizeWords(cultureInfo);

      //  return source;
      //}

      //#endregion

      //#region ToLower/Upper

      ///// <summary>
      ///// <para>Convert all characters, in the specified range, to upper case.</para>
      ///// </summary>
      ///// <param name="index"></param>
      ///// <param name="length"></param>
      ///// <param name="cultureInfo">If null, the current culture is used.</param>
      ///// <returns></returns>
      //public SpanBuilder<char> ToLower(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().ToLower(index, length, cultureInfo);

      //  return source;
      //}

      ///// <summary>
      ///// <para>Convert all characters, in the specified range, to upper case.</para>
      ///// </summary>
      ///// <param name="range"></param>
      ///// <param name="cultureInfo">If null, the current culture is used.</param>
      ///// <returns></returns>
      //public SpanBuilder<char> ToLower(System.Range range, System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().ToLower(range, cultureInfo);

      //  return source;
      //}

      ///// <summary>
      ///// <para>Convert all characters, in the specified range, to upper case.</para>
      ///// </summary>
      ///// <param name="cultureInfo">If null, the current culture is used.</param>
      ///// <returns></returns>
      //public SpanBuilder<char> ToLower(System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().ToLower(cultureInfo);

      //  return source;
      //}

      ///// <summary>
      ///// <para>Convert all characters, in the specified range, to upper case.</para>
      ///// </summary>
      ///// <param name="index"></param>
      ///// <param name="length"></param>
      ///// <param name="cultureInfo">If null, the current culture is used.</param>
      ///// <returns></returns>
      //public SpanBuilder<char> ToUpper(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().ToUpper(index, length, cultureInfo);

      //  return source;
      //}

      ///// <summary>
      ///// <para>Convert all characters, in the specified range, to upper case.</para>
      ///// </summary>
      ///// <param name="range"></param>
      ///// <param name="cultureInfo">If null, the current culture is used.</param>
      ///// <returns></returns>
      //public SpanBuilder<char> ToUpper(System.Range range, System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().ToUpper(range, cultureInfo);

      //  return source;
      //}

      ///// <summary>
      ///// <para>Convert all characters, in the specified range, to upper case.</para>
      ///// </summary>
      ///// <param name="cultureInfo">If null, the current culture is used.</param>
      ///// <returns></returns>
      //public SpanBuilder<char> ToUpper(System.Globalization.CultureInfo? cultureInfo = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);

      //  source.AsSpan().ToUpper(cultureInfo);

      //  return source;
      //}

      //#endregion

      #region InsertOrdinalIndicatorSuffix

      /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.), if the predicate is satisfied.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
      /// <param name="predicate">The first string is the string up until and including the numeric value, and the second string is the suffix to be affixed.</param>
      public SpanBuilder<char> InsertOrdinalIndicatorSuffix(System.Func<string, string, bool>? predicate = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var wasDigit = false;

        for (var index = source.Length - 1; index >= 0; index--)
        {
          var c = source[index];

          var isDigit = char.IsDigit(c);

          if (isDigit && !wasDigit)
          {
            var isBetweenTenAndTwenty = index > 0 && source[index - 1] == '1';

            var suffix = c switch
            {
              '1' when !isBetweenTenAndTwenty => @"st",
              '2' when !isBetweenTenAndTwenty => @"nd",
              '3' when !isBetweenTenAndTwenty => @"rd",
              _ => @"th"
            };

            if (predicate?.Invoke(source.ToString(0, index + 1), suffix) ?? true)
              source.Insert(index + 1, suffix);
          }

          wasDigit = isDigit;
        }

        return source;
      }

      #endregion

      #region MakeNumbersFixedLength

      /// <summary>
      /// <para>Make all numeric groups be of at least <paramref name="fixedLength"/> in <paramref name="source"/> from <paramref name="index"/> and <paramref name="length"/> characters.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="fixedLength"></param>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <returns></returns>
      public SpanBuilder<char> MakeNumbersFixedLength(int fixedLength, int index, int length)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        System.Index.AssertInRange(index, source.Length);

        var endIndex = index + length - 1;

        bool wasDigit = false;
        var digitCount = 0;

        for (var i = endIndex; i >= index; i--)
        {
          var isDigit = char.IsDigit(source[i]);

          if (!isDigit && wasDigit && digitCount < fixedLength)
            source.Insert(i + 1, '0', fixedLength - digitCount);
          else if (isDigit && !wasDigit)
            digitCount = 1;
          else
            digitCount++;

          wasDigit = isDigit;
        }

        if (wasDigit)
          source.Insert(0, '0', fixedLength - digitCount);

        return source;
      }

      public SpanBuilder<char> MakeNumbersFixedLength(int fixedLength, System.Range range)
      {
        var (offset, length) = range.GetOffsetAndLength(source.Length);

        return MakeNumbersFixedLength(source, fixedLength, offset, length);
      }

      #endregion

      #region NormalizeAll

      /// <summary>
      /// <para>Normalize all consecutive instances of characters satisfying the <paramref name="predicate"/> to <paramref name="replacementCharacter"/> in the <paramref name="source"/>.</para>
      /// <example><code>"".NormalizeAll(' ', char.IsWhiteSpace);</code></example>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="replacementCharacter"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      /// <remarks>Normalizing means removing leading/trailing and replacing consecutive instances of characters with a single character.</remarks>
      public SpanBuilder<char> NormalizeAll(char replacementCharacter, System.Func<char, int, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);

        var normalizedIndex = 0;

        var isPrevious = true;

        for (var index = 0; index < source.Length; index++)
        {
          var c = source[index];

          var isCurrent = predicate(c, index);

          if (!(isPrevious && isCurrent))
          {
            source[normalizedIndex++] = isCurrent ? replacementCharacter : c;

            isPrevious = isCurrent;
          }
        }

        if (isPrevious) normalizedIndex--;

        return normalizedIndex == source.Length ? source : source.Remove(normalizedIndex, source.Length - normalizedIndex);
      }

      /// <summary>
      /// <para>Normalize all consecutive instances of characters satisfying the <paramref name="predicate"/> to <paramref name="replacementCharacter"/> in the <paramref name="source"/>.</para>
      /// <example><code>"".NormalizeAll(' ', char.IsWhiteSpace);</code></example>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="replacementCharacter"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      /// <remarks>Normalizing means removing leading/trailing and replacing consecutive instances of characters with a single character.</remarks>
      public SpanBuilder<char> NormalizeAll(char replacementCharacter, System.Func<char, bool> predicate)
        => source.NormalizeAll(replacementCharacter, (e, i) => predicate(e));

      /// <summary>
      /// <para>Normalize all consecutive instances of the specified <paramref name="charactersToNormalize"/> to <paramref name="replacementCharacter"/> in the <paramref name="source"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="replacementCharacter"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="charactersToNormalize"></param>
      /// <returns></returns>
      /// <remarks>Normalizing means removing leading/trailing and replacing consecutive instances of characters with a single character.</remarks>
      public SpanBuilder<char> NormalizeAll(char replacementCharacter, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] charactersToNormalize)
        => source.NormalizeAll(replacementCharacter, t => charactersToNormalize.Contains(t, equalityComparer ?? System.Collections.Generic.EqualityComparer<char>.Default));

      #endregion

      #region Un/PrefixCap

      /// <summary>
      /// <para>Inserts a space in front of any single upper case character, except the first one in the string.</para>
      /// </summary>
      /// <param name="prefix"></param>
      /// <returns></returns>
      public SpanBuilder<char> PrefixCapWords(int index, int length, char prefix = ' ')
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var maxIndex = System.Range.AssertInRange(index, length, source.Length);

        for (var i = 0; i <= maxIndex; i++)
        {
          if (
            (i == 0 || !char.IsUpper(source[i])) // If, on first or c is not upper-case, then advance.
            || (!char.IsLower(source[i - 1]) && (i < maxIndex) && !char.IsLower(source[i + 1])) // If, (above ensured previous) previous is not lower-case and (ensure next) next is not lower-case, then advance.
          )
            continue;

          source.Insert(i, prefix);
        }

        return source;
      }

      public SpanBuilder<char> PrefixCapWords(System.Range range, char prefix = ' ')
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var (offset, length) = range.GetOffsetAndLength(source.Length);

        return PrefixCapWords(source, offset, length, prefix);
      }

      public SpanBuilder<char> PrefixCapWords(char prefix = ' ')
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return PrefixCapWords(source, 0, source.Length, prefix);
      }

      /// <summary>
      /// <para>Join CamelCase of words separated by the specified predicate.</para>
      /// </summary>
      /// <param name="prefix"></param>
      /// <returns></returns>
      public SpanBuilder<char> UnprefixCapWords(int index, int length, char prefix = ' ')
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var maxIndex = System.Range.AssertInRange(index, length, source.Length);

        for (var i = 0; i > index; i++)
        {
          if (
            (source[i] != prefix) // If, c is not prefix, then advance.
            || ((i < maxIndex) && !char.IsUpper(source[i + 1])) // If, (ensure next) next is not upper-case, then advance.
          )
            continue;

          source.Remove(i, 1);
        }

        return source;
      }

      public SpanBuilder<char> UnprefixCapWords(System.Range range, char prefix = ' ')
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var (offset, length) = range.GetOffsetAndLength(source.Length);

        return UnprefixCapWords(source, offset, length, prefix);
      }

      public SpanBuilder<char> UnprefixCapWords(char prefix = ' ')
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return UnprefixCapWords(source, 0, source.Length, prefix);
      }

      #endregion

      #region Split

      public System.Collections.Generic.List<SpanBuilder<char>> SplitSb(System.Func<char, bool> predicate, System.StringSplitOptions options = StringSplitOptions.None)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(predicate);

        var te = (options & StringSplitOptions.TrimEntries) != 0;
        var ree = (options & StringSplitOptions.RemoveEmptyEntries) != 0;

        var list = new System.Collections.Generic.List<SpanBuilder<char>>();

        var atIndex = 0;

        var maxIndex = source.Length - 1;

        var buffer = new char[source.Length];

        for (var index = 0; index <= maxIndex; index++)
        {
          if ((predicate(source[index]) ? source.AsReadOnlySpan().Slice(atIndex, index - atIndex) : (index == maxIndex) ? source.AsReadOnlySpan().Slice(atIndex) : default) is var ros)
          {
            if (!(ree && (ros.Length == 0 || (te && source.AsSpan().IsWhitespace()))))
              list.Add(te ? source.TrimCommonPrefix(char.IsWhiteSpace).TrimCommonSuffix(char.IsWhiteSpace) : source);

            atIndex = index + 1;
          }
        }

        return list;
      }

      /// <summary>
      /// <para>Splits a <see cref="SpanBuilder{T}"/> into substrings based on the specified <paramref name="predicate"/> and <paramref name="options"/>.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <param name="options"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<string> Split(System.Func<char, int, bool> predicate, System.StringSplitOptions options = StringSplitOptions.None)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(predicate);

        var te = (options & StringSplitOptions.TrimEntries) != 0;
        var ree = (options & StringSplitOptions.RemoveEmptyEntries) != 0;

        var list = new System.Collections.Generic.List<string>();

        var atIndex = 0;

        var maxIndex = source.Length - 1;

        for (var index = 0; index <= maxIndex; index++)
        {
          if ((predicate(source[index], index) ? source.ToString(atIndex, index - atIndex) : (index == maxIndex) ? source.ToString(atIndex) : default) is var s && s is not null)
          {
            if (!(ree && (string.IsNullOrEmpty(s) || (te && string.IsNullOrWhiteSpace(s)))))
              list.Add(te ? s.Trim() : s);

            atIndex = index + 1;
          }
        }

        return list;
      }

      /// <summary>
      /// <para>Splits a <see cref="SpanBuilder{T}"/> into substrings based on the specified <paramref name="predicate"/> and <paramref name="options"/>.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <param name="options"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<string> Split(System.Func<char, bool> predicate, System.StringSplitOptions options = StringSplitOptions.None)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);

        return Split(source, (e, i) => predicate(e), options);
      }

      /// <summary>
      /// <para>Splits a <see cref="System.Text.StringBuilder"/> into substrings based on the specified <paramref name="options"/> and <paramref name="separators"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="options"></param>
      /// <param name="separators"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<string> Split(System.StringSplitOptions options, params char[] separators)
        => source.Split((e, i) => separators.Contains(e), options);

      #endregion
    }
  }
}
