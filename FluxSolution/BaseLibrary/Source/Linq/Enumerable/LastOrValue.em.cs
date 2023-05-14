using System.Data.Common;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the last element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static T LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool>? predicate = null)
    {
      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext())
      {
        var index = 0;

        do
        {
          if (e.Current is var current && (predicate?.Invoke(current, index) ?? true))
            value = current;

          index++;
        }
        while (e.MoveNext());
      }

      return value;
    }

    /// <summary>Returns the last element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static T LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, bool>? predicate = null)
      => LastOrValue(source, value, (e, i) => predicate?.Invoke(e) ?? true);
  }
}
