namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension(System.ReadOnlySpan<char> source)
    {
      #region IsCommonSuffixAny

      /// <summary>
      /// <para>Returns whether <paramref name="source"/> ends with <paramref name="maxLength"/> (or the actual length if less) of any string <paramref name="values"/>.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      public bool IsCommonSuffixAny(int maxLength, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
      {
        for (var valuesIndex = 0; valuesIndex < values.Length; valuesIndex++)
          if (values[valuesIndex] is var value && IsCommonSuffix(source, value.AsSpan()[..int.Min(value.Length, maxLength)], equalityComparer))
            return true;

        return false;
      }

      /// <summary>
      /// <para>Returns whether <paramref name="source"/> ends with any of the string <paramref name="values"/>.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      public bool IsCommonSuffixAny(System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
        => IsCommonSuffixAny(source, int.MaxValue, equalityComparer, values);

      #endregion // System.Char extension methods
    }

    extension<T>(System.ReadOnlySpan<T> source)
    {
      #region CommonSuffixLength

      /// <summary>
      /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and the <paramref name="predicate"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
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
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <param name="maxTestLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonSuffixLength(System.Func<T, bool> predicate, int maxTestLength = int.MaxValue)
        => CommonSuffixLength(source, (e, i) => predicate(e), maxTestLength);

      /// <summary>
      /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="maxTestLength"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonSuffixLength(T value, int maxTestLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTestLength);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return CommonSuffixLength(source, (e, i) => equalityComparer.Equals(e, value), maxTestLength);
      }

      /// <summary>
      /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <remarks>
      /// <para>There is a built-in method for <see cref="System.MemoryExtensions.CommonPrefixLength{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, IEqualityComparer{T}?)"/> but not one for common-suffix-length.</para>
      /// </remarks>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="maxLength"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int CommonSuffixLength(System.ReadOnlySpan<T> target, int maxLength, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxLength);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var sourceMaxIndex = source.Length - 1;
        var targetMaxIndex = target.Length - 1;

        maxLength = int.Min(maxLength, int.Min(sourceMaxIndex, targetMaxIndex));

        var length = 0;
        while (length < maxLength && equalityComparer.Equals(source[sourceMaxIndex - length], target[targetMaxIndex - length]))
          length++;
        return length;
      }

      /// <summary>
      /// <para>Simulating a built-in version of a CommonSuffixLength extension (akin to the actual built-in CommonPrefixLength extension).</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="maxLength"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int CommonSuffixLength(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonSuffixLength(source, target, int.MaxValue, equalityComparer);

      #endregion

      #region IsCommonSuffixAny

      /// <summary>
      /// <para>Returns whether <paramref name="source"/> ends with any <paramref name="length"/> combination of <paramref name="values"/>. Uses the <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="length"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public bool IsCommonSuffixAny(int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
        => IsCommonSuffix(source, length, c => values.Contains(c, equalityComparer));

      #endregion // IsCommonSuffixAny

      #region IsCommonSuffix

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> ends with <paramref name="length"/> elements that satisfy the <paramref name="predicate"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="length"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public bool IsCommonSuffix(int length, System.Func<T, bool> predicate)
        => CommonSuffixLength(source, predicate, length) == length;

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> ends with at least <paramref name="length"/> occurences of the <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="length"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool IsCommonSuffix(int length, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonSuffixLength(source, value, length, equalityComparer) == length;

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> ends with at least <paramref name="length"/> elements from the <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="length"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool IsCommonSuffix(System.ReadOnlySpan<T> target, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonSuffixLength(source, target, length, equalityComparer) == length;

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> ends with the <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool IsCommonSuffix(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => IsCommonSuffix(source, target, target.Length, equalityComparer);

      #endregion

      #region TrimCommonSuffix

      /// <summary>
      /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of matching suffix elements satisfying the <paramref name="predicate"/> removed.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonSuffix(System.Func<T, bool> predicate, int maxTrimLength = int.MaxValue)
        => source[..^CommonSuffixLength(source, predicate, maxTrimLength)];

      /// <summary>
      /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of suffix elements matching <paramref name="value"/> removed.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonSuffix(T value, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => source[..^CommonSuffixLength(source, value, maxTrimLength, equalityComparer)];

      /// <summary>
      /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of matching suffix elements from <paramref name="value"/> removed.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonSuffix(System.ReadOnlySpan<T> value, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => source[..^CommonSuffixLength(source, value, maxTrimLength, equalityComparer)];

      #endregion
    }
  }
}
