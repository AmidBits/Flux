namespace Flux
{
  public static class SpanBuilderExtensions
  {
    extension<T>(SpanBuilder<T> source)
    {

    }

    extension(SpanBuilder<char> source)
    {
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

        var endIndex = System.Range.AssertInRange(index, length, source.Length);

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

      #region Split

      //public System.Collections.Generic.List<System.Text.StringBuilder> SplitSb(System.Func<char, bool> predicate, System.StringSplitOptions options = StringSplitOptions.None)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);
      //  System.ArgumentNullException.ThrowIfNull(predicate);

      //  var te = (options & StringSplitOptions.TrimEntries) != 0;
      //  var ree = (options & StringSplitOptions.RemoveEmptyEntries) != 0;

      //  var list = new System.Collections.Generic.List<System.Text.StringBuilder>();

      //  var atIndex = 0;

      //  var maxIndex = source.Length - 1;

      //  var buffer = new char[source.Length];

      //  for (var index = 0; index <= maxIndex; index++)
      //  {
      //    if ((predicate(source[index]) ? source.AsReadOnlySpan().Slice(atIndex, index - atIndex) : (index == maxIndex) ? source.AsReadOnlySpan().Slice(atIndex) : default) is var ros)
      //    {
      //      if (!(ree && (ros.Length == 0 || (te && sb.IsWhitespace()))))
      //        list.Add(te ? sb.TrimCommonPrefix(0, char.IsWhiteSpace).TrimCommonSuffix(0, char.IsWhiteSpace) : sb);

      //      atIndex = index + 1;
      //    }
      //  }

      //  return list;
      //}

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
