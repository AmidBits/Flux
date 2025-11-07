//namespace Flux
//{
//  public static partial class IEnumerables
//  {
//    /// <summary>Throws an exception if <paramref name="source"/> is null or the sequence empty. Deferred execution.</summary>
//    /// <exception cref="System.ArgumentNullException">The sequence cannot be null.</exception>
//    /// <exception cref="System.ArgumentException">The sequence cannot be empty.</exception>
//    public static System.Collections.Generic.IEnumerable<TSource> ThrowOnNullOrEmpty<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, string? paramName = null)
//    {
//      using var e = source.ThrowOnNull(paramName).GetEnumerator();

//      if (e.MoveNext())
//      {
//        do
//          yield return e.Current;
//        while (e.MoveNext());

//        yield break;
//      }

//      throw new System.ArgumentException("The sequence cannot be empty.", paramName ?? nameof(source));
//    }
//  }
//}
