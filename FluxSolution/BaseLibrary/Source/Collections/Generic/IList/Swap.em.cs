using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Swap two elements by the specified indices.</summary>
    public static System.Collections.Generic.IList<T> Swap<T>(this System.Collections.Generic.IList<T> source, int indexA, int indexB)
    {
      if (source == null)
        throw new System.ArgumentNullException(nameof(source));
      else if (source.Count == 0)
        throw new System.ArgumentException(@"The sequence is empty.");
      else if (indexA < 0 || indexA >= source.Count)
        throw new System.ArgumentOutOfRangeException(nameof(indexA));
      else if (indexB < 0 || indexB >= source.Count)
        throw new System.ArgumentOutOfRangeException(nameof(indexB));
      else if (indexA != indexB)
      {
        var tmp = source[indexA];
        source[indexA] = source[indexB];
        source[indexB] = tmp;
      }

      return source;
    }

    public static System.Collections.Generic.IList<T> SwapFirstWith<T>(this System.Collections.Generic.IList<T> source, int index)
      => Swap(source, 0, index);

    public static System.Collections.Generic.IList<T> SwapLastWith<T>(this System.Collections.Generic.IList<T> source, int index)
      => Swap(source, index, source.Count - 1);
  }
}
