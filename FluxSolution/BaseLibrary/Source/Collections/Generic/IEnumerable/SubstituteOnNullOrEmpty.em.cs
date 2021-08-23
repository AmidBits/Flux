using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the substitute sequence, if the source sequence is either null or empty. An exception is thrown if the substitute sequence is either null or empty.</summary>
    /// <exception cref="substitute">When either null or empty.</exception>
    public static System.Collections.Generic.IEnumerable<T> SubstituteOnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? substitute)
    {
      using var e = (source ?? System.Linq.Enumerable.Empty<T>()).GetEnumerator();

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
          yield return item;
      }
    }
    /// <summary>Returns the substitute sequence, if the source sequence is either null or empty. If either of the sequences are null, an exception is thrown. However, if the substitutes sequence is empty, no exception is thrown.</summary>
    public static System.Collections.Generic.IEnumerable<T> SubstituteOnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T>? source, params T[] substitute)
      => SubstituteOnNullOrEmpty(source, substitute.AsEnumerable());
  }
}
