namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Locate the index and value of both the minimum element and the maximum element of the sequence. Uses the specified comparer (null for default).</para>
    /// <see href="https://en.wikipedia.org/wiki/Maximum_and_minimum"/>
    /// </summary>
    public static (int MinimumIndex, TValue MinimumValue, int MaximumIndex, TValue MaximumValue) GetExtremum<TSource, TValue>(this System.ReadOnlySpan<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var minIndex = -1;
      var minValue = default(TValue);
      var maxIndex = -1;
      var maxValue = default(TValue);

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var value = valueSelector(source[index]);

        if (minIndex < 0 || comparer.Compare(value, minValue) < 0)
        {
          minIndex = index;
          minValue = value;
        }

        if (maxIndex < 0 || comparer.Compare(value, maxValue) > 0)
        {
          maxIndex = index;
          maxValue = value;
        }
      }

      return (minIndex, minValue!, maxIndex, maxValue!);
    }
  }
}
