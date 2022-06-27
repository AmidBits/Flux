//namespace Flux
//{
//  public ref partial struct SpanBuilder<T>
//  {
//    /// <summary>Creates a new dictionary with all indices of all occurences in the source. Uses the specified equality comparer.</summary>
//    public System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.HashSet<int>> CreateIndexMap<TKey>(System.Func<T, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey> equalityComparer)
//      where TKey : notnull
//    {
//      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
//      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

//      var map = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.HashSet<int>>(equalityComparer);

//      for (var index = 0; index < m_bufferPosition; index++)
//      {
//        var key = keySelector(m_buffer[index]);

//        if (!map.ContainsKey(key))
//          map[key] = new System.Collections.Generic.HashSet<int>();

//        map[key].Add(index);
//      }

//      return map;
//    }
//    /// <summary>Creates a new dictionary with all indices of all target occurences in the source. Uses the default equality comparer.</summary>
//    public System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.HashSet<int>> CreateIndexMap<TKey>(System.Func<T, TKey> keySelector)
//      where TKey : notnull
//      => CreateIndexMap(keySelector, System.Collections.Generic.EqualityComparer<TKey>.Default);
//  }
//}
