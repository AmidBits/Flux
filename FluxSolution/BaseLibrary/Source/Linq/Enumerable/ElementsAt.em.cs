using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns a new sequence of elements based on the specified indices. Can be used to create a subset, repeating duplicates, reorder, etc.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> ElementsAt<T>(this System.Collections.Generic.IEnumerable<T> source, params int[] indices)
    {
      if (indices is null) throw new System.ArgumentNullException(nameof(indices));

      var index = 0;
      var cache = new System.Collections.Generic.Dictionary<int, T>(indices.Length);

      foreach (var item in source)
      {
        if (indices.Contains(index))
        {
          if (!cache.ContainsKey(index))
            cache.Add(index, item);

          if (cache.Count == indices.Length)
            break;
        }

        index++;
      }

      return indices.Select(i => cache[i]);
    }
  }
}
