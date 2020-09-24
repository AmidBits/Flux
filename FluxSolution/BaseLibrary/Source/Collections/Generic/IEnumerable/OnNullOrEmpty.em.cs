using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    public static System.Collections.Generic.IEnumerable<T> EmptyOnNull<T>(this System.Collections.Generic.IEnumerable<T>? source)
      => source ?? System.Linq.Enumerable.Empty<T>();

    /// <summary>Returns the substitute sequence, if the source sequence is empty. If either of the sequences are null, an exception is thrown.</summary>
    public static System.Collections.Generic.IEnumerable<T> SubstituteOnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? substitutes)
    {
      using var e = source.ThrowOnNull().GetEnumerator();

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
        foreach (var item in substitutes.ThrowOnNull(nameof(substitutes)))
        {
          yield return item;
        }
      }
    }
    /// <summary>Returns the substitute sequence, if the source sequence is empty. If either of the sequences are null, an exception is thrown.</summary>
    public static System.Collections.Generic.IEnumerable<T> SubstituteOnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T>? source, params T[] substitutes)
      => source.SubstituteOnNullOrEmpty(substitutes.AsEnumerable());

    ///// <summary>Returns the substitute sequence, if the source sequence is null. If the substitute sequence is null, an exception is thrown.</summary>
    public static System.Collections.Generic.IEnumerable<T> SubstituteOnNull<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? substitutes)
      => source ?? substitutes.ThrowOnNull(nameof(substitutes));
    ///// <summary>Returns the substitute sequence, if the source sequence is null. If the substitute sequence is null, an exception is thrown.</summary>
    public static System.Collections.Generic.IEnumerable<T> SubstituteOnNull<T>(this System.Collections.Generic.IEnumerable<T>? source, params T[] substitutes)
      => source.SubstituteOnNull(substitutes.AsEnumerable());

    ///// <summary>Throw an exception if the sequence is null.</summary>
    public static System.Collections.Generic.IEnumerable<T> ThrowOnNull<T>(this System.Collections.Generic.IEnumerable<T>? source, string? name = null)
      => source ?? throw new System.ArgumentNullException(name ?? nameof(source));
    ///// <summary>Throw an exception if the sequence is null or if it is empty.</summary>
    public static System.Collections.Generic.IEnumerable<T> ThrowOnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T>? source, string? name = null)
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
  }
}
