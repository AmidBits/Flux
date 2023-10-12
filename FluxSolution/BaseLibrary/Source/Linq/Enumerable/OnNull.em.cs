namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Throws an exception if <paramref name="source"/> is null. Deferred execution.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TSource> ThrowOnNull<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, string? paramName = null)
    {
      foreach (var item in source ?? throw new System.ArgumentNullException(paramName ?? nameof(source), "The sequence cannot be null."))
        yield return item;  // Must yield for deferred execution.
    }

    /// <summary>If <paramref name="source"/> is null, a new sequence of all elements in <paramref name="values"/>, otherwise the elements in <paramref name="source"/>. Deferred execution.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TSource> ValuesOnNull<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, System.Collections.Generic.IEnumerable<TSource>? values)
      => (source ?? values).ThrowOnNull(nameof(values));

    /// <summary>If <paramref name="source"/> is null, a new sequence of all elements in <paramref name="values"/>, otherwise the elements in <paramref name="source"/>. Deferred execution.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TSource> ValuesOnNull<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, params TSource[] values)
      => source.ValuesOnNull(values.AsEnumerable());
  }
}
