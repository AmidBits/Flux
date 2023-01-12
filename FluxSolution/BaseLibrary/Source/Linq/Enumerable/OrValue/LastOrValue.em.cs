namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the last element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static T LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate, T value)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      using var e = source.ThrowIfNull().GetEnumerator();

      if (e.MoveNext())
      {
        var index = 0;

        do
        {
          if (e.Current is var current && predicate(current, index))
            value = current;

          index++;
        }
        while (e.MoveNext());
      }

      return value;
    }
    /// <summary>Returns the last element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static T LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate, T value)
      => LastOrValue(source, (e, i) => predicate(e), value);
    /// <summary>Returns the last element in the sequence, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static T LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
      => LastOrValue(source, (e, i) => true, value);
  }
}
