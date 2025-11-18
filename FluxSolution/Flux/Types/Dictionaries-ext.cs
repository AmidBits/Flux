namespace Flux
{
  public static partial class DictionaryExtensions
  {
    public static bool ContainsKeyValuePair<TKey, TValue>(this System.Collections.Generic.Dictionary<TKey, TValue> source, TKey key, TValue value, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
      where TKey : notnull
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

      return source.TryGetValue(key, out var foundValue) && equalityComparer.Equals(foundValue, value);
    }



    public static object[][] ToJaggedArray(this System.Collections.Generic.IEnumerable<System.Collections.DictionaryEntry> source, bool expandValueCollectionsToArrays = false)
    {
      var dim0 = new System.Collections.Generic.List<object[]>();

      var dim1 = new System.Collections.Generic.List<object>();

      foreach (var de in source)
      {
        dim1.Clear();

        dim1.Add(de.Key);

        if (expandValueCollectionsToArrays && de.Value is System.Collections.IDictionary dv)
          dim1.Add(dv);
        else if (expandValueCollectionsToArrays && de.Value is System.Collections.IEnumerable ev)
          dim1.AddRange();
        else
          dim1.Add(de.Value!);

        dim0.Add(dim1.ToArray());
      }

      return dim0.ToArray();
    }

    public static object[] ConvertToArray(this System.Collections.DictionaryEntry source, bool expandValueToArray = false)
    {
      var list = new System.Collections.Generic.List<object> { source.Key };

      if (expandValueToArray && source.Value is System.Collections.IDictionary dv)
        list.Add(ToJaggedArray(dv.Cast<System.Collections.DictionaryEntry>(), expandValueToArray));
      else if (expandValueToArray && source.Value is System.Collections.IEnumerable ev)
        list.AddRange(ev);
      else
        list.Add(source.Value!);

      return list.ToArray();
    }

    extension(System.Collections.Generic.IEnumerable<System.Collections.DictionaryEntry> source)
    {
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> EnumerateKeyValuePair<TKey, TValue>(System.Globalization.CultureInfo? culture = null)
        where TKey : notnull
      {
        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        foreach (var de in source)
          yield return new KeyValuePair<TKey, TValue>((TKey)de.Key.TypeConverter(culture, [typeof(TKey)]), (TValue)de.Value!.TypeConverter(culture, [typeof(TValue)]));
      }

      public object[][] ToJaggedArray()
        => source.Select(kvp => new object[] { kvp.Key, kvp.Value! }).ToArray();

      //public string ToConsoleString(System.Func<object, string> keySelector, System.Func<object, string> valueSelector, ConsoleFormatOptions? options = null)
      //  => ToJaggedArray(source).JaggedToConsoleString(options ?? ConsoleFormatOptions.Default with { HorizontalSeparator = "=" });
    }

    extension(System.Collections.IDictionary source)
    {
      public System.Collections.Generic.IEnumerable<System.Collections.DictionaryEntry> EnumerateDictionaryEntry()
        => source.Cast<System.Collections.DictionaryEntry>();

      public object[][] ToJaggedArray(bool pivot)
      {
        if (pivot)
        {
          var array0 = new object[source.Count];
          var array1 = new object[source.Count];

          var e = source.GetEnumerator();

          for (var i = 0; i < source.Count && e.MoveNext(); i++)
            (array0[i], array1[i]) = (e.Key, e.Value!);

          return [array0, array1];
        }
        else
        {
          var array = new object[source.Count][];

          var e = source.GetEnumerator();

          for (var i = 0; i < source.Count && e.MoveNext(); i++)
            array[i] = [e.Key, e.Value!];

          return array;
        }
      }

      public object[,] ToRank2Array()
      {
        var array = new object[source.Count, 2];

        var e = source.GetEnumerator();

        for (var i = 0; i < source.Count && e.MoveNext(); i++)
          (array[i, 0], array[i, 1]) = (e.Key, e.Value!);

        return array;
      }
    }

    extension<TKey, TValue>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source)
      where TKey : notnull
    {
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TValue, TKey>> FlipPairs()
        => source.GroupBy(kvp => kvp.Value).SelectMany(g => g.Select(gi => System.Collections.Generic.KeyValuePair.Create(gi.Value, gi.Key)));

      public object[][] ToJaggedArray()
        => source.Select(kvp => new object[] { kvp.Key, kvp.Value! }).ToArray();

      /// <summary>Converts a sequence of <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> into a single composite string.</summary>
      public string ToConsoleString(System.Func<TKey, string> keySelector, System.Func<TValue, string> valueSelector, ConsoleFormatOptions? options = null)
        => ToJaggedArray(source).JaggedToConsoleString(options ?? ConsoleFormatOptions.Default with { HorizontalSeparator = "=" });
    }

    extension<TKey, TValue>(System.Collections.Generic.IDictionary<TKey, TValue> source)
      where TKey : notnull
    {
      /// <summary>
      /// <para>Merge-overwrite <paramref name="source"/> with elements from the <paramref name="other"/> <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/> or enumerable of <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/>.</para>
      /// <para>Essentially a merge of the elements in <paramref name="other"/> into <paramref name="source"/>. If keys are equal, <paramref name="other"/> values will overwrite the values in <paramref name="source"/>.</para>
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

      public object[][] ToJaggedArray(bool pivot)
      {
        if (pivot)
        {
          var array = new object[2][];

          array[0] = [source.Keys];
          array[1] = [source.Values];

          return array;
        }
        else
        {
          var array = new object[source.Count][];

          using var e = source.GetEnumerator();

          for (var i = 0; i < source.Count && e.MoveNext(); i++)
            array[i] = [e.Current.Key, e.Current.Value!];

          return array;
        }
      }

      public object[,] ToRank2Array()
      {
        var array = new object[source.Count, 2];

        using var e = source.GetEnumerator();

        for (var i = 0; i < source.Count && e.MoveNext(); i++)
          (array[i, 0], array[i, 1]) = (e.Current.Key, e.Current.Value!);

        return array;
      }

      public System.Collections.Generic.IEnumerable<TKey> TryGetKeys(TValue value, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

        return source.Where(kvp => equalityComparer.Equals(kvp.Value, value)).Select(kvp => kvp.Key);
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

    #endregion // Swich functionality
  }
}
