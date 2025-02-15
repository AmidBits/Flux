namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Union of all elements, i.e. potential duplicates.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="additionalCapacity"></param>
    /// <returns></returns>
    /// <remarks>Uses <see cref="System.Runtime.InteropServices.CollectionsMarshal.SetCount{T}(List{T}, int)"/> and <see cref="System.Runtime.InteropServices.CollectionsMarshal.AsSpan{T}(List{T}?)"/> to copy <paramref name="source"/> and <paramref name="target"/> into a resulting <see cref="System.Collections.Generic.List{T}"/>.</remarks>
    public static System.Collections.Generic.List<T> UnionAll<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var unionAll = source.ToList(target.Length);
      unionAll.AddRange(target);
      return unionAll;
    }

    /// <summary>
    /// <para>Union of distinct elements, i.e. no duplicates. Uses the specified <paramref name="equalityComparer"/> (or default if null).</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="additionalCapacity"></param>
    /// <returns></returns>
    public static System.Collections.Generic.HashSet<T> Union<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      var union = source.ToHashSet(equalityComparer, target.Length);
      union.AddSpan(target);
      return union;
    }
  }
}
