//namespace Flux
//{
//  public static partial class IEnumerables
//  {
//    /// <summary>Throws an exception if <paramref name="source"/> is null. Deferred execution.</summary>
//    /// <exception cref="System.ArgumentNullException"/>
//    public static System.Collections.Generic.IEnumerable<TSource> ThrowOnNull<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, string? paramName = null)
//    {
//      foreach (var item in source ?? throw new System.ArgumentNullException(paramName ?? nameof(source), "The sequence cannot be null."))
//        yield return item;  // Must yield for deferred execution.
//    }
//  }
//}
