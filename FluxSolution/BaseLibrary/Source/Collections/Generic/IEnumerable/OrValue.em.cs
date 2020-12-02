namespace Flux
{
  public static partial class IEnumerableEm
  {
    /// <summary>Returns the first element in the sequence that satisfies the predicate, or the specified value if no such element is found.</summary>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate, T value)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var index = 0;

        do
        {
          if (predicate(e.Current, index++))
            return e.Current;
        }
        while (e.MoveNext());
      }

      return value;
    }
    /// <summary>Returns the first element in the sequence that satisfies the predicate, or the specified value if no such element is found.</summary>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate, T value)
      => FirstOrValue(source, (e, i) => predicate(e), value);
    /// <summary>Returns the first element in the sequence, or the specified value if no elements are found.</summary>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
      => FirstOrValue(source, (e, i) => true, value);

    /// <summary>Returns the last element in the sequence that satisfies the predicate, or the specified value if no such element is found.</summary>
    public static T LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate, T value)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var index = 0;

        do
        {
          if (predicate(e.Current, index++))
            value = e.Current;
        }
        while (e.MoveNext());
      }

      return value;
    }
    /// <summary>Returns the last element in the sequence that satisfies the predicate, or the specified value if no such element is found.</summary>
    public static T LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate, T value)
      => LastOrValue(source, (e, i) => predicate(e), value);
    /// <summary>Returns the last element in the sequence, or the specified value if no elements are found.</summary>
    public static T LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
      => LastOrValue(source, (e, i) => true, value);
  }
}
