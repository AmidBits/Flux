namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Compute both the minimum and maximum element in a single pass, and return them as a 2-tuple. Uses the specified comparer.</summary>
    public static (TSource minimum, TSource maximum) Extrema<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var loSource = e.Current;
        var hiSource = e.Current;

        var loValue = valueSelector(e.Current);
        var hiValue = valueSelector(e.Current);

        while (e.MoveNext())
        {
          var cSource = e.Current;
          var cValue = valueSelector(e.Current);

          if (comparer.Compare(cValue, loValue) < 0)
          {
            loSource = cSource;
            loValue = cValue;
          }

          if (comparer.Compare(cValue, hiValue) > 0)
          {
            hiSource = cSource;
            hiValue = cValue;
          }
        }

        return (loSource, hiSource);
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
    /// <summary>Compute both the minimum and maximum element in a single pass, and return them as a 2-tuple. Uses the default comparer.</summary>
    public static (TSource minimum, TSource maximum) Extrema<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector)
      => Extrema(source, valueSelector, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
