namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the specified <paramref name="equalityComparer"/> (or default if null).</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="additionalCapacity"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.HashSet<T> SymmetricExcept<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source == target)
        return new(equalityComparer); // A symmetric difference of a set with itself is an empty set.

      if (source.Length == 0)
        return target.ToHashSet(equalityComparer); // If source is empty, target is the result.

      if (target.Length == 0)
        return source.ToHashSet(equalityComparer); // If target is empty, source is the result.

      var symmetricExcept = source.Except(target, equalityComparer);
      symmetricExcept.UnionWith(target.Except(source, equalityComparer));
      return symmetricExcept;
    }
  }
}
