namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Locate the nearest (less than/greater than) elements and indices to <paramref name="referenceValue"/>, as evaluated by the <paramref name="valueSelector"/>, in <paramref name="source"/>. Uses the specified <paramref name="comparer"/> (default if null).</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static (int TowardZeroIndex, TSource? TowardZeroItem, TValue? TowardZeroValue, int AwayFromZeroIndex, TSource? AwayFromZeroItem, TValue? AwayFromValue) GetInfimumAndSupremum<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, TValue referenceValue, bool proper, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var tzIndex = -1;
      var tzItem = default(TSource);
      var tzValue = referenceValue;

      var afzIndex = -1;
      var afzItem = default(TSource);
      var afzValue = referenceValue;

      var index = 0;

      foreach (var item in source.ThrowOnNullOrEmpty())
      {
        var value = valueSelector(item);

        var cmp = comparer.Compare(value, referenceValue);

        if ((!proper ? cmp <= 0 : cmp < 0) && (tzIndex < 0 || comparer.Compare(value, tzValue) > 0))
        {
          tzIndex = index;
          tzItem = item;
          tzValue = value;
        }

        if ((!proper ? cmp >= 0 : cmp > 0) && (afzIndex < 0 || comparer.Compare(value, afzValue) < 0))
        {
          afzIndex = index;
          afzItem = item;
          afzValue = value;
        }

        index++;
      }

      return (tzIndex, tzItem, tzValue, afzIndex, afzItem, afzValue);
    }
  }
}
