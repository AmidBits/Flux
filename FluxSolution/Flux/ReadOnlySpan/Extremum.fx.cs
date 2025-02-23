namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Locate the index and value of both the minimum element and the maximum element of the sequence. Uses the specified comparer (null for default).</para>
    /// <see href="https://en.wikipedia.org/wiki/Maximum_and_minimum"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="valueSelector"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static (int MinIndex, TSource? MinItem, TValue? MinValue, int MaxIndex, TSource? MaxItem, TValue? MaxValue) Extremum<TSource, TValue>(this System.ReadOnlySpan<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var minIndex = -1;
      var minItem = default(TSource);
      var minValue = default(TValue);

      var maxIndex = -1;
      var maxItem = default(TSource);
      var maxValue = default(TValue);

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var item = source[index];

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
      }

      return (minIndex, minItem, minValue, maxIndex, maxItem, maxValue);
    }
  }
}
