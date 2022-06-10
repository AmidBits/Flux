namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Locate both the minimum and the maximum element of the sequence. Uses the specified comparer.</summary>
    public static (TSource elementMin, int indexMin, TSource elementMax, int indexMax) Extrema<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var index = 0;
        var valueCurrent = valueSelector(e.Current);

        var elementMin = e.Current;
        var indexMin = index;
        var valueMin = valueCurrent;

        var elementMax = e.Current;
        var indexMax = index;
        var valueMax = valueCurrent;

        while (e.MoveNext())
        {
          index++;
          valueCurrent = valueSelector(e.Current);

          if (comparer.Compare(valueCurrent, valueMin) < 0)
          {
            elementMin = e.Current;
            indexMin = index;
            valueMin = valueCurrent;
          }
          if (comparer.Compare(valueCurrent, valueMax) > 0)
          {
            elementMax = e.Current;
            indexMax = index;
            valueMax = valueCurrent;
          }
        }

        return (elementMin, indexMin, elementMax, indexMax);
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
    /// <summary>Locate both the minimum and the maximum element of the sequence. Uses the default comparer.</summary>
    public static (TSource elementMin, int indexMin, TSource elementMax, int indexMax) Extrema<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector)
      => Extrema(source, valueSelector, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
