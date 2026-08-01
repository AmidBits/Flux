namespace Flux
{
  public static class OrderedDictionaryExtensions
  {
    extension<TKey, TValue>(System.Collections.Generic.OrderedDictionary<TKey, TValue> source)
      where TKey : notnull
    {
      public bool TryGetKeyAndValue(int index, out TKey key, out TValue value)
      {
        try
        {
          if (index > -1 && index < source.Count)
          {
            (key, value) = source.GetAt(index);

            return true;
          }
        }
        catch { }

        key = default!;
        value = default!;
        return false;
      }

      public bool TryGetIndexAndValue(TKey key, out int index, out TValue value)
      {
        try
        {
          if (key is not null)
          {
            index = source.IndexOf(key);

            if (index > -1)
            {
              (_, value) = source.GetAt(index);

              return true;
            }
          }
        }
        catch { }

        index = -1;
        value = default!;
        return false;
      }

      public bool TryGetIndexAndKey(TValue value, out int index, out TKey key, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
      {
        try
        {
          index = source.IndexOf(value, out var kvp, equalityComparer);

          if (index > -1)
          {
            key = kvp.Key;

            return true;
          }
        }
        catch { }

        index = -1;
        key = default!;
        return false;
      }

      public int IndexOf(TValue value, out System.Collections.Generic.KeyValuePair<TKey, TValue> keyValuePair, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

        for (var i = 0; i < source.Count; i++)
        {
          keyValuePair = source.GetAt(i);

          if (equalityComparer.Equals(keyValuePair.Value, value))
            return i;
        }

        keyValuePair = default!;
        return -1;
      }
    }
  }
}
