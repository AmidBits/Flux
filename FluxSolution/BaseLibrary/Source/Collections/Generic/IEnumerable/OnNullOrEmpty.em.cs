using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>If the sequence is null or empty, replace it with an alternate sequence, otherwise continue normally.</summary>
    public static System.Collections.Generic.IEnumerable<T> OnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> alternate)
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
        foreach (var item in alternate ?? throw new System.ArgumentNullException(nameof(alternate)))
        {
          yield return item;
        }
      }
    }
    /// <summary>If the sequence is null or empty, replace it with an alternate sequence, otherwise continue normally.</summary>
    public static System.Collections.Generic.IEnumerable<T> OnNullOrEmpty<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] alternate)
      => source.OnNullOrEmpty(alternate.AsEnumerable());
  }
}
