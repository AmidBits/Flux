using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Returns whether the source contains all of the items in subset, using the default comparer.</summary>
    public static int CopyTo<T>(this System.Collections.Generic.IEnumerable<T> source, System.Array array, int arrayIndex)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (array is null) throw new System.ArgumentNullException(nameof(array));

      if (arrayIndex < 0 || arrayIndex >= array.Length) throw new System.ArgumentOutOfRangeException(nameof(arrayIndex));

      var count = 0;

      foreach (var item in source)
      {
        array.SetValue(item, arrayIndex);

        count++;
      }

      return count;
    }
  }
}
