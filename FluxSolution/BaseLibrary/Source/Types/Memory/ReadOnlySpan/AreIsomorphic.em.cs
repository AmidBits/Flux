namespace Flux
{
  public static partial class ReadOnlySpanEm
  {
    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b. Uses the specified equality comparer.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static bool AreIsomorphic<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      where T : notnull
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      if (source.Length != target.Length)
        return false;

      var map1 = new System.Collections.Generic.Dictionary<T, T>(equalityComparer);
      var map2 = new System.Collections.Generic.Dictionary<T, T>(equalityComparer);

      for (var i = source.Length - 1; i >= 0; i--)
      {
        var c1 = source[i];
        var c2 = target[i];

        if (map1.ContainsKey(c1))
        {
          if (!equalityComparer.Equals(c2, map1[c1]))
            return false;
        }
        else
        {
          if (map2.ContainsKey(c2))
            return false;

          map1[c1] = c2;
          map2[c2] = c1;
        }
      }

      return true;
    }
    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b. Uses the default equality comparer.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static bool AreIsomorphic<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      where T : notnull
      => AreIsomorphic(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b. Uses the specified equality comparer.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static bool AreIsomorphic(this string source, string target, System.Collections.Generic.IEqualityComparer<char> equalityComparer)
      => AreIsomorphic((System.ReadOnlySpan<char>)source, (System.ReadOnlySpan<char>)target, equalityComparer);
    /// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b. Uses the default equality comparer.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static bool AreIsomorphic(this string source, string target)
      => AreIsomorphic((System.ReadOnlySpan<char>)source, (System.ReadOnlySpan<char>)target, System.Collections.Generic.EqualityComparer<char>.Default);
  }
}
