using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Invokes a transform on each element of a generic sequence and returns the index of the maximum value. Uses the specified comparer.</summary>
    public static int IndexOfMax<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> selector, out TValue maxValue, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (source is null) throw new System.ArgumentOutOfRangeException(nameof(source));
      else if (selector is null) throw new System.ArgumentNullException(nameof(selector));

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var maxIndex = -1;
      maxValue = default(TValue)!;

      var index = -1;
      foreach (var value in source.Select(selector))
      {
        if (index++ == -1 || comparer.Compare(value, maxValue) > 0)
        {
          maxIndex = index;
          maxValue = value;
        }
      }

      return maxIndex;
    }
    /// <summary>Invokes a transform on each element of a generic sequence and returns the index of the maximum value. Uses the default comparer.</summary>
    public static int IndexOfMax<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> selector, out TValue maxValue)
      => IndexOfMax(source, selector, out maxValue, System.Collections.Generic.Comparer<TValue>.Default);

    /// <summary>Invokes a transform on each element of a generic sequence and returns the index of the minimum value. Uses the specified comparer.</summary>
    public static int IndexOfMin<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> selector, out TValue maxValue, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (source is null) throw new System.ArgumentOutOfRangeException(nameof(source));
      else if (selector is null) throw new System.ArgumentNullException(nameof(selector));

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var maxIndex = -1;
      maxValue = default(TValue)!;

      var index = -1;
      foreach (var value in source.Select(selector))
      {
        if (index++ == -1 || comparer.Compare(value, maxValue) < 0)
        {
          maxIndex = index;
          maxValue = value;
        }
      }

      return maxIndex;
    }
    /// <summary>Invokes a transform on each element of a generic sequence and returns the index of the minimum value. Uses the default comparer.</summary>
    public static int IndexOfMin<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> selector, out TValue maxValue)
      => IndexOfMin(source, selector, out maxValue, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
