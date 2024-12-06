namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new dictionary with all keys (by <paramref name="keySelector"/>) and indices of all occurences in the <paramref name="source"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IDictionary<char, System.Collections.Generic.List<int>> CreateIndexMap(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      var map = new System.Collections.Generic.Dictionary<char, System.Collections.Generic.List<int>>(equalityComparer ?? System.Collections.Generic.EqualityComparer<char>.Default);

      for (var index = 0; index < source.Length; index++)
      {
        var key = source[index];

        if (!map.TryGetValue(key, out var value))
        {
          value = new();

          map[key] = value;
        }

        value.Add(index);
      }

      return map;
    }
  }
}
