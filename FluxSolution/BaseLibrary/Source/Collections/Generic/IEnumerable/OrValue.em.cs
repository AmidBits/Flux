namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns the first element in the sequence that satisfies the predicate, or the specified value if no such element is found.</summary>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate, T value)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext())
      {
        var index = 0;

        do
        {
          if (predicate?.Invoke(e.Current, index++) ?? true)
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
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      using var e = (source ?? throw new System.ArgumentNullException(nameof(source))).GetEnumerator();

      if (e.MoveNext())
      {
        var index = 0;

        do
        {
          if (predicate?.Invoke(e.Current, index++) ?? true)
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
