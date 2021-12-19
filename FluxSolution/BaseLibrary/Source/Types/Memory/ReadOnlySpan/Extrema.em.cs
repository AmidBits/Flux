namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Locate the index of the minimum element and the index of the maximum element of the sequence. Uses the specified comparer.</summary>
    public static (int indexMinimum, int indexMaximum) Extrema<TValue>(this System.ReadOnlySpan<TValue> source, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var indexMin = -1;
      var indexMax = -1;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var key = source[index];

        if (indexMin < 0 || comparer.Compare(key, source[indexMin]) < 0)
          indexMin = index;
        if (indexMax < 0 || comparer.Compare(key, source[indexMax]) > 0)
          indexMax = index;
      }

      return (indexMin, indexMax);
    }
    /// <summary>Locate the index of the minimum element and the index of the maximum element of the sequence. Uses the default comparer.</summary>
    public static (int indexMinimum, int indexMaximum) Extrema<TValue>(this System.ReadOnlySpan<TValue> source)
      => Extrema(source, System.Collections.Generic.Comparer<TValue>.Default);

    /// <summary>Locate the index of the minimum element and the index of the maximum element of the sequence. Uses the specified comparer.</summary>
    public static (int indexMinimum, int indexMaximum) Extrema<TSource, TValue>(this System.ReadOnlySpan<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var indexMin = -1;
      var indexMax = -1;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var key = valueSelector(source[index]);

        if (indexMin < 0 || comparer.Compare(key, valueSelector(source[indexMin])) < 0)
          indexMin = index;
        if (indexMax < 0 || comparer.Compare(key, valueSelector(source[indexMax])) > 0)
          indexMax = index;
      }

      return (indexMin, indexMax);
    }
    /// <summary>Locate the index of the minimum element and the index of the maximum element of the sequence. Uses the default comparer.</summary>
    public static (int indexMinimum, int indexMaximum) Extrema<TSource, TValue>(this System.ReadOnlySpan<TSource> source, System.Func<TSource, TValue> valueSelector)
      => Extrema(source, valueSelector, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
