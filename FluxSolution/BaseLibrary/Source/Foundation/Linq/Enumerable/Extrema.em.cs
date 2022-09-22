namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Locate both the minimum and the maximum element of the sequence. Uses the specified comparer.</summary>
    public static (TSource minItem, int minIndex, TSource maxItem, int maxIndex) Extrema<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var value = valueSelector(e.Current);

        var minItem = e.Current;
        var minIndex = 0;
        var minValue = value;

        var maxItem = e.Current;
        var maxIndex = 0;
        var maxValue = value;

        var index = 1;

        while (e.MoveNext())
        {
          value = valueSelector(e.Current);

          if (comparer.Compare(value, minValue) < 0)
          {
            minItem = e.Current;
            minIndex = index;
            minValue = value;
          }
          if (comparer.Compare(value, maxValue) > 0)
          {
            maxItem = e.Current;
            maxIndex = index;
            maxValue = value;
          }

          index++;
        }

        return (minItem, minIndex, maxItem, maxIndex);
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
  }
}
