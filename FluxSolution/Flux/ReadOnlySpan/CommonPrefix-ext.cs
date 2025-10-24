namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension(System.ReadOnlySpan<char> source)
    {
      #region IsCommonPrefixAny

      /// <summary>
      /// <para>Returns whether <paramref name="source"/> starts with <paramref name="maxLength"/> (or the actual length if less) of any string <paramref name="values"/>.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="maxLength"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public bool IsCommonPrefixAny(int maxLength, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
      {
        for (var valuesIndex = 0; valuesIndex < values.Length; valuesIndex++)
          if (values[valuesIndex] is var value && IsCommonPrefix(source, value.AsSpan()[..int.Min(value.Length, maxLength)], equalityComparer))
            return true;

        return false;
      }

      /// <summary>
      /// <para>Returns whether <paramref name="source"/> starts with any of the string <paramref name="values"/>.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public bool IsCommonPrefixAny(System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
        => IsCommonPrefixAny(source, int.MaxValue, equalityComparer, values);

      #endregion
    }

    extension<T>(System.ReadOnlySpan<T> source)
    {
      #region CommonPrefixLength

      /// <summary>
      /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and the <paramref name="predicate"/> satisfied.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
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
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <param name="maxTestLength"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonPrefixLength(System.Func<T, bool> predicate, int maxTestLength = int.MaxValue)
        => CommonPrefixLength(source, (e, i) => predicate(e), maxTestLength);

      /// <summary>
      /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and a <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="maxTestLength"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public int CommonPrefixLength(T value, int maxTestLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTestLength);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return CommonPrefixLength(source, (e, i) => equalityComparer.Equals(e, value), maxTestLength);
      }

      /// <summary>
      /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="maxTestLength"></param>
      /// <param name="equalityComparer"></param>
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

      public int CommonPrefixLength(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonPrefixLength(source, target, int.MaxValue, equalityComparer);

      #endregion // CommonPrefixLength

      #region IsCommonPrefixAny

      /// <summary>
      /// <para>Returns whether <paramref name="source"/> starts with any <paramref name="length"/> combination of <paramref name="values"/>. Uses the <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="length"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public bool IsCommonPrefixAny(int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
        => IsCommonPrefix(source, length, c => values.Contains(c, equalityComparer));

      #endregion // IsCommonPrefixAny

      #region IsCommonPrefix

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="length"/> elements that satisfies the <paramref name="predicate"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="length"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public bool IsCommonPrefix(int length, System.Func<T, bool> predicate)
        => CommonPrefixLength(source, predicate, length) == length;

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="length"/> occurences of the <paramref name="value"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="length"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool IsCommonPrefix(int length, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonPrefixLength(source, value, length, equalityComparer) == length;

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="length"/> elements from the <paramref name="target"/> span. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="length"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool IsCommonPrefix(System.ReadOnlySpan<T> target, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => CommonPrefixLength(source, target, length, equalityComparer) == length;

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> starts with the <paramref name="target"/> span. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool IsCommonPrefix(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => IsCommonPrefix(source, target, target.Length, equalityComparer);

      #endregion // IsCommonPrefix

      #region TrimCommonPrefix

      /// <summary>
      /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of matching prefix elements satisfying the <paramref name="predicate"/> removed.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonPrefix(System.Func<T, bool> predicate, int maxTrimLength = int.MaxValue)
        => source[CommonPrefixLength(source, predicate, maxTrimLength)..];

      /// <summary>
      /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of prefix elements matching <paramref name="value"/> removed.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonPrefix(T value, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => source[CommonPrefixLength(source, value, maxTrimLength, equalityComparer)..];

      /// <summary>
      /// <para>Slice the <paramref name="source"/> with the matching prefix elements from <paramref name="value"/> removed.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="maxTrimLength"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> TrimCommonPrefix(System.ReadOnlySpan<T> value, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => source[CommonPrefixLength(source, value, maxTrimLength, equalityComparer)..];

      #endregion // TrimCommonPrefix
    }
  }
}
