namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the index of the maximum value, as well as the actual value as an output parameter. If no value was found, -1 is returned and the maxValue is set to its default value. Uses the specified comparer.</summary>
    public static int IndexOfMax<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, out TValue maxValue, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (source is null) throw new System.ArgumentOutOfRangeException(nameof(source));
      else if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var maxIndex = -1;
      maxValue = default!;

      var index = -1;

      foreach (var value in System.Linq.Enumerable.Select(source, valueSelector))
        if (index++ == -1 || comparer.Compare(value, maxValue) > 0)
        {
          maxIndex = index;
          maxValue = value;
        }

      return maxIndex;
    }
    /// <summary>Returns the index of the maximum value, as well as the actual value as an output parameter. If no value was found, -1 is returned and the maxValue is set to its default value. Uses the default comparer.</summary>
    public static int IndexOfMax<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, out TValue maxValue)
      => IndexOfMax(source, valueSelector, out maxValue, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
