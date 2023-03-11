using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Sum of all System.Numerics.INumber<TSelf> elements.</summary>
    public static TSelf Sum<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.AsParallel().Aggregate(TSelf.Zero, (a, e) => a + e);
  }
}
