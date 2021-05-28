using System.Linq;

namespace Flux
{
  public static partial class SystemCollectionsGenericIEnumerableEm
  {
    /// <summary>Returns the index of the maximum value, as well as the actual value as an output parameter. If no value was found, -1 is returned and the maxValue is set to its default value. Uses the specified comparer.</summary>
    public static int IndexOfMax<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> selector, out TValue maxValue, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (source is null) throw new System.ArgumentOutOfRangeException(nameof(source));
      else if (selector is null) throw new System.ArgumentNullException(nameof(selector));

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var indexOfMaximumValue = -1;
      maxValue = default!;

      var index = -1;
      foreach (var value in source.Select(selector))
      {
        if (index++ == -1 || comparer.Compare(value, maxValue) > 0)
        {
          indexOfMaximumValue = index;
          maxValue = value;
        }
      }

      return indexOfMaximumValue;
    }
    /// <summary>Returns the index of the maximum value, as well as the actual value as an output parameter. If no value was found, -1 is returned and the maxValue is set to its default value. Uses the default comparer.</summary>
    public static int IndexOfMax<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> selector, out TValue maxValue)
      => IndexOfMax(source, selector, out maxValue, System.Collections.Generic.Comparer<TValue>.Default);

    /// <summary>Returns the index of the minimum value, as well as the actual value as an output parameter. If no value was found, -1 is returned and the minValue is set to its default value. Uses the specified comparer.</summary>
    public static int IndexOfMin<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> selector, out TValue minValue, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (source is null) throw new System.ArgumentOutOfRangeException(nameof(source));
      else if (selector is null) throw new System.ArgumentNullException(nameof(selector));

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var indexOfMinimumValue = -1;
      minValue = default!;

      var index = -1;
      foreach (var value in source.Select(selector))
      {
        if (index++ == -1 || comparer.Compare(value, minValue) < 0)
        {
          indexOfMinimumValue = index;
          minValue = value;
        }
      }

      return indexOfMinimumValue;
    }
    /// <summary>Returns the index of the minimum value, as well as the actual value as an output parameter. If no value was found, -1 is returned and the minValue is set to its default value. Uses the default comparer.</summary>
    public static int IndexOfMin<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> selector, out TValue minValue)
      => IndexOfMin(source, selector, out minValue, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
