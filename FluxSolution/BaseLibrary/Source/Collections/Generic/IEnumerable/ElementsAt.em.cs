using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Returns a new sequence of elements based on the specified indices. Can be used to create a subset, repeating duplicates, reorder, etc.</summary>
    public static System.Collections.Generic.IEnumerable<T> ElementsAt<T>(this System.Collections.Generic.IEnumerable<T> source, params int[] indices)
    {
      var cache = new System.Collections.Generic.Dictionary<int, T>((indices ?? throw new System.ArgumentNullException(nameof(indices))).Length);

      var indexCounter = 0;

      foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        if (indices.Contains(indexCounter))
        {
          cache.Add(indexCounter, item);

          if (cache.Count == indices.Length) break;
        }

        indexCounter++;
      }

      foreach (var index in indices)
        yield return cache[index];
    }
  }
}
