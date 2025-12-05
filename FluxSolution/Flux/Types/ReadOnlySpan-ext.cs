namespace Flux
{
  public static partial class ReadOnlySpanExtensions
  {
    #region GetAlternatingElement helpers

    private static int m_alternatingIndex = 0; // This is a field used for the method below.

    #endregion

    extension<T>(System.ReadOnlySpan<T> source)
    {
      #region AsSpan

      /// <summary>
      /// <para>Creates a new non-allocating <see cref="System.Span{T}"/> over a <see cref="System.ReadOnlySpan{T}"/>.</para>
      /// </summary>
      /// <returns></returns>
      public System.Span<T> AsSpan()
        => System.Runtime.InteropServices.MemoryMarshal.CreateSpan(ref System.Runtime.InteropServices.MemoryMarshal.GetReference(source), source.Length);

      #endregion

      #region Booths algorithm (minimal rotation)

      /// <summary>
      /// <para>Find the rotation of the <paramref name="source"/> possessing the lowest lexicographical order of all such rotation. Uses the specified <paramref name="comparer"/>, or default if null.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Lexicographically_minimal_string_rotation#Booth's_Algorithm"/></para>
      /// </summary>
      /// <param name="comparer">If null, then <see cref="System.Collections.Generic.Comparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int BoothsMinimalRotation(System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        var s = new T[source.Length * 2]; // Double up the string, to avoid modular arithmetic (using index remainder).
        source.CopyTo(s);
        source.CopyTo(s.AsSpan(source.Length));

        var f = new int[s.Length];

        System.Array.Fill(f, -1); // Failure function.

        var k = 0; // Minimal (least) rotation of string found so far.

        for (var j = 1; j < s.Length; j++)
        {
          var sj = s[j];

          var i = f[j - k - 1];

          while (i != -1 && comparer.Compare(sj, s[k + i + 1]) != 0)
          {
            if (comparer.Compare(sj, s[k + i + 1]) < 0)
              k = j - i - 1;

            i = f[i];
          }

          if (comparer.Compare(sj, s[k + i + 1]) != 0) // if sj != S[k + i + 1], then i == -1
          {
            if (comparer.Compare(sj, s[k]) < 0) // k + i + 1 = k
              k = j;

            f[j - k] = -1;
          }
          else
            f[j - k] = i + 1;
        }

        return k;
      }

      #endregion

      #region CircularCopyTo

      /// <summary>
      /// <para>Copies the specified <paramref name="count"/> from <paramref name="source"/> starting at <paramref name="sourceIndex"/> into <paramref name="target"/> at the specified <paramref name="targetIndex"/>. If the <paramref name="count"/> wraps the <paramref name="target"/>, it will be wrapped to the beginning in a circular fashion. The <paramref name="source"/> is treated the same way.</para>
      /// </summary>
      /// <param name="sourceIndex"></param>
      /// <param name="target"></param>
      /// <param name="targetIndex"></param>
      /// <param name="count"></param>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public void CircularCopyTo(int sourceIndex, System.Span<T> target, int targetIndex, int count)
      {
        if (sourceIndex < 0 || sourceIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex));
        if (targetIndex < 0 || targetIndex >= target.Length) throw new System.ArgumentOutOfRangeException(nameof(targetIndex));
        if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

        for (var index = 0; index < count; index++)
          target[(targetIndex + index) % target.Length] = source[(sourceIndex + index) % source.Length];
      }

      #endregion

      #region CircularIndexOf

      /// <summary>
      /// <para>Returns the index in <see cref="System.ReadOnlySpan{T}"/> where the rotation of the <paramref name="target"/> begins, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int CircularIndexOf(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        if (source.Length >= target.Length) // If source length is less than target length, target cannot be a rotation within source.
        {
          equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

          for (var si = 0; si < source.Length; si++)
          {
            for (var ti = 0; ti < target.Length; ti++)
            {
              if (!equalityComparer.Equals(source[(si + ti) % source.Length], target[ti]))
                break;
              else if (ti == target.Length - 1)
                return si;
            }
          }
        }

        return -1;
      }

      #endregion

      #region ..CommonPrefix..

      /// <summary>
      /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and the <paramref name="predicate"/> satisfied.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <param name="maxTestLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonPrefixLength(System.Func<T, int, bool> predicate, int maxTestLength = int.MaxValue)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTestLength);
        System.ArgumentNullException.ThrowIfNull(predicate);

        maxTestLength = int.Min(maxTestLength, source.Length);

        var length = 0;
        while (length < maxTestLength && predicate(source[length], length))
          length++;
        return length;
      }

      /// <summary>
      /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and the <paramref name="predicate"/> satisfied.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <param name="maxTestLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonPrefixLength(System.Func<T, bool> predicate, int maxTestLength = int.MaxValue)
        => CommonPrefixLength(source, (e, i) => predicate(e), maxTestLength);

      /// <summary>
      /// <para>Finds the length of any common prefix shared between a <see cref="System.ReadOnlySpan{T}"/> and <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="maxTestLength"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonPrefixLength(System.ReadOnlySpan<T> target, int maxTestLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTestLength);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        maxTestLength = int.Min(maxTestLength, int.Min(source.Length, target.Length));

        var length = 0;
        while (length < maxTestLength && equalityComparer.Equals(source[length], target[length]))
          length++;
        return length;
      }

      /// <summary>
      /// <para>Finds the length of any common prefix shared between a <see cref="System.ReadOnlySpan{T}"/> and <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int CommonPrefixLength(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonPrefixLength(source, target, target.Length, equalityComparer);

      /// <summary>
      /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and a <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="maxTestLength"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonPrefixLengthAny(System.Collections.Generic.IEnumerable<T> any, int maxTestLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return CommonPrefixLength(source, (e, i) => any.Contains(e, equalityComparer), maxTestLength);
      }

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="length"/> elements that satisfies the <paramref name="predicate"/>.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <param name="length"></param>
      /// <returns></returns>
      public bool IsCommonPrefix(System.Func<T, bool> predicate, int length)
        => CommonPrefixLength(source, predicate, length) == length;

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="length"/> elements from the <paramref name="target"/> span. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="length"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool IsCommonPrefix(System.ReadOnlySpan<T> target, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonPrefixLength(source, target, length, equalityComparer) == length;

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> starts with the <paramref name="target"/> span. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool IsCommonPrefix(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => IsCommonPrefix(source, target, target.Length, equalityComparer);

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="length"/> occurences of the <paramref name="value"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="length"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool IsCommonPrefixAny(System.Collections.Generic.IEnumerable<T> any, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonPrefixLengthAny(source, any, length, equalityComparer) == length;

      /// <summary>
      /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of matching prefix elements satisfying the <paramref name="predicate"/> removed.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonPrefix(System.Func<T, bool> predicate, int maxTrimLength = int.MaxValue)
        => source[CommonPrefixLength(source, predicate, maxTrimLength)..];

      /// <summary>
      /// <para>Slice the <paramref name="source"/> with the matching prefix elements from <paramref name="value"/> removed.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="maxTrimLength"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonPrefix(System.ReadOnlySpan<T> target, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => source[CommonPrefixLength(source, target, maxTrimLength, equalityComparer)..];

      /// <summary>
      /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of prefix elements matching <paramref name="value"/> removed.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="maxTrimLength"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonPrefixAny(System.Collections.Generic.IEnumerable<T> any, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => source[CommonPrefixLengthAny(source, any, maxTrimLength, equalityComparer)..];

      #endregion

      #region ..CommonSuffix..

      /// <summary>
      /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and the <paramref name="predicate"/>.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <param name="maxTestLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonSuffixLength(System.Func<T, int, bool> predicate, int maxTestLength = int.MaxValue)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTestLength);
        System.ArgumentNullException.ThrowIfNull(predicate);

        var sourceMaxIndex = source.Length - 1;

        maxTestLength = int.Min(maxTestLength, sourceMaxIndex);

        var length = 0;
        while (length < maxTestLength && predicate(source[sourceMaxIndex - length], length))
          length++;
        return length;
      }

      /// <summary>
      /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and the <paramref name="predicate"/>.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <param name="maxTestLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonSuffixLength(System.Func<T, bool> predicate, int maxTestLength = int.MaxValue)
        => CommonSuffixLength(source, (e, i) => predicate(e), maxTestLength);

      /// <summary>
      /// <para>Finds the length of any common suffix shared between a <see cref="System.ReadOnlySpan{T}"/> and the specified <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <remarks>
      /// <para>There is a built-in method for <see cref="System.MemoryExtensions.CommonPrefixLength{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, IEqualityComparer{T}?)"/> but not one for common-suffix-length.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="maxTestLength"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int CommonSuffixLength(System.ReadOnlySpan<T> target, int maxTestLength, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(maxTestLength);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var sourceMaxIndex = source.Length - 1;
        var targetMaxIndex = target.Length - 1;

        maxTestLength = int.Min(maxTestLength, int.Min(sourceMaxIndex, targetMaxIndex));

        var length = 0;
        while (length < maxTestLength && equalityComparer.Equals(source[sourceMaxIndex - length], target[targetMaxIndex - length]))
          length++;
        return length;
      }

      /// <summary>
      /// <para>Simulating a built-in version of a CommonSuffixLength extension (akin to the actual built-in CommonPrefixLength extension).</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int CommonSuffixLength(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonSuffixLength(source, target, target.Length, equalityComparer);

      /// <summary>
      /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="maxTestLength"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonSuffixLengthAny(System.Collections.Generic.IEnumerable<T> any, int maxTestLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTestLength);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return CommonSuffixLength(source, (e, i) => any.Contains(e, equalityComparer), maxTestLength);
      }

      /// <summary>
      /// <para>Indicates whether a <see cref="System.ReadOnlySpan{T}"/> ends with <paramref name="length"/> elements that satisfy the <paramref name="predicate"/>.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <param name="length"></param>
      /// <returns></returns>
      public bool IsCommonSuffix(System.Func<T, bool> predicate, int length)
        => CommonSuffixLength(source, predicate, length) == length;

      /// <summary>
      /// <para>Indicates whether a <see cref="System.ReadOnlySpan{T}"/> ends with at least <paramref name="length"/> elements from the <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="length"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool IsCommonSuffix(System.ReadOnlySpan<T> target, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonSuffixLength(source, target, length, equalityComparer) == length;

      /// <summary>
      /// <para>Indicates whether a <see cref="System.ReadOnlySpan{T}"/> ends with the <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool IsCommonSuffix(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => IsCommonSuffix(source, target, target.Length, equalityComparer);

      /// <summary>
      /// <para>Indicates whether a <see cref="System.ReadOnlySpan{T}"/> ends with at least <paramref name="length"/> occurences of the <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="length"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool IsCommonSuffixAny(System.Collections.Generic.IEnumerable<T> any, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonSuffixLengthAny(source, any, length, equalityComparer) == length;

      /// <summary>
      /// <para>Slice a <see cref="System.ReadOnlySpan{T}"/> with <paramref name="maxTrimLength"/> of matching suffix elements satisfying the <paramref name="predicate"/> removed.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonSuffix(System.Func<T, bool> predicate, int maxTrimLength = int.MaxValue)
        => source[..^CommonSuffixLength(source, predicate, maxTrimLength)];

      /// <summary>
      /// <para>Slice a <see cref="System.ReadOnlySpan{T}"/> with <paramref name="maxTrimLength"/> of matching suffix elements from <paramref name="value"/> removed.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="maxTrimLength"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonSuffix(System.ReadOnlySpan<T> value, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => source[..^CommonSuffixLength(source, value, maxTrimLength, equalityComparer)];

      /// <summary>
      /// <para>Slice a <see cref="System.ReadOnlySpan{T}"/> with <paramref name="maxTrimLength"/> of suffix elements matching <paramref name="value"/> removed.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="maxTrimLength"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonSuffixAny(System.Collections.Generic.IEnumerable<T> any, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => source[..^CommonSuffixLengthAny(source, any, maxTrimLength, equalityComparer)];

      #endregion

      #region Contains

      public bool Contains(System.Func<T, int, bool> predicate)
      {
        for (var index = source.Length - 1; index >= 0; index--)
          if (predicate(source[index], index))
            return true;

        return false;
      }

      public bool Contains(System.Func<T, bool> predicate)
        => Contains(source, (e, i) => predicate(e));

      #endregion

      #region Count..

      /// <summary>
      /// <para>Computes how many elements satisfying the <paramref name="predicate"/> is found in a <see cref="System.ReadOnlySpan{T}"/>.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public int Count(System.Func<T, int, bool> predicate)
      {
        var count = 0;

        for (var index = source.Length - 1; index >= 0; index--)
          if (predicate(source[index], index))
            count++;

        return count;
      }

      /// <summary>
      /// <para>Computes how many elements satisfying the <paramref name="predicate"/> is found in a <see cref="System.ReadOnlySpan{T}"/>.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public int Count(System.Func<T, bool> predicate)
        => Count(source, (e, i) => predicate(e));

      /// <summary>
      /// <para>Computes how many elements of <paramref name="any"/> is found in a <see cref="System.ReadOnlySpan{T}"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int CountAny(System.Collections.Generic.IEnumerable<T> any, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return Count(source, e => any.Contains(e, equalityComparer));
      }

      #endregion

      #region CreateIndexMap

      /// <summary>
      /// <para>Creates a new dictionary with all keys (by <paramref name="keySelector"/>) and indices of all occurences in a <see cref="System.ReadOnlySpan{T}"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="TKey"></typeparam>
      /// <param name="keySelector"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.List<int>> CreateIndexMap<TKey>(System.Func<T, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
        where TKey : notnull
      {
        System.ArgumentNullException.ThrowIfNull(keySelector);

        var map = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<int>>(equalityComparer ?? System.Collections.Generic.EqualityComparer<TKey>.Default);

        for (var index = 0; index < source.Length; index++)
        {
          var key = keySelector(source[index]);

          if (!map.TryGetValue(key, out var value))
          {
            value = [];

            map[key] = value;
          }

          value.Add(index);
        }

        return map;
      }

      #endregion

      #region DiceSørensenCoefficient

      /// <summary>
      /// <para>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</para>
      /// <see href="https://en.wikipedia.org/wiki/Dice-S%C3%B8rensen_coefficient"/>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double DiceSørensenCoefficient(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => 2d * (double)source.Intersect(target, equalityComparer).Count / (double)(source.Length + target.Length);

      #endregion

      #region Extremum

      /// <summary>
      /// <para>Locate the index and value of both the minimum element and the maximum element of the sequence. Uses the specified comparer (null for default).</para>
      /// <see href="https://en.wikipedia.org/wiki/Maximum_and_minimum"/>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="valueSelector"></param>
      /// <param name="comparer">If null, then <see cref="System.Collections.Generic.Comparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public (T? MinItem, int MinIndex, TValue? MinValue, T? MaxItem, int MaxIndex, TValue? MaxValue) Extremum<TValue>(System.Func<T, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(valueSelector);

        comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

        var minItem = default(T);
        var minIndex = -1;
        var minValue = default(TValue);

        var maxItem = default(T);
        var maxIndex = -1;
        var maxValue = default(TValue);

        for (var index = source.Length - 1; index >= 0; index--)
        {
          var item = source[index];

          var value = valueSelector(item);

          if (minIndex < 0 || comparer.Compare(value, minValue) < 0)
          {
            minItem = item;
            minIndex = index;
            minValue = value;
          }

          if (maxIndex < 0 || comparer.Compare(value, maxValue) > 0)
          {
            maxItem = item;
            maxIndex = index;
            maxValue = value;
          }
        }

        return (minItem, minIndex, minValue, maxItem, maxIndex, maxValue);
      }

      #endregion

      #region GetAlternatingElement

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="customAlternatingIndex"></param>
      /// <returns></returns>
      public T GetAlternatingElement(ref int customAlternatingIndex)
        => source[customAlternatingIndex++ % source.Length];

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <returns></returns>
      public T GetAlternatingElement()
        => GetAlternatingElement(source, ref m_alternatingIndex);

      public bool TryGetAlternatingElement(out T result)
      {
        try
        {
          result = GetAlternatingElement(source);
          return true;
        }
        catch { }

        result = default!;
        return false;
      }

      #endregion

      #region GetRandomElement

      /// <summary>
      /// <para>Returns a random element from the <paramref name="source"/>. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
      /// </summary>
      /// <param name="rng"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"/>
      public T GetRandomElement(System.Random? rng = null)
        => source[(rng ?? System.Random.Shared).Next(source.Length)];

      /// <summary>
      /// <para>Attempts to fetch a random element from the <paramref name="source"/> into <paramref name="result"/> and returns whether successful. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
      /// </summary>
      /// <param name="result"></param>
      /// <param name="rng"></param>
      /// <returns></returns>
      public bool TryGetRandomElement(out T result, System.Random? rng = null)
      {
        try
        {
          result = GetRandomElement(source, rng);
          return true;
        }
        catch { }

        result = default!;
        return false;
      }

      #endregion

      #region HammingDistance

      /// <summary>
      /// <para>The Hamming distance between two sequences of equal length is the number of positions at which the corresponding symbols are different.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Hamming_distance"/></para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns>The minimum number of substitutions required to change the source to target, or the minimum number of errors that could have transformed source to target.</returns>
      /// <exception cref="System.ArgumentException"></exception>
      public int HammingDistance(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        if (source.Length != target.Length) throw new System.ArgumentException("Spans must be of equal length.");

        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var count = 0;

        for (var index = source.Length - 1; index >= 0; index--)
          if (!equalityComparer.Equals(source[index], target[index]))
            count++;

        return count;
      }

      #endregion

      #region IndexOf

      /// <summary>
      /// <para>Reports the first index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or -1 if not found.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public int IndexOf(System.Func<T, int, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);

        for (var index = 0; index < source.Length; index++)
          if (predicate(source[index], index))
            return index;

        return -1;
      }

      /// <summary>
      /// <para>Reports the first index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or -1 if not found.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public int IndexOf(System.Func<T, bool> predicate)
        => IndexOf(source, (e, i) => predicate(e));

      /// <summary>
      /// <para>Reports the first index of the specified <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int IndexOf(T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return IndexOf(source, (e, i) => equalityComparer.Equals(e, value));
      }

      /// <summary>
      /// <para>Returns the first index of the specified <paramref name="target"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int IndexOf(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var maxIndex = source.Length - target.Length;

        for (var index = 0; index <= maxIndex; index++)
          if (source[index..].IsCommonPrefix(target, equalityComparer))
            return index;

        return -1;
      }

      /// <summary>
      /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>, or -1 if none were found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int IndexOfAny(System.Collections.Generic.IEnumerable<T> any, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return IndexOf(source, (e, i) => any.Contains(e, equalityComparer));
      }

      #endregion

      #region InfimumSupremum

      public (int InfimumIndex, T InfimumItem, TTarget InfimumValue) Infimum<TTarget>(TTarget referenceValue, System.Func<T, TTarget> valueSelector, bool proper, System.Collections.Generic.IComparer<TTarget>? comparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(valueSelector);

        comparer ??= System.Collections.Generic.Comparer<TTarget>.Default;

        var infimumIndex = -1;
        var infimumItem = default(T);
        var infimumValue = referenceValue;

        for (var index = source.Length - 1; index >= 0; index--)
        {
          var item = source[index];
          var value = valueSelector(item);

          var cmp = comparer.Compare(value, referenceValue);

          if ((!proper ? cmp <= 0 : cmp < 0) && (infimumIndex < 0 || comparer.Compare(value, infimumValue) > 0))
          {
            infimumIndex = index;
            infimumItem = item;
            infimumValue = value;
          }
        }

        return (infimumIndex, infimumItem!, infimumValue);
      }

      /// <summary>
      /// <para>Locate the index, item and value of both the largest element that is less-than(-or-equal) and the smallest element that is greater-than(-or-equal) to the singleton set {<paramref name="referenceValue"/>} (set S) identified by the <paramref name="valueSelector"/> (in set P). Uses the specified comparer (null for default).</para>
      /// <see href="https://en.wikipedia.org/wiki/Infimum_and_supremum"/>
      /// </summary>
      /// <remarks>By definition of infimum and supremum, the function is supposed to return both the less-than-or-equal and greater-than-or-equal, but this version makes the (-or-equal) optional via the <paramref name="proper"/> parameter. Also, infimum and supremum are positive constructs, so to accomodate negatives we return as toward-zero and away-from-zero for clarity.</remarks>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="referenceValue"></param>
      /// <param name="valueSelector"></param>
      /// <param name="proper"></param>
      /// <param name="comparer">If null, then <see cref="System.Collections.Generic.Comparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public (T InfimumItem, int InfimumIndex, TTarget InfimumValue, T SupremumItem, int SupremumIndex, TTarget SupremumValue) InfimumSupremum<TTarget>(TTarget referenceValue, System.Func<T, TTarget> valueSelector, bool proper, System.Collections.Generic.IComparer<TTarget>? comparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(valueSelector);

        comparer ??= System.Collections.Generic.Comparer<TTarget>.Default;

        T infimumItem = default!;
        var infimumIndex = -1;
        var infimumValue = referenceValue;

        T supremumItem = default!;
        var supremumIndex = -1;
        var supremumValue = referenceValue;

        for (var index = source.Length - 1; index >= 0; index--)
        {
          var item = source[index];
          var value = valueSelector(item);

          var cmp = comparer.Compare(value, referenceValue);

          if ((!proper ? cmp <= 0 : cmp < 0) && (infimumIndex < 0 || comparer.Compare(value, infimumValue) > 0))
          {
            infimumItem = item;
            infimumIndex = index;
            infimumValue = value;
          }

          if ((!proper ? cmp >= 0 : cmp > 0) && (supremumIndex < 0 || comparer.Compare(value, supremumValue) < 0))
          {
            supremumItem = item;
            supremumIndex = index;
            supremumValue = value;
          }
        }

        return (infimumItem, infimumIndex, infimumValue, supremumItem, supremumIndex, supremumValue);
      }

      public (int SupremumIndex, T SupremumItem, TTarget SupremumValue) Supremum<TTarget>(TTarget referenceValue, System.Func<T, TTarget> valueSelector, bool proper, System.Collections.Generic.IComparer<TTarget>? comparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(valueSelector);

        comparer ??= System.Collections.Generic.Comparer<TTarget>.Default;

        var supremumIndex = -1;
        var supremumItem = default(T);
        var supremumValue = referenceValue;

        for (var index = source.Length - 1; index >= 0; index--)
        {
          var item = source[index];
          var value = valueSelector(item);

          var cmp = comparer.Compare(value, referenceValue);

          if ((!proper ? cmp >= 0 : cmp > 0) && (supremumIndex < 0 || comparer.Compare(value, supremumValue) < 0))
          {
            supremumIndex = index;
            supremumItem = item;
            supremumValue = value;
          }
        }

        return (supremumIndex, supremumItem!, supremumValue);
      }

      #endregion // GetInfimumAndSupremum

      #region IsPalindrome

      /// <summary>
      /// <para>Determines whether the <paramref name="source"/> is a palindrome. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool IsPalindrome(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        for (int indexL = 0, indexR = source.Length - 1; indexL < indexR; indexL++, indexR--)
          if (!equalityComparer.Equals(source[indexL], source[indexR]))
            return false;

        return true;
      }

      #endregion

      #region IsPangram

      public bool IsPangram(System.ReadOnlySpan<T> alphabet, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        foreach (var letter in alphabet)
          if (source.IndexOf(letter, equalityComparer) < 0)
            return false;

        return true;
      }

      #endregion

      #region IsWrapped

      /// <summary>Indicates whether the source is wrapped in the specified characters. E.g. brackets, or parenthesis.</summary>
      public bool IsWrapped(T left, T right, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return source.Length >= 2 && equalityComparer.Equals(source[0], left) && equalityComparer.Equals(source[^1], right);
      }

      /// <summary>Indicates whether the source is wrapped in the specified left and right strings. If either the strings are null, a false is returned.</summary>
      public bool IsWrapped(System.ReadOnlySpan<T> left, System.ReadOnlySpan<T> right, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return source.Length >= (left.Length + right.Length) && source.IsCommonPrefix(left, equalityComparer) && source.IsCommonSuffix(right, equalityComparer);
      }

      #endregion

      #region JackardIndex

      /// <summary>
      /// <para>The Jaccard coefficient measures similarity between finite sample sets, and is defined as the size of the intersection divided by the size of the union of the sample sets. The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Jaccard_index"/></para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double JackardIndex(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => (double)source.Intersect(target, equalityComparer).Count / (double)source.Union(target, equalityComparer).Count;

      #endregion

      #region JaroWinkler

      /// <summary>
      /// <para>Computes the Jaro-Winkler distance between <paramref name="source"/> and <paramref name="target"/>, which is a normalized value in the range [0, 1] (from greater to less editing).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Jaro-Winkler_distance"/></para>
      /// <para><seealso href="https://stackoverflow.com/questions/19123506/jaro-winkler-distance-algorithm-in-c-sharp"/></para>
      /// <para><seealso href="http://alias-i.com/lingpipe/docs/api/com/aliasi/spell/JaroWinklerDistance.html"/></para>
      /// <para><seealso href="https://www.geeksforgeeks.org/jaro-and-jaro-winkler-similarity/"/></para>
      /// <remarks>The Jaro–Winkler distance is a string metric measuring an edit distance between two sequences. The lower the Jaro–Winkler distance for two sequences is, the more similar the sequences are. The score is normalized such that 0 means an exact match and 1 means there is no similarity. The Jaro–Winkler similarity is the inversion, (1 - Jaro–Winkler distance).</remarks>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <param name="boostThreshold"></param>
      /// <param name="prefixSize"></param>
      /// <returns></returns>
      public double JaroWinklerDistance(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, double boostThreshold = 0.1, int prefixSize = 4)
        => 1 - JaroWinklerSimilarity(source, target, equalityComparer, boostThreshold, prefixSize);

      /// <summary>
      /// <para>Computes the Jaro-Winkler similarity between <paramref name="source"/> and <paramref name="target"/>, which is a normalized value in the range [0, 1] (from less to greater match).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Jaro-Winkler_distance"/></para>
      /// <para><seealso href="https://stackoverflow.com/questions/19123506/jaro-winkler-distance-algorithm-in-c-sharp"/></para>
      /// <para><seealso href="http://alias-i.com/lingpipe/docs/api/com/aliasi/spell/JaroWinklerDistance.html"/></para>
      /// <para><seealso href="https://www.geeksforgeeks.org/jaro-and-jaro-winkler-similarity/"/></para>
      /// <remarks>The Jaro–Winkler distance is a string metric measuring an edit distance between two sequences. The lower the Jaro–Winkler distance for two sequences is, the more similar the sequences are. The score is normalized such that 0 means an exact match and 1 means there is no similarity. The Jaro–Winkler similarity is the inversion, (1 - Jaro–Winkler distance).</remarks>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <param name="boostThreshold"></param>
      /// <param name="prefixSize"></param>
      /// <returns></returns>
      public double JaroWinklerSimilarity(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, double boostThreshold = 0.1, int prefixSize = 4)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var sourceCount = source.Length;
        var targetCount = target.Length;

        if (sourceCount == 0) return targetCount == 0 ? 1 : 0;
        else if (targetCount == 0) return sourceCount == 0 ? 1 : 0;

        var sourceFlags = new System.Collections.BitArray(sourceCount, false);
        var targetFlags = new System.Collections.BitArray(targetCount, false);

        // Step 1: Matches. The match phase is a greedy alignment step of items in the source sequence against the items in the target sequence.

        var matches = 0;
        var searchRange = int.Max(sourceCount, targetCount) / 2 - 1; // The maximum distance at which items may be matched.

        for (var i = 0; i < sourceCount; i++)
        {
          var loLimit = int.Max(0, i - searchRange);
          var hiLimit = int.Min(i + searchRange + 1, targetCount);

          for (var j = loLimit; j < hiLimit; j++)
          {
            if (targetFlags[j] || !equalityComparer.Equals(source[i], target[j]))
              continue;

            sourceFlags[i] = true;
            targetFlags[j] = true;

            matches++;

            break;
          }
        }

        if (matches == 0)
          return 0; // Return complete mismatch.

        // Step 2, Transpositions. The subsequence of items actually matched in both sequences are counted and will be the same length. 

        var transpositions = 0; // The number of items in the source sequence that do not line up (by index in the matched subsequence) with identical items in the target sequence is the number of "half transpositions".

        for (int i = 0, j = 0; i < sourceCount; i++)
        {
          if (!sourceFlags[i])
            continue;

          while (!targetFlags[j])
            j++;

          if (!equalityComparer.Equals(source[i], target[j]))
            transpositions++;

          j++;
        }

        transpositions /= 2; // The total number of transpositons is the number of half transpositions divided by two, rounding down.

        var score = ((double)matches / sourceCount + (double)matches / targetCount + (matches - transpositions) / (double)matches) / 3d; // The measure is the average of; the percentage of the first string matched, the percentage of the second string matched, and the percentage of matches that were not transposed.

        if (score <= boostThreshold)
          return score; // Below boost threshold, return Jaro distance score unmodified.

        // Step 3: Winkler Modification. The Winkler modification to the Jaro comparison, resulting in the Jaro-Winkler comparison, boosts scores for strings that match character for character initially.
        // Let BoostThreshold be 
        // The second parameter for the Winkler modification is 
        // If the Jaro score is below the boost threshold, or if the prefixCount is zero, the Jaro score is returned unadjusted.

        int prefixCount = 0, maxLength = int.Min(int.Min(prefixSize, sourceCount), targetCount);
        while (prefixCount < maxLength && equalityComparer.Equals(source[prefixCount], target[prefixCount]))
          prefixCount++;

        if (prefixCount == 0)
          return score; // No initial match, return Jaro distance score unmodified.

        return score + 0.1 * prefixCount * (1d - score); // Return the Winkler modified distance score.
      }

      #endregion

      #region Knuth, Morris & Pratt algorithm

      /// <summary>
      /// <para>Searches a <paramref name="source"/> text for all indices of a <paramref name="target"/> substring. Returns an empty list if not found. Uses the specified <paramref name="equalityComparer"/> (or default if null).</para>
      /// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="table"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.Collections.Generic.List<int> KnuthMorrisPrattIndices(System.ReadOnlySpan<T> target, out System.Collections.Generic.Dictionary<int, int> table, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        table = KnuthMorrisPrattTable(target, equalityComparer);

        var indices = new System.Collections.Generic.List<int>();

        var sourceIndex = 0;
        var targetIndex = 0;

        var sourceLength = source.Length;
        var targetLength = target.Length;

        while (sourceIndex < sourceLength)
        {
          if (equalityComparer.Equals(target[targetIndex], source[sourceIndex]))
          {
            sourceIndex++;
            targetIndex++;

            if (targetIndex == targetLength)
            {
              indices.Add(sourceIndex - targetIndex);

              targetIndex = table[targetIndex];
            }
          }
          else
          {
            targetIndex = table[targetIndex];

            if (targetIndex < 0)
            {
              sourceIndex++;
              targetIndex++;
            }
          }
        }

        return indices;
      }

      /// <summary>
      /// <para>Creates a map of the amount of safely skippable elements in <paramref name="source"/> (word). Uses the specified <paramref name="equalityComparer"/> (or default if null).</para>
      /// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
      /// </summary>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.Collections.Generic.Dictionary<int, int> KnuthMorrisPrattTable(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var table = new System.Collections.Generic.Dictionary<int, int>()
        {
          [0] = -1
        };

        var positionIndex = 1; // Position index.
        var candidateIndex = 0; // Current candidate index.

        while (positionIndex < source.Length)
        {
          if (equalityComparer.Equals(source[positionIndex], source[candidateIndex]))
            table[positionIndex] = table[candidateIndex];
          else
          {
            table[positionIndex] = candidateIndex;

            while (candidateIndex >= 0 && !equalityComparer.Equals(source[positionIndex], source[candidateIndex]))
              candidateIndex = table[candidateIndex];
          }

          positionIndex++;
          candidateIndex++;
        }

        table[positionIndex] = candidateIndex;

        return table;
      }

      #endregion

      #region LastIndexOf

      /// <summary>
      /// <para>Reports the last index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or -1 if not found.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public int LastIndexOf(System.Func<T, int, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);

        for (var index = source.Length - 1; index >= 0; index--)
          if (predicate(source[index], index))
            return index;

        return -1;
      }

      /// <summary>
      /// <para>Reports the last index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or -1 if not found.</para>
      /// </summary>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public int LastIndexOf(System.Func<T, bool> predicate)
        => LastIndexOf(source, (e, i) => predicate(e));

      /// <summary>
      /// <para>Returns the last index of <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int LastIndexOf(T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return LastIndexOf(source, (e, i) => equalityComparer.Equals(e, value));
      }

      /// <summary>
      /// <para>Returns the last index of <paramref name="target"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int LastIndexOf(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        for (var index = source.Length - target.Length; index >= 0; index--)
          if (source[index..].IsCommonPrefix(target, equalityComparer))
            return index;

        return -1;
      }

      /// <summary>
      /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>. or -1 if none is found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int LastIndexOfAny(System.Collections.Generic.IEnumerable<T> any, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return LastIndexOf(source, (e, i) => any.Contains(e, equalityComparer));
      }

      #endregion

      #region LevenshteinDistance

