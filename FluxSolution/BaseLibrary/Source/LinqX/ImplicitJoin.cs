using System.Linq;

namespace Flux
{
  public static partial class LinqX
  {
    public static T[] ImplicitJoin<T>(params T[][] source)
    {
      T[] ij = default!;

      for (var index = 0; index < source.Length; index++)
      {
        ij = index == 0 ? source[index] : ij.Join(source[index], outer => outer, inner => inner, (outer, inner) => inner).ToArray();
      }

      return ij;
    }
  }
}
