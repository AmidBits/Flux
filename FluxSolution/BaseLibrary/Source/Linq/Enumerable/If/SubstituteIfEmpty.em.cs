using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Substitute if the sequence is empty. An exception is thrown if the source sequence or if the substitute sequence is null.</summary>
    /// <exception cref="System.ArgumentNullException">Either source is null or substitute is null.</exception>
    public static System.Collections.Generic.IEnumerable<TSource> SubstituteIfEmpty<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, System.Collections.Generic.IEnumerable<TSource>? substitute)
    {
      using var e = source.ThrowIfNull().GetEnumerator();

      if (e.MoveNext())
      {
        do
          yield return e.Current;
        while (e.MoveNext());
      }
      else
      {
        using var e2 = substitute.ThrowIfNull(nameof(substitute)).GetEnumerator();

        while (e2.MoveNext())
          yield return e2.Current;
      }
    }
  }
}