namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
    /// <summary>Throws an exception if <paramref name="source"/> is null or the sequence empty. Deferred execution.</summary>
    /// <exception cref="System.ArgumentNullException">The sequence cannot be null.</exception>
    /// <exception cref="System.ArgumentException">The sequence cannot be empty.</exception>
    public static System.Collections.Generic.IEnumerable<TSource> ThrowOnNullOrEmpty<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, string? paramName = null)
    {
      using var e = source.ThrowOnNull(paramName).GetEnumerator();

      if (e.MoveNext())
      {
        do
          yield return e.Current;
        while (e.MoveNext());

        yield break;
      }

      throw new System.ArgumentException("The sequence cannot be empty.", paramName ?? nameof(source));
    }

    /// <summary>If <paramref name="source"/> is null or the sequence is empty, a new sequence of all elements in <paramref name="values"/>, otherwise the elements in <paramref name="source"/>. Deferred execution.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.ArgumentException"/>
    public static System.Collections.Generic.IEnumerable<TSource> ValuesOnNullOrEmpty<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, System.Collections.Generic.IEnumerable<TSource>? values)
    {
      if (source is not null)
      {
        using var e1 = source.GetEnumerator();

        if (e1.MoveNext())
        {
          do
            yield return e1.Current;
          while (e1.MoveNext());

          yield break;
        }
      }

      using var e2 = values.ThrowOnNullOrEmpty(nameof(values)).GetEnumerator();

      while (e2.MoveNext())
        yield return e2.Current;
    }

    /// <summary>If <paramref name="source"/> is null or the sequence is empty, a new sequence of all elements in <paramref name="values"/>, otherwise the elements in <paramref name="source"/>. Deferred execution.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.ArgumentException"/>
    public static System.Collections.Generic.IEnumerable<TSource> ValuesOnNullOrEmpty<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, params TSource[] values)
      => source.ValuesOnNullOrEmpty(values.AsEnumerable());
  }
}