#if RESEARCH

      /// <summary>
      /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
      /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      /// <remarks>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// </remarks>
      public int[,] LevenshteinDistanceMatrix(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var sn = source.Length;
        var tn = target.Length;

        var dp = new int[sn + 1, tn + 1];

        for (var i = 1; i <= sn; i++) dp[i, 0] = i;
        for (var j = 1; j <= tn; j++) dp[0, j] = j;

        for (var i = 1; i <= sn; i++)
          for (var j = 1; j <= tn; j++)
            dp[i, j] = int.Min(
              dp[i - 1, j] + 1, // Deletion.
              int.Min(
                dp[i, j - 1] + 1, // Insertion.
                dp[i - 1, j - 1] + System.Convert.ToInt32(!equalityComparer.Equals(source[i - 1], target[j - 1])) // Substitution.
              )
            );

        return dp;
      }

#endif

      /// <summary>
      /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
      /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      /// <remarks>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
      /// </remarks>
      public int LevenshteinDistance(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

        var sn = source.Length;
        var tn = target.Length;

        if (sn == 0) return tn;
        else if (tn == 0) return sn;

        var v0 = new int[tn + 1]; // Row of costs, one row back (previous row).
        var v1 = new int[tn + 1]; // Row of costs, current row.

        for (var j = tn - 1; j >= 0; j--)
          v0[j] = j; // Initialize the 'previous' row.

        for (var i = 0; i < sn; i++)
        {
          v0[0] = i + 1; // The first element is delete (i + 1) chars from source to match empty target.

          for (var j = 0; j < tn; j++)
          {
            v1[j + 1] = int.Min(
              v0[j + 1] + 1, // Deletion.
              int.Min(
                v1[j] + 1, // Insertion.
                v0[j] + System.Convert.ToInt32(!equalityComparer.Equals(source[i], target[j])) // Substitution.
              )
            );
          }

          (v0, v1) = (v1, v0);
        }

        return v0[target.Length];

        #region Optimized version with only one vector and variables for prior costs, not yet tested!
        //var v = new int[target.Length]; // Current row of costs.

        //for (var ti = 0; ti < target.Length; ti++)
        //  v[ti] = ti + 1; // Initialize v1 (the previous row of costs) to an edit distance for an empty source, i.e. the the number of characters to delete from target.

        //var current = 0;

        //for (var si = 0; si < source.Length; si++)
        //{
        //  current = si;

        //  for (int ti = 0, left = si; ti < target.Length; ti++)
        //  {
        //    var above = current;
        //    current = left; // cost on diagonal (substitution)
        //    left = v[ti];

        //    if (!equalityComparer.Equals(source[si], target[ti]))
        //    {
        //      current = int.Min(above + 1, int.Min(left + 1, current + 1));
        //    }

        //    v[ti] = current;
        //  }
        //}

        //return v[target.Length - 1];
        #endregion Optimized version with only one vector and variables for prior costs, not yet tested!

        #region Another optimized version with one vector and temp variables this time, not yet tested!
        //var v = new int[target.Length + 1]; // Current row of costs.

        //for (var ti = 0; ti < target.Length; ti++)
        //  v[ti] = ti;

        //for (var si = 0; si < source.Length; si++)
        //{
        //  v[0] = si; // initialize with zero cost

        //  for (int ti = 0, lastDiagonal = si; ti < target.Length; ti++)
        //  {
        //    var substitute = lastDiagonal + (equalityComparer.Equals(source[si], target[ti]) ? 0 : 1); // last diagonal cost + possible inequality cost

        //    lastDiagonal = v[ti + 1]; // remember the cost of the last diagonal stripe

        //    var insert = v[ti] + 1;
        //    var delete = lastDiagonal + 1; // reuse the recently cached last diagonal in order to reduce use of indexer

        //    v[ti + 1] = int.Min(substitute, int.Min(insert, delete));
        //  }
        //}

        //return v[target.Length];
        #endregion Another optimized version with one vector and temp variables this time, not yet tested!
      }

      /// <summary>
      /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
      /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      /// <remarks>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
      /// </remarks>
      public double LevenshteinDistanceSmc(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => 1d - LevenshteinDistanceSmd(source, target, equalityComparer);

      /// <summary>
      /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
      /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      /// <remarks>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
      /// </remarks>
      public double LevenshteinDistanceSmd(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => (double)LevenshteinDistance(source, target, equalityComparer) / (double)int.Max(source.Length, target.Length);

      #endregion

      #region LevenshteinDistanceCustom

#if RESEARCH

      /// <summary>
      /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
      /// <para>This implementation allows use of custom values for deletion, insertion and substitution.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Levenshtein_distance" /></para>
      /// </summary>
      /// <remarks>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="costOfDeletion"></param>
      /// <param name="costOfInsertion"></param>
      /// <param name="costOfSubstitution"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double[,] LevenshteinDistanceCustomMatrix(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var sn = source.Length;
        var tn = target.Length;

        var dp = new double[sn + 1, tn + 1];

        for (var i = sn - 1; i >= 0; i--)
          dp[i, 0] = i * costOfInsertion;
        for (var j = tn - 1; j >= 0; j--)
          dp[0, j] = j * costOfInsertion;

        for (var i = 1; i <= sn; i++)
          for (var j = 1; j <= tn; j++)
            dp[i, j] = double.Min(
              dp[i - 1, j] + costOfDeletion,
              double.Min(
                dp[i, j - 1] + costOfInsertion,
                dp[i - 1, j - 1] + (equalityComparer.Equals(source[i - 1], target[j - 1]) ? 0 : costOfSubstitution)
              )
            );

        return dp;
      }

#endif

      /// <summary>
      /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
      /// <para>This implementation allows use of custom values for deletion, insertion and substitution.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Levenshtein_distance" /></para>
      /// </summary>
      /// <remarks>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="costOfDeletion"></param>
      /// <param name="costOfInsertion"></param>
      /// <param name="costOfSubstitution"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double LevenshteinDistanceCustom(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

        var sn = source.Length;
        var tn = target.Length;

        if (sn == 0) return tn;
        else if (tn == 0) return sn;

        var v0 = new double[tn + 1]; // Row of costs, previous row.
        var v1 = new double[tn + 1]; // Row of costs, current row.

        for (var j = tn - 1; j >= 0; j--)
          v0[j] = j * costOfInsertion; // Initialize the 'previous' (swapped to 'v1' in loop) row.

        for (var i = 0; i < sn; i++)
        {
          v1[0] = i + costOfDeletion; // The first element is delete (i + 1) chars from source to match empty target.

          for (var j = 0; j < tn; j++)
          {
            v1[j + 1] = double.Min(
              v0[j + 1] + costOfDeletion, // Deletion.
              double.Min(
                v1[j] + costOfInsertion, // Insertion.
                v1[j] + (equalityComparer.Equals(source[i], target[j]) ? 0 : costOfSubstitution) // Substitution.
              )
            );
          }

          (v0, v1) = (v1, v0);
        }

        return v0[target.Length];
      }

      #endregion

      #region LongestAlternatingSubsequence

      /// <summary>
      /// <para>The longest alternating subsequence problem, one wants to find a subsequence of a given <paramref name="source"/> in which the elements are in alternating order, and in which the sequence is as long as possible. Uses the specified <paramref name="comparer"/>, default if null.</para>
      /// <see href="https://en.wikipedia.org/wiki/Longest_alternating_subsequence"/>
      /// </summary>
      /// <remarks>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
      /// </remarks>
      /// <param name="comparer">If null, then <see cref="System.Collections.Generic.Comparer{T}.Default"/> is used.</param>
      /// <returns>The matrix of the longest alternating subsequence that was found, using dynamic programming.</returns>
      public T[] LongestAlternatingSubsequence(out int[,] matrix, System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        matrix = new int[source.Length, 2];

        for (var i = 0; i < source.Length; i++) // Initialize BOTH dimensions to 1.
        {
          matrix[i, 0] = 1; // Length of the longest alternating subsequence ending at index i and last element is greater than its previous element.
          matrix[i, 1] = 1; // Length of the longest alternating subsequence ending at index i and last element is smaller than its previous element.
        }

        var maxLength = 0;

        for (var i = 1; i < source.Length; i++)
        {
          for (var j = 0; j < i; j++)
          {
            var cmp = comparer.Compare(source[j], source[i]);

            if (cmp < 0 && matrix[j, 1] + 1 is var mj1p1 && matrix[i, 0] < mj1p1)
              matrix[i, 0] = mj1p1;

            if (cmp > 0 && matrix[j, 0] + 1 is var mj0p1 && matrix[i, 1] < mj0p1)
              matrix[i, 1] = mj0p1;
          }

          maxLength = int.Max(maxLength, int.Max(matrix[i, 0], matrix[i, 1]));
        }

        if (maxLength > 0)
        {
          var subsequence = new T[maxLength];

          for (int i = 0, mark = 0; i < source.Length; i++)
          {
            var max = int.Max(matrix[i, 0], matrix[i, 1]);

            if (max > mark)
            {
              subsequence[mark] = source[i];

              mark++;
            }
          }

          return subsequence;
        }

        return [];
      }

      #endregion

      #region LongestCommonSubsequence

      /// <summary>
      /// <para>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
      /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-subsequence/"/></para>
      /// <para><seealso href="https://www.ics.uci.edu/~eppstein/161/960229.html"/></para>
      /// </summary>
      /// <remarks>
      /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
      public int LongestCommonSubsequenceLength(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        source.TrimCommonEnds(target, out source, out target, out var equalAtStart, out var equalAtEnd, equalityComparer);

        var v1 = new int[target.Length + 1];
        var v0 = new int[target.Length + 1];

        for (var i = source.Length - 1; i >= 0; i--)
        {
          (v0, v1) = (v1, v0);

          for (var j = target.Length - 1; j >= 0; j--)
            v0[j] = equalityComparer.Equals(source[i], target[j]) ? v1[j + 1] + 1 : int.Max(v1[j], v0[j + 1]);
        }

        return v0[0] + equalAtStart + equalAtEnd;
      }

