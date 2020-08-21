using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Invokes a transform on each element of a generic sequence and returns the index of the maximum value. Uses the specified comparer.</summary>
    public static int IndexOfMax<T, TResult>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, TResult> selector, System.Collections.Generic.IComparer<TResult> comparer)
    {
      if (source is null) throw new System.ArgumentOutOfRangeException(nameof(source));
      else if (selector is null) throw new System.ArgumentNullException(nameof(selector));

      comparer ??= System.Collections.Generic.Comparer<TResult>.Default;

      var maxIndex = -1;
      var maxValue = default(TResult)!;

      var index = -1;
      foreach (var value in source.Select(v => selector(v)))
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
    public static int IndexOfMax<T, TResult>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, TResult> selector)
      => IndexOfMax(source, selector, System.Collections.Generic.Comparer<TResult>.Default);

    /// <summary>Invokes a transform on each element of a generic sequence and returns the index of the minimum value. Uses the specified comparer.</summary>
    public static int IndexOfMin<T, TResult>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, TResult> selector, System.Collections.Generic.IComparer<TResult> comparer)
    {
      if (source is null) throw new System.ArgumentOutOfRangeException(nameof(source));
      else if (selector is null) throw new System.ArgumentNullException(nameof(selector));

      comparer ??= System.Collections.Generic.Comparer<TResult>.Default;

      var maxIndex = -1;
      var maxValue = default(TResult)!;

      var index = -1;
      foreach (var value in source.Select(v => selector(v)))
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
    public static int IndexOfMin<T, TResult>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, TResult> selector)
      => IndexOfMin(source, selector, System.Collections.Generic.Comparer<TResult>.Default);
  }
}
