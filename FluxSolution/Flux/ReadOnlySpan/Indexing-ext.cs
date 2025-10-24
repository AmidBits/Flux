namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension(System.ReadOnlySpan<char> source)
    {
      #region IndexOfAny

      /// <summary>
      /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>, or -1 if none were found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public int IndexOfAny(System.Collections.Generic.IEqualityComparer<char> equalityComparer, params string[] values)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
          if (IndexOf(source, values[valueIndex], equalityComparer) is var index && index > -1)
            return index;

        return -1;
      }

      #endregion

      #region LastIndexOfAny

      /// <summary>
      /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>. or -1 if none is found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public int LastIndexOfAny(System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, params string[] values)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
          if (LastIndexOf(source, values[valueIndex], equalityComparer) is var index && index > -1)
            return index;

        return -1;
      }

      #endregion
    }

    extension<T>(System.ReadOnlySpan<T> source)
    {
      #region CreateIndexMap

      /// <summary>
      /// <para>Creates a new dictionary with all keys (by <paramref name="keySelector"/>) and indices of all occurences in the <paramref name="source"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <typeparam name="TKey"></typeparam>
      /// <param name="source"></param>
      /// <param name="keySelector"></param>
      /// <param name="equalityComparer"></param>
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

      #region IndexOfAny

      /// <summary>
      /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>, or -1 if none were found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public int IndexOfAny(System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return IndexOf(source, (e, i) => values.Contains(e, equalityComparer));
      }

      #endregion

      #region IndexOf

      /// <summary>
      /// <para>Reports the first index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or -1 if not found.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
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
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public int IndexOf(System.Func<T, bool> predicate)
        => IndexOf(source, (e, i) => predicate(e));

      /// <summary>
      /// <para>Reports the first index of the specified <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int IndexOf(T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return IndexOf(source, (e, i) => equalityComparer.Equals(e, value));
      }

      /// <summary>
      /// <para>Returns the first index of the specified <paramref name="target"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="equalityComparer"></param>
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

      #endregion

      #region LastIndexOfAny

      /// <summary>
      /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>. or -1 if none is found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      public int LastIndexOfAny(System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return LastIndexOf(source, (e, i) => values.Contains(e, equalityComparer));
      }

      #endregion

      #region LastIndexOf

      /// <summary>
      /// <para>Reports the last index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or -1 if not found.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
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
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public int LastIndexOf(System.Func<T, bool> predicate)
        => LastIndexOf(source, (e, i) => predicate(e));

      /// <summary>
      /// <para>Returns the last index of <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int LastIndexOf(T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return LastIndexOf(source, (e, i) => equalityComparer.Equals(e, value));
      }

      /// <summary>
      /// <para>Returns the last index of <paramref name="target"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int LastIndexOf(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        for (var index = source.Length - target.Length; index >= 0; index--)
          if (source[index..].IsCommonPrefix(target, equalityComparer))
            return index;

        return -1;
      }

      #endregion
    }
  }
}
