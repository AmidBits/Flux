namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Locate the minimum/maximum elements and indices, as evaluated by the <paramref name="valueSelector"/>, in <paramref name="source"/>. Uses the specified (default if null) <paramref name="comparer"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static (TSource minItem, int minIndex, TSource maxItem, int maxIndex) GetExtremum<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext())
      {
        var value = valueSelector(e.Current);

        var minIndex = 0;
        var minItem = e.Current;
        var minValue = value;

        var maxIndex = 0;
        var maxItem = e.Current;
        var maxValue = value;

        for (var index = 1; e.MoveNext(); index++)
        {
          value = valueSelector(e.Current);

          if (comparer.Compare(value, minValue) < 0)
          {
            minIndex = index;
            minItem = e.Current;
            minValue = value;
          }

          if (comparer.Compare(value, maxValue) > 0)
          {
            maxIndex = index;
            maxItem = e.Current;
            maxValue = value;
          }
        }

        return (minItem, minIndex, maxItem, maxIndex);
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }

    public static (int minIndex, TSource? minItem, int maxIndex, TSource? maxItem) GetExtremum2<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var index = 0;

      var minIndex = -1;
      TSource? minItem = default;
      TValue? minValue = default;

      var maxIndex = -1;
      TSource? maxItem = default;
      TValue? maxValue = default;

      foreach (var item in source.ThrowOnNullOrEmpty())
      {
        var value = valueSelector(item);

        if (index == 0)
        {
          minIndex = 0;
          minItem = item;
          minValue = value;

          maxIndex = 0;
          maxItem = item;
          maxValue = value;
        }
        else
        {
          if (comparer.Compare(value, minValue) < 0)
          {
            minIndex = index;
            minItem = item;
            minValue = value;
          }

          if (comparer.Compare(value, maxValue) > 0)
          {
            maxIndex = index;
            maxItem = item;
            maxValue = value;
          }
        }

        index++;
      }

      return (minIndex, minItem, maxIndex, maxItem);
    }
  }
}
