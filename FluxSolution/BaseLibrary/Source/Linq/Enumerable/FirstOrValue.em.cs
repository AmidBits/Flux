namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the first element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool>? predicate = null)
    {
      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext())
      {
        var index = 0;

        do
        {
          if (predicate?.Invoke(e.Current, index) ?? true)
            return e.Current;

          index++;
        }
        while (e.MoveNext());
      }

      return value;
    }

    /// <summary>Returns the first element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, bool>? predicate = null)
      => FirstOrValue(source, value, (e, i) => predicate?.Invoke(e) ?? true);
  }
}
