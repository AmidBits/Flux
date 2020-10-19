namespace Flux.Data
{
  public static class SqlX
  {
    public static System.Data.Common.DataColumnMappingCollection CreateColumnMappings(System.Collections.Generic.IList<string> sourceNames, System.Collections.Generic.IList<string> targetNames)
    {
      if (sourceNames is null) throw new System.ArgumentNullException(nameof(sourceNames));
      if (targetNames is null) throw new System.ArgumentNullException(nameof(targetNames));

      var dcmc = new System.Data.Common.DataColumnMappingCollection();
      for (var index = 0; index < sourceNames.Count && index < targetNames.Count; index++)
        dcmc.Add(sourceNames[index], targetNames[index]);
      return dcmc;
    }

    //public static System.Collections.Generic.IEnumerable<(TResultLeft, TResultRight)> CreateColumnMappings<TResultLeft, TResultRight>(System.Collections.Generic.IDictionary<int, string> source, System.Collections.Generic.IDictionary<int, string> target, System.Func<System.Collections.Generic.KeyValuePair<int, string>, System.Collections.Generic.KeyValuePair<int, string>, (TResultLeft, TResultRight)> selector)
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));
    //  if (target is null) throw new System.ArgumentNullException(nameof(target));
    //  if (selector is null) throw new System.ArgumentNullException(nameof(selector));

    //  using var sourceEnumerator = source.GetEnumerator();
    //  using var targetEnumerator = target.GetEnumerator();

    //  while (sourceEnumerator.MoveNext() && targetEnumerator.MoveNext())
    //  {
    //    yield return selector(sourceEnumerator.Current, targetEnumerator.Current);
    //  }
    //}
  }
}
