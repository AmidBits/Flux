namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Locate the index and value of both the max element that is less-than and the min element that is greater-than, the specified reference value identified by the <paramref name="valueSelector"/>. Uses the specified comparer (null for default).</summary>
    /// <remarks>By definition of infimum and supremum, the function is supposed to return both the less-than-or-equal and greater-than-or-equal, but this version skips the -or-equal.</remarks>
    public static (int IndexLte, TValue ValueLte, int IndexGte, TValue ValueGte) GetInfimumAndSupremum<TSource, TValue>(this System.ReadOnlySpan<TSource> source, TValue referenceValue, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var indexLte = -1;
      var valueLte = default(TValue);
      var indexGte = -1;
      var valueGte = default(TValue);

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var value = valueSelector(source[index]);

        var cmp = comparer.Compare(value, referenceValue);

        if (cmp < 0 && (indexLte < 0 || comparer.Compare(value, valueLte) > 0))
        {
          indexLte = index;
          valueLte = value;
        }

        if (cmp > 0 && (indexGte < 0 || comparer.Compare(value, valueGte) < 0))
        {
          indexGte = index;
          valueGte = value;
        }
      }

      return (indexLte, valueLte!, indexGte, valueGte!);
    }
  }
}
