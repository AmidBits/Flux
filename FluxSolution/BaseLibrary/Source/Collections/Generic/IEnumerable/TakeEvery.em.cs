using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence with every n-th element from the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<T> TakeEvery<T>(this System.Collections.Generic.IEnumerable<T> source, int nth)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (nth <= 0) throw new System.ArgumentOutOfRangeException(nameof(nth));

      return source.Where((e, i) => i % nth == 0);
    }
  }
}
