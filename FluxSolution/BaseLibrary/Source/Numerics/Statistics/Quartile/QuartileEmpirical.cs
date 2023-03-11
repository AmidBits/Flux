using System.Linq;

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
      => Compute(sample);

    public static (TSelf q1, TSelf q2, TSelf q3) Compute<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        ComputeAt(sample, TSelf.CreateChecked(0.25)),
        ComputeAt(sample, TSelf.CreateChecked(0.50)),
        ComputeAt(sample, TSelf.CreateChecked(0.75))
      );

    public static TSelf ComputeAt<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var a = p * TSelf.CreateChecked(sample.Count() + 1);
      var k = TSelf.Truncate(a);

      a -= k;

      var c = sample.ElementAt(System.Convert.ToInt32(k) - 1);

      return c + a * (sample.ElementAt(System.Convert.ToInt32(k)) - c);
    }
  }
}
