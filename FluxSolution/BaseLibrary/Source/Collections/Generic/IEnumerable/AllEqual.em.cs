namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static bool AllEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IEqualityComparer<TValue> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      using var enumerator = source.GetEnumerator();

      if (enumerator.MoveNext() && valueSelector(enumerator.Current) is var firstValue)
      {
        while (enumerator.MoveNext())
        {
          var currentValue = valueSelector(enumerator.Current);

          if (!comparer.Equals(currentValue, firstValue))
            return false;
        }

        return true;
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
    public static bool AllEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector)
      => AllEqual(source, valueSelector, System.Collections.Generic.EqualityComparer<TValue>.Default);
  }
}
