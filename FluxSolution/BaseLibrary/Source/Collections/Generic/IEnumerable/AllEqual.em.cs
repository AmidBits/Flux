namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static bool AllEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IEqualityComparer<TValue> equalityComparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      using var enumerator = source.GetEnumerator();

      if (enumerator.MoveNext() && valueSelector(enumerator.Current) is var firstValue)
      {
        while (enumerator.MoveNext())
          if (!equalityComparer.Equals(valueSelector(enumerator.Current), firstValue))
            return false;

        return true;
      }

      return false;
    }
    public static bool AllEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector)
      => AllEqual(source, valueSelector, System.Collections.Generic.EqualityComparer<TValue>.Default);
  }
}
