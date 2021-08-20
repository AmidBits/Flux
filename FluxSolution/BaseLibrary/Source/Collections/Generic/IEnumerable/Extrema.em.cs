namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Locate both the minimum and the maximum element of the sequence. Uses the specified comparer.</summary>
    public static (TSource elementMin, TSource elementMax) Extrema<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IComparer<TKey> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var elementMin = e.Current;
        var indexMin = -1;
        var keyMin = keySelector(e.Current);

        var elementMax = e.Current;
        var indexMax = -1;
        var keyMax = keySelector(e.Current);

        var index = 0;

        while (e.MoveNext())
        {
          var elementCurrent = e.Current;
          var keyCurrent = keySelector(e.Current);

          if (comparer.Compare(keyCurrent, keyMin) < 0)
          {
            elementMin = elementCurrent;
            indexMin = index;
            keyMin = keyCurrent;
          }

          if (comparer.Compare(keyCurrent, keyMax) > 0)
          {
            elementMax = elementCurrent;
            indexMax = index;
            keyMax = keyCurrent;
          }

          index++;
        }

        return (elementMin, indexMin, elementMax, indexMax);
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
    /// <summary>Locate both the minimum and the maximum element of the sequence. Uses the specified comparer.</summary>
    public static (TSource minimum, TSource maximum) Extrema<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector)
      => Extrema(source, valueSelector, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
