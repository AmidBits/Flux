using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Substitute if the sequence is empty. An exception is thrown if the source sequence or if the substitute sequence is null.</summary>
    /// <exception cref="System.ArgumentNullException">Either source is null or substitute is null.</exception>
    public static System.Collections.Generic.IEnumerable<T> SubstituteIfEmpty<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? substitute)
    {
      using var e = source.ThrowIfNull().GetEnumerator();

      if (e.MoveNext())
        do yield return e.Current;
        while (e.MoveNext());
      else
        foreach (var item in substitute.ThrowIfNull(nameof(substitute)))
          yield return item;
    }
  }
}
