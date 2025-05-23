namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="additionalCapacity"></param>
    /// <returns></returns>
    public static System.Collections.Generic.HashSet<T> Except<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source == target)
        return new(equalityComparer); // A set minus itself is an empty set.

      var except = source.ToHashSet(equalityComparer);

      if (target.Length > 0)
        except.RemoveSpan(target); // If target is empty, source is the result.

      return except;
    }
  }
}
