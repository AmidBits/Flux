namespace Flux.Numerics
{
  /// <summary>
  /// <para>This interpolates between data points to find the pth empirical quantile</para>
  /// <see href="https://en.wikipedia.org/wiki/Quartile"/>
  /// </summary>
  public record class QuartileEmpirical
    : IQuartileComputable
  {
    public (TSelf q1, TSelf q2, TSelf q3) ComputeQuartiles<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        Estimate(sample, TSelf.CreateChecked(0.25)),
        Estimate(sample, TSelf.CreateChecked(0.50)),
        Estimate(sample, TSelf.CreateChecked(0.75))
      );

    public static TSelf Estimate<TSelf>(System.Collections.Generic.IEnumerable<TSelf> source, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var a = p * TSelf.CreateChecked(source.Count() + 1);
      var k = TSelf.Truncate(a);

      a -= k;

      var c = source.ElementAt(System.Convert.ToInt32(k) - 1);

      return c + a * (source.ElementAt(System.Convert.ToInt32(k)) - c);
    }
  }
}
