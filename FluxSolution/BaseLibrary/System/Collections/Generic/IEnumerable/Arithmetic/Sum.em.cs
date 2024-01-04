namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Compute the sum of all elements in <paramref name="source"/>.</summary>
    public static TSelf Sum<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.Aggregate(TSelf.Zero, (a, e) => a + e);
  }
}
