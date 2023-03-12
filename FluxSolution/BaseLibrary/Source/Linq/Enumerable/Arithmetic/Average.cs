using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Compute the average of all elements in <paramref name="source"/>.</summary>
    public static TSelf Average<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.AsParallel().Aggregate(() => TSelf.Zero, (a, e, i) => a + e, (a, i) => a / TSelf.CreateChecked(i));
  }
}
