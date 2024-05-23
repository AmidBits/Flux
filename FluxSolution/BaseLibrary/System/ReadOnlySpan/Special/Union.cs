namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Union of all elements, i.e. potential duplicates.</para>
    /// </summary>
    public static System.Collections.Generic.List<T> UnionAll<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var list = new System.Collections.Generic.List<T>();
      list.AddSpan(source);
      list.AddSpan(target);
      return list;
    }

    /// <summary>
    /// <para>Union of distinct elements, i.e. no duplicates.</para>
    /// </summary>
    public static System.Collections.Generic.HashSet<T> UnionDistinct<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      source.ToHashSet(equalityComparer).UnionWith(target.ToHashSet());
      var set = new System.Collections.Generic.HashSet<T>(equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default);
      set.AddSpan(source);
      set.AddSpan(target);
      return set;
    }
  }
}
