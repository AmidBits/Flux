namespace Flux
{
  public static partial class StringBuilderExtensions
  {
    extension(System.Text.StringBuilder source)
    {
      public System.Text.StringBuilder AppendSpaced(string value, UnicodeSpacing spacing = UnicodeSpacing.Space)
      {
        var spacingString = spacing.ToSpacingString();

        if (!source.IsCommonSuffix(0, spacingString))
          source.Append(spacingString);

        return source.Append(value);
      }

      #region AreIsomorphic

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> and <paramref name="target"/> are isomorphic. Two sequences are isomorphic if the characters (equal characters must be replaced with the same replacements, in the same positions) in <paramref name="source"/> can be replaced to get <paramref name="target"/>.</para>
      /// </summary>
      /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
      public bool AreIsomorphic(System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        if (source.Length != target.Length) return false;

        var map1 = new System.Collections.Generic.Dictionary<char, char>(equalityComparer);
        var map2 = new System.Collections.Generic.Dictionary<char, char>(equalityComparer);

        for (var i = source.Length - 1; i >= 0; i--)
        {
          var c1 = source[i];
          var c2 = target[i];

          if (map1.TryGetValue(c1, out char value))
          {
            if (!equalityComparer.Equals(c2, value)) return false;
          }
          else
          {
            if (map2.ContainsKey(c2)) return false;

            map1[c1] = c2;
            map2[c2] = c1;
          }
        }

        return true;
      }

      #endregion

      #region Un/Capitalize

      /// <summary>Capitalize any lower-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</summary>
      public System.Text.StringBuilder CapitalizeWords(System.Globalization.CultureInfo? culture = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        var maxIndex = source.Length - 1;

        for (var index = maxIndex; index >= 0; index--)
        {
          var c = source[index]; // Avoid multiple indexers.

          if (!char.IsLower(c)) continue; // If, c is not lower-case, advance.

          if ((index > 0) && !char.IsWhiteSpace(source[index - 1])) continue; // If, (ensure previous) previous is not white-space, advance.

          if ((index < maxIndex) && !char.IsLower(source[index + 1])) continue; // If, (ensure next) next is not lower-case, advance.

          source[index] = char.ToUpper(c, culture);
        }

        return source;
      }

      /// <summary>Uncapitalize any upper-case character with a lower case character on the right and, a whitespace on the left or that is at the beginning.</summary>
      public System.Text.StringBuilder UncapitalizeWords(System.Globalization.CultureInfo? culture = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        var maxIndex = source.Length - 1;

        for (var index = maxIndex; index >= 0; index--)
        {
          var c = source[index]; // Avoid multiple indexers.

          if (!char.IsUpper(c)) continue; // If, c is not upper-case, advance.

          if ((index > 0) && !char.IsWhiteSpace(source[index - 1])) continue; // If, (ensure previous) previous is not white-space, advance.

          if ((index < maxIndex) && !char.IsLower(source[index + 1])) continue; // If, (ensure next) next is not lower-case, advance.

          source[index] = char.ToLower(c, culture);
        }

        return source;
      }

      #endregion

      #region Case (To..)

      /// <summary>Convert all characters, in the specified range, to lower case. Uses the specified culture.</summary>
      public System.Text.StringBuilder ToLower(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        cultureInfo ??= System.Globalization.CultureInfo.InvariantCulture;

        for (var i = endIndex; i >= index; i--)
          if (source[i] is var sourceChar && char.ToLower(sourceChar, cultureInfo) is var targetChar && sourceChar != targetChar)
            source[i] = targetChar;

        return source;
      }

      /// <summary>Converts the letter at <paramref name="index"/> to lower-case, using the specified <paramref name="cultureInfo"/> (current-culture if null).</summary>
      public System.Text.StringBuilder ToLower(int index, System.Globalization.CultureInfo? cultureInfo = null)
        => ToLower(source, index, (source?.Length ?? 0) - index, cultureInfo);

      /// <summary>Convert all characters to lower case. Uses the specified culture.</summary>
      public System.Text.StringBuilder ToLower(System.Globalization.CultureInfo? cultureInfo = null)
        => ToLower(source, 0, cultureInfo);

      /// <summary>
      /// <para>Converts characters to upper-case in <paramref name="source"/>, starting at <paramref name="index"/> for <paramref name="length"/> characters, to upper-case. Uses the specified <paramref name="cultureInfo"/>, or current-culture if null.</para>
      /// </summary>
      public System.Text.StringBuilder ToUpper(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        cultureInfo ??= System.Globalization.CultureInfo.CurrentCulture;

        for (var i = endIndex; i >= index; i--)
          if (source[i] is var sourceChar && char.ToUpper(sourceChar, cultureInfo) is var targetChar && sourceChar != targetChar)
            source[i] = targetChar;

        return source;
      }

      /// <summary>
      /// <para>Converts characters to upper-case in <paramref name="source"/>, starting at <paramref name="index"/> to the end. Uses the specified <paramref name="cultureInfo"/>, or current-culture if null.</para>
      /// </summary>
      public System.Text.StringBuilder ToUpper(int index, System.Globalization.CultureInfo? cultureInfo = null)
        => ToUpper(source, index, (source?.Length ?? 0) - index, cultureInfo);

      /// <summary>
      /// <para>Converts characters to upper-case in <paramref name="source"/>. Uses the specified <paramref name="cultureInfo"/>, or current-culture if null.</para>
      /// </summary>
      public System.Text.StringBuilder ToUpper(System.Globalization.CultureInfo? cultureInfo = null)
        => ToUpper(source, 0, cultureInfo);

      #endregion

      #region Common..Length..

      /// <summary>
      /// <para>Yields the number of characters that the source and the target have in common from the specified respective indices. Uses the specified comparer.</para>
      /// </summary>
      public int CommonLengthAt(int sourceStartIndex, System.ReadOnlySpan<char> target, int targetStartIndex, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        System.Index.AssertInRange(sourceStartIndex, source.Length);
        System.Index.AssertInRange(targetStartIndex, target.Length);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        var minLength = int.Min(source.Length - sourceStartIndex, target.Length - targetStartIndex);

        var length = 0;
        while (length < minLength && equalityComparer.Equals(source[sourceStartIndex++], target[targetStartIndex++]))
          length++;
        return length;
      }

      /// <summary>
      /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and the <paramref name="predicate"/>, optionally skipping <paramref name="offset"/> elements.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="predicate"></param>
      /// <param name="maxLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonPrefixLength(int offset, System.Func<char, bool> predicate, int maxLength = int.MaxValue)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(predicate);

        System.Index.AssertInRange(offset, source.Length);

        var length = 0;
        while (offset < source.Length && length < maxLength && predicate(source[offset++]))
          length++;
        return length;
      }

      /// <summary>
      /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and <paramref name="value"/>, optionally skipping <paramref name="offset"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="maxLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonPrefixLength(int offset, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxLength = int.MaxValue)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        return CommonPrefixLength(source, offset, c => equalityComparer.Equals(c, value), maxLength);
      }

      /// <summary>
      /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and <paramref name="value"/>, optionally skipping <paramref name="offset"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="maxLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonPrefixLength(int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxLength = int.MaxValue)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        System.Index.AssertInRange(offset, source.Length);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        var minLength = int.Min(source.Length, value.Length);

        var length = 0;
        while (length < minLength && offset < source.Length && length < maxLength && equalityComparer.Equals(source[offset++], value[length]))
          length++;
        return length;
      }

      /// <summary>
      /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and the <paramref name="predicate"/>, optionally skipping <paramref name="offset"/> elements.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="predicate"></param>
      /// <param name="maxLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonSuffixLength(int offset, System.Func<char, bool> predicate, int maxLength = int.MaxValue)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(predicate);

        if (source.Length == 0 || maxLength <= 0)
          return 0;

        System.Index.AssertInRange(offset, source.Length);

        var length = 0;
        for (var si = source.Length - 1 - offset; si >= 0 && length < maxLength && predicate(source[si]); si--)
          length++;
        return length;
      }

      /// <summary>
      /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and <paramref name="value"/>, optionally skipping <paramref name="offset"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="maxLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonSuffixLength(int offset, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxLength = int.MaxValue)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        return CommonSuffixLength(source, offset, c => equalityComparer.Equals(c, value), maxLength);
      }

      /// <summary>
      /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and <paramref name="value"/>, optionally skipping <paramref name="offset"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="maxLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonSuffixLength(int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxLength = int.MaxValue)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (source.Length == 0 || value.Length == 0 || maxLength <= 0)
          return 0;

        System.Index.AssertInRange(offset, source.Length);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        var si = source.Length - offset;
        var ti = value.Length;

        var length = 0;
        while (--si >= 0 && --ti >= 0 && length < maxLength && equalityComparer.Equals(source[si], value[ti]))
          length++;
        return length;
      }

      #endregion

      #region Copy

      public System.Text.StringBuilder Copy(int sourceIndex, int length, int targetIndex)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        System.Range.AssertInRange(sourceIndex, length, source.Length);
        System.Range.AssertInRange(targetIndex, length, source.Length);

        for (; length > 0; length--)
          source[targetIndex++] = source[sourceIndex++];

        return source;
      }

      #endregion

      #region CopyTo

      public void CopyTo(int sourceStartIndex, System.Collections.Generic.IList<char> target, int targetStartIndex, int length)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(target);

        System.Range.AssertInRange(sourceStartIndex, length, source.Length);
        System.Range.AssertInRange(targetStartIndex, length, source.Length);

        while (length-- > 0)
          target[targetStartIndex++] = source[sourceStartIndex++];
      }

      public void CopyTo(System.Collections.Generic.IList<char> target, int length)
        => CopyTo(source, 0, target, 0, length);

      public int CopyTo(System.Collections.Generic.IList<char> target)
      {
        var length = int.Min(source.Length, target.Count);
        CopyTo(source, 0, target, 0, length);
        return length;
      }

      #endregion

      #region CreateIndexMap

      /// <summary>
      /// <para>Creates a new dictionary with all keys (by <paramref name="keySelector"/>) and indices of all occurences in the <paramref name="source"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public System.Collections.Generic.IDictionary<char, System.Collections.Generic.List<int>> CreateIndexMap(System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      {
        var map = new System.Collections.Generic.Dictionary<char, System.Collections.Generic.List<int>>(equalityComparer ?? System.Collections.Generic.EqualityComparer<char>.Default);

        for (var index = 0; index < source.Length; index++)
        {
          var key = source[index];

          if (!map.TryGetValue(key, out var value))
          {
            value = new();

            map[key] = value;
          }

          value.Add(index);
        }

        return map;
      }

      #endregion

      #region EnumerateRunes

      public System.Collections.Generic.IEnumerable<System.Text.Rune> EnumerateRunes()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        for (var index = 0; index < source.Length; index++)
        {
          var c1 = source[index];

          if (char.IsHighSurrogate(c1))
          {
            var c2 = source[++index];

            if (!char.IsLowSurrogate(c2))
              throw new System.InvalidOperationException(@"Missing low surrogate (orphan high surrogate found).");

            yield return new System.Text.Rune(c1, c2);
          }
          else if (char.IsLowSurrogate(c1))
            throw new System.InvalidOperationException(@"Unexpected low surrogate.");
          else // Yield char as rune.
            yield return new System.Text.Rune(c1);
        }
      }

      public System.Collections.Generic.IEnumerable<System.Text.Rune> EnumerateRunesReverse()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        for (var index = source.Length - 1; index >= 0; index--)
        {
          var rc2 = source[index];

          if (char.IsLowSurrogate(rc2))
          {
            var rc1 = source[--index];

            if (!char.IsHighSurrogate(rc1))
              throw new System.InvalidOperationException(@"Missing high surrogate (required before low surrogate).");

            yield return new System.Text.Rune(rc1, rc2);
          }
          else if (char.IsHighSurrogate(rc2))
            throw new System.InvalidOperationException(@"Unexpected high surrogate (only allowed before low surrogate).");
          else
            yield return new System.Text.Rune(rc2);
        }
      }

      #endregion

      #region GetRandom

      /// <summary>
      /// <para>Returns a random element from the <paramref name="source"/>. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
      /// </summary>
      /// <exception cref="System.ArgumentOutOfRangeException"/>
      public char GetRandom(System.Random? rng = null)
        => source[(rng ?? System.Random.Shared).Next(source.Length)];

      /// <summary>
      /// <para>Attempts to fetch a random element from the <paramref name="source"/> into <paramref name="result"/> and returns whether successful. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
      /// </summary>
      public bool TryGetRandom(out char result, System.Random? rng = null)
      {
        try
        {
          result = GetRandom(source, rng);
          return true;
        }
        catch { }

        result = default!;
        return false;
      }

      #endregion

      #region IndexOf..

      public int IndexOf(int offset, System.Func<char, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(predicate);

        var sourceLength = source.Length;

        for (; offset < sourceLength; offset++)
          if (predicate(source[offset]))
            return offset;

        return -1;
      }

      /// <summary>Reports the first index of the specified char in the string builder, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>.</summary>
      public int IndexOf(int offset, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        var sourceLength = source.Length;

        for (; offset < sourceLength; offset++)
          if (equalityComparer.Equals(source[offset], value))
            return offset;

        return -1;
      }

      /// <summary>Returns the first index of the specified string in the string builder, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>.</summary>
      public int IndexOf(int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        var maxIndex = source.Length - 1 - value.Length;

        for (; offset <= maxIndex; offset++)
          if (source.IsCommonPrefix(offset, value, equalityComparer))
            return offset;

        return -1;
      }

      /// <summary>
      /// <para>Reports the index of any of the specified <paramref name="values"/> within the <paramref name="source"/>, or -1 if none were found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public int IndexOfAny(int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] values)
      {
        if (source.IndexOf(offset, c => values.Contains(c, equalityComparer)) is var index && index > -1)
          return index;

        return -1;
      }

      /// <summary>
      /// <para>Reports the index of any of the specified <paramref name="values"/> within the <paramref name="source"/>, or -1 if none were found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public int IndexOfAny(int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
      {
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
          if (source.IndexOf(offset, values[valueIndex], equalityComparer) is var index && index > -1)
            return index;

        return -1;
      }

      #endregion

      #region InsertOrdinalIndicatorSuffix

      /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.), if the predicate is satisfied.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
      /// <param name="predicate">The first string is the string up until and including the numeric value, and the second string is the suffix to be affixed.</param>
      public System.Text.StringBuilder InsertOrdinalIndicatorSuffix(System.Func<string, string, bool>? predicate = null)
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

      #region IsCommon..

      /// <summary>
      /// <para>Indicates whether there are <paramref name="length"/> elements satisfying the <paramref name="predicate"/> at <paramref name="source"/>[<paramref name="offset"/>].</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="length"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public bool IsCommonPrefix(int offset, int length, System.Func<char, bool> predicate)
        => CommonPrefixLength(source, offset, predicate, length) == length;

      /// <summary>
      /// <para>Indicates whether there are <paramref name="length"/> elements equal to <paramref name="value"/> at <paramref name="source"/>[<paramref name="offset"/>]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="length"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool IsCommonPrefix(int offset, int length, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
        => CommonPrefixLength(source, offset, value, equalityComparer, length) == length;

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> exists at <paramref name="source"/>[offset]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool IsCommonPrefix(int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
        => CommonPrefixLength(source, offset, value, equalityComparer, value.Length) == value.Length;

      /// <summary>
      /// <para>Returns whether <paramref name="count"/> of any <paramref name="values"/> are found at <paramref name="offset"/> in the <paramref name="source"/>. Uses the <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      public bool IsCommonPrefixAny(int offset, int count, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] values)
        => IsCommonPrefix(source, offset, count, c => values.Contains(c, equalityComparer));

      /// <summary>
      /// <para>Returns whether <paramref name="maxLength"/> (or the actual length if less) of any <paramref name="values"/> are found at the <paramref name="offset"/> in the <paramref name="source"/>.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      public bool IsCommonPrefixAny(int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, int maxLength, params string[] values)
      {
        for (var valuesIndex = 0; valuesIndex < values.Length; valuesIndex++)
          if (values[valuesIndex] is var value && IsCommonPrefix(source, offset, value.AsSpan()[..int.Min(value.Length, maxLength)], equalityComparer))
            return true;

        return false;
      }

      /// <summary>
      /// <para>Returns whether any <paramref name="values"/> are found at the <paramref name="sourceIndex"/> in the <paramref name="source"/>.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      public bool IsCommonPrefixAny(int sourceIndex, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
        => IsCommonPrefixAny(source, sourceIndex, equalityComparer, int.MaxValue, values);

      /// <summary>
      /// <para>Indicates whether there are <paramref name="count"/> elements satisfying the <paramref name="predicate"/> that ends <paramref name="source"/>[end - <paramref name="offset"/>].</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="count"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public bool IsCommonSuffix(int offset, int count, System.Func<char, bool> predicate)
        => CommonSuffixLength(source, offset, predicate, count) == count;

      /// <summary>
      /// <para>Indicates whether there are <paramref name="count"/> elements equal to <paramref name="value"/> that ends at <paramref name="source"/>[end - <paramref name="offset"/>]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="count"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool IsCommonSuffix(int offset, int count, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
        => CommonSuffixLength(source, offset, value, equalityComparer, count) == count;

      /// <summary>
      /// <para>Indicates whether the <paramref name="value"/> ends at <paramref name="source"/>[end - <paramref name="offset"/>]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool IsCommonSuffix(int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
        => CommonSuffixLength(source, offset, value, equalityComparer, value.Length) == value.Length;

      /// <summary>
      /// <para>Returns whether <paramref name="count"/> of any <paramref name="values"/> that ends at <paramref name="source"/>[end - <paramref name="offset"/>]. Uses the <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="count"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public bool IsCommonSuffixAny(int offset, int count, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] values)
        => IsCommonSuffix(source, offset, count, c => values.Contains(c, equalityComparer));

      /// <summary>
      /// <para>Returns whether <paramref name="maxLength"/> (or the actual length if less) of any <paramref name="values"/> that ends at <paramref name="source"/>[end - <paramref name="offset"/>].</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      public bool IsCommonSuffixAny(int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, int maxLength, params string[] values)
      {
        for (var valuesIndex = 0; valuesIndex < values.Length; valuesIndex++)
          if (values[valuesIndex] is var value && IsCommonSuffix(source, offset, value.AsSpan()[..int.Min(value.Length, maxLength)], equalityComparer))
            return true;

        return false;
      }

      /// <summary>
      /// <para>Returns whether any <paramref name="values"/> that ends at <paramref name="source"/>[end - <paramref name="offset"/>].</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      public bool IsCommonSuffixAny(int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
        => IsCommonSuffixAny(source, offset, equalityComparer, int.MaxValue, values);

      #endregion

      #region IsPalindrome

      /// <summary>Determines whether the string is a palindrome.</summary>
      public bool IsPalindrome()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        for (int indexL = 0, indexR = source.Length - 1; indexL < indexR; indexL++, indexR--)
          if (source[indexL] != source[indexR])
            return false;

        return true;
      }

      #endregion

      public bool IsWhitespace()
      {
        for (var index = source.Length - 1; index >= 0; index--)
          if (!char.IsWhiteSpace(source[index]))
            return false;

        return true;
      }

      #region LastIndexOf..

      public int LastIndexOf(System.Func<char, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(predicate);

        for (var index = source.Length - 1; index >= 0; index--)
          if (predicate(source[index]))
            return index;

        return -1;
      }

      /// <summary>Reports the last index of the specified char in the string builder. Or -1 if not found. Uses the specified comparer.</summary>
      public int LastIndexOf(char target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        for (var index = source.Length - 1; index >= 0; index--)
          if (equalityComparer.Equals(source[index], target))
            return index;

        return -1;
      }

      /// <summary>Reports the last index of the specified string in the string builder. Or -1 if not found. Uses the specified comparer.</summary>
      public int LastIndexOf(System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        for (var index = source.Length - 1 - target.Length; index >= 0; index--)
          if (source.IsCommonPrefix(index, target, equalityComparer))
            return index;

        return -1;
      }

      /// <summary>
      /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>. or -1 if none is found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public int LastIndexOfAny(System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] values)
      {
        if (source.LastIndexOf(c => values.Contains(c, equalityComparer)) is var index && index > -1)
          return index;

        return -1;
      }

      /// <summary>
      /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>. or -1 if none is found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public int LastIndexOfAny(System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
      {
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
          if (source.LastIndexOf(values[valueIndex], equalityComparer) is var index && index > -1)
            return index;

        return -1;
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
      public System.Text.StringBuilder MakeNumbersFixedLength(int fixedLength, int index, int length)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        bool wasDigit = false;
        var digitCount = 0;

        for (var i = endIndex; i >= index; i--)
        {
          var isDigit = char.IsDigit(source[i]);

          if (!isDigit && wasDigit && digitCount < fixedLength)
            source.Insert(i + 1, @"0", fixedLength - digitCount);
          else if (isDigit && !wasDigit)
            digitCount = 1;
          else
            digitCount++;

          wasDigit = isDigit;
        }

        if (wasDigit) source.Insert(0, @"0", fixedLength - digitCount);

        return source;
      }

      #endregion

      #region ..Most

      /// <summary>Returns a string containing at most <paramref name="length"/> of characters from the left (start-of-string), if available, otherwise the entire string is returned.</summary>
      public string LeftMost(int length)
        => source.ToString(source.Length - int.Min(source.Length, length));

      /// <summary>Returns a string containing at most <paramref name="length"/> of characters from the right (end-of-string), if available, otherwise the entire string is returned.</summary>
      public string RightMost(int length)
        => source.ToString(int.Max(0, source.Length - length));

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
      public System.Text.StringBuilder NormalizeAll(char replacementCharacter, System.Func<char, int, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(source);
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
      public System.Text.StringBuilder NormalizeAll(char replacementCharacter, System.Func<char, bool> predicate)
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
      public System.Text.StringBuilder NormalizeAll(char replacementCharacter, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] charactersToNormalize)
        => source.NormalizeAll(replacementCharacter, t => charactersToNormalize.Contains(t, equalityComparer ?? System.Collections.Generic.EqualityComparer<char>.Default));

      #endregion

      #region NormalizeConsecutive

      /// <summary>
      /// <para>Normalize all consecutive character instances satisfying the <paramref name="predicate"/> to <paramref name="maxConsecutiveLength"/>.</para>
      /// </summary>
      public System.Text.StringBuilder NormalizeConsecutive(int maxConsecutiveLength, System.Func<char, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(predicate);

        if (maxConsecutiveLength < 1) throw new System.ArgumentOutOfRangeException(nameof(maxConsecutiveLength));

        var normalizedIndex = 0;
        var isPrevious = false;
        var consecutiveLength = 1;

        for (var index = 0; index < source.Length; index++)
        {
          var c = source[index];

          var isCurrent = predicate(c);

          var nonAdjacent = !(isCurrent && isPrevious);

          if (nonAdjacent || consecutiveLength < maxConsecutiveLength)
          {
            source[normalizedIndex++] = c;

            isPrevious = isCurrent;
          }

          if (nonAdjacent) consecutiveLength = 1;
          else consecutiveLength++;
        }

        return source.Remove(normalizedIndex, source.Length - normalizedIndex);
      }

      /// <summary>
      /// <para>Normalize all consecutive character instances of the specified <paramref name="charactersToNormalize"/> to <paramref name="maxConsecutiveLength"/>.</para>
      /// </summary>
      public System.Text.StringBuilder NormalizeConsecutive(int maxConsecutiveLength, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] charactersToNormalize)
        => source.NormalizeConsecutive(maxConsecutiveLength, c => charactersToNormalize is null || charactersToNormalize.Length == 0 || charactersToNormalize.Contains(c, equalityComparer));

      #endregion

      #region Pad..

      /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
      public System.Text.StringBuilder PadEven(int totalWidth, char paddingLeft, char paddingRight)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (totalWidth > source.Length)
        {
          PadLeft(source, source.Length + (totalWidth - source.Length) / 2, paddingLeft);
          PadRight(source, totalWidth, paddingRight);
        }

        return source;
      }

      /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding strings for left and right respectively.</summary>
      public System.Text.StringBuilder PadEven(int totalWidth, string paddingLeft, string paddingRight)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (totalWidth > source.Length)
        {
          PadLeft(source, source.Length + (totalWidth - source.Length) / 2, paddingLeft);
          PadRight(source, totalWidth, paddingRight);
        }

        return source;
      }

      /// <summary>Pads this StringBuilder on the left with the specified padding character.</summary>
      public System.Text.StringBuilder PadLeft(int totalWidth, char padding)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.Insert(0, padding.ToString(), totalWidth - source.Length);
      }

      /// <summary>Pads this StringBuilder on the left with the specified padding string.</summary>
      public System.Text.StringBuilder PadLeft(int totalWidth, System.ReadOnlySpan<char> padding)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        while (source.Length < totalWidth)
          source.Insert(0, padding);

        source.Remove(0, source.Length - totalWidth);

        return source;
      }

      /// <summary>Pads this StringBuilder on the right with the specified padding character.</summary>
      public System.Text.StringBuilder PadRight(int totalWidth, char padding)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.Append(padding, totalWidth - source.Length);
      }

      /// <summary>Pads this StringBuilder on the right with the specified padding string.</summary>
      public System.Text.StringBuilder PadRight(int totalWidth, System.ReadOnlySpan<char> padding)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        while (source.Length < totalWidth)
          source.Append(padding);

        source.Remove(totalWidth, source.Length - totalWidth);

        return source;
      }

      #endregion

      #region Un/PrefixCap

      /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
      public System.Text.StringBuilder PrefixCapWords(char prefix = ' ')
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var maxIndex = source.Length - 1;

        for (var index = maxIndex; index >= 0; index--)
        {
          if (index == 0 || !char.IsUpper(source[index])) continue; // If, on first or c is not upper-case, then advance.

          if (!char.IsLower(source[index - 1]) && (index < maxIndex) && !char.IsLower(source[index + 1])) continue; // If, (above ensured previous) previous is not lower-case and (ensure next) next is not lower-case, then advance.

          source.Insert(index, prefix);
        }

        return source;
      }

      /// <summary>Join CamelCase of words separated by the specified predicate. The first character</summary>
      public System.Text.StringBuilder UnprefixCapWords(char prefix = ' ')
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var maxIndex = source.Length - 1;

        for (var index = maxIndex; index > 0; index--)
        {
          if (source[index] != prefix) continue; // If, c is not prefix, then advance.

          if ((index < maxIndex) && !char.IsUpper(source[index + 1])) continue; // If, (ensure next) next is not upper-case, then advance.

          source.Remove(index, 1);
        }

        return source;
      }

      #endregion

      #region PrefixFunction

      /// <summary>
      /// <para>The Prefix function for this string-builder is an array of length n, where p[i] is the length of the longest proper prefix of the substring <paramref name="source"/>[0...i] which is also a suffix of this substring.</para>
      /// <para>A proper prefix of a string is a prefix that is not equal to the string itself.</para>
      /// <para>I.e., z[i] is the length of the longest common prefix between <paramref name="source"/> and the suffix of <paramref name="source"/> starting at i.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/></para>
      /// <para><see href="https://cp-algorithms.com/string/prefix-function.html"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int[] PrefixFunction()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var p = new int[source.Length];

        for (int i = 1; i < source.Length; i++)
        {
          var j = p[i - 1];

          while (j > 0 && source[i] != source[j])
            j = p[j - 1];

          if (source[i] == source[j])
            j++;

          p[i] = j;
        }

        return p;
      }

      #endregion

      #region Remove

      /// <summary>
      /// <span>Removes the characters from <paramref name="startIndex"/> on to the end of the <paramref name="source"/>.</span>
      /// </summary>
      public System.Text.StringBuilder Remove(int startIndex)
        => source.Remove(startIndex, source.Length - startIndex);

      #endregion

      #region RemoveAll

      /// <summary>
      /// <para>Remove all characters satisfying the <paramref name="predicate"/> from the <paramref name="source"/>.</para>
      /// <para><example><code>"".RemoveAll(char.IsWhiteSpace);</code></example></para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public System.Text.StringBuilder RemoveAll(System.Func<char, int, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(predicate);

        var removedIndex = 0;

        for (var index = 0; index < source.Length; index++)
          if (source[index] is var c && !predicate(c, index))
            source[removedIndex++] = c;

        return source.Remove(removedIndex, source.Length - removedIndex);
      }

      /// <summary>
      /// <para>Remove all characters satisfying the <paramref name="predicate"/> from the <paramref name="source"/>.</para>
      /// <para><example><code>"".RemoveAll(char.IsWhiteSpace);</code></example></para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public System.Text.StringBuilder RemoveAll(System.Func<char, bool> predicate)
        => RemoveAll(source, (e, i) => predicate(e));

      /// <summary>
      /// <para>Remove the specified <paramref name="charactersToRemove"/> from the <paramref name="source"/>. Uses a specified <paramref name="equalityComparer"/> (or default if null).</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="charactersToRemove"></param>
      /// <returns></returns>
      public System.Text.StringBuilder RemoveAll(System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] charactersToRemove)
        => RemoveAll(source, (e, i) => charactersToRemove.Contains(e, equalityComparer ?? System.Collections.Generic.EqualityComparer<char>.Default));

      #endregion

      #region Repeat

      /// <summary>
      /// <span>Extends the content of the <paramref name="source"/> by repeating the content <paramref name="count"/> times.</span>
      /// </summary>
      public System.Text.StringBuilder Repeat(int count)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (source.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));

        var original = source.ToString();

        while (count-- > 0)
          source.Append(original);

        return source;
      }

      #endregion

      #region ReplaceAll

      public System.Text.StringBuilder ReplaceAll(int index, int length, System.Func<char, int, bool> predicate, System.Func<char, int, string?> replacementSelector)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        System.ArgumentNullException.ThrowIfNull(predicate);
        System.ArgumentNullException.ThrowIfNull(replacementSelector);

        for (var i = endIndex; i >= index; i--)
          if (source[i] is var c && predicate(c, i) && replacementSelector(c, i) is var r && r is not null)
            source.Remove(i, 1).Insert(i, r);

        return source;
      }

      /// <summary>
      /// <para>Replace all characters satisfying the <paramref name="predicate"/> using the <paramref name="replacementSelector"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <param name="replacementSelector"></param>
      /// <returns></returns>
      /// <remarks>In both the <paramref name="predicate"/>(e, bool) and the <paramref name="replacementSelector"/>(e, string): e = the character.</remarks>
      public System.Text.StringBuilder ReplaceAll(System.Func<char, int, bool> predicate, System.Func<char, int, string?> replacementSelector)
        => ReplaceAll(source, 0, source.Length, predicate, replacementSelector);

      /// <summary>
      /// <para>Replace all characters satisfying the <paramref name="predicate"/> using the <paramref name="replacementSelector"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <param name="replacementSelector"></param>
      /// <returns></returns>
      /// <remarks>In both the <paramref name="predicate"/>(e, i, bool) and the <paramref name="replacementSelector"/>(e, i, string): e = the character, i = the index of the character.</remarks>
      public System.Text.StringBuilder ReplaceAll(int index, int length, System.Func<char, bool> predicate, System.Func<char, string?> replacementSelector)
        => ReplaceAll(source, index, length, (e, i) => predicate(e), (e, i) => replacementSelector(e));

      /// <summary>
      /// <para>Replace all characters satisfying the <paramref name="predicate"/> using the <paramref name="replacementSelector"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <param name="replacementSelector"></param>
      /// <returns></returns>
      /// <remarks>In both the <paramref name="predicate"/>(e, bool) and the <paramref name="replacementSelector"/>(e, string): e = the character.</remarks>
      public System.Text.StringBuilder ReplaceAll(System.Func<char, bool> predicate, System.Func<char, string?> replacementSelector)
        => ReplaceAll(source, 0, source.Length, predicate, replacementSelector);

      public System.Text.StringBuilder ReplaceAll(int index, int length, System.Func<char, bool> predicate, System.Func<char, char> replacementSelector)
        => ReplaceAll(source, index, length, (e, i) => predicate(e), (e, i) => replacementSelector(e).ToString());

      public System.Text.StringBuilder ReplaceAll(System.Func<char, bool> predicate, System.Func<char, char> replacementSelector)
        => ReplaceAll(source, 0, source.Length, (e, i) => predicate(e), (e, i) => replacementSelector(e).ToString());

      /// <summary>
      /// <para>Replace all characters using the <paramref name="replacementSelector"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="replacementSelector"></param>
      /// <returns></returns>
      public System.Text.StringBuilder ReplaceAll(System.Func<char, char> replacementSelector)
        => ReplaceAll(source, 0, source.Length, (e, i) => true, (e, i) => replacementSelector(e).ToString());

      #endregion

      #region ReplaceIfEqualAt

      public System.Text.StringBuilder ReplaceIfEqualAt(int startAt, System.ReadOnlySpan<char> key, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (source.IsCommonPrefix(startAt, key, equalityComparer ?? System.Collections.Generic.EqualityComparer<char>.Default))
        {
          source.Remove(startAt, key.Length);
          source.Insert(startAt, value);
        }

        return source;
      }

      #endregion

      /// <summary>In-place replacement of diacritical latin strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters and their replacements are all exactly a single char.</summary>
      public System.Text.StringBuilder ReplaceUnicodeLatinStrokes()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        for (var index = source.Length - 1; index >= 0; index--)
          if (char.IsLatinStroke(source[index], out var replacement))
            source[index] = replacement;

        return source;
      }

      #region Replicate

      /// <summary>
      /// <span>Returns the string builder with the specified <paramref name="characters"/> replicated <paramref name="count"/> times throughout. If no characters are specified, all characters are replicated. If the string builder is empty, nothing is replicated. Uses the specified comparer.</span>
      /// </summary>
      public System.Text.StringBuilder Replicate(int count, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, params char[] characters)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        for (var index = 0; index < source.Length; index++)
        {
          var sourceChar = source[index];

          if (characters.Length == 0 || characters.Contains(sourceChar, equalityComparer))
          {
            source.Insert(index, sourceChar.ToString(), count);

            index += count;
          }
        }

        return source;
      }

      #endregion

      #region Reverse

      /// <summary>Reverse all ranged characters sort of in-place. Handles surrogates.</summary>
      public System.Text.StringBuilder Reverse(int startIndex, int length)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var endIndex = System.Range.AssertInRange(startIndex, length, source.Length);

        while (startIndex < endIndex)
          source.Swap(startIndex++, endIndex--);

        return source;
      }

      /// <summary>Reverse all characters sort of in-place. Handles surrogates.</summary>
      public System.Text.StringBuilder Reverse()
        => Reverse(source, 0, source.Length);

      #endregion

      #region Slice

      public System.Text.StringBuilder Slice(int index, int length, char[] buffer)
      {
        source.CopyTo(index, buffer, length);

        return new System.Text.StringBuilder().Append(buffer, 0, length);
      }

      public System.Text.StringBuilder Slice(int index, char[] buffer)
        => Slice(source, index, source.Length, buffer);

      #endregion

      #region Split

      public System.Collections.Generic.List<System.Text.StringBuilder> SplitSb(System.Func<char, bool> predicate, System.StringSplitOptions options = StringSplitOptions.None)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(predicate);

        var te = (options & StringSplitOptions.TrimEntries) != 0;
        var ree = (options & StringSplitOptions.RemoveEmptyEntries) != 0;

        var list = new System.Collections.Generic.List<System.Text.StringBuilder>();

        var atIndex = 0;

        var maxIndex = source.Length - 1;

        var buffer = new char[source.Length];

        for (var index = 0; index <= maxIndex; index++)
        {
          if ((predicate(source[index]) ? source.Slice(atIndex, index - atIndex, buffer) : (index == maxIndex) ? source.Slice(atIndex, buffer) : default) is var sb && sb is not null)
          {
            if (!(ree && (sb.Length == 0 || (te && sb.IsWhitespace()))))
              list.Add(te ? sb.TrimCommonPrefix(0, char.IsWhiteSpace).TrimCommonSuffix(0, char.IsWhiteSpace) : sb);

            atIndex = index + 1;
          }
        }

        return list;
      }

      /// <summary>
      /// <para>Splits a <see cref="System.Text.StringBuilder"/> into substrings based on the specified <paramref name="predicate"/> and <paramref name="options"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <param name="options"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<string> Split(System.Func<char, bool> predicate, System.StringSplitOptions options = StringSplitOptions.None)
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
          if ((predicate(source[index]) ? source.ToString(atIndex, index - atIndex) : (index == maxIndex) ? source.ToString(atIndex) : default) is var s && s is not null)
          {
            if (!(ree && (string.IsNullOrEmpty(s) || (te && string.IsNullOrWhiteSpace(s)))))
              list.Add(te ? s.Trim() : s);

            atIndex = index + 1;
          }
        }

        return list;
      }

      /// <summary>
      /// <para>Splits a <see cref="System.Text.StringBuilder"/> into substrings based on the specified <paramref name="options"/> and <paramref name="separators"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="options"></param>
      /// <param name="separators"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<string> Split(System.StringSplitOptions options, params char[] separators)
        => source.Split(c => separators.Contains(c), options);

      #endregion

      #region Swap

      /// <summary>
      /// <para>Swap the two values specified by <paramref name="indexA"/> and <paramref name="indexB"/>.</para>
      /// </summary>
      internal bool SwapImpl(int indexA, int indexB)
      {
        var isUnequal = indexA != indexB;

        if (isUnequal)
          (source[indexB], source[indexA]) = (source[indexA], source[indexB]);

        return isUnequal;
      }

      /// <summary>
      /// <para>Swap the two values specified by <paramref name="indexA"/> and <paramref name="indexB"/>.</para>
      /// </summary>
      public bool Swap(int indexA, int indexB)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (indexA < 0 || indexA >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(indexA));
        if (indexB < 0 || indexB >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(indexB));

        return SwapImpl(source, indexA, indexB);
      }

      /// <summary>
      /// <para>Swap the first value in the <paramref name="source"/> with the value at the specified <paramref name="index"/>.</para>
      /// </summary>
      public bool SwapFirstWith(int index)
        => Swap(source, 0, index);

      /// <summary>
      /// <para>Swap the last value in the <paramref name="source"/> with the value at the specified <paramref name="index"/>.</para>
      /// </summary>
      public bool SwapLastWith(int index)
        => Swap(source, index, (source ?? throw new System.ArgumentNullException(nameof(source))).Length - 1);

      #endregion

      #region To..

      /// <summary>Creates a new list of <see cref="System.Text.Rune"/> from <paramref name="source"/>.</summary>
      /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
      /// <param name="source"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<System.Text.Rune> ToListOfRune()
      {
        var list = new System.Collections.Generic.List<System.Text.Rune>();

        foreach (var chunk in source.GetChunks())
          foreach (var rune in chunk.Span.EnumerateRunes())
            list.Add(rune);

        return list;
      }

      /// <summary>Creates a new list of <see cref="BaseLibrary.Source.Text.TextElement"/> from <paramref name="source"/>.</summary>
      /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
      /// <param name="source"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<System.Range> ToListOfTextElement()
      {
        var ranges = new System.Collections.Generic.List<System.Range>();

        foreach (var chunk in source.GetChunks())
          foreach (var range in chunk.Span.EnumerateTextElements())
            ranges.Add(range);

        return ranges;
      }

      ///// <summary>
      ///// <para>Creates a new <see cref="SpanMaker{T}"/> from the characters in source.</para>
      ///// </summary>
      ///// <param name="source"></param>
      ///// <returns></returns>
      //public SpanMaker<char> ToSpanMaker()
      //{
      //  var sm = new SpanMaker<char>(source.Length);
      //  foreach (var chunk in source.GetChunks())
      //    sm.Append(1, chunk.Span);
      //  return sm;
      //}

      /// <summary>Converts the value of a substring of this instance, starting at <paramref name="startIndex"/>, to a <see langword="string"/>.</summary>
      public string ToString(int startIndex)
        => source.ToString(startIndex, source.Length - startIndex);

      #endregion

      #region TrimCommon..

      /// <summary>
      /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed at the start.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public System.Text.StringBuilder TrimCommonPrefix(int offset, System.Func<char, bool> predicate, int maxTrimLength = int.MaxValue)
      {
        var length = CommonPrefixLength(source, offset, predicate, maxTrimLength);

        return source.Remove(offset, length);
      }

      /// <summary>
      /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed at the start.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.Text.StringBuilder TrimCommonPrefix(int offset, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxTrimLength = int.MaxValue)
      {
        var length = CommonPrefixLength(source, offset, value, equalityComparer, maxTrimLength);

        return source.Remove(offset, length);
      }

      /// <summary>
      /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed at the start.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public System.Text.StringBuilder TrimCommonPrefix(int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      {
        var length = CommonPrefixLength(source, offset, value, equalityComparer);

        return source.Remove(offset, length);
      }

      public System.Text.StringBuilder TrimCommonPrefixAny(int offset, System.Collections.Generic.IEnumerable<char> any, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
        => TrimCommonPrefix(source, offset, c => any.Contains(c, equalityComparer));

      /// <summary>
      /// <para>Returns the <paramref name="source"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed from the end.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="predicate"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.Text.StringBuilder TrimCommonSuffix(int offset, System.Func<char, bool> predicate, int maxTrimLength = int.MaxValue)
      {
        var length = CommonSuffixLength(source, offset, predicate, maxTrimLength);

        return source.Remove(source.Length - offset - length, length);
      }

      /// <summary>
      /// <para>Returns the <paramref name="source"/> with all consecutive elements equal to <paramref name="value"/> removed from the end. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.Text.StringBuilder TrimCommonSuffix(int offset, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxTrimLength = int.MaxValue)
      {
        var length = CommonSuffixLength(source, offset, value, equalityComparer, maxTrimLength);

        return source.Remove(source.Length - offset - length, length);
      }

      /// <summary>
      /// <para>Returns the <paramref name="source"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed from the end. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="offset"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.Text.StringBuilder TrimCommonSuffix(int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxTrimLength = int.MaxValue)
      {
        var length = CommonSuffixLength(source, offset, value, equalityComparer, maxTrimLength);

        return source.Remove(source.Length - offset - length, length);
      }

      public System.Text.StringBuilder TrimCommonSuffixAny(int offset, System.Collections.Generic.IEnumerable<char> any, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
        => TrimCommonSuffix(source, offset, c => any.Contains(c, equalityComparer));

      #endregion

      #region Wrapping

      /// <summary>Indicates whether the source is wrapped in the specified characters. E.g. brackets, or parenthesis.</summary>
      public bool IsWrapped(char left, char right)
        => source is not null && source.Length >= 2 && source[0] == left && source[^1] == right;

      /// <summary>Remove the specified wrapping characters from the source, if they exist. E.g. brackets, or parenthesis.</summary>
      public System.Text.StringBuilder Unwrap(char left, char right)
        => source is not null && IsWrapped(source, left, right) ? source.Remove(0, 1).Remove(source.Length - 1, 1) : (source ?? throw new System.ArgumentNullException(nameof(source)));

      /// <summary>Add the specified wrap characters to the source. E.g. brackets, or parenthesis.</summary>
      public System.Text.StringBuilder Wrap(char left, char right)
        => (source ?? throw new System.ArgumentNullException(nameof(source))).Insert(0, left).Append(right);

      /// <summary>Indicates whether the source is wrapped in the specified left and right strings. If either the strings are null, a false is returned.</summary>
      public bool IsWrapped(string left, string right)
        => source is not null && left is not null && right is not null && source.Length >= (left.Length + right.Length) && source.IsCommonPrefix(0, left) && source.IsCommonSuffix(0, right);

      /// <summary>Remove the specified wrap strings from the source, if they exist.</summary>
      public System.Text.StringBuilder Unwrap(string left, string right)
        => source is not null && !string.IsNullOrEmpty(left) && !string.IsNullOrEmpty(right) && IsWrapped(source, left, right) ? source.Remove(0, left.Length).Remove(source.Length - right.Length, right.Length) : (source ?? throw new System.ArgumentNullException(nameof(source)));

      /// <summary>Add the specified wrap strings to the source.</summary>
      public System.Text.StringBuilder Wrap(string left, string right)
        => (source ?? throw new System.ArgumentNullException(nameof(source))).Insert(0, left).Append(right);

      #endregion

      #region Zfunction

      /// <summary>
      /// <para>The Z-function for this string-builder is an array of length n where the i-th character is equal to the greatest number of characters starting from the position i that coincide with the first characters of <paramref name="source"/>.</para>
      /// <para>I.e. z[i] is the length of the longest string that is, at the same time, a prefix of <paramref name="source"/> and a prefix of the suffix of <paramref name="source"/> starting at i.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// <para><see href="https://cp-algorithms.com/string/z-function.html"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int[] Zfunction()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var z = new int[source.Length];

        for (int i = 1, l = 0, r = 0; i < source.Length; i++)
        {
          if (i <= r)
            z[i] = int.Min(r - i + 1, z[i - l]);

          while (i + z[i] < source.Length && source[z[i]] == source[i + z[i]])
            z[i]++;

          if (i + z[i] - 1 > r)
          {
            l = i;
            r = i + z[i] - 1;
          }
        }

        return z;
      }

      #endregion
    }

    /* "ToStringByChunks" below does not work in extension context. */

    /// <summary>
    /// <para>Kept this primarily to show how string.Create<> can be used.</para>
    /// <para><see href="https://stackoverflow.com/a/54598107/3178666"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToStringByChunks(this System.Text.StringBuilder source)
      => string.Create(source.Length, source, (span, sb) =>
      {
        foreach (var chunk in sb.GetChunks())
        {
          chunk.Span.CopyTo(span);

          span = span.Slice(chunk.Length);
        }
      });
  }
}
