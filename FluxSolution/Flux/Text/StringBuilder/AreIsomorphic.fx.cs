namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> and <paramref name="target"/> are isomorphic. Two sequences are isomorphic if the characters (equal characters must be replaced with the same replacements, in the same positions) in <paramref name="source"/> can be replaced to get <paramref name="target"/>.</para>
    /// </summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static bool AreIsomorphic(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      if (source.Length != target.Length) return false;

      var map1 = new System.Collections.Generic.Dictionary<char, char>(equalityComparer);
      var map2 = new System.Collections.Generic.Dictionary<char, char>(equalityComparer);

      for (var i = source.Length - 1; i >= 0; i--)
      {
        var c1 = source[i];
        var c2 = target[i];

        if (map1.TryGetValue(c1, out char value))
        {
          if (!equalityComparer.Equals(c2, value)) return false;
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
  }
}