#if RESEARCH

      /// <summary>
      /// <para>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
      /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-subsequence/"/></para>
      /// <para><seealso href="https://www.ics.uci.edu/~eppstein/161/960229.html"/></para>
      /// </summary>
      /// <remarks>
      /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
      /// <para>Implemented based on the Wiki article.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
      public int[,] LongestCommonSubsequenceMatrix(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var dp = new int[source.Length + 1, target.Length + 1];

        for (int si = source.Length - 1; si >= 0; si--)
          dp[si, 0] = 0;
        for (int ti = target.Length - 1; ti >= 0; ti--)
          dp[0, ti] = 0;

        for (int si = 0; si < source.Length; si++)
          for (int ti = 0; ti < target.Length; ti++)
            dp[si + 1, ti + 1] = equalityComparer.Equals(source[si], target[ti]) ? dp[si, ti] + 1 : int.Max(dp[si + 1, ti], dp[si, ti + 1]);

        return dp;
      }

#endif

      /// <summary>
      /// <para>Compute the longest common subsequence (LCS) edit distance when only insertion and deletion is allowed (not substitution), or when the cost of the substitution is double of the cost of an insertion or deletion.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
      /// </summary>
      /// <remarks>
      /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int LongestCommonSubsequenceDistance(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => (source.Length + target.Length) - 2 * LongestCommonSubsequenceLength(source, target, equalityComparer);

      /// <summary>
      /// <para>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
      /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-subsequence/"/></para>
      /// <para><seealso href="https://www.ics.uci.edu/~eppstein/161/960229.html"/></para>
      /// </summary>
      /// <remarks>
      /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
      public double LongestCommonSubsequenceSmc(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => 1 - LongestCommonSubsequenceSmd(source, target, equalityComparer);

      /// <summary>
      /// <para>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
      /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-subsequence/"/></para>
      /// <para><seealso href="https://www.ics.uci.edu/~eppstein/161/960229.html"/></para>
      /// </summary>
      /// <remarks>
      /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
      public double LongestCommonSubsequenceSmd(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => (double)LongestCommonSubsequenceDistance(source, target, equalityComparer) / (double)int.Max(source.Length, target.Length);

#if RESEARCH

      /// <summary>
      /// <para>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
      /// <para>Returns the items comprising the longest sub-sequence.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
      /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-subsequence/"/></para>
      /// <para><seealso href="https://www.ics.uci.edu/~eppstein/161/960229.html"/></para>
      /// </summary>
      /// <remarks>
      /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
      /// <para>Implemented based on the Wiki article.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="matrix"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.Collections.Generic.List<T> LongestCommonSubsequenceValues(System.ReadOnlySpan<T> target, out int[,] matrix, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        matrix = LongestCommonSubsequenceMatrix(source, target);

        var lcsv = new System.Collections.Generic.List<T>();

        var si = source.Length;
        var ti = target.Length;

        while (si > 0 && ti > 0)
        {
          if (equalityComparer.Equals(source[si - 1], target[ti - 1]))
          {
            lcsv.Insert(0, source[si - 1]);

            si--;
            ti--;
          }
          else if (matrix[si, ti - 1] > matrix[si - 1, ti]) // If not same, then go in the direction of the greater one.
            ti--;
          else
            si--;
        }

        return lcsv;
      }

#endif

      #endregion

      #region LongestCommonSubstring

      /// <summary>
      /// <para>Finding the longest consecutive sequence of elements common to two or more sequences.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /></para>
      /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-substring/"/></para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int LongestCommonSubstringLength(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var maxLength = 0;

        var v1 = new int[target.Length + 1];
        var v0 = new int[target.Length + 1];

        for (var i = source.Length - 1; i >= 0; i--)
        {
          (v0, v1) = (v1, v0);

          for (var j = target.Length - 1; j >= 0; j--)
          {
            if (equalityComparer.Equals(source[i], target[j]))
            {
              v0[j] = v1[j + 1] + 1;

              maxLength = int.Max(maxLength, v0[j]);
            }
            else
            {
              v0[j] = 0;
            }
          }
        }

        return maxLength;
      }

#if RESEARCH

      /// <summary>
      /// <para>Finding the longest consecutive sequence of elements common to two or more sequences.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_substring_problem"/></para>
      /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-substring/"/></para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="length"></param>
      /// <param name="sourceMaxIndex"></param>
      /// <param name="targetMaxIndex"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int[,] LongestCommonSubstringMatrix(System.ReadOnlySpan<T> target, out int length, out int sourceMaxIndex, out int targetMaxIndex, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var dp = new int[source.Length + 1, target.Length + 1];

        length = 0;

        sourceMaxIndex = 0;
        targetMaxIndex = 0;

        for (var si = 0; si <= source.Length; si++)
        {
          for (var ti = 0; ti <= target.Length; ti++)
          {
            if (si > 0 && ti > 0 && equalityComparer.Equals(source[si - 1], target[ti - 1]))
            {
              var temporaryLength = dp[si, ti] = dp[si - 1, ti - 1] + 1;

              if (temporaryLength > length)
              {
                length = temporaryLength;

                sourceMaxIndex = si;
                targetMaxIndex = ti;
              }
            }
            else
              dp[si, ti] = 0;
          }
        }

        return dp;
      }

      /// <summary>
      /// <para>Finding the longest consecutive sequence of elements common to two or more sequences.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_substring_problem"/></para>
      /// <para><seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="matrix"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.Collections.Generic.List<T> LongestCommonSubstringValues(System.ReadOnlySpan<T> target, out int[,] matrix, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        matrix = LongestCommonSubstringMatrix(source, target, out var length, out var sourceIndex, out var targetIndex, equalityComparer);

        if (length > 0)
        {
          var lcsv = new System.Collections.Generic.List<T>();
          //var lcsv = new T[length];

          while (matrix[sourceIndex, targetIndex] != 0)
          {
            lcsv.Insert(0, source[sourceIndex - 1]);
            //System.Array.InsertToCopy(lcsv, 0, 1, [source[sourceIndex - 1]]); // Can also use target[targetIndex - 1].

            sourceIndex--;
            targetIndex--;
          }

          return lcsv;
        }

        return [];
      }

