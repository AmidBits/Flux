namespace Flux
{
  #region Extension methods.
  public static partial class ExtensionMethods
  {
    public static void AppendLine( this SequenceBuilder<char> source, System.ReadOnlySpan<char> value)
    {
      source.Append(value);
      source.Append(System.Environment.NewLine);
    }

    public static void InsertLine( this SequenceBuilder<char> source, int index, System.ReadOnlySpan<char> value)
    {
      source.Insert(index, value);
      source.Insert(index + value.Length, System.Environment.NewLine);
    }

    /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.), if the predicate is satisfied.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    /// <param name="predicate">The first string is the string up until and including the numeric value, and the second string is the suffix to be affixed.</param>
    public static void InsertOrdinalIndicatorSuffix( this SequenceBuilder<char> source, System.Func<string, string, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

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

          if (predicate(source.ToString(0, index + 1), suffix))
            source.Insert(index + 1, suffix);
        }

        wasDigit = isDigit;
      }
    }
    /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    public static void InsertOrdinalIndicatorSuffix( this SequenceBuilder<char> source)
      => InsertOrdinalIndicatorSuffix( source, (s1, s2) => true);

    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static void JoinToCamelCase( this SequenceBuilder<char> source, System.Func<char, bool> predicate)
    {
      for (var index = 0; index < source.Length; index++)
        if (predicate(source[index]))
        {
          do { source.Remove(index, 1); }
          while (predicate(source[index]));

          if (index < source.Length)
            source[index] = char.ToUpper(source[index]);
        }
    }
    public static void JoinToCamelCase( this SequenceBuilder<char> source)
      => JoinToCamelCase( source, char.IsWhiteSpace);

    public static void MakeNumbersFixedLength( this SequenceBuilder<char> source, int length)
    {
      bool wasDigit = false;
      var digitCount = 0;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var isDigit = char.IsDigit(source[index]);

        if (!isDigit && wasDigit && digitCount < length)
          source.Insert(index + digitCount, '0', length - digitCount);
        else if (isDigit && !wasDigit)
          digitCount = 1;
        else
          digitCount++;

        wasDigit = isDigit;
      }

      if (wasDigit) source.Insert(0, '0', length - digitCount);
    }

    /// <summary>Create a new char array with all diacritical (latin) strokes, which are not covered by the normalization forms in NET, replaced. Can be done simplistically because the diacritical latin stroke characters (and replacements) all fit in a single char.</summary>
    public static void ReplaceDiacriticalLatinStrokes( this SequenceBuilder<char> source)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = (char)((System.Text.Rune)source[index]).ReplaceDiacriticalLatinStroke().Value;
    }

    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static void SplitFromCamelCase( this SequenceBuilder<char> source)
    {
      for (var index = source.Length - 1; index > 0; index--)
        if (char.IsUpper(source[index]) && (!char.IsUpper(source[index - 1]) || char.IsLower(source[index + 1])))
          source.Insert(index, ' ');
    }

    public static SequenceBuilder<char> ToSequenceBuilderOfChar(this  SpanBuilder<System.Text.Rune> source)
    {
      var target = new SequenceBuilder<char>();

      for (var index = 0; index < source.Length; index++)
        target.Append(source[index].ToString());

      return target;
    }

    //public static SpanBuilder<System.Text.Rune> ToRuneBuilder( this SequenceBuilder<char> source)
    //{
    //  var runes = new SpanBuilder<System.Text.Rune>();

    //  for (var index = 0; index < source.Length; index++)
    //  {
    //    var c1 = source[index];

    //    if (char.IsHighSurrogate(c1))
    //    {
    //      var c2 = source[++index];

    //      if (!char.IsLowSurrogate(c2))
    //        throw new System.InvalidOperationException(@"Missing low surrogate (required after high surrogate).");

    //      runes.Append(new System.Text.Rune(c1, c2));
    //    }
    //    else if (char.IsLowSurrogate(c1))
    //      throw new System.InvalidOperationException(@"Unexpected low surrogate (only allowed after high surrogate).");
    //    else
    //      runes.Append(new System.Text.Rune(c1));
    //  }

    //  return runes;
    //}

    public static SequenceBuilder<System.Text.Rune> ToSequenceBuilderOfRune(this  SequenceBuilder<char> source)
    {
      var target = new SequenceBuilder<System.Text.Rune>();

      foreach (var rune in source.AsReadOnlySpan().EnumerateRunes())
        target.Append(rune);

      return target;
    }

    public static string ToString( this SequenceBuilder<char> source, int startIndex, int length)
      => source.AsReadOnlySpan().Slice(startIndex, length).ToString();
    public static string ToString( this SequenceBuilder<char> source, int startIndex)
      => source.ToString(startIndex, source.Length - startIndex);
  }
  #endregion Extension methods.

  public class SequenceBuilder<T>
  {
    private const int DefaultBufferSize = 32;

    private int m_version = 0;

    private T[] m_array;

    private int m_head; // Start of content.
    private int m_tail; // End of content.

    private SequenceBuilder(int count)
    {
      m_array = System.Buffers.ArrayPool<T>.Shared.Rent(count);

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }
    public SequenceBuilder()
      : this(DefaultBufferSize)
    {
    }

    public T this[int index]
    { get => GetValue(index); set => SetValue(index, value); }

    public int Capacity
      => m_array.Length;

    public int Length
      => m_tail - m_head;

    /// <summary>Grows a uniform (left and right) buffer capacity to at least that specified.</summary>
    private void EnsureUniformSpace(int length)
    {
      if (Length + length + length < Capacity) // We got space, just need to make sure we have it on both sides.
      {
        var newHead = (m_array.Length - Length) / 2;
        var newTail = newHead + Length;

        System.Array.Copy(m_array, m_head, m_array, newHead, Length);

        if (newHead > m_head)
          System.Array.Clear(m_array, m_head, newHead - m_head);
        else if (newHead < m_head)
          System.Array.Clear(m_array, newTail, m_head - newHead);

        m_head = newHead;
        m_tail = newTail;
      }
      else if (m_head < length || m_tail + length > Capacity) // Not enough uniform space available.
      {
        var allocateLength = BitOps.FoldRight(length * 2) + 1;

        var newArray = System.Buffers.ArrayPool<T>.Shared.Rent(allocateLength + Capacity);
        System.Array.Clear(newArray);

        var newHead = (newArray.Length - Length) / 2;
        var newTail = newHead + Length;

        System.Array.Copy(m_array, m_head, newArray, newHead, Length);

        System.Buffers.ArrayPool<T>.Shared.Return(m_array);

        m_array = newArray;

        m_head = newHead;
        m_tail = newTail;
      }
    }

    /// <summary>Grows an append (right) buffer capacity to at least that specified.</summary>
    private void EnsureAppendSpace(int length)
    {
      if (m_tail + length > Capacity) // Not enough append space available.
      {
        var allocateLength = BitOps.FoldRight(length) + 1;

        var newArray = System.Buffers.ArrayPool<T>.Shared.Rent(allocateLength + Capacity);
        System.Array.Clear(newArray);

        System.Array.Copy(m_array, m_head, newArray, m_head, Length);

        System.Buffers.ArrayPool<T>.Shared.Return(m_array);

        m_array = newArray;
      }
    }

    /// <summary>Grows a prepend (left) buffer capacity to at least that specified.</summary>
    private void EnsurePrependSpace(int length)
    {
      if (m_head < length) // Not enough prepend space available.
      {
        if (Capacity - Length < length) // Not enough prepend space to shift.
        {
          var allocateLength = BitOps.FoldRight(length) + 1;

          var newArray = System.Buffers.ArrayPool<T>.Shared.Rent(allocateLength + Capacity);
          System.Array.Clear(newArray);

          var newHead = m_head + allocateLength;
          var newTail = m_tail + allocateLength;

          System.Array.Copy(m_array, m_head, newArray, newHead, Length);

          System.Buffers.ArrayPool<T>.Shared.Return(m_array);

          m_array = newArray;

          m_head = newHead;
          m_tail = newTail;
        }
        else if (Length > 0) // Enough space to shift existing content.
        {
          System.Array.Copy(m_array, m_head, m_array, m_head + length, Length); // Enough prepend space, so utilize by moving content.
          System.Array.Clear(m_array, m_head, length); // Clear prepend space.

          m_head += length;
          m_tail += length;
        }
        else // Enough space to alter head/tail.
        {
          m_head = length;
          m_tail = length;
        }
      }
    }

    public SequenceBuilder<T> Append(T value, int count = 1)
    {
      m_version++;

      EnsureAppendSpace(count);

      while (count-- > 0)
        m_array[m_tail++] = value;

      return this;
    }
    public SequenceBuilder<T> Append(System.ReadOnlySpan<T> value)
    {
      m_version++;

      EnsureAppendSpace(value.Length);

      value.CopyTo(m_array, m_tail);

      m_tail += value.Length;

      return this;
    }

    public System.ReadOnlySpan<T> AsReadOnlySpan()
      => new System.ReadOnlySpan<T>(m_array, m_head, m_tail - m_head);

    public void Clear()
    {
      m_version++;

      System.Array.Clear(m_array);

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }

    public T GetValue(int index)
    {
      if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      return m_array[m_head + index];
    }

    public SequenceBuilder<T> Insert(int startAt, T value, int count = 1)
    {
      m_version++;

      EnsureUniformSpace(count);

      startAt += m_head;

      if (m_head >= m_array.Length - m_tail) // Grow from start.
      {
        System.Array.Copy(m_array, m_head, m_array, m_head - count, startAt - m_head);
        System.Array.Fill(m_array, value, startAt - count, count);
        m_head -= count;
      }
      else // Otherwise grow from end.
      {
        System.Array.Copy(m_array, startAt, m_array, startAt + count, m_tail - startAt);
        System.Array.Fill(m_array, value, startAt, count);
        m_tail += count;
      }

      return this;
    }
    public SequenceBuilder<T> Insert(int startAt, System.ReadOnlySpan<T> value)
    {
      m_version++;

      EnsureUniformSpace(value.Length);

      startAt += m_head;

      if (m_head >= m_array.Length - m_tail) // Grow from start.
      {
        System.Array.Copy(m_array, m_head, m_array, m_head - value.Length, startAt - m_head);
        value.CopyTo(m_array, startAt - value.Length);
        m_head -= value.Length;
      }
      else // Otherwise grow from end.
      {
        System.Array.Copy(m_array, startAt, m_array, startAt + value.Length, m_tail - startAt);
        value.CopyTo(m_array, startAt);
        m_tail += value.Length;
      }

      return this;
    }

    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the specfied comparer.</summary>
    public void NormalizeAdjacent(System.Collections.Generic.IList<T> values, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      m_version++;

      var targetIndex = 0;
      var previous = default(T);

      for (var sourceIndex = 0; sourceIndex < Length; sourceIndex++)
      {
        var current = GetValue(sourceIndex);

        if (!equalityComparer.Equals(current, previous) || (values.Count > 0 && !values.Contains(current, equalityComparer)))
        {
          SetValue(targetIndex++, current);

          previous = current;
        }
      }

      Remove(targetIndex, Length - targetIndex);
    }
    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the default comparer.</summary>
    public void NormalizeAdjacent(System.Collections.Generic.IList<T> values)
      => NormalizeAdjacent(values, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public void NormalizeAll(T normalizeWith, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var normalizedIndex = 0;

      var isPrevious = true; // Set to true in order for trimming to occur on the left.

      for (var sourceIndex = 0; sourceIndex < Length; sourceIndex++)
      {
        var character = GetValue(sourceIndex);

        var isCurrent = predicate(character);

        if (!(isPrevious && isCurrent))
        {
          SetValue(normalizedIndex++, isCurrent ? normalizeWith : character);

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedIndex--;

      Remove(normalizedIndex, Length - normalizedIndex);
    }
    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
    public void NormalizeAll(T normalizeWith, System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> normalize)
      => NormalizeAll(normalizeWith, t => normalize.Contains(t, equalityComparer));
    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the default equality comparer.</summary>
    public void NormalizeAll(T normalizeWith, System.Collections.Generic.IList<T> normalize)
      => NormalizeAll(normalizeWith, normalize.Contains);

    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
    public void PadEven(int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true)
    {
      if (totalWidth > Length)
      {
        var quotient = System.Math.DivRem(totalWidth - Length, 2, out var remainder);

        PadLeft(Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        // The two lines below are the original right biased (always) which works great.
        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        PadRight(totalWidth, paddingRight);
      }
    }
    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding strings for left and right respectively.</summary>
    public void PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
    {
      if (totalWidth > Length)
      {
        var quotient = System.Math.DivRem(totalWidth - Length, 2, out var remainder);

        PadLeft(Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        // The two lines below are the original right biased (always) which works great.
        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        PadRight(totalWidth, paddingRight);
      }
    }

    /// <summary>Pads this StringBuilder on the left with the specified padding character.</summary>
    public void PadLeft(int totalWidth, T padding)
      => Insert(0, padding, totalWidth - Length);
    /// <summary>Pads this StringBuilder on the left with the specified padding string.</summary>
    public void PadLeft(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Insert(0, padding);

      Remove(0, Length - totalWidth);
    }

    /// <summary>Pads this StringBuilder on the right with the specified padding character.</summary>
    public void PadRight(int totalWidth, T padding)
      => Append(padding, totalWidth - Length);
    /// <summary>Pads this StringBuilder on the right with the specified padding string.</summary>
    public void PadRight(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Append(padding);

      Remove(totalWidth, Length - totalWidth);
    }

    public SequenceBuilder<T> Prepend(T value)
    {
      m_version++;

      EnsurePrependSpace(1);

      m_array[--m_head] = value;

      return this;
    }
    public SequenceBuilder<T> Prepend(System.ReadOnlySpan<T> value)
    {
      m_version++;

      EnsurePrependSpace(value.Length);

      m_head -= value.Length;

      value.CopyTo(m_array, m_head);

      return this;
    }

    public SequenceBuilder<T> Remove(int startAt, int count)
    {
      m_version++;

      startAt += m_head;

      if (m_head <= m_array.Length - m_tail) // Shrink from start.
      {
        System.Array.Copy(m_array, m_head, m_array, m_head + count, startAt - m_head);
        System.Array.Fill(m_array, default, m_head, count);
        m_head += count;
      }
      else // Otherwise shrink from end.
      {
        System.Array.Copy(m_array, startAt + count, m_array, startAt, m_tail - startAt - count);
        m_tail -= count;
        System.Array.Fill(m_array, default, m_tail, count);
      }

      return this;
    }

    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed.</summary>
    public void RemoveAll(System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var removedIndex = 0;

      for (var sourceIndex = 0; sourceIndex < Length; sourceIndex++)
      {
        var sourceValue = GetValue(sourceIndex);

        if (!predicate(sourceValue))
          SetValue(removedIndex++, sourceValue);
      }

      Remove(removedIndex, Length - removedIndex);
    }
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the specified comparer.</summary>
    public void RemoveAll([System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> remove)
      => RemoveAll(t => remove.Contains(t, equalityComparer));
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the default comparer.</summary>
    public void RemoveAll(System.Collections.Generic.IList<T> remove)
      => RemoveAll(remove.Contains);

    /// <summary>Replace (in-place) all characters using the specified replacement selector function.</summary>
    public void ReplaceAll(System.Func<T, T> replacementSelector)
    {
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = Length - 1; index >= 0; index--)
        SetValue(index, replacementSelector(GetValue(index)));
    }
    /// <summary>Replace (in-place) all characters satisfying the predicate with the specified character.</summary>
    public void ReplaceAll(T replacement, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = Length - 1; index >= 0; index--)
        if (predicate(GetValue(index)))
          SetValue(index, replacement);
    }
    /// <summary>Replace (in-place) all specified elements with the specified element. Uses the specified comparer.</summary>
    public void ReplaceAll(T replacement, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, params T[] replace)
      => ReplaceAll(replacement, t => System.Array.Exists(replace, e => equalityComparer.Equals(e, t)));
    /// <summary>Replace (in-place) all specified elements with the specified element. Uses the default comparer.</summary>
    public void ReplaceAll(T replacement, params T[] replace)
      => ReplaceAll(replacement, System.Collections.Generic.EqualityComparer<T>.Default, replace);

    /// <summary>Reverse all items in the range [startIndex, endIndex], in the builder.</summary>
    public void Reverse(int startIndex, int endIndex)
    {
      if (startIndex < 0 || startIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < startIndex || endIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      m_version++;

      while (startIndex < endIndex)
        Swap(startIndex++, endIndex--);
    }
    /// <summary>Reverse all items in the builder.</summary>
    public void Reverse()
      => Reverse(0, Length - 1);

    public void SetValue(int index, T value)
    {
      if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      m_version++;

      m_array[m_head + index] = value;
    }

    /// <summary>Shuffle all items in the builder. Uses the specified Random.</summary>
    public void Shuffle(System.Random random)
    {
      if (random is null) throw new System.ArgumentNullException(nameof(random));

      for (var index = Length - 1; index > 0; index--) // Shuffle each element by swapping with a random element of a lower index.
        Swap(index, random.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.
    }
    /// <summary>Shuffle all items in the builder. Uses the cryptographic Random.</summary>
    public void Shuffle()
      => Shuffle(Randomization.NumberGenerator.Crypto);

    /// <summary>Swap two elements by the specified indices.</summary>
    public void Swap(int indexA, int indexB)
    {
      if (indexA != indexB)
      {
        var a = GetValue(indexA);
        var b = GetValue(indexB);

        SetValue(indexA, b);
        SetValue(indexB, a);
      }
    }
  }
}
