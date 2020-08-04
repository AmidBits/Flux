using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    public static System.Collections.Generic.IEnumerable<T> Repeat<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      while (count-- >= 0)
        foreach (var item in source?.ToList() ?? throw new System.ArgumentNullException(nameof(source)))
          yield return item;
    }
  }
}