#endif

      #endregion

      #region LongestIncreasingSubsequence

      /// <summary>
      /// <para>The longest increasing subsequence is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</para>
      /// <see href="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
      /// </summary>
      /// <param name="dp"></param>
      /// <param name="comparer">If null, then <see cref="System.Collections.Generic.Comparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public T[] LongestIncreasingSubsequence(out int[,] dp, System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        var length = 0; // Length is returned in the matrix[0, 0].

        dp = new int[2, source.Length + 1]; // [0,...] = smallest value indices (M), [1,...] = predecessor indices (P)

        for (var i = 0; i < source.Length; i++)
        {
          // Binary search for the largest positive j ≤ L such that X[M[j]] < X[i]

          var lo = 1;
          var hi = length + 1;

          while (lo < hi)
          {
            var mid = lo + ((hi - lo) / 2);

            if (comparer.Compare(source[dp[0, mid]], source[i]) < 0)
              lo = mid + 1;
            else
              hi = mid;
          }

          var newL = lo; // After searching, lo is 1 greater than the length of the longest prefix of X[i].

          dp[1, i] = dp[0, newL - 1]; // The predecessor of X[i] is the last index of the subsequence of length newL-1.
          dp[0, newL] = i;

          if (newL > length)
            length = newL; // If we found a subsequence longer than any we've found yet, update length.
        }

        if (length > 0)
        {
          var subsequence = new T[length];

          for (int i = length - 1, k = dp[0, length]; i >= 0; i--, k = dp[1, k])
            subsequence[i] = source[k];

          return subsequence;
        }

        return [];
      }

      #endregion

      #region LongestRepeatingSubstring

      /// <summary>
      /// <para>The longest repeated substring problem is the problem of finding the longest substring of a string that occurs at least twice.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Longest_repeated_substring_problem"/></para>
      /// </summary>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public System.Range LongestRepeatingSubstring(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var dp = new int[source.Length + 1, source.Length + 1];

        var length = 0;

        var result = new System.Range(0, 0);

        for (var i = source.Length - 1; i >= 0; i--)
          for (var j = source.Length - 1; j > i; j--)
            if (equalityComparer.Equals(source[i], source[j]))
            {
              dp[i, j] = 1 + int.Min(dp[i + 1, j + 1], j - i - 1);

              if (dp[i, j] > length)
              {
                length = dp[i, j];

                result = System.Range.FromOffsetAndLength(i, length);
              }
            }

        return result;
      }

      #endregion

      #region ..Most

      /// <summary>
      /// <para>Slice a span, keeping the left-most <paramref name="count"/> of elements if available, otherwise as many as there are.</para>
      /// </summary>
      /// <param name="count"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> LeftMost(int count)
        => source[..int.Min(source.Length, count)];

      /// <summary>
      /// <para>Slice a span, keeping the right-most <paramref name="count"/> of elements if available, otherwise as many as there are.</para>
      /// </summary>
      /// <param name="count"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> RightMost(int count)
        => source[int.Max(0, source.Length - count)..];

      #endregion

      #region OptimalStringAlignment

