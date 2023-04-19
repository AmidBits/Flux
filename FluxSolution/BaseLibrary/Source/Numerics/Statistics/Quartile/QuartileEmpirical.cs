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
    public (double q1, double q2, double q3) ComputeQuartiles(System.Collections.Generic.IEnumerable<double> sample)
      => Compute(sample);

    public static (double q1, double q2, double q3) Compute(System.Collections.Generic.IEnumerable<double> sample)
      => (
        ComputeAt(sample, 0.25),
        ComputeAt(sample, 0.50),
        ComputeAt(sample, 0.75)
      );

    public static double ComputeAt(System.Collections.Generic.IEnumerable<double> sample, double p)
    {
      var a = p * (sample.Count() + 1);
      var k = System.Math.Truncate(a);

      a -= k;

      var c = sample.ElementAt(System.Convert.ToInt32(k) - 1);

      return c + a * (sample.ElementAt(System.Convert.ToInt32(k)) - c);
    }
  }
}
