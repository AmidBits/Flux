namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Yields an empty sequence when <paramref name="source"/> is null.</summary>
    public static System.Collections.Generic.IEnumerable<TSource> EmptyIfNull<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source)
      => source ?? System.Linq.Enumerable.Empty<TSource>();
  }
}
