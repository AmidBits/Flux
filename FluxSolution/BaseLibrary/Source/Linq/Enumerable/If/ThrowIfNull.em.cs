namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Throws an exception if the sequence is null. Deferred execution.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TSource> ThrowIfNull<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, string? paramName = null)
    {
      foreach (var item in source ?? throw new System.ArgumentNullException(paramName ?? nameof(source), "The sequence cannot be null."))
        yield return item;
    }
  }
}
