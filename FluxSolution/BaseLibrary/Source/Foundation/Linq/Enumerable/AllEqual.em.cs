namespace Flux
{
  public static partial class Enumerable
  {
    public static bool AllEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

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
  }
}
