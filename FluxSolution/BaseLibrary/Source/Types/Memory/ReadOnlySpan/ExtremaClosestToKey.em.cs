namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Locate the index of the max element that is less than and the index of the min element that is greater than, the specified reference value identified by the <paramref name="valueSelector"/>. Uses the specified comparer.</summary>
    public static (int indexLtKey, int indexGtKey) ExtremaClosestTo<TSource, TValue>(this System.ReadOnlySpan<TSource> source, TValue referenceValue, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var indexLtValue = -1;
      var indexGtValue = -1;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var value = valueSelector(source[index]);

        var cmp = comparer.Compare(value, referenceValue);

        if (cmp < 0 && (indexLtValue < 0 || comparer.Compare(value, valueSelector(source[indexLtValue])) > 0))
          indexLtValue = index;
        if (cmp > 0 && (indexGtValue < 0 || comparer.Compare(value, valueSelector(source[indexGtValue])) < 0))
          indexGtValue = index;
      }

      return (indexLtValue, indexGtValue);
    }
    /// <summary>Locate the index of the max element that is less than and the index of the min element that is greater than, the specified reference value identified by the <paramref name="valueSelector"/>. Uses the default comparer.</summary>
    public static (int indexLtKey, int indexGtKey) ExtremaClosestTo<TSource, TValue>(this System.ReadOnlySpan<TSource> source, TValue referenceValue, System.Func<TSource, TValue> valueSelector)
      => ExtremaClosestTo(source, referenceValue, valueSelector, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
