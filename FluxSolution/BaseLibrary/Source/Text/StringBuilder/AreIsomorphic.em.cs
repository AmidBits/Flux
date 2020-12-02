namespace Flux
{
  public static partial class StringBuilderEm
  {
    /// <summary>Given two sequences a and b, determine if they are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static bool AreIsomorphic(this System.Text.StringBuilder source, string target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      if (source.Length != target.Length) return false;

      var map1 = new System.Collections.Generic.Dictionary<char, char>(comparer);
      var map2 = new System.Collections.Generic.Dictionary<char, char>(comparer);

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
    public static bool AreIsomorphic(this System.Text.StringBuilder source, string target)
      => AreIsomorphic(source, target, System.Collections.Generic.EqualityComparer<char>.Default);
  }
}
