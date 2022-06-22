namespace Flux
{
  public static partial class ReadOnlySpanEm
  {
    /// <summary>Locate the index of the minimum element and the index of the maximum element of the sequence. Uses the specified comparer.</summary>
    public static (int indexMinimum, int indexMaximum) GetExtremum<TSource, TValue>(ref this SpanBuilder<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var indexMin = -1;
      var indexMax = -1;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var key = valueSelector(source[index]);

        if (indexMin < 0 || comparer.Compare(key, valueSelector(source[indexMin])) < 0)
          indexMin = index;
        if (indexMax < 0 || comparer.Compare(key, valueSelector(source[indexMax])) > 0)
          indexMax = index;
      }

      return (indexMin, indexMax);
    }
    /// <summary>Locate the index of the minimum element and the index of the maximum element of the sequence. Uses the default comparer.</summary>
    public static (int indexMinimum, int indexMaximum) GetExtremum<TSource, TValue>(ref this SpanBuilder<TSource> source, System.Func<TSource, TValue> valueSelector)
      => GetExtremum(ref source, valueSelector, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
