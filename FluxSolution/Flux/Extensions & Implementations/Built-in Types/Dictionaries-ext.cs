namespace Flux
{
  public static partial class DictionaryExtensions
  {
    extension<TKey, TValue>(System.Collections.Generic.IDictionary<TKey, TValue> source)
    {
      /// <summary>
      /// <para>Determines whether the dictionary contains the specified key and its associated value, using an optional equality comparer for value comparison.</para>
      /// </summary>
      /// <param name="key">The key to locate in the dictionary.</param>
      /// <param name="value">The value to compare with the value associated with the specified key.</param>
      /// <param name="equalityComparer">An optional equality comparer to use for comparing values. If null, the default equality comparer for the value type is used.</param>
      /// <returns>True if the dictionary contains an entry with the specified key and value; otherwise, false.</returns>
      public bool ContainsKeyAndValue(TKey key, TValue value, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

        return source.TryGetValue(key, out var foundValue) && equalityComparer.Equals(foundValue, value);
      }
    }

    extension<TKey, TValue>(System.Collections.Generic.IDictionary<TKey, TValue> source)
      where TKey : notnull
    {
      /// <summary>
      /// <para>Modifies the current <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/> so that it contains the keys from both dictionaries. If values of equal keys are present between the two dictionaries, the current are kept.</para>
      /// </summary>
      /// <param name="other"></param>
      public void MergeKeepWith(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> other)
      {
        foreach (var (key, value) in other)
          if (!source.ContainsKey(key))
            source[key] = value;
      }

      /// <summary>
      /// <para>Modifies the current <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/> so that it contains the keys from both dictionaries. If values of equal keys are present between the two dictionaries, the other overwrites those in the current.</para>
      /// </summary>
      /// <typeparam name="TKey"></typeparam>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="source"></param>
      /// <param name="other"></param>
      public void MergeOverwriteWith(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> other)
      {
        foreach (var (key, value) in other)
          source[key] = value;
      }

      /// <summary>
      /// <para>Creates a new 2-dimensional array from the <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/>.</para>
      /// </summary>
      /// <param name="pivot">Whether the 2-dimensional array should pivot the dictionary keys and values.</param>
      /// <returns></returns>
      public object?[,] ToRank2Array(bool pivot)
      {
        var array = pivot ? new object?[2, source.Count] : new object?[source.Count, 2];

        var index = 0;

        foreach (var (k, v) in source)
        {
          if (pivot)
            (array[0, index], array[1, index]) = (k, v);
          else
            (array[index, 0], array[index, 1]) = (k, v);

          index++;
        }

        return array;
      }
    }

    extension(System.Collections.IDictionary source)
    {
      /// <summary>
      /// <para>Determines whether the collection contains the specified key and its associated value.</para>
      /// </summary>
      /// <param name="key">The key to locate in the collection. Cannot be null.</param>
      /// <param name="value">The value to compare with the value associated with the specified key.</param>
      /// <returns>true if the collection contains an entry with the specified key and value; otherwise, false.</returns>
      public bool ContainsKeyAndValue(object key, object value)
        => source.Contains(key) && source[key] == value;

      /// <summary>
      /// <para>Projects the elements of the source collection into a sequence of key/value pairs using the specified key and value selector functions.</para>
      /// </summary>
      /// <typeparam name="TKey">The type of the keys returned by the key selector function.</typeparam>
      /// <typeparam name="TValue">The type of the values returned by the value selector function.</typeparam>
      /// <param name="keySelector">A function to extract the key from each element in the source collection.</param>
      /// <param name="valueSelector">A function to extract the value from each element in the source collection.</param>
      /// <returns>An enumerable collection of key/value pairs where each pair is created by applying the key and value selector functions to each element of the source collection.</returns>
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> ToKeyValuePairs<TKey, TValue>(System.Func<object, TKey> keySelector, System.Func<object, TValue> valueSelector)
      {
        System.ArgumentNullException.ThrowIfNull(keySelector);
        System.ArgumentNullException.ThrowIfNull(valueSelector);

        return source.Cast<System.Collections.DictionaryEntry>().Select(de => new System.Collections.Generic.KeyValuePair<TKey, TValue>(keySelector(de), valueSelector(de)));
      }
    }

    extension<TKey, TValue>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source)
    {
      /// <summary>
      /// <para>Converts a sequence of <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> into a single composite string.</para>
      /// </summary>
      /// <param name="options"></param>
      /// <param name="pivot"></param>
      /// <returns></returns>
      public string ToConsoleString(ConsoleFormatOptions? options = null, bool pivot = false)
        => System.Array.JaggedArrayToConsoleString(ToJaggedArray(source, pivot), options ?? ConsoleFormatOptions.Default with { HorizontalSeparator = "=" });

      /// <summary>
      /// <para>Creates a new jagged array from the <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/>.</para>
      /// </summary>
      /// <param name="pivot">Whether the jagged array should pivot the dictionary keys and values.</param>
      /// <returns></returns>
      public object?[][] ToJaggedArray(bool pivot)
      {
        object?[][] array = pivot ? new object?[2][] : [];

        foreach (var (k, v) in source)
        {
          if (pivot)
          {
            var a0 = System.Buffers.ArrayPool<object?>.Shared.Rent((array[0]?.Length ?? 0) + 1);
            var a1 = System.Buffers.ArrayPool<object?>.Shared.Rent((array[1]?.Length ?? 0) + 1);

            if (a0.Length > 1)
            {
              System.Array.Copy(array[0], a0, array[0].Length);
              System.Buffers.ArrayPool<object?>.Shared.Return(array[0]);
            }
            if (a1.Length > 0)
            {
              System.Array.Copy(array[1], a1, array[1].Length);
              System.Buffers.ArrayPool<object?>.Shared.Return(array[1]);
            }

            a0[^1] = k;
            a1[^1] = v;

            (array[0], array[1]) = (a0, a1);
          }
          else // A dictionary straight to jagged array without pivoting can be built by simply appending the key-value pairs as arrays to the jagged array.
          {
            System.Array.Resize(ref array, array.Length + 1);

            array[^1] = [k, v];
          }
        }

        return array;
      }

      /// <summary>
      /// <para>Tries to get the keys associated with the specified value. Returns true if at least one key is found, false otherwise.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="keys"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool TryGetKeys(TValue value, out System.Collections.Generic.List<TKey> keys, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

        keys = [];

        foreach (var (k, v) in source)
          if (equalityComparer.Equals(v, value))
            keys.Add(k);

        return keys.Count > 0;
      }
    }

    extension<TKey, TValue>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source)
      where TKey : notnull
    {
      /// <summary>
      /// <para>Creates a new <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/> so that it contains the keys from both dictionaries. If values of equal keys are present between the two dictionaries, the current are used.</para>
      /// </summary>
      /// <param name="other"></param>
      public System.Collections.Generic.IDictionary<TKey, TValue> MergeKeep(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> other)
      {
        var dictionary = source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        dictionary.MergeKeepWith(other);
        return dictionary;
      }

      /// <summary>
      /// <para>Creates a new <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/> so that it contains the keys from both dictionaries. If values of equal keys are present between the two dictionaries, the other are used.</para>
      /// </summary>
      /// <param name="other"></param>
      public System.Collections.Generic.IDictionary<TKey, TValue> MergeOverwrite(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> other)
      {
        var dictionary = source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        dictionary.MergeOverwriteWith(other);
        return dictionary;
      }
    }

    #region Switch functionality

    public static System.Collections.Generic.IDictionary<TValue, System.Collections.Generic.IList<TKey>> Switch<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.IList<TValue>> source, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
      where TKey : notnull
      where TValue : notnull
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

      var switched = new DataStructures.OrderedDictionary<TValue, System.Collections.Generic.IList<TKey>>(equalityComparer);

      foreach (var kvp in source)
        foreach (var value in kvp.Value)
        {
          if (!switched.TryGetValue(value, out var ilist))
            ilist = new System.Collections.Generic.List<TKey>();
          ilist.Add(kvp.Key);
          switched[value] = ilist;
        }

      return switched;
    }

    public static System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.IList<TValue>> ToSwitchable<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> source)
      where TKey : notnull
    {
      var switchable = new DataStructures.OrderedDictionary<TKey, System.Collections.Generic.IList<TValue>>();

      foreach (var kvp in source)
      {
        if (kvp.Value is System.Collections.Generic.IList<TValue> list)
          switchable.Add(kvp.Key, list);
        else
          switchable.Add(kvp.Key, [kvp.Value]);
      }

      return switchable;
    }

    public static System.Collections.Generic.IDictionary<TKey, TValue> ToUnswitchable<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.IList<TValue>> source)
      where TKey : notnull
    {
      var unswitchable = new DataStructures.OrderedDictionary<TKey, TValue>();

      foreach (var kvp in source)
        unswitchable.Add(kvp.Key, kvp.Value.Single());

      return unswitchable;
    }

    #endregion
  }
}
