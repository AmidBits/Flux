namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Throws an exception if either the sequence is null or if it is empty. Deferred execution.</summary>
    /// <exception cref="System.ArgumentNullException">The sequence cannot be null.</exception>
    /// <exception cref="System.ArgumentException">The sequence cannot be empty.</exception>
    public static System.Collections.Generic.IEnumerable<T> ThrowIfEmpty<T>(this System.Collections.Generic.IEnumerable<T>? source, string? paramName = null)
    {
      using var e = source.ThrowIfNull(paramName).GetEnumerator();

      if (e.MoveNext())
      {
        do
        {
          yield return e.Current;
        }
        while (e.MoveNext());
      }
      else throw new System.ArgumentException("The sequence cannot be empty.", paramName ?? nameof(source));
    }
  }
}
