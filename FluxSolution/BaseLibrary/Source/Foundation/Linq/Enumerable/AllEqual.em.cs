namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Return whether all elements evaluate equal to each other. Uses the specified (default if null) <paramref name="equalityComparer"/>.</summary>
    /// <exception cref="System.ArgumentNullException">The <paramref name="source"/> cannot be empty.</exception>
    public static bool AllEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
    {
      //if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

      using var enumerator = source.ThrowIfNullOrEmpty().GetEnumerator();

      if (enumerator.MoveNext())
      {
        var first = valueSelector(enumerator.Current);

        while (enumerator.MoveNext())
          if (!equalityComparer.Equals(valueSelector(enumerator.Current), first))
            return false; // Found an unequal element, all is lost, report false.
      }

      return true; // If here, it means all elements were considered equal.
    }
  }
}
