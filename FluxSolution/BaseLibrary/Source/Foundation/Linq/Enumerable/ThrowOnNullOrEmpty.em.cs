using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Throw an exception if the sequence is null or if it is empty.</summary>
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
      else throw new System.ArgumentException("The sequence is empty.", name ?? nameof(source));
    }
  }
}