#if RESEARCH

      /// <summary>
      /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
      /// </summary>
      /// <remarks>Implemented based on the Wiki article.</remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int[,] OptimalStringAlignmentMatrix(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var sn = source.Length;
        var tn = target.Length;

        var dp = new int[sn + 1, tn + 1];

        for (var i = sn - 1; i >= 0; i--)
          dp[i, 0] = i;
        for (var j = tn - 1; j >= 0; j--)
          dp[0, j] = j;

        for (var i = 1; i <= sn; i++)
        {
          var sourceItem = source[i - 1];

          for (var j = 1; j <= tn; j++)
          {
            var targetItem = target[j - 1];

            dp[i, j] = int.Min(
              int.Min(
                dp[i - 1, j] + 1, // Deletion.
                dp[i, j - 1] + 1 // Insertion.
              ),
              int.Min(
                dp[i - 1, j - 1] + System.Convert.ToInt32(!equalityComparer.Equals(sourceItem, targetItem)), // Substitution.
                i > 1 && j > 1 && equalityComparer.Equals(sourceItem, target[j - 2]) && equalityComparer.Equals(source[i - 2], targetItem) ? dp[i - 2, j - 2] + 1 : int.MaxValue // Transposition.
              )
            );
          }
        }

        return dp;
      }

#endif

      /// <summary>
      /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
      /// </summary>
      /// <remarks>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs three alternating horizontal rows throughout the process.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int OptimalStringAlignment(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

        var sn = source.Length;
        var tn = target.Length;

        if (sn == 0) return tn;
        else if (tn == 0) return sn;

        var v2 = new int[tn + 1]; // Row of costs, two rows back.
        var v1 = new int[tn + 1]; // Row of costs, one row back (previous).
        var v0 = new int[tn + 1]; // Row of costs, current row.

        for (int ti = 0; ti <= tn; ti++)
          v0[ti] = ti; // Initialize v1 (the previous row of costs) to an edit distance for empty source items, i.e. the the number of characters to delete from target.

        for (int si = 1; si <= sn; si++)
        {
          (v0, v1, v2) = (v2, v0, v1); // Rotate and reuse buffered rows of the cost matrix.

          v0[0] = si; // Edit distance is delete (i) chars from source to match empty target.

          var sourceItem = source[si - 1];

          for (int ti = 1; ti <= tn; ti++)
          {
            var targetItem = target[ti - 1];

            v0[ti] = int.Min(
              int.Min(
                v1[ti] + 1, // Deletion.
                v0[ti - 1] + 1 // Insertion.
              ),
              int.Min(
                v1[ti - 1] + System.Convert.ToInt32(!equalityComparer.Equals(sourceItem, targetItem)), // Substitution.
                si > 1 && ti > 1 && equalityComparer.Equals(sourceItem, target[ti - 2]) && equalityComparer.Equals(source[si - 2], targetItem) ? v2[ti - 2] + 1 : int.MaxValue // Transposition.
              )
            );
          }
        }

        return v0[tn];
      }

      /// <summary>
      /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
      /// </summary>
      /// <remarks>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs three alternating horizontal rows throughout the process.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double OptimalStringAlignmentSmc(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => 1d - OptimalStringAlignmentSmd(source, target, equalityComparer);

      /// <summary>
      /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
      /// </summary>
      /// <remarks>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs three alternating horizontal rows throughout the process.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double OptimalStringAlignmentSmd(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => (double)OptimalStringAlignment(source, target, equalityComparer) / (double)int.Max(source.Length, target.Length);

      #endregion

      #region OptimalStringAlignmentCustom

#if RESEARCH

      /// <summary>
      /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
      /// </summary>
      /// <remarks>Implemented based on the Wiki article.</remarks>
      /// <param name="target"></param>
      /// <param name="costOfDeletion"></param>
      /// <param name="costOfInsertion"></param>
      /// <param name="costOfSubstitution"></param>
      /// <param name="costOfTransposition"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double[,] OptimalStringAlignmentCustomMatrix(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, double costOfTransposition = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var sn = source.Length;
        var tn = target.Length;

        var dp = new double[sn + 1, tn + 1];

        for (var i = sn - 1; i >= 0; i--)
          dp[i, 0] = i * costOfInsertion;
        for (var j = tn - 1; j >= 0; j--)
          dp[0, j] = j * costOfInsertion;

        for (var i = 1; i <= sn; i++)
        {
          var sourceItem = source[i - 1];

          for (var j = 1; j <= tn; j++)
          {
            var targetItem = target[j - 1];

            dp[i, j] = double.Min(
              double.Min(
                dp[i - 1, j] + costOfDeletion,
                dp[i, j - 1] + costOfInsertion
              ),
              double.Min(
                dp[i - 1, j - 1] + (equalityComparer.Equals(sourceItem, targetItem) ? 0 : costOfSubstitution),
                i > 1 && j > 1 && equalityComparer.Equals(sourceItem, target[j - 2]) && equalityComparer.Equals(source[i - 2], targetItem) ? dp[i - 2, j - 2] + costOfTransposition : double.MaxValue
              )
            );
          }
        }

        return dp;
      }

