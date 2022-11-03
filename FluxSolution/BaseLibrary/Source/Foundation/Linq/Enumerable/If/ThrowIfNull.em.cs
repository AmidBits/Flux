namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Throws an exception if the sequence is null. Deferred execution.</summary>
    /// <exception cref="System.ArgumentNullException">The sequence cannot be null.</exception>
    public static System.Collections.Generic.IEnumerable<TSource> ThrowIfNull<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, string? paramName = null)
    {
      if (source is null) throw new System.ArgumentNullException(paramName ?? nameof(source), "The sequence cannot be null.");

      foreach (var item in source)
        yield return item;
    }
  }
}
