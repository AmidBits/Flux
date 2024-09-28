namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> and <paramref name="target"/> are isomorphic. Two sequences are isomorphic if the elements (equal elements must be replaced with the same replacements, in the same positions) in <paramref name="source"/> can be replaced to get the same in <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static bool AreIsomorphic<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
    {
      if (source.Length != target.Length)
        return false;

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var map1 = new System.Collections.Generic.Dictionary<T, T>(equalityComparer);
      var map2 = new System.Collections.Generic.Dictionary<T, T>(equalityComparer);

      for (var i = source.Length - 1; i >= 0; i--)
      {
        var c1 = source[i];
        var c2 = target[i];

        if (map1.TryGetValue(c1, out T? value))
        {
          if (!equalityComparer.Equals(c2, value))
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
  }
}
