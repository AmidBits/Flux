namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
    /// <summary>Locate the nearest (less than/greater than) elements and indices to <paramref name="targetValue"/>, as evaluated by the <paramref name="valueSelector"/>, in <paramref name="source"/>. Uses the specified (default if null) <paramref name="comparer"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static (int ltIndex, TSource? ltItem, int gtIndex, TSource? gtItem) GetInfimumAndSupremum<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, TValue targetValue, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var index = 0;

      var ltIndex = -1;
      TSource? ltItem = default;
      TValue? ltValue = targetValue;

      var gtIndex = -1;
      TSource? gtItem = default;
      TValue? gtValue = targetValue;

      foreach (var item in source.ThrowOnNullOrEmpty())
      {
        var value = valueSelector(item);

        var cmp = comparer.Compare(value, targetValue);

        if (cmp < 0 && (ltIndex < 0 || comparer.Compare(value, ltValue) > 0))
        {
          ltIndex = index;
          ltItem = item;
          ltValue = value;
        }

        if (cmp > 0 && (gtIndex < 0 || comparer.Compare(value, gtValue) < 0))
        {
          gtIndex = index;
          gtItem = item;
          gtValue = value;
        }

        index++;
      }

      return (ltIndex, ltItem, gtIndex, gtItem);
    }
  }
}
