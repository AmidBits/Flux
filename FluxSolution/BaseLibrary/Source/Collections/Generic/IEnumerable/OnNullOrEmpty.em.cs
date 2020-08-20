using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>If the sequence is null or empty, replace it with an alternate sequence, otherwise continue normally.</summary>
    public static System.Collections.Generic.IEnumerable<T> OnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> replacement)
    {
      using var se = (source ?? System.Linq.Enumerable.Empty<T>()).GetEnumerator();

      if (se.MoveNext())
      {
        do
        {
          yield return se.Current;
        }
        while (se.MoveNext());
      }
      else
      {
        foreach (var item in replacement ?? throw new System.ArgumentNullException(nameof(replacement)))
        {
          yield return item;
        }
      }
    }
    /// <summary>If the sequence is null or empty, replace it with an alternate sequence, otherwise continue normally.</summary>
    public static System.Collections.Generic.IEnumerable<T> OnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] replacement)
      => source.OnNullOrEmpty(replacement);

    public static System.Collections.Generic.IEnumerable<T> EmptyOnNull<T>(this System.Collections.Generic.IEnumerable<T> source)
      => source ?? System.Linq.Enumerable.Empty<T>();

    //public static System.Collections.Generic.IEnumerable<T> SubstituteOnEmpty<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> substitution)
    //  =>

    public static System.Collections.Generic.IEnumerable<T> ThrowOnNull<T>(this System.Collections.Generic.IEnumerable<T> source, string? name = null)
      => source ?? throw new System.ArgumentNullException(name ?? nameof(source));
    public static System.Collections.Generic.IEnumerable<T> ThrowOnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T> source, string? name = null)
    {
      using var e = source.ThrowOnNull(name).GetEnumerator();

      if (e.MoveNext())
      {
        do
        {
          yield return e.Current;
        }
        while (e.MoveNext());
      }
      else throw new System.ArgumentException(@"The sequence is empty.", name ?? nameof(source));
    }

    /// <summary>If the sequence is null or empty, replace it with an alternate sequence, otherwise continue normally.</summary>
    public static System.Collections.Generic.IEnumerable<T> WhenNull<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> replacement)
    {
      using var se = (source ?? System.Linq.Enumerable.Empty<T>()).GetEnumerator();

      if (se.MoveNext())
      {
        do
        {
          yield return se.Current;
        }
        while (se.MoveNext());
      }
      else
      {
        foreach (var item in replacement ?? throw new System.ArgumentNullException(nameof(replacement)))
        {
          yield return item;
        }
      }
    }
    /// <summary>If the sequence is null or empty, replace it with an alternate sequence, otherwise continue normally.</summary>
    public static System.Collections.Generic.IEnumerable<T> WhenNull<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] replacement)
      => source.WhenNull(replacement);

  }
}
