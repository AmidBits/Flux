namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Given two sequences a and b, determine if they are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static bool IsIsomorphic<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      where T : System.IEquatable<T>
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      if (source.Length != target.Length) return false;

      var map1 = new System.Collections.Generic.Dictionary<T, T>(comparer);
      var map2 = new System.Collections.Generic.Dictionary<T, T>(comparer);

      for (var i = source.Length - 1; i >= 0; i--)
      {
        var c1 = source[i];
        var c2 = target[i];

        if (map1.ContainsKey(c1))
        {
          if (!comparer.Equals(c2, map1[c1])) return false;
        }
        else
        {
          if (map2.ContainsKey(c2)) return false;

          map1[c1] = c2;
          map2[c2] = c1;
        }
      }

      return true;
    }
    /// <summary>Given two sequences a and b, determine if they are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static bool IsIsomorphic<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      where T : System.IEquatable<T>
    => IsIsomorphic(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
