namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the first element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate, T value)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      using var e = source.ThrowIfNull().GetEnumerator();

      if (e.MoveNext())
      {
        var index = 0;

        do
        {
          if (predicate(e.Current, index))
            return e.Current;

          index++;
        }
        while (e.MoveNext());
      }

      return value;
    }
    /// <summary>Returns the first element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate, T value)
      => FirstOrValue(source, (e, i) => predicate(e), value);
    /// <summary>Returns the first element in the sequence, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
      => FirstOrValue(source, (e, i) => true, value);
  }
}
