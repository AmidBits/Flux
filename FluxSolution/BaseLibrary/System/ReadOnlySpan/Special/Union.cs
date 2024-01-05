namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Union of all elements, i.e. potential duplicates.</para>
    /// </summary>
    public static System.Collections.Generic.List<T> UnionAll<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> other)
    {
      var list = new System.Collections.Generic.List<T>();
      list.AddSpan(source);
      list.AddSpan(other);
      return list;
    }

    /// <summary>
    /// <para>Union of distinct elements, i.e. no duplicates.</para>
    /// </summary>
    public static System.Collections.Generic.HashSet<T> UnionDistinct<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> other, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      source.ToHashSet(equalityComparer).UnionWith(other.ToHashSet());
      var set = new System.Collections.Generic.HashSet<T>(equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default);
      set.AddSpan(source);
      set.AddSpan(other);
      return set;
    }
  }
}
