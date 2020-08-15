namespace Flux
{
  public static partial class XtendString
  {
    /// <summary>Given two strings s and t, determine if they are isomorphic. Two strings are isomorphic if the characters in s can be replaced to get t.</summary>
    /// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
    public static bool IsIsomorphic(this string source, string target)
    {
      if (source.Length != target.Length)
      {
        return false;
      }

      var map1 = new System.Collections.Generic.Dictionary<char, char>();
      var map2 = new System.Collections.Generic.Dictionary<char, char>();

      for (var i = 0; i < source.Length; i++)
      {
        var c1 = source[i];
        var c2 = target[i];

        if (map1.ContainsKey(c1))
        {
          if (c2 != map1[c1])
          {
            return false;
          }
        }
        else
        {
          if (map2.ContainsKey(c2))
          {
            return false;
          }

          map1[c1] = c2;
          map2[c2] = c1;
        }
      }

      return true;
    }
  }
}
