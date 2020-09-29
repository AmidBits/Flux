using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    public static System.Collections.Generic.IEnumerable<T> EmptyOnNull<T>(this System.Collections.Generic.IEnumerable<T>? source)
      => source ?? System.Linq.Enumerable.Empty<T>();

    /// <summary>Returns the substitute sequence, if the source sequence is either null or empty. An exception is thrown if the substitute sequence is either null or empty.</summary>
    /// <exception cref="substitute">When either null or empty.</exception>
    public static System.Collections.Generic.IEnumerable<T> SubstituteOnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? substitute)
    {
      using var e = EmptyOnNull(source).GetEnumerator();

      if (e.MoveNext())
      {
        do
        {
          yield return e.Current;
        }
        while (e.MoveNext());
      }
      else
      {
        foreach (var item in ThrowOnNullOrEmpty(substitute, nameof(substitute)))
        {
          yield return item;
        }
      }
    }
    /// <summary>Returns the substitute sequence, if the source sequence is either null or empty. If either of the sequences are null, an exception is thrown. However, if the substitutes sequence is empty, no exception is thrown.</summary>
    public static System.Collections.Generic.IEnumerable<T> SubstituteOnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T>? source, params T[] substitute)
      => SubstituteOnNullOrEmpty(source, substitute.AsEnumerable());

    ///// <summary>Throw an exception if the sequence is null.</summary>
    public static System.Collections.Generic.IEnumerable<T> ThrowOnNull<T>(this System.Collections.Generic.IEnumerable<T>? source, string? name = null)
      => source ?? throw new System.ArgumentNullException(name ?? nameof(source));
    ///// <summary>Throw an exception if the sequence is null or if it is empty.</summary>
    public static System.Collections.Generic.IEnumerable<T> ThrowOnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T>? source, string? name = null)
    {
      using var e = ThrowOnNull(source, name).GetEnumerator();

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
  }
}
