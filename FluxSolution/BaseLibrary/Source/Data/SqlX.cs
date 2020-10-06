namespace Flux.Data
{
  public static class SqlX
  {
    public enum ColumnMappingEnum
    {
      IndexToIndex,
      IndexToName,
      NameToIndex,
      NameToName,
    }

    public static System.Collections.Generic.Dictionary<TSource, TTarget> CreateColumnMappings<TSource, TTarget>(System.Collections.Generic.IDictionary<int, string> source, System.Collections.Generic.IDictionary<int, string> target, ColumnMappingEnum mappingType)
      where TSource : notnull
    {
      var dictionary = new System.Collections.Generic.Dictionary<TSource, TTarget>();

      var sourceEnumerator = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));
      var targetEnumerator = target?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));

      while (sourceEnumerator.MoveNext() && targetEnumerator.MoveNext())
      {
        //var (sourceColumn, targetColumn) = selector(sourceEnumerator.Current, targetEnumerator.Current);

        //dictionary.Add(sourceColumn, targetColumn);
      }

      return dictionary;
    }

    public static System.Collections.Generic.Dictionary<TSource, TTarget> CreateColumnMappings<TSource, TTarget>(System.Collections.Generic.IDictionary<int, string> source, System.Collections.Generic.IDictionary<int, string> target, System.Func<System.Collections.Generic.KeyValuePair<int, string>, System.Collections.Generic.KeyValuePair<int, string>, (TSource sourceColumn, TTarget targetColumn)> selector)
      where TSource : notnull
    {
      if (selector is null) throw new System.ArgumentNullException(nameof(selector));

      var dictionary = new System.Collections.Generic.Dictionary<TSource, TTarget>();

      var sourceEnumerator = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));
      var targetEnumerator = target?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));

      while (sourceEnumerator.MoveNext() && targetEnumerator.MoveNext())
      {
        var (sourceColumn, targetColumn) = selector(sourceEnumerator.Current, targetEnumerator.Current);

        dictionary.Add(sourceColumn, targetColumn);
      }

      return dictionary;
    }
  }
}
