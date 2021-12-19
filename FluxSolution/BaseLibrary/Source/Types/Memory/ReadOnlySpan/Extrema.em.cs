namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Locate the index of the minimum element and the index of the maximum element of the sequence. Uses the specified comparer.</summary>
    public static (int indexMin, int indexMax) Extrema<TSource, TKey>(this System.ReadOnlySpan<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IComparer<TKey> comparer)
    {
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var indexMin = -1;
      var indexMax = -1;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var key = keySelector(source[index]);

        if (indexMin < 0 || comparer.Compare(key, keySelector(source[indexMin])) < 0)
          indexMin = index;
        if (indexMax < 0 || comparer.Compare(key, keySelector(source[indexMax])) > 0)
          indexMax = index;
      }

      return (indexMin, indexMax);
    }
    /// <summary>Locate the index of the minimum element and the index of the maximum element of the sequence. Uses the default comparer.</summary>
    public static (int indexMin, int indexMax) Extrema<TSource, TKey>(this System.ReadOnlySpan<TSource> source, System.Func<TSource, TKey> valueSelector)
      => Extrema(source, valueSelector, System.Collections.Generic.Comparer<TKey>.Default);
  }
}
