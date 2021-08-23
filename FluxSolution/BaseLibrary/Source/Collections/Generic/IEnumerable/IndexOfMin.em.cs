using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the index of the minimum value, as well as the actual value as an output parameter. If no value was found, -1 is returned and the minValue is set to its default value. Uses the specified comparer.</summary>
    public static int IndexOfMin<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, out TValue minValue, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (source is null) throw new System.ArgumentOutOfRangeException(nameof(source));
      else if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var indexOfMinimumValue = -1;
      minValue = default!;

      var index = -1;
      foreach (var value in source.Select(valueSelector))
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
    public static int IndexOfMin<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, out TValue minValue)
      => IndexOfMin(source, valueSelector, out minValue, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
