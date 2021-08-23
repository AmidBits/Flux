namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Locate both the minimum and the maximum element of the sequence. Uses the specified comparer.</summary>
    public static (TSource elementMin, int indexMin, TSource elementMax, int indexMax) Extrema<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IComparer<TKey> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var index = 0;

        var keyCurrent = keySelector(e.Current);

        var elementMin = e.Current;
        var indexMin = index;
        var keyMin = keyCurrent;

        var elementMax = e.Current;
        var indexMax = index;
        var keyMax = keyCurrent;

        while (e.MoveNext())
        {
          index++;

          keyCurrent = keySelector(e.Current);

          if (comparer.Compare(keyCurrent, keyMin) < 0)
          {
            elementMin = e.Current;
            indexMin = index;
            keyMin = keyCurrent;
          }

          if (comparer.Compare(keyCurrent, keyMax) > 0)
          {
            elementMax = e.Current;
            indexMax = index;
            keyMax = keyCurrent;
          }
        }

        return (elementMin, indexMin, elementMax, indexMax);
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
    /// <summary>Locate both the minimum and the maximum element of the sequence. Uses the specified comparer.</summary>
    public static (TSource elementMin, int indexMin, TSource elementMax, int indexMax) Extrema<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector)
      => Extrema(source, valueSelector, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
