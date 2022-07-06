namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Average for BigInteger.</summary>
    public static double Average(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source)
      => source.AsParallel().Aggregate(System.Numerics.BigInteger.Zero, (a, e, i) => a + e, (a, i) => (double)a / (double)i);
  }
}
