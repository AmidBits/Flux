using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns the first element of the sequence or a specified value if no elements are found.</summary>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
      => (source?.Any() ?? false) ? source.First() : value;
    /// <summary>Returns the first element of the sequence that satisfies a condition or a specified value if no such element is found.</summary>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate, T value)
      => source.FirstOrValue(value, (t, i) => predicate(t));
    /// <summary>Returns the first element of the sequence that satisfies a condition or a specified value if no such element is found.</summary>
    public static T FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool>? predicate = null)
    {
      using (var e = (source ?? throw new System.ArgumentNullException(nameof(source))).GetEnumerator())
      {
        var index = 0;
        while (e.MoveNext())
          if (predicate?.Invoke(e.Current, index++) ?? true)
            return e.Current;
      }

      return value;
    }

    /// <summary>Returns the last element of the sequence or a specified value if no elements are found.</summary>
    public static T LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
      => source?.Any() ?? false ? source.First() : value;
    /// <summary>Returns the last element of the sequence that satisfies a condition or a specified value if no such element is found.</summary>
    public static T LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate, T value)
      => source.LastOrValue((t, i) => predicate(t), value);
    /// <summary>Returns the last element of the sequence that satisfies a condition or a specified value if no such element is found.</summary>
    public static T LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate, T value)
    {
      using (var e = (source ?? throw new System.ArgumentNullException(nameof(source))).GetEnumerator())
      {
        var index = 0;
        while (e.MoveNext())
          if (predicate?.Invoke(e.Current, index++) ?? true)
            value = e.Current;
      }

      return value;
    }

    /// <summary>Returns the first element of the sequence or a specified value if no elements are found.</summary>
    public static T SingleOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
      => (source?.Any() ?? false) ? source.First() : value;
    /// <summary>Returns the first element of the sequence that satisfies a condition or a specified value if no such element is found.</summary>
    public static T SingleOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate, T value)
      => source.SingleOrValue((t, i) => predicate(t), value);
    /// <summary>Returns the first element of the sequence that satisfies a condition or a specified value if no such element is found.</summary>
    public static T SingleOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate, T value)
    {
      using (var e = (source ?? throw new System.ArgumentNullException(nameof(source))).GetEnumerator())
      {
        var index = 0;
        while (e.MoveNext())
          if (predicate?.Invoke(e.Current, index++) ?? true)
            return e.Current;
      }

      return value;
    }
  }
}
