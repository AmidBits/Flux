namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Average for BigInteger.</summary>
    public static double Average(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source)
      => source.AsParallel().Aggregate(System.Numerics.BigInteger.Zero, (a, e, i) => a + e, (a, i) => (double)a / (double)i);
  }

#if NET7_0_OR_GREATER
  public static partial class Enumerable
  {
    /// <summary>Average for System.Numerics.INumber<TSelf>.</summary>
    public static TSelf Average<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.AsParallel().Aggregate(TSelf.Zero, (a, e, i) => a + e, (a, i) => a / TSelf.CreateChecked(i));
  }
#endif
}
