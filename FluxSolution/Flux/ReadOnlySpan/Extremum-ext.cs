namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      #region Extremum

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
      public (int MinIndex, T? MinItem, TValue? MinValue, int MaxIndex, T? MaxItem, TValue? MaxValue) Extremum<TValue>(System.Func<T, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(valueSelector);

        comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

        var minIndex = -1;
        var minItem = default(T);
        var minValue = default(TValue);

        var maxIndex = -1;
        var maxItem = default(T);
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

      #endregion
    }
  }
}
