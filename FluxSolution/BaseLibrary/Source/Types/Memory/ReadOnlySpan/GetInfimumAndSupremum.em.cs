namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Locate the index of the max element that is less than and the index of the min element that is greater than, the specified reference value identified by the <paramref name="valueSelector"/>. Uses the specified comparer (null for default).</summary>
    public static (int indexLessThan, int indexGreaterThan) GetInfimumAndSupremum<TSource, TValue>(this System.ReadOnlySpan<TSource> source, TValue referenceValue, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var indexLt = -1;
      var indexGt = -1;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var value = valueSelector(source[index]);

        var cmp = comparer.Compare(value, referenceValue);

        if (cmp < 0 && (indexLt < 0 || comparer.Compare(value, valueSelector(source[indexLt])) > 0))
          indexLt = index;
        if (cmp > 0 && (indexGt < 0 || comparer.Compare(value, valueSelector(source[indexGt])) < 0))
          indexGt = index;
      }

      return (indexLt, indexGt);
    }
  }
}