#endif

      /// <summary>
      /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
      /// </summary>
      /// <remarks>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs three alternating horizontal rows throughout the process.</para>
      /// </remarks>
      /// <param name="target"></param>
      /// <param name="costOfDeletion"></param>
      /// <param name="costOfInsertion"></param>
      /// <param name="costOfSubstitution"></param>
      /// <param name="costOfTransposition"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double OptimalStringAlignmentCustom(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, double costOfTransposition = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

        var sn = source.Length;
        var tn = target.Length;

        if (sn == 0) return tn;
        else if (tn == 0) return sn;

        var v2 = new double[tn + 1]; // Row of costs, two rows back.
        var v1 = new double[tn + 1]; // Row of costs, one row back (previous).
        var v0 = new double[tn + 1]; // Row of costs, current row.

        for (var j = tn - 1; j >= 0; j--)
          v0[j] = j * costOfInsertion; // Initialize v1 (the previous row of costs) to an edit distance for empty source items, i.e. the the number of characters to delete from target.

        for (var i = 1; i <= sn; i++)
        {
          (v0, v1, v2) = (v2, v0, v1); // Rotate and reuse buffered rows of the cost matrix.

          v0[0] = i; // Edit distance is delete (i) chars from source to match empty target.

          var sourceItem = source[i - 1];

          for (var j = 1; j <= tn; j++)
          {
            var targetItem = target[j - 1];

            v0[j] = double.Min(
              double.Min(
                v1[j] + costOfDeletion, // Deletion.
                v0[j - 1] + costOfInsertion // Insertion.
              ),
              double.Min(
                v1[j - 1] + (equalityComparer.Equals(sourceItem, targetItem) ? 0 : costOfSubstitution), // Substitution.
                i > 1 && j > 1 && equalityComparer.Equals(sourceItem, target[j - 2]) && equalityComparer.Equals(source[i - 2], targetItem) ? v2[j - 2] + costOfTransposition : double.MaxValue // Transposition.
              )
            );
          }
        }

        return v0[tn];
      }

      #endregion

      #region OverlapCoefficient

      /// <summary>
      /// <para>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</para>
      /// <see href="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double OverlapCoefficient(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => (double)source.Intersect(target, equalityComparer).Count / (double)int.Min(source.Length, target.Length);

      #endregion

      #region PolynomialRollingHash

      /// <summary>
      /// <para><see href="https://cp-algorithms.com/string/string-hashing.html"/></para>
      /// </summary>
      /// <param name="p">It is reasonable to make this a prime number roughly equal to the number of characters in the input "alphabet".</param>
      /// <param name="m">This is the limit of the hash-value created. It should be a large number since the probability of two random strings colliding is about (1/m).</param>
      /// <param name="valueSelector">This is used to create some numerical value for each "letter" in the "alphabet".</param>
      /// <returns></returns>
      public long PolynomialRollingHash(long p, long m, System.Func<T, int, long> valueSelector)
      {
        var hash_value = 0L;
        var p_pow = 1L;

        for (var i = 0; i < source.Length; i++)
        {
          hash_value = (hash_value + (valueSelector(source[i], i) + 1) * p_pow) % m;

          p_pow = p_pow * p % m;
        }

        return hash_value;
      }

      #endregion

      #region PrefixFunction

      /// <summary>
      /// <para>The Prefix function for this span is an array of length n, where p[i] is the length of the longest proper prefix of the sub-span <paramref name="source"/>[0...i] which is also a suffix of this sub-span.</para>
      /// <para>A proper prefix of a span is a prefix that is not equal to the span itself.</para>
      /// <para>I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/></para>
      /// <para><see href="https://cp-algorithms.com/string/prefix-function.html"/></para>
      /// </summary>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int[] PrefixFunction(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var pi = new int[source.Length];

        for (int i = 1; i < source.Length; i++)
        {
          var j = pi[i - 1];

          while (j > 0 && !equalityComparer.Equals(source[i], source[j]))
            j = pi[j - 1];

          if (equalityComparer.Equals(source[i], source[j]))
            j++;

          pi[i] = j;
        }

        return pi;
      }

      #endregion

      public System.Range ShortestRepeatingSubstring(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        if (source.Length == 0)
          return System.Range.FromOffsetAndLength(0, 0);

        for (var subLength = 1; subLength <= source.Length; subLength++)
        {
          if (source.Length % subLength != 0) // Pattern must evenly divide sequence length.
            continue;

          var isString = true;
          var subRange = System.Range.FromOffsetAndLength(0, subLength); // Candidate target.

          var subString = source[subRange];

          for (var i = 0; i < source.Length; i++) // Check if repeating candidate forms the sequence.
            if (!equalityComparer.Equals(source[i], subString[i % subLength]))
            {
              isString = false;

              break;
            }

          if (isString)
            return subRange; // Found shortest repeating pattern
        }

        return System.Range.FromOffsetAndLength(0, source.Length); // If no repetition, the whole sequence is the pattern.
      }

      #region Set operations

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.Collections.Generic.HashSet<T> Except(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        if (source == target)
          return new(equalityComparer); // A set minus itself is an empty set.

        var except = source.ToHashSet(equalityComparer);

        if (target.Length > 0)
          except.RemoveSpan(target); // If target is empty, source is the result.

        return except;
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.Collections.Generic.HashSet<T> Intersect(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        var intersect = new System.Collections.Generic.HashSet<T>(int.Min(source.Length, target.Length));

        if (source.Length > 0 && target.Length > 0)
        {
          if (source.Length < target.Length)
            return Intersect(target, source, equalityComparer); // If source has more items, it's faster to switch it around with target.

          var intersectable = target.ToHashSet(equalityComparer);

          for (var index = source.Length - 1; index >= 0; index--)
          {
            if (source[index] is var item && intersectable.Remove(item))
              intersect.Add(item);

            if (intersectable.Count == 0)
              break;
          }
        }

        return intersect;
      }

      /// <summary>
      /// <para>Creates a new sequence, a set of all subsets (as lists) of the source set, including the empty set and the source itself.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Power_set"/></para>
      /// </summary>
      /// <returns></returns>
      public System.Collections.Generic.List<T[]> PowerSet()
      {
        var powerSet = new System.Collections.Generic.List<T[]>();

        var powerCount = (int)System.Numerics.BigInteger.Pow(2, source.Length);

        var subsetList = new System.Collections.Generic.List<T>(source.Length);

        for (var o = 0; o < powerCount; o++)
        {
          subsetList.Clear();

          for (var i = 0; i < powerCount; i++)
            if ((o & (1L << i)) > 0)
              subsetList.Add(source[i]);

          powerSet.Add(subsetList.ToArray());
        }

        return powerSet;
      }

      /// <summary>
      /// <para>Returns the number of unfound and also the number of unique elements between <paramref name="source"/> and <paramref name="target"/>.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="returnIfUnfound">Optional. Returns immediatly if <paramref name="returnIfUnfound"/> = true and an unfound element is found.</param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public (int unfoundCount, int uniqueCount) ScanSetCounts(System.ReadOnlySpan<T> target, bool returnIfUnfound, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var unfoundCount = 0;
        var unique = new System.Collections.Generic.HashSet<T>(equalityComparer);

        foreach (var item in target)
        {
          if (source.IndexOf(item, equalityComparer) > -1) unique.Add(item);
          else if (returnIfUnfound) break;
          else unfoundCount++;
        }

        return (unfoundCount, unique.Count);
      }

      /// <summary>
      /// <para>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the specified <paramref name="equalityComparer"/> (or default if null).</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.HashSet<T> SymmetricExcept(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        if (source == target)
          return new(equalityComparer); // A symmetric difference of a set with itself is an empty set.

        if (source.Length == 0)
          return target.ToHashSet(equalityComparer); // If source is empty, target is the result.

        if (target.Length == 0)
          return source.ToHashSet(equalityComparer); // If target is empty, source is the result.

        var symmetricExcept = source.Except(target, equalityComparer);
        symmetricExcept.UnionWith(target.Except(source, equalityComparer));
        return symmetricExcept;
      }

      /// <summary>
      /// <para>Union of distinct elements, i.e. no duplicates. Uses the specified <paramref name="equalityComparer"/> (or default if null).</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.Collections.Generic.HashSet<T> Union(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        var union = source.ToHashSet(equalityComparer);
        union.AddSpan(target);
        return union;
      }

      /// <summary>
      /// <para>Union of all elements, i.e. potential duplicates.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<T> UnionAll(System.ReadOnlySpan<T> target)
      {
        var unionAll = source.ToList();
        unionAll.AddRange(target);
        return unionAll;
      }

      #endregion

      #region ShortestCommonSupersequence

      /*
        var scs = new Flux.Metrical.ShortestCommonSupersequence<char>();

        var a = @"abcbdab";
        var b = @"bdcaba";

        var fm = scs.GetFullMatrix(a, b);
        var l = scs.GetList(a, b); // Now contains: "abdcabdab"
      */

      /// <summary>
      /// <para>Find the length of the shortest common supersequence (SCS) between two sequences, by creating a dynamic programming matrix.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Shortest_common_supersequence_problem"/></para>
      /// <para><seealso cref="http://rosettacode.org/wiki/Shortest_common_supersequence#C"/></para>
      /// <para><see href="https://www.techiedelight.com/shortest-common-supersequence-finding-scs/"/></para>
      /// <remarks>This is the same routine as longest common subsequence (LCS).</remarks>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="matrix"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.Collections.Generic.List<T> ShortestCommonSupersequence(System.ReadOnlySpan<T> target, out int[,] matrix, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        matrix = new int[source.Length + 1, target.Length + 1];

        for (int si = source.Length - 1; si >= 0; si--)
          matrix[si, 0] = si;
        for (int ti = target.Length - 1; ti >= 0; ti--)
          matrix[0, ti] = ti;

        for (var si = 0; si < source.Length; si++)
          for (var ti = 0; ti < target.Length; ti++)
            matrix[si + 1, ti + 1] = equalityComparer.Equals(source[si], target[ti]) ? matrix[si, ti] + 1 : int.Min(matrix[si, ti + 1], matrix[si + 1, ti]) + 1;

        return GetSupersequence(matrix, source, target, source.Length, target.Length, equalityComparer);
      }

      #endregion

      #region SplitByLength

      /// <summary>
      /// <para>Splits a <see cref="System.ReadOnlySpan{T}"/> into smaller ranges by a specified <paramref name="length"/>. If the source.Length is not a multiple of <paramref name="length"/> the last .</para>
      /// </summary>
      /// <param name="length"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<System.Range> SplitByLength(int length)
        => source.Length.GenerateSubRanges(length);

      #endregion

      #region To..

      /// <summary>
      /// <para>Creates a new <see cref="System.Array"/> with all elements from <paramref name="source"/>, and a <paramref name="preLength"/> and a <paramref name="postLength"/> number of default slots.</para>
      /// </summary>
      /// <param name="preLength">The number of array slots to add before the <paramref name="source"/> elements in the new <see cref="System.Array"/>.</param>
      /// <param name="postLength">The number of array slots to add after the <paramref name="source"/> elements in the new <see cref="System.Array"/>.</param>
      /// <returns></returns>
      public T[] ToArray(int preLength, int postLength)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(preLength);
        System.ArgumentOutOfRangeException.ThrowIfNegative(postLength);

        var target = new T[preLength + source.Length + postLength];
        source.CopyTo(new System.Span<T>(target, preLength, source.Length));
        return target;
      }

      /// <summary>
      /// <para>Creates a CSV (tabular data) string from the <see cref="ReadOnlySpan{T}"/>.</para>
      /// </summary>
      /// <param name="delimiter">The delimiter that separates the values.</param>
      /// <param name="trimWhiteSpace">Indicates whether leading and trailing white space should be trimmed from field values.</param>
      /// <param name="alwaysEncloseInDoubleQuotes">Whether to always add double quotes (true) or only add then when needed (false).</param>
      public string ToCsvString(string delimiter = ",", bool trimWhiteSpace = false, bool alwaysEncloseInDoubleQuotes = true)
      {
        var sb = new System.Text.StringBuilder();

        for (var i = 0; i < source.Length; i++)
        {
          if (i > 0) sb.Append(delimiter);

          var s = $"{source[i]}";

          if (trimWhiteSpace)
            s = s.Trim();

          if (!alwaysEncloseInDoubleQuotes || s.Contains(delimiter) || s.Contains(System.Environment.NewLine) || s.Contains('\n'))
            s = $"\"{s.Replace("\"", "\"\"")}\"";

          sb.Append(s);
        }

        return sb.ToString();
      }

      /// <summary>
      /// <para>Creates a new <see cref="System.Collections.Generic.HashSet{T}"/> with all elements from <paramref name="source"/> and the specified <paramref name="equalityComparer"/> (or default if null).</para>
      /// </summary>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <param name="additionalCapacity"></param>
      /// <returns></returns>
      public System.Collections.Generic.HashSet<T> ToHashSet(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        var target = new System.Collections.Generic.HashSet<T>(equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default);
        target.AddSpan(source);
        return target;
      }

      /// <summary>
      /// <para>Creates a new <see cref="System.Collections.Generic.List{T}"/> with all elements from <paramref name="source"/>.</para>
      /// </summary>
      /// <param name="additionalCapacity"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<T> ToList()
      {
        var target = new System.Collections.Generic.List<T>(source.Length);
        target.AddRange(source);
        return target;
      }

      ///// <summary>
      ///// <para>Creates a new <see cref="SpanMaker{T}"/> with all elements from <paramref name="source"/>.</para>
      ///// </summary>
      ///// <returns></returns>
      //public SpanBuilder<T> ToSpanBuilder()
      //  => new SpanBuilder<T>().Append(source);

      /// <summary>
      /// <para>Creates an URGF (Unicode tabular data) string from the <paramref name="source"/> data.</para>
      /// </summary>
      /// <returns></returns>
      public string ToUrgfString(System.Text.StringBuilder? sb = null)
      {
        sb ??= new System.Text.StringBuilder();

        for (var index = 0; index < source.Length; index++)
        {
          if (index > 0) sb.Append((char)UnicodeInformationSeparator.UnitSeparator);

          sb.Append(source[index]);
        }

        return sb.ToString();
      }

      #endregion

      #region TrimCommonEnds

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="sourceSlice"></param>
      /// <param name="targetSlice"></param>
      /// <param name="atStart"></param>
      /// <param name="atEnd"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      public void TrimCommonEnds(System.ReadOnlySpan<T> target, out System.ReadOnlySpan<T> sourceSlice, out System.ReadOnlySpan<T> targetSlice, out int atStart, out int atEnd, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      {
        sourceSlice = source;
        targetSlice = target;

        atStart = sourceSlice.CommonPrefixLength(targetSlice, equalityComparer);

        if (atStart > 0) // If equality exist in the beginning, adjust.
        {
          sourceSlice = sourceSlice[atStart..];
          targetSlice = targetSlice[atStart..];
        }

        atEnd = sourceSlice.CommonSuffixLength(targetSlice, equalityComparer);

        if (atEnd > 0) // If equality exist at the end, adjust.
        {
          sourceSlice = sourceSlice[..^atEnd];
          targetSlice = targetSlice[..^atEnd];
        }
      }

      #endregion

      #region ZFunction

      /// <summary>
      /// <para>The Z-function for this span is an array of length n where the i-th element is equal to the greatest number of <typeparamref name="T"/>'s starting from the position i that coincide with the first <typeparamref name="T"/>'s of <paramref name="source"/>.</para>
      /// <para>I.e. z[i] is the length of the longest sub-span that is, at the same time, a prefix of <paramref name="source"/> and a prefix of the suffix of <paramref name="source"/> starting at i.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// <para><see href="https://cp-algorithms.com/string/z-function.html"/></para>
      /// </summary>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int[] Zfunction(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var z = new int[source.Length];

        var l = 0;
        var r = 0;

        for (var i = 1; i < source.Length; i++)
        {
          if (i < r)
            z[i] = int.Min(r - i, z[i - l]);

          while (i + z[i] < source.Length && equalityComparer.Equals(source[z[i]], source[i + z[i]]))
            z[i]++;

          if (i + z[i] > r)
          {
            l = i;
            r = i + z[i];
          }
        }

        return z;
      }

      #endregion
    }

    extension<T>(System.ReadOnlySpan<T> source)
       where T : notnull
    {
      #region AreIsomorphic

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> and <paramref name="target"/> are isomorphic. Two sequences are isomorphic if the elements (equal elements must be replaced with the same replacements, in the same positions) in <paramref name="source"/> can be replaced to get the same in <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool AreIsomorphic(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        if (source.Length != target.Length)
          return false;

        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var map1 = new System.Collections.Generic.Dictionary<T, T>(equalityComparer);
        var map2 = new System.Collections.Generic.Dictionary<T, T>(equalityComparer);

        for (var i = source.Length - 1; i >= 0; i--)
        {
          var c1 = source[i];
          var c2 = target[i];

          if (map1.TryGetValue(c1, out T? value))
          {
            if (!equalityComparer.Equals(c2, value))
              return false;
          }
          else
          {
            if (map2.ContainsKey(c2))
              return false;

            map1[c1] = c2;
            map2[c2] = c1;
          }
        }

        return true;
      }

      #endregion

      #region Boyer, Moore & Horspool algorithm

      /// <summary>
      /// <para>Searches a text (source) for the index of a substring (target). Returns -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore%E2%80%93Horspool_algorithm"/></para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="table"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int BoyerMooreHorspoolIndex(System.ReadOnlySpan<T> target, out System.Collections.Generic.Dictionary<T, int> table, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        table = BoyerMooreHorspoolTable(source, target, equalityComparer); // Skippables.

        var skip = 0;

        var sourceLength = source.Length;
        var targetLength = target.Length;

        while (sourceLength - skip >= targetLength)
        {
          if (BoyerMooreHorspoolIsSame(source[skip..], target, targetLength, equalityComparer))
            return skip;

          skip += table[source[skip + targetLength - 1]];
        }

        return -1;
      }

      /// <summary>
      /// <para>Creates a map of the amount of safely skippable elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// <see href="https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore%E2%80%93Horspool_algorithm"/>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public System.Collections.Generic.Dictionary<T, int> BoyerMooreHorspoolTable(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var table = new System.Collections.Generic.Dictionary<T, int>(equalityComparer); // The alphabet.

        var sourceLength = source.Length;
        var targetLength = target.Length;

        for (var index = int.Max(targetLength, sourceLength) - 1; index >= 0; index--)
        {
          if (index < targetLength && target[index] is var wc && !table.ContainsKey(wc)) // Add to alphabet from source (word/needle), if it is not already in the table.
            table.Add(wc, targetLength);
          if (index < sourceLength && source[index] is var tc && !table.ContainsKey(tc)) // Add to alphabet from target (text/haystack), if it is not already in the table.
            table.Add(tc, targetLength);
        }

        for (var i = 0; i < targetLength; i++)
          table[target[i]] = targetLength - 1 - i;

        return table;
      }

      #endregion

      #region DamerauLevenshteinDistance

      /// <summary>
      /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
      /// </summary>
      /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
      /// <param name="target"></param>
      /// <param name="matrix"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int DamerauLevenshteinDistance(System.ReadOnlySpan<T> target, out int[,] matrix, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

        matrix = new int[0, 0];

        if (source.Length == 0) return target.Length;
        else if (target.Length == 0) return source.Length;

        matrix = DamerauLevenshteinDistanceMatrix(source, target, equalityComparer);

        return matrix[source.Length + 1, target.Length + 1];
      }

      /// <summary>
      /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
      /// </summary>
      /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int[,] DamerauLevenshteinDistanceMatrix(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var dp = new int[source.Length + 2, target.Length + 2];

        var dr = new System.Collections.Generic.Dictionary<T, int>(equalityComparer); // Dictionary of items from both lists.

        for (var si = source.Length - 1; si >= 0; si--)
          if (!dr.ContainsKey(source[si]))
            dr[source[si]] = 0;
        for (var ti = target.Length - 1; ti >= 0; ti--)
          if (!dr.ContainsKey(target[ti]))
            dr[target[ti]] = 0;

        var maxDistance = source.Length + target.Length;

        dp[0, 0] = maxDistance;

        for (var si = source.Length + 1; si >= 1; si--)
        {
          dp[si, 1] = si - 1;
          dp[si, 0] = maxDistance;
        }
        for (var ti = target.Length + 1; ti >= 1; ti--)
        {
          dp[1, ti] = ti - 1;
          dp[0, ti] = maxDistance;
        }

        for (var si = 1; si <= source.Length; si++)
        {
          var ltim = 0; // Last target index of target item matching source item.

          var sourceItem = source[si - 1];

          for (var ti = 1; ti <= target.Length; ti++)
          {
            var targetItem = target[ti - 1];

            var lsi = dr[targetItem]; // Last source index of source item.

            var isEqual = equalityComparer.Equals(sourceItem, targetItem);

            dp[si + 1, ti + 1] = int.Min(
              int.Min(
                dp[si, ti + 1] + 1, // Deletion.
                dp[si + 1, ti] + 1 // Insertion
              ),
              int.Min(
                isEqual ? dp[si, ti] : dp[si, ti] + 1, // Substitution.
                dp[lsi, ltim] + (si - lsi - 1) + 1 + (ti - ltim - 1) // Transposition.
              )
            );

            if (isEqual)
              ltim = ti;
          }

          dr[sourceItem] = si;
        }

        return dp;
      }

      /// <summary>
      /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
      /// </summary>
      /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double DamerauLevenshteinDistanceSmc(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => 1d - DamerauLevenshteinDistanceSmd(source, target, equalityComparer);

      /// <summary>
      /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
      /// </summary>
      /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
      /// <param name="target"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double DamerauLevenshteinDistanceSmd(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => (double)DamerauLevenshteinDistance(source, target, out var _, equalityComparer) / (double)int.Max(source.Length, target.Length);

      #endregion

      #region DamerauLevenshteinDistanceCustom

      /// <summary>
      /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
      /// </summary>
      /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
      /// <param name="target"></param>
      /// <param name="costOfDeletion"></param>
      /// <param name="costOfInsertion"></param>
      /// <param name="costOfSubstitution"></param>
      /// <param name="costOfTransposition"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double DamerauLevenshteinDistanceCustom(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, double costOfTransposition = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

        if (source.Length == 0) return target.Length;
        else if (target.Length == 0) return source.Length;

        var matrix = DamerauLevenshteinDistanceCustomMatrix(source, target, costOfDeletion, costOfInsertion, costOfSubstitution, costOfTransposition, equalityComparer);

        return matrix[source.Length + 1, target.Length + 1];
      }

      /// <summary>
      /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
      /// </summary>
      /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
      /// <param name="target"></param>
      /// <param name="costOfDeletion"></param>
      /// <param name="costOfInsertion"></param>
      /// <param name="costOfSubstitution"></param>
      /// <param name="costOfTransposition"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public double[,] DamerauLevenshteinDistanceCustomMatrix(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, double costOfTransposition = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var ldg = new double[source.Length + 2, target.Length + 2];

        var dr = new System.Collections.Generic.Dictionary<T, int>(equalityComparer); // Dictionary of items from both lists.
        for (var si = source.Length - 1; si >= 0; si--)
          if (!dr.ContainsKey(source[si]))
            dr[source[si]] = 0;
        for (var ti = target.Length - 1; ti >= 0; ti--)
          if (!dr.ContainsKey(target[ti]))
            dr[target[ti]] = 0;

        var maxDistance = (source.Length + target.Length) * costOfInsertion;

        ldg[0, 0] = maxDistance;

        for (var i = source.Length + 1; i >= 1; i--)
        {
          ldg[i, 1] = (i - 1) * costOfInsertion;
          ldg[i, 0] = maxDistance;
        }
        for (var j = target.Length + 1; j >= 1; j--)
        {
          ldg[1, j] = (j - 1) * costOfInsertion;
          ldg[0, j] = maxDistance;
        }

        for (var si = 1; si <= source.Length; si++)
        {
          var ltim = 0; // Last target index of target item matching source item.

          var sourceItem = source[si - 1];

          for (var ti = 1; ti <= target.Length; ti++)
          {
            var targetItem = target[ti - 1];

            var lsi = dr[targetItem]; // Last source index of source item.

            var isEqual = equalityComparer.Equals(sourceItem, targetItem);

            ldg[si + 1, ti + 1] = double.Min(
              double.Min(
                ldg[si, ti + 1] + costOfDeletion,
                ldg[si + 1, ti] + costOfInsertion
              ),
              double.Min(
                isEqual ? ldg[si, ti] : ldg[si, ti] + costOfSubstitution,
                ldg[lsi, ltim] + (si - lsi - 1) + costOfTransposition + (ti - ltim - 1)
              )
            );

            if (isEqual)
              ltim = ti;
          }

          dr[sourceItem] = si;
        }

        return ldg;
      }

      #endregion

      #region ShortestBalancingSubstring

      /// <summary>
      /// <para>Finds the shortest substring that can be replaced to achieve a balanced count of all <paramref name="vocabulary"/> elements in <paramref name="source"/> with no change in space requirements.</para>
      /// <see href="https://stackoverflow.com/questions/38877718/smallest-substring-that-can-be-replaced-to-make-the-string-have-the-same-number"/>
      /// </summary>
      /// <param name="vocabulary"></param>
      /// <returns>A 2-tuple with (index, count) of elements in the shortest balancing substring, or (-1, 0) if not found.</returns>
      public System.Range ShortestBalancingSubstring(System.ReadOnlySpan<T> vocabulary)
      {
        var frequencies = new System.Collections.Generic.Dictionary<T, int>();

        for (var i = 0; i < vocabulary.Length; i++)
          frequencies.Add(vocabulary[i], 0);

        for (int i = 0; i < source.Length; ++i)
          frequencies[source[i]] += 1;

        var balancedCount = source.Length / 4; // This is the target count for each item in the vocabulary.

        if (!frequencies.Where(p => p.Value != balancedCount).Any())
          return System.Range.FromOffsetAndLength(0, 0);

        var minLength = frequencies.Where(p => p.Value > balancedCount).Sum(p => p.Value - balancedCount);

        var surplusCharacter = frequencies.Where(p => p.Value > balancedCount).Select(p => p.Key).ToList();

        var index = -1;
        var count = 0;

        var shortest = source;

        for (var left = 0; left < source.Length - minLength + 1; left++)
          if (surplusCharacter.Contains(source[left]))
          {
            if (minLength == 1)
              return System.Range.FromOffsetAndLength(0, 1);

            for (var right = left + minLength - 1; right < source.Length; right++)
              if (surplusCharacter.Contains(source[right]))
              {
                var subString = source[left..right];

                if (subString.Length >= shortest.Length)
                  break;

                var isBalancing = true; // True (by default) if substring can be the string that needs to be replaced.

                foreach (var sc in surplusCharacter)
                  if (subString.Count(sc, null) < frequencies[sc] - balancedCount)
                  {
                    isBalancing = false;
                    break;
                  }

                if (isBalancing)
                {
                  index = left;
                  count = right - left;

                  shortest = subString;
                }
              }
          }

        return System.Range.FromOffsetAndLength(index, count);
      }

      #endregion
    }

    extension(System.ReadOnlySpan<char> source)
    {
      #region Balanced constructs

      /// <summary>
      /// <para>Checks whether the string has balanced pairs (e.g. parenthesis).</para>
      /// </summary>
      public bool IsBalanced(char open, char close)
        => IsBalancedConstruct(source, $"\\{open}", $"[^\\{open}\\{close}]", $"\\{close}");

      /// <summary>
      /// <para>Checks whether the string is a matching balanced construct.</para>
      /// <para><see href="https://www.regular-expressions.info/balancing.html"/></para>
      /// </summary>
      /// <example>See below for balanced parenthesis.</example>
      public bool IsBalancedConstruct(System.ReadOnlySpan<char> open, System.ReadOnlySpan<char> match, System.ReadOnlySpan<char> close)
        => System.Text.RegularExpressions.Regex.IsMatch(source, CreateBalancedExpression(open, match, close));

      #endregion

      #region IndexOfAny

      /// <summary>
      /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>, or -1 if none were found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int IndexOfAny(System.Collections.Generic.IEnumerable<string> any, System.Collections.Generic.IEqualityComparer<char> equalityComparer)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        foreach (var value in any)
          if (IndexOf(source, value, equalityComparer) is var index && index > -1)
            return index;

        return -1;
      }

      #endregion

      #region IsCommonPrefixAny

      /// <summary>
      /// <para>Returns whether <paramref name="source"/> starts with <paramref name="maxTestLength"/> (or the actual length if less) of any string <paramref name="values"/>.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="maxTestLength"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool IsCommonPrefixAny(System.Collections.Generic.IEnumerable<string> any, int maxTestLength, System.Collections.Generic.IEqualityComparer<char>? equalityComparer)
      {
        foreach (var value in any)
          if (IsCommonPrefix(source, value.AsSpan()[..int.Min(value.Length, maxTestLength)], equalityComparer))
            return true;

        return false;
      }

      /// <summary>
      /// <para>Returns whether <paramref name="source"/> starts with any of the string <paramref name="values"/>.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool IsCommonPrefixAny(System.Collections.Generic.IEnumerable<string> any, System.Collections.Generic.IEqualityComparer<char>? equalityComparer)
        => IsCommonPrefixAny(source, any, int.MaxValue, equalityComparer);

      #endregion

      #region IsCommonSuffixAny

      /// <summary>
      /// <para>Returns whether <paramref name="source"/> ends with <paramref name="maxTestLength"/> (or the actual length if less) of any string <paramref name="values"/>.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="maxTestLength"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool IsCommonSuffixAny(System.Collections.Generic.IEnumerable<string> any, int maxTestLength, System.Collections.Generic.IEqualityComparer<char>? equalityComparer)
      {
        foreach (var value in any)
          if (IsCommonSuffix(source, value.AsSpan()[..int.Min(value.Length, maxTestLength)], equalityComparer))
            return true;

        return false;
      }

      /// <summary>
      /// <para>Returns whether <paramref name="source"/> ends with any of the string <paramref name="values"/>.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public bool IsCommonSuffixAny(System.Collections.Generic.IEnumerable<string> any, System.Collections.Generic.IEqualityComparer<char>? equalityComparer)
        => IsCommonSuffixAny(source, any, int.MaxValue, equalityComparer);

      #endregion // System.Char extension methods

      #region IsPalindrome

      /// <summary>Matches palindromes of any length.</summary>
      /// <see href="https://www.regular-expressions.info/balancing.html"/>
      public bool IsPalindrome()
        => RegexPalindrome().IsMatch(source);

      #endregion

      #region IsTextElement

      /// <summary>
      /// <para>Determines whether the <paramref name="source"/> represents a single text-element not many, i.e. a grapheme not a cluster.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public bool IsTextElement()
        => System.Globalization.StringInfo.GetNextTextElementLength(source) == source.Length;

      #endregion

      #region LastIndexOfAny

      /// <summary>
      /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>. or -1 if none is found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="any"></param>
      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
      /// <returns></returns>
      public int LastIndexOfAny(System.Collections.Generic.IEnumerable<string> any, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        foreach (var value in any)
          if (LastIndexOf(source, value, equalityComparer) is var index && index > -1)
            return index;

        return -1;
      }

      #endregion

      #region MainLorentz algorithm

      /// <summary>
      /// <para>Find all repetitions. A repetition is two occurrences of a string in a row, i.e. two adjacent sequences (of one or more elements). In other words a repetition can be described by a pair of indices  i&lt;j  such that the substring s[i...j]  consists of two identical strings written after each other.</para>
      /// <see href="https://cp-algorithms.com/string/main_lorentz.html"/>
      /// </summary>
      /// <returns></returns>
      public System.Collections.Generic.List<System.Range> MainLorentzRepetitions()
      {
        var repetitions = new System.Collections.Generic.List<System.Range>();

        LocateRepetitions(repetitions, source);

        return repetitions;
      }

      #endregion

      #region RabinKarp algorithm

      /// <summary>
      /// <para>Given a <paramref name="pattern"/> and a source text, determine if the pattern appears in the text and if it does, return the indices of all occurences in <c>O(|pattern| + |text|)</c> time.</para>
      /// <para><see href="https://cp-algorithms.com/string/rabin-karp.html"/></para>
      /// </summary>
      /// <param name="pattern"></param>
      /// <returns>The indices of all occurences of <paramref name="pattern"/> in the source text.</returns>
      public System.Collections.Generic.List<int> RabinKarpIndices(System.ReadOnlySpan<char> pattern)
      {
        const int p = 31;
        const int m = 1000000009;

        var sl = pattern.Length;
        var tl = source.Length;

        var p_pow = new long[int.Max(sl, tl)];
        p_pow[0] = 1;
        for (var i = 1; i < (int)p_pow.Length; i++)
          p_pow[i] = (p_pow[i - 1] * p) % m;

        var h = new long[tl + 1];
        for (var i = 0; i < tl; i++)
          h[i + 1] = (h[i] + (source[i] - 'a' + 1) * p_pow[i]) % m;

        var h_s = 0L;
        for (var i = 0; i < sl; i++)
          h_s = (h_s + (pattern[i] - 'a' + 1) * p_pow[i]) % m;

        var occurrences = new System.Collections.Generic.List<int>();

        for (var i = 0; i + sl - 1 < tl; i++)
        {
          var cur_h = (h[i + sl] + m - h[i]) % m;

          if (cur_h == h_s * p_pow[i] % m)
            occurrences.Add(i);
        }

        return occurrences;
      }

      #endregion

      #region Regex

      /// <summary>
      /// <para>Creates a new list of ranges from matching <paramref name="regexPattern"/> in a <see cref="System.ReadOnlySpan{T}"/>.</para>
      /// </summary>
      /// <param name="regexPattern"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<Range> GetRegexMatches(string regexPattern)
      {
        var ranges = new System.Collections.Generic.List<Range>();
        foreach (var vm in new System.Text.RegularExpressions.Regex(regexPattern).EnumerateMatches(source))
          ranges.Add(System.Range.FromOffsetAndLength(vm.Index, vm.Length));
        return ranges;
      }

      /// <summary>
      /// <para>Creates a new list of ranges from splitting by <paramref name="regexPattern"/> in a <see cref="System.ReadOnlySpan{T}"/>.</para>
      /// </summary>
      /// <param name="regexPattern"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<Range> GetRegexSplits(string regexPattern)
      {
        var ranges = new System.Collections.Generic.List<Range>();
        foreach (var range in new System.Text.RegularExpressions.Regex(regexPattern).EnumerateSplits(source))
          ranges.Add(range);
        return ranges;
      }

      #endregion

      #region To..

      /// <summary>
      /// <para>Creates a new <see cref="System.Text.StringBuilder"/> with all characters from <paramref name="source"/>.</para>
      /// </summary>
      public System.Text.StringBuilder ToStringBuilder()
        => new System.Text.StringBuilder().Append(source);

      #endregion

      #region UtfSequenceLength

      /// <summary>
      /// <para><see cref="https://www.rfc-editor.org/rfc/rfc3629#section-3"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public int Utf8SequenceLength()
      {
        var length = 0;
        for (var i = source.Length - 1; i >= 0; i--)
          length += source[i].Utf8SequenceLength();
        return length;
      }

      #endregion
    }

    extension<TInteger>(System.ReadOnlySpan<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region SubsetSum problem

      /// <summary>
      /// <para>Build a boolean table matrix using dynamic programming.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Subset_sum_problem"/></para>
      /// <para><seealso href="https://www.geeksforgeeks.org/subset-sum-problem-dp-25/"/></para>
      /// <para><remarks>The subset sum problem (SSP) is a decision problem in computer science. Given a set of non-negative integers, and a value sum, determine if there is a subset of the given set with sum equal to given sum.</remarks></para>
      /// </summary>
      /// <param name="targetSum"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public bool[,] SubsetSumMatrix(int targetSum)
      {
        // The state matrix[i][j] will be true if there exists a subset of elements from A[0….i] with sum value = 'j'.
        var dp = new bool[targetSum + 1, source.Length + 1]; // The value of subset[i][j] will be true if there is a subset of set[0..j-1] with sum equal to i.

        for (var i = source.Length - 1; i >= 0; i--) // If sum is 0, then answer is true.
          dp[0, i] = true;

        for (var i = 1; i <= targetSum; i++)
        {
          dp[i, 0] = false;

          for (var j = 1; j <= source.Length; j++)
          {
            var b = dp[i, j - 1];

            dp[i, j] = b;

            if (source[j - 1] is var jm1 && TInteger.CreateChecked(i) is var it && it >= jm1)
            {
              if (TInteger.IsNegative(jm1)) throw new System.ArgumentOutOfRangeException(nameof(source), "All values must be non-negative."); // Instead of checking all values prior, we simply throw when a value is found non-negative.

              dp[i, j] = b || dp[int.CreateChecked(it - jm1), j - 1];
            }
          }
        }

        // System.Diagnostics.Debug.WriteLine($"{nameof(SubsetSum)}.{nameof(GetDpMatrix)}():{System.Environment.NewLine}{dp.TransposeToCopy().ToConsole()}");

        return dp;
      }

      /// <summary>
      /// <para>Given a <paramref name="source"/> set of non-negative integers, and a value <paramref name="targetSum"/> sum, determine if there is a subset of the given <paramref name="source"/> set with sum equal to given <paramref name="targetSum"/> sum.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Subset_sum_problem"/></para>
      /// <para><seealso href="https://www.geeksforgeeks.org/subset-sum-problem-dp-25/"/></para>
      /// <para><remarks>The subset sum problem (SSP) is a decision problem in computer science. Given a set of non-negative integers, and a value sum, determine if there is a subset of the given set with sum equal to given sum.</remarks></para>
      /// </summary>
      /// <param name="targetSum"></param>
      /// <param name="dpmatrix"></param>
      /// <returns>Whether there is a subset of the given set with sum equal to given sum.</returns>
      public bool IsSubsetSum(int targetSum, out bool[,] dpmatrix)
      {
        dpmatrix = SubsetSumMatrix(source, targetSum);

        return dpmatrix[targetSum, source.Length];
      }

      /// <summary>
      /// <para>Given a <paramref name="source"/> set of non-negative integers, and a value <paramref name="targetSum"/> sum, determine if there is a subset of the given <paramref name="source"/> set with sum equal to given <paramref name="targetSum"/> sum.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Subset_sum_problem"/></para>
      /// <para><seealso href="https://www.geeksforgeeks.org/subset-sum-problem-dp-25/"/></para>
      /// <para><remarks>The subset sum problem (SSP) is a decision problem in computer science. Given a set of non-negative integers, and a value sum, determine if there is a subset of the given set with sum equal to given sum.</remarks></para>
      /// </summary>
      /// <param name="targetSum"></param>
      /// <returns>Whether there is a subset of the given set with sum equal to given sum.</returns>
      public bool IsSubsetSum(int targetSum)
        => IsSubsetSum(source, targetSum, out var _);

      #endregion
    }

    extension<TNumber>(System.ReadOnlySpan<TNumber> source)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region MaximumSumSubarray

      /// <summary>
      /// <para>Find the maximum sum subarray in a span of values.</para>
      /// <see href="https://en.wikipedia.org/wiki/Maximum_subarray_problem"/>
      /// </summary>
      /// <returns></returns>
      /// <remarks>In computer science, the maximum sum subarray problem, also known as the maximum segment sum problem, is the task of finding a contiguous subarray with the largest sum, within a given one-dimensional array of numbers.</remarks>
      public (int Index, int Count, TNumber Sum) MaximumSumSubarray()
      {
        var bestSum = -TNumber.One;
        var bestStartIndex = -1;
        var bestEndIndex = -1;

        var currentSum = -TNumber.One;
        var currentStartIndex = 0;
        var currentEndIndex = 0;

        for (var index = 0; index < source.Length; index++)
        {
          if (currentSum <= TNumber.Zero) // Start a new sequence at the current element.
          {
            currentStartIndex = currentEndIndex;
            currentSum = source[index];
          }
          else // Extend the existing sequence with the current element.
            currentSum += source[index];

          if (currentSum > bestSum)
          {
            bestSum = currentSum;
            bestStartIndex = currentStartIndex;
            bestEndIndex = currentEndIndex;
          }

          currentEndIndex++;
        }

        return (
          bestStartIndex,
          bestEndIndex - bestStartIndex + 1,
          bestSum
        );
      }

      #endregion
    }

    /// <summary>
    /// <para>Returns a column name of the <paramref name="index"/>ed column from the array as if it were an array of column names, substituting if not enough column names are specified.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string EnsureColumnName(this System.ReadOnlySpan<string> source, int index)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(index);

      return index < source.Length && source[index] is var value && !string.IsNullOrWhiteSpace(value)
        ? source[index]
        : index.ToSingleOrdinalColumnName();
    }

    #region BalancedConstructs helpers

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="reOpen"></param>
    /// <param name="reMatch"></param>
    /// <param name="reClose"></param>
    /// <returns></returns>
    public static string CreateBalancedExpression(System.ReadOnlySpan<char> reOpen, System.ReadOnlySpan<char> reMatch, System.ReadOnlySpan<char> reClose)
      => $"^{reMatch}*(?>(?>(?'balance'{reOpen}){reMatch}*)+(?>(?'-balance'{reClose}){reMatch}*)+)+(?(balance)(?!))$";

    #endregion

    #region BoyerMooreHorspool algorithm helpers

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="length"></param>
    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
    /// <returns></returns>
    private static bool BoyerMooreHorspoolIsSame<T>(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int length, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      where T : notnull
    {
      for (var i = length - 1; i >= 0; i--)
        if (!equalityComparer.Equals(source[i], target[i]))
          return false;

      return true;
    }

    #endregion

    #region IsPalindrome helpers

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <returns></returns>
    [System.Text.RegularExpressions.GeneratedRegex(@"^(?'letter'\p{L})+\p{L}?(?:\k'letter'(?'-letter'))+(?(letter)(?!))$")]
    public static partial System.Text.RegularExpressions.Regex RegexPalindrome();

    #endregion

    #region MainLorentz algorithm helpers

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="repetitions"></param>
    /// <param name="shift"></param>
    /// <param name="left"></param>
    /// <param name="cntr"></param>
    /// <param name="l"></param>
    /// <param name="k1"></param>
    /// <param name="k2"></param>
    private static void ConvertToRepetitions(System.Collections.Generic.List<System.Range> repetitions, int shift, bool left, int cntr, int l, int k1, int k2)
    {
      for (var l1 = int.Max(1, l - k2); l1 <= int.Min(l, k1); l1++)
      {
        if (left && l1 == l) break;
        //var l2 = l - l1;
        var pos = shift + (left ? cntr - l1 : cntr - l - l1 + 1);

        repetitions.Insert(0, new System.Range(pos, pos + 2 * l));
      }
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="z"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    private static int GetZ(int[] z, int i)
    {
      if (0 <= i && i < z.Length)
        return z[i];
      else
        return 0;
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="repetitions"></param>
    /// <param name="ros"></param>
    /// <param name="shift"></param>
    private static void LocateRepetitions(System.Collections.Generic.List<System.Range> repetitions, System.ReadOnlySpan<char> ros, int shift = 0)
    {
      if (ros.Length is var n && n <= 1)
        return;

      var s = ros.ToString();

      var nu = n / 2;
      var nv = n - nu;
      var u = s[..nu];
      var v = s[nu..];
      var ru = string.Concat(u.Reverse());
      var rv = string.Concat(v.Reverse());

      LocateRepetitions(repetitions, u, shift);
      LocateRepetitions(repetitions, v, shift + nu);

      var z1 = ru.AsSpan().Zfunction();
      var z2 = (v + '#' + u).AsSpan().Zfunction();
      var z3 = (ru + '#' + rv).AsSpan().Zfunction();
      var z4 = v.AsSpan().Zfunction();

      //var z1 = Zfunction(ru);
      //var z2 = Zfunction(v + '#' + u);
      //var z3 = Zfunction(ru + '#' + rv);
      //var z4 = Zfunction(v);

      for (var cntr = 0; cntr < n; cntr++)
      {
        int l, k1, k2;

        if (cntr < nu)
        {
          l = nu - cntr;
          k1 = GetZ(z1, nu - cntr);
          k2 = GetZ(z2, nv + 1 + cntr);
        }
        else
        {
          l = cntr - nu + 1;
          k1 = GetZ(z3, nu + 1 + nv - 1 - (cntr - nu));
          k2 = GetZ(z4, (cntr - nu) + 1);
        }

        if (k1 + k2 >= l)
          ConvertToRepetitions(repetitions, shift, cntr < nu, cntr, l, k1, k2);
      }
    }

    #endregion

    #region ShortestCommonSupersequence helpers

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="matrix"></param>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="si"></param>
    /// <param name="ti"></param>
    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
    /// <returns></returns>
    private static System.Collections.Generic.List<T> GetSupersequence<T>(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int si, int ti, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (si == 0) // If the end of the first string is reached, return the second string.
        return target[..ti].ToArray().ToList();
      else if (ti == 0) // If the end of the second string is reached, return the first string.
        return source[..si].ToArray().ToList();

      if (equalityComparer.Equals(source[si - 1], target[ti - 1])) // If the last character of si and ti matches, include it and recur to find SCS of substring.
      {
        var list = GetSupersequence(matrix, source, target, si - 1, ti - 1, equalityComparer);
        list.Add(source[si - 1]);
        return list;
      }
      else
      {
        if (matrix[si - 1, ti] <= matrix[si, ti - 1]) // If the top cell has a value less or equal to that in the left cell, then include the current source element and find SCS of substring less the one added.
        {
          var list = GetSupersequence(matrix, source, target, si - 1, ti, equalityComparer);
          list.Add(source[si - 1]);
          return list;
        }
        else // If the left cell has a value greater than that in the top cell, then include the current target element find SCS of substring less the one added.
        {
          var list = GetSupersequence(matrix, source, target, si, ti - 1, equalityComparer);
          list.Add(target[ti - 1]);
          return list;
        }
      }
    }

    #endregion

    #region To.. helpers

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToUrgfString(this System.ReadOnlySpan<string> source)
      => string.Join((char)UnicodeInformationSeparator.UnitSeparator, source);

    #endregion
  }
}
