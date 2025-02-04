namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Locate the minimum/maximum elements and indices, as evaluated by the <paramref name="valueSelector"/>, in <paramref name="source"/>. Uses the specified <paramref name="comparer"/> (default if null).</para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="valueSelector"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.ArgumentException"></exception>
    public static (int MinIndex, TSource? MinItem, TValue? MinValue, int MaxIndex, TSource? MaxItem, TValue? MaxValue) GetExtremum<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var minIndex = -1;
      var minItem = default(TSource);
      var minValue = default(TValue);

      var maxIndex = -1;
      var maxItem = default(TSource);
      var maxValue = default(TValue);

      var index = 0;

      foreach (var item in source)
      {
        var value = valueSelector(item);

        if (minIndex < 0 || comparer.Compare(value, minValue) < 0)
        {
          minIndex = index;
          minItem = item;
          minValue = value;
        }

        if (maxIndex < 0 || comparer.Compare(value, maxValue) > 0)
        {
          maxIndex = index;
          maxItem = item;
          maxValue = value;
        }

        index++;
      }

      return (minIndex, minItem, minValue, maxIndex, maxItem, maxValue);
    }
  }
}
