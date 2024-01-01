namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Locate the index and value of both the minimum element and the maximum element of the sequence. Uses the specified comparer (null for default).</summary>
    public static (int IndexMin, TValue ValueMin, int IndexMax, TValue ValueMax) GetExtremum<TSource, TValue>(this System.ReadOnlySpan<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var indexMin = -1;
      var valueMin = default(TValue);
      var indexMax = -1;
      var valueMax = default(TValue);

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var value = valueSelector(source[index]);

        if (indexMin < 0 || comparer.Compare(value, valueMin) < 0)
        {
          indexMin = index;
          valueMin = value;
        }

        if (indexMax < 0 || comparer.Compare(value, valueMax) > 0)
        {
          indexMax = index;
          valueMax = value;
        }
      }

      return (indexMin, valueMin!, indexMax, valueMax!);
    }
  }
}
