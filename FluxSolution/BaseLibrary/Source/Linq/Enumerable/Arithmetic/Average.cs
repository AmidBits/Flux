using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Average of all System.Numerics.INumber<TSelf> elements.</summary>
    public static TSelf Average<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.AsParallel().Aggregate(() => TSelf.Zero, (a, e, i) => a + e, (a, i) => a / TSelf.CreateChecked(i));
  }
}
